In this step you create a **Bot Channels Registration** application that allows the following:

- User's interaction with the bot via a set of channels such as *Web Chat*.
- The definition of *OAuth Connection Settings* to authenticate a user and to create a *token* used by the bot to access protected resources on behalf of the user.

> [!NOTE] Because you are deploying a locally developed bot, you need a **Bot Channels Registration** resource.
> This creates the *container* to which you can deploy your bot code, as opposed to a *Web App Bot* which would create an app on Azure as well.

To create a bot channels registration, execute the following command:

```cmd
az ad app create --display-name "displayName" --password "AtLeastSixteenCharacters_0" --available-to-other-tenants
```

| Option   | Description |
|:---------|:------------|
| display-name | The name of the *bot channels registration* application. It is listed in the Azure portal in the general resources list and in the resource group it belongs.|
| password | The *bot channels registration* application password, also known as **client secret**. This is a password you create for this resource. It must be at least 16 characters long, contain at least 1 upper or lower case alphabetical character, and contain at least 1 special character.|
| available-to-other-tenants| Indicates whether the application can be used from any Azure AD tenant. Set to `true` to enable your bot to work with the Azure Bot Service channels.|

The above command outputs JSON with the key `appId`, copy and save it.
You are going to use this `appId` and the password you entered in the ARM deployment step, to assign values to the `appId` and the `appSecret` parameters, respectively.
