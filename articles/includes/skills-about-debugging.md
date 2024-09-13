Since traffic between skills and skill consumers is authenticated, there are extra steps when debugging such bots.

- The skill consumer and all the skills it consumes, directly or indirectly, must be running.
- If the bots are running locally and if any of the bots has an app ID and password, then all bots must have valid IDs and passwords.
- If the bots are all deployed, see how to [Debug a bot from any channel using devtunnel](../bot-service-debug-channel-devtunnel.md).
- If some of the bots are running locally and some are deployed, then see how to [Debug a skill or skill consumer](../v4sdk/skills-debug-skill-or-consumer.md).

Otherwise, you can debug a skill consumer or skill much like you debug other bots. For more information, see [Debugging a bot](../bot-service-debug-bot.md) and [Debug with the Bot Framework Emulator](../bot-service-debug-emulator.md).
