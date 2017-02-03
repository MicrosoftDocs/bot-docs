---
layout: page
title: Overview
permalink: /en-us/azure-bot-service/templates/overview/
weight: 11990
parent1: Azure Bot Service
parent2: Templates
---

Azure Bot Service is powered by the serveless infrastructure of Azure functions, and it shares its [runtime concepts](https://azure.microsoft.com/en-us/documentation/articles/functions-reference/){:target="_blank"}, which you should become familiar with.

All Azure Bot Service bots include the following Azure Functions specific files:


<div id="thetabs1">
    <ul>
        <li><a href="#tab11">C#</a></li>
        <li><a href="#tab12">Node.js</a></li>
    </ul>

    <div id="tab11">

|**File**|**Description**
|function.json|This file contains the function’s bindings. You should not modify this file.
|project.json|This file contains the project’s NuGet references. You should only have to change this file if you add a new reference.
|project.lock.json|This file is generated automatically, and should not be modified.
|host.json|A metadata file that contains the global configuration options affecting the function.

    </div>
    <div id="tab12">

|**File**|**Description**
|function.json|This file contains the function’s bindings. You should not modify this file.
|packaje.json|This file contains the project’s NuGet references. You should only have to change this file if you add a new reference.
|host.json|A metadata file that contains the global configuration options affecting the function.


    </div>  
</div>