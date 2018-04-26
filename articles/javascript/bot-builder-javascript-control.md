---
title: Create custom control | Microsoft Docs
description: Learn how to create custom control in the Bot Builder SDK for Node.js.
author: v-ducvo
ms.author: v-ducvo
manager: kamrani
ms.topic: article
ms.prod: bot-framework
ms.date: 3/1/2018
monikerRange: 'azure-bot-service-4.0'
---

# Create custom control
<!----
> > [!div class="op_single_selector"]
> - [.NET](../dotnet/bot-builder-dotnet-manage-conversation-flow.md)
> - [Node.js](../nodejs/bot-builder-nodejs-dialog-manage-conversation-flow.md)
---->

The Bot Builder SDK has a `control` class that is implemented from a `dialog` class. In turn, the `prompts` library is implemented as a `control`. This allows a prompt to be pushed onto the dialog stack and operate like a dialog. You can leverage the `control` class to create your own control classes in the same way that the `prompts` library did.

Features of your bot that are good candidates as a control may:

* have long list
* have multiple lists
* display in chunks
* require pagination

This topic will show you how to create a custom control that display different menus for a restaurant.

## Define a custom control class

To creat a custom control class, define the class and extends the `Control` class. Then, implement the constructor, the `dialogBegin` and `dialogContinue` function; add any additional helper functions the custom control may need. In this case, we only needed to add the `showMenu` helper function to show the menu that the user has chosen. For simplicity, the menu is created as an array of JSON objects call `menus[]`.

**custom_control.js**

```javascript
const botbuilder_dialogs = require("botbuilder-dialogs");
const botbuilder = require("botbuilder");


class menuControl extends botbuilder_dialogs.Control {
    constructor(defaultMenuName) {
        super();
        this.currentMenu = defaultMenuName;
    }

    async dialogBegin(dc, args){
        dc.instance.state = Object.assign({}, args);
        this.currentMenu = (args ? args.menuName : this.currentMenu);
        return this.showMenu(dc);
    }

    async dialogContinue(dc){
        return this.showMenu(dc)
    }

    // Helper function to display the choosen menu
    async showMenu(dc){
        const utterance = (dc.context.activity.text || '').trim();
        var menuItems = menus[utterance];
        
        // Display the selected menu with options to display other menus
        if(menuItems){
            var itemFacts = [];
            Object.keys(menuItems).forEach(i => {
                itemFacts.push({
                    "title": "Item:",
                    "value": `${menuItems[i].description}`,
                },
                {
                    "title": "Price",
                    "value": `${menuItems[i].price}`
                });
            });

            var adaptiveCard = botbuilder.CardFactory.adaptiveCard({
                "type": "AdaptiveCard",
	            "version": "1.0",
                "body": [{
                "type": "Container",
                "items": [
                    {
                        "type": "TextBlock",
                        "text": `${utterance}`,
                        "weight": "bolder",
                        "size": "large"
                    },
                    {
                        "type": "FactSet",
                        "facts": itemFacts
                    }
                ]
                }]
            });
            await dc.context.sendActivity(botbuilder.MessageFactory.attachment(adaptiveCard));
        }
        await dc.context.sendActivity(botbuilder.MessageFactory.suggestedActions(Object.keys(menus)));
        return dc.end();
    }

}
exports.menuControl = menuControl;

// List of menus

const menus = [];
menus["burgerMenu"] = {
    cheeseBurger: {
        description: "Cheese Burger",
        price: 1.99
    },
    hamBurger: {
        description: "Hamburger",
        price: 2.99
    },
    chickenBurger: {
        description: "Grilled Checken Burger",
        price: 3.99
    }
}

menus["dessertMenu"] = {
    applePie: {
        description: "Apple Pie",
        price: 3.99
    },
    cherryPie: {
        description: "Cherry Pie",
        price: 3.99
    },
    chocolateChipCookie: {
        description: "Chocolate Chip Cookie",
        price: .99
    }
}

menus["drinkMenu"] = {
    coke: {
        description: "Cococla",
        price: 1.25
    },
    pepsi: {
        description: "Pepsi",
        price: 1.25
    },
    mtdew: {
        description: "Mt. Dew",
        price: 1.25
    }
}

```

## Add custom control to bot

To add the custom control to your bot, simply import it and add it to the bot's `dialogs` object.

**app.js**

```javascript
const customControl = require("./custom_control");
dialogs.add('showMenu', new customControl.menuControl({menuName: "burgerMenu"}));
```

The syntax for adding a custom control to your bot is exactly the same as adding a `prompt`. Once it is added, you can push it onto the dialog stack just like any other dialog or prompts.

**app.js**

```javascript
// Show the menu control if user ask for 'menu'
if(utterance.match(/menu/ig)){
    await dc.begin('showMenu');
}
```

## Next step

Now that you know how to create your own custom control, lets take a look at how you can `compositControl` to [create modular bot logic](bot-builder-javascript-compositcontrol.md).
