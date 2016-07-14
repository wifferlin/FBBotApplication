using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.Bot.Connector;
using Newtonsoft.Json;

namespace Bot_Application1
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {
            if (activity.Type == ActivityTypes.Message)
            {
                ConnectorClient connector = new ConnectorClient(new Uri(activity.ServiceUrl));
                // calculate something for us to return


                // return our reply to the user
                if (activity.Text == "hi" || activity.Text == "嗨")
                {
                    Activity reply = activity.CreateReply($"hihi, my name is testbot,你可以輸入台灣縣市名來查詢指外線指數喔!!!(ex.臺北)");
                    await connector.Conversations.ReplyToActivityAsync(reply);
                }
                else if (activity.Text == "笑")
                {
                    Activity reply = activity.CreateReply($":)");
                    await connector.Conversations.ReplyToActivityAsync(reply);
                }
                else if (activity.Text == "難過")
                {
                    Activity reply = activity.CreateReply($":(");
                    await connector.Conversations.ReplyToActivityAsync(reply);
                }
                else if (activity.Text == "嘿嘿")
                {
                    Activity reply = activity.CreateReply($":3");
                    await connector.Conversations.ReplyToActivityAsync(reply);
                }
                else if (activity.Text == "哭")
                {
                    Activity reply = activity.CreateReply($":'(");
                    await connector.Conversations.ReplyToActivityAsync(reply);
                }
                else if (activity.Text == "愛")
                {
                    Activity reply = activity.CreateReply($"<3");
                    await connector.Conversations.ReplyToActivityAsync(reply);
                }
                else
                {
                    Activity reply = activity.CreateReply(await GetUV(activity.Text));
                    await connector.Conversations.ReplyToActivityAsync(reply);
                }

            }
            else
            {
                HandleSystemMessage(activity);
            }
            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }

        private async Task<string> GetUV(string strUV)
        {
            string strRet = string.Empty;
            string data = await UV.GetUVAsync(strUV);

            //string UVI = data.Split(',')[0];
            //string publishtime = data.Split(',')[1];

            if (data == "null")
            {
                strRet = string.Format("地點: \"{0}\"不是一個正確的測站地點", strUV.ToUpper());
            }
            else
            {
                string UVI = data.Split(',')[0];
                string publishtime = data.Split(',')[1];
                strRet = string.Format("地點:{0} 所測得的紫外線指數為 {1}, 觀測時間為{2}", strUV.ToUpper(), UVI, publishtime);
            }
            return strRet;
        }
        private Activity HandleSystemMessage(Activity message)
        {
            if (message.Type == ActivityTypes.DeleteUserData)
            {
                // Implement user deletion here
                // If we handle user deletion, return a real message
            }
            else if (message.Type == ActivityTypes.ConversationUpdate)
            {
                // Handle conversation state changes, like members being added and removed
                // Use Activity.MembersAdded and Activity.MembersRemoved and Activity.Action for info
                // Not available in all channels
            }
            else if (message.Type == ActivityTypes.ContactRelationUpdate)
            {
                // Handle add/remove from contact lists
                // Activity.From + Activity.Action represent what happened
            }
            else if (message.Type == ActivityTypes.Typing)
            {
                // Handle knowing tha the user is typing
            }
            else if (message.Type == ActivityTypes.Ping)
            {
            }

            return null;
        }
    }
}