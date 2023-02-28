using Microsoft.AspNetCore.Mvc;

namespace Tamagotchi.Controllers
{
    // Turn into an ASP API controller
    public class TamagotchiApiController : Controller
    {
        private readonly ILifeCycleManager _lifeCycleManager;

        public TamagotchiApiController(ILifeCycleManager lifeCycleManager)
        {
            _lifeCycleManager = lifeCycleManager;
        }

        // POST \TamagotchiApi\{dragonName}

        public ViewResult CreateDragonViewResult()
        {
            return View();
        }
        public Guid StartGame(string name)
        {
            return _lifeCycleManager.CreateDragon(name);
        }

        // PUT \TamagotchiApi\feed\{dragonId}
        public FeedDragonResponse FeedDragon(Guid dragonId)
        {
            return _lifeCycleManager.IncreaseFeedometer(dragonId);
        }

        // PUT \TamagotchiApi\pet\{dragonId}
        public PetDragonResponse PetDragon(Guid dragonId)
        {
            return _lifeCycleManager.IncreaseHappiness(dragonId);
        }

        // GET \TamagotchiApi\{dragonId}
        public Dragon GetGameStatus(Guid dragonId)
        {
            return _lifeCycleManager.GetDragonById(dragonId);
        }
    }
}
