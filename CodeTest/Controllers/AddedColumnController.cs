using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeTest.Interfaces;
using CodeTest.Model;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CodeTest.Controllers
{
    [Route("api/[controller]")]
    public class AddedColumnController : BaseController
    {
        public IAddedColumnService _addedColumn;
        public AddedColumnController(IAddedColumnService _addedColumn)
        {
            this._addedColumn = _addedColumn;
        }
        // POST api/<controller>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]AddedColumnCreateModel model)
        {
            return await GetResult(await _addedColumn.Create(model));
        }

        
    }
}
