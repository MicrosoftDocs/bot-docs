When using the non-configured [zip deploy API](https://github.com/projectkudu/kudu/wiki/Deploying-from-a-zip-file-or-url) to deploy your bot's code, Web App/Kudu's behavior is as follows:

_Kudu assumes by default that deployments from zip files are ready to run and do not require additional build steps during deployment, such as npm install or dotnet restore/dotnet publish._

As such, it is important to include your built code and with all necessary dependencies in the zip file being deployed to the Web App, otherwise your bot will not work as intended.

> [!IMPORTANT]
> Before zipping your project files, make sure that you are _in_ the project folder. 
> - For C# bots, it is the folder that has the .csproj file. 
> - For JavaScript bots, it is the folder that has the app.js or index.js file. 
> - For TypeScript bots, it is the folder that includes the _src_ folder (where the bot.ts and index.ts files are). 
>
>**Within** the project folder, select all the files and zip them up, then run the command while still in the folder. 
>
> If your root folder location is incorrect, the **bot will fail to run in the Azure portal**.
