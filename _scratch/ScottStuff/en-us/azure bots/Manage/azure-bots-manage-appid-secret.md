---
layout: page
title: Managing your MSA App ID and password
permalink: /en-us/azure-bot-service/manage/appid-password/
weight: 13200
parent1: Azure Bot Service
parent2: Manage
---

Azure Bot Service is powered by Microsoft Bot Framework, which requires an app ID and password to work. If you create a bot by using the Azure Bot Service templates, you will be asked to generate an MSA app ID and password during the creation flow (see [Creating your first bot](/en-us/azure-bots/build/first-bot/)). The MSA app ID and password are saved to the following application settings keys:

- MicrosoftAppId
- MicrosoftAppPassword

There may be cases when you will need to generate a new password (for example, if you think your secret has been compromised). The easiest way to do it, is to use the Bot Framework Developer portal. The following shows how to do that. 

### Creating a new password and updating your bot configuration
To create a new password, follow these simple steps:

1. Go to the [Bot Framework Developer portal](https://dev.botframework.com/){:target="_blank"}, find your bot under **My bots**, and open it.
2. Click **Edit** in the **Details** panel.
    [![Edit your bot](/en-us/images/azure-bots/msa-password-update-devportal-dashboard.png)](/en-us/images/azure-bots/msa-password-update-devportal-dashboard.png)
3. Click **Manage Microsoft App ID and password**. 
    [![Manage Microsoft App ID and password](/en-us/images/azure-bots/msa-password-update-devportal-edit.png)](/en-us/images/azure-bots/msa-password-update-devportal-edit.png)
4. If prompted, login with the Microsoft account you used to create the MSA app ID, then click **Generate New Password** on the next page.
    [![Generate New Password](/en-us/images/azure-bots/msa-password-update-msa-createnew.png)](/en-us/images/azure-bots/msa-password-update-msa-createnew.png)
5. Copy the password. 
    [![Copy the password](/en-us/images/azure-bots/msa-password-update-msa-pwdcreated.png)](/en-us/images/azure-bots/msa-password-update-msa-pwdcreated.png)

    <div class="docs-text-note"><strong>Note:</strong> This is the only time you'll see the password; copy it and store it securely.</div>

6. Paste the password in the bot application settings in Azure Bot Service.
    [![Paste the password in the bot application settings in the Azure Bot Service](/en-us/images/azure-bots/msa-password-update-portal.png)](/en-us/images/azure-bots/msa-password-update-portal.png)
7. If you already have two passwords, you will need to delete one of them. Delete the unused password and generate a new one following the previous steps.
    [![Delete a password](/en-us/images/azure-bots/msa-password-update-msa-pwddelete.png)](/en-us/images/azure-bots/msa-password-update-msa-pwddelete.png)

    <div class="docs-text-note"><strong>Note:</strong> If you delete the password currently configured in your bot, the bot will stop working. Make sure you delete the unused one.</div>

