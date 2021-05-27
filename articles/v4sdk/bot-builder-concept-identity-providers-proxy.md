---
title: Identity providers proxy - Bot Service
description: Creating an OAuth2 proxy service to call custom or advanced identity providers in the Azure Bot Service.
keywords: azure bot service, authentication, identity providers proxy, bot framework token service
author: kamrani
ms.author: kamrani
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.date: 05/24/2021
monikerRange: 'azure-bot-service-4.0'
---

# Identity providers proxy

This document explains how to create a proxy to interact with custom or advanced identity providers that use OAuth2 protocol. 
 
The Bot Framework token service supports a bot to help users when logging in a variety of identity providers that use the OAuth2 protocol. However, identity providers can deviate from the core OAuth2 protocol, by offering more advanced capabilities, or alternative sign-in options. In these cases, you may not find an appropriate *connection setting configuration* that works for you. A possible solution is to write an OAuth2 provider proxy that is in between the Bot Framework token service and the *more customized or advanced* identity provider. 
 
You can configure the connection setting to call this proxy, and have this proxy make the calls to the custom or advanced identity provider. The proxy can also map or transform responses to make them conform to what the Bot Framework token service expects. 


## OAuth2 Proxy Service

To build an **OAuth2 Proxy Service**, you need to implement a REST service with two OAuth2 APIs: one for authorization and one for retrieving a token. Below you will find a C# example of each of these methods and what you can do in these methods to call a custom or advanced identity provider.

### Authorize API

The authorize API is an **HTTP GET** that authorizes the caller, generates a code property, and redirects to the redirect URI. 

```csharp
[HttpGet("authorize")]
public ActionResult Authorize(
    string response_type, 
    string client_id, 
    string state, 
    string redirect_uri, 
    string scope = null)
{
    // validate parameters
    if (string.IsNullOrEmpty(state))
    {
        return BadRequest("Authorize request missing parameter 'state'");
    }

    if (string.IsNullOrEmpty(redirect_uri))
    {
        return BadRequest("Authorize request missing parameter 'redirect_uri'");
    }

    // redirect to an external identity provider, 
    // or for this sample, generate a code and token pair and redirect to the redirect_uri

    var code = Guid.NewGuid().ToString("n");
    var token = Guid.NewGuid().ToString("n");
    _tokens.AddOrUpdate(code, token, (c, t) => token);

    return Redirect($"{redirect_uri}?code={code}&state={state}");
}
```

### Token API

The Token API is an **HTTP POST** that is called by the Bot Framework token service. The Bot Framework token service will send the `client_id` and `client_secret` in the request’s body. These values should be validated and/or passed along to the custom or advanced identity provider.
The response to this call is a JSON object containing the `access_token` and expiration value of the token (all other values are ignored). If your identity provider returns an `id_token` or some other value that you want to return instead, you just need to map it to the `access_token` property of your response before you return.

```csharp
[HttpPost("token")]
public async Task<ActionResult> Token()
{
    string body;

    using (var reader = new StreamReader(Request.Body))
    {
        body = await reader.ReadToEndAsync();
    }

    if (string.IsNullOrEmpty(body))
    {
        return BadRequest("Token request missing body");
    }

    var parameters = HttpUtility.ParseQueryString(body);
    string authorizationCode = parameters["code"];
    string grantType = parameters["grant_type"];
    string clientId = parameters["client_id"];
    string clientSecret = parameters["client_secret"];
    string redirectUri= parameters["redirect_uri"];

    // Validate any of these parameters here, or call out to an external identity provider with them

    if (_tokens.TryRemove(authorizationCode, out string token))
    {
        return Ok(new TokenResponse()
        {
            AccessToken = token,
            ExpiresIn = 3600,
            TokenType = "custom",
        });
    }
    else
    {
        return BadRequest("Token request body did not contain parameter 'code'");
    }
}
```

## Proxy Connection Setting Configuration

Once you have your **OAuth2 Proxy Service** running, you can create an *OAuth Service Provider Connection Setting* on your Azure Bot Service resource. Follow the steps described below.

1. Give a name to the connection setting.
1. Select the **Generic Oauth 2** service provider. 
1. Include whatever `client id` and `client secret` that are appropriate. Perhaps these are the values from your advanced/custom identity provider, or these could be specific just to your proxy if the identity provider you are using does not use client id and secret.
1. For the **Authorization URL**, you should copy the address of your authorization REST API, for example `https://proxy.com/api/oauth/authorize`.
1. For the **Token and Refresh URL**, you should copy the address of your token REST API, for example `https://proxy.com/api/oauth/token`. The Token Exchange URL is valid only for AAD based providers and so can be ignored.
1. Finally, add any scopes that are appropriate.

## OAuthController for .NET Core 3.1 ASP.NET Web App

```csharp
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.IO;
using System.Threading.Tasks;
using System.Web;

namespace CustomOAuthProvider.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OAuthController : ControllerBase
    {
        ConcurrentDictionary<string, string> _tokens;

        public OAuthController(ConcurrentDictionary<string, string> tokens)
        {
            _tokens = tokens;
        }

        [HttpGet("authorize")]
        public ActionResult Authorize(
            string response_type, 
            string client_id, 
            string state, 
            string redirect_uri, 
            string scope = null)
        {
            if (string.IsNullOrEmpty(state))
            {
                return BadRequest("Authorize request missing parameter 'state'");
            }

            if (string.IsNullOrEmpty(redirect_uri))
            {
                return BadRequest("Authorize request missing parameter 'redirect_uri'");
            }

            // reidrect to an external identity provider, 
            // or for this sample, generte a code and token pair and redirect to the redirect_uri

            var code = Guid.NewGuid().ToString("n");
            var token = Guid.NewGuid().ToString("n");
            _tokens.AddOrUpdate(code, token, (c, t) => token);

            return Redirect($"{redirect_uri}?code={code}&state={state}");
        }

        [HttpPost("token")]
        public async Task<ActionResult> Token()
        {
            string body;

            using (var reader = new StreamReader(Request.Body))
            {
                body = await reader.ReadToEndAsync();
            }

            if (string.IsNullOrEmpty(body))
            {
                return BadRequest("Token request missing body");
            }

            var parameters = HttpUtility.ParseQueryString(body);
            string authorizationCode = parameters["code"];
            string grantType = parameters["grant_type"];
            string clientId = parameters["client_id"];
            string clientSecret = parameters["client_secret"];
            string redirectUri= parameters["redirect_uri"];

            // Validate any of these parameters here, or call out to an external identity provider with them

            if (_tokens.TryRemove(authorizationCode, out string token))
            {
                return Ok(new TokenResponse()
                {
                    AccessToken = token,
                    ExpiresIn = 3600,
                    TokenType = "custom",
                });
            }
            else
            {
                return BadRequest("Token request body did not contain parameter 'code'");
            }
        }
    }

    public class TokenResponse
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("id_token")]
        public string IdToken { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }

        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }

        [JsonProperty("scope")]
        public string Scope { get; set; }
    }
}
```