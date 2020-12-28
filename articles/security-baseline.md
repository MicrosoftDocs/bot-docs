---
title: Azure security baseline for Azure Bot Service
description: The Azure Bot Service security baseline provides procedural guidance and resources for implementing the security recommendations specified in the Azure Security Benchmark.
author: msmbaldwin
ms.service: bot-service
ms.topic: conceptual
ms.date: 11/17/2020
ms.author: mbaldwin
ms.custom: subject-security-benchmark

# Important: This content is machine generated; do not modify this topic directly. Contact mbaldwin for more information.

---

# Azure security baseline for Azure Bot Service

This security baseline applies guidance from the [Azure Security Benchmark version 2.0](https://docs.microsoft.com/azure/security/benchmarks/overview) to Microsoft Azure Bot Service. The Azure Security Benchmark provides recommendations on how you can secure your cloud solutions on Azure. The content is grouped by the **security controls** defined by the Azure Security Benchmark and the related guidance applicable to Azure Bot Service. **Controls** not applicable to Azure Bot Service have been excluded.

To see how Azure Bot Service completely maps to the Azure Security Benchmark, see the [full Azure Bot Service security baseline mapping file](https://github.com/MicrosoftDocs/SecurityBenchmarks/tree/master/Azure%20Offer%20Security%20Baselines).

## Network Security

*For more information, see the [Azure Security Benchmark: Network Security](/azure/security/benchmarks/security-controls-v2-network-security).*

### NS-1: Implement security for internal traffic

**Guidance**: When you deploy Azure Bot Service resources, you must create or use an existing virtual network. Ensure that all Azure virtual networks follow an enterprise segmentation principle that aligns to the business risks. Any system that could incur higher risk for the organization should be isolated within its own virtual network and sufficiently secured with either a network security group (NSG) and/or Azure Firewall.

- [Use direct line app service extension within a VNET](https://docs.microsoft.com/azure/bot-service/bot-service-channel-directline-extension-vnet?view=azure-bot-service-4.0)

- [How to create a network security group with security rules](https://docs.microsoft.com/azure/virtual-network/tutorial-filter-network-traffic)



- [How to deploy and configure Azure Firewall](https://docs.microsoft.com/azure/firewall/tutorial-firewall-deploy-portal)

**Azure Security Center monitoring**: Yes

**Responsibility**: Customer

### NS-2: Connect private networks together

**Guidance**: Use Azure ExpressRoute or Azure virtual private network (VPN) to create private connections between Azure datacenters and on-premises infrastructure in a colocation environment. ExpressRoute connections do not go over the public internet, and they offer more reliability, faster speeds, and lower latencies than typical internet connections. For point-to-site VPN and site-to-site VPN, you can connect on-premises devices or networks to a virtual network using any combination of these VPN options and Azure ExpressRoute.

To connect two or more virtual networks in Azure together, use virtual network peering. Network traffic between peered virtual networks is private and is kept on the Azure backbone network.

- [What are the ExpressRoute connectivity models](https://docs.microsoft.com/azure/expressroute/expressroute-connectivity-models)

- [Azure VPN overview](https://docs.microsoft.com/azure/vpn-gateway/vpn-gateway-about-vpngateways)

- [Virtual network peering](https://docs.microsoft.com/azure/virtual-network/virtual-network-peering-overview)

**Azure Security Center monitoring**: Not applicable

**Responsibility**: Customer

### NS-4: Protect applications and services from external network attacks

**Guidance**: Protect your Azure Bot Service resources against attacks from external networks, including distributed denial of service (DDoS) Attacks, application-specific attacks, and unsolicited and potentially malicious internet traffic. Use Azure Firewall to protect applications and services against potentially malicious traffic from the internet and other external locations. Protect your assets against DDoS attacks by enabling DDoS standard protection on your Azure virtual networks. Use Azure Security Center to detect misconfiguration risks related to your network-related resources.

Use Web Application Firewall (WAF) capabilities in Azure Application gateway, Azure Front Door, and Azure Content Delivery Network (CDN) to protect your applications running on Azure Bot Service against application layer attacks.

- [Azure Firewall Documentation](https://docs.microsoft.com/azure/firewall/)

- [Manage Azure DDoS Protection Standard using the Azure portal](https://docs.microsoft.com/azure/virtual-network/manage-ddos-protection)

- [Azure Security Center recommendations](https://docs.microsoft.com/azure/security-center/recommendations-reference#recs-network)

- [How to deploy Azure WAF](https://docs.microsoft.com/azure/web-application-firewall/overview)

**Azure Security Center monitoring**: Yes

**Responsibility**: Customer

### NS-5: Deploy intrusion detection/intrusion prevention systems (IDS/IPS)

**Guidance**: Use Azure Firewall with threat intelligence-based filtering to alert on and/or block traffic to and from known malicious IP addresses and domains. The IP addresses and domains are sourced from the Microsoft Threat Intelligence feed. When payload inspection is required, you can deploy a third-party IDS/IPS solution from Azure Marketplace with payload inspection capabilities. Alternately you may choose to use host-based IDS/IPS or a host-based endpoint detection and response (EDR) solution in conjunction with or instead of network-based IDS/IPS.

Note: If you have a regulatory or other requirement for IDS/IPS use, ensure that it is always tuned to provide high-quality alerts to your SIEM solution.

- [How to deploy Azure Firewall](https://docs.microsoft.com/azure/firewall/tutorial-firewall-deploy-portal)

- [Azure Marketplace includes third party IDS capabilities](https://azuremarketplace.microsoft.com/marketplace?search=IDS)

- [Microsoft Defender ATP EDR capability](https://docs.microsoft.com/windows/security/threat-protection/microsoft-defender-atp/overviewendpoint-detection-response)

**Azure Security Center monitoring**: Not applicable

**Responsibility**: Customer

### NS-6: Simplify network security rules

**Guidance**: Use Virtual Network Service Tags to define network access controls on Network Security Groups or Azure Firewall configured for your Azure Bot Service resources. You can use service tags in place of specific IP addresses when creating security rules. By specifying the service tag name (For example: AzureBotService) in the appropriate source or destination field of a rule, you can allow or deny the traffic for the corresponding service. Microsoft manages the address prefixes encompassed by the service tag and automatically updates the service tag as addresses change.

- [Understand and using Service Tags](https://docs.microsoft.com/azure/virtual-network/service-tags-overview)

**Azure Security Center monitoring**: Not applicable

**Responsibility**: Customer

## Identity Management

*For more information, see the [Azure Security Benchmark: Identity Management](/azure/security/benchmarks/security-controls-v2-identity-management).*

### IM-1: Standardize Azure Active Directory as the central identity and authentication system

**Guidance**: Azure Bot Service uses Azure Active Directory (Azure AD) as the default identity and access management service. Standardize on Azure AD to govern your organization’s identity and access management in:
- Microsoft cloud resources, such as the Azure portal, Azure Storage, Azure Virtual Machines (Linux and Windows), Azure Key Vault, PaaS, and SaaS applications.

- Your organization's resources, such as applications on Azure or your corporate network resources.

Securing Azure AD should be a high priority in your organization’s cloud security practice. Azure AD provides an identity secure score to help you assess your identity security posture relative to Microsoft’s best practice recommendations. Use the score to gauge how closely your configuration matches best practice recommendations, and to make improvements in your security posture.

Note: Azure AD supports external identity providers, which allow users without a Microsoft account to sign in to their applications and resources with their external identity.

- [Tenancy in Azure AD](https://docs.microsoft.com/azure/active-directory/develop/single-and-multi-tenant-apps)

- [How to create and configure an Azure AD instance](https://docs.microsoft.com/azure/active-directory/fundamentals/active-directory-access-create-new-tenant)

- [Define Azure AD tenants](https://azure.microsoft.com/resources/securing-azure-environments-with-azure-active-directory/)

- [Use external identity providers for an application](https://docs.microsoft.com/azure/active-directory/b2b/identity-providers)

- [What is the identity secure score in Azure AD](https://docs.microsoft.com/azure/active-directory/fundamentals/identity-secure-score)

**Azure Security Center monitoring**: Yes

**Responsibility**: Customer

### IM-3: Use Azure AD single sign-on (SSO) for application access

**Guidance**: Azure AD provides identity and access management to Azure resources, cloud applications, and on-premises applications. Identity and access management applies to enterprise identities such as employees, as well as external identities such as partners, vendors, and suppliers.

Use Azure AD single sign-on (SSO) to manage and secure access to your organization’s data and resources on-premises and in the cloud. Connect all your users, applications, and devices to Azure AD for seamless, secure access, and greater visibility and control.

- [Understand application SSO with Azure AD](https://docs.microsoft.com/azure/active-directory/manage-apps/what-is-single-sign-on)

**Azure Security Center monitoring**: Not applicable

**Responsibility**: Customer

### IM-4: Use strong authentication controls for all Azure Active Directory based access

**Guidance**: Azure Bot Service uses Azure Active Directory that supports strong authentication controls through multi-factor authentication (MFA) and strong passwordless methods.
- Multi-factor authentication: Enable Azure AD MFA, follow Azure Security Center identity, and access management recommendations for your MFA setup. MFA can be enforced on all users, select users, or at the per-user level based on sign-in conditions and risk factors.

- Passwordless authentication: Three passwordless authentication options are available: Windows Hello for Business, Microsoft Authenticator app, and on-premises authentication methods such as smart cards.

For administrator and privileged users, ensure the highest level of the strong authentication method is used, followed by rolling out the appropriate strong authentication policy to other users.

If legacy password-based authentication is still used for Azure AD authentication, be aware that cloud-only accounts (user accounts created directly in Azure) have a default baseline password policy. And hybrid accounts (user accounts that come from on-premises Active Directory) follow the on-premises password policies. When using password-based authentication, Azure AD provides a password protection capability that prevents users from setting passwords that are easy to guess. Microsoft provides a global list of banned passwords that is updated based on telemetry, and customers can augment the list based on their needs (for example, branding, cultural references, etc.). This password protection can be used for cloud-only and hybrid accounts.

Note: Authentication based on password credentials alone is susceptible to popular attack methods. For higher security, use strong authentication such as MFA and a strong password policy. For third-party applications and marketplace services that may have default passwords, you should change them during initial service setup.

- [How to enable MFA in Azure](https://docs.microsoft.com/azure/active-directory/authentication/howto-mfa-getstarted)

- [Introduction to passwordless authentication options for Azure Active Directory](https://docs.microsoft.com/azure/active-directory/authentication/concept-authentication-passwordless)

- [Azure AD default password policy](https://docs.microsoft.com/azure/active-directory/authentication/concept-sspr-policy#password-policies-that-only-apply-to-cloud-user-accounts)

- [Eliminate bad passwords using Azure AD Password Protection](https://docs.microsoft.com/azure/active-directory/authentication/concept-password-ban-bad)

**Azure Security Center monitoring**: Yes

**Responsibility**: Customer

### IM-5: Monitor and alert on account anomalies

**Guidance**: Azure Bot Service is integrated with Azure Active Directory, which provides the following data sources:

- Sign-ins - The sign-ins report provides information about the usage of managed applications and user sign-in activities.

- Audit logs - Provides traceability through logs for all changes done by various features within Azure AD. Examples of audit logs include changes made to any resources within Azure AD like adding or removing users, apps, groups, roles, and policies.

- Risky sign-ins - A risky sign-in is an indicator for a sign-in attempt that might have been performed by someone who is not the legitimate owner of a user account.

- Users flagged for risk - A risky user is an indicator for a user account that might have been compromised.

These data sources can be integrated with Azure Monitor, Azure Sentinel or third-party SIEM systems.

Azure Security Center can also alert on certain suspicious activities such as excessive number of failed authentication attempts, deprecated accounts in the subscription.

Azure Advanced Threat Protection (ATP) is a security solution that can use Active Directory signals to identify, detect, and investigate advanced threats, compromised identities, and malicious insider actions.

Audit activity reports in the Azure Active Directory https://docs.microsoft.com/azure/active-directory/reports-monitoring/concept-audit-logs

- [How to view Azure AD risky sign-ins](https://docs.microsoft.com/azure/active-directory/reports-monitoring/concept-risky-sign-ins)

- [How to identify Azure AD users flagged for risky activity](https://docs.microsoft.com/azure/active-directory/reports-monitoring/concept-user-at-risk)

- [How to monitor users' identity and access activity in Azure Security Center](https://docs.microsoft.com/azure/security-center/security-center-identity-access)

- [Alerts in Azure Security Center's threat intelligence protection module](https://docs.microsoft.com//azure/security-center/alerts-reference)

- [How to integrate Azure Activity Logs into Azure Monitor](https://docs.microsoft.com/azure/active-directory/reports-)

**Azure Security Center monitoring**: Yes

**Responsibility**: Customer

### IM-7: Eliminate unintended credential exposure

**Guidance**: Azure Bot Service allows customers to deploy/run code or configurations or persisted data potentially with identities/secretes, it is recommended to implement Credential Scanner to identify credentials within code or configurations or persisted data. Credential Scanner will also encourage moving discovered credentials to more secure locations such as Azure Key Vault.

For GitHub, you can use native secret scanning feature to identify credentials or other form of secrets within the code.

- [How to setup Credential Scanner](https://secdevtools.azurewebsites.net/helpcredscan.html)

- [GitHub secret scanning](https://docs.github.com/github/administering-a-repository/about-secret-scanning)

**Azure Security Center monitoring**: Currently not available

**Responsibility**: Customer

## Privileged Access

*For more information, see the [Azure Security Benchmark: Privileged Access](/azure/security/benchmarks/security-controls-v2-privileged-access).*

### PA-2: Restrict administrative access to business-critical systems

**Guidance**: Azure Bot Service uses Azure RBAC to isolate access to business-critical systems by restricting which accounts are granted privileged access to the subscriptions and management groups they are in.

Ensure that you also restrict access to the management, identity, and security systems that have administrative access to your business critical assets, such as Active Directory Domain Controllers (DCs), security tools, and system management tools with agents installed on business critical systems. Attackers who compromise these management and security systems can immediately weaponize them to compromise business critical assets.

All types of access controls should be aligned to your enterprise segmentation strategy to ensure consistent access control.

Ensure to assign separate privileged accounts that are distinct from the standard user accounts used for email, browsing, and productivity tasks.

- [Azure Components and Reference model](https://docs.microsoft.com/security/compass/microsoft-security-compass-introduction#azure-components-and-reference-model-2151)

- [Management Group Access](https://docs.microsoft.com/azure/governance/management-groups/overview#management-group-access)

- [Azure subscription administrators](https://docs.microsoft.com/azure/cost-management-billing/manage/add-change-subscription-administrator)

**Azure Security Center monitoring**: Not applicable

**Responsibility**: Customer

### PA-3: Review and reconcile user access regularly

**Guidance**: Azure Bot Service uses Azure Active Directory (Azure AD) accounts to manage its resources, review user accounts and access assignment regularly to ensure the accounts and their level of access are valid. You can use Azure AD access reviews to review group memberships, access to enterprise applications, and role assignments. Azure AD reporting can provide logs to help discover stale accounts. You can also use Azure AD Privileged Identity Management to create an access review report workflow that facilitates the review process.

In addition, Azure Privileged Identity Management can be configured to alert when an excessive number of administrator accounts are created, and to identify administrator accounts that are stale or improperly configured.

Note: Some Azure services support local users and roles that aren't managed through Azure AD. You must manage these users separately.

- [Create an access review of Azure resource roles in Privileged Identity Management (PIM)](https://docs.microsoft.com/azure/active-directory/privileged-identity-management/pim-resource-roles-start-access-review)

- [How to use Azure AD identity and access reviews](https://docs.microsoft.com/azure/active-directory/governance/access-reviews-overview)

**Azure Security Center monitoring**: Yes

**Responsibility**: Customer

### PA-4: Set up emergency access in Azure AD

**Guidance**: Azure Bot Service uses Azure Active Directory to manage its resources. To prevent being accidentally locked out of your Azure AD organization, set up an emergency access account for access when normal administrative accounts cannot be used. Emergency access accounts are usually highly privileged, and they should not be assigned to specific individuals. Emergency access accounts are limited to emergency or "break glass"' scenarios where normal administrative accounts can't be used.

You should ensure that the credentials (such as password, certificate, or smart card) for emergency access accounts are kept secure and known only to individuals who are authorized to use them only in an emergency.

- [Manage emergency access accounts in Azure AD](https://docs.microsoft.com/azure/active-directory/users-groups-roles/directory-emergency-access)

**Azure Security Center monitoring**: Not applicable

**Responsibility**: Customer

### PA-5: Automate entitlement management

**Guidance**: Azure Bot Service is integrated with Azure Active Directory to manage its resources. Use Azure AD entitlement management features to automate access request workflows, including access assignments, reviews, and expiration. Dual or multi-stage approval is also supported.

- [What are Azure AD access reviews](https://docs.microsoft.com/azure/active-directory/governance/access-reviews-overview)

- [What is Azure AD entitlement management](https://docs.microsoft.com/azure/active-directory/governance/entitlement-management-overview)

**Azure Security Center monitoring**: Not applicable

**Responsibility**: Customer

### PA-6: Use privileged access workstations

**Guidance**: Secured, isolated workstations are critically important for the security of sensitive roles like administrators, developers, and critical service operators. Use highly secured user workstations and/or Azure Bastion for administrative tasks. Use Azure Active Directory, Microsoft Defender Advanced Threat Protection (ATP), and/or Microsoft Intune to deploy a secure and managed user workstation for administrative tasks. The secured workstations can be centrally managed to enforce secured configuration including strong authentication, software and hardware baselines, restricted logical and network access.

- [Understand privileged access workstations](https://docs.microsoft.com/azure/active-directory/devices/concept-azure-managed-workstation)

- [Deploy a privileged access workstation](https://docs.microsoft.com/azure/active-directory/devices/howto-azure-managed-workstation)

**Azure Security Center monitoring**: Not applicable

**Responsibility**: Customer

### PA-7: Follow just enough administration (least privilege principle)

**Guidance**: Azure Bot Service is integrated with Azure role-based access control (RBAC) to manage its resources. Azure RBAC allows you to manage Azure resource access through role assignments. You can assign these roles to users, groups service principals and managed identities. There are pre-defined built-in roles for certain resources, and these roles can be inventoried or queried through tools such as Azure CLI, Azure PowerShell or the Azure portal. The privileges you assign to resources through the Azure RBAC should be always limited to what is required by the roles. This complements the just in time (JIT) approach of Azure AD Privileged Identity Management (PIM) and should be reviewed periodically.

Use built-in roles to allocate permission and only create custom role when required.

What is Azure role-based access control (Azure RBAC) https://docs.microsoft.com/azure/role-based-access-control/overview

- [How to configure RBAC in Azure](https://docs.microsoft.com/azure/role-based-access-control/role-assignments-portal)

- [How to use Azure AD identity and access reviews](https://docs.microsoft.com/azure/active-directory/governance/access-reviews-overview)

**Azure Security Center monitoring**: Not applicable

**Responsibility**: Customer

## Data Protection

*For more information, see the [Azure Security Benchmark: Data Protection](/azure/security/benchmarks/security-controls-v2-data-protection).*

### DP-2: Protect sensitive data

**Guidance**: Protect sensitive data by restricting access using Azure Role Based Access Control (Azure RBAC), network-based access controls, and specific controls in Azure services (such as encryption in SQL and other databases).

To ensure consistent access control, all types of access control should be aligned to your enterprise segmentation strategy. The enterprise segmentation strategy should also be informed by the location of sensitive or business critical data and systems.

For the underlying platform, which is managed by Microsoft, Microsoft treats all customer content as sensitive and guards against customer data loss and exposure. To ensure customer data within Azure remains secure, Microsoft has implemented some default data protection controls and capabilities.

- [Azure Role Based Access Control (RBAC)](https://docs.microsoft.com/azure/role-based-access-control/overview)

- [Understand customer data protection in Azure](https://docs.microsoft.com/azure/security/fundamentals/protection-customer-data)

**Azure Security Center monitoring**: Not applicable

**Responsibility**: Shared

### DP-3: Monitor for unauthorized transfer of sensitive data

**Guidance**: Monitor for unauthorized transfer of data to locations outside of enterprise visibility and control. This typically involves monitoring for anomalous activities (large or unusual transfers) that could indicate unauthorized data exfiltration.

Azure Storage Advanced Threat Protection (ATP) and Azure SQL ATP can alert on anomalous transfer of information that might indicate unauthorized transfers of sensitive information.

Azure Information protection (AIP) provides monitoring capabilities for information that has been classified and labeled.

If required for compliance of data loss prevention (DLP), you can use a host-based DLP solution to enforce detective and/or preventative controls to prevent data exfiltration.

- [Enable Azure SQL ATP](https://docs.microsoft.com/azure/azure-sql/database/threat-detection-overview)

- [Enable Azure Storage ATP](https://docs.microsoft.com/azure/storage/common/storage-advanced-threat-protection?tabs=azure-security-center)

**Azure Security Center monitoring**: Currently not available

**Responsibility**: Customer

### DP-4: Encrypt sensitive information in transit

**Guidance**: All of the Azure Bot Service endpoints are exposed over HTTP enforce TLS 1.2. With an enforced security protocol, consumers attempting to call an Azure Bot Service endpoint should adhere to these guidelines:

- The client Operating System (OS) needs to support TLS 1.2.
- The language (and platform) used to make the HTTP call need to specify TLS 1.2 as part of the request. Depending on the language and platform, specifying TLS is done either implicitly or explicitly.

To complement access controls, data in transit should be protected against ‘out of band’ attacks (e.g., traffic capture) using encryption to ensure that attackers cannot easily read or modify the data.

While this is optional for traffic on private networks, this is critical for traffic on external and public networks. For HTTP traffic, ensure that any clients connecting to your Azure resources can negotiate TLS v1.2 or greater. For remote management, use SSH (for Linux) or RDP/TLS (for Windows) instead of an unencrypted protocol. Obsoleted SSL, TLS, and SSH versions and protocols, and weak ciphers should be disabled.

By default, Azure provides encryption for data in transit between Azure data centers.

- [Azure Bot Service enforcing transport layer security (TLS) 1.2](https://azure.microsoft.com/updates/azure-bot-service-enforcing-transport-layer-security-tls-1-2/)

- [Understand encryption in transit with Azure](https://docs.microsoft.com/azure/security/fundamentals/encryption-overview#encryption-of-data-in-transit)

- [Information on TLS Security](https://docs.microsoft.com/security/engineering/solving-tls1-problem)

- [Double encryption for Azure data in transit](https://docs.microsoft.com/azure/security/fundamentals/double-encryption#data-in-transit)

**Azure Security Center monitoring**: Yes

**Responsibility**: Shared

### DP-5: Encrypt sensitive data at rest

**Guidance**: To complement access controls, data at rest should be protected against ‘out of band’ attacks (such as accessing underlying storage) using encryption. This helps ensure that attackers cannot easily read or modify the data.

Azure provides data at rest encryption by default. For highly sensitive data, you have options to implement additional encryption at rest on all Azure resources where available. Azure manages your encryption keys by default, but Azure provides options to manage your own keys (customer-managed keys) for certain Azure services.

If required for compliance on compute resources, implement a third-party tool, such as an automated host-based Data Loss Prevention solution, to enforce access controls to data even when data is copied off a system.

- [Understand encryption at rest in Azure](https://docs.microsoft.com/azure/security/fundamentals/encryption-atrest#encryption-at-rest-in-microsoft-cloud-services)

- [How to configure customer managed encryption keys](https://docs.microsoft.com/azure/storage/common/storage-encryption-keys-portal)

- [Encryption Model and key management table](https://docs.microsoft.com/azure/security/fundamentals/encryption-models)

- [Data at rest double encryption in Azure](https://docs.microsoft.com/azure/security/fundamentals/double-encryption#data-at-rest)

**Azure Security Center monitoring**: Currently not available

**Responsibility**: Customer

## Asset Management

*For more information, see the [Azure Security Benchmark: Asset Management](/azure/security/benchmarks/security-controls-v2-asset-management).*

### AM-1: Ensure security team has visibility into risks for assets

**Guidance**: Ensure security teams are granted Security Reader permissions in your Azure tenant and subscriptions so they can monitor for security risks using Azure Security Center.

Depending on how security team responsibilities are structured, monitoring for security risks could  be the responsibility of a central security team or a local team. That said, security insights and risks must always be aggregated centrally within an organization.

Security Reader permissions can be applied broadly to an entire tenant (Root Management Group) or scoped to management groups or specific subscriptions.

Note: Additional permissions might be required to get visibility into workloads and services.

- [Overview of Security Reader Role](https://docs.microsoft.com/azure/role-based-access-control/built-in-roles#security-reader)

- [Overview of Azure Management Groups](https://docs.microsoft.com/azure/governance/management-groups/overview)

**Azure Security Center monitoring**: Currently not available

**Responsibility**: Customer

### AM-2: Ensure security team has access to asset inventory and metadata

**Guidance**: Apply tags to your Azure resources, resource groups, and subscriptions to logically organize them into a taxonomy. Each tag consists of a name and a value pair. For example, you can apply the name "Environment" and the value "Production" to all the resources in production.

Azure Bot Service does not offer support for Azure Resource Manager-based resource deployments and discovery with Azure Resource Graph.

- [How to create queries with Azure Resource Graph Explorer](https://docs.microsoft.com/azure/governance/resource-graph/first-query-portal)

- [Azure Security Center asset inventory management](https://docs.microsoft.com/azure/security-center/asset-inventory)

- [For more information about tagging assets, see the resource naming and tagging decision guide](https://docs.microsoft.com/azure/cloud-adoption-framework/decision-guides/resource-tagging/?toc=/azure/azure-resource-manager/management/toc.json)

**Azure Security Center monitoring**: Not applicable

**Responsibility**: Customer

### AM-3: Use only approved Azure services

**Guidance**: Use Azure Policy to audit and restrict which services users can provision in your environment. Use Azure Resource Graph to query for and discover resources within their subscriptions. You can also use Azure Monitor to create rules to trigger alerts when a non-approved service is detected.

- [How to configure and manage Azure Policy](https://docs.microsoft.com/azure/governance/policy/tutorials/create-and-manage)

- [How to deny a specific resource type with Azure Policy](https://docs.microsoft.com/azure/governance/policy/samples/built-in-policies#general)

- [How to create queries with Azure Resource Graph Explorer](https://docs.microsoft.com/azure/governance/resource-graph/first-query-portal)

**Azure Security Center monitoring**: Not applicable

**Responsibility**: Customer

### AM-4: Ensure security of asset lifecycle management

**Guidance**: Not applicable. The Azure Bot Service cannot be used for ensuring security of assets in a lifecycle management process. It is the customer's responsibility to maintain attributes and network configurations of assets which are considered high-impact. It is recommended that the customer create a process to capture the attribute and network-configuration changes, measure the change-impact and create remediation tasks, as applicable.

**Azure Security Center monitoring**: Not applicable

**Responsibility**: Customer

### AM-5: Limit users' ability to interact with Azure Resource Manager

**Guidance**: Use Azure AD Conditional Access to limit users' ability to interact with Azure Resource Manager by configuring "Block access" for the "Microsoft Azure Management" App.

- [How to configure Conditional Access to block access to Azure Resources Manager](https://docs.microsoft.com/azure/role-based-access-control/conditional-access-azure-management)

**Azure Security Center monitoring**: Not applicable

**Responsibility**: Customer

## Logging and Threat Detection

*For more information, see the [Azure Security Benchmark: Logging and Threat Detection](/azure/security/benchmarks/security-controls-v2-logging-threat-protection).*

### LT-1: Enable threat detection for Azure resources

**Guidance**: Azure Bot Service does not provide native capabilities to monitor security threats related to its resources.

Forward any logs from Azure Bot Service to your SIEM which can be used to set up custom threat detections. Ensure you are monitoring different types of Azure assets for potential threats and anomalies. Focus on getting high-quality alerts to reduce false positives for analysts to sort through. Alerts can be sourced from log data, agents, or other data.

- [Create custom analytics rules to detect threats](https://docs.microsoft.com/azure/sentinel/tutorial-detect-threats-custom)

- [Cyber threat intelligence with Azure Sentinel](https://docs.microsoft.com/azure/architecture/example-scenario/data/sentinel-threat-intelligence)

**Azure Security Center monitoring**: Yes

**Responsibility**: Customer

### LT-2: Enable threat detection for Azure identity and access management

**Guidance**: Azure AD provides the following user logs that can be viewed in Azure AD reporting or integrated with Azure Monitor, Azure Sentinel or other SIEM/monitoring tools for more sophisticated monitoring and analytics use cases:
-	Sign-ins – The sign-ins report provides information about the usage of managed applications and user sign-in activities.

-	Audit logs - Provides traceability through logs for all changes done by various features within Azure AD. Examples of audit logs include changes made to any resources within Azure AD like adding or removing users, apps, groups, roles and policies.

-	Risky sign-ins - A risky sign-in is an indicator for a sign-in attempt that might have been performed by someone who is not the legitimate owner of a user account.

-	Users flagged for risk - A risky user is an indicator for a user account that might have been compromised.

Azure Security Center can also alert on certain suspicious activities such as an excessive number of failed authentication attempts, and deprecated accounts in the subscription. In addition to the basic security hygiene monitoring, Azure Security Center’s Threat Protection module can also collect more in-depth security alerts from individual Azure compute resources (such as virtual machines, containers, app service), data resources (such as SQL DB and storage), and Azure service layers. This capability allows you to see account anomalies inside the individual resources.

- [Audit activity reports in Azure AD](https://docs.microsoft.com/azure/active-directory/reports-monitoring/concept-audit-logs)

- [Enable Azure Identity Protection](https://docs.microsoft.com/azure/active-directory/identity-protection/overview-identity-protection)

- [Threat protection in Azure Security Center](https://docs.microsoft.com/azure/security-center/threat-protection)

**Azure Security Center monitoring**: Yes

**Responsibility**: Customer

### LT-3: Enable logging for Azure network activities

**Guidance**: Enable and collect network security group (NSG) resource logs, NSG flow logs, Azure Firewall logs, and Web Application Firewall (WAF) logs for security analysis to support incident investigations, threat hunting, and security alert generation. You can send the flow logs to an Azure Monitor Log Analytics workspace and then use Traffic Analytics to provide insights.

Azure Bot Service does not produce or process DNS query logs which would need to be enabled.

- [How to enable network security group flow logs](https://docs.microsoft.com/azure/network-watcher/network-watcher-nsg-flow-logging-portal)

- [How to enable network security group flow logs](https://docs.microsoft.com/azure/network-watcher/network-watcher-nsg-flow-logging-portal)

- [Azure Firewall logs and metrics](https://docs.microsoft.com/azure/firewall/logs-and-metrics)

- [How to enable and use Traffic Analytics](https://docs.microsoft.com/azure/network-watcher/traffic-analytics)

- [Monitoring with Network Watcher](https://docs.microsoft.com/azure/network-watcher/network-watcher-monitoring-overview)

- [Azure networking monitoring solutions in Azure Monitor](https://docs.microsoft.com/azure/azure-monitor/insights/azure-networking-analytics)

**Azure Security Center monitoring**: Not applicable

**Responsibility**: Customer

### LT-4: Enable logging for Azure resources

**Guidance**: Activity logs, which are automatically available, contain all write operations (PUT, POST, DELETE) for your Azure Bot Services resources except read operations (GET). Activity logs can be used to find an error when troubleshooting or to monitor how a user in your organization modified a resource.

Azure Bot Service currently does not produce Azure resource logs.

- [How to collect platform logs and metrics with Azure Monitor](https://docs.microsoft.com/azure/azure-monitor/platform/diagnostic-settings)

- [Understand logging and different log types in Azure](https://docs.microsoft.com/azure/azure-monitor/platform/platform-logs-overview)

**Azure Security Center monitoring**: Not applicable

**Responsibility**: Customer

### LT-5: Centralize security log management and analysis

**Guidance**: Centralize logging storage and analysis to enable correlation. For each log source, ensure you have assigned a data owner, access guidance, storage location, what tools are used to process and access the data, and data retention requirements.

Ensure you are integrating Azure activity logs into your central logging. Ingest logs via Azure Monitor to aggregate security data generated by endpoint devices, network resources, and other security systems. In Azure Monitor, use Log Analytics workspaces to query and perform analytics, and use Azure Storage accounts for long term and archival storage.

In addition, enable and onboard data to Azure Sentinel or a third-party SIEM.

Many organizations choose to use Azure Sentinel for "hot" data that is used frequently and Azure Storage for "cold" data that is used less frequently.

- [How to collect platform logs and metrics with Azure Monitor](https://docs.microsoft.com/azure/azure-monitor/platform/diagnostic-settings)

- [How to onboard Azure Sentinel](https://docs.microsoft.com/azure/sentinel/quickstart-onboard)

**Azure Security Center monitoring**: Yes

**Responsibility**: Customer

### LT-6: Configure log storage retention

**Guidance**: Ensure that any storage accounts or Log Analytics workspaces used for storing Azure Bot Service logs has the log retention period set according to your organization's compliance regulations.

In Azure Monitor, you can set your Log Analytics workspace retention period according to your organization's compliance regulations. Use Azure Storage, Data Lake or Log Analytics workspace accounts for long-term and archival storage.

- [How to configure Log Analytics Workspace Retention Period](https://docs.microsoft.com/azure/azure-monitor/platform/manage-cost-storage)

- [Storing resource logs in an Azure Storage Account](https://docs.microsoft.com/azure/azure-monitor/platform/resource-logs-collect-storage)

**Azure Security Center monitoring**: Not applicable

**Responsibility**: Customer

## Incident Response

*For more information, see the [Azure Security Benchmark: Incident Response](/azure/security/benchmarks/security-controls-v2-incident-response).*

### IR-1: Preparation – update incident response process for Azure

**Guidance**: Ensure your organization has processes to respond to security incidents, has updated these processes for Azure, and is regularly exercising them to ensure readiness.

- [Implement security across the enterprise environment](https://docs.microsoft.com/azure/cloud-adoption-framework/security/security-top-10#4-process-update-incident-response-ir-processes-for-cloud)

- [Incident response reference guide](https://docs.microsoft.com/microsoft-365/downloads/IR-Reference-Guide.pdf)

**Azure Security Center monitoring**: Not applicable

**Responsibility**: Customer

### IR-2: Preparation – setup incident notification

**Guidance**: Set up security incident contact information in Azure Security Center. This contact information is used by Microsoft to contact you if the Microsoft Security Response Center (MSRC) discovers that your data has been accessed by an unlawful or unauthorized party. You also have options to customize incident alert and notification in different Azure services based on your incident response needs.

- [How to set the Azure Security Center security contact](https://docs.microsoft.com/azure/security-center/security-center-provide-security-contact-details)

**Azure Security Center monitoring**: Yes

**Responsibility**: Customer

### IR-3: Detection and analysis – create incidents based on high quality alerts

**Guidance**: Ensure you have a process to create high-quality alerts and measure the quality of alerts. This allows you to learn lessons from past incidents and prioritize alerts for analysts, so they don’t waste time on false positives.

High-quality alerts can be built based on experience from past incidents, validated community sources, and tools designed to generate and clean up alerts by fusing and correlating diverse signal sources.

Azure Security Center provides high-quality alerts across many Azure assets. You can use the ASC data connector to stream the alerts to Azure Sentinel. Azure Sentinel lets you create advanced alert rules to generate incidents automatically for an investigation.

Export your Azure Security Center alerts and recommendations using the export feature to help identify risks to Azure resources. Export alerts and recommendations either manually or in an ongoing, continuous fashion.

- [How to configure export](https://docs.microsoft.com/azure/security-center/continuous-export)

- [How to stream alerts into Azure Sentinel](https://docs.microsoft.com/azure/sentinel/connect-azure-security-center)

**Azure Security Center monitoring**: Currently not available

**Responsibility**: Customer

### IR-4: Detection and analysis – investigate an incident

**Guidance**: Ensure analysts can query and use diverse data sources as they investigate potential incidents, to build a full view of what happened. Diverse logs should be collected to track the activities of a potential attacker across the kill chain to avoid blind spots.  You should also ensure insights and learnings are captured for other analysts and for future historical reference.

The data sources for investigation include the centralized logging sources that are already being collected from the in-scope services and running systems, but can also include:

- Network data – use network security groups' flow logs, Azure Network Watcher, and Azure Monitor to capture network flow logs and other analytics information.

- Snapshots of running systems:

    - Use Azure virtual machine's snapshot capability to create a snapshot of the running system's disk.

    - Use the operating system's native memory dump capability to create a snapshot of the running system's memory.

    - Use the snapshot feature of the Azure services or your software's own capability to create snapshots of the running systems.

Azure Sentinel provides extensive data analytics across virtually any log source and a case management portal to manage the full lifecycle of incidents. Intelligence information during an investigation can be associated with an incident for tracking and reporting purposes.

- [Snapshot a Windows machine's disk](https://docs.microsoft.com/azure/virtual-machines/windows/snapshot-copy-managed-disk)

- [Snapshot a Linux machine's disk](https://docs.microsoft.com/azure/virtual-machines/linux/snapshot-copy-managed-disk)

- [Microsoft Azure Support diagnostic information and memory dump collection](https://azure.microsoft.com/support/legal/support-diagnostic-information-collection/)

- [Investigate incidents with Azure Sentinel](https://docs.microsoft.com/azure/sentinel/tutorial-investigate-cases)

**Azure Security Center monitoring**: Not applicable

**Responsibility**: Customer

### IR-5: Detection and analysis – prioritize incidents

**Guidance**: Provide context to analysts on which incidents to focus on first based on alert severity and asset sensitivity.

Azure Security Center assigns a severity to each alert to help you prioritize which alerts should be investigated first. The severity is based on how confident Security Center is in the finding or the analytically used to issue the alert, as well as the confidence level that there was malicious intent behind the activity that led to the alert.

Additionally, mark resources using tags and create a naming system to identify and categorize Azure resources, especially those processing sensitive data.  It is your responsibility to prioritize the remediation of alerts based on the criticality of the Azure resources and environment where the incident occurred.

- [Security alerts in Azure Security Center](https://docs.microsoft.com/azure/security-center/security-center-alerts-overview)

- [Use tags to organize your Azure resources](https://docs.microsoft.com/azure/azure-resource-manager/resource-group-using-tags)

**Azure Security Center monitoring**: Currently not available

**Responsibility**: Customer

### IR-6: Containment, eradication and recovery – automate the incident handling

**Guidance**: Automate manual repetitive tasks to speed up response time and reduce the burden on analysts. Manual tasks take longer to execute, slowing each incident and reducing how many incidents an analyst can handle. Manual tasks also increase analyst fatigue, which increases the risk of human error that causes delays, and degrades the ability of analysts to focus effectively on complex tasks.
Use workflow automation features in Azure Security Center and Azure Sentinel to automatically trigger actions or run a playbook to respond to incoming security alerts. The playbook takes actions, such as sending notifications, disabling accounts, and isolating problematic networks.

- [Configure workflow automation in Security Center](https://docs.microsoft.com/azure/security-center/workflow-automation)

- [Set up automated threat responses in Azure Security Center](https://docs.microsoft.com/azure/security-center/tutorial-security-incident#triage-security-alerts)

- [Set up automated threat responses in Azure Sentinel](https://docs.microsoft.com/azure/sentinel/tutorial-respond-threats-playbook)

**Azure Security Center monitoring**: Currently not available

**Responsibility**: Customer

## Posture and Vulnerability Management

*For more information, see the [Azure Security Benchmark: Posture and Vulnerability Management](/azure/security/benchmarks/security-controls-v2-vulnerability-management).*

### PV-8: Conduct regular attack simulation

**Guidance**: As required, conduct penetration testing or red team activities on your Azure resources and ensure remediation of all critical security findings.
Follow the Microsoft Cloud Penetration Testing Rules of Engagement to ensure your penetration tests are not in violation of Microsoft policies. Use Microsoft's strategy and execution of Red Teaming and live site penetration testing against Microsoft-managed cloud infrastructure, services, and applications.

- [Penetration testing in Azure](https://docs.microsoft.com/azure/security/fundamentals/pen-testing)

- [Penetration Testing Rules of Engagement](https://www.microsoft.com/msrc/pentest-rules-of-engagement?rtc=1)

- [Microsoft Cloud Red Teaming](https://gallery.technet.microsoft.com/Cloud-Red-Teaming-b837392e)

**Azure Security Center monitoring**: Not applicable

**Responsibility**: Shared

## Backup and Recovery

*For more information, see the [Azure Security Benchmark: Backup and Recovery](/azure/security/benchmarks/security-controls-v2-backup-recovery).*

### BR-1: Ensure regular automated backups

**Guidance**: Ensure you are backing up systems and data to maintain business continuity after an unexpected event. This should be defined by any objectives for Recovery Point Objective (RPO) and Recovery Time Objective (RTO).

Enable Azure Backup and configure the backup source (e.g. Azure VMs, SQL Server, HANA databases, or File Shares), as well as the desired frequency and retention period.

For a higher level of protection, you can enable geo-redundant storage option to replicate backup data to a secondary region and recover using cross region restore.

- [Enterprise-scale business continuity and disaster recovery](https://docs.microsoft.com/azure/cloud-adoption-framework/ready/enterprise-scale/business-continuity-and-disaster-recovery)

- [How to enable Azure Backup](https://docs.microsoft.com/azure/backup/)

- [How to enable cross region restore](https://docs.microsoft.com/azure/backup/backup-azure-arm-restore-vms#cross-region-restore)

**Azure Security Center monitoring**: Not applicable

**Responsibility**: Customer

### BR-2: Encrypt backup data

**Guidance**: Ensure your backups are protected against attacks. This should include encryption of the backups to protect against loss of confidentiality.

For on-premises backups using Azure Backup, encryption-at-rest is provided using the passphrase you provide. For regular Azure service backups, backup data is automatically encrypted using Azure platform-managed keys. You can choose to encrypt the backups using customer-managed key. In this case, ensure this customer-managed key in the key vault is also in the backup scope.

Use role-based access control in Azure Backup, Azure Key Vault, or other resources to protect backups and customer-managed keys. Additionally, you can enable advanced security features to require MFA before backups can be altered or deleted.

- [Overview of security features in Azure Backup](https://docs.microsoft.com/azure/backup/security-overview)

- [Encryption of backup data using customer-managed keys](https://docs.microsoft.com/azure/backup/encryption-at-rest-with-cmk)

- [How to backup Key Vault keys in Azure](https://docs.microsoft.com/powershell/module/azurerm.keyvault/backup-azurekeyvaultkey?view=azurermps-6.13.0)

- [Security features to help protect hybrid backups from attacks](https://docs.microsoft.com/azure/backup/backup-azure-security-feature#prevent-attacks)

**Azure Security Center monitoring**: Not applicable

**Responsibility**: Customer

### BR-3: Validate all backups including customer-managed keys

**Guidance**: Periodically ensure that you can restore backed-up customer-managed keys.

- [How to restore Key Vault keys in Azure](https://docs.microsoft.com/powershell/module/azurerm.keyvault/restore-azurekeyvaultkey?view=azurermps-6.13.0)

**Azure Security Center monitoring**: Not applicable

**Responsibility**: Customer

### BR-4: Mitigate risk of lost keys

**Guidance**: Ensure you have measures in place to prevent and recover from loss of keys. Enable soft delete and purge protection in Azure Key Vault to protect keys against accidental or malicious deletion.

- [How to enable soft delete and purge protection in Key Vault](https://docs.microsoft.com/azure/storage/blobs/storage-blob-soft-delete?tabs=azure-portal)

**Azure Security Center monitoring**: Not applicable

**Responsibility**: Customer

## Governance and Strategy

*For more information, see the [Azure Security Benchmark: Governance and Strategy](/azure/security/benchmarks/security-controls-v2-governance-strategy).*

### GS-1: Define asset management and data protection strategy

**Guidance**: Ensure you document and communicate a clear strategy for continuous monitoring and protection of systems and data. Prioritize discovery, assessment, protection, and monitoring of business-critical data and systems.

This strategy should include documented guidance, policy, and standards for the following elements:

-	Data classification standard in accordance with the business risks

-	Security organization visibility into risks and asset inventory

-	Security organization approval of Azure services for use

-	Security of assets through their lifecycle

-	Required access control strategy in accordance with organizational data classification

-	Use of Azure native and third-party data protection capabilities

-	Data encryption requirements for in-transit and at-rest use cases

-	Appropriate cryptographic standards

For more information, see the following references:
- [Azure Security Architecture Recommendation - Storage, data, and encryption](https://docs.microsoft.com/azure/architecture/framework/security/storage-data-encryption?toc=/security/compass/toc.json&amp;bc=/security/compass/breadcrumb/toc.json)

- [Azure Security Fundamentals - Azure Data security, encryption, and storage](https://docs.microsoft.com/azure/security/fundamentals/encryption-overview)

- [Cloud Adoption Framework - Azure data security and encryption best practices](https://docs.microsoft.com/azure/security/fundamentals/data-encryption-best-practices?toc=/azure/cloud-adoption-framework/toc.json&amp;bc=/azure/cloud-adoption-framework/_bread/toc.json)

- [Azure Security Benchmark - Asset management](https://docs.microsoft.com/azure/security/benchmarks/security-benchmark-v2-asset-management)

- [Azure Security Benchmark - Data Protection](https://docs.microsoft.com/azure/security/benchmarks/security-benchmark-v2-data-protection)

**Azure Security Center monitoring**: Not applicable

**Responsibility**: Customer

### GS-2: Define enterprise segmentation strategy

**Guidance**: Establish an enterprise-wide strategy to segmenting access to assets using a combination of identity, network, application, subscription, management group, and other controls.

Carefully balance the need for security separation with the need to enable daily operation of the systems that need to communicate with each other and access data.

Ensure that the segmentation strategy is implemented consistently across control types including network security, identity and access models, and application permission/access models, and human process controls.

- [Guidance on segmentation strategy in Azure (video)](https://docs.microsoft.com/security/compass/microsoft-security-compass-introduction#azure-components-and-reference-model-2151)

- [Guidance on segmentation strategy in Azure (document)](https://docs.microsoft.com/security/compass/governance#enterprise-segmentation-strategy)

- [Align network segmentation with enterprise segmentation strategy](https://docs.microsoft.com/security/compass/network-security-containment#align-network-segmentation-with-enterprise-segmentation-strategy)

**Azure Security Center monitoring**: Not applicable

**Responsibility**: Customer

### GS-3: Define security posture management strategy

**Guidance**: Continuously measure and mitigate risks to your individual assets and the environment they are hosted in. Prioritize high value assets and highly-exposed attack surfaces, such as published applications, network ingress and egress points, user and administrator endpoints, etc.

- [Azure Security Benchmark - Posture and vulnerability management](https://docs.microsoft.com/azure/security/benchmarks/security-benchmark-v2-posture-vulnerability-management)

**Azure Security Center monitoring**: Not applicable

**Responsibility**: Customer

### GS-4: Align organization roles, responsibilities, and accountabilities

**Guidance**: Ensure you document and communicate a clear strategy for roles and responsibilities in your security organization. Prioritize providing clear accountability for security decisions, educating everyone on the shared responsibility model, and educate technical teams on technology to secure the cloud.

- [Azure Security Best Practice 1 – People: Educate Teams on Cloud Security Journey](https://docs.microsoft.com/azure/cloud-adoption-framework/security/security-top-10#1-people-educate-teams-about-the-cloud-security-journey)

- [Azure Security Best Practice 2 - People: Educate Teams on Cloud Security Technology](https://docs.microsoft.com/azure/cloud-adoption-framework/security/security-top-10#2-people-educate-teams-on-cloud-security-technology)

- [Azure Security Best Practice 3 - Process: Assign Accountability for Cloud Security Decisions](https://docs.microsoft.com/azure/cloud-adoption-framework/security/security-top-10#4-process-update-incident-response-ir-processes-for-cloud)

**Azure Security Center monitoring**: Not applicable

**Responsibility**: Customer

### GS-5: Define network security strategy

**Guidance**: Establish an Azure network security approach as part of your organization’s overall security access control strategy.

This strategy should include documented guidance, policy, and standards for the following elements:

-	Centralized network management and security responsibility

-	Virtual network segmentation model aligned with the enterprise segmentation strategy

-	Remediation strategy in different threat and attack scenarios

-	Internet edge and ingress and egress strategy

-	Hybrid cloud and on-premises interconnectivity strategy

-	Up-to-date network security artifacts (e.g. network diagrams, reference network architecture)

For more information, see the following references:
- [Azure Security Best Practice 11 - Architecture. Single unified security strategy](https://docs.microsoft.com/azure/cloud-adoption-framework/security/security-top-10#11-architecture-establish-a-single-unified-security-strategy)

- [Azure Security Benchmark - Network Security](https://docs.microsoft.com/azure/security/benchmarks/security-benchmark-v2-network-security)

- [Azure network security overview](https://docs.microsoft.com/azure/security/fundamentals/network-overview)

- [Enterprise network architecture strategy](https://docs.microsoft.com/azure/cloud-adoption-framework/ready/enterprise-scale/architecture)

**Azure Security Center monitoring**: Not applicable

**Responsibility**: Customer

### GS-6: Define identity and privileged access strategy

**Guidance**: Establish an Azure identity and privileged access approaches as part of your organization’s overall security access control strategy.

This strategy should include documented guidance, policy, and standards for the following elements:

-	A centralized identity and authentication system and its interconnectivity with other internal and external identity systems

-	Strong authentication methods in different use cases and conditions

-	Protection of highly privileged users

-	Anomaly user activities monitoring and handling

-	User identity and access review and reconciliation process

For more information, see the following references:

- [Azure Security Benchmark - Identity management](https://docs.microsoft.com/azure/security/benchmarks/security-benchmark-v2-identity-management)

- [Azure Security Benchmark - Privileged access](https://docs.microsoft.com/azure/security/benchmarks/security-benchmark-v2-privileged-access)

- [Azure Security Best Practice 11 - Architecture. Single unified security strategy](https://docs.microsoft.com/azure/cloud-adoption-framework/security/security-top-10#11-architecture-establish-a-single-unified-security-strategy)

- [Azure identity management security overview](https://docs.microsoft.com/azure/security/fundamentals/identity-management-overview)

**Azure Security Center monitoring**: Not applicable

**Responsibility**: Customer

### GS-7: Define logging and threat response strategy

**Guidance**: Establish a logging and threat response strategy to rapidly detect and remediate threats while meeting compliance requirements. Prioritize providing analysts with high-quality alerts and seamless experiences so that they can focus on threats rather than integration and manual steps.

This strategy should include documented guidance, policy, and standards for the following elements:

-	The security operations (SecOps) organization’s role and responsibilities

-	A well-defined incident response process aligning with NIST or another industry framework

-	Log capture and retention to support threat detection, incident response, and compliance needs

-	Centralized visibility of and correlation information about threats, using SIEM, native Azure capabilities, and other sources

-	Communication and notification plan with your customers, suppliers, and public parties of interest

-	Use of Azure native and third-party platforms for incident handling, such as logging and threat detection, forensics, and attack remediation and eradication

-	Processes for handling incidents and post-incident activities, such as lessons learned and evidence retention

For more information, see the following references:

- [Azure Security Benchmark - Logging and threat detection](https://docs.microsoft.com/azure/security/benchmarks/security-benchmark-v2-logging-threat-detection)

- [Azure Security Benchmark - Incident response](https://docs.microsoft.com/azure/security/benchmarks/security-benchmark-v2-incident-response)

- [Azure Security Best Practice 4 - Process. Update Incident Response Processes for Cloud](https://docs.microsoft.com/azure/cloud-adoption-framework/security/security-top-10#4-process-update-incident-response-ir-processes-for-cloud)

- [Azure Adoption Framework, logging, and reporting decision guide](https://docs.microsoft.com/azure/cloud-adoption-framework/decision-guides/logging-and-reporting/)

- [Azure enterprise scale, management, and monitoring](https://docs.microsoft.com/azure/cloud-adoption-framework/ready/enterprise-scale/management-and-monitoring)

**Azure Security Center monitoring**: Not applicable

**Responsibility**: Customer

## Next steps

- See the [Azure Security Benchmark V2 overview](/azure/security/benchmarks/overview)
- Learn more about [Azure security baselines](/azure/security/benchmarks/security-baselines-overview)
