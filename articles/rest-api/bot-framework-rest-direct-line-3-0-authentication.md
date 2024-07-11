---
title: Direct Line Authentication in Azure AI Bot Service
description: Become familiar with authentication in version 3.0 of the Direct Line API. See how to use secrets and tokens. Learn about Azure AI Bot Service authentication.
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: micchow
ms.service: bot-service
ms.topic: overview
ms.date: 10/18/2021
ms.custom:
  - evergreen
---

# Authentication in Direct Line API 3.0

A client can authenticate requests to Direct Line API 3.0 either by using a **secret** that you [obtain from the Direct Line channel configuration page](../bot-service-channel-connect-directline.md) in the Bot Framework Portal or by using a **token** that you obtain at runtime. The secret or token should be specified in the `Authorization` header of each request, using this format:

```http
Authorization: Bearer SECRET_OR_TOKEN
```

## Secrets and tokens

A Direct Line **secret** is a master key that can be used to access any conversation that belongs to the associated bot. A **secret** can also be used to obtain a **token**. Secrets don't expire.

A Direct Line **token** is a key that can be used to access a single conversation. A token expires but can be refreshed.

Deciding when or if to use the **secret** key or a **token** must be based on security considerations.
Exposing the secret key could be acceptable if done intentionally and with care. As matter of a fact, this is the default behavior because this allows Direct Line to figure out if the client is legitimate.
Generally speaking though, security is a concern if you're trying to persist user data.
For more information, see section [Security considerations](#security-considerations).

If you're creating a service-to-service application, specifying the **secret** in the `Authorization` header of Direct Line API requests may be simplest approach. If you're writing an application where the client runs in a web browser or mobile app, you may want to exchange your secret for a token (which only works for a single conversation and will expire unless refreshed) and specify the **token** in the `Authorization` header of Direct Line API requests. Choose the security model that works best for you.

> [!NOTE]
> Your Direct Line client credentials are different from your bot's credentials. This enables you to revise your keys independently and lets you share client tokens without disclosing your bot's password.

## Get a Direct Line secret

You can [obtain a Direct Line secret](../bot-service-channel-connect-directline.md) via the Direct Line channel configuration page for your bot in the [Azure portal](https://portal.azure.com):

:::image type="content" source="../media/direct-line-configure.png" alt-text="Direct Line configuration":::

<a id="generate-token"></a>

## Generate a Direct Line token

To generate a Direct Line token that can be used to access a single conversation, first obtain the Direct Line secret from the Direct Line channel configuration page in the [Azure portal](https://portal.azure.com). Then issue this request to exchange your Direct Line secret for a Direct Line token:

```http
POST https://directline.botframework.com/v3/directline/tokens/generate
Authorization: Bearer SECRET
```

In the `Authorization` header of this request, replace **SECRET** with the value of your Direct Line secret.

The following snippets provide an example of the Generate Token request and response.

### Request

```http
POST https://directline.botframework.com/v3/directline/tokens/generate
Authorization: Bearer RCurR_XV9ZA.cwA.BKA.iaJrC8xpy8qbOF5xnR2vtCX7CZj0LdjAPGfiCpg4Fv0
```

The request payload, which contains the token parameters, is optional but recommended. When generating a token that can be sent back to the Direct Line service, provide the following payload to make the connection more secure. By including these values, Direct Line can perform additional security validation of the user ID and name, inhibiting tampering of these values by malicious clients. Including these values also improves Direct Line's ability to send the _conversation update_ activity, allowing it to generate the conversation update immediately upon the user joining the conversation. When this information isn't provided, the user must send content before Direct Line can send the conversation update.

```json
{
  "user": {
    "id": "string",
    "name": "string"
  },
  "trustedOrigins": [
    "string"
  ]
}
```

| Parameter | Type | Description |
| :--- | :--- | :--- |
| `user.id` | string | Optional. Channel-specific ID of the user to encode within the token. For a Direct Line user, this must begin with `dl_`. You can create a unique user ID for each conversation, and for better security, you should make this ID unguessable. |
| `user.name` | string | Optional. The display-friendly name of the user to encode within the token. |
| `trustedOrigins` | string array | Optional. A list of trusted domains to embed within the token. These are the domains that can host the bot's Web Chat client. This should match the list in the Direct Line configuration page for your bot. |

### Response

If the request is successful, the response contains a `token` that is valid for one conversation and an `expires_in` value that indicates the number of seconds until the token expires. For the token to remain useful, you must [refresh the token](#refresh-token) before it expires.

```http
HTTP/1.1 200 OK
[other headers]
```

```json
{
  "conversationId": "abc123",
  "token": "RCurR_XV9ZA.cwA.BKA.iaJrC8xpy8qbOF5xnR2vtCX7CZj0LdjAPGfiCpg4Fv0y8qbOF5xPGfiCpg4Fv0y8qqbOF5x8qbOF5xn",
  "expires_in": 1800
}
```

### Generate Token versus Start Conversation

The Generate Token operation (`POST /v3/directline/tokens/generate`) is similar to the [Start Conversation](bot-framework-rest-direct-line-3-0-start-conversation.md) operation (`POST /v3/directline/conversations`) in that both operations return a `token` that can be used to access a single conversation. However, unlike the Start Conversation operation, the Generate Token operation doesn't start the conversation, doesn't contact the bot, and doesn't create a streaming WebSocket URL.

If you plan to distribute the token to clients and want them to initiate the conversation, use the Generate Token operation. If you intend to start the conversation immediately, use the [Start Conversation](bot-framework-rest-direct-line-3-0-start-conversation.md) operation instead.

<a id="refresh-token"></a>

## Refresh a Direct Line token

A Direct Line token can be refreshed an unlimited number of times, as long as it hasn't expired. An expired token can't be refreshed. To refresh a Direct Line token, issue this request:

```http
POST https://directline.botframework.com/v3/directline/tokens/refresh
Authorization: Bearer TOKEN_TO_BE_REFRESHED
```

In the `Authorization` header of this request, replace **TOKEN_TO_BE_REFRESHED** with the Direct Line token that you want to refresh.

The following snippets provide an example of the Refresh Token request and response.

### Request

```http
POST https://directline.botframework.com/v3/directline/tokens/refresh
Authorization: Bearer CurR_XV9ZA.cwA.BKA.iaJrC8xpy8qbOF5xnR2vtCX7CZj0LdjAPGfiCpg4Fv0y8qbOF5xPGfiCpg4Fv0y8qqbOF5x8qbOF5xn
```

### Response

If the request is successful, the response contains a new `token` that is valid for the same conversation as the previous token and an `expires_in` value that indicates the number of seconds until the new token expires. For the new token to remain useful, you must [refresh the token](#refresh-token) before it expires.

```http
HTTP/1.1 200 OK
[other headers]
```

```json
{
  "conversationId": "abc123",
  "token": "RCurR_XV9ZA.cwA.BKA.y8qbOF5xPGfiCpg4Fv0y8qqbOF5x8qbOF5xniaJrC8xpy8qbOF5xnR2vtCX7CZj0LdjAPGfiCpg4Fv0",
  "expires_in": 1800
}
```

## Azure AI Bot Service authentication

The information presented in this section is based on the [Add authentication to your bot via Azure AI Bot Service](../v4sdk/bot-builder-authentication.md) article.

**Azure AI Bot Service authentication** enables you to authenticate users to and get **access tokens** from various identity providers such as _Microsoft Entra ID_, _GitHub_, _Uber_ and so on. You can also configure authentication for a custom **OAuth2** identity provider. All this enables you to write **one piece of authentication code** that works across all supported identity providers and channels. To use these capabilities:

1. Statically configure `settings` on your bot that contains the details of your application registration with an identity provider.
1. Use an `OAuthCard`, backed by the application information you supplied in the previous step, to sign-in a user.
1. Retrieve access tokens through **Azure AI Bot Service API**.

### Security considerations

<!-- Summarized from: https://blog.botframework.com/2018/09/25/enhanced-direct-line-authentication-features/ -->

When you use _Azure AI Bot Service authentication_ with [Web Chat](../bot-service-channel-connect-webchat.md), there are some important security considerations you must keep in mind.

1. **Impersonation**. Impersonation is when an attacker convinces the bot that the attacker is someone else. In Web Chat, an attacker can impersonate someone else by _changing the user ID_ of their Web Chat instance. To prevent impersonation, we recommend that bot developers make the _user ID unguessable_.

    If you enable **enhanced authentication** options, Azure AI Bot Service can further detect and reject any user ID change. This means the user ID (`Activity.From.Id`) on messages from Direct Line to your bot will always be the same as the one you initialized the Web Chat with. This feature requires the user ID to start with `dl_`.

    > [!NOTE]
    > When a _User.Id_ is provided while exchanging a secret for a token, that _User.Id_ is embedded in the token. Direct Line makes sure the messages sent to the bot have that ID as the activity's _From.Id_. If a client sends a message to Direct Line having a different _From.Id_, it will be changed to the _ID in the token_ before forwarding the message to the bot. So you can't use another user ID after a channel secret is initialized with a user ID

1. **User identities**. Each user has multiple user identities:

    1. The user's identity in a channel.
    1. The user's identity in an identity provider that the bot is interested in.

When a bot asks user A in a channel to sign-in to an identity provider P, the sign-in process must assure that user A is the one that signs into P. If another user B is allowed to sign-in, then user A would have access to user B's resource through the bot. In Web Chat, we have two mechanisms for ensuring the right user signed in as described next.

1. At the end of sign-in, in the past, the user was presented with a randomly generated 6-digit code (magic code). The user must type this code in the conversation that initiated the sign-in to complete the sign-in process. This mechanism tends to result in a bad user experience. Additionally, it's still susceptible to phishing attacks. A malicious user can trick another user to sign-in and obtain the magic code through phishing.

1. Because of the issues with the previous approach, Azure AI Bot Service removed the need for the magic code. Azure AI Bot Service guarantees that the sign-in process can only be completed in the **same browser session** as the Web Chat itself. To enable this protection, as a bot developer, you must start Web Chat with a **Direct Line token** that contains a **list of trusted domains that can host the bot's Web Chat client**. Before, you could only obtain this token by passing an undocumented optional parameter to the Direct Line token API. Now, with enhanced authentication options, you can statically specify the trusted domain (origin) list in the Direct Line configuration page.

For more information, see [Add authentication to your bot via Azure AI Bot Service](../v4sdk/bot-builder-authentication.md).

### Code examples

The following .NET controller works with enhanced authentication options enabled and returns a Direct Line Token and user ID.

```csharp
public class HomeController : Controller
{
    public async Task<ActionResult> Index()
    {
        var secret = GetSecret();

        HttpClient client = new HttpClient();

        HttpRequestMessage request = new HttpRequestMessage(
            HttpMethod.Post,
            $"https://directline.botframework.com/v3/directline/tokens/generate");

        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", secret);

        var userId = $"dl_{Guid.NewGuid()}";

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

public class DirectLineToken
{
    public string conversationId { get; set; }
    public string token { get; set; }
    public int expires_in { get; set; }
}
public class ChatConfig
{
    public string Token { get; set; }
    public string UserId { get; set; }
}

```

The following JavaScript controller works with enhanced authentication options enabled and returns a Direct Line Token and user ID.

```javascript
var router = express.Router(); // get an instance of the express Router

// Get a directline configuration (accessed at GET /api/config)
const userId = "dl_" + createUniqueId();

router.get('/config', function(req, res) {
    const options = {
        method: 'POST',
        uri: 'https://directline.botframework.com/v3/directline/tokens/generate',
        headers: {
            'Authorization': 'Bearer ' + secret
        },
        json: {
            User: { Id: userId }
        }
    };

    request.post(options, (error, response, body) => {
        if (!error && response.statusCode < 300) {
            res.json({
                    token: body.token,
                    userId: userId
                });
        }
        else {
            res.status(500).send('Call to retrieve token from Direct Line failed');
        }
    });
});

```

## Additional information

- [Key concepts](bot-framework-rest-direct-line-3-0-concepts.md)
- [Connect a bot to Direct Line](../bot-service-channel-connect-directline.md)
- [Add authentication to your bot via Azure AI Bot Service](../bot-builder-tutorial-authentication.md)
