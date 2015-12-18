using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace SMS.Hubs
{
    public class FinalVotingHub : Hub
    {
        public static void Show()
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<FinalVotingHub>();
            context.Clients.All.displayStatus();
        }
    }
}