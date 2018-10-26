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

            if (repository.Balance().Amount >= id && id>=100 && id % 100 == 0)
            {
                Atm atm = new Atm();

                atm.money =  repository.GetMoney(id);
                return Ok(atm);
            }
            else return BadRequest(new Errors { ErrorMessage= "сумма должа быть кратна 100 и не превышать ваш баланс" });

            
        }

        // POST api/values
        [HttpPost]
        public void Post(int changeBalance)
        {
            //победить NullReferenceExeption!!!!!!
            Repository repository = new Repository();
            repository.AccountRefill(changeBalance);

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
