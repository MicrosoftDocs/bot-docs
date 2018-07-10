The Bot Builder Framework enables your bot to store and retrieve state data that is associated with a user, a conversation, or a specific user within the context of a specific conversation. 
State data can be used for many purposes, such as determining where the prior conversation left off or simply greeting a returning user by name. If you store a user's preferences, you can use that information to customize the conversation the next time you chat. For example, you might alert the user to a news article about a topic that interests them, or alert a user when an appointment becomes available. 

For testing and prototyping purposes, you can use the Bot Builder Framework's in-memory data storage. For production bots, you can implement your own storage adapter or use one of Azure Extensions. The Azure Extensions allow you to store your bot's state data in either Table Storage, CosmosDB, or SQL. This article will show you how to use the in-memory storage adapter to store your bot's state data. 

> [!IMPORTANT]
> The Bot Framework State Service API is not recommended for production environments, and may be deprecated in a future release. It is recommended that you update your bot code to use the in-memory storage adapter for testing purposes or use one of the **Azure Extensions** for production bots.
