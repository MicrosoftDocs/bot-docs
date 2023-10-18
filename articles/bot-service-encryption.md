---
title: Azure AI Bot Service encryption for data at rest in Bot Framework SDK
description: Azure AI Bot Service protects your data by automatically encrypting it before persisting it to the cloud with Microsoft provided encryption keys.
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: jameslew
ms.service: bot-service
ms.topic: conceptual
ms.date: 06/06/2022
---

# Azure AI Bot Service encryption for data at rest

[!INCLUDE [applies-to-v4](includes/applies-to-v4-current.md)]

Azure AI Bot Service automatically encrypts your data when it's persisted to the cloud to protect the data and to meet organizational security and compliance commitments.

Encryption and decryption are transparent, meaning encryption and access are managed for you. Your data is secure by default and you don't need to modify your code or applications to take advantage of encryption.

## About encryption key management

By default, your subscription uses Microsoft-managed encryption keys. You can manage your bot resource with your own keys called customer-managed keys. Customer-managed keys offer greater flexibility to create, rotate, disable, and revoke access controls to the data Azure AI Bot Service stores. You can also audit the encryption keys used to protect your data.

When encrypting data, Azure AI Bot Service encrypts with two levels of encryption. In the case where customer-managed keys aren't enabled, both keys used are Microsoft-managed keys. When customer-managed keys are enabled, the data is encrypted with both the customer-managed key and a Microsoft-managed key.

## Customer-managed keys with Azure Key Vault

To utilize the customer-managed keys feature, you must store and manage keys in **Azure Key Vault**. You can either create your own keys and store them in a key vault, or you can use the Azure Key Vault APIs to generate keys. Your Azure Bot resource and the key vault must be in the same Entra ID tenant, but they can be in different subscriptions. For more information about Azure Key Vault, see [What is Azure Key Vault?](/azure/key-vault/key-vault-overview).

When using a customer-managed key, Azure AI Bot Service encrypts your data in its storage. If access to that key is revoked or the key is deleted, your bot won't be able to use Azure AI Bot Service to send or receive messages, and you won't be able to access or edit the configuration of your bot in the Azure portal.

