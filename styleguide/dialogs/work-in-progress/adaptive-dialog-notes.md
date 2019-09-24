# <a id="top"></a>Changes in dialogs: botbuilder-dotnet (4.6-preview)

Apparent changes:

- Dialog events and event bubbling
- Tags
- Bindings and skills
- Dialog containers
- Adaptive dialogs

Namespaces explored:
- [Microsoft.Bot.Builder.Dialogs](updated-microsoftbotbuilderdialogs)
- [Microsoft.Bot.Builder.Dialogs.Adaptive](new-microsoftbotbuilderdialogsadaptive)

## <a id="ns-dialogs"></a>[updated] Microsoft.Bot.Builder.Dialogs

### <a id="DialogManagerAdapter"></a>class **DialogManagerAdapter** : BotAdapter

<details><summary>Public and protected members</summary>

```csharp
public DialogManagerAdapter() { }

public readonly List<Activity> Activities = new List<Activity>();

public override Task<ResourceResponse[]> SendActivitiesAsync(ITurnContext turnContext, Activity[] activities, CancellationToken cancellationToken) {…}

// Both of these throw a NotImplementedException.
public override Task<ResourceResponse> UpdateActivityAsync(ITurnContext turnContext, Activity activity, CancellationToken cancellationToken) {…}
public override Task DeleteActivityAsync(ITurnContext turnContext, ConversationReference reference, CancellationToken cancellationToken) {…}
```

</details>

