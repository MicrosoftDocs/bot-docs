
layout: page
title: Authentication
permalink: /en-us/restapi/authentication/
weight: 4229
parent1: REST API
parent2: Bots



* TOC
{:toc}

## Introduction and overview

This guide documents the security technologies and requirements for a bot to send and receive messages from the Bot Connector service. It is intended for developers writing their own authentication code.

**NOTE**: If you are using the **Bot Builder SDK for C#** or the **Bot Builder SDK for Node.js**, all steps within this document are handled automatically by the SDK. All you have to do is configure it with your bot's ID and password.

Your bot communicates to the Bot Connector service using HTTP over a secured channel (SSL/TLS). Within this channel, the bot and the Bot Connector service must prove to each other that they are the service they claim to be. Once the bot and the service are authenticated, they may exchange messages. This page describes authentication that takes place at the service level; it does not describe user-level authentication (such as a user logging in to your bot).

**NOTE**: If you are not using the Bot Builder SDK, you must implement the security procedures within this document. Note that it is **VERY IMPORTANT that you implement all procedures correctly**. If you don't, it may be possible for an attacker to read messages sent to your bot, steal secret keys, and send messages impersonating your bot. Properly implementing all steps in this guide mitigates those risks.


## Authentication technologies

There are four authentication technologies used to establish trust between the bot and the Bot Connector.

1. SSL/TLS is used on all service-to-service connections. X.509v3 certificates are used to establish the identity of all HTTPS services. **Clients should always inspect service certificates to ensure they are trusted and valid**. (Note: client certificates are NOT used as part of this scheme.)

2. OAuth 2.0 login to the Microsoft Account (MSA)/AAD v2 login service is used to generate a secure token that a bot can use to send messages. This token is a service-to-service token; no user login is required.

3. JSON Web Tokens (JWT) are used to encode tokens sent to and from the bot. **Clients should fully verify all JWT tokens they receive**, according to the requirements outlined below.

4. OpenId metadata at a well-known, hardcoded endpoint is used for the Bot Connector service to publish a list of valid tokens it uses to sign its own JWT tokens.

This guide describes how to use all of these technologies using standard HTTPS and JSON technologies. No special SDKs are required, although helpers for OpenId etc. may be helpful.


## Authentication calls from the bot to the Bot Connector service

When a bot issues a call to the Bot Connector service, for example when sending a message to a user, it must first retrieve a token from the MSA service. This token may be cached until it expires (typically one hour). To authenticate yourself to the MSA service, you must supply your Microsoft app ID (available from the Bot Framework developer portal) and your Microsoft app password (visible only once when you generated your app). These parameters are used in step 1.

![Authenticate to the MSA/AAD v2 login service and then to the bot](/en-us/images/connector/auth_bot_to_bot_connector.png)

### (Bot -> Connector) Step 1: POST to the MSA/AAD v2 login service to retrieve a token

Issue an HTTPS POST call to https://login.microsoftonline.com/common/oauth2/v2.0/token with the following form-encoded values:

    grant_type=client_credentials
    client_id=<YOUR MICROSOFT APP ID>
    client_secret=<YOUR MICROSOFT APP PASSWORD>
    scope=https://graph.microsoft.com/.default

Example request to the MSA/AAD server:

    -- connect to login.microsftonline.com --
    POST /common/oauth2/v2.0/token HTTP/1.1

    grant_type=client_credentials&client_id=<YOUR MICROSOFT APP ID>&client_secret=<YOUR MICROSOFT APP PASSWORD>&scope=https%3A%2F%2Fgraph.microsoft.com%2F.default



### (Bot -> Connector) Step 2: Receive JSON containing JWT token from MSA/AAD v2 login service

If your application is authorized by the MSA/AAD v2 login service, it will return a JSON payload containing your access token, its type, and its expiration (in seconds). The should is used in its existing form, without further escaping or encoding. It may be used until its expiration, and we encourage bot developers to cache and proactively refresh this token to keep it from impacting your bot's performance.

