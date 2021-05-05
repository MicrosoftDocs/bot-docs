When using the non-configured [zip deploy API](https://github.com/projectkudu/kudu/wiki/Deploying-from-a-zip-file-or-url) to deploy your bot's code, Web App/Kudu's behavior is as follows:

_Kudu assumes by default that deployments from zip files are ready to run and do not require additional build steps during deployment, such as npm install or dotnet restore/dotnet publish._

It is important to include your built code with all necessary dependencies in the zip file being deployed, otherwise your bot will not work as intended.

> [!IMPORTANT]
> Before zipping your project files, make sure that you are in the bot's project folder.
>
> - For C# bots, it is the folder that has the .csproj file.
> - For JavaScript bots, it is the folder that has the app.js or index.js file.
> - For TypeScript bots, it is the folder that includes the _src_ folder (where the bot.ts and index.ts files are).
> - For Python bots, it is the folder that has the app.py file.
>
> **Within** the project folder, make sure you select all the files and folders before running the command to create the zip file. This will create a single zip file within the project folder. If your root folder location is incorrect, the **bot will fail to run in the Azure portal**.
