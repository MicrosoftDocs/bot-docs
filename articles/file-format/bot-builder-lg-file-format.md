---
title: .lg file format - Bot Service
description: .lg file format reference
keywords: lg file format, reference, language generation
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: micchow
ms.service: bot-service
ms.topic: reference
ms.date: 11/01/2021
monikerRange: 'azure-bot-service-4.0'
---

# .lg file format

[!INCLUDE [applies-to-v4](../includes/applies-to-v4-current.md)]

The .lg file describes language generation templates with entity references and their composition. This article covers the various concepts expressed with the .lg file format.

## Special Characters

### Comments

Use **>** to create a comment. All lines that have this prefix will be skipped by the parser.

```lg
> This is a comment.
```

### Escape character

Use **\\** as an escape character.

```lg
# TemplateName
- You can say cheese and tomato \[toppings are optional\]
```

## Arrays and objects

### Create an array

To create an array, use the **${[object1, object2, ...]}** syntax. For example, this  expression:

```lg
${['a', 'b', 'c']}
```

Returns the array `['a', 'b', 'c']`.

### Create an object

To create an object, use the **${{key1:value1, key2:value2, ...}}** syntax. For example, this expression:

```lg
${{user: {name: "Wilson", age: 27}}}
```

Returns the following JSON object:

```json
{
  "user": {
    "name": "Wilson",
    "age": 27
  }
}
```

## Templates

**Templates** are the core concept of the language generation system. Each template has a name and one of the following:

- a list of one-of variation text values
- a structured content definition
- a collection of conditions, each with:
  - an [adaptive expression][3]
  - a list of one-of variation text values per condition

### Template names

Template names are case-sensitive and can only contain letters, underscores, and numbers. The following is an example of a template named `TemplateName`.

```lg
# TemplateName
```

Templates can't start with a number, and any part of a template name split by **.** can't start with a number.

### Template response variations

Variations are expressed as a Markdown list. You can prefix each variation using the **-**, **'**, or **+** character.

```lg
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

```lg
> Greeting template with two variations.
# GreetingPrefix
- Hi
- Hello
```

### Conditional response template

Conditional response templates let you author content that's selected based on a condition. All conditions are expressed using [adaptive expressions][3].

> [!IMPORTANT]
> Conditional templates can't be nested in a single conditional response template. Use composition in a [structured response template](../language-generation/language-generation-structured-response-template.md) to nest conditionals.

#### If-else template

The if-else template lets you build a template that picks a collection based on a cascading order of conditions. Evaluation is top-down and stops when a condition evaluates to `true` or the ELSE block is hit.

Conditional expressions are enclosed in braces **${}**. Here's an example that shows a simple IF ELSE conditional response template definition.

```lg
> time of day greeting reply template with conditions.
# timeOfDayGreeting
- IF: ${timeOfDay == 'morning'}
    - good morning
- ELSE:
    - good evening
```

Here's another example that shows an if-else conditional response template definition. Note that you can include references to other simple or conditional response templates in the variation for any of the conditions.

```lg
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

```lg
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

```lg
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

> [!NOTE]
> Like conditional templates, switch templates can't be nested.

### Structured response template

Structured response templates let you define a complex structure that supports major LG functionality, like templating, composition, and substitution, while leaving the interpretation of the structured response up to the caller of the LG library.

For bot applications, we natively support:

- activity definition
- card definition

Read about [structure response templates](../language-generation/language-generation-structured-response-template.md) for more information.

## Template composition and expansion

### References to templates

Variation text can include references to another named template to aid with composition and resolution of sophisticated responses. References to other named templates are denoted using braces, such as **${\<TemplateName>()}**.

```lg
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

```lg
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

```lg
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

Given templates and prebuilt functions share the same invocation signature, a template name can't be the same as a prebuilt function name.

 A template name should not match a prebuilt function name. The prebuilt function takes precedence. To avoid such conflicts, you can prepend `lg.` when referencing your template name. For example:

```lg
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

```lg
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

```lg
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

```lg
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

```lg
[Link description](filePathOrUri)
```

All templates defined in the target file will be pulled in. Ensure that your template names are unique (or namespaced with `# \<namespace>.\<templatename>`) across files being pulled in.

```lg
[Shared](../shared/common.lg)
```

## Functions injected by LG

[Adaptive expressions][3] provide the ability to inject a custom set of functions. Read [functions injected from the LG library][13] for more information.

## Options

Developers can set parser options to further customize how input is evaluated. Use the `> !#` notation to set parser options.

> [!IMPORTANT]
>
> The last setting found in the file trumps any prior setting found in the same document.

### Strict option

Developers who do not want to allow a null result for a null evaluated result can implement the **strict** option. Below is an example of a simple strict option:

```lg
> !# @strict = true
# template
- hi
```

If the strict option is on, null errors will throw a friendly message.

```lg
# welcome
- hi ${name}
```

If name is null, the diagnostic would be **'name' evaluated to null. [welcome] Error occurred when evaluating '- hi ${name}'.**. If strict is set to false or not set, a compatible result will be given. The above sample would produce **hi null**.

### replaceNull option

