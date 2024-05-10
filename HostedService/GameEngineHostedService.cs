
using ChgCharityJamPrototype.Hubs;
using ChgCharityJamPrototype.Models.GameEngineModels;
using SDCS.Engine;

namespace ChgCharityJamPrototype.HostedService;

public class GameEngineHostedService : BackgroundService
{
	public Game Game { get; }
	public Engine Engine { get; }
	public ICommunicationHub CommunicationHub { get; }

	public GameEngineHostedService(Game game, Engine engine, ICommunicationHub communicationHub)
	{
		Game = game ?? throw new ArgumentNullException(nameof(game));
		Engine = engine ?? throw new ArgumentNullException(nameof(engine));
		CommunicationHub = communicationHub ?? throw new ArgumentNullException(nameof(communicationHub));
	}

	protected override async Task ExecuteAsync(CancellationToken stoppingToken)
	{
		await CommunicationHub.UpdateGameStatus(new GameStatusModel(), stoppingToken);

		/*Engine.GenericInputDevice =
		Engine.GameUpdateHandler = this;

		var gameTask = Engine.Run(Game);

		while (!stoppingToken.IsCancellationRequested)
		{

		}
		*/
	}
}
