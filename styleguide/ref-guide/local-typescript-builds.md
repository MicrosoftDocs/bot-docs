# Local JS ref doc builds (how to)

These instructions are distilled from the **Onboarding guide** and a variety of conversations and experiments.

- Most of the command examples are for a command prompt, as opposed to PowerShell.

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

I've been using a **\\temp\\typeDocs** directory for my intermediate files.

```cmd
rmdir /s /q \temp\typeDocs
```

## typedoc

1. Switch to the root of your local copy of the **botbuilder-js** repo.

    ```cmd
    cd \Users\jofingold\source\repos\botbuilder-js
    ```

1. Run **typedoc** on **each** library to generate the intermediate JSON files.

    ```cmd
    typedoc --mode file --json \temp\typeDocs\<lib-name>.json libraries\<lib-name>\src --ignoreCompilerErrors --includeDeclarations --excludeExternals --excludeNotExported --excludePrivate
    ```

    For 4.5, the set of commands looked like this:

    ```cmd
    typedoc --mode file --json \temp\typeDocs\botbuilder.json libraries\botbuilder\src --ignoreCompilerErrors --includeDeclarations --excludeExternals --excludePrivate --excludeNotExported
    typedoc --mode file --json \temp\typeDocs\botbuilder-ai.json libraries\botbuilder-ai\src --ignoreCompilerErrors --includeDeclarations --excludeExternals --excludePrivate --excludeNotExported
    typedoc --mode file --json \temp\typeDocs\botbuilder-applicationinsights.json libraries\botbuilder-applicationinsights\src --ignoreCompilerErrors --includeDeclarations --excludeExternals --excludePrivate --excludeNotExported
    typedoc --mode file --json \temp\typeDocs\botbuilder-azure.json libraries\botbuilder-azure\src --ignoreCompilerErrors --includeDeclarations --excludeExternals --excludePrivate --excludeNotExported
    typedoc --mode file --json \temp\typeDocs\botbuilder-core.json libraries\botbuilder-core\src --ignoreCompilerErrors --includeDeclarations --excludeExternals --excludePrivate --excludeNotExported
    typedoc --mode file --json \temp\typeDocs\botbuilder-dialogs.json libraries\botbuilder-dialogs\src --ignoreCompilerErrors --includeDeclarations --excludeExternals --excludePrivate --excludeNotExported
    typedoc --mode file --json \temp\typeDocs\botbuilder-testing.json libraries\botbuilder-testing\src --ignoreCompilerErrors --includeDeclarations --excludeExternals --excludePrivate --excludeNotExported
    typedoc --mode file --json \temp\typeDocs\botframework-config.json libraries\botframework-config\src --ignoreCompilerErrors --includeDeclarations --excludeExternals --excludePrivate --excludeNotExported
    typedoc --mode file --json \temp\typeDocs\botframework-connector.json libraries\botframework-connector\src --ignoreCompilerErrors --includeDeclarations --excludeExternals --excludePrivate --excludeNotExported
    typedoc --mode file --json \temp\typeDocs\botframework-schema.json libraries\botframework-schema\src --ignoreCompilerErrors --includeDeclarations --excludeExternals --excludePrivate --excludeNotExported
    ```

## type2docfx

1. Switch to the directory containing the intermediate JSON files.

    ```cmd
    cd \temp\typeDocs
    ```

1. Run **type2docfx** for each JSON file to generate intermediate YAML files.

    ```cmd
    type2docfx <lib-name>.json _yaml\<lib-name>
    ```

    For 4.5, the set of commands looked like this:

    ```cmd
    type2docfx botbuilder.json _yaml\botbuilder
    type2docfx botbuilder-ai.json _yaml\botbuilder-ai
    type2docfx botbuilder-applicationinsights.json _yaml\botbuilder-applicationinsights
    type2docfx botbuilder-azure.json _yaml\botbuilder-azure
    type2docfx botbuilder-core.json _yaml\botbuilder-core
    type2docfx botbuilder-dialogs.json _yaml\botbuilder-dialogs
    type2docfx botbuilder-testing.json _yaml\botbuilder-testing
    type2docfx botframework-config.json _yaml\botframework-config
    type2docfx botframework-connector.json _yaml\botframework-connector
    type2docfx botframework-schema.json _yaml\botframework-schema
    ```

## Aggregate the TOC files

Note that **type2doxfx** generates a TOC for each library. We need to combine these together.

1. The previous set of commands generated YAML files in a set of subdirectories. Drop down to the root of the YAML files.

    ```cmd
    cd _yaml
    ```

1. Create the combined **toc.yml** file by appending all the lower-level TOCs into a common, master TOC.

    For 4.5, the command looked like this:

    ```cmd
    type botbuilder\toc.yml botbuilder-ai\toc.yml botbuilder-applicationinsights\toc.yml botbuilder-azure\toc.yml botbuilder-core\toc.yml botbuilder-dialogs\toc.yml botbuilder-testing\toc.yml botframework-config\toc.yml botframework-connector\toc.yml botframework-schema\toc.yml > toc.yml
    ```

## Prepare the JS ref doc repo

1. Create a branch off of **botbuilder-docs-sdk-typescript**
1. Replace the old "autogen" files with the ones you generated locally.

    ```cmd
    cd \Users\jofingold\source\repos\botbuilder-docs-sdk-typescript\botbuilder-typescript\docs-ref-autogen\
    del /s /q *.*
    robocopy \temp\typeDocs\_yaml .\ *.* /s
    ```

## Option 1: local OPS build

1. In PowerShell, switch to the repo root and set your execution policy. Agree to **[A]ll** the policy changes.

    ```powershell
    cd \Users\jofingold\source\repos\botbuilder-docs-sdk-typescript\
    Set-ExecutionPolicy -Scope Process -ExecutionPolicy Unrestricted
    ```

1. If it already exists, delete the **_themes** directory. Agree to delete **[A]ll** items in the directory.

    ```powershell
    rmdir _themes
    ```

1. Run the publishing script:

    ```powershell
    .\.openpublishing.build.ps1
    ```

## Option 2: local docFX build

1. Run docfx (from \Users\jofingold\source\repos\botbuilder-docs-sdk-typescript\botbuilder-typescript)

    ```cmd
    cd \Users\jofingold\source\repos\botbuilder-docs-sdk-typescript\botbuilder-typescript
    ```

1. Clobber the old site

    ```cmd
    rmdir /s /q docfx_project
    rmdir /s /q obj
    rmdir /s /q _site
    ```

1. Rebuild the site

    ```cmd
    "C:\Program Files\docfx\docfx.exe" init -q
    "C:\Program Files\docfx\docfx.exe"
    ```

## Use docfx to view the built site

1. Serve the site locally:

    ```cmd
    "C:\Program Files\docfx\docfx.exe" serve _site
    ```

1. Open the site in a browser to review the local build:
   - http://localhost:8080/_site/botbuilder-typescript/botbuilder-ts-latest/api/botbuilder/
   - **Note**: The port number might be different; check the status message from the docfx serve command.
   - **Note**: Links in the TOC are missing a **.html** at the end of the URLs.

---

# Other notes

- Do use @remarks tags.
- Don't use [[ ]] links.
- Do use \[\<link-text>](xref:\<link-uid>) links.
  - The UIDs are in the generated .yaml files.
  - Or, search for the target at https://docs.microsoft.com/en-us/javascript/api/
- Don't use @see tags.
