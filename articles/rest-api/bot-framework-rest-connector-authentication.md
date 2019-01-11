---
title: Authenticate requests | Microsoft Docs
description: Learn how to authenticate API requests in the Bot Connector API and Bot State API. 
author: RobStand
ms.author: kamrani
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.subservice: sdk
ms.date: 12/13/2017
---

# Authentication

Your bot communicates with the Bot Connector service using HTTP over a secured channel (SSL/TLS). 
When your bot sends a request to the Connector service, it must include information that the Connector service can use to verify its identity. 
Likewise, when the Connector service sends a request to your bot, it must include information that the bot can use to verify its identity. 
This article describes the authentication technologies and requirements for the service-level authentication that takes place between a bot and the Bot Connector service. If you are writing your own authentication code, you must implement the security procedures described in this article to enable your bot to exchange messages with the Bot Connector service.

> [!IMPORTANT]
> If you are writing your own authentication code, it is critical that you implement all security procedures correctly. 
> By implementing all steps in this article, you can mitigate the risk of an attacker being able to read messages that 
> are sent to your bot, send messages that impersonate your bot, and steal secret keys. 

If you are using the [Bot Framework SDK for .NET](../dotnet/bot-builder-dotnet-overview.md) or the [Bot Framework SDK for Node.js](../nodejs/index.md), you do not need to implement the security procedures described in this article, because the SDK automatically does it for you. Simply configure your project with the App ID and password that you obtained for your bot during [registration](../bot-service-quickstart-registration.md) and the SDK will handle the rest.

