using ChgCharityJamPrototype.Models.GameEngineModels;

namespace ChgCharityJamPrototype.HostedService;

public interface IGameStatusProvider
{
	GameStatusModel GetLatestGameStatus();
}