---
title: Authenticate requests with the Bot Connector API
description: Learn how to authenticate API requests in the Bot Connector API and Bot State API.
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: micchow
ms.service: azure-ai-bot-service
ms.topic: how-to
ms.date: 02/10/2023
ms.custom:
  - evergreen
---

# Authentication with the Bot Connector API

Your bot communicates with the Bot Connector service using HTTP over a secured channel (SSL/TLS).
When your bot sends a request to the Connector service, it must include information that the Connector service can use to verify its identity.
Likewise, when the Connector service sends a request to your bot, it must include information that the bot can use to verify its identity.
This article describes the authentication technologies and requirements for the service-level authentication that takes place between a bot and the Bot Connector service. If you're writing your own authentication code, you must implement the security procedures described in this article to enable your bot to exchange messages with the Bot Connector service.

> [!IMPORTANT]
> If you're writing your own authentication code, it's critical that you implement all security procedures correctly.
> By implementing all steps in this article, you can mitigate the risk of an attacker being able to read messages that
> are sent to your bot, send messages that impersonate your bot, and steal secret keys.

If you're using the [Bot Framework SDK](../index-bf-sdk.yml), you don't need to implement the security procedures described in this article, because the SDK automatically does it for you. Simply configure your project with the App ID and password that you obtained for your bot during [registration](../bot-service-quickstart-registration.md) and the SDK will handle the rest.

## Authentication technologies

Four authentication technologies are used to establish trust between a bot and the Bot Connector:

| Technology | Description |
|----|----|
| **SSL/TLS** | SSL/TLS is used for all service-to-service connections. `X.509v3` certificates are used to establish the identity of all HTTPS services. **Clients should always inspect service certificates to ensure they are trusted and valid.** (Client certificates are NOT used as part of this scheme.) |
| **OAuth 2.0** | OAuth 2.0 uses the Microsoft Entra ID account login service to generate a secure token that a bot can use to send messages. This token is a service-to-service token; no user login is required. |
| **JSON Web Token (JWT)** | JSON Web Tokens are used to encode tokens that are sent to and from the bot. **Clients should fully verify all JWT tokens that they receive**, according to the requirements outlined in this article. |
| **OpenID metadata** | The Bot Connector service publishes a list of valid tokens that it uses to sign its own JWT tokens to OpenID metadata at a well-known, static endpoint. |

This article describes how to use these technologies via standard HTTPS and JSON. No special SDKs are required, although you may find that helpers for OpenID and others are useful.

## <a id="bot-to-connector"></a> Authenticate requests from your bot to the Bot Connector service

To communicate with the Bot Connector service, you must specify an access token in the `Authorization` header of each API request, using this format:

```http
Authorization: Bearer ACCESS_TOKEN
```

To get and use a JWT token for your bot:

1. Your bot sends a GET HTTP request to the MSA Login Service.
1. The response from the service contains the JWT token to use.
1. Your bot includes this JWT token in the authorization header in requests to the Bot Connector service.

### Step 1: Request an access token from the Microsoft Entra ID account login service

> [!IMPORTANT]
> If you haven't already done so, you must [register your bot](../bot-service-quickstart-registration.md) with the Bot Framework to obtain its AppID and password. You need the bot's App ID and password to request an access token.

Your bot identity can be managed in Azure in a few different ways.

- As a _user-assigned managed identity_, so that you don't need to manage the bot's credentials yourself.
- As a _single-tenant_ app.
- As a _multi-tenant_ app.

Request an access token based on your bot's application type.

#### [Multi-tenant](#tab/multitenant)

To request an access token from the login service, issue the following request, replacing **MICROSOFT-APP-ID** and **MICROSOFT-APP-PASSWORD** with the bot's AppID and password that you obtained when you [registered](../bot-service-quickstart-registration.md) your bot with the Bot Service.

```http
POST https://login.microsoftonline.com/botframework.com/oauth2/v2.0/token
Host: login.microsoftonline.com
Content-Type: application/x-www-form-urlencoded

grant_type=client_credentials&client_id=MICROSOFT-APP-ID&client_secret=MICROSOFT-APP-PASSWORD&scope=https%3A%2F%2Fapi.botframework.com%2F.default
```

#### [Single-tenant](#tab/singletenant)

