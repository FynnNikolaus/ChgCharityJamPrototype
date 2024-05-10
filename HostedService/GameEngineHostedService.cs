
using ChgCharityJamPrototype.Hubs;
using ChgCharityJamPrototype.Models.GameEngineModels;
using SDCS.Engine;

namespace ChgCharityJamPrototype.HostedService;

public class GameEngineHostedService : BackgroundService
{
	public Game Game { get; }
	public Engine Engine { get; }
	public ICommunicationHub CommunicationHub { get; }
	public GameStatusProvider GameStatusProvider { get; }

	public GameEngineHostedService(Game game, Engine engine, GameStatusProvider gameStatusProvider, ICommunicationHub communicationHub)
	{
		Game = game ?? throw new ArgumentNullException(nameof(game));
		Engine = engine ?? throw new ArgumentNullException(nameof(engine));
		GameStatusProvider = gameStatusProvider ?? throw new ArgumentNullException(nameof(gameStatusProvider));
    CommunicationHub = communicationHub ?? throw new ArgumentNullException(nameof(communicationHub));
	}

	protected override async Task ExecuteAsync(CancellationToken stoppingToken)
	{
		Engine.GameUpdateHandler = GameStatusProvider;

		var gameTask = Engine.Run(Game, targetFPS: 1);

    await CommunicationHub.UpdateGameStatus(new GameStatusModel(), stoppingToken);

		while (!stoppingToken.IsCancellationRequested)
		{

		}

		return Task.CompletedTask;
	}
}
