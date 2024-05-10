using ChgCharityJamPrototype.Models.GameEngineModels;
using SDCS;
using SDCS.Engine;
using SDCS.EventSystem;
using SDCS.Teams;

namespace ChgCharityJamPrototype.HostedService;

public class GameStatusProvider : IGameStatusProvider, IGameUpdate
{
	private GameStatusModel _backendModel = new GameStatusModel();
	public Game Game { get; }
	public TimeProvider TimeProvider { get; }

	public event EventHandler<GameStatusUpdatedEventArgs> GameStatusUpdated;

	public GameStatusProvider(Game game, TimeProvider timeProvider)
	{
		Game = game ?? throw new ArgumentNullException(nameof(game));
		TimeProvider = timeProvider ?? throw new ArgumentNullException(nameof(timeProvider));
	}

	public GameStatusModel GetLatestGameStatus() => _backendModel;

	public Task GameUpdated(IGame game)
	{
		var teams = Game.TeamManager.teams.Select(t => new Team { Id = t.Key.Guid.ToString(), Balance = t.Value.TeamData.Balance, Effects = GetTeamEffects(t.Key), Workspace = t.Value.TeamData.Workspace }).ToArray();
		var newModel = new GameStatusModel
		{
			Teams = teams,
			Uptime = Game.Engine.Uptime
		};

		// ToDo: Diff gamestatus
		// publish event
		// GameStatusUpdated.Invoke(newModel);

		_backendModel = newModel;
		return Task.CompletedTask;
	}

	private Effect[] GetTeamEffects(Handle<ITeam> teamId)
	{
		var effectHandles = Game.EffectManager.GetTeamEffects(teamId);
		var effects = effectHandles.Select(e => (Handle: e, Effect: Game.EffectManager.GetRawEffect(e)));

		return effects.Select(e =>
		{
			var activationTime = GetActivationTime(e.Effect.ActivationTime);
			var duration = TimeSpan.FromSeconds(e.Effect.Duration);

			return new Effect
			{
				Id = e.Handle.Guid.ToString(),
				ActivationTime = activationTime,
				Duration = duration,
				EffectEnd = activationTime + duration
			};
		}).ToArray();
	}

	private DateTimeOffset? GetActivationTime(TimeSpan? activationTime)
	{
		if (activationTime is null)
			return null;

		return TimeProvider.GetLocalNow().Date + activationTime;
	}
}

public class GameStatusUpdatedEventArgs : System.EventArgs
{
	public GameStatusModel GameStatusModel { get; }

	public GameStatusUpdatedEventArgs(GameStatusModel gameStatusModel)
	{
		GameStatusModel = gameStatusModel ?? throw new ArgumentNullException(nameof(gameStatusModel));
	}
}