using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Griffin.Core.Messaging;
using Griffin.Messaging;

namespace Griffin.Core.Messaging
{

    class RequestAsyncResult<TRequestContext> : RequestAsyncResultBase where TRequestContext : IRequestContext
    {
        public TRequestContext Context { get; set; }

        public IEnumerable<IHandlerOf<TRequestContext>> Subscribers
        {
            get;
            set;
        }

        public override void Invoke()
        {
            foreach (var subscriber in Subscribers)
            {
                try
                {
                    subscriber.ProcessRequest(Context);
                    if (Context.Cancel)
                    {
                        Completed();
                        return;
                    }
                }
                catch (Exception err)
                {

                }
            }
            Completed();
        }

        private void Completed()
        {
            IsCompleted = true;
            ((ManualResetEvent)AsyncWaitHandle).Set();
        }
    }
}