Developers can create delegates to replace null values in evaluated expressions by using the **replaceNull** option:

```lg
> !# @replaceNull = ${path} is undefined
```

In the above example, the null input in the `path` variable would be replaced with **${path} is undefined**. The following input, where `user.name` is null:
:

```lg
hi ${user.name}
```

Would result in **hi user.name is undefined**.

### lineBreakStyle option

Developers can set options for how the LG system renders line breaks using the **lineBreakStyle** option. Two modes are currently supported:

- `default`: line breaks in multiline text create normal line breaks.
- `markdown`: line breaks in multiline text will be automatically converted to two lines to create a newline

The example below shows how to set the lineBreakStyle option to `markdown`:

```lg
> !# @lineBreakStyle = markdown
```

### Namespace option

You can register a namespace for the LG templates you want to export. If there is no namespace specified, the namespace will be set to the filename without an extension.

The example below shows how to set the namespace option to `foo`:

```lg
> !# @Namespace = foo
```

### Exports option

You can specify a list of LG templates to export. The exported templates can be called like prebuilt functions.

The example below shows how to set the exports option to `template1, template2`:

```lg
> !# @Namespace = foo
> !# @Exports = template1, template2

# template1(a, b)
- ${a + b}

# template2(a, b)
- ${join(a, b)}
```

Use  `foo.template1(1,2), foo.template2(['a', 'b', 'c'], ',')` to call these exported templates.

### Cache scope

The _cache scope_ options let you control when the LG evaluator reevaluates an expression it has seen before and when it stores and uses a cached result.

- _Global cache_ is effective in the life cycle of an evaluation. LG caches all evaluation results, and if the template name and parameters are the same, returns the result from the cache.
- _Local cache_ scope is the default. In the same layer, if the previous template has been called with the same template name and the same parameters, the cached result is directly returned.
- _None cache_ scope disables all cache scope, and each time returns the new result.

For examples, see the [global](#global-cache-scope-example) and [local](#local-cache-scope-example) cache scope examples.

```lg
> !# @cacheScope= global // global cache
> !# @cacheScope= local // local cache
> !# @cacheScope= none // none cache
> !# @cacheScope= xxx // fallback to local cache
```

Note that the cache scope option is case-insensitive.

```lg
> !# @cacheScope= global // ok
> !# @CACHESCOPE= global // ok
> !# @cachescope= global // ok
```

Note that cache scope follows the scope of the entrance .lg file.

Say you have two files: `a.lg` and `b.lg`, shown below:

**a.lg**

```lg
> !# @cacheScope= global
 [import](b.lg)
```

**b.lg**

```lg
> !# @cacheScope= none
# template1
- ${template2()} ${template2()}

# template2
- ${rand(1, 10000000)}
```

If you run the following code, you'll notice that `template2` uses the cached result of the first evaluated result because of the `global` cache scope option in **a.lg**:

```csharp
var templates = Templates.ParseFile("a.lg");
var result = templates.Evaluate("template1"); // the second "template2" would use the cache of the first evaluate result
```

#### Re-execute mark influence

If the template name ends with "!", the template forces re-execution. This result won't be added to the cache regardless of the cache scope.

Say you have following template:

```lg
# template2
- ${template1()} ${template1!()} ${template1()}
```

`template1!()` fires and the result is added to the cache. The second `template1()` clobbers the result from the first `template1()`. The final call uses the results stored in the cache.

#### Global cache scope example

Say you have the following templates:

```lg
# template1
- ${template2()} ${template3()}

# template2
- ${rand(1, 10)}
- abc
- hi

# template3
- ${template2()}
```

`template2` would be evaluated once, and the second execution in `template3` would apply the cache of the first one.

Another example is in the following code snippet:

```csharp
var templates = Templates.ParseFile("xxx.lg");
var result1 = templates.Evaluate("template", null, new EvaluationOptions { CacheScope = LGCacheScope.Global});

// The second evaluation would drop all the results cached before.
var result2 = templates.Evaluate("template", null, new EvaluationOptions { CacheScope = LGCacheScope.Global});
```

A template is parsed using the `Templates.ParseFile()` function, and the template evaluation results are stored in `result1`. Note that the second evaluation results, `result2`, drops all results previously cached.

#### Local cache scope example

The following examples show when the local cache scope does and doesn't work. Assume that `t()` and `subT()` are templates that take a parameter:

```lg
>  Cache works, the second template call would re-use the first's result.
# template1
- ${t(param)} ${t(param)}

> Cache doesn't work because param1's value is different with param2's. value)
# template2
- ${t(param1)} ${t(param2)}

> Cache doesn't work because of different layers.
# template3
- ${subT(param1)} ${t(param2)}

# subT(param)
- ${t(param)}
```

## Additional Resources

- [C# API Reference](/dotnet/api/microsoft.bot.builder.languagegeneration)
- [JavaScript API reference](/javascript/api/botbuilder-lg)
- Read [Debug with Adaptive Tools](../bot-service-debug-adaptive-tools.md) to learn how to analyze and debug .lg files.

[1]:https://github.com/Microsoft/botbuilder-tools/blob/master/packages/Ludown/docs/lu-file-format.md
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
