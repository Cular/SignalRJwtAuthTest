using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRAuthTest.Hub
{
    public interface IChatClient
    {
        Task ReceiveMessage(string message);
    }
}