To request an access token from the login service, issue the following request, replacing **MICROSOFT-APP-ID**,  **MICROSOFT-APP-PASSWORD** and **MICROSOFT-TENANT-ID** with the bot's AppID, password and tenant Id that you obtained when you [registered](../bot-service-quickstart-registration.md) your bot with the Bot Service.

```http
POST https://login.microsoftonline.com/MICROSOFT-TENANT-ID/oauth2/v2.0/token
Host: login.microsoftonline.com
Content-Type: application/x-www-form-urlencoded

grant_type=client_credentials&client_id=MICROSOFT-APP-ID&client_secret=MICROSOFT-APP-PASSWORD&scope=https%3A%2F%2Fapi.botframework.com%2F.default
```

#### [User-assigned managed identity](#tab/userassigned)

App Service and Azure Functions provide an internally accessible REST endpoint for token retrieval. To request an access token from the `MSI/token` endpoint, make a GET request to the endpoint. Use the `resource` and `client_id` query parameters. Set `resource` to `https://api.botframework.com` and `client_id` to the bot's managed identity app ID. The request doesn't require other query parameters.

- For more information about the token endpoint, see [Connect to Azure services in app code](/azure/app-service/overview-managed-identity?context=%2Fazure%2Factive-directory%2Fmanaged-identities-azure-resources%2Fcontext%2Fmsi-context&tabs=portal%2Chttp#connect-to-azure-services-in-app-code).
- For more information about bot app IDs, see [Create an Azure Bot resource](../bot-service-quickstart-registration.md).

---

### Step 2: Obtain the JWT token from the Microsoft Entra ID account login service response

If your application is authorized by the login service, the JSON response body will specify your access token, its type, and its expiration (in seconds).

When adding the token to the `Authorization` header of a request, you must use the exact value that is specified in this response&mdash;don't escape or encode the token value. The access token is valid until its expiration. To prevent token expiration from impacting your bot's performance, you may choose to cache and proactively refresh the token.

This example shows a response from the Microsoft Entra ID account login service:

```http
HTTP/1.1 200 OK
... (other headers)
```

```json
{
    "token_type":"Bearer",
    "expires_in":3600,
    "ext_expires_in":3600,
    "access_token":"eyJhbGciOiJIUzI1Ni..."
}
```

### Step 3: Specify the JWT token in the Authorization header of requests

When you send an API request to the Bot Connector service, specify the access token in the `Authorization` header of the request using this format:

```http
Authorization: Bearer ACCESS_TOKEN
```

All requests that you send to the Bot Connector service must include the access token in the `Authorization` header.
If the token is correctly formed, isn't expired, and was generated by the Microsoft Entra ID account login service, the Bot Connector service will authorize the request. Additional checks are performed to ensure that the token belongs to the bot that sent the request.

The following example shows how to specify the access token in the `Authorization` header of the request.

```http
POST https://smba.trafficmanager.net/teams/v3/conversations/12345/activities
Authorization: Bearer eyJhbGciOiJIUzI1Ni...

(JSON-serialized Activity message goes here)
```

> [!IMPORTANT]
> Only specify the JWT token in the `Authorization` header of requests you send to the Bot Connector service.
> Do NOT send the token over unsecured channels and do NOT include it in HTTP requests that you send to other services.
> The JWT token that you obtain from the the Microsoft Entra ID account login service is like a password and should be handled with
> great care. Anyone that possesses the token may use it to perform operations on behalf of your bot.

#### Bot to Connector: example JWT components

```json
header:
{
  typ: "JWT",
  alg: "RS256",
  x5t: "<SIGNING KEY ID>",
  kid: "<SIGNING KEY ID>"
},
payload:
{
  aud: "https://api.botframework.com",
  iss: "https://sts.windows.net/d6d49420-f39b-4df7-a1dc-d59a935871db/",
  nbf: 1481049243,
  exp: 1481053143,
  appid: "<YOUR MICROSOFT APP ID>",
  ... other fields follow
}
```

> [!NOTE]
> Actual fields may vary in practice. Create and validate all JWT tokens as specified above.

## <a id="connector-to-bot"></a> Authenticate requests from the Bot Connector service to your bot

When the Bot Connector service sends a request to your bot, it specifies a signed JWT token in the `Authorization` header of the request. Your bot can authenticate calls from the Bot Connector service by verifying the authenticity of the signed JWT token.

To authenticate calls from the Bot Connector service:

1. Your bot gets the JWT token from the authorization header in requests sent from the Bot Connector service.
1. Your bot gets the OpenID metadata document for the Bot Connector service.
1. Your bot gets the list of valid signing keys from the document.
1. Your bot verifies the authenticity of the JWT token.

### <a id="openid-metadata-document"></a> Step 2: Get the OpenID metadata document

The OpenID metadata document specifies the location of a second document that lists the Bot Connector service's valid signing keys. To get the OpenID metadata document, issue this request via HTTPS:

```http
GET https://login.botframework.com/v1/.well-known/openidconfiguration
```

> [!TIP]
> This is a static URL that you can hardcode into your application.

The following example shows an OpenID metadata document that is returned in response to the `GET` request. The `jwks_uri` property specifies the location of the document that contains the Bot Connector service's valid signing keys.

```json
{
    "issuer": "https://api.botframework.com",
    "authorization_endpoint": "https://invalid.botframework.com",
    "jwks_uri": "https://login.botframework.com/v1/.well-known/keys",
    "id_token_signing_alg_values_supported": [
      "RS256"
    ],
    "token_endpoint_auth_methods_supported": [
      "private_key_jwt"
    ]
}
```

### <a id="connector-to-bot-step-3"></a> Step 3: Get the list of valid signing keys

To get the list of valid signing keys, issue a `GET` request via HTTPS to the URL specified by the `jwks_uri` property in the OpenID metadata document. For example:

```http
GET https://login.botframework.com/v1/.well-known/keys
```

The response body specifies the document in the [JWK format](https://tools.ietf.org/html/rfc7517) but also includes an additional property for each key: `endorsements`.

> [!TIP]
> The list of keys is stable and may be cached, but new keys may be added at any time. To ensure your bot has an up-to-date copy of the document before these keys are used, all bot instances should **refresh their local cache** of the document **at least once every 24 hours**.

The `endorsements` property within each key contains one or more endorsement strings that you can use to verify that the channel ID specified in the `channelId` property within the [Activity][] object of the incoming request is authentic. The list of channel IDs that require endorsements is configurable within each bot. By default, it will be the list of all published channel IDs, although bot developers may override selected channel ID values either way.

### Step 4: Verify the JWT token

To verify the authenticity of the token that was sent by the Bot Connector service, you must extract the token from the `Authorization` header of the request, parse the token, verify its contents, and verify its signature.

JWT parsing libraries are available for many platforms and most implement secure and reliable parsing for JWT tokens, although you must typically configure these libraries to require that certain characteristics of the token (its issuer, audience, and so on) contain correct values.
When parsing the token, you must configure the parsing library or write your own validation to ensure the token meets these requirements:

1. The token was sent in the HTTP `Authorization` header with "Bearer" scheme.
1. The token is valid JSON that conforms to the [JWT standard](http://openid.net/specs/draft-jones-json-web-token-07.html).
1. The token contains an "issuer" claim with value of `https://api.botframework.com`.
1. The token contains an "audience" claim with a value equal to the bot's Microsoft App ID.
1. The token is within its validity period. Industry-standard clock-skew is 5 minutes.
1. The token has a valid cryptographic signature, with a key listed in the OpenID keys document that was retrieved in [Step 3](#connector-to-bot-step-3), using the signing algorithm that is specified in the `id_token_signing_alg_values_supported` property of the Open ID Metadata document that was retrieved in [Step 2](#openid-metadata-document).
1. The token contains a "serviceUrl" claim with value that matches the `serviceUrl` property at the root of the [Activity][] object of the incoming request.

If endorsement for a channel ID is required:

- You should require that any `Activity` object sent to your bot with that channel ID is accompanied by a JWT token that is signed with an endorsement for that channel.
- If the endorsement isn't present, your bot should reject the request by returning an **HTTP 403 (Forbidden)** status code.

> [!IMPORTANT]
> All of these requirements are important, particularly requirements 4 and 6.
> Failure to implement ALL of these verification requirements will leave the bot open to attacks
> which could cause the bot to divulge its JWT token.

Implementers shouldn't expose a way to disable validation of the JWT token that is sent to the bot.

#### Connector to Bot: example JWT components

```json
header:
{
  typ: "JWT",
  alg: "RS256",
  x5t: "<SIGNING KEY ID>",
  kid: "<SIGNING KEY ID>"
},
payload:
{
  aud: "<YOU MICROSOFT APP ID>",
  iss: "https://api.botframework.com",
  nbf: 1481049243,
  exp: 1481053143,
  ... other fields follow
}
```

> [!NOTE]
> Actual fields may vary in practice. Create and validate all JWT tokens as specified above.

## <a id="emulator-to-bot"></a> Authenticate requests from the Bot Framework Emulator to your bot

The [Bot Framework Emulator](../bot-service-debug-emulator.md) is a desktop tool that you can use to test the functionality of your bot. Although the Bot Framework Emulator uses the same [authentication technologies](#authentication-technologies) as described above, it's unable to impersonate the real Bot Connector service.
Instead, it uses the Microsoft App ID and Microsoft App Password that you specify when you connect the Emulator to your bot to create tokens that are identical to those that the bot creates.
When the Emulator sends a request to your bot, it specifies the JWT token in the `Authorization` header of the request&mdash;in essence, using the bot's own credentials to authenticate the request.

If you're implementing an authentication library and want to accept requests from the Bot Framework Emulator, you must add this additional verification path. The path is structurally similar to the [Connector -> Bot](#connector-to-bot) verification path, but it uses MSA's OpenID document instead of the Bot Connector's OpenID document.

To authenticate calls from the Bot Framework Emulator:

1. Your bot gets the JWT token from the authorization header in requests sent from the Bot Framework Emulator.
1. Your bot gets the OpenID metadata document for the Bot Connector service.
1. Your bot gets the list of valid signing keys from the document.
1. Your bot verifies the authenticity of the JWT token.

---

### Step 2: Get the MSA OpenID metadata document

The OpenID metadata document specifies the location of a second document that lists the valid signing keys. To get the MSA OpenID metadata document, issue this request via HTTPS:

```http
GET https://login.microsoftonline.com/botframework.com/v2.0/.well-known/openid-configuration
```

The following example shows an OpenID metadata document that is returned in response to the `GET` request. The `jwks_uri` property specifies the location of the document that contains the valid signing keys.

```json
{
    "authorization_endpoint":"https://login.microsoftonline.com/common/oauth2/v2.0/authorize",
    "token_endpoint":"https://login.microsoftonline.com/common/oauth2/v2.0/token",
    "token_endpoint_auth_methods_supported":["client_secret_post","private_key_jwt"],
    "jwks_uri":"https://login.microsoftonline.com/common/discovery/v2.0/keys",
    ...
}
```

### <a id="emulator-to-bot-step-3"></a> Step 3: Get the list of valid signing keys

To get the list of valid signing keys, issue a `GET` request via HTTPS to the URL specified by the `jwks_uri` property in the OpenID metadata document. For example:

```http
GET https://login.microsoftonline.com/common/discovery/v2.0/keys
Host: login.microsoftonline.com
```

The response body specifies the document in the [JWK format](https://tools.ietf.org/html/rfc7517).

### Step 4: Verify the JWT token

To verify the authenticity of the token that was sent by the Emulator, you must extract the token from the `Authorization` header of the request, parse the token, verify its contents, and verify its signature.

JWT parsing libraries are available for many platforms and most implement secure and reliable parsing for JWT tokens, although you must typically configure these libraries to require that certain characteristics of the token (its issuer, audience, and so on) contain correct values.
When parsing the token, you must configure the parsing library or write your own validation to ensure the token meets these requirements:

1. The token was sent in the HTTP `Authorization` header with "Bearer" scheme.
1. The token is valid JSON that conforms to the [JWT standard](http://openid.net/specs/draft-jones-json-web-token-07.html).
1. The token contains an "issuer" claim with one of the [highlighted values](https://github.com/microsoft/botbuilder-dotnet/blob/3c335046f95deeac50fbb0b48c7c8c42051d4f6d/libraries/Microsoft.Bot.Connector/Authentication/EmulatorValidation.cs#L28-L31) for non governmental cases. Checking for both issuer values will ensure you're checking for both the security protocol v3.1 and v3.2 issuer values.
1. The token contains an "audience" claim with a value equal to the bot's Microsoft App ID.
1. The Emulator, depending on the version, sends the AppId via either the appid claim (version 1) or the authorized party claim (version 2).
1. The token is within its validity period. Industry-standard clock-skew is 5 minutes.
1. The token has a valid cryptographic signature with a key listed in the OpenID keys document that was retrieved in [Step 3](#emulator-to-bot-step-3).

> [!NOTE]
> Requirement 5 is a specific to the Emulator verification path.

If the token doesn't meet all of these requirements, your bot should terminate the request by returning an **HTTP 403 (Forbidden)** status code.

> [!IMPORTANT]
> All of these requirements are important, particularly requirements 4 and 7.
> Failure to implement ALL of these verification requirements will leave the bot open to attacks
> which could cause the bot to divulge its JWT token.

#### Emulator to Bot: example JWT components

```json
header:
{
  typ: "JWT",
  alg: "RS256",
  x5t: "<SIGNING KEY ID>",
  kid: "<SIGNING KEY ID>"
},
payload:
{
  aud: "<YOUR MICROSOFT APP ID>",
  iss: "https://sts.windows.net/d6d49420-f39b-4df7-a1dc-d59a935871db/",
  nbf: 1481049243,
  exp: 1481053143,
  ... other fields follow
}
```

> [!NOTE]
> Actual fields may vary in practice. Create and validate all JWT tokens as specified above.

## Security protocol changes

### [Bot to Connector authentication](#bot-to-connector)

#### OAuth login URL

| Protocol version | Valid value |
|----|----|
| v3.1 & v3.2 | `https://login.microsoftonline.com/botframework.com/oauth2/v2.0/token` |

#### OAuth scope

| Protocol version | Valid value |
|----|----|
| v3.1 & v3.2 |  `https://api.botframework.com/.default` |

### [Connector to Bot authentication](#connector-to-bot)

#### OpenID metadata document

| Protocol version | Valid value |
|----|----|
| v3.1 & v3.2 | `https://login.botframework.com/v1/.well-known/openidconfiguration` |

#### JWT Issuer

| Protocol version | Valid value |
|----|----|
| v3.1 & v3.2 | `https://api.botframework.com` |

### [Emulator to Bot authentication](#emulator-to-bot)

#### OAuth login URL

| Protocol version | Valid value |
|----|----|
| v3.1 & v3.2 | `https://login.microsoftonline.com/botframework.com/oauth2/v2.0/token` |

#### OAuth scope

| Protocol version | Valid value |
|----|----|
| v3.1 & v3.2 |  Your bot's Microsoft App ID + `/.default` |

#### JWT Audience

| Protocol version | Valid value |
|----|----|
| v3.1 & v3.2 | Your bot's Microsoft App ID |

#### JWT Issuer

| Protocol version | Valid value |
|----|----|
| v3.1  1.0| `https://sts.windows.net/aaaabbbb-0000-cccc-1111-dddd2222eeee/` |
| v3.1  2.0| `https://login.microsoftonline.com/aaaabbbb-0000-cccc-1111-dddd2222eeee/v2.0`|
| v3.2  1.0| `https://sts.windows.net/f8cdef31-a31e-4b4a-93e4-5f571e91255a/` |
| v3.2  2.0| `https://login.microsoftonline.com/f8cdef31-a31e-4b4a-93e4-5f571e91255a/v2.0`|

See also the [highlighted values](https://github.com/microsoft/botbuilder-dotnet/blob/3c335046f95deeac50fbb0b48c7c8c42051d4f6d/libraries/Microsoft.Bot.Connector/Authentication/EmulatorValidation.cs#L28-L31) for non governmental cases.

#### OpenID metadata document

| Protocol version | Valid value |
|----|----|
| v3.1 & v3.2 | `https://login.microsoftonline.com/botframework.com/v2.0/.well-known/openid-configuration` |

## Additional resources

- [Troubleshooting Bot Framework authentication](../bot-service-troubleshoot-authentication-problems.md)
- [Bot Framework Activity schema](https://github.com/Microsoft/botframework-sdk/blob/main/specs/botframework-activity/botframework-activity.md)
- [JSON Web Token (JWT) draft-jones-json-web-token-07](http://openid.net/specs/draft-jones-json-web-token-07.html)
- [JSON Web Signature (JWS) draft-jones-json-web-signature-04](https://tools.ietf.org/html/draft-jones-json-web-signature-04)
- [JSON Web Key (JWK) RFC 7517](https://tools.ietf.org/html/rfc7517)

[Activity]: bot-framework-rest-connector-api-reference.md#activity-object
