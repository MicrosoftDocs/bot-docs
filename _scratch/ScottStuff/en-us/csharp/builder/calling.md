---
layout: page
title: Skype Calling Bots
permalink: /en-us/csharp/builder/calling/
weight: 2500
parent1: Building your Bot Using Bot Builder for .NET
parent2: Skype Calling Bots
---

If you want to build an Interactive Voice Response ([IVR](https://en.wikipedia.org/wiki/Interactive_voice_response)) bot to automate common tasks for incoming customer calls, you can use the Calling Bot Application template to get started quickly. The template is a fully functional calling bot that has a simple menu for letting the user record his or her voice.

1. Install prerequisite software

    - Visual Studio 2015 (latest update). You can download the free community version at <a href="https://www.visualstudio.com/" target="_blank">www.visualstudio.com</a>

    - Important: Update all VS extensions to their latest versions. Click **Tools**, **Extensions an Updates**, and then **Updates**  
  
2. Download and install the Calling Bot Application template

    - Download the template from <a href="https://aka.ms/bf-builder-calling" target="_blank">here</a>

    - Save the zip file to your Visual Studio 2015 templates directory, which is traditionally in "%USERPROFILE%\Documents\Visual Studio 2015\Templates\ProjectTemplates\Visual C#\"  
  
3. Open Visual Studio

4. Create a new C\# project using the new Calling Bot Application template. The template is a fully functional Calling Bot that has a simple menu that lets the user record his or her voice.
   ![Create a new C\# project using the new %Calling %Bot Application template.](/en-us/images/ivr/calling-getstarted-create-project.png)

5. Register the Bot with Bot Framework (see [Registering a Bot](/en-us/registration/)) 

6. Configure the Skype channel to enable calling for your bot, and then register your calling endpoint with Skype
   ![Create a new C\# project using the new %Calling %Bot Application template.](/en-us/images/ivr/skypeconfig.png)

7. Record the AppId and AppPassword from the registration process in the project's web.config file

8. The `Microsoft.Bot.Builder.Calling.CallbackUrl` app setting should match the route set for CallingEvent in the template. The template sets the callback route to `https://<your domain>/api/calling/callback`. For example, if your service is deployed to ivrtest.azurewebsites.net, the URL should be https://ivrtest.azurewebsites.net/api/calling/callback.

9. Modify the Route attributes for the following methods in Controllers\CallingController.cs

    -   **ProcessIncomingCallAsync**: The route depends on the calling URL that you specified during registration. For example, if you specified https://ivrtest.azurewebsites.net/api/calling/call, the route would be **api/calling/call**.
    -   **ProcessCallingEventAsync**: The route needs to match the callback URL specified in the web.config file. The template uses **api/calling/callback**.

10. Publish the project


For information about receiving and handling Skype voice calls by bots, see [Skype Calling API](/en-us/skype/calling/).

For information about building a calling bot using the Bot Builder Calling SDK, see [Skype Calling Bot Tutorial](/en-us/csharp/builder/calling-tutorial/).




