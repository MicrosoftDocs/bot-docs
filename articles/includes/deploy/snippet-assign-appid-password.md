
Add the app ID and password for the Azure Bot resource to your bot project configuration file.

### [C#](#tab/csharp)

The `appsettings.json` file contains these settings:

```json
{
  "MicrosoftAppId": "<your app id>",
  "MicrosoftAppPassword": "<your password>"
}
```

### [JavaScript / TypeScript](#tab/javascript+typescript)

The `.env` file contains these settings:

```javascript
MicrosoftAppId="<your app id>"
MicrosoftAppPassword="<your password>"
```

### [Java](#tab/java)

The `application.properties` file contains these settings:

```java
MicrosoftAppId="<your app id>"
MicrosoftAppPassword="<your password>"
```

### [Python](#tab/python)

The `config.py` file contains these settings:

```python
APP_ID = os.environ.get("MicrosoftAppId", "<your app id>")
APP_PASSWORD = os.environ.get("MicrosoftAppPassword", "<your password>")
```

---

>[!IMPORTANT]
> After you have updated the configuration file, make sure to clean and rebuild the bot project.
