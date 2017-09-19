// <classDefinition>
[LuisModel("c413b2ef-382c-45bd-8ff0-f76d60e2a821", "6d0966209c6e4f6b835ce34492f3e6d9", domain: "westus2.api.cognitive.microsoft.com")]
[Serializable]
public class SimpleNoteDialog : LuisDialog<object>
{
    ...
}
// </classDefinition>

// <classDefinitionNotes>
    [LuisModel("<YOUR_LUIS_APP_ID>", "YOUR_SUBSCRIPTION_KEY", domain: "westus.api.cognitive.microsoft.com")]
    [Serializable]
    public class SimpleNoteDialog : LuisDialog<object>
    {
        // ...
    }

// </classDefinitionNotes>

// <classDefinitionLogFalse>
[LuisModel("<YOUR_LUIS_APP_ID>", "YOUR_SUBSCRIPTION_KEY", Log = false)]
[Serializable]
public class SimpleNoteDialog : LuisDialog<object>
{
    // ...
}
// </classDefinitionLogFalse>

// <turnOffAlarmHandler>
[LuisIntent("builtin.intent.alarm.turn_off_alarm")]
public async Task TurnOffAlarm(IDialogContext context, LuisResult result)
{
    if (TryFindAlarm(result, out this.turnOff))
    {
        PromptDialog.Confirm(context, AfterConfirming_TurnOffAlarm, "Are you sure?", promptStyle: PromptStyle.None);
    }
    else
    {
        await context.PostAsync("did not find alarm");
        context.Wait(MessageReceived);
    }
}
// </turnOffAlarmHandler>

// <deleteNoteHandler>
[LuisIntent("Note.Delete")]
public async Task DeleteNote(IDialogContext context, LuisResult result)
{
    Note note;
    if (TryFindNote(result, out note))
    {
        this.noteByTitle.Remove(note.Title);
        await context.PostAsync($"Note {note.Title} deleted");
    }
    else
    {                             
        // Prompt the user for a note title
        PromptDialog.Text(context, After_DeleteTitlePrompt, "What is the title of the note you want to delete?");                         
    }
}
// </deleteNoteHandler>

// <tryFindNote>
        public bool TryFindNote(LuisResult result, out Note note)
        {
            note = null;

            string titleToFind;

            EntityRecommendation title;
            if (result.TryFindEntity(Entity_Note_Title, out title))
            {
                return this.noteByTitle.TryGetValue(title.Entity, out note); // TryGetValue returns false if no match is found.
            }
            else
            {
                return false;
            }
        }
// </tryFindNote>

// <fullNotesExample>
using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Bot.Builder.Luis.Models;

namespace NotesBot.Dialogs
{

    [LuisModel("<YOUR_LUIS_APP_ID>", "YOUR_SUBSCRIPTION_KEY", domain: "westus.api.cognitive.microsoft.com")]
    [Serializable]
    public class SimpleNoteDialog : LuisDialog<object>
    {
        // Store notes in a dictionary that uses the title as a key
        private readonly Dictionary<string, Note> noteByTitle = new Dictionary<string, Note>();

        // Default note title
        public const string DefaultNoteTitle = "default";

        // Name of note title entity
        public const string Entity_Note_Title = "Note.Title";

        /// <summary>
        /// This method overload inspects the result from LUIS to see if a title entity was detected, and finds the note with that title, or the note with the default title, if no title entity was found.
        /// </summary>
        /// <param name="result">The result from LUIS that contains intents and entities that LUIS recognized.</param>
        /// <param name="note">This parameter returns any note that is found in the list of notes that has a matching title.</param>
        /// <returns>true if a note was found, otherwise false</returns>
        public bool TryFindNote(LuisResult result, out Note note)
        {
            note = null;

            string titleToFind;

            EntityRecommendation title;
            if (result.TryFindEntity(Entity_Note_Title, out title))
            {
                titleToFind = title.Entity;
            }
            else
            {
                titleToFind = DefaultNoteTitle;
            }

            return this.noteByTitle.TryGetValue(titleToFind, out note); // TryGetValue returns false if no match is found.
        }

        /// <summary>
        /// This method overload takes a string and finds the note with that title.
        /// </summary>
        /// <param name="noteTitle">A string containing the title of the note to search for.</param>
        /// <param name="note">This parameter returns any note that is found in the list of notes that has a matching title.</param>
        /// <returns>true if a note was found, otherwise false</returns>
        public bool TryFindNote(string noteTitle, out Note note)
        {
            bool foundNote = this.noteByTitle.TryGetValue(noteTitle, out note); // TryGetValue returns false if no match is found.
            return foundNote;
        }


