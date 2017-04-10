---
title: Adding Location Capabilities with Cognitive Services | Microsoft Docs
description: Add location capabilities to your bot with the Bot Framework and Cognitive Services.
keywords: intelligence, location, address, lookup, validation,
author: RobStand
manager: rstand
ms.topic: intelligence-location-article

ms.prod: bot-framework
ms.service: Cognitive Services
ms.date: 
ms.reviewer: rstand

#ROBOTS: Index
---

# Add location control capabilities to your bot

You can find screenshots of the scenarios supported by the location control in the [Examples](#examples) section.

> [!IMPORTANT]
>As a prerequisite to use the location control, you must obtain a Bing Maps API subscription key. You can sign up to get a free key with up to 10,000 transactions per month in <a href="https://azure.microsoft.com/en-us/marketplace/partners/bingmaps/mapapis/" target="_blank">Azure Portal</a>.

This article describes how to add the location control to your bot. You can find the full developer guides and sample bots for <a href="https://github.com/Microsoft/BotBuilder-Location/tree/master/CSharp" target="_blank">C#</a> and <a href="https://github.com/Microsoft/BotBuilder-Location/tree/master/Node" target="_blank">Node.js</a> in the <a href="https://github.com/Microsoft/BotBuilder-Location/tree/master/" target="_blank">BotBuilder-Location</a> GitHub repository.

## Start using the location control

In C#, import the `Microsoft.Bot.Builder.Location` package from <a href="https://www.nuget.org/packages/Microsoft.ProjectOxford.Vision/" target="_blank">NuGet</a> and add the following namespace in your code.

```cs
using Microsoft.Bot.Builder.Location;
```

In Node.js, install the `BotBuilder-Location` module by using **npm** and then load it in your code.

```
npm install --save botbuilder-location    
```

```javascript
var locationDialog = require('botbuilder-location');
```

## Calling the location control with default parameters

The example initiates the location control with default parameters, which returns a custom prompt message that asks the user to provide an address.

```cs
var apiKey = WebConfigurationManager.AppSettings["BingMapsApiKey"];
var prompt = "Where should I ship your order? Type or say an address.";
var locationDialog = new LocationDialog(apiKey, message.ChannelId, prompt);
context.Call(locationDialog, (dialogContext, result) => {...});
```

```javascript
locationDialog.getLocation(session,
 { prompt: "Where should I ship your order? Type or say an address." });
```

## Using FB Messenger's location picker GUI dialog

FB Messenger supports a location picker GUI dialog to let the user select an address. If you prefer to use FB Messenger's native dialog,  pass the `LocationOptions.UseNativeControl` option in the location control's constructor.  

```cs
var apiKey = WebConfigurationManager.AppSettings["BingMapsApiKey"];
var prompt = "Where should I ship your order? Type or say an address.";
var locationDialog = new LocationDialog(apiKey, message.ChannelId, prompt, LocationOptions.UseNativeControl);
context.Call(locationDialog, (dialogContext, result) => {...});
```

```javascript
var options = {
    prompt: "Where should I ship your order? Type or say an address.",
    useNativeControl: true
};
locationDialog.getLocation(session, options);
```

FB Messenger by default returns only the lat/long coordinates for any address selected via the location picker GUI dialog. You can also use the `LocationOptions.ReverseGeocode` option to have Bing reverse geocode the returned coordinates and automatically fill in the remaining address fields.

```cs
var apiKey = WebConfigurationManager.AppSettings["BingMapsApiKey"];
var prompt = "Where should I ship your order? Type or say an address.";
var locationDialog = new LocationDialog(apiKey, message.ChannelId, prompt, LocationOptions.UseNativeControl | LocationOptions.ReverseGeocode);
context.Call(locationDialog, (dialogContext, result) => {...});
```

```javascript
var options = {
    prompt: "Where should I ship your order? Type or say an address.",
    useNativeControl: true,
    reverseGeocode: true
};
locationDialog.getLocation(session, options);
```

> [!WARNING]
> Reverse geocoding is an inherently imprecise operation. For that reason, when the reverse geocode option is selected, the location control will collect only the `PostalAddress.Locality`, `PostalAddress.Region`, `PostalAddress.Country` and `PostalAddress.PostalCode` fields and ask the user to provide the desired street address manually.

## Specifying required fields

You can specify required location fields that need to be collected by the control. If the user does not provide values for one or more required fields, the control will prompt him to fill them in. You can specify required fields by passing them in the location control's constructor using the `LocationRequiredFields` enumeration. The example specifies the street address and postal (zip) code as required.


[!code-js[Required Fields (C Sharp)](~/includes/code/intelligence-location-control.cs#specifyingRequiredFields)]
[!code-js[Required Fields (Javascript)](~/includes/code/intelligence-location-control.js#specifyingRequiredFields)]

## Handling returned location

The following example shows how you can leverage the location object returned by the location control in your bot code.

```cs
var apiKey = WebConfigurationManager.AppSettings["BingMapsApiKey"];
var prompt = "Where should I ship your order? Type or say an address.";
var locationDialog = new LocationDialog(apiKey, message.ChannelId, prompt, LocationOptions.None, LocationRequiredFields.StreetAddress | LocationRequiredFields.PostalCode);
context.Call(locationDialog, (context, result) => {
    Place place = await result;
    if (place != null)
    {
        var address = place.GetPostalAddress();
        string name = address != null ?
            $"{address.StreetAddress}, {address.Locality}, {address.Region}, {address.Country} ({address.PostalCode})" :
            "the pinned location";
        await context.PostAsync($"OK, I will ship it to {name}");
    }
    else
    {
        await context.PostAsync("OK, cancelled");
    }
}
```

[!code-js[sample](~/includes/code/intelligence-location-control.js#handlingReturnedLocation)]

## Examples

These examples show different location selection scenarios that the Bing location control supports.

**Address selection with single result returned**

![Single Address](~/media/skype_singleaddress_2.png)

**Address selection with multiple results returned**

![Multiple Addresses](~/media/skype_multiaddress_1.png)

**Address selection with required fields filling**

![Required Fields](~/media/skype_requiredaddress_1.png)

**Address selection using FB Messenger's location picker GUI dialog**

![Messenger Location Dialog](~/media/messenger_locationdialog_1.png)
