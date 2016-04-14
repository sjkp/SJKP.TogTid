using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Connector.Utilities;
using Microsoft.Bot.Builder.Dialogs;
using Newtonsoft.Json;

namespace SJKP.TogTid
{
    [Serializable]
    public class EchoDialog : IDialog<object>
    {
        private int count = 1;
        private int locationId = -1;
        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);
        }
        public async Task MessageReceivedAsync(IDialogContext context, IAwaitable<Message> argument)
        {
           
            var message = await argument;
            if (message.Text == "reset")
            {
                PromptDialog.Confirm(
                    context,
                    AfterResetAsync,
                    "Are you sure you want to reset the count?",
                    "Didn't get that!");
            }
            //else if (locationId < 0)
            //{
            //    await context.PostAsync("Please write the location you want to see depatures for");
            //    context.Wait(MessageReceivedAsync);
            //}
            else
            {
                await HandleLocationSelect(context, message.Text);
            }
        }

        private async Task HandleLocationSelect(IDialogContext context, string text)
        {
            var client = new RejseplanApi();
            try {
                var response = await client.GetLocation(text);
                var location = response.FirstOrDefault(s => s.name == text);
                if (location != null)
                {
                    locationId = int.Parse(location.id);
                    var depatures = await client.GetDepartures(locationId);


                    await context.PostAsync(string.Join(Environment.NewLine, depatures.Select(s => s.ToString()).Take(10).ToArray()));
                }
                else
                {
                    PromptDialog.Choice<string>(context, AfterLocationSelectAsync, response.Select(s => s.name), "Select the location you meant", "Try again", 3);
                }
            }
            catch (Exception e)
            {

            }
        }

        public async Task AfterLocationSelectAsync(IDialogContext context, IAwaitable<string> argument)
        {
            var location = await argument;
            await HandleLocationSelect(context, location);

        }
        public async Task AfterResetAsync(IDialogContext context, IAwaitable<bool> argument)
        {
            var confirm = await argument;
            if (confirm)
            {
                this.count = 1;
                await context.PostAsync("Reset count.");
            }
            else
            {
                await context.PostAsync("Did not reset count.");
            }
            context.Wait(MessageReceivedAsync);
        }
    }

    [BotAuthentication]
    public class MessagesController : ApiController
    {
        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>
        public async Task<Message> Post([FromBody]Message message)
        {
            if (message.Type == "Message")
            {
                // calculate something for us to return
                return await Conversation.SendAsync(message, () => new EchoDialog());
            }
            else
            {
                return HandleSystemMessage(message);
            }
        }

        private Message HandleSystemMessage(Message message)
        {
            if (message.Type == "Ping")
            {
                Message reply = message.CreateReplyMessage();
                reply.Type = "Ping";
                return reply;
            }
            else if (message.Type == "DeleteUserData")
            {
                // Implement user deletion here
                // If we handle user deletion, return a real message
            }
            else if (message.Type == "BotAddedToConversation")
            {
                var reply = message.CreateReplyMessage($"Which station do you want depature information for");
            }
            else if (message.Type == "BotRemovedFromConversation")
            {
            }
            else if (message.Type == "UserAddedToConversation")
            {
            }
            else if (message.Type == "UserRemovedFromConversation")
            {
            }
            else if (message.Type == "EndOfConversation")
            {
            }

            return null;
        }
    }
}