        /// <summary>
        /// Send a generic help message if an intent without an intent handler is detected.
        /// </summary>
        /// <param name="context">Dialog context.</param>
        /// <param name="result">The result from LUIS.</param>
        [LuisIntent("")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            
            string message = $"I'm the Notes bot. I can understand requests to create, delete, and read notes. \n\n Detected intent: " + string.Join(", ", result.Intents.Select(i => i.Intent));
            await context.PostAsync(message);
            context.Wait(MessageReceived);
        }

        /// <summary>
        /// Handle the Note.Delete intent. If a title isn't detected in the LUIS result, prompt the user for a title.
        /// </summary>
        /// <param name="context">Dialog context.</param>
        /// <param name="result">The result from LUIS.</param>
        [LuisIntent("Note.Delete")]
        public async Task DeleteNote(IDialogContext context, LuisResult result)
        {
            Note note;
            if (TryFindNote(result, out note))
            {
                this.noteByTitle.Remove(note.Title);
                await context.PostAsync($"Note {note.Title} deleted");
            }
            else
            {                             
                // Prompt the user for a note title
                PromptDialog.Text(context, After_DeleteTitlePrompt, "What is the title of the note you want to delete?");                         
            }

        }

        // Try to delete note that the user specified in response to the prompt.
        private async Task After_DeleteTitlePrompt(IDialogContext context, IAwaitable<string> result)
        {
            Note note;
            string titleToDelete = await result;
            bool foundNote = this.noteByTitle.TryGetValue(titleToDelete, out note);

            if (foundNote)
            {
                this.noteByTitle.Remove(note.Title);
                await context.PostAsync($"Note {note.Title} deleted");
            }
            else
            {
                await context.PostAsync($"Did not find note named {titleToDelete}.");
            }

            context.Wait(MessageReceived);
        }

        /// <summary>
        /// Handles the Note.ReadAloud intent by displaying a note or notes. 
        /// If a title of an existing note is found in the LuisResult, that note is displayed. 
        /// If no title is detected in the LuisResult, all of the notes are displayed.
        /// </summary>
        /// <param name="context">Dialog context.</param>
        /// <param name="result">LUIS result.</param>
        [LuisIntent("Note.ReadAloud")]
        public async Task ReadNote(IDialogContext context, LuisResult result)
        {
            Note note;
            if (TryFindNote(result, out note))
            {
                await context.PostAsync($"**{note.Title}**: {note.Text}.");
            }
            else
            {
                // Print out all the notes if no specific note name was detected
                string NoteList = "Here's the list of all notes: \n\n";
                foreach (KeyValuePair<string, Note> entry in noteByTitle)
                {
                    Note noteInList = entry.Value;
                    NoteList += $"**{noteInList.Title}**: {noteInList.Text}.\n\n";
                }
                await context.PostAsync(NoteList);
            }

            context.Wait(MessageReceived);
        }

        private Note noteToCreate;
        private string currentTitle;

        /// <summary>
        /// Handles the Note.Create intent. Prompts the user for the note title if the title isn't detected in the LuisResult.
        /// </summary>
        /// <param name="context">Dialog context.</param>
        /// <param name="result">LUIS result.</param>
        [LuisIntent("Note.Create")]
        public Task CreateNote(IDialogContext context, LuisResult result)
        {
            EntityRecommendation title;
            if (!result.TryFindEntity(Entity_Note_Title, out title))
            {
                // Prompt the user for a note title
                PromptDialog.Text(context, After_TitlePrompt, "What is the title of the note you want to create?");
            }
            else
            {
                var note = new Note() { Title = title.Entity };
                noteToCreate = this.noteByTitle[note.Title] = note;

                // Prompt the user for what they want to say in the note           
                PromptDialog.Text(context, After_TextPrompt, "What do you want to say in your note?");
            }

            return Task.CompletedTask;
        }

