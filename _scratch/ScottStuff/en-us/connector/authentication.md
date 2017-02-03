---
layout: page
title: Authentication
permalink: /en-us/connector/authentication/
weight: 3070
parent1: Building your Bot Using the Bot Connector REST API
---

Bot Connector service uses [OAuth 2.0 client credentials](https://tools.ietf.org/html/rfc6749#section-4.4) for bot authentication. To send messages to the connector, your bot must get an access token from the Microsoft Account (MSA) server. After getting the access token, you include the token in the `Authorization` header of all requests that your bot sends to the connector. For details, see [Getting an Access Token and Calling the Bot Connector Service](#getaccesstoken).  

To ensure that calls made to your bot by the connector actually came from the connector, it is strongly recommended that your bot verify the authenticity of the calls. For details, see [Authenticating calls that the Bot Connector service sends your bot](#verifyaccesstoken).

<div class="docs-text-note"> <strong>NOTE</strong>: If you use the <a href="/en-us/csharp/builder/sdkreference/Getting%20Started.md" target="_blank">Bot Builder SDK for C#</a> or the <a href="/en-us/node/builder/getting-started.md" target="_blank">Bot Builder SDK for Node.js</a>, the SDK will handle authentication for you; all you need  to do is configure your project with your bot's Microsoft App ID and Password and the SDK does the rest. </div>

<div class="docs-text-note"> <strong>NOTE</strong>: This topic describes server-to-server authentication and does not describe user-level authentication, such as a user logging in to your bot. </div>

<div class="docs-text-note"> <strong>IMPORTANT</strong>: To prevent an attacker from reading messages sent to your bot, stealing secret keys, and sending messages impersonating your bot, you must implement all procedures outlined in this topic. </div>


<a id="getaccesstoken" />

### Getting an access token and calling the Bot Connector service

Before your bot can exchange messages with Bot Connector, you must login to the Microsoft Account (MSA) server to get an access token. To login to the MSA server, use the Microsoft App ID and Password that you received when you registered your bot with Microsoft Bot Framework (see Registering your bot with the Microsoft Bot Framwork).

The following diagram shows the steps of getting and using the access token.

![Authenticate to the MSA login service and then to the bot](/en-us/images/connector/auth_bot_to_bot_connector.png)

#### Step 1: POST to the MSA login service

To get a service-to-service token, send an HTTPS POST request to https://login.microsoftonline.com/botframework.com/oauth2/v2.0/token. The body of the request must include the following form-encoded parameters:


|**Parameter**|**Description**
|grant_type|The OAuth grant type, which must be set to client_credentails.
|client_id|The Microsoft App ID the you received when you registered your bot.
|client_secret|The Microsoft App Password that you received when you registered your bot.
|scope|The scope of the access token, which must be set to `https://api.botframework.com/.default`.

The following shows a request to get an access token.

```cmd
POST https://login.microsoftonline.com/botframework.com/oauth2/v2.0/token HTTP/1.1
Host: login.microsoftonline.com
Content-Type: application/x-www-form-urlencoded
 
grant_type=client_credentials&client_id=<YOUR MICROSOFT APP ID>&client_secret=<YOUR MICROSOFT APP PASSWORD>&scope=https%3A%2F%2Fapi.botframework.com%2F.default
```

#### Step 2: Get back JSON containing a JWT token

If the MSA request succeeded, the response body will contain a JSON object that includes an access token. You should cache and use the token in all requests to the connector service. To ensure uninterrupted service, request a new access token before the token expires; you should not wait until after the token expires to get a new token. The `expires_in` property contains the number of seconds until the token expires (typically one hour). 

The following shows an example response from the MSA server.

```cmd
HTTP/1.1 200 OK
... (other headers) 

{
    "token_type":"Bearer",
    "expires_in":3600,
    "ext_expires_in":3600,
    "access_token":"eyJhbGciOiJIUzI1Ni..."
}
```

#### Step 3: Use the JWT token in an Authorization header

To call the connector service, the request must include an `Authorization` header that is set to the access token that you received in Step 2. 

The following shows an example request to the connector's Activities endpoint.

```cmd
POST https://api.botframework.com/v3/conversations/12345/activities HTTP/1.1
Host: api.botframework.com
Authorization: Bearer eyJhbGciOiJIUzI1Ni...
    
<JSON Activity message goes here>
```

<div class="docs-text-note"><strong>IMPORTANT</strong>: Use the access token only to call the connector service. Do not send the token over unsecured channels, and do not use it to call other services. The JWT token is like a password and should be treated with the same care. Anyone that possesses the token may use it to perform operations on behalf of your bot.</div>
 

<a id="verifyaccesstoken"/>

### Authenticating calls that the Bot Connector service sends your bot


When Bot Connector calls your bot, it includes an `Authorization` header that is set to a JWT token that the connector signed. Your bot should verify the authenticity of this token. 

The following diagram shows the steps of getting and verifying the connector's JWT token.

![Authenticate calls from the Bot Connector to your bot](/en-us/images/connector/auth_bot_connector_to_bot.png)

#### Step 2: Get the OpenId metadata

To get the OpenId metadata, send an HTTPS GET request to https://login.botframework.com/v1/.well-known/openidconfiguration. Hardcode this URL into your application. Th OpenId metadata contains a link (see the `jwks_uri` property) to a document that contains the valid signing keys that Bot Framework uses. 

The following shows an example request to get the OpenId document.

```cmd
GET https://login.botframework.com/v1/.well-known/openidconfiguration HTTP/1.1
Host: login.botframework.com
```

The following shows an example of the OpenId document.

```cmd
{ 
    "issuer":"https://api.botframework.com",
    "authorization_endpoint":"https://invalid.botframework.com/",
    "jwks_uri":"https://api.aps.skype.com/v1/keys",
    "id_token_signing_alg_values_supported":["RSA256"],
    "token_endpoint_auth_methods_supported":["private_key_jwt"]
}
```

#### Step 3: Download the list of valid signing keys

To get the list of valid signing keys, send an HTTPS GET request to the URL in the `jwks_uri` property. The document is in JWK format (see [JSON Web Key (JWK) RFC 7517](https://tools.ietf.org/html/rfc7517)). The list of keys is relatively stable and may be cached for long periods of time (for example, the Bot Builder SDKs cache the keys for 5 days).

The following shows an example request to get the signing keys.

```cmd
GET https://api.aps.skype.com/v1/keys HTTP/1.1
Host: api.aps.skype.com
```

#### Step 4: Verify the JWT token

To verify the authenticity of the token sent by Bot Connector, you must extract the token from the HTTP Authorization header, parse the token, verify its contents, and verify its signature. There are JWT libraries that you can use to reliably parse the JWT token. To use these libraries, you must configure them to ensure the token meets the following requirements:

1. The token was sent in the HTTP Authorization header with "Bearer" scheme.
2. The token is valid JSON that conforms to the JWT standard (see [JSON Web Token (JWT) draft-jones-json-web-token-07](http://openid.net/specs/draft-jones-json-web-token-07.html)).
3. The token contains an issuer claim with value of https://api.botframework.com.
4. The token contains an audience claim with a value equivalent to your bot's Microsoft App ID.
5. The token has not yet expired. Industry-standard clock-skew is 5 minutes.
6. The token has a valid cryptographic signature with a key listed in the OpenId keys document retrieved in Step 3, above.

If the token does not meet all of these requirements, your bot should terminate the request by returning an HTTP 403 (Forbidden) status code.

<div class="docs-text-note"><strong>IMPORTANT</strong>: All of the requirements are important, particularly requirements 4 and 6. Failure to implement ALL verification requirements will leave the bot open to attacks which could cause the bot to divulge its JWT token.<p/>

Implementers should not expose a way to disable validation of the JWT token sent to the bot.</div>


### Enabling authentication from the Bot Framework Emulator

If you use the [Bot Framework Emulator](en-us/tools/bot-framework-emulator) to test your bot, and you authenticate the messages that your bot receives, your bot will need to include additional logic to authenticate the emulator's token.

Because the emulator uses the same authentication scheme as the connector service for authenticating your bot, your current logic of getting an access token and passing it in the Authorization header stays the same. The logic that needs to change is the logic that authenticates the emulator's token. Because the emulator is unable to impersonate the real Bot Connector service, the emulator uses the Microsoft App ID and Microsoft App Password that you provide when you set the App ID and Password fields in the Emulator UI, if using the Windows desktop version of the emulator. In essence, the emulator uses your bot's own credentials when it sends messages to your bot.

The following diagram shows the steps of getting and verifying the emulator's JWT token.

![Authenticate calls from the Bot Framework Emulator to your bot](/en-us/images/connector/auth_bot_framework_emulator_to_bot.png)

### Steps 2: Download the MSA OpenId metadata document

Instead of getting the connector's OpenId metadata, you will get MSA's OpenId metadata. To get MSA's OpenId metadata, send an HTTPS GET request to https://login.microsoftonline.com/common/v2.0/.well-known/openid-configuration as shown in the following example:

```cmd
GET https://login.microsoftonline.com/common/v2.0/.well-known/openid-configuration HTTP/1.1
```

The following shows an example of the OpenId document.


```cmd
{
    "authorization_endpoint":"https://login.microsoftonline.com/common/oauth2/v2.0/authorize",
    "token_endpoint":"https://login.microsoftonline.com/common/oauth2/v2.0/token",
    "token_endpoint_auth_methods_supported":["client_secret_post","private_key_jwt"],
    "jwks_uri":"https://login.microsoftonline.com/common/discovery/v2.0/keys",
    . . .
}
```

#### Step 3: Download the list of valid signing keys

To get the list of valid signing keys, send an HTTPS GET request to the URL in the `jwks_uri` property. The document is in JWK format (see [JSON Web Key (JWK) RFC 7517](https://tools.ietf.org/html/rfc7517)). The list of keys is relatively stable and may be cached for long periods of time (for example, the Bot Builder SDKs cache the keys for 5 days).

The following shows an example request to get the signing keys.

```cmd
GET https://login.microsoftonline.com/common/discovery/v2.0/keys HTTP/1.1
Host: login.microsoftonline.com
```

### Step 4: Verify the JWT token

To verify the authenticity of the token sent by the emulator, you must extract the token from the HTTP Authorization header, parse the token, verify its contents, and verify its signature. There are JWT libraries that you can use to reliably parse the JWT token. To use these libraries, you must configure them to ensure the token meets the following requirements:

1. The token was sent in the HTTP Authorization header with "Bearer" scheme.
2. The token is valid JSON that conforms to the JWT standard (see [JSON Web Token (JWT) draft-jones-json-web-token-07](http://openid.net/specs/draft-jones-json-web-token-07.html)).
3. The token contains an issuer claim with value of https://sts.windows.net/d6d49420-f39b-4df7-a1dc-d59a935871db/.
4. The token contains an audience claim with value of the bot's app ID.
5. The token contains an "appid" claim with the value equal to the bot's Microsoft App ID.
6. The token has not yet expired. Industry-standard clock-skew is 5 minutes.
7. The token has a valid cryptographic signature with a key listed in the OpenId keys document retrieved in Step 3, above.

Requirement 5 is a new requirement specific to the emulator verification path.

If the token does not meet all of these requirements, your bot should terminate the request by returning an HTTP 403 (Forbidden) status code.

<div class="docs-text-note"><strong>IMPORTANT</strong>: The same precautions must be taken in parsing this token as when parsing a token from the connector service. Failure to implement all validation requirements will leave the bot vulnerable to attack.</div>

<a name="references"/>

## References

1. [JSON Web Token (JWT) draft-jones-json-web-token-07](http://openid.net/specs/draft-jones-json-web-token-07.html)
2. [JSON Web Signature (JWS) draft-jones-json-web-signature-04](https://tools.ietf.org/html/draft-jones-json-web-signature-04)
3. [JSON Web Key (JWK) RFC 7517](https://tools.ietf.org/html/rfc7517)
