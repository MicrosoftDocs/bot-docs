// <useSchema>
public static IForm<JObject> BuildJsonForm()
{
    using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Microsoft.Bot.Sample.AnnotatedSandwichBot.AnnotatedSandwich.json"))
    {
        var schema = JObject.Parse(new StreamReader(stream).ReadToEnd());
        return new FormBuilderJson(schema)
            .AddRemainingFields()
            .Build();
    }
    ...
}
// </useSchema>