        //  Creates a new note using the user's response to the prompt for a title
        private async Task After_TitlePrompt(IDialogContext context, IAwaitable<string> result)
        {
            EntityRecommendation title;

            // Set the title to the user's response
            currentTitle = await result;
            if (currentTitle != null)
            {
                title = new EntityRecommendation(type: Entity_Note_Title) { Entity = currentTitle };
            }
            else
            {
                // Use the default note title
                title = new EntityRecommendation(type: Entity_Note_Title) { Entity = DefaultNoteTitle };
            }

            // Create a new note object 
            var note = new Note() { Title = title.Entity };
            // Add the new note to the list of notes and also save it in order to add text to it later
            noteToCreate = this.noteByTitle[note.Title] = note;

            // Prompt the user for what they want to say in the note           
            PromptDialog.Text(context, After_TextPrompt, "What do you want to say in your note?");

        }

        //  Sets the text of a newly created note using the user's response to the prompt for the text of the note.
        private async Task After_TextPrompt(IDialogContext context, IAwaitable<string> result)
        {
            // Set the text of the note
            noteToCreate.Text = await result;
            
            await context.PostAsync($"Created note **{this.noteToCreate.Title}** that says \"{this.noteToCreate.Text}\".");
            
            context.Wait(MessageReceived);
        }


        public SimpleNoteDialog()
        {

        }

        public SimpleNoteDialog(ILuisService service)
            : base(service)
        {
        }

        [Serializable]
        public sealed class Note : IEquatable<Note>
        {

            public string Title { get; set; }
            public string Text { get; set; }

            public override string ToString()
            {
                return $"[{this.Title} : {this.Text}]";
            }

            public bool Equals(Note other)
            {
                return other != null
                    && this.Text == other.Text
                    && this.Title == other.Title;
            }

            public override bool Equals(object other)
            {
                return Equals(other as Note);
            }

            public override int GetHashCode()
            {
                return this.Title.GetHashCode();
            }
        }
    }


}
// </fullNotesExample>

// <MessagesControllerPost>
public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
{
    if (activity.Type == ActivityTypes.Message)
    {
        await Conversation.SendAsync(activity, () => new Dialogs.SimpleNoteDialog());
    }
    else
    {
        HandleSystemMessage(activity);
    }
    var response = Request.CreateResponse(HttpStatusCode.OK);
    return response;
}
// </MessagesControllerPost>

// <fullExample>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;

namespace Microsoft.Bot.Sample.SimpleAlarmBot
{
    // domain defaults to westus2.api.cognitive.microsoft.com if not provided
    [LuisModel("c413b2ef-382c-45bd-8ff0-f76d60e2a821", "6d0966209c6e4f6b835ce34492f3e6d9", domain: "westus2.api.cognitive.microsoft.com", staging: true)]
    [Serializable]
    public class SimpleAlarmDialog : LuisDialog<object>
    {
        private readonly Dictionary<string, Alarm> alarmByWhat = new Dictionary<string, Alarm>();

        public const string DefaultAlarmWhat = "default";

        public bool TryFindAlarm(LuisResult result, out Alarm alarm)
        {
            alarm = null;

            string what;

            EntityRecommendation title;
            if (result.TryFindEntity(Entity_Alarm_Title, out title))
            {
                what = title.Entity;
            }
            else
            {
                what = DefaultAlarmWhat;
            }

            return this.alarmByWhat.TryGetValue(what, out alarm);
        }

        public const string Entity_Alarm_Title = "builtin.alarm.title";
        public const string Entity_Alarm_Start_Time = "builtin.alarm.start_time";
        public const string Entity_Alarm_Start_Date = "builtin.alarm.start_date";

        [LuisIntent("")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            string message = $"Sorry I did not understand: " + string.Join(", ", result.Intents.Select(i => i.Intent));
            await context.PostAsync(message);
            context.Wait(MessageReceived);
        }

        [LuisIntent("builtin.intent.alarm.delete_alarm")]
        public async Task DeleteAlarm(IDialogContext context, LuisResult result)
        {
            Alarm alarm;
            if (TryFindAlarm(result, out alarm))
            {
                this.alarmByWhat.Remove(alarm.What);
                await context.PostAsync($"alarm {alarm} deleted");
            }
            else
            {
                await context.PostAsync("did not find alarm");
            }

            context.Wait(MessageReceived);
        }

        [LuisIntent("builtin.intent.alarm.find_alarm")]
        public async Task FindAlarm(IDialogContext context, LuisResult result)
        {
            Alarm alarm;
            if (TryFindAlarm(result, out alarm))
            {
                await context.PostAsync($"found alarm {alarm}");
            }
            else
            {
                await context.PostAsync("did not find alarm");
            }

            context.Wait(MessageReceived);
        }

