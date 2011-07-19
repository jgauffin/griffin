using Griffin.Messaging;

namespace Griffin.Core.Tests.Messaging
{
    public class UserService : IHandlerOf<FindContactsContext>
    {
        public void ProcessRequest(FindContactsContext context)
        {
            if (context.Request.SearchValue == "blabla")
            {
                context.Response.Contacts.Add("My contact name");
            }
        }
    }
}