Example response from the MSA/AAD server:

    HTTP/1.1 200 OK
    ... (other headers) 

    {"token_type":"Bearer","expires_in":3600,"ext_expires_in":3600,"access_token":"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJZb3UgZm91bmQgdGhlIG1hcmJsZSBpbiB0aGUgb2F0bWVhbCEiLCJuYW1lIjoiQm90IEZyYW1ld29yayJ9.JPyDDC5yKmHfOS7Gz2jjEhOPvZ6iStYFu9XlkZDc7wc"}



### (Bot -> Connector) Step 3: Send this token to the Bot Connector service

When you call the Bot Connector service, simply add an Authorization header with "Bearer" scheme and the raw JWT token as the authorization value. This header must be present on all calls to the Bot Connector service. The Bot Connector will authorize the request if the token is correctly formed, is not expired, and was generated by the MSA/AAD v2 login service. Further checks are performed to ensure the token belongs to the bot executing the request.

Example request:

    -- connect to groupme.botframework.com --
    POST /v3/conversations/12345/activities HTTP/1.1
    Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJZb3UgZm91bmQgdGhlIG1hcmJsZSBpbiB0aGUgb2F0bWVhbCEiLCJuYW1lIjoiQm90IEZyYW1ld29yayJ9.JPyDDC5yKmHfOS7Gz2jjEhOPvZ6iStYFu9XlkZDc7wc
    
    ... (JSON-serialized activity message follows)

**NOTE**: DO NOT include this header on HTTP requests to other services. The JWT token from the MSA/AAD v2 login service is like a password -- it allows the holder of this token to perform operations on behalf of your bot. **Do not send your JWT token over unsecured channels, and do not send it to services other than the Bot Connector**.


## Authenticating calls from the Bot Connector service to the bot

When the Bot Connector service calls a bot, it includes a JWT token that was signed by the Bot Connector. To verify the authenticity of this token, your bot can download an OpenId metadata document from a well-known endpoint and ensure that the JWT token is signed with a key listed in the document.

![Authenticate calls from the Bot Connector to your bot](/en-us/images/connector/auth_bot_connector_to_bot.png)

### (Connector -> Bot) Step 1: Download the OpenId metadata document

Issue HTTPS GET calls to download the OpenId JSON document hosted at https://api.aps.skype.com/v1/.well-known/openidconfiguration. Hardcode this URL into your application. This document contains a link to a second document listing the valid signing keys used by the Bot Framework. The URL of this document is inside the "jwks_uri" property.

Example request:

    -- connect to api.aps.skype.com --
    GET /v1/.well-known/openidconfiguration HTTP/1.1

Example response:

{% highlight json %}

{ 
    "issuer":"https://api.botframework.com",
    "authorization_endpoint":"https://invalid.botframework.com/",
    "jwks_uri":"https://api.aps.skype.com/v1/keys",
    "id_token_signing_alg_values_supported":["RSA256"],
    "token_endpoint_auth_methods_supported":["private_key_jwt"]
}

{% endhighlight %}

### (Connector -> Bot) Step 2: Download the list of valid signing keys