- This is a transient, internal adapter used by the **[DialogManager](#DialogManager).RunAsync** method.
- This throws for the _update_ and _delete_ activity operations.

back to [top](#top)

### <a id="IDialog"></a>[updated] public interface **IDialog**

<details><summary>Public and protected members</summary>

```csharp
string Id { get; set; }
IBotTelemetryClient TelemetryClient { get; set; }
List<string> Tags { get; }
Dictionary<string, string> InputBindings { get; }
string OutputBinding { get; }

Task<DialogTurnResult> BeginDialogAsync(DialogContext dc, object options = null, CancellationToken cancellationToken = default(CancellationToken));
Task<DialogTurnResult> ContinueDialogAsync(DialogContext dc, CancellationToken cancellationToken = default(CancellationToken));
Task<DialogTurnResult> ResumeDialogAsync(DialogContext dc, DialogReason reason, object result = null, CancellationToken cancellationToken = default(CancellationToken));
Task RepromptDialogAsync(ITurnContext turnContext, DialogInstance instance, CancellationToken cancellationToken = default(CancellationToken));
Task EndDialogAsync(ITurnContext turnContext, DialogInstance instance, DialogReason reason, CancellationToken cancellationToken = default(CancellationToken));

Task<bool> OnDialogEventAsync(DialogContext dc, DialogEvent e, CancellationToken cancellationToken);
```

</details>

back to [top](#top)

### <a id="Dialog"></a>[updated] public abstract class **Dialog** : IDialog

<details><summary>Public and protected members</summary>

```csharp
public static readonly DialogTurnResult EndOfTurn = new DialogTurnResult(DialogTurnStatus.Waiting);

private string id;
private IBotTelemetryClient _telemetryClient;

public Dialog(string dialogId = null) {…}

public string Id {get {…} set {…}}
public virtual IBotTelemetryClient TelemetryClient {get {…} set {…}}
public List<string> Tags { get; private set; } = new List<string>();
public Dictionary<string, string> InputBindings { get; set; } = new Dictionary<string, string>();
public string OutputBinding { get; set; }

public abstract Task<DialogTurnResult> BeginDialogAsync(DialogContext dc, object options = null, CancellationToken cancellationToken = default(CancellationToken));
public virtual async Task<DialogTurnResult> ContinueDialogAsync(DialogContext dc, CancellationToken cancellationToken = default(CancellationToken)) {…}
public virtual async Task<DialogTurnResult> ResumeDialogAsync(DialogContext dc, DialogReason reason, object result = null, CancellationToken cancellationToken = default(CancellationToken)) {…}
public virtual Task RepromptDialogAsync(ITurnContext turnContext, DialogInstance instance, CancellationToken cancellationToken = default(CancellationToken)) {…}
public virtual Task EndDialogAsync(ITurnContext turnContext, DialogInstance instance, DialogReason reason, CancellationToken cancellationToken = default(CancellationToken)) {…}

public virtual async Task<bool> OnDialogEventAsync(DialogContext dc, DialogEvent e, CancellationToken cancellationToken) {…}
protected virtual Task<bool> OnPreBubbleEvent(DialogContext dc, DialogEvent e, CancellationToken cancellationToken) {…}
protected virtual Task<bool> OnPostBubbleEvent(DialogContext dc, DialogEvent e, CancellationToken cancellationToken) {…}

protected virtual string OnComputeId() {…}
protected virtual string BindingPath() {…}

protected void RegisterSourceLocation(string path, int lineNumber) {…}
```

</details>

back to [top](#top)

### <a id="DialogContainer"></a>[new] public abstract class **DialogContainer** : Dialog

<details><summary>Public and protected members</summary>

```csharp
protected readonly DialogSet _dialogs = new DialogSet();

public DialogContainer(string dialogId = null) : base(dialogId) {…}

public abstract DialogContext CreateChildContext(DialogContext dc);

public virtual Dialog AddDialog(IDialog dialog) {…}
public IDialog FindDialog(string dialogId) {…}
```

</details>

**DialogContainer** is now a common base class for [component](#ComponentDialog) and [adaptive](#AdaptiveDialog) dialogs.

- It defines an inner dialog set.
- I'm not sure why it doesn't override the **[Dialog](#Dialog).TelemetryClient** setter to apply the telemetry client to its child dialogs. The implementations in **ComponentDialog** and **AdaptiveDialog**, are slightly differently but logically identical.

back to [top](#top)

### <a id="ComponentDialog"></a>[updated] public class **ComponentDialog** : DialogContainer

<details><summary>Public and protected members</summary>

```csharp
public const string PersistedDialogState = "dialogs";

public ComponentDialog(string dialogId = null) : base(dialogId) {…}

public string InitialDialogId { get; set; }

public new IBotTelemetryClient TelemetryClient { get {…} set {…} }

public override async Task<DialogTurnResult> BeginDialogAsync(DialogContext outerDc, object options = null, CancellationToken cancellationToken = default(CancellationToken)) {…}
public override async Task<DialogTurnResult> ContinueDialogAsync(DialogContext outerDc, CancellationToken cancellationToken = default(CancellationToken)) {…}
public override async Task<DialogTurnResult> ResumeDialogAsync(DialogContext outerDc, DialogReason reason, object result = null, CancellationToken cancellationToken = default(CancellationToken)) {…}
public override async Task RepromptDialogAsync(ITurnContext turnContext, DialogInstance instance, CancellationToken cancellationToken = default(CancellationToken)) {…}
public override async Task EndDialogAsync(ITurnContext turnContext, DialogInstance instance, DialogReason reason, CancellationToken cancellationToken = default(CancellationToken)) {…}

protected async Task EnsureInitialized(DialogContext outerDc) {…}

public override Dialog AddDialog(IDialog dialog) {…}
public IDialog FindDialog(string dialogId) {…}

public override DialogContext CreateChildContext(DialogContext dc) {…}

protected virtual Task OnInitialize(DialogContext dc) {…}

protected virtual Task<DialogTurnResult> OnBeginDialogAsync(DialogContext innerDc, object options, CancellationToken cancellationToken = default(CancellationToken)) {…}
protected virtual Task<DialogTurnResult> OnContinueDialogAsync(DialogContext innerDc, CancellationToken cancellationToken = default(CancellationToken)) {…}
protected virtual Task OnEndDialogAsync(ITurnContext context, DialogInstance instance, DialogReason reason, CancellationToken cancellationToken = default(CancellationToken)) {…}
protected virtual Task OnRepromptDialogAsync(ITurnContext turnContext, DialogInstance instance, CancellationToken cancellationToken = default(CancellationToken)) {…}

protected virtual Task<DialogTurnResult> EndComponentAsync(DialogContext outerDc, object result, CancellationToken cancellationToken) {…}

protected override string OnComputeId() {…}
```

</details>

back to [top](#top)

### <a id="DialogContext"></a>[updated] public class **DialogContext**

<details><summary>Public and protected members</summary>

```csharp
private List<string> activeTags = new List<string>();

public DialogContext(DialogSet dialogs, DialogContext parentDialogContext, DialogState state, IDictionary<string, object> conversationState = null, IDictionary<string, object> userState = null, IDictionary<string, object> settings = null) {…}
public DialogContext(DialogSet dialogs, ITurnContext turnContext, DialogState state, IDictionary<string, object> conversationState = null, IDictionary<string, object> userState = null, IDictionary<string, object> settings = null) {…}

public DialogContext Parent { get; set; }
public DialogSet Dialogs { get; private set; }
public ITurnContext Context { get; private set; }
public IList<DialogInstance> Stack { get; private set; }
public DialogContextState State { get; private set; }
public DialogContext Child { get {…} }
public DialogInstance ActiveDialog { get {…} }
public List<string> ActiveTags { get {…} }
public Dictionary<string, object> DialogState { get {…} }

public async Task<DialogTurnResult> BeginDialogAsync(string dialogId, object options = null, CancellationToken cancellationToken = default(CancellationToken)) {…}
public async Task<DialogTurnResult> PromptAsync(string dialogId, PromptOptions options, CancellationToken cancellationToken = default(CancellationToken)) {…}
public async Task<DialogTurnResult> ContinueDialogAsync(CancellationToken cancellationToken = default(CancellationToken)) {…}
public async Task<DialogTurnResult> EndDialogAsync(object result = null, CancellationToken cancellationToken = default(CancellationToken)) {…}
public async Task<DialogTurnResult> CancelAllDialogsAsync(string eventName = DialogEvents.CancelDialog, object eventValue = null, CancellationToken cancellationToken = default(CancellationToken)) {…}
public async Task<DialogTurnResult> ReplaceDialogAsync(string dialogId, object options = null, CancellationToken cancellationToken = default(CancellationToken)) {…}
public async Task RepromptDialogAsync(CancellationToken cancellationToken = default(CancellationToken)) {…}

public IDialog FindDialog(string dialogId) {…}
public async Task<bool> EmitEventAsync(string name, object value = null, bool bubble = true, bool fromLeaf = false, CancellationToken cancellationToken = default(CancellationToken)) {…}
protected virtual bool ShouldInheritState(IDialog dialog) {…}
private async Task EndActiveDialogAsync(DialogReason reason, object result = null, CancellationToken cancellationToken = default(CancellationToken)) {…}

public class DialogEvents
{
    public const string BeginDialog = "beginDialog";
    public const string ResumeDialog = "resumeDialog";
    public const string RepromptDialog = "repromptDialog";
    public const string CancelDialog = "cancelDialog";
    public const string EndDialog = "endDialog";
    public const string ActivityReceived = "activityReceived";
}
```

</details>

back to [top](#top)

### <a id="DialogState"></a>[updated] public class **DialogState**

<details><summary>Public and protected members</summary>

```csharp
public DialogState() : this(null) { }
public DialogState(IList<DialogInstance> stack) {…}

public IList<DialogInstance> DialogStack { get; set; } = new List<DialogInstance>();
public IDictionary<string, object> ConversationState { get; set; } = new Dictionary<string, object>();
public IDictionary<string, object> UserState { get; set; } = new Dictionary<string, object>();
```

</details>

- The additon of the **ConversationState** and **UserState** properties looks ugly. I'll try to get them to change the names so they don't actively collide with the state management classes of the same name.

### <a id="DialogEvent"></a>[new] public class **DialogEvent**

<details><summary>Public and protected members</summary>

```csharp
public bool Bubble { get; set; }  // Whether to propagate events to parent contexts.
public string Name { get; set; }  // Event name.
public object Value { get; set; } // Optional. Event value.
```

</details>

### <a id="DialogManager"></a>public class **DialogManager**

<details><summary>Public and protected members</summary>

```csharp
public DialogManager(IDialog rootDialog = null) {…}

public IDialog RootDialog { get {…} set {…} }

public async Task<DialogManagerResult> RunAsync(Activity activity, StoredBotState state = null) {…}
public async Task<DialogManagerResult> OnTurnAsync(ITurnContext context, StoredBotState storedState = null, CancellationToken cancellationToken = default(CancellationToken)) {…}

private static async Task<StoredBotState> LoadBotState(IStorage storage, BotStateStorageKeys keys) {…}
private static async Task SaveBotState(IStorage storage, StoredBotState newState, BotStateStorageKeys keys) {…}

private static BotStateStorageKeys ComputeKeys(ITurnContext context) {…}
```

</details>

This appears to take over from the **DialogExtensions.RunAsync** extension method.

- In **OnTurnAsync**, the dialog manager creates a **DialogContext** for the turn.
- Since state is saved in the turn context, and the dialog context wraps the turn context, this makes a certain amount of sense as a transitory construct.

back to [top](#top)

## <a id="ns-dialogs-adaptive"></a>[new] Microsoft.Bot.Builder.Dialogs.Adaptive

### <a id="AdaptiveDialog"></a>public class **AdaptiveDialog** : DialogContainer

<details><summary>Public and protected members</summary>

```csharp
public IStatePropertyAccessor<BotState> BotState { get; set; }
public IStatePropertyAccessor<Dictionary<string, object>> UserState { get; set; }

public IRecognizer Recognizer { get; set; }
public ILanguageGenerator Generator { get; set; }

public List<IDialog> Steps { get; set; } = new List<IDialog>();
public virtual List<IRule> Rules { get; set; } = new List<IRule>();
public bool AutoEndDialog { get; set; } = true;
public IRuleSelector Selector { get; set; }

public string DefaultResultProperty { get; set; } = "dialog.result";

public override IBotTelemetryClient TelemetryClient { get {…} set {…} }

public AdaptiveDialog(string dialogId = null, [CallerFilePath] string callerPath = "", [CallerLineNumber] int callerLine = 0) : base(dialogId) {…}

public override async Task<DialogTurnResult> BeginDialogAsync(DialogContext dc, object options = null, CancellationToken cancellationToken = default(CancellationToken)) {…}
public override async Task<DialogTurnResult> ContinueDialogAsync(DialogContext dc, CancellationToken cancellationToken = default(CancellationToken)) {…}
public override async Task<DialogTurnResult> ResumeDialogAsync(DialogContext dc, DialogReason reason, object result = null, CancellationToken cancellationToken = default(CancellationToken)) {…}
public override async Task RepromptDialogAsync(ITurnContext turnContext, DialogInstance instance, CancellationToken cancellationToken = default(CancellationToken)) {…}

protected override async Task<bool> OnPreBubbleEvent(DialogContext dc, DialogEvent dialogEvent, CancellationToken cancellationToken = default(CancellationToken)) {…}
protected override async Task<bool> OnPostBubbleEvent(DialogContext dc, DialogEvent dialogEvent, CancellationToken cancellationToken = default(CancellationToken)) {…}

protected async Task<bool> ProcessEventAsync(SequenceContext sequenceContext, DialogEvent dialogEvent, bool preBubble, CancellationToken cancellationToken = default(CancellationToken)) {…}

public void AddRule(IRule rule) {…}
public void AddRules(IEnumerable<IRule> rules) {…}

public void AddDialogs(IEnumerable<IDialog> dialogs) {…}

protected override string OnComputeId() {…}
private string GetUniqueInstanceId(DialogContext dc) {…}

public override DialogContext CreateChildContext(DialogContext dc) {…}

protected async Task<DialogTurnResult> ContinueStepsAsync(DialogContext dc, object options, CancellationToken cancellationToken) {…}
protected async Task<bool> EndCurrentStepAsync(SequenceContext sequenceContext, CancellationToken cancellationToken = default(CancellationToken)) {…}
protected async Task<DialogTurnResult> OnEndOfStepsAsync(SequenceContext sequenceContext, CancellationToken cancellationToken = default(CancellationToken)) {…}
protected async Task<RecognizerResult> OnRecognize(SequenceContext sequenceContext, CancellationToken cancellationToken = default(CancellationToken)) {…}
```

</details>

back to [top](#top)

### <a id="SequenceContext"></a>public class **SequenceContext** : DialogContext

<details><summary>Public and protected members</summary>

```csharp
public AdaptiveDialogState Plans { get; private set; }

public List<StepState> Steps { get; set; }

public List<StepChangeList> Changes { get {…} private set {…} }

public SequenceContext(DialogSet dialogs, DialogContext dc, DialogState state, List<StepState> steps, string changeKey, DialogSet stepDialogs)
    : base(dialogs, dc.Context, state, conversationState: dc.State.Conversation, userState: dc.State.User, settings: dc.State.Settings) {…}

public void QueueChanges(StepChangeList changes) {…}

public async Task<bool> ApplyChangesAsync(CancellationToken cancellationToken = default(CancellationToken)) {…}

public SequenceContext InsertSteps(List<StepState> steps) {…}
public SequenceContext InsertStepsBeforeTags(List<string> tags, List<StepState> steps) {…}
public SequenceContext AppendSteps(List<StepState> steps) {…}

public SequenceContext EndSequence(List<StepState> steps) {…}
public SequenceContext ReplaceSequence(List<StepState> steps) {…}

protected override bool ShouldInheritState(IDialog dialog) {…}
```

</details>

back to [top](#top)

### <a id="AdaptiveEvents"></a>public class **AdaptiveEvents** : DialogContext.DialogEvents

<details><summary>Public and protected members</summary>

```csharp
public const string RecognizedIntent = "recognizedIntent";
public const string UnknownIntent = "unknownIntent";
public const string SequenceStarted = "stepsStarted";
public const string SequenceEnded = "stepsEnded";
```

</details>

back to [top](#top)

### <a id="StepState"></a>public class **StepState** : DialogState

<details><summary>Public and protected members</summary>

```csharp
public AdaptiveDialogState() { }

public dynamic Options { get; set; }
public List<StepState> Steps { get; set; } = new List<StepState>();
public object Result { get; set; }
```

</details>

- Not sure if the use of `dynamic` here is a bug. The SDK has actively avoided the use of `dynamic` up to this point.

back to [top](#top)

### <a id="StepChangeTypes"></a>public enum **StepChangeTypes**

<details><summary>Public and protected members</summary>

```csharp
public enum StepChangeTypes
{
    InsertSteps,
    InsertStepsBeforeTags,
    AppendSteps,
    EndSequence,
    ReplaceSequence,
}
```

</details>

back to [top](#top)

### <a id="StepChangeList"></a>public class **StepChangeList**

<details><summary>Public and protected members</summary>

```csharp
public StepChangeTypes ChangeType { get; set; } = StepChangeTypes.InsertSteps;
public List<StepState> Steps { get; set; } = new List<StepState>();
public List<string> Tags { get; set; } = new List<string>();
public Dictionary<string, object> Turn { get; set; }
```

</details>

back to [top](#top)
