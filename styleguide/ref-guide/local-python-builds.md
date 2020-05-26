# Python local reference documentation build

This article describes how to create a local build for Python API reference documentation.

The API reference documentation is generated from comments in the source code and created using the [reStructuredText](https://en.wikipedia.org/wiki/ReStructuredText) format.

The **reStructuredText** (RST) is a file format for textual data used primarily in the Python programming language community for technical documentation.
It is a lightweight markup language designed to be both processable by documentation-processing software, and easily readable by humans.

For the RST syntax, see [How to document a Python API](https://review.docs.microsoft.com/en-us/help/onboard/admin/reference/python/documenting-api?branch=master)

This article follows the instructions provided in [Testing Python Content Locally](https://review.docs.microsoft.com/en-us/help/onboard/admin/reference/python/testing-locally?branch=master).

## Prerequisites

- Install [Python 3.6](https://www.python.org/downloads/) or higher
- Install [Sphinx](http://www.sphinx-doc.org/en/master/). Creates Python documentation from `reStructuredText`, and has facilities for the documentation of software projects in a range of languages. Its output (YML files) feds into the DocFX tool.
- Downloaded [DocFX](https://dotnet.github.io/docfx/). Generates static sites from markdown and code. Accepts as input the Sphinx output (YML files). It is recommended to extract it in C:\Program Files\docfx. If you create a PATH variable it is even better.

    ```cmd
    pip install -U Sphinx
    ```

- Install an additional component:

    ```cmd
    pip install -U sphinx-docfx-yaml
    ```

### Check files for errors (optional)

Before creating Sphinx content you can check your files for any issues. Run `pip install rstcheck` from a command line to install [rstcheck](https://pypi.org/project/rstcheck/).

You can set a variety of [options](https://github.com/myint/rstcheck#options), like [ignoring specific errors](https://github.com/myint/rstcheck#ignore-specific-errors) or messages, when running rstcheck. To check a single file run:

```cmd
rstcheck <file_name>
```

## Create Sphinx content

The following steps produce a local doc build structure. .rst files that must c and finally a set of YAML files from the source code.

1. Create a new directory on your local machine where to create the documentation for example: `<local path>\Reference`.
1. Open a terminal console in this directory and set the Sphinx base configuration by executing this command:

    ```cmd
    sphinx-quickstart
    ```

1. You will be asked some questions. Answer as follows:

    1. Root path accept the current directory: Accept default (simply enter).
    1. Separate source and build directories (y/n) [n]: Accept default (simply enter).
    Accept the default hyphen prefix for the other directories to be created (simply enter).

	1. Project name: botbuilder-python
	1. Author name(s): your alias
	1. Project version: 1.0
	1. Project release: 1.0.0
	1. For all the requested values accept the defaults by just clicking enter.

    After done entering the above values the following directory structure is created:

    ![sphinx dir structure](../media/sphinx-dir-structure.PNG)

1. Open the `conf.py` file and add the following directive:

    `extensions = ['sphinx.ext.autodoc', 'docfx_yaml.extension']`

1. Create a number of  `.rst` files representing of the APIs to document.

    ```cmd
    sphinx-apidoc <path to folder where the .py files are> -o . --module-first --no-headings --no-toc --implicit-namespaces
    ```

1. Create the YAML files in the `_build/docfx_yaml` folder by executing this command:

    ```cmd
    sphinx-build . _build
	```

    Once the build completes, you should have the YAML files in `_build/docfx_yaml`.


## Documentation preview

Now that we have the YAML files, we can preview them with a locally-running DocFX instance.

1. Copy the path of the `docfx.exe`.
1. Bootstrap a documentation project based on our own pipeline. In the current folder, create a new folder, for example _docfx. In the console terminal, navigate to this folder and bootstrap a new DocFX project by executing this command:

    ```cmd
    "<path to DocFX folder>\docfx.exe" init -q
    ```
This creates a new `docfx_project` folder.

1. Copy the YAML files previously generated via Sphinx in `_build/docfx_yaml`. into the `docfx_project/api` folder.
1. Once done, make sure that your terminal console is open in the `docfx_project` folder.
1. Build the site (on line docs) by running this command:

    ```cmd
    "<path to DocFX folder>\docfx.exe"
    ```

1. Display the documentation the site as follows:

    ```cmd
    "<path to DocFX folder>\docfx.exe" serve _site
    ```

1. In yout browser, navigate to http://localhost:8080 to see the online docs.
