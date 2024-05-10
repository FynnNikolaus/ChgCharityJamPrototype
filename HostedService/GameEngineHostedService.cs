
using SDCS.Engine;

namespace ChgCharityJamPrototype.HostedService;

public class GameEngineHostedService : BackgroundService
{
	public Game Game { get; }
	public Engine Engine { get; }


	public GameEngineHostedService(Game game, Engine engine)
	{
		Game = game ?? throw new ArgumentNullException(nameof(game));
		Engine = engine ?? throw new ArgumentNullException(nameof(engine));
	}

	protected override Task ExecuteAsync(CancellationToken stoppingToken)
	{
		return Task.CompletedTask;

		/*Engine.GenericInputDevice =
		Engine.GameUpdateHandler = this;

		var gameTask = Engine.Run(Game);

		while (!stoppingToken.IsCancellationRequested)
		{

		}
		*/
	}
}
