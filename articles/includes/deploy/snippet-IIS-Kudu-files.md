
You need to prepare your project files before you can deploy your C#, Javascript, or Typescript bot. If you are deploying a Python bot you can skip this step.

<!-- **C# bots** -->
##### [C#](#tab/csharp)

```cmd
az bot prepare-deploy --lang Csharp --code-dir "." --proj-file-path "MyBot.csproj"
```

You must provide the path to the .csproj file relative to --code-dir. This can be performed via the --proj-file-path argument. The command would resolve --code-dir and --proj-file-path to "./MyBot.csproj"

<!-- **JavaScript bots** -->
##### [JavaScript](#tab/javascript)

```cmd
az bot prepare-deploy --code-dir "." --lang Javascript
```

This command will fetch a web.config which is needed for Node.js apps to work with IIS on Azure App Services. Make sure web.config is saved to the root of your bot.

<!-- **TypeScript bots** -->
##### [TypeScript](#tab/typescript)

```cmd
az bot prepare-deploy --code-dir "." --lang Typescript
```

This command works similarly to JavaScript above, but for a Typescript bot.

---

> [!NOTE]
>  For C# bots and JavaScript bots, the `az bot prepare-depoloy` command should generate a `.deployment` file in your bot project folder.
> For TypeScript bots, the command should generate two `web.config` files. One is in your project folder and another in the **src** folder within your project folder. 
