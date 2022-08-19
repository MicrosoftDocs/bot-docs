---
title: Direct Line enhanced authentication
description: Learn about potential security risks when users connect to a bot and how Direct Line enhanced authentication can mitigate some risks.
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: micchow
ms.topic: conceptual
ms.service: bot-service
ms.date: 08/18/2022
---

# Direct Line enhanced authentication

[!INCLUDE [applies-to-v4](../includes/applies-to-v4-current.md)]

This article describes potential security risks when users connect to a bot, for example using the [Web Chat](../bot-service-channel-connect-webchat.md#embed-the-web-chat-control-in-a-web-page) control. Also, it shows mitigating solutions using the Direct Line [enhanced authentication settings](../bot-service-channel-connect-directline.md#configure-settings) and secure _user ID_ handling.

There are two user identities:

- The channel user's identity. An attacker can use it for [Impersonation](#impersonation).
- The user's identity from the identity provider that the bot uses to authenticate the user. An attacker can use it for [Identity spoofing](#identity-spoofing).

## Impersonation

Impersonation refers to the action of an attacker who makes the bot think that they're someone else. For example, in Web Chat, the attacker can impersonate someone else by _changing the user ID_ of the Web Chat instance.

### Impersonation mitigation

- Make the user ID _unguessable_.
- [Connect a bot to Direct Line](../bot-service-channel-connect-directline.md).
- Enable the Direct Line channel's [enhanced authentication](../bot-service-channel-connect-directline.md#configure-settings) option to allow the Azure Bot Service to further detect and reject any user ID change. This means the user ID (`Activity.From.Id`) on messages from Direct Line to the bot will always be the same as the one you used to initialize the Web Chat control.

    > [!NOTE]
    > Direct Line creates a _token_ based on the Direct Line secret and embeds the `User.Id` in the token.
    > It assures that the messages sent to the bot have that `User.Id` as the activity's `From.Id`. If a client sends a message to Direct Line having a different `From.Id`, it will be changed to the _ID embedded in the token_ before forwarding the message to the bot. So you cannot use another user ID after a channel secret is initialized with that ID.

    This feature requires the user ID to start with `dl_` as shown below.

    > [!TIP]
    > For a regional bot, set `dlUrl` to "https://westeurope.directline.botframework.com/v3/directline/tokens/generate".
    > For more information about regional bots, see [Regionalization in Azure Bot Service](bot-builder-concept-regionalization.md).

    ```csharp
    public class HomeController : Controller
    {
        private const string secret = "<TODO: DirectLine secret>";
        private const string dlUrl = "https://directline.botframework.com/v3/directline/tokens/generate";
    
        public async Task<ActionResult> Index()
        {
            HttpClient client = new HttpClient();
            var userId = $"dl_{Guid.NewGuid()}";
    
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, dlUrl);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", secret);
            request.Content = new StringContent(
                JsonConvert.SerializeObject(
                    new { User = new { Id = userId } }),
                    Encoding.UTF8,
                    "application/json");
    
            var response = await client.SendAsync(request);
    
            string token = String.Empty;
            if (response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync();
                token = JsonConvert.DeserializeObject<DirectLineToken>(body).token;
            }
    
            var config = new ChatConfig()
            {
                Token = token,
                UserId = userId
            };
    
            return View(config);
        }
    }    
    
    ```

    The generated token, based on the Direct Line secret, is then used in the Web Chat control as shown below:

    ```csharp
    @model Bot_Auth_DL_Secure_Site_MVC.Models.ChatConfig
    @{
        ViewData["Title"] = "Home Page";
    }
    <div id="webchat" role="main" />
    <head>
        <script src="https://cdn.botframework.com/botframework-webchat/latest/webchat.js"></script>
    </head>
    <body>
        <script>
          window.WebChat.renderWebChat({
              directLine: window.WebChat.createDirectLine({ token: '@Model.Token' }),
                userID: '@Model.UserId'
          }, document.getElementById('webchat'));
        </script>
    </body>

    ```

## Identity spoofing

Identity spoofing refers to the action of an attacker that assumes the identity of a legitimate user and then uses that identity to accomplish a malicious goal.

When a bot asks the channel user A to sign-in to an identity provider, the sign-in process must assure that user A is the only one that signs into the provider. If another user is also allowed to sign-in the provider, they would have access to user A resources through the bot.

### User identity spoofing mitigation

In the Web Chat control, there are two mechanisms to assure that the proper user is signed in.

1. **Magic code**. At the end of the sign-in process, the user is presented with a randomly generated 6-digit code (_magic code_). The user must type this code in the conversation to complete the sign-in process. This tends to result in a bad user experience. Additionally, it's susceptible to phishing attacks; a malicious user can trick another user to sign-in and obtain the magic code.

1. **Direct Line enhanced authentication**. Use Direct Line enhanced authentication to guarantee that the sign-in process can only be completed in the _same browser session_ as the Web Chat client.

    To enable this protection, start Web Chat with a Direct Line token that contains a list of _trusted domains that can host the bot's Web Chat client_. With enhanced authentication options, you can statically specify the trusted domains (trusted origins) list in the Direct Line configuration page. See the [Configure enhanced authentication](../bot-service-channel-connect-directline.md#configure-enhanced-authentication) section.
