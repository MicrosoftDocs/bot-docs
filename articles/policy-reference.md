---
title: Built-in policy definitions for Azure Bot Service
description: Lists Azure Policy built-in policy definitions for Azure Bot Service. These built-in policy definitions provide common approaches to managing your Azure resources.
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: micchow
ms.service: bot-service
ms.topic: reference
ms.date: 04/21/2021
ms.custom: subject-policy-reference
---
# Azure Policy built-in definitions for Azure Bot Service

This page is an index of [Azure Policy](/azure/governance/policy/overview) built-in policy
definitions for Azure Bot Service. For additional Azure Policy built-ins for other services, see
[Azure Policy built-in definitions](/azure/governance/policy/samples/built-in-policies).

The name of each built-in policy definition links to the policy definition in the Azure portal. Use
the link in the **Version** column to view the source on the
[Azure Policy GitHub repo](https://github.com/Azure/azure-policy).

## Azure Bot Service

|Name<br /><sub>(Azure portal)</sub> |Description |Effect(s) |Version<br /><sub>(GitHub)</sub> |
|---|---|---|---|
|[Bot Service endpoint should be a valid HTTPS URI](https://portal.azure.com/#blade/Microsoft_Azure_Policy/PolicyDetailBlade/definitionId/%2Fproviders%2FMicrosoft.Authorization%2FpolicyDefinitions%2F6164527b-e1ee-4882-8673-572f425f5e0a) |Data can be tampered with during transmission. Protocols exist that provide encryption to address problems of misuse and tampering. To ensure your bots are communicating only over encrypted channels, set the endpoint to a valid HTTPS URI. This ensures the HTTPS protocol is used to encrypt your data in transit and is also often a requirement for compliance with regulatory or industry standards. Please visit: [/azure/bot-service/bot-builder-security-guidelines](/azure/bot-service/bot-builder-security-guidelines). |audit, deny, disabled |[1.0.1](https://github.com/Azure/azure-policy/blob/master/built-in-policies/policyDefinitions/Bot%20Service/BotService_ValidEndpoint_Audit.json) |
|[Bot Service should be encrypted with a customer-managed key](https://portal.azure.com/#blade/Microsoft_Azure_Policy/PolicyDetailBlade/definitionId/%2Fproviders%2FMicrosoft.Authorization%2FpolicyDefinitions%2F51522a96-0869-4791-82f3-981000c2c67f) |Azure Bot Service automatically encrypts your resource to protect your data and meet organizational security and compliance commitments. By default, Microsoft-managed encryption keys are used. For greater flexibility in managing keys or controlling access to your subscription, select customer-managed keys, also known as bring your own key (BYOK). Learn more about Azure Bot Service encryption: [/azure/bot-service/bot-service-encryption](/azure/bot-service/bot-service-encryption). |audit, deny, disabled |[1.0.0](https://github.com/Azure/azure-policy/blob/master/built-in-policies/policyDefinitions/Bot%20Service/BotService_CMKEnabled_Audit.json) |

## Next steps

- See the built-ins on the [Azure Policy GitHub repo](https://github.com/Azure/azure-policy).
- Review the [Azure Policy definition structure](/azure/governance/policy/concepts/definition-structure).
- Review [Understanding policy effects](/azure/governance/policy/concepts/effects).
