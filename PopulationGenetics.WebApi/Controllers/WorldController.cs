using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using PopulationGenetics.Library;
using PopulationGenetics.Library.Factories;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace PopulationGenetics.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class WorldController : Controller
    {
        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            var random = new TrulyRandomGenerator();
            var pf = new PersonFactory(random);
            var pop = new Population(pf);
            var world = new World(pop, pf, random);
            yield return "bob";
        }

    }
}