Issue an HTTPS GET to this document to retrieve a list of valid signing keys. The document is in the JWK format (see [references](#references) for more details). The list of keys is relatively stable and may be cached for long periods of time (by default, 5 days within the Bot Builder SDK).

    -- connect to api.aps.skype.com --
    GET /v1/keys HTTP/1.1

### (Connector -> Bot) Step 3: Verify the JWT token

This is the most complicated step in the auth flow. To authorize the Bot Connector, you must extract the token from the HTTP Authorization header, parse the token, verify its contents, and verify its signature. JWT parsing libraries are available for many platforms and most implement secure and reliable parsing for JWT tokens, although you must typically configure these libraries to require that certain characteristics of the token (its issuer, audience, etc.) contain correct values.

When parsing the token, you must configure the parsing library or write your own validation to ensure the token meets the following requirements:

1. The token was sent in the HTTP Authorization header with "Bearer" scheme
2. The token is valid JSON that conforms to the JWT standard (see [references](#references))
3. The token contains an issuer claim with value of https://api.botframework.com
4. The token contains an audience claim with a value equivalent to your bot's Microsoft App ID.
5. The token has not yet expired. Industry-standard clock-skew is 5 minutes.
6. The token has a valid cryptographic signature with a key listed in the OpenId keys document retrieved in step 1, above.

If the token supplied by the Bot Connector does not meet all 5 requirements, the bot should terminate the request. Typically it is standard to return an HTTP 403 (Forbidden) status code.

**NOTE**: All 6 requirements are important, particularly 4 and 6. Failure to implement ALL verification requirements will leave the bot open to attacks which could cause the bot to divulge its JWT tokens.

**NOTE**: Implementers should not expose a way to disable validation of the JWT token sent to the bot.


## Enabling authentication from the Bot Framework Emulator

The Bot Framework Emulator is a desktop tool that developers can use to test the functionality of their bot. The Bot Framework Emulator uses the same authentication technologies listed above but is unable to impersonate the real Bot Connector service. Instead, it uses the Microsoft App ID and Microsoft App Password supplied by the developer (typically pasted into the App ID and Password fields in the Emulator UI, if using the Windows desktop version of the emulator) and it creates tokens identical to those that the bot creates. When it authenticates to the bot, it sends this token to the bot -- in essence, it uses the bot's own credentials when connecting to the bot.

If you are implementing an authentication library and want to accept calls from the Bot Framework Emulator, you must add this additional verification path.

This verification path is structurally similar to the Connector -> Bot verification path, but it uses MSA's OpenId document instead of the Bot Connector's OpenId document.

![Authenticate calls from the Bot Framework Emulator to your bot](/en-us/images/connector/auth_bot_framework_emulator_to_bot.png)

### (Emulator -> Bot) Steps 1 and 2: Download the MSA OpenId metadata document

Follow the steps above to issue an HTTPS GET call to https://login.microsoftonline.com/common/v2.0/.well-known/openid-configuration. Find the "jwks_uri" property and issue an HTTPS GET call to retrieve and parse the list of valid signing keys.

Example request:

    -- connect to login.microsoftonline.com --
    GET /common/v2.0/.well-known/openid-configuration HTTP/1.1

Key retrieval process follows earlier example for verifying the Connector from a Bot.

### (Emulator -> Bot) Step 3: Verify the JWT token

Extract the token as you would a JWT token sent by the Bot Connector. However, use these requirements instead:

1. The token was sent in the HTTP Authorization header with "Bearer" scheme
2. The token is valid JSON that conforms to the JWT envelope standard (see [references](#references))
3. The token contains an issuer claim with value of https://sts.windows.net/72f988bf-86f1-41af-91ab-2d7cd011db47/
4. The token contains an audience claim with value of https://graph.microsoft.com
5. The token contains an a claim of type "appid" from the issuer above, with the value equal to the bot's Microsoft app ID.
6. The token has not yet expired. Industry-standard clock-skew is 5 minutes.
7. The token has a valid cryptographic signature with a key listed in the OpenId keys document retrieved in step 1, above.

**NOTE**: Requirement 5 is a new requirement specific to the emulator verification path.

The same precautions must be taken in parsing this token as when parsing a token from the Bot Connector service. Failure to implement all validation requirements will leave the bot vulnerable to attack.

<a name="references"/>

## References

1. [JSON Web Token (JWT) draft-jones-json-web-token-07](http://openid.net/specs/draft-jones-json-web-token-07.html)
2. [JSON Web Signature (JWS) draft-jones-json-web-signature-04](https://tools.ietf.org/html/draft-jones-json-web-signature-04)
3. [JSON Web Key (JWK) RFC 7517](https://tools.ietf.org/html/rfc7517)
