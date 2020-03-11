<!-- Azure AD v1 settings -->

Azure AD developer platform (v1.0), also known as **Azure AD v1** endpoint, allows to build  apps that securely sign in 
users with a Microsoft work or school account.
For more information, see [Azure Active Directory for developers (v1.0) overview](https://docs.microsoft.com/azure/active-directory/azuread-dev/v1-overview).

The AD v1 settings shown below enable a bot to access Office 365 data via the Microsoft Graph API.

1. Navigate to your bot's resource page on the [Azure Portal](https://portal.azure.com/).
1. Click **Settings**.
1. Under **OAuth Connection Settings** near the bottom of the page, click **Add Setting**.
1. Fill in the form as follows:

    1. For **Name**, enter a name for your connection. You'll use this name in your bot code.
    1. For **Service Provider**, select **Azure Active Directory**. Once you select this, the Azure AD-specific fields will be displayed.
    1. For **Client id**, enter the application (client) ID that you recorded for your Azure AD v1 application.
    1. For **Client secret**, enter the secret that you created to grant the bot access to the Azure AD app.
    1. For **Grant Type**, enter `authorization_code`.
    1. For **Login URL**, enter `https://login.microsoftonline.com`.
    1.For **Tenant ID**, enter the **directory (tenant) ID** that your recorded earlier for your AAD app or **common** depending on the supported account types selected when you created the ADD app. To decide which value to assign follow these criteria:

        - When creating the AAD app if you selected either *Accounts in this organizational directory only (Microsoft only - Single tenant)* or *Accounts in any organizational directory(Microsoft AAD directory - Multi tenant)* enter the **tenant ID** you recorded earlier for the AAD app.

        - However, if you selected *Accounts in any organizational directory (Any AAD directory - Multi tenant and personal Microsoft accounts e.g. Skype, Xbox, Outlook.com)* enter the word **common** instead of a tenant ID. Otherwise, the AAD app will verify through the tenant whose ID was selected and exclude personal MS accounts.

       This will be the tenant associated with the users who can be authenticated.

    1. For **Resource URL**, enter `https://graph.microsoft.com/`.
    1. Leave **Scopes** blank.

1. Click **Save**.
