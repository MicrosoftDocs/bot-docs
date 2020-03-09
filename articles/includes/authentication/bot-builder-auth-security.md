
## Authentication security considerations

<!-- Summarized from: https://blog.botframework.com/2018/09/25/enhanced-direct-line-authentication-features/ -->

When you use *Azure Bot Service authentication* with [Web Chat](../bot-service-channel-connect-webchat.md) there are some important security considerations you must keep in mind.

1. **Impersonation**. Impersonation here means an attacker makes the bot thinks he is someone else. In Web Chat, an attacker can impersonate someone else by **changing the user ID** of his Web Chat instance. To prevent this, it is recommended to bot developers to make the **user ID unguessable**.

    If you enable **enhanced authentication** options, Azure Bot Service can further detect and reject any user ID change. This means the user ID (`Activity.From.Id`) on messages from Direct Line to your bot will always be the same as the one you initialized the Web Chat with. Note that this feature requires the user ID starts with `dl_`.

    > [!NOTE]
    > When a *User.Id* is provided while exchanging a secret for a token, that *User.Id* is embedded in the token. DirectLine males sure the messages sent to the bot have that id as the activity's *From.Id*. If a client sends a message to DirectLine having a different *From.Id*, it will be changed to the **Id in the token** before forwarding the message to the bot. So you cannot use another user id after a channel secret is initialized with a user id

1. **User identities**. You must be aware that your are dealing with two user identities:

    1. The user’s identity in a channel.
    1. The user’s identity in an identity provider that the bot is interested in.

    When a bot asks user A in a channel to sign-in to an identity provider P, the sign-in process must assure that user A is the one that signs into P.
    If another user B is allowed to sign-in, then user A would have access to user B’s resource through the bot. In Web Chat we have 2 mechanisms for ensuring the right user signed in as described next.

    1. At the end of sign-in, in the past, the user was presented with a randomly generated 6-digit code (aka magic code). The user must type this code in the conversation that initiated the sign-in to complete the sign-in process. This mechanism tends to result in a bad user experience. Additionally, it is still susceptible to phishing attacks. A malicious user can trick another user to sign-in and obtain the magic code through phishing.

    2. Because of the issues with the previous approach, Azure Bot Service removed the need for the magic code. Azure Bot Service guarantees that the sign-in process can only be completed in the **same browser session** as the Web Chat itself.
    To enable this protection, as a bot developer, you must start Web Chat with a **Direct Line token** that contains a **list of trusted domains that can host the bot’s Web Chat client**. Before, you could only obtain this token by passing an undocumented optional parameter to the Direct Line token API. Now, with enhanced authentication options, you can statically specify the trusted domain (origin) list in the Direct Line configuration page.

    See also [Add authentication to your bot via Azure Bot Service](../v4sdk/bot-builder-authentication.md).
