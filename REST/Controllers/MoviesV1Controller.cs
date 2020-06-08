using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Module1.Models;

namespace Module1.Controllers
{
    [ApiVersion("1.0")] // Send request type header message: x-api-version 1.0
    [Route("api/movies")]
    //[Route("api/v{version:apiVersion}/movies")] // URL Versioning
    [Produces("application/json")]
    [ApiController]
    public class MoviesV1Controller : ControllerBase
    {
        static List<MoviesV1> _movies = new List<MoviesV1>() {
            new MoviesV1(){ Id = 0, Name = "00" },
            new MoviesV1(){ Id = 1, Name = "11" },
            new MoviesV1(){ Id = 2, Name = "22" },
            new MoviesV1(){ Id = 3, Name = "33" }
        };
        
        [HttpGet]
        public IEnumerable<MoviesV1> Get ()
        {
            return _movies;
        }
    }
}