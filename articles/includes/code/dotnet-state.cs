// <getStateClient1>
StateClient stateClient = activity.GetStateClient();
// </getStateClient1>


// <getStateClient2>
StateClient stateClient = new StateClient(new MicrosoftAppCredentials(microsoftAppId, microsoftAppPassword));
// </getStateClient2>


// <getProperty1>
BotData userData = await stateClient.BotState.GetUserDataAsync(activity.ChannelId, activity.From.Id);
var sentGreeting = userData.GetProperty<bool>("SentGreeting");
// </getProperty1>


// <getProperty2>
MyCustomType myUserData = new MyCustomType();
BotData botData = await botState.GetUserDataAsync(activity.ChannelId, activity.From.Id);
myUserData = botData.GetProperty<MyCustomType>("UserData");
// </getProperty2>


// <setProperty1>
BotData userData = await stateClient.BotState.GetUserDataAsync(activity.ChannelId, activity.From.Id);
userData.SetProperty<bool>("SentGreeting", true);
await stateClient.BotState.SetUserDataAsync(activity.ChannelId, activity.From.Id, userData);
// </setProperty1>


// <setProperty2>
BotData userData = await stateClient.BotState.GetUserDataAsync(activity.ChannelId, activity.From.Id);
userData.SetProperty<MyCustomType>("UserData", myUserData);
await stateClient.BotState.SetUserDataAsync(activity.ChannelId, activity.From.Id, userData);
// </setProperty2>


// <handleException>
var builder = new ContainerBuilder();
builder
    .Register(c => new CachingBotDataStore(c.Resolve<ConnectorStore>(), CachingBotDataStoreConsistencyPolicy.LastWriteWins))
    .As<IBotDataStore<BotData>>()
    .AsSelf()
    .InstancePerLifetimeScope();
builder.Update(Conversation.Container);
// </handleException>