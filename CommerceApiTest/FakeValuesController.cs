using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommerceApi;
using CommerceApi.dao;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;


namespace CommerceApiTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FakeValuesController : ControllerBase
    {
        public static IValuesDao _testValueDao;


        public FakeValuesController(IValuesDao transactionDao)
        {
            _testValueDao = transactionDao;
        }



        // GET api/values
        [HttpGet]
        public ActionResult<List<Transaction>> GetAllTransactions()
        {
            return _testValueDao.getAllTransactions();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
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
