In this step you create a **Bot Channel Registration** application that allows the following:

- User's interaction with the bot via a set of channels such as *Web Chat*.
- The definition of *OAuth Connection Settings* to authenticate a user and to create a *token* used by the bot to access protected resources on behalf of the user.

To create a bot channels registration, perform the following command:

```cmd
az ad app create --display-name "displayName" --password "AtLeastSixteenCharacters_0" --available-to-other-tenants
```

| Option   | Description |
|:---------|:------------|
| display-name | The name of the *bot channel registration* application. It is listed in the Azure portal in the general resources list and in the resource group it belongs.|
| password | The *bot channel registration* application password, also known as **client secret**. This is the password you created. It must be at least 16 characters long, contain at least 1 upper or lower case alphabetical character, and contain at least 1 special character.|
| available-to-other-tenants| Indicates whether the application can be used from any Azure AD tenant. Set to `true` to enable your bot to work with the Azure Bot Service channels.|

The above command outputs JSON with the key `appId`, copy and save it.
You are going to use this `appId` and the password you entered in the ARM deployment step, to assign values to the `appId` and the `appSecret` parameters, respectively.

> [!NOTE]
> If you would like to use an existing App registration you can use the command:
> ``` cmd
> az bot create --kind webapp --resource-group "<name-of-resource-group>" --name "<name-of-registration-app>" --appid "<existing-app-id>" --password "<existing-app-password>" --lang <Javascript|Csharp>
> ```
