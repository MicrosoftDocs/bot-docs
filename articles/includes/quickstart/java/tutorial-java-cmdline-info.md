<!-- Include under "Prerequisites" header after the "quickstart-java-create-bot.md" file include. in the file:
bot-builder-tutorial-create-basic-bot.md -->

### Using command line options

This example shows how to pass command-line options to the generator, by setting the default package to a custom one and the default template to Core. 

```bash
yo botbuilder-java --P "<your custom package>" --T "core"
```

### Using --noprompt mode

The generator can be run in `--noprompt` mode, which can be used for automated bot creation.  When run in `--noprompt` mode, the generator can be configured using command line options as documented above.  If a command line option is ommitted a reasonable default will be used.  In addition, passing the `--noprompt` option will cause the generator to create a new bot project without prompting for confirmation before generating the bot.

#### Default Options

| Command&nbsp;line&nbsp;Option  | Default Value |
| ------------------- | ----------- |
| --botname, -N     | `echo` |
| --packageName, -p | `echo` |
| --template, -T    | `echo` |

This example shows how to run the generator in --noprompt mode, by setting all required options on the command line.

```bash
yo botbuilder-java --noprompt -N "<your bot name>" -P "<your custom package>" -T "echo"
```
