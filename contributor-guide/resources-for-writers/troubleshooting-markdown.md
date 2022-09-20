# Troubleshooting Markdown issues

## Tabbed content

Tabbed content consists of 2 to 4 sections of the form `# [Tab Display Name](#tab/tab-id)` followed by a triple-dash (`---`).

- If the triple-dash is missing or is not preceded by a blank line, you will get strange warnings when you try to build your content, such as some sort of duplicate tab warning.

See [Tabbed conceptual](https://review.learn.microsoft.com/help/platform/validation-ref/tabbed-conceptual?branch=main#approved-tabs) for a list of the supported tab IDs.

~~~markdown
# [C#](#tab/csharp)

```csharp
```

# [JavaScript](#tab/javascript)

```javascript
```

---
~~~
