using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CashMachine.Controllers
{
    [Route("api/[controller]")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // GET api/home
        [HttpGet]
        public Account Get()
        {
            Repository repository = new Repository();
            return repository.Balance();
        }

        // GET api/home/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
           
            Repository repository = new Repository();
            //if (id % 100 != 0) yield return "введите сумму кратную 100 рублям";
            if (repository.Balance().Amount >= id && id>=100 && id % 100 == 0)
            {
                Atm atm = new Atm();

                atm.money =  repository.GetMoney(id);
                return Ok(atm);

                //foreach (var item in money)
                //{
                //    yield return $"по {item.Key}  -- {item.Value} купюр";
                //}
            }
            else return BadRequest(new Errors { ErrorMessage= "сумма должа быть кратна 100 и не превышать ваш баланс" });

            
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
