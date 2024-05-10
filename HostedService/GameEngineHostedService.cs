
using SDCS.Engine;

namespace ChgCharityJamPrototype.HostedService;

public class GameEngineHostedService : BackgroundService
{
	public Game Game { get; }
	public Engine Engine { get; }
	public GameStatusProvider GameStatusProvider { get; }

	public GameEngineHostedService(Game game, Engine engine, GameStatusProvider gameStatusProvider)
	{
		Game = game ?? throw new ArgumentNullException(nameof(game));
		Engine = engine ?? throw new ArgumentNullException(nameof(engine));
		GameStatusProvider = gameStatusProvider ?? throw new ArgumentNullException(nameof(gameStatusProvider));
	}

	protected override Task ExecuteAsync(CancellationToken stoppingToken)
	{
		Engine.GameUpdateHandler = GameStatusProvider;

		var gameTask = Engine.Run(Game, targetFPS: 1);

		while (!stoppingToken.IsCancellationRequested)
		{

		}

		return Task.CompletedTask;
	}
}
