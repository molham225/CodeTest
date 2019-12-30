using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CodeTest.Interfaces;
using CodeTest.Model;
using CodeTest.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ServiceStack.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CodeTest.Controllers
{
    [Route("api/[controller]")]
    public class ContactController : BaseController
    {
        private readonly IContactService _contact;
        public ContactController(IContactService _contact)
        {
            this._contact = _contact;
        }
        // GET: api/<controller>
        [HttpGet]
        public async Task<IActionResult> Get(PaginationInfo paginationInfo)
        {
            var result =  await _contact.GetAll(paginationInfo);
            return await GetResult(result);
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _contact.GetById(id);
            return await GetResult(result);
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Dictionary<string, object> contact)
        {
            
            return await GetResult( await _contact.Create(contact));
        }

        // PUT api/<controller>/5
        [HttpPut()]
        public async Task<IActionResult> Put([FromBody]Dictionary<string, object> contact)
        {
            return await GetResult(await _contact.Update(contact));
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await GetResult(await _contact.Delete(id));
        }


        [HttpPost]
        [Route("Filter")]
        public async Task<IActionResult> Filter([FromBody]List<ColumnFilterInfo> columnFilterInfos, PaginationInfo paginationInfo)
        {
            return await GetResult(await _contact.Filter(columnFilterInfos,paginationInfo));
        }
    }
}
