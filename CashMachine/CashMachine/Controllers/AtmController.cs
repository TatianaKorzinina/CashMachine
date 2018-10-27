using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CashMachine.Controllers
{
    [Route("api/[controller]")]
    public class AtmController : Controller
    {
        private readonly ILogger<AtmController> _logger;
        private IRepository repository;

        public AtmController(ILogger<AtmController> logger, IRepository repo)
        {
            _logger = logger;
            repository = repo;
        }

        // GET api/atm
        [HttpGet]
        public Account Get()
        {
            return repository.GetBalance();
        }

        // POST api/atm/getcash
        [HttpPost("getcash")]
        public IActionResult Post([FromBody] Account getMoney)
        {
           
            if (getMoney != null && repository.GetBalance().Amount >= getMoney.Amount &&
                getMoney.Amount >= 100 && getMoney.Amount % 100 == 0)
            {
                Atm atm = new Atm();
                var getCash = getMoney.Amount;
                atm.Money =  repository.GetMoney(getCash);
                if (atm.Money != null)
                {
                    return Ok(atm);
                }
                else
                {
                    return BadRequest(new Errors { ErrorMessage = "невозможно выдать указанную сумму" });
                }
            }
            else return BadRequest(new Errors { ErrorMessage= "сумма должа быть кратна 100 и не превышать ваш баланс" });

            
        }

        //POST api/atm/refillbalance
       [HttpPost("refillbalance")]
        public IActionResult Post1([FromBody] Account refillBalance)
        {
            if (refillBalance == null)
            {
                return BadRequest(new Errors
                { ErrorMessage = "сумма должна быть положительным целым числом" });
            }
            if (refillBalance.Amount > 0)
            {
                repository.AccountRefill(refillBalance.Amount);
                return Ok(repository.GetBalance());
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
