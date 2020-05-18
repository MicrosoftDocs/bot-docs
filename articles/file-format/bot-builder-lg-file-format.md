---
title: .lg file format - Bot Service
description: .lg file format reference
keywords: lg file format, reference, language generation
author: kamrani
ms.author: kamrani
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.date: 05/16/2020
monikerRange: 'azure-bot-service-4.0'
---

# .lg file format

[!INCLUDE[applies-to](../includes/applies-to.md)]

The `.lg` file describes language generation templates with entity references and their composition. This article covers the various concepts expressed with the `.lg` file format.

## Special Characters

### Comments

Use **>** to create a comment. All lines that have this prefix will be skipped by the parser.

```.lg
> This is a comment.
```

### Escape character

Use **\\** as an escape character.

```.lg
# TemplateName
- You can say cheese and tomato \[toppings are optional\]
```

## Templates

**Templates** are the core concept of the language generation system. Each template has a name and one of the following:

- a list of one-of variation text values
- a structured content definition
- a collection of conditions, each with:
    - an [adaptive expression][3]
    - a list of one-of variation text values per condition

Template names follow the Markdown header definition.

```.lg
# TemplateName
```

Variations are expressed as a Markdown list. You can prefix each variation using the **-**, **'**, or **+** character.

```.lg
# Template1
- text variation 1
- text variation 2
- one
- two

# Template2
* text variation 1
* text variation 2

# Template3
+ one
+ two
```

### Simple response template

A simple response template includes one or more variations of text that are used for composition and expansion. One of the variations provided will be selected at random by the LG library.

Here is an example of a simple template that includes two variations.

```.lg
> Greeting template with 2 variations.
# GreetingPrefix
- Hi
- Hello
```

### Conditional response template

Conditional response templates let you author content that's selected based on a condition. All conditions are expressed using [adaptive expressions][3].
 
> [!IMPORTANT]
> Conditional templates cannot be nested in a single conditional response template. Use composition in a [structured response template](../language-generation/language-generation-structured-response-template.md) to nest conditionals. 

#### If-else template

The if-else template lets you build a template that picks a collection based on a cascading order of conditions. Evaluation is top-down and stops when a condition evaluates to `true` or the ELSE block is hit.

Conditional expressions are enclosed in braces **${}**. Here's an example that shows a simple IF ELSE conditional response template definition.

```.lg
> time of day greeting reply template with conditions.
# timeOfDayGreeting
- IF: ${timeOfDay == 'morning'}
    - good morning
- ELSE:
    - good evening
```

Here's another example that shows an if-else conditional response template definition. Note that you can include references to other simple or conditional response templates in the variation for any of the conditions.

```.lg
# timeOfDayGreeting
- IF: ${timeOfDay == 'morning'}
    - ${morningTemplate()}
- ELSEIF: ${timeOfDay == 'afternoon'}
    - ${afternoonTemplate()}
- ELSE:
    - I love the evenings! Just saying. ${eveningTemplate()}
```

#### Switch template

The switch template lets you design a template that matches an expression's value to a CASE clause and produces output based on that case. Condition expressions are enclosed in braces **${}**.

Here's how you can specify a SWITCH CASE DEFAULT block in LG.

```.lg
# TestTemplate
- SWITCH: ${condition}
- CASE: ${case-expression-1}
    - output1
- CASE: ${case-expression-2}
    - output2
- DEFAULT:
   - final output
```

Here's a more complicated SWITCH CASE DEFAULT example:

```.lg
> Note: Any of the cases can include reference to one or more templates.
# greetInAWeek
- SWITCH: ${dayOfWeek(utcNow())}
- CASE: ${0}
    - Happy Sunday!
-CASE: ${6}
    - Happy Saturday!
-DEFAULT:
    - ${apology-phrase()}, ${defaultResponseTemplate()}
```

Like conditional templates, switch templates also cannot be nested. 

### Structured response template

Structured response templates let you define a complex structure that supports major LG functionality, like templating, composition, and substitution, while leaving the interpretation of the structured response up to the caller of the LG library.

For bot applications, we natively support:

- activity definition
- card definition

Read about [structure response templates](../language-generation/language-generation-structured-response-template.md) for more information.

## Template composition and expansion

### References to templates

Variation text can include references to another named template to aid with composition and resolution of sophisticated responses. References to other named templates are denoted using braces, such as **${<TemplateName>()}**.

```.lg
> Example of a template that includes composition reference to another template.
# GreetingReply
- ${GreetingPrefix()}, ${timeOfDayGreeting()}

# GreetingPrefix
- Hi
- Hello

# timeOfDayGreeting
- IF: ${timeOfDay == 'morning'}
    - good morning
- ELSEIF: ${timeOfDay == 'afternoon'}
    - good afternoon
- ELSE:
    - good evening
```

Calling the `GreetingReply` template can result in one of the following expansion resolutions:

```.lg
Hi, good morning
Hi, good afternoon
Hi, good evening
Hello, good morning
Hello, good afternoon
Hello, good evening
```

## Entities

When used directly within a one-of variation text, entity references are denoted by enclosing them in braces, such as  ${`entityName`}, or without braces when used as a parameter.

