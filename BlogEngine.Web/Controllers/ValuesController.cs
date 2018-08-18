using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BlogEngine.Service.Dtos;
using BlogEngine.Service.Interfaces;
using BlogEngine.WebApi.Helper;
using BlogEngine.WebApi.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace BlogEngine.Web.Controllers
{
    [Route("api/[controller]")]
    //[ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IBlogService _blogService;
        public ValuesController(IBlogService blogService)
        {
            _blogService = blogService;
        }
        // GET api/values
        [HttpGet]
        public ActionResult<IList<BlogDTO>> Get()
        {
            var tmp = _blogService.GetAll();
            return new JsonResult(tmp);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public ActionResult<string> Post([FromBody] BlogViewModel view)
        {
            return "";
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
