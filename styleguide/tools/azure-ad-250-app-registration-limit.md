
# Fix Entra ID 250 app registration limit

## Overview

If you create a lot of bots and Entra ID apps, you probably run into the limit imposed by Entra ID on app registrations that allows
to have **250 apps** at a given time. The problem is that when you delete an app using the Azure portal, it takes 30 days for the app to be fully deleted, which means that you cannot provision another one for 30 days. The approach described here takes **3-5 minutes** to complete.

> [!NOTE]
> Michael Richardson wrote up a great solution to do an effective deletion under 5 minutes, which Eric Dahlvang pointed to.

Again, Azure AI Bot Service requires an Entra ID app. There is a limit on 250 apps for our Azure accounts. At that point, you can get an error when provisioning new bots in using the Azure portal, and analogous errors through other bot creation means such as ARM SDK, ARM templates and others.

## Problem

The problem is that, if you go to the Azure portal and delete some Entra ID app registrations, they will take up to 7 days to be fully internally deleted and only then you will be able to create another bot.

The section below illustrates how to delete app registrations in such a way that the **deletion is reflected right away**.

## Solution

The following steps show how to **hard delete apps manually so that you can create new ones again.**:

> [NOTE]
> TODO: There are issues and questions with steps 4 and 5, Check with Eric and revise as necessary.

1. In your browser, navigate to [Azure portal][azure-portal].
2. Go to the app registration page.
3. Copy the **Object ID**.
4. If you don't have it or deleted it, get it as follows:
   <!-- These steps do not work -->
   1. In **Powershell** execute:
       1. `Install-Module -Name AzureAD`
       1. `Connect-AzureAD`
   1. `Get-AzureADUserCreatedObject -All $true -ObjectId <your @microsoft email>`. The ObjectId's appear on the right 
5. In your browser, navigate to [graph explorer][graph-explorer].
   1. In the left pane, toggle **Preview**.
   1. In the right pane, from the drop-down list, select the **DELETE** verb.  
   1. In the next drop-down list, select **Beta**.
   1. Enter (?) https://graph.microsoft.com/beta/directory/deletedItems/<ObjectId>.
   1. Then what??

   The status code **204** indicates success. There is no response data.

6. Alternatively to Step 5.2, you can use the undocumented `Remove-AzureADDeletedApplication -ObjectId <ObjectId>`  command.

7. Or, if you're feeling particularly daring, you can delete ALL of them at once do this:
   1. `$Objects = Get-AzureADUserCreatedObject -All $true -ObjectId <your @microsoft email>`
   1. `foreach ($Obj in $Objects) { Remove-AzureADDeletedApplication -ObjectId $Obj."ObjectId" }`

<!-- Foot links -->
[azure-portal]: https://ms.portal.azure.com/
[graph-explorer]: https://developer.microsoft.com/graph/graph-explorer
