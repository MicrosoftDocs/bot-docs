
<!-- Azure AD v2 settings -->

| **Property** | **Description** | **Value** |
|---|---|---|
|**Name** | The name of your connection | \<Your name for the connection\> <img width="300px">|
|**Service Provider**| AAD Identity provider | `Azure Active Directory v2` |
|**Client id** | AAD identity provider app ID| <AAD provider app id\> |
|**Client secret** | AAD identity provider app secret| <AAD provider app secret\> |
|**Tenant ID** | | <directory (tenant) ID> or `common`. See note. |
|**Scopes** | | <API access permissions>. See note. |
|||


**Note**

- Enter the **tenant ID** you recorded for the AAD identity provider app, if you selected one of the following:

    - *Accounts in this organizational directory only (Microsoft only - Single tenant)*

    - *Accounts in any organizational directory(Microsoft AAD directory - Multi tenant)*
- Enter `common`  if you selected *Accounts in any organizational directory (Any AAD directory - Multi tenant and personal Microsoft accounts e.g. Skype, Xbox, Outlook.com)*. Otherwise, the AAD identity provider app will verify through the tenant whose ID was selected and exclude personal MS accounts.
- Scopes takes a case-sensitive, space-separated list of values.
