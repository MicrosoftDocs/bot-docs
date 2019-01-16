Get the encryption key.

1. Log into the [Azure portal](http://portal.azure.com/).
1. Open the Web App Bot resource for your bot.
1. Open the bot's **Application Settings**.
1. In the **Application Settings** window, scroll down to the **Application settings**.
1. Locate the **botFileSecret** and copy its value.

Decrypt the .bot file.

```cmd
msbot secret --bot <name-of-bot-file> --secret "<bot-file-secret>" --clear
```

| Option | Description |
|:---|:---|
| --bot | The relative path to the downloaded .bot file. |
| --secret | The encryption key. |

Copy the decrypted `.bot` file to the directory that contains your local bot project, update your bot to use this new `.bot` file, and remove your old `.bot` file.

# [C#](#tab/csharp)

In **appsettings.json**, update the **botFilePath** property to point to the new `.bot` file you've added to your local directory.

# [JavaScript](#tab/javascript)

In **.env**, update the **botFilePath** property to point to the new `.bot` file you've added to your local directory.

---

Once your bot has been updated, delete the temporary directory of the downloaded bot.