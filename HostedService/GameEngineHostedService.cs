
using AsyncAwaitBestPractices;
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
		GameStatusModel gameStatus = null;

		Engine.GameUpdateHandler = GameStatusProvider;

		// run the game
		Engine.Run(Game, targetFPS: 1).SafeFireAndForget();

		while (!stoppingToken.IsCancellationRequested)
		{
			var oldGameStatus = gameStatus;

			// get the current game status and check for changes
			gameStatus = GameStatusProvider.GetLatestGameStatus();
			var needsUpdate = oldGameStatus is null || !oldGameStatus.Equals(gameStatus);

			// if the game status did change, notify all the frontend panels
			if (needsUpdate)
				await CommunicationHub.UpdateGameStatus(gameStatus, stoppingToken);

			await Task.Delay(TimeSpan.FromSeconds(1), stoppingToken);
		}
	}
}
