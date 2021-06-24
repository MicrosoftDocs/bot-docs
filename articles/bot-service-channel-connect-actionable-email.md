---
title: Connect to Outlook for Actionable Messages - Bot Service
description: Learn how to configure bots to send and receive actionable email messages by using Adaptive Cards to power your Outlook Actionable Messages.
author: JonathanFingold
ms.author: kamrani
ms.service: bot-service
ms.topic: how-to
ms.date: 06/09/2021
ms.custom: template-how-to
---

# Connect a bot to the Outlook channel for Actionable Messages (Preview)

[!INCLUDE [applies-to-v4](includes/applies-to-v4-current.md)]

Actionable Messages allow users to take quick actions from within Outlook. When you connect a bot to the Outlook Actionable Messages channel, your bot can use Adaptive Cards to create actionable messages, enhancing user engagement.

> [!NOTE]
> The Outlook channel for Actionable Messages is in public preview. Features included in preview might not be complete, and might undergo changes before becoming available in the public release. They're provided for evaluation and exploration purposes only.

> [!WARNING]
> It's a violation of the Bot Framework [Code of Conduct](https://www.botframework.com/Content/Developer-Code-of-Conduct-for-Microsoft-Bot-Framework.htm) to create "spambots", including bots that send unwanted or unsolicited bulk email.

## Prerequisites

- Knowledge of [Adaptive Cards](/adaptive-cards) and the [Universal Action Model](/adaptive-cards/authoring-cards/universal-action-model).
- Familiarity with [Actionable messages in Outlook and Office 365 Groups](/outlook/actionable-messages/).
- A channel-ready bot deployed to Azure.
- A dedicated email address that your bot will use to send and receive messages.

> [!NOTE]
>
> - Don't use your own personal email accounts for bots, as every message sent to that email account will be forwarded to the bot. This can result in the bot inappropriately sending a response to a sender. For this reason, bots should only use dedicated O365 email accounts.
> - If you use Microsoft Exchange Server, enable [Autodiscover](/exchange/client-developer/exchange-web-services/autodiscover-for-exchange) before configuring the email channel.

## Design the messages

The Outlook channel for Actionable Messages sends and receives Adaptive Cards over email.

You can use the [Actionable Message Designer](https://amdesigner.azurewebsites.net/) to design and test actionable message cards.

> [!IMPORTANT]
> The universal Bot action model is introduced in the Adaptive Cards schema version 1.4. To use these new capabilities, the version property of your Adaptive Card should be set to 1.4 or later. See [Action.Execute](https://adaptivecards.io/explorer/Action.Execute.html) in the Adaptive Cards Schema Explorer.

## Implement and deploy the bot

The general flow is for the bot to send an actionable message to a user and then handle the user's action.

1. When the user responds to the card in email, Outlook sends an invoke activity to the bot.
    The activity's `value` property contains an action object with `type`, `verb`, and `data` properties. For example, the activity would include the following information, where the _verb_ and the _data_ correspond to information on the card the bot sent out initially.

    ```json
    "type": "invoke",
    "name": "adaptiveCard/action",
    "value": {
      "action": {
        "type": "Action.Execute",
        "verb": "<DEVELOPER_DEFINED_VERB>",
        "data": {
          // DEVELOPER_DEFINED_PROPERTIES
        }
      },
    }
    ```

    See [Action.Execute](https://adaptivecards.io/explorer/Action.Execute.html) in the Adaptive Cards Schema Explorer for the complete object schema.

1. The bot handles the incoming invoke activity and returns a result that includes a new Adaptive Card that will take the place of the original Adaptive Card. The invoke response might look like:

    ```json
    {
      "statusCode": 200,
      "type": "application/vnd.microsoft.card.adaptive",
      "value": <UPDATED_ADAPTIVE_CARD>
    }
    ```

## Request access

1. Open your bot resource in the [Azure portal](https://ms.portal.azure.com/).
1. Open the **Channels** pane.
1. Select the **Outlook** channel.
1. On the **Configure Outlook** page, select **please register here**.
1. Fill out the registration form to request access. See [Register your service with the actionable email developer dashboard](/outlook/actionable-messages/email-dev-dashboard) for more information.

## Next steps

- Learn more about [Actionable messages in Outlook and Office 365 Groups](/outlook/actionable-messages/).
- Learn more about [Adaptive Cards for Outlook Actionable Message Developers](/adaptive-cards/getting-started/outlook)
