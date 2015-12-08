using System;
using System.Collections.Generic;
using Microsoft.AspNet.Mvc;
using PopulationGenetics.Library.Interfaces;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace PopulationGenetics.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class WorldController : Controller
    {
        private IWorld _world;

        public WorldController(IWorld world)
        {
            _world = world;
        }
        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new List<string>() { _world.Population.PopulationSize.ToString() };
        }

    }
}
