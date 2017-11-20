using dtso.data.Repositories.Interfaces;
using dtso.data.StoredProcedures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dtso.api.Controllers
{
    [Route("api")]
    [Authorize]
    public class SearchController : Controller
    {
        private IStoredProcedureRepository _storedProcRepository;

        public SearchController(IStoredProcedureRepository storedProcRepo)
        {
            _storedProcRepository = storedProcRepo;
        }

        [HttpGet("search")]
        public IActionResult Search([FromQuery] string searchString)
        {
            List<SearchResult> searchResults = _storedProcRepository.Search(searchString);

            searchResults.Select(result =>
                {
                    if (!result.Relavance.HasValue)
                    {
                        result.Relavance = 1;
                    }
                    return result;
                }
            ).OrderBy(result => result.Relavance);

            return Ok(searchResults);
        }
    }
}
