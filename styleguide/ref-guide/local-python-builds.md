# Python local reference documentation build

This article describes how to create a local build for Python API reference documentation.

The API reference documentation is generated from comments in the source code and created using the [reStructuredText](https://en.wikipedia.org/wiki/ReStructuredText) format.

The **reStructuredText** (RST) is a file format for textual data used primarily in the Python programming language community for technical documentation.
It is a lightweight markup language designed to be both processable by documentation-processing software, and easily readable by humans.

- For the RST syntax, see [How to document a Python API](https://review.learn.microsoft.com/help/onboard/admin/reference/python/documenting-api?branch=main).

- This article follows the instructions provided in [Testing Python Content Locally](https://review.learn.microsoft.com/help/onboard/admin/reference/python/testing-locally?branch=main).

## Prerequisites

- Install [Python 3.6](https://www.python.org/downloads/) or later.
- Install [Sphinx](http://www.sphinx-doc.org/en/master/). Refer to the steps described below. It creates Python documentation from `reStructuredText`, and has facilities for the documentation of software projects in a range of languages. Its output (YML files) feds into the DocFX tool.
- Downloaded [DocFX](https://dotnet.github.io/docfx/). It generates static sites from markdown and code. Accepts as input the Sphinx output (YML files). It is recommended to extract it in C:\Program Files\docfx. If you create a PATH variable it is even better.

### Install Sphinx

```cmd
pip install -U Sphinx
```

Install an additional component to support the DocFX tool:

```cmd
pip install -U sphinx-docfx-yaml
```

### Check files for errors (optional)

Before creating Sphinx content you can check your files for any issues. Run `pip install rstcheck` from a command line to install [rstcheck](https://pypi.org/project/rstcheck/).

You can set a variety of [options](https://github.com/myint/rstcheck#options), like [ignoring specific errors](https://github.com/myint/rstcheck#ignore-specific-errors) or messages, when running rstcheck. To check a single file run, do the following:

1. Prepare your file by removing all code from the file. This ensure that `rstcheck` is recognizing issues solely with RST, not issues with the code itself. Make sure that all your left with is the classes/methods and docstrings containing RST reference comments. Here's an example:

    ```python
    class DialogSet:

        def add():
            """
            Adds a new dialog to the set and returns the added dialog.
            
            :param dialog: The dialog to add.
            """
            return
            
        async def find():
            """
            Finds a dialog that was previously added to the set using meth:`add()`
            
            :param dialog_id: ID of the dialog/prompt to look up.
            :return: The dialog if found, otherwise null.
            """
            return
    ```

    Adding `return` at the end of each method reduces the likelihood of running into an EOL error.

1. Run the following from the command line:

    ```cmd
    rstcheck <file_name>
    ```

## Organize your code directory

We suggest to perform the steps below to facilitate the creation of a local reference build.

1. If possible, create a list of files that have errors or need to be updated. Having this list will greatly reduce the amount of time spent on the local build, and it will be used in later steps in the build process.
1. In your local repository clone the SDK library for which you want to create a local documentation build. For example for Python, clone `https://github.com/microsoft/botbuilder-python`.
1. Create a folder named `<local path>\APIReference` folder.
1. In the folder, create a sub-folder named `libraries`.
1. From your cloned SDK copy into the `libraries` the folders that contain the actual code (with related sub-folders). For examples from `\botbuilder-python\libraries\botbuilder-core\botbuilder\core` copy the `core` folder. From `botbuilder-python\libraries\botbuilder-dialogs\botbuilder\dialogs` copy the `dialogs` folder. The following is a an example of how the library directory looks like:

    ![sphinx libraries dir structure](../media/sphinx-libraries.PNG)

1. Once done copying, make sure that each library folder (and sub-folders) contain an `__init__.py` file. These **files must be empty**; delete whatever code they contain.
1. Optionally, remove all classes that don't have reference comment errors or don't need to be checked.
1. This next step is time intensive. To avoid errors, delete the actual code in the .py code files and just leave the comments. Make sure that all your left with is the classes/methods and docstrings containing RST reference comments. You can also delete classes/methods without comments if you are solely testing for errors.

## Create Sphinx content

The following steps produce a local doc build structure. `.rst` files that contain the info about packages and modules, and finally a set of `YML` files from the source code.

1. Switch to the reference directory on your local machine where to create the documentation: `<local path>\APIReference`.
1. Open a terminal console in this directory and set the Sphinx base configuration by executing this command:

    ```cmd
    sphinx-quickstart
    ```

    > [!NOTE]
    > If you get this sphinx error: `could not import extension docfx_yaml.extension` you must (re)install **Sphinx DocFX YAML** which is an exporter for the Sphinx Autodoc module into DocFX YAML. Execute the command: `pip install sphinx-docfx-yaml`. For more information, see [Sphinx DocFX YAML](https://github.com/docascode/sphinx-docfx-yaml).

1. You will be asked some questions. Answer as follows:

    1. Root path accept the current directory: Accept default (simply enter).
    1. Separate source and build directories (y/n) [n]: y.
    1. Accept the default hyphen prefix for the other directories to be created (simply enter).
    1. Project name: `API Reference` or whatever name you decide.
    1. Author name(s): your alias
    1. Project version: 1.0
    1. Project release: 1.0.0
    1. For all the requested values accept the defaults by just clicking enter.

    After done entering the above values the following directory structure is created in the source folder.

    ![sphinx dir structure](../media/sphinx-dir-structure.PNG)

1. Open the `conf.py` file and add the following directive:

    ```python
    extensions = ['sphinx.ext.autodoc', 'docfx_yaml.extension']
    ```

1. Add the following path information:

    ```python
    import os
    import sys
    sys.path.insert(0, os.path.abspath('../libraries'))
    ```

1. Create a number of  `.rst` files in the `source` folder representing of the APIs to document.

    ```cmd
    sphinx-apidoc -f .\libraries  -o source
    ```

1. Edit the `index.rst` file by adding the `modules` line as shown below. Make sure the alignment is correct

    ![sphinx dir structure](../media/sphinx-index-rst.PNG)

<!--
sphinx-apidoc <path to folder where the .py files are> -o . --module-first --no-headings --no-toc --implicit-namespaces
-->

1. Create the `YML` files in the `build/docfx_yaml` folder by executing the command:

    ```cmd
    sphinx-build source build
    ```

    Once the build completes, you should have the `YML` files in `build/docfx_yaml` folder.

    > [!NOTE
    > If encounter this error: `contents.rst not found`, add the following entry to the `config.py` file: `master_doc = 'index'`. See also [Sphinx error: master file [..]/checkouts/latest/contents.rst not found #2569](https://github.com/readthedocs/readthedocs.org/issues/2569).

## Documentation preview

Now that we have the `YML` files, we can preview them with a locally-running `DocFX` instance.

1. Bootstrap a documentation project based on our own pipeline. In the `APIReference` folder bootstrap a new DocFX project by executing the command:

    ```cmd
    "<path to DocFX folder>\docfx.exe" init -q
    ```

    This creates a new `docfx_project` folder.

1. Copy the `YML` files previously generated via Sphinx in `build/docfx_yaml`. into the `docfx_project/api` folder.
1. Once done, make sure that your terminal console is open in the `docfx_project` folder.
1. Build the site (on line docs) locally and display the documentation by running the command:

    ```cmd
    "<path to DocFX folder>\docfx.exe" --serve
    ```

1. In your browser, navigate to http://localhost:8080 to see the online docs. The output should look similar to this:

    ![DocFX local build](../media/docfx-local-build.png)

## Some shortcuts

When you rebuild the documentation because comments have changed or for whatever other reasons, you do not have to start from scratch. Instead, perform the steps described below.

1. In the `source` folder, delete the `.rst` files. **Do not delete the `index.rst` file**. Perform this step only if you have changed the library content.
1. Delete the content of the `build` folder.
1. Delete the content of the `docfx_project/api` folder. **Do not delete the `index.md` file**.
1. In the directory `<local path>\APIReference` execute the commands:

    ```cmd
    sphinx-apidoc -f .\libraries  -o source
    sphinx-build source build
    ```

1. Perform a preliminary test using the HTML output files. Navigate to the `build` directory and click on the `index.html` file. YOu should be able to navigate to the documentation file you are analyzing and check what kind of errors it contains. This should give you a clue on how to fix them.

    ![sphinx local build html](../media/sphinx-index-html.PNG)

1. The following picture shows an example of errors.

    ![sphinx doc errors](../media/sphinx-errors.PNG)

    **Fix the errors** and **redo the previous steps**. Then perform the steps below, to see how the documentation looks in the actual build.
1. Copy the `YML` files previously generated in `build/docfx_yaml`. into the `docfx_project/api` folder.
1. Once done, make sure that your terminal console is open in the `docfx_project` folder.
1. Build the site (on line docs) locally and display the documentation by running the command:

    ```cmd
    "<path to DocFX folder>\docfx.exe" --serve
    ```

    The following is an example of the local build:

    ![docfx local build](../media/sphinx-docfx.png)