        [LuisIntent("builtin.intent.alarm.set_alarm")]
        public async Task SetAlarm(IDialogContext context, LuisResult result)
        {
            EntityRecommendation title;
            if (!result.TryFindEntity(Entity_Alarm_Title, out title))
            {
                title = new EntityRecommendation(type: Entity_Alarm_Title) { Entity = DefaultAlarmWhat };
            }

            EntityRecommendation date;
            if (!result.TryFindEntity(Entity_Alarm_Start_Date, out date))
            {
                date = new EntityRecommendation(type: Entity_Alarm_Start_Date) { Entity = string.Empty };
            }

            EntityRecommendation time;
            if (!result.TryFindEntity(Entity_Alarm_Start_Time, out time))
            {
                time = new EntityRecommendation(type: Entity_Alarm_Start_Time) { Entity = string.Empty };
            }

            var parser = new Chronic.Parser();
            var span = parser.Parse(date.Entity + " " + time.Entity);

            if (span != null)
            {
                var when = span.Start ?? span.End;
                var alarm = new Alarm() { What = title.Entity, When = when.Value };
                this.alarmByWhat[alarm.What] = alarm;

                string reply = $"alarm {alarm} created";
                await context.PostAsync(reply);
            }
            else
            {
                await context.PostAsync("could not find time for alarm");
            }

            context.Wait(MessageReceived);
        }

        [LuisIntent("builtin.intent.alarm.snooze")]
        public async Task AlarmSnooze(IDialogContext context, LuisResult result)
        {
            Alarm alarm;
            if (TryFindAlarm(result, out alarm))
            {
                alarm.When = alarm.When.Add(TimeSpan.FromMinutes(7));
                await context.PostAsync($"alarm {alarm} snoozed!");
            }
            else
            {
                await context.PostAsync("did not find alarm");
            }

            context.Wait(MessageReceived);
        }

        [LuisIntent("builtin.intent.alarm.time_remaining")]
        public async Task TimeRemaining(IDialogContext context, LuisResult result)
        {
            Alarm alarm;
            if (TryFindAlarm(result, out alarm))
            {
                var now = DateTime.UtcNow;
                if (alarm.When > now)
                {
                    var remaining = alarm.When.Subtract(DateTime.UtcNow);
                    await context.PostAsync($"There is {remaining} remaining for alarm {alarm}.");
                }
                else
                {
                    await context.PostAsync($"The alarm {alarm} expired already.");
                }
            }
            else
            {
                await context.PostAsync("did not find alarm");
            }

            context.Wait(MessageReceived);
        }

        private Alarm turnOff;

        [LuisIntent("builtin.intent.alarm.turn_off_alarm")]
        public async Task TurnOffAlarm(IDialogContext context, LuisResult result)
        {
            if (TryFindAlarm(result, out this.turnOff))
            {
                PromptDialog.Confirm(context, AfterConfirming_TurnOffAlarm, "Are you sure?", promptStyle: PromptStyle.None);
            }
            else
            {
                await context.PostAsync("did not find alarm");
                context.Wait(MessageReceived);
            }
        }

        public async Task AfterConfirming_TurnOffAlarm(IDialogContext context, IAwaitable<bool> confirmation)
        {
            if (await confirmation)
            {
                this.alarmByWhat.Remove(this.turnOff.What);
                await context.PostAsync($"Ok, alarm {this.turnOff} disabled.");
            }
            else
            {
                await context.PostAsync("Ok! We haven't modified your alarms!");
            }

            context.Wait(MessageReceived);
        }

        [LuisIntent("builtin.intent.alarm.alarm_other")]
        public async Task AlarmOther(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("what ?");
            context.Wait(MessageReceived);
        }

        public SimpleAlarmDialog()
        {

        }

        public SimpleAlarmDialog(ILuisService service)
            : base(service)
        {
        }

        [Serializable]
        public sealed class Alarm : IEquatable<Alarm>
        {
            public DateTime When { get; set; }
            public string What { get; set; }

            public override string ToString()
            {
                return $"[{this.What} at {this.When}]";
            }

            public bool Equals(Alarm other)
            {
                return other != null
                    && this.When == other.When
                    && this.What == other.What;
            }

            public override bool Equals(object other)
            {
                return Equals(other as Alarm);
            }

            public override int GetHashCode()
            {
                return this.What.GetHashCode();
            }
        }
    }
}
// </fullExample>