---
title: Manage your MSA App iD for Azure Bot Service | Microsoft Docs
description: Learn how to manage your MSA App ID for your bot using Azure Bot Service.
keywords: Bot Framework, Azure Bot Service, App ID, password, MSA
author: RobStand
manager: rstand
ms.topic: bot-service-article
ms.prod: botframework
ms.service: Azure Bot Service
ms.date: 3/21/2017
ms.reviewer:
#ROBOTS: Index
---

# Manage your MSA App ID and password

Azure Bot Service is powered by Microsoft Bot Framework, which requires an app ID and password to work. If you create a bot by using the Azure Bot Service templates, you will be asked to generate an MSA app ID and password during the creation flow (see Creating your first bot). The MSA app ID and password are saved to the following application settings keys:
- MicrosoftAppId
- MicrosoftAppPassword
There may be cases when you will need to generate a new password (for example, if you think your secret has been compromised). The easiest way to do it, is to use the Bot Framework Developer portal. The following shows how to do that.

##Create a new password and updating your bot configuration
To create a new password, follow these steps:
1. Go to the Bot Framework Developer portal, find your bot under My bots, and open it.
2. Click **Edit** in the **Details** panel. ![msa password update devportal dashboard](media/msa-password-update-devportal-dashboard.png) 
3. Click **Manage Microsoft App ID and password**.  ![msa password update devportal edit](media/msa-password-update-devportal-edit.png)  4. If prompted, login with the Microsoft account you used to create the MSA app ID, then click **Generate New Password** on the next page.  ![msa password update msa createnew](media/msa-password-update-msa-createnew.png) 5. Copy the password.  ![msa password update msa pwdcreated](media/msa-password-update-msa-pwdcreated.png)
>[!NOTE]
>This is the only time you'll see the password; copy it and store it securely.

Paste the password in the bot application settings in Azure Bot Service.  ![msa password update portal](media/msa-password-update-portal.png)  
If you already have two passwords, you will need to delete one of them. Delete the unused password and generate a new one following the previous steps. ![msa-password-update-msa-pwddelete.png](media/msa-password-update-msa-pwddelete.png)

>[!NOTE]
>If you delete the password currently configured in your bot, the bot will stop working. Make sure you delete the unused one.