Entities can be used as a parameter:
- within a [prebuilt function][4]
- within a condition in a [conditional response template](#conditional-response-template)
- to [template resolution call](#parametrization-of-templates)

## Using prebuilt functions in variations

[Prebuilt functions][4] supported by [adaptive expressions][3] can also be used inline in a one-of variation text to achieve even more powerful text composition. To use an expression inline, simply wrap it in braces.

```.lg
# RecentTasks
- IF: ${count(recentTasks) == 1}
    - Your most recent task is ${recentTasks[0]}. You can let me know if you want to add or complete a task.
- ELSEIF: ${count(recentTasks) == 2}
    - Your most recent tasks are ${join(recentTasks, ', ', ' and ')}. You can let me know if you want to add or complete a task.
- ELSEIF: ${count(recentTasks) > 2}
    - Your most recent ${count(recentTasks)} tasks are ${join(recentTasks, ', ', ' and ')}. You can let me know if you want to add or complete a task.
- ELSE:
    - You don't have any tasks.
```

The example above uses the [join][5] prebuilt function to list all values in the `recentTasks` collection.

Given templates and prebuilt functions share the same invocation signature, a template name cannot be the same as a prebuilt function name.

 A template name should not match a prebuilt function name. The prebuilt function takes precedence. To avoid such conflicts, you can prepend `lg.` when referencing your template name. For example:

```.lg
> Custom length function with one parameter.
# length(a)
- This is use's customized length function

# myfunc1
> will call prebuilt function length, and return 2
- ${length('hi')}

# mufunc2
> this calls the lg template and output 'This is use's customized length function'
- ${lg.length('hi')}
```

## Multiline text in variations

Each one-of variation can include multiline text enclosed in triple quotes.

```.lg
# MultiLineExample
    - ```This is a multiline list
        - one
        - two
        ```
    - ```This is a multiline variation
        - three
        - four
    ```
```

Multiline variation can request template expansion and entity substitution by enclosing the requested operation in braces, ${}.

```.lg
# MultiLineExample
    - ```
        Here is what I have for the order
        - Title: ${reservation.title}
        - Location: ${reservation.location}
        - Date/ time: ${reservation.dateTimeReadBack}
    ```
```

With multiline support, you can have the Language Generation sub-system fully resolve a complex JSON or XML (like SSML wrapped text to control bot's spoken reply).

## Parametrization of templates

To aid with contextual reusability, templates can be parametrized. Different callers to the template can pass in different values for use in expansion resolution.

```.lg
# timeOfDayGreetingTemplate (param1)
- IF: ${param1 == 'morning'}
    - good morning
- ELSEIF: ${param1 == 'afternoon'}
    - good afternoon
- ELSE:
    - good evening

# morningGreeting
- ${timeOfDayGreetingTemplate('morning')}

# timeOfDayGreeting
- ${timeOfDayGreetingTemplate(timeOfDay)}
```

## Importing external references

You can split your language generation templates into separate files and reference a template from one file in another. You can use Markdown-style links to import templates defined in another file.

```.lg
[Link description](filePathOrUri)
```

All templates defined in the target file will be pulled in. Ensure that your template names are unique (or namespaced with `# \<namespace>.\<templatename>`) across files being pulled in.


```.lg
[Shared](../shared/common.lg)
```

## Functions injected by LG

[Adaptive expressions][3] provide the ability to inject a custom set of functions. Read [functions injected from the LG library][13] for more information.

## Options

Deverloper can set parser options to further customize how input is evaluated. Use the `> !#` notation to set parser options.

> [!IMPORTANT]
>
> The last setting found in the file trumps any prior setting found in the same document.

### Strict option

Developers who do not want to allow a null result for a null evaluated result can implement the **strict** option. Below is an example of a simple strict option:

```.lg
> !# @strict = true
# template
- hi
```

If the strict option is on, null errors will throw a friendly message.

```.lg
# welcome
- hi ${name}
```

If name is null, the diagnostic would be **'name' evaluated to null. [welcome] Error occurred when evaluating '- hi ${name}'.**. If strict is set to false or not set, a compatible result will be given. The above sample would produce **hi null**.

### replaceNull option

Developers can creat delegates to replace null values in evaluated expressions by using the **replaceNull** option:

```.lg
> !# @replaceNull = ${path} is undefined 
```

In the above example, the null input in the `path` variable would be replaced with **${path} is undefined**. The following input, where `user.name` is null:
:

```.lg
hi ${user.name}
```

Would result in **hi user.name is undefined**.

### lineBreakStyle option

Developers can set options for how the LG system renders line breaks using the **lineBreakStyle** option. Two modes are currently supported:

- `default`: line breaks in multiline text create normal line breaks.
- `markdown`: line breaks in multiline text will be automatically converted to two lines to create a newline

The example below shows how to set the lineBreakStyle option to `markdown`:

```.lg
> !# @lineBreakStyle = markdown
```

## Additional Resources

- [Language generation API reference][2]


[1]:https://github.com/Microsoft/botbuilder-tools/blob/master/packages/Ludown/docs/lu-file-format.md
[2]:../language-generation/language-generation-API-reference.md
[3]:../v4sdk/bot-builder-concept-adaptive-expressions.md
[4]:../adaptive-expressions/adaptive-expressions-prebuilt-functions.md
[5]:../adaptive-expressions/adaptive-expressions-prebuilt-functions.md#join
[6]:https://github.com/microsoft/botframework-cli/tree/master/packages/chatdown
[7]:https://github.com/microsoft/botframework-cli/blob/master/packages/chatdown/docs/chatdown-format.md
[8]:https://github.com/microsoft/botframework-cli/blob/master/packages/chatdown/docs/examples/CardExamples.chat
[9]:https://github.com/microsoft/botframework-cli/blob/master/packages/chatdown/docs/chatdown-format.md#message-commands
[10]:https://github.com/microsoft/botframework-cli/blob/master/packages/chatdown/docs/chatdown-format.md#message-cards
[11]:https://github.com/microsoft/botframework-cli/blob/master/packages/chatdown/docs/chatdown-format.md#message-attachments
[12]:https://github.com/microsoft/botframework-cli/blob/master/packages/chatdown/docs/chatdown-format.md
[13]:../language-generation/functions-injected-from-language-generation.md
