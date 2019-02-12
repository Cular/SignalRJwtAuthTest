using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRAuthTest.Hub
{
    [Authorize]
    public class ChatHub : Hub<IChatClient>
    {
        /// <summary>
        /// Joins the game chat.
        /// </summary>
        /// <param name="gameId">The game identifier.</param>
        /// <returns>Void result.</returns>
        public Task JoinGameChat(string gameId)
        {
            return this.Groups.AddToGroupAsync(this.Context.ConnectionId, gameId);
        }

        /// <summary>
        /// Leaves the game chat.
        /// </summary>
        /// <param name="gameId">The game identifier.</param>
        /// <returns>Void result.</returns>
        public Task LeaveGameChat(string gameId)
        {
            return this.Groups.RemoveFromGroupAsync(this.Context.ConnectionId, gameId);
        }

        /// <summary>
        /// Sends the message asynchronous.
        /// </summary>
        /// <param name="messageDto">The message dto.</param>
        /// <returns>Void result.</returns>
        public Task SendMessage(MessageDto message)
        {
            return this.Clients.Group(message.RoomId).ReceiveMessage($"{this.Context.User?.Identity?.Name ?? "null"}: {message.Body}");
        }
    }

    public class MessageDto
    {
        public string RoomId { get; set; }

        public string Body { get; set; }
    }
}
