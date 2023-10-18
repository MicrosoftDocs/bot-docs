---
title: User authentication in the Azure AI Bot Service - Bot Service
description: Learn about user authentication features in the Azure AI Bot Service. See how bots use OAuth connections to sign in users and access secured online resources.
keywords: Azure AI Bot Service, authentication, bot framework token service
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: micchow
ms.topic: conceptual
ms.service: bot-service
ms.date: 08/08/2022
monikerRange: 'azure-bot-service-4.0'
---

# User authentication

[!INCLUDE [applies-to-v4](../includes/applies-to-v4-current.md)]

At times a bot must access secured online resources on behalf of the user, such as checking email, checking on flight status, or placing an order. The user must authorize the bot to do so on their behalf, and in order to authorize the bot, the user must authenticate their identity. _OAuth_ is used to authenticate the user and authorize the bot. See also [Authentication types](bot-builder-concept-authentication-types.md).

If you want to refresh your OAuth knowledge, see the following:

- [Good OAuth overview](https://aaronparecki.com/oauth-2-simplified/) easier to follow than the formal specification
- [OAuth specification](https://oauth.net/2/)

## User authentication in a conversation

To perform certain operations on behalf of a user, such as checking email, referencing a calendar, checking on flight status, or placing an order, the bot will need to call an external service, such as the Microsoft Graph, GitHub, or a company's REST service.
Each external service has a way of securing those calls. A common way to issue those requests is to use a _user token_ that uniquely identifies the user on that external service (sometimes referred to as a [JSON Web Token](https://jwt.io/introduction/) (JWT)).

To secure the call to an external service, the bot must ask the user to sign-in, so it can acquire the user's token for that service.
Many services support token retrieval via the **OAuth** or **OAuth2** protocol.

The Azure AI Bot Service provides specialized **sign-in** cards and services that work with the OAuth protocol and manage the token life-cycle. A bot can use these features to acquire a user token.

- As part of bot configuration, an **OAuth connection** is registered within the Azure AI Bot Service resource in Azure.

    The connection contains information about the **identity provider** to use, along with a valid OAuth client ID and secret, the OAuth scopes to enable, and any other connection metadata required by that identity provider.

- In the bot's code, the OAuth connection is used to help sign-in the user and get the user token.

The following image shows the elements involved in the authentication process.

:::image type="content" source="media/concept-bot-authentication/bot-auth-components.png" alt-text="Diagram illustrating the relationship between authentication components in Azure AI Bot Service.":::

## About the Bot Framework Token Service

The Bot Framework Token Service is responsible for:

- Facilitating the use of the OAuth protocol with a wide variety of external services.
- Securely storing tokens for a particular bot, channel, conversation, and user.
- Acquiring user tokens.
  > [!TIP]
  > If the bot has an expired user token, the bot should:
  >
  > - Log the user out
  > - Initiate the sign-in flow again

For example, a bot that can check a user's recent emails, using the Microsoft Graph API, requires a user token from an **Identity Provider**, in this case **Microsoft Entra ID**. At design time, the bot developer performs these two important steps:

1. Registers an Entra ID application, an Identity Provider, with the Bot Framework Token Service, via the Azure portal.
1. Configures an OAuth connection (named for example `GraphConnection`) for the bot.

The following picture shows the time sequence of the user's interaction with a bot when an email request is made using the Microsoft Graph service.

:::image type="content" source="media/concept-bot-authentication/bot-auth-time-sequence.PNG" alt-text="Sequence diagram outlining the steps for a bot to send an email on behalf of a user.":::

1. The user makes an email request to the bot.
1. An activity with this message is sent from the user to the Bot Framework channel service. The channel service ensures that the `userid` field within the activity has been set and the message is sent to the bot.

    > [!NOTE]
    > User ID's are channel specific, such as the user's Facebook ID or their SMS phone number.

1. The bot makes a request to the Bot Framework Token Service asking if it already has a token for the UserId for the OAuth connection `GraphConnection`.
1. Since this is the first time this user has interacted with the bot, the Bot Framework Token Service doesn't yet have a token for this user, and returns a _NotFound_ result to the bot.

    > [!NOTE]
    > If the token is found, the authentication steps are skipped and the bot can make the email request using the stored token.

1. The bot creates an OAuthCard with a connection name of `GraphConnection` and replies to the user asking to sign-in using this card.
1. The activity passes through the Bot Framework Channel Service, which calls into the Bot Framework Token Service to create a valid OAuth sign-in URL for this request. This sign-in URL is added to the OAuthCard and the card is returned to the user.
1. The user is presented with a message to sign-in by clicking on the OAuthCard's sign-in button.
1. When the user clicks the sign-in button, the channel service opens a web browser and calls out to the external service to load its sign-in page.
1. The user signs-in to this page for the external service. Then the external service completes the OAuth protocol exchange with the Bot Framework Token Service, resulting in the external service sending the Bot Framework Token Service the user token. The Bot Framework Token Service securely stores this token and sends an activity to the bot with this token.
1. The bot receives the activity with the token and is able to use it to make calls against the MS Graph API.

## Securing the sign-in URL

An important consideration when the Bot Framework facilitates a user login is how to secure the sign-in URL. When a user is presented with a sign-in URL, this URL is associated with a specific conversation ID and user ID for that bot. Don't share this URL&mdash;it would cause the wrong sign-in to occur for a particular bot conversation. To mitigate security attacks that use a shared sign-in URL, ensure that the machine and person who clicks on the sign-in URL is the person who _owns_ the conversation window.

Some channels such as Microsoft Teams, Direct Line, and WebChat are able to do this without the user noticing. For example, WebChat uses session cookies to ensure that the sign-in flow took place in the same browser as the WebChat conversation. However, for other channels the user is often presented with a 6-digit _magic code_. This is similar to a built-in multi-factor authentication, as the Bot Framework Token Service won't release the token to the bot unless the user finishes the final authentication, proving that the person who signed-in has access to the chat experience by entering the 6-digit code.

> [!IMPORTANT]
> Please, keep in mind these important [Security considerations](~/rest-api/bot-framework-rest-direct-line-3-0-authentication.md#security-considerations).
> You can find additional information in this blog post: [Using WebChat with Azure AI Bot Service Authentication](https://blog.botframework.com/2018/09/01/using-webchat-with-azure-bot-services-authentication/).

## Next steps

Now that you know about user authentication, let's take a look at how to apply that to your bot.

> [!div class="nextstepaction"]
> [Add authentication to a bot](bot-builder-authentication.md)

## See also

- [Identity providers](bot-builder-concept-identity-providers.md)
- [REST Connector authentication](/azure/bot-service/rest-api/bot-framework-rest-connector-authentication?view=azure-bot-service-4.0&preserve-view=true)
- [REST Direct Line authentication](/azure/bot-service/rest-api/bot-framework-rest-direct-line-3-0-authentication?view=azure-bot-service-4.0&preserve-view=true)
