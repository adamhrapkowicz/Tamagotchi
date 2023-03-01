using System.Web.Http;

namespace Tamagotchi.Controllers
{
    // Turn into an ASP API controller
    public class TamagotchiApiController : ApiController
    {
        private readonly ILifeCycleManager _lifeCycleManager;

        public TamagotchiApiController(ILifeCycleManager lifeCycleManager)
        {
            _lifeCycleManager = lifeCycleManager;
        }

        // POST \TamagotchiApi\{dragonName}
        [HttpPost]
        public Guid StartGame(string name)
        {
            return _lifeCycleManager.CreateDragon(name);
        }

        // PUT \TamagotchiApi\feed\{dragonId}
        [HttpPut]
        public FeedDragonResponse FeedDragon(Guid dragonId)
        {
            return _lifeCycleManager.IncreaseFeedometer(dragonId);
        }

        // PUT \TamagotchiApi\pet\{dragonId}
        [HttpPut]
        public PetDragonResponse PetDragon(Guid dragonId)
        {
            return _lifeCycleManager.IncreaseHappiness(dragonId);
        }

        // GET \TamagotchiApi\{dragonId}
        [HttpGet]
        public Dragon GetGameStatus(Guid dragonId)
        {
            return _lifeCycleManager.GetDragonById(dragonId);
        }
    }
}