When you create an Azure Bot resource via the portal, Azure generates an _app ID_ and a _password_, but doesn't store them in Azure Key Vault.
You can use Key Vault with Azure AI Bot Service. For information, see [Configure the web app to connect to Key Vault](/azure/key-vault/general/tutorial-net-create-vault-azure-web-app#configure-the-web-app-to-connect-to-key-vault). For an example on how to store and retrieve secrets with Key Vault, see [Quickstart: Azure Key Vault secret client library for .NET (SDK v4)](/azure/key-vault/secrets/quick-create-net).

> [!IMPORTANT]
> The Azure AI Bot Service team can't recover a customer-managed encryption key bot without access to the key.

## What data is encrypted?

Azure AI Bot Service stores customer data about the bot, the channels it uses, configuration settings the developer sets, and where necessary, a record of currently active conversations. It also transiently, for less than 24 hours, stores the messages sent over the Direct Line or Web Chat channels and any attachments uploaded.

All customer data is encrypted with two layers of encryption in Azure AI Bot Service; either with Microsoft managed encryption keys, or Microsoft and customer-managed encryption keys. Azure AI Bot Service encrypts transiently stored data using the Microsoft-managed encryption keys, and, depending on the configuration of the Azure Bot resource, encrypts longer-term data using either the Microsoft or customer-managed encryption keys.

> [!NOTE]
> As Azure AI Bot Service exists to provide customers the ability to deliver messages to and from users on other services outside Azure AI Bot Service, encryption doesn't extend to those services. This means that while under Azure AI Bot Service control, data will be stored encrypted as per the guidance in this article; however, when leaving the service to deliver to another service, the data is decrypted and then sent using TLS 1.2 encryption to the target service.

## How to configure your Azure Key Vault instance

Using customer-managed keys with Azure AI Bot Service requires that you enable two properties on the Azure Key Vault instance you plan to use to host your encryption keys: **Soft delete** and **Purge protection**. These features ensure that if for some reason your key is accidentally deleted, you can recover it. For more information about soft delete and purge protection, see the [Azure Key Vault soft-delete overview](/azure/key-vault/general/soft-delete-overview).

:::image type="content" source="media/key-vault/encryption-settings.png" alt-text="Screenshot of soft delete and purge protection enabled.":::

If you're using an existing Azure Key Vault instance, you can verify that these properties are enabled by looking at the **Properties** section on the Azure portal. If any of these properties aren't enabled, see the **Key Vault** section in [How to enable soft delete and purge protection](/azure/key-vault/general/key-vault-recovery).

### Grant Azure AI Bot Service access to a key vault

For Azure AI Bot Service to have access to the key vault you created for this purpose, an access policy needs to be set, which gives Azure AI Bot Service's service principal the current set of permissions. For more information about Azure Key Vault, including how to create a key vault, see [About Azure Key Vault](/azure/key-vault/general/overview).

1. Register the Azure AI Bot Service resource provider on your subscription containing the key vault.
    1. Go to the [Azure portal](https://ms.portal.azure.com).
    1. Open the **Subscriptions** blade and select the subscription that contains the key vault.
    1. Open the **Resource providers** blade and register the **Microsoft.BotService** resource provider.

    :::image type="content" source="media/key-vault/register-resource-provider.png" alt-text="Microsoft.BotService registered as a resource provider":::

1. Azure Key Vault supports two permission models: Azure role-based access control (RBAC) or vault access policy. You can choose to use either permission model. Ensure that the **Firewalls and virtual networks** in the **Networking** blade of the Key Vault is set to **Allow public access from all networks** at this step. Additionally, ensure that the operator has been granted the Key Management Operations permission.

    :::image type="content" source="media/key-vault/choose-permission-model.png" alt-text="Screenshot of the two permission models available for your key vault.":::

    1. To configure the Azure RBAC permission model on your key vault:
        1. Open the **Key vaults** blade and select your key vault.
        2. Go to the **Access control (IAM)** blade, and assign the **Key Vault Crypto Service Encryption User** role to **Bot Service CMEK Prod**. Only a user with the subscription owner role can make this change.

        :::image type="content" source="media/key-vault/key-vault-rbac.png" alt-text="Screenshot of key vault configuration showing the crypto service encryption user role has been added.":::

    1. To configure the Key Vault access policy permission model on your key vault:
        1. Open the **Key vaults** blade and select your key vault.
        1. Add the **Bot Service CMEK Prod** application as an access policy, and assign it the following permissions:
         - **Get** (from the **Key Management Operations**)
         - **Unwrap Key** (from the **Cryptographic Operations**)
         - **Wrap Key** (from the **Cryptographic Operations**)
        1. Select **Save** to save any changes you made.

        :::image type="content" source="media/key-vault/access-policies.png" alt-text="Bot Service CMEK Prod added as an access policy":::

1. Allow Key Vault to bypass your firewall.
    1. Open the **Key vaults** blade and select your key vault.
    1. Open the **Networking** blade and go to the **Firewalls and virtual networks** tab.
    1. If **Allow access from** is set to **Disable public access**, make sure **Allow trusted Microsoft services to bypass this firewall** is selected.
    1. Select **Save** to save any changes you made.

    :::image type="content" source="media/key-vault/firewall-exception.png" alt-text="Firewall exception added for Key Vault":::

### Enable customer-managed keys

To encrypt your bot with a customer-managed encryption key, follow these steps:

1. Open the Azure Bot resource blade for your bot.
1. Open the **Encryption** blade of your bot and select **Customer-Managed Keys** for the **Encryption type**.
1. Either input your key's complete URI, including version, or click **Select a key vault and a key** to find your key.
1. Click **Save** at the top of the blade.

    :::image type="content" source="media/key-vault/customer-managed-encryption.png" alt-text="Bot resource using customer-managed encryption":::

Once these steps are completed, Azure AI Bot Service will start the encryption process, which can take up to 24 hours to complete. Your bot remains functional during this time period.

### Rotate customer-managed keys

To rotate a customer-managed encryption key, you must update the Azure AI Bot Service resource to use the new URI for the new key (or new version of the existing key).

Because re-encryption with the new key occurs asynchronously, ensure the old key remains available so that data can continue to be decrypted; otherwise, your bot could stop working. You should retain the old key for at least one week.

### Revoke access to customer-managed keys

To revoke access, remove the access policy for the **Bot Service CMEK Prod** service principal from your key vault.

> [!NOTE]
> Revoking access will break most of the functionality associated with your bot. To disable the customer-managed keys feature, turn off the feature before revoking access to ensure the bot can continue working.

## Next steps

Learn more [About Azure Key Vault](/azure/key-vault/key-vault-overview)
