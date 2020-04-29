> [!IMPORTANT]
> With the release of the Bot Framework 4.8 SDK, the .NET Bot Framework samples now target the .NET Core 3.1 SDK.
> Not all Azure data centers are configured to build such bots.
>
> See the map of [.NET Core on App Service](https://aspnetcoreon.azurewebsites.net/) for the centers in which you can build .NET Core 3.1 apps using Kudu. (All centers can run .NET Core 3.1 apps.)
>
> If you you are deploying a bot that targets the .NET Core 3.1 SDK and you are deploying to a center that can't build .NET Core 3.1 apps using Kudu, use this work around to prepare and zip up your bot files; otherwise, you can use the steps in the next sections.
>
> 1. Run this command in the directory that contains the solution or project file for your bot.
>
>    ```powershell
>    dotnet publish --configuration Release --runtime win-x86 --self-contained
>    ```
>
> 1. Zip up the contents of the **/Release/netcoreapp3.1/publish/** folder.
