using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Module1.Models;

namespace Module2.Controllers
{
    [ApiVersion("2.0")] // Send request type header message: x-api-version 2.0
    [Route("api/movies")]
    //[Route("api/v{version:apiVersion}/movies")] // URL versioning
    [Produces("application/json")]
    [ApiController]
    public class MoviesV2Controller : ControllerBase
    {
        static List<MoviesV2> _movies = new List<MoviesV2>() {
            new MoviesV2(){ Id = 0, Name = "00", Description = "Desc00", Type = "00" },
            new MoviesV2(){ Id = 1, Name = "11", Description = "Desc11", Type = "11" },
            new MoviesV2(){ Id = 2, Name = "22", Description = "Desc22", Type = "22" },
            new MoviesV2(){ Id = 3, Name = "33", Description = "Desc33", Type = "33" }
        };
        
        [HttpGet]
        public IEnumerable<MoviesV2> Get ()
        {
            return _movies;
        }
    }
}