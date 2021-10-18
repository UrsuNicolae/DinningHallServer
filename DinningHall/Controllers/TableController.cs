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
    public class TableController : ControllerBase
    {
        private readonly ITableRepository _repo;

        public TableController(ITableRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public ActionResult<IEnumerable<GetTableDto>> GetTables()
        {
            return Ok(_repo.GetAllTables());
        }

        [HttpGet("{tableId}")]
        public ActionResult<GetTableDto> GetTable(Guid tableId)
        {
            return Ok(_repo.GetTableById(tableId));
        }

        [HttpPost]
        public ActionResult<GetTableDto> CreateTable(CreateTableDto table)
        {
            var tableModel = _repo.CreateTable(table).Result;
            return CreatedAtAction(nameof(GetTable), new { tableId = tableModel.Id}, tableModel);
        }

        [HttpDelete("{tableId}")]
        public ActionResult DeleteTable(Guid tableId)
        {
            _repo.DeleteTable(tableId);
            return Ok();
        }

        [HttpPost("{nr}")]
        public ActionResult<IEnumerable<GetTableDto>> CreateNTables(int nr)
        {
            return Ok(_repo.CreateNTables(nr));
        }
    }
}
