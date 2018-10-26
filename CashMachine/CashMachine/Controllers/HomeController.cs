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
        [HttpPost("getmoney")]
        public IActionResult Post([FromBody] Account changeBalance)
        {
            Repository repository = new Repository();
            //if (changeBalance == null)
            //{
            //    return BadRequest(new Errors { ErrorMessage = "сумма должа быть кратна 100 и не превышать ваш баланс" });
            //}
            
            if (changeBalance != null && repository.Balance().Amount >= changeBalance.Amount &&
                changeBalance.Amount >= 100 && changeBalance.Amount % 100 == 0)
            {
                Atm atm = new Atm();
                var getCash = changeBalance.Amount;
                atm.money =  repository.GetMoney(getCash);
                return Ok(atm);
            }
            else return BadRequest(new Errors { ErrorMessage= "сумма должа быть кратна 100 и не превышать ваш баланс" });

            
        }

        //POST api/values
       [HttpPost("changebalance")]
        public IActionResult Post1([FromBody] Account changeBalance)
        {
            
            Repository repository = new Repository();
            if (changeBalance == null)
            {
                return BadRequest(new Errors
                { ErrorMessage = "сумма должна быть положительным целым числом" });
            }
            if (changeBalance.Amount > 0)
            {
                repository.AccountRefill(changeBalance.Amount);
                return Ok(repository.Balance());
            }
            else return BadRequest(new Errors
            { ErrorMessage = "сумма должна быть положительным целым числом" });

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
