using System.Threading.Tasks;
using Microsoft.Bot;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Core.Extensions;
using Microsoft.Bot.Schema;

namespace InputHintsPublic
{
    public class EchoBot : IBot
    {
        /// <summary>
        /// Every Conversation turn for our EchoBot will call this method. In here
        /// the bot checks the Activty type to verify it's a message, bumps the 
        /// turn conversation 'Turn' count, and then echoes the users typing
        /// back to them. 
        /// </summary>
        /// <param name="context">Turn scoped context containing all the data needed
        /// for processing this conversation turn. </param>        
        public async Task OnTurn(ITurnContext context)
        {
            // This bot is only handling Messages
            if (context.Activity.Type == ActivityTypes.Message)
            {
                {
                    // <Accepting input>
                    var reply = MessageFactory.Text(
                        "This is the text that will be displayed.",
                        "This is the text that will be spoken.",
                        InputHints.AcceptingInput);
                    await context.SendActivity(reply).ConfigureAwait(false);
                    // </Accepting input>
                }
                {
                    // <Expecting input>
                    var reply = MessageFactory.Text(
                        "This is the text that will be displayed.",
                        "This is the text that will be spoken.",
                        InputHints.ExpectingInput);
                    await context.SendActivity(reply).ConfigureAwait(false);
                    // </Expecting input>
                }
                {
                    // <Ignoring input>
                    var reply = MessageFactory.Text(
                        "This is the text that will be displayed.",
                        "This is the text that will be spoken.",
                        InputHints.IgnoringInput);
                    await context.SendActivity(reply).ConfigureAwait(false);
                    // </Ignoring input>
                }
            }
        }
    }
}