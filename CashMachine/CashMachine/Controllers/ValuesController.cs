using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CashMachine.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // GET api/values
        [HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}
        public Account Get()
        {
            Repository repository = new Repository();
            return repository.Balance();
        }
        // GET api/values/5
        [HttpGet("{id:min(100)}")]
        public JsonResult Get(int id)
        {
           
            Repository repository = new Repository();
            //if (id % 100 != 0) yield return "введите сумму кратную 100 рублям";
            if (repository.Balance().Amount >= id && id % 100 == 0)
            {
                Atm atm = new Atm();

                atm.money =  repository.GetMoney(id);
                return Json(atm);

                //foreach (var item in money)
                //{
                //    yield return $"по {item.Key}  -- {item.Value} купюр";
                //}
            }
            else return Json((object)"на вашем счете недостаточно средств");

            
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
