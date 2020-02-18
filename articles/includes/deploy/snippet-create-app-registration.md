In this step you create an Azure Active Directory application, which will allow:

- The user to interact with the bot via a set of channels such as *Web Chat*.
- The definition of *OAuth Connection Settings* to authenticate a user and to create a *token* used by the bot to access protected resources on behalf of the user.

To create an Azure Active Directory application, execute the following command:

```cmd
az ad app create --display-name "displayName" --password "AtLeastSixteenCharacters_0" --available-to-other-tenants
```

| Option   | Description |
|:---------|:------------|
| display-name | The display name of the application. It is listed in the Azure portal in the general resources list and in the resource group it belongs.|
| password | The password, also known as **client secret**, for the application. This is a password you create for this resource. It must be at least 16 characters long, contain at least 1 upper or lower case alphabetical character, and contain at least 1 special character.|
| available-to-other-tenants| Indicates that the application can be used from any Azure AD tenant. Set this to enable your bot to work with the Azure Bot Service channels.|

The above command outputs JSON with the key `appId`, copy and save it.
You are going to use this `appId` and the password you entered in the ARM deployment step, to assign values to the `appId` and the `appSecret` parameters, respectively.
