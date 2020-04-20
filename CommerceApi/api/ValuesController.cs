using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommerceApi.dao;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;


namespace CommerceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private static IValuesDao _valuesDao;


        public ValuesController(IValuesDao valuesDao)
        {
            _valuesDao = valuesDao;
        }


        // GET api/values
        [HttpGet]
        public ActionResult<List<Transaction>> GetAllTransactions() {
            return _valuesDao.getAllTransactions();
        }

        //[HttpGet]
        //public ActionResult<List<Notification>> GetAllNotifications() {
        //    return _valuesDao.getAllNotifications();
        //}

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
