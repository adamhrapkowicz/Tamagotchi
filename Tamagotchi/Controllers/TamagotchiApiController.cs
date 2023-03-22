using Microsoft.AspNetCore.Mvc;
using Tamagotchi.Contracts;

namespace Tamagotchi.Controllers
{
    // Turn into an ASP API controller
    [Route("TamagotchiApi")]
    [ApiController]
    public class TamagotchiApiController : ControllerBase
    {
        private readonly ILifeCycleManager _lifeCycleManager;

        public TamagotchiApiController(ILifeCycleManager lifeCycleManager)
        {
            _lifeCycleManager = lifeCycleManager;
        }

        // GET \TamagotchiApi\{dragonId}
        [HttpGet]
        [Route("{dragonId:guid}")]
        public GameStatusResponse GetGameStatus(Guid dragonId)
        {
            return _lifeCycleManager.GetGameStatus(dragonId);
        }

        // POST \TamagotchiApi\{dragonName}
        [HttpPost]
        [Route("{dragonName}")]
        public Guid StartGame(string dragonName)
        {
            return _lifeCycleManager.CreateDragon(dragonName);
        }

        // PUT \TamagotchiApi\feed\{dragonId}
        [HttpPut]
        [Route("feed/{dragonId:guid}")]
        public async Task<FeedDragonResponse> FeedDragon(Guid dragonId)
        {
            return await _lifeCycleManager.IncreaseFeedometerAsync(dragonId);
        }

        // PUT \TamagotchiApi\pet\{dragonId}
        [HttpPut]
        [Route("pet/{dragonId:guid}")]
        public async Task<PetDragonResponse> PetDragon(Guid dragonId)
        {
            return await _lifeCycleManager.IncreaseHappinessAsync(dragonId);
        }
    }
}
