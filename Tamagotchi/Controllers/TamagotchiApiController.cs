using System.Net;
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
        public ActionResult<GameStatusResponse> GetGameStatus(Guid dragonId)
        {
            return _lifeCycleManager.GetGameStatus(dragonId);
        }

        // POST \TamagotchiApi\{dragonName}
        [HttpPost]
        [Route("{dragonName}")]
        public ActionResult<CreateDragonResponse> StartGame(string dragonName)
        {
            if (string.IsNullOrWhiteSpace(dragonName)) 
                return BadRequest("dragonName cannot be empty");

            return new ObjectResult(_lifeCycleManager.CreateDragon(dragonName))
                { StatusCode = (int)HttpStatusCode.Created };
        }

        // PUT \TamagotchiApi\feed\{dragonId}
        [HttpPut]
        [Route("feed/{dragonId:guid}")]
        public async Task<ActionResult<FeedDragonResponse>> FeedDragon(Guid dragonId)
        {
            return await _lifeCycleManager.IncreaseFeedometerAsync(dragonId);
        }

        // PUT \TamagotchiApi\pet\{dragonId}
        [HttpPut]
        [Route("pet/{dragonId:guid}")]
        public async Task<ActionResult<PetDragonResponse>> PetDragon(Guid dragonId)
        {
            return await _lifeCycleManager.IncreaseHappinessAsync(dragonId);
        }
    }
}
