<!-- Include under "Create a bot" header
bot-builder-tutorial-create-basic-bot.md and bot-builder-java-quickstart.md -->

### Create an echo bot

Run the following command to create an echo bot from templates. The command uses default options for its parameters. 

```bash
# Create an echo bot using the default options.
yo botbuilder-java -T "echo"
```

Yeoman prompts you for some information with which to create your bot. For this tutorial, use the default values.

```text
? What's the name of your bot? (echo)
? What's the fully qualified package name of your bot? (com.mycompany.echo)
? Which template would you like to start with? (Use arrow keys) Select "Echo Bot"
? Looking good.  Shall I go ahead and create your new bot? (Y/n) Enter "y"
```

The generator supports a number of command line options that can be used to change the generator's default options or to pre-seed a prompt.

| Command&nbsp;line&nbsp;Option  | Description |
| ------------------- | ----------- |
| --help, -h        | List help text for all supported command-line options |
| --botName, -N     | The name given to the bot project |
| --packageName, -P | The Java package name to use for the bot |
| --template, -T    | The template used to generate the project.  Options are `empty`, or `echo`.  See [https://github.com/Microsoft/BotBuilder-Samples/tree/master/generators/generator-botbuilder](https://github.com/Microsoft/BotBuilder-Samples/tree/master/generators/generator-botbuilder) for additional information regarding the different template option and their functional differences. |
| --noprompt        | The generator will not prompt for confirmation before creating a new bot.  Any requirement options not passed on the command line will use a reasonable default value.  This option is intended to enable automated bot generation for testing purposes. |

Thanks to the template, your project contains all the code that's necessary to create the bot in this quickstart. You don't need any additional code to test your bot.

> [!NOTE]
> If you create a `Core` bot, you'll need a LUIS language model. You can create a language model at [luis.ai](https://www.luis.ai). After creating the model, update the configuration file.
