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

        [HttpGet("notifications")]
        public ActionResult<List<Notification>> GetAllNotifications() {
            return _valuesDao.getAllNotifications();
        }

        [HttpGet("notifications/{id}")]
        public ActionResult<List<Notification>> GetNotificationsByAccount(string id) {
            return _valuesDao.getNotificationsByAccount(id);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<List<Transaction>> GetTransactionByAccountNumber(string id)
        {
            return _valuesDao.getTransactionByAccountNumber(id);
        }

        [HttpGet("triggers")]
        public ActionResult<List<Trigger>> GetAllTriggers() {
            return _valuesDao.getAllTriggers();
        }

        [HttpPost("{id}/trigger")]
        public void CreateTrigger(string id, string triggerType, string triggerValue) {
            _valuesDao.createTrigger(id, triggerType, triggerValue);
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