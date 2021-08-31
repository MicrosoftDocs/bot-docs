<!-- Azure AD v1 settings -->
<!-- Fixed ID -->
| **Property** | **Description** | **Value** |
|---|---|---|
|**Name** | The name of your connection | \<Your name for the connection\> <img width="300px">|
|**Service Provider**| Azure AD Identity provider | `Azure Active Directory` |
|**Client ID** | Azure AD identity provider app ID| \<AAD provider app ID\> |
|**Client secret** | Azure AD identity provider app secret| \<AAD provider app secret\> |
|**Grant Type** | | `authorization_code` |
|**Login URL** | | `https://login.microsoftonline.com` |
|**Tenant ID** | | <directory (tenant) ID> or `common`. See note.|
|**Resource URL** | | `https://graph.microsoft.com/` |
|**Scopes** | | _leave this blank_ |
|**Token Exchange URL** |Used for SSO in Azure AD v2|See note below|
| | |

**Note**

- Enter the **tenant ID** you recorded for the Azure AD identity provider app, if you selected one of the following:
  - *Accounts in this organizational directory only (Microsoft only - Single tenant)*
  - *Accounts in any organizational directory(Microsoft AAD directory - Multi tenant)*
- Enter `common`  if you selected *Accounts in any organizational directory (Any Azure AD directory - Multi tenant and personal Microsoft accounts e.g. Skype, Xbox, Outlook.com)*. Otherwise, the Azure AD identity provider app will verify through the tenant whose ID was selected and exclude personal Microsoft accounts.
- The **Token Exchange URL** is left blank for the root bot but is populated for the skill bot. See [Create an OAuth connection settings](../../v4sdk/bot-builder-authentication-sso.md#create-an-oauth-connection-settings-1) to learn how to get its value. 
