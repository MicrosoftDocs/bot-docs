<!-- Include under "Create a bot" header
bot-builder-tutorial-create-basic-bot.md and bot-builder-java-quickstart.md -->

1. From a terminal, navigate to the directory where you saved your bot, then execute the commands listed below. 

1. Build the Maven project and packages it into a *.jar* file (archive). 

    ```bash
    mvn package
    ```

1. Run the bot locally. Replace the *archive-name* with the actual name from the previous command. 

    ```bash
    java -jar .\target\<archive-name>.jar
    ```

You are now ready to start the Emulator.
