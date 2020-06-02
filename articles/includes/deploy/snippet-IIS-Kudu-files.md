
You need to prepare your project files before you can deploy your C#, Javascript, or Typescript bot. If you are deploying a Python bot you can skip this step and continue to step 5.2.

<!-- **C# bots** -->
##### [C#](#tab/csharp)

```cmd
az bot prepare-deploy --lang Csharp --code-dir "." --proj-file-path "MyBot.csproj"
```

You must provide the path to the .csproj file relative to --code-dir. This can be performed via the --proj-file-path argument. The command would resolve --code-dir and --proj-file-path to "./MyBot.csproj".

This command generates a `.deployment` file in your bot project folder.

<!-- **JavaScript bots** -->
##### [JavaScript](#tab/javascript)

```cmd
az bot prepare-deploy --code-dir "." --lang Javascript
```

The command generates two `web.config` file in your project folder. Node.js apps need web.config to work with IIS on Azure App Services. Make sure web.config is saved to the root of your bot.

<!-- **TypeScript bots** -->
##### [TypeScript](#tab/typescript)

```cmd
az bot prepare-deploy --code-dir "." --lang Typescript
```

This command generates a `web.config` file in your project folder.

<!-- **TPython bots** -->
##### [Python](#tab/Python)

You do not need to prepare your project files before deploying a Python bot. Continue to step 5.2.

---
