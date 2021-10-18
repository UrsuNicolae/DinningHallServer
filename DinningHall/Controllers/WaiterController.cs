using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DinningHall.Data.Interfaces;
using DinningHall.DTOs;

namespace DinningHall.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class WaiterController : ControllerBase
    {
        private readonly IWaiterRepository _repo;

        public WaiterController(IWaiterRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("{waiterId}")]
        public ActionResult<GetWaiterDto> GetWaiter(Guid waiterId)
        {
            var waiterModel = _repo.GetWaiterById(waiterId).Result;
            return Ok(waiterModel);
        }

        [HttpGet]
        public ActionResult<GetWaiterDto> GetWaiters()
        {
            return Ok(_repo.GetAllWaiters());
        }

        [HttpDelete("{waiterId}")]
        public ActionResult DeleteWaiter(Guid waiterId)
        {
            _repo.DeleteWaiter(waiterId);
            return Ok();
        }

        [HttpPost]
        public ActionResult<GetWaiterDto> CreateWaiter(CreateWaiterDto waiter)
        {
            var waiterModel = _repo.CreateWaiter(waiter);
            return CreatedAtAction(nameof(GetWaiter), new { waiterId = waiterModel.Id}, waiterModel);
        }

        [HttpPost("{nr}")]
        public ActionResult<IEnumerable<GetWaiterDto>> CreateNWaiters(int nr)
        {
            return Ok(_repo.CreateNWaiters(nr));
        }
    }
}
