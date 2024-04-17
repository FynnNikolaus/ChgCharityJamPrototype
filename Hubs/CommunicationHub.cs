using Microsoft.AspNetCore.SignalR;

namespace ChgCharityJamPrototype.Hubs
{
	public class CommunicationHub : Hub
	{
		public async Task SendMessage(string user, string message)
		{
			await Clients.All.SendAsync("ReceiveMessage", user, message);
		}
	}
}
