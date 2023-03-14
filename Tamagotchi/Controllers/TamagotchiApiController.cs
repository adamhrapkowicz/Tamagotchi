﻿using Microsoft.AspNetCore.Mvc;
using Tamagotchi.Contracts;
using Tamagotchi.Models;

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
        [Route("{dragonId}")]
        public Dragon GetGameStatus(Guid dragonId)
        {
            return _lifeCycleManager.GetDragonById(dragonId);
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
        [Route("feed/{dragonId}")]
        public FeedDragonResponse FeedDragon(Guid dragonId)
        {
            return _lifeCycleManager.IncreaseFeedometer(dragonId);
        }

        // PUT \TamagotchiApi\pet\{dragonId}
        [HttpPut]
        [Route("pet/{dragonId}")]
        public PetDragonResponse PetDragon(Guid dragonId)
        {
            return _lifeCycleManager.IncreaseHappiness(dragonId);
        }
    }
}