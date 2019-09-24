# Local JS ref doc builds (how to)

These instructions are distilled from the **Onboarding guide** and a variety of conversations and experiments.

## Install tools

We nee **typedoc**, **type2docfx**, and **docfx** for local builds:

- The first two are available via npm:

    ```cmd
    npm i -g type2docfx --save-dev
    npm i -g typedoc
    ```

- Get **docfx** from https://github.com/dotnet/docfx/releases, and then extract it.
- Periodically check for updates, as these tools do not run a self check.

The example commands here assume **docfx** is in **C:\\Program Files\\docfx\\** and that you are using **\\temp\\typeDocs\\** for temporary files.

The "current" (as of 4.5) modules are:

- botbuilder
- botbuilder-ai
- botbuilder-applicationinsights
- botbuilder-azure
- botbuilder-core
- botbuilder-dialogs
- botbuilder-testing
- botframework-config
- botframework-connector
- botframework-schema

## Remove old temp files

```cmd
rmdir /s /q \temp\typeDocs
```

## typedoc

Run **typedoc** from root of your local botbuilder-js

```cmd
cd \Users\jofingold\source\repos\botbuilder-js
```

Run **typedoc** on each library to generate the intermediate JSON files.

```cmd
typedoc --mode file --json \temp\typeDocs\<lib-name>.json libraries\<lib-name>\src --ignoreCompilerErrors --includeDeclarations --excludeExternals --excludeNotExported --excludePrivate
```

## type2docfx

Run **type2docfx** from the directory containing the intermediate JSON files.

```cmd
cd \temp\typeDocs
```

Run **type2docfx** for each JSON file to generate intermediate YAML files.

```cmd
type2docfx <lib-name>.json _yaml\<lib-name>
```

## Aggregate the TOC files

**type2doxfx** generates a TOC for each library. We need to combine these together. (If the list of modules changes, this command will need updating.)

```cmd
cd _yaml
type botbuilder\toc.yml botbuilder-ai\toc.yml botbuilder-applicationinsights\toc.yml botbuilder-azure\toc.yml botbuilder-core\toc.yml botbuilder-dialogs\toc.yml botbuilder-testing\toc.yml botframework-config\toc.yml botframework-connector\toc.yml botframework-schema\toc.yml > toc.yml
```

## Option 1: local OPS build

1. Create a branch off of **botbuilder-docs-sdk-typescript**
1. Replace the old "autogen" files.

    ```cmd
    cd \Users\jofingold\source\repos\botbuilder-docs-sdk-typescript\botbuilder-typescript\docs-ref-autogen\
    del /s /q *.*
    robocopy \temp\typeDocs\_yaml .\ *.* /s
    ```

1. In PowerShell, set your execution policy and run the publishing script:

    ```powershell
	cd \Users\jofingold\source\repos\botbuilder-docs-sdk-typescript\
	Set-ExecutionPolicy -Scope Process -ExecutionPolicy Unrestricted
	.\.openpublishing.build.ps1
    ```

1. Copy the generated site files?

## Option 2: local docFX build

### Replace "autogen" files

- Delete the contents of \Users\jofingold\source\repos\botbuilder-docs-sdk-typescript\botbuilder-typescript\docs-ref-autogen
- Copy in the contents of \temp\typeDocs\_yaml

cd \Users\jofingold\source\repos\botbuilder-docs-sdk-typescript\botbuilder-typescript\docs-ref-autogen\
del /s /q *.*
robocopy \temp\typeDocs\_yaml .\ *.* /s

### docfx (from \Users\jofingold\source\repos\botbuilder-docs-sdk-typescript\botbuilder-typescript

cd \Users\jofingold\source\repos\botbuilder-docs-sdk-typescript\botbuilder-typescript

#### Clobber the old site

```cmd
rmdir /s /q docfx_project
rmdir /s /q obj
rmdir /s /q _site
```

#### Rebuild the site

```cmd
"C:\Program Files\docfx\docfx.exe" init -q
"C:\Program Files\docfx\docfx.exe"
"C:\Program Files\docfx\docfx.exe" serve _site
```

Open the site in a browser to review the local build:
- http://localhost:8080/botbuilder-ts-latest/api/botbuilder/

---

Do use @remarks tags.
Don't use [[ ]] links.
Do use [<link-text>](xref:<link-uid>) links.
- The UIDs are in the generated .yaml files.
- Or, search for the target at https://docs.microsoft.com/en-us/javascript/api/
Don't use @see tags.

Type alias: botbuilder-dialogs.PromptValidator
Type alias: botbuilder-dialogs.TokenizerFunction
Type alias: botbuilder-dialogs.WaterfallStep
 - Function: botbuilder-dialogs.defaultTokenizer
 - Function: botbuilder-dialogs.findChoices
 - Function: botbuilder-dialogs.findValues
 - Function: botbuilder-dialogs.recognizeChoices