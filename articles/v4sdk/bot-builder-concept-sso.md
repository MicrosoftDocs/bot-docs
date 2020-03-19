# Overview
Single Sign on allows a client (virtual assistant/webchat) to call into a bot or skill on behalf of the user.
Currently, only AAD V2 is supported.
There are 2 scenarios that would be addressed through single sign on
- For a virtual assistant and multiple skill bots, a user can sign in once into the virtual assistant and the virtual assistant can invoke multiple skills on behalf of the user.
- A webchat embedded into a website can call into a bot or a skill on behalf of the user signed into the website.
SSO has the following advantages
    - The user does not have to login again, if they are already logged into the Virtual assitant or into the parent website.
    - The parent website/ Virtual assistant does not get any user permissions. Only the skil/bot does.

# Architecture
The following sequence diagram explains the interactions between the various components for SSO to work.
SSO falls back to the existing behaviour of showing the OAuth card, in case of failure scenarios like if user consent is required or token exchange fails.
- The happy path flow is as shown in the below sequence diagram-
- The fallback path flow is as shown in the below sequence diagram-
1) Client starts a conversation with the bot triggering an OAuth scenario.
2) Bot sends back an OAuth Card to the client .
3) The client intercepts the OAuth card before displaying it to the user and checks if it has a `TokenExchangeResource` property in it. If it does, then the client sends a `TokenExchangeInvokeRequest` to the bot. The client needs to have an exchangeable token for the user , which must be an AAD V2 token and whose audience must be the same as `TokenExchangeResource.Uri` property. For a sample on how to get the user's exchangeable token , please refer to this [Webchat Sample](https://linkrequired). The client sends an Invoke activity to the bot with the body as below
```json
{
    "type": "Invoke",
    "name": "signin/tokenExchange",
    "value": {
        "id": "<any unique Id>",
        "connectionName": "<connection Name on the skill bot (from the OAuth Card)>",
        "token": "<exchangeable token>"
    }
}
```
4) The bot processes the `TokenExchangeInvokeRequest` and returns a `TokenExchangeInvokeResponse` back to the client. The
client should wait till it receives the `TokenExchangeInvokeResponse`.
```json
{
    "status": "<response code>",
    "body": {
        "id":"<unique Id>",
        "connectionName": "<connection Name on the skill bot (from the OAuth Card)>",
        "failureDetail": "<failure reason if status code is not 200, null otherwise>"
    }
}
```
5) If the `TokenExchangeInvokeResponse` has a `status` of `200`, then the client does not show the OAuth card. For any other `status` or if the `TokenExchangeInvokeResponse` is not received, then the client shows the OAuth card to the user. This ensures that the SSO flow falls back to normal OAuthCard flow, incase of any errors or unmet dependencies like user consent etc.

# Create AAD applications
Currently SSO in botframework is only supported for aadV2 apps.
We need to create 2 applications - one for the client and one for the Bot.
Depending on the scenario, the client may be webchat or a virtual assistant.
The general case for a Bot would be a skill Bot.
## Client AAD app
The client AAD application will be used to create an exchangeable token that will be passed onto the bot.
For an example of how to create an AAD app, look at the [bot builder authentication docs](https://docs.microsoft.com/en-us/azure/bot-service/bot-builder-authentication?view=azure-bot-service-4.0&tabs=csharp#create-your-azure-ad-application).
## Service AAD app
1) Follow the steps on [Create your Azure AD application](https://docs.microsoft.com/en-us/azure/bot-service/bot-builder-authentication?view=azure-bot-service-4.0&tabs=csharp#create-your-azure-ad-application).
2) In the **Expose an api** panel, click **Add a scope**
    - Fill in the fields
    - Click the **Add scope button**.
    - Click the **Add a client application** button, and enter the app Id for the client AAD app. Select the Scope that you created in the previous step. This ensures that the user will not be asked to consent when the client tries to get an exchangeable token for this app's scope
3) In the **Manifest** panel, set the `accessTokenAcceptedVersion` key to be `2`.

## Service Auth Connection
1) Follow the directions in the [bot builder authentication doc](https://docs.microsoft.com/en-us/azure/bot-service/bot-builder-authentication?view=azure-bot-service-4.0&tabs=csharp#azure-ad-v2)
2) In the **Expose an api** panel, copy the scope that you added earlier. Fill it in the **Token Exchange Uri** field.
3) Save the connection setting.