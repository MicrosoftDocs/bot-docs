### Generate a C# class for the model results

The `luis:generate:cs` command generates a strongly typed C# source code from a LUIS model (JSON).

Run the following command to create a .cs representation of your LUIS model:

```cli
bf luis:generate:cs -i <luis-model-file> -o <output-file-name> --className <class-name>
```

For additional information on using this command, see [bf luis:generate:cs][bf-luisgeneratecs] in the Bot Framework CLI LUIS README.

### Generate a TypeScript type for the model results

The `luis:generate:ts` command generates a strongly typed TypeScript source code from a LUIS model (JSON).

Run the following command to create a .ts representation of your LUIS model:

```cli
bf luis:generate:ts -i <luis-model-file> -o <output-file-name> --className <class-name>
```

For additional information on using this command, see [bf luis:generate:ts][bf-luisgeneratets] in the BF CLI LUIS README.

[bf-luisgeneratecs]: https://aka.ms/botframework-cli-luis#bf-luisgeneratecs
[bf-luisgeneratets]: https://aka.ms/botframework-cli-luis#bf-luisgeneratets
