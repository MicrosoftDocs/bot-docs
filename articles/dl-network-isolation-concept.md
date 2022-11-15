---
title: About network isolation in Azure Bot Service
description: Learn about Azure Virtual Network and how a virtual network lets you restrict user access to your bot.
displayName: private network, isolated network
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: jameslew
ms.service: bot-service
ms.topic: conceptual
ms.date: 04/05/2022
---

# Network isolation in Azure Bot Service

This article covers concepts around network isolation for your Azure bot and its dependent services.

You may want to restrict access to your bot to a private network.
The only way to do this in the Azure Bot Service is to use the Direct Line App Service extension.
For example, you can use the App Service extension to host a company-internal bot and require users to access the bot from within your company network.

For detailed instructions on how to configure your bot in a private network, see how to [Use an isolated network](./dl-network-isolation-how-to.md).

For more information about the features that support network isolation, see:

| Feature                                  | Article                                                                                        |
|:-----------------------------------------|:-----------------------------------------------------------------------------------------------|
| Direct Line App Service extension        | [Direct Line App Service extension](./bot-service-channel-directline-extension.md)             |
| Azure Virtual Network                    | [What is Azure Virtual Network?](/azure/virtual-network/virtual-networks-overview)             |
| Azure network security groups            | [Network security groups](/azure/virtual-network/network-security-groups-overview)             |
| Azure Private Link and private endpoints | [What is a private endpoint?](/azure/private-link/private-endpoint-overview)                   |
| Azure DNS                                | [Create an Azure DNS zone and record using the Azure portal](/azure/dns/dns-getstarted-portal) |

## Use of private endpoints

When your bot endpoint is within a virtual network, and with the appropriate rules set in your network security group, you can restrict access to both inbound and outbound requests for your bot's app service by using a private endpoint.

Private endpoints are available in the Bot Service via the Direct Line App Service extension. See the requirements for using private endpoints below:

1. Activities must be sent to and from the App Service endpoint.

    The App Service extension is co-located with your bot endpoint app service. All messages to and from the endpoint are local to your virtual network and reach your client directly without being sent to Bot Framework services.

1. For [user authentication](./v4sdk/bot-builder-concept-authentication.md) to work, your bot client needs to communicate with the service provider&mdash;such as Azure Active Directory or GitHub&mdash;and the token endpoint.

    If your bot client is in your virtual network, you'll need to allowlist both endpoints from within your virtual network. Do this for the token endpoint via [service tags](./bot-service-channel-directline-extension-vnet.md). Your bot endpoint itself also needs access to the token endpoint, as described below.

1. With the App Service extension, your bot endpoint and the App Service extension need to send outbound HTTPS requests to Bot Framework services.

    These requests are for various meta operations, such as retrieving your bot configuration or retrieving tokens from the token endpoint. To facilitate these requests, you need to setup and configure a private endpoint.

## How the Bot Service implements private endpoints

There are two main scenarios where private endpoints are used:

- For your bot to access the token endpoint.
- For the Direct Line channel extension to access the Bot Service.

A private endpoint _projects_ required services into your virtual network, so that they're available inside your network directly, without exposing your virtual network to the internet or allow-listing any IP addresses. All traffic through a private endpoint goes through the Azure internal servers to ensure that your traffic isn't leaked to the internet.

The service uses two sub-resources, `Bot` and `Token`, to project services into your network. When you add a private endpoint, Azure generates a bot-specific DNS record for each sub-resource and configures the endpoint in the DNS zone group. This ensures that endpoints from different bots which target the same sub-resource can be distinguished from each other, while reusing the same DNS zone group resource.

## Example Scenario

Say you have a bot named **SampleBot** and a corresponding app service for it, `SampleBot.azurewebsites.net`, that serves as the messaging endpoint for this bot.
You configure a private endpoint for **SampleBot** with sub-resource type `Bot` in the Azure portal for public cloud, which creates a DNS zone group with an `A` record corresponding to `SampleBot.botplinks.botframework.com`. This DNS record maps to a local IP in your virtual network. Similarly, using the sub-resource type `Token` generates an endpoint, `SampleBot.bottoken.botframework.com`.

The `A` record in the DNS zone you created is mapped to an IP Address within your virtual network. So, requests sent to this endpoint are local to your network and don't violate rules in your network security group or Azure firewall that restrict outbound traffic from your network. The Azure networking layer and Bot Framework services ensure that your requests are not leaked to the public internet, and isolation is maintained for your network.
