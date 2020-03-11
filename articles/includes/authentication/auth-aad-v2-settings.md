
<!-- Azure AD v2 settings -->

The Microsoft identity platform (v2.0), also known as **Azure AD v2** endpoint, is an evolution of the Azure AD platform (v1.0). 
It allows a bot to get tokens to call Microsoft APIs, such as Microsoft Graph or custom APIs. 
For more information, see the [Microsoft identity platform (v2.0) overview](https://docs.microsoft.com/azure/active-directory/develop/active-directory-appmodel-v2-overview).

The AD v2 settings shown below enable a bot to access Office 365 data via the Microsoft Graph API.

1. Navigate to your bot's Bot Channels Registration page on the [Azure Portal](https://portal.azure.com/).
1. Click **Settings**.
1. Under **OAuth Connection Settings** near the bottom of the page, click **Add Setting**.
1. Fill in the form as follows:

    1. For **Name**, enter a name for your connection. You'll use it in your bot code.
    1. For **Service Provider**, select **Azure Active Directory v2**. Once you select this, the Azure AD-specific fields will be displayed.
    1. For **Client id**, enter the application (client) ID that you recorded for your Azure AD v1 application.
    1. For **Client secret**, enter the secret that you created to grant the bot access to the Azure AD app.
    1. For **Tenant ID**, enter the **directory (tenant) ID** that your recorded earlier for your AAD app or **common** depending on the supported account types selected when you created the ADD app. To decide which value to assign follow these criteria:

        - When creating the AAD app if you selected either *Accounts in this organizational directory only (Microsoft only - Single tenant)* or *Accounts in any organizational directory(Microsoft AAD directory - Multi tenant)* enter the **tenant ID** you recorded earlier for the AAD app.

        - However, if you selected *Accounts in any organizational directory (Any AAD directory - Multi tenant and personal Microsoft accounts e.g. Skype, Xbox, Outlook.com)* enter the word **common** instead of a tenant ID. Otherwise, the AAD app will verify through the tenant whose ID was selected and exclude personal MS accounts.

       This will be the tenant associated with the users who can be authenticated.

    1. For **Scopes**, enter the names of the permission you chose from application registration:
       `Mail.Read Mail.Send openid profile User.Read User.ReadBasic.All`.

        > [!NOTE]
        > For Azure AD v2, **Scopes** takes a case-sensitive, space-separated list of values.

1. Click **Save**.