> [!WARNING]
> In December 2016, v3.1 of the Bot Framework security protocol introduced changes to several values that are 
> used during token generation and validation. In late fall of 2017, v3.2 of the Bot Framework security protocol was introduced 
> which included changes to values that are used during token generation and validation.
> For more information, see [Security protocol changes](#security-protocol-changes).

## Authentication technologies

Four authentication technologies are used to establish trust between a bot and the Bot Connector:

| Technology | Description |
|----|----|
| **SSL/TLS** | SSL/TLS is used for all service-to-service connections. `X.509v3` certificates are used to establish the identity of all HTTPS services. **Clients should always inspect service certificates to ensure they are trusted and valid.** (Client certificates are NOT used as part of this scheme.) |
| **OAuth 2.0** | OAuth 2.0 login to the Microsoft Account (MSA)/AAD v2 login service is used to generate a secure token that a bot can use to send messages. This token is a service-to-service token; no user login is required. |
| **JSON Web Token (JWT)** | JSON Web Tokens are used to encode tokens that are sent to and from the bot. **Clients should fully verify all JWT tokens that they receive**, according to the requirements outlined in this article. |
| **OpenID metadata** | The Bot Connector service publishes a list of valid tokens that it uses to sign its own JWT tokens to OpenID metadata at a well-known, static endpoint. |

This article describes how to use these technologies via standard HTTPS and JSON. No special SDKs are required, although you may find that helpers for OpenID etc. are useful.

## <a id="bot-to-connector"></a> Authenticate requests from your bot to the Bot Connector service

To communicate with the Bot Connector service, you must specify an access token in the `Authorization` header of each API request, using this format: 

```http
Authorization: Bearer ACCESS_TOKEN
```

This diagram shows the steps for bot-to-connector authentication:

![Authenticate to the MSA login service and then to the bot](../media/connector/auth_bot_to_bot_connector.png)

> [!IMPORTANT]
> If you have not already done so, you must [register your bot](../bot-service-quickstart-registration.md) with the Bot Framework to obtain its AppID and password. You will need the bot's App ID and password to request an access token.

### Step 1: Request an access token from the MSA/AAD v2 login service

To request an access token from the MSA/AAD v2 login service, issue the following request, replacing **MICROSOFT-APP-ID** and **MICROSOFT-APP-PASSWORD** with the AppID and password that you obtained when you [registered](../bot-service-quickstart-registration.md) your bot with the Bot Framework.

```http
POST https://login.microsoftonline.com/botframework.com/oauth2/v2.0/token
Host: login.microsoftonline.com
Content-Type: application/x-www-form-urlencoded

grant_type=client_credentials&client_id=MICROSOFT-APP-ID&client_secret=MICROSOFT-APP-PASSWORD&scope=https%3A%2F%2Fapi.botframework.com%2F.default
```

### Step 2: Obtain the JWT token from the MSA/AAD v2 login service response

If your application is authorized by the MSA/AAD v2 login service, the JSON response body will specify your access token, its type, and its expiration (in seconds). 

When adding the token to the `Authorization` header of a request, you must use the exact value that is specified in this response (i.e., do not escape or encode the token value). The access token is valid until its expiration. To prevent token expiration from impacting your bot's performance, you may choose to cache and proactively refresh the token.

This example shows a response from the MSA/AAD v2 login service:

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
If the token is correctly formed, is not expired, and was generated by the MSA/AAD v2 login service, the Bot Connector service will authorize the request. Additional checks are performed to ensure that the token belongs to the bot that sent the request.

The following example shows how to specify the access token in the `Authorization` header of the request. 

```http
POST https://smba.trafficmanager.net/apis/v3/conversations/12345/activities 
Authorization: Bearer eyJhbGciOiJIUzI1Ni...
    
(JSON-serialized Activity message goes here)
```

> [!IMPORTANT]
> Only specify the JWT token in the `Authorization` header of requests you send to the Bot Connector service. 
> Do NOT send the token over unsecured channels and do NOT include it in HTTP requests that you send to other services. 
> The JWT token that you obtain from the MSA/AAD v2 login service is like a password and should be handled with 
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

This diagram shows the steps for connector-to-bot authentication:

![Authenticate calls from the Bot Connector to your bot](../media/connector/auth_bot_connector_to_bot.png)

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

The response body specifies the document in the [JWK format](https://tools.ietf.org/html/rfc7517) but also includes an additional property for each key: `endorsements`. The list of keys is relatively stable and may be cached for long periods of time (by default, 5 days within the Bot Framework SDK).

The `endorsements` property within each key contains one or more endorsement strings which you can use to verify that the channel ID specified in the `channelId` property within the [Activity][Activity] object of the incoming request is authentic. The list of channel IDs that require endorsements is configurable within each bot. By default, it will be the list of all published channel IDs, although bot developers may override selected channel ID values either way. If endorsement for a channel ID is required:

- You should require that any [Activity][Activity] object sent to your bot with that channel ID is accompanied by a JWT token that is signed with an endorsement for that channel. 
- If the endorsement is not present, your bot should reject the request by returning an **HTTP 403 (Forbidden)** status code.

### Step 4: Verify the JWT token

To verify the authenticity of the token that was sent by the Bot Connector service, you must extract the token from the `Authorization` header of the request, parse the token, verify its contents, and verify its signature. 

JWT parsing libraries are available for many platforms and most implement secure and reliable parsing for JWT tokens, although you must typically configure these libraries to require that certain characteristics of the token (its issuer, audience, etc.) contain correct values. 
When parsing the token, you must configure the parsing library or write your own validation to ensure the token meets these requirements:

1. The token was sent in the HTTP `Authorization` header with "Bearer" scheme.
2. The token is valid JSON that conforms to the [JWT standard](http://openid.net/specs/draft-jones-json-web-token-07.html).
3. The token contains an "issuer" claim with value of `https://api.botframework.com`.
4. The token contains an "audience" claim with a value equal to the bot's Microsoft App ID.
5. The token is within its validity period. Industry-standard clock-skew is 5 minutes.
6. The token has a valid cryptographic signature, with a key listed in the OpenID keys document that was retrieved in [Step 3](#connector-to-bot-step-3), using the signing algorithm that is specified in the `id_token_signing_alg_values_supported` property of the Open ID Metadata document that was retrieved in [Step 2](#openid-metadata-document).
7. The token contains a "serviceUrl" claim with value that matches the `servieUrl` property at the root of the [Activity][Activity] object of the incoming request. 

If the token does not meet all of these requirements, your bot should reject the request by returning an **HTTP 403 (Forbidden)** status code.

> [!IMPORTANT]
> All of these requirements are important, particularly requirements 4 and 6. 
> Failure to implement ALL of these verification requirements will leave the bot open to attacks 
> which could cause the bot to divulge its JWT token.

Implementers should not expose a way to disable validation of the JWT token that is sent to the bot.

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

> [!WARNING]
> In late fall of 2017, v3.2 of the Bot Framework security protocol was introduced. This new version includes a new "issuer" value within tokens that are exchanged between the Bot Framework Eumaltor and your bot. To prepare for this change, the below steps outline how to check for both the v3.1 and v3.2 issuer values. 

The [Bot Framework Emulator](../bot-service-debug-emulator.md) is a desktop tool that you can use to test the functionality of your bot. Although the Bot Framework Emulator uses the same [authentication technologies](#authentication-technologies) as described above, it is unable to impersonate the real Bot Connector service. 
Instead, it uses the Microsoft App ID and Microsoft App Password that you specify when you connect the emulator to your bot to create tokens that are identical to those that the bot creates. 
When the emulator sends a request to your bot, it specifies the JWT token in the `Authorization` header of the request -- in essence, using the bot's own credentials to authenticate the request. 

If you are implementing an authentication library and want to accept requests from the Bot Framework Emulator, you must add this additional verification path. The path is structurally similar to the [Connector -> Bot](#connector-to-bot) verification path, but it uses MSA’s OpenID document instead of the Bot Connector’s OpenID document.

This diagram shows the steps for emulator-to-bot authentication:

![Authenticate calls from the Bot Framework Emulator to your bot](../media/connector/auth_bot_framework_emulator_to_bot.png)

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

To verify the authenticity of the token that was sent by the emulator, you must extract the token from the `Authorization` header of the request, parse the token, verify its contents, and verify its signature. 

JWT parsing libraries are available for many platforms and most implement secure and reliable parsing for JWT tokens, although you must typically configure these libraries to require that certain characteristics of the token (its issuer, audience, etc.) contain correct values. 
When parsing the token, you must configure the parsing library or write your own validation to ensure the token meets these requirements:

1. The token was sent in the HTTP `Authorization` header with "Bearer" scheme.
2. The token is valid JSON that conforms to the [JWT standard](http://openid.net/specs/draft-jones-json-web-token-07.html).
3. The token contains an "issuer" claim with value of `https://sts.windows.net/d6d49420-f39b-4df7-a1dc-d59a935871db/` or `https://sts.windows.net/f8cdef31-a31e-4b4a-93e4-5f571e91255a/`. (Checking for both issuer values will ensure you are checking for both the security protocol v3.1 and v3.2 issuer values)
4. The token contains an "audience" claim with a value equal to the bot's Microsoft App ID.
5. The token contains an "appid" claim with the value equal to the bot's Microsoft App ID.
6. The token is within its validity period. Industry-standard clock-skew is 5 minutes.
7. The token has a valid cryptographic signature with a key listed in the OpenID keys document that was retrieved in [Step 3](#emulator-to-bot-step-3).

> [!NOTE]
> Requirement 5 is a specific to the emulator verification path. 

If the token does not meet all of these requirements, your bot should terminate the request by returning an **HTTP 403 (Forbidden)** status code.

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

> [!WARNING]
> Support for v3.0 of the security protocol was discontinued on **July 31, 2017**. 
> If you have written your own authentication code (i.e., did not use the Bot Framework SDK to create your bot), 
> you must upgrade to v3.1 of the security protocol by updating your application to use the v3.1 values that are listed below. 

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
| v3.1 & v3.2 |  Your bot’s Microsoft App ID + `/.default` |

#### JWT Audience

| Protocol version | Valid value |
|----|----|
| v3.1 & v3.2 | Your bot’s Microsoft App ID |

#### JWT Issuer

| Protocol version | Valid value |
|----|----|
| v3.1 | `https://sts.windows.net/d6d49420-f39b-4df7-a1dc-d59a935871db/` |
| v3.2 | `https://sts.windows.net/f8cdef31-a31e-4b4a-93e4-5f571e91255a/` |

#### OpenID metadata document

| Protocol version | Valid value |
|----|----|
| v3.1 & v3.2 | `https://login.microsoftonline.com/botframework.com/v2.0/.well-known/openid-configuration` |

## Additional resources

- [Troubleshooting Bot Framework authentication](../bot-service-troubleshoot-authentication-problems.md)
- [JSON Web Token (JWT) draft-jones-json-web-token-07](http://openid.net/specs/draft-jones-json-web-token-07.html)
- [JSON Web Signature (JWS) draft-jones-json-web-signature-04](https://tools.ietf.org/html/draft-jones-json-web-signature-04)
- [JSON Web Key (JWK) RFC 7517](https://tools.ietf.org/html/rfc7517)

[Activity]: bot-framework-rest-connector-api-reference.md#activity-object
