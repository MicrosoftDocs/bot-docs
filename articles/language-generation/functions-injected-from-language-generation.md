---
title: Functions injected from the language generation library - Bot Service
description: Describes how to inject functions from LG into templates.
keywords: functions from lg, reference, language generation
author: kamrani
ms.author: kamrani
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.date: 05/16/2020
monikerRange: 'azure-bot-service-4.0'
---


# Functions injected from the language generation library

[!INCLUDE[applies-to](../includes/applies-to.md)]

The following article details how to inject functions from the [Language generation (LG)](../v4sdk/bot-builder-concept-language-generation.md) library.

## ActivityAttachment

Return an `activityAttachment` constructed from an object and a type.

```.lg
ActivityAttachment(<collection-of-objects>)
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*content*> | Yes | object | Object containing the information of the attachment |
| <*type*> | Yes | string  | A string representing the type of attachment |
|||||

| Return value | Type | Description |
| ------------ | -----| ----------- |
| <*activityAttachment*> | object | An `activityAttachment` formed from the inputs |
||||

*Example*:

This example converts a collection of objects to an `activityAttachment`.
<!--
Using the `ActivityAttachment()` function in the template body, type, title, value are parameters in the template name.-->

Suppose you have the following template:

```.lg
# externalHeroCardActivity(type, title, value)
[Activity
    attachments = ${ActivityAttachment(json(fromFile('.\\herocard.json')), 'herocard')}
]
```

and the following `herocard.json`:

```.lg
{
  "title": "titleContent",
  "text": "textContent",
  "Buttons": [
    {
      "type": "imBack",
      "Title": "titleContent",
      "Value": "textContent",
      "Text": "textContent"
    }
  ],
  "tap": {
    "type": "${type}",
    "title": "${title}",
    "text": "${title}",
    "value": "${value}"
  }
}
```

By calling `externalHeroCardActivity()` as a function:

```.lg
externalHeroCardActivity('signin', 'Signin Button', 'http://login.microsoft.com')
```

It returns a `herocard`:

```.lg
{
    "lgType" = "attachment",
    "contenttype" = "herocard",
    "content" = {
        "title": "titleContent",
        "text": "textContent",
        "Buttons": [
            {
            "type": "imBack",
            "Title": "titleContent",
            "Value": "textContent",
            "Text": "textContent"
            }
        ],
        "tap": {
            "type": "signin",
            "title": "Signin Button",
            "text": "Signin Button",
            "value": "http://login.microsoft.com"
        }
    }
}
```

## expandText

Evaluate the plain text in an object and return the expanded text data.

```.lg
expandText(<object>)
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*object*> | Yes | object | The object with text to expand. |
||||

| Return value | Type | Description |
| ------------ | -----| ----------- |
| <*evaluated-result*> | object | The expanded text data. |
||||

*Example*

This example evaluates the plain text in a JSON object and returns the expanded text result.

Say you have the following object:

```json
{
	"@answer": "hello ${user.name}",
	"user": {
		"name": "vivian"
	}
}
``` 

Calling `expandText(@answer)` will result in the object **hello vivian**.

## template

Return the evaluated result of given template name and scope.

```.lg
template(<templateName>, '<param1>', '<param2>', ...)
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*templateName*> | Yes | string  | A string representing the template name |
| <*param1*>,<*param2*>, ... | Yes | Object  | The  parameters passed to the template |
|||||

| Return value | Type | Description |
| ------------ | -----| ----------- |
| <*evaluated-result*> | object | The result evaluated from the template as a function  |
||||

*Example*

This example evaluates the result of calling the template as a function.

Suppose you have the following template:

```.lg
    # welcome(userName)

    - Hi ${userName}

    - Hello ${userName}

    - Hey ${userName}
```

Calling `template("welcome", "DL")` will result in one of the following:

- _Hi DL_
- _Hello DL_
- _Hey DL_

## fromFile

Return the evaluated result of the expression in the given file.

```.lg
fromFile(<filePath>)
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*filePath*> | Yes | string  | relative or absolute path of a file contains expressions |
|||||

| Return value | Type | Description |
| ------------ | -----| ----------- |
| <*result*> | string | The string representation of the evaluated result |
||||

*Example*

This example evaluates the result from the given file.

Suppose you have a file called  `/home/user/test.txt`. Inside the file there is the following:
```.lg
   `you have ${add(1,2)} alarms`

    fromFile('/home/user/test.txt')
```

The `fromFile()` function will evaluate the expression and the result will replace the original expression.

Calling `fromFile('/home/user/test.txt')` results in the string _you have 3 alarms_.

## isTemplate

Return whether a given template name is included in the evaluator.

```.lg
isTemplate(<templateName>)
```

| Parameter | Required | Type | Description |
| --------- | -------- | ---- | ----------- |
| <*templateName*> | Yes | String  | A template name to check |
|||||

| Return value | Type | Description |
| ------------ | -----| ----------- |
| <*result*> | Boolean | Whether the given template name is included in the evaluator  |
||||

*Example*

This example uses the `isTemplate()` function to check whether a given template name is in the evaluator. For example, here are three templates:

```.lg
# welcome
- hi

# show-alarms
- 7:am and 8:pm

# add-to-do
- you add a task at 7:pm
```

Calling `isTemplate("welcome")` would evaluate to `true`. Calling `isTemplate("delete-to-do")` would evaluate to `false`.

## Additional Information

- [.lg file format](../file-format/bot-builder-lg-file-format.md) 
- [Structured response template](language-generation-structured-response-template.md) 
