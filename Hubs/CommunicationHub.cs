using ChgCharityJamPrototype.Models.GameEngineModels;
using Microsoft.AspNetCore.SignalR;

namespace ChgCharityJamPrototype.Hubs
{
	public class CommunicationHub : Hub
	{
	}

	/// <summary>
	/// Implementation class for the communication hub that cannot be injected by itself.
	/// </summary>
	/// <param name="context">The SignalR context to notify the clients with</param>
	public class CommunicationHubImplementation(IHubContext<CommunicationHub> context) : ICommunicationHub
	{
		public async Task UpdateGameStatus(GameStatusModel gameStatus, CancellationToken cancellationToken)
		{
			await context.Clients.All.SendAsync("UpdateGameStatus", gameStatus, cancellationToken);
		}
	}

	/// <summary>
	/// The hub to notify clients about game state changes
	/// </summary>
	public interface ICommunicationHub
	{
		Task UpdateGameStatus(GameStatusModel gameStatus, CancellationToken cancellationToken);
	}
}
