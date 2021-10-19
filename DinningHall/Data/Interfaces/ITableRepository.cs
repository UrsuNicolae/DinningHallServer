using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DinningHall.DTOs;

namespace DinningHall.Data.Interfaces
{
    public interface ITableRepository
    {
        Task<TableDto> CreateTable(CreateTableDto table);

        Task DeleteTable(Guid tableId);

        Task<TableDto> GetTableById(Guid tableId);

        Task<IEnumerable<TableDto>> GetAllTables();

        Task<IEnumerable<TableDto>> CreateNTables(int nr);

        Task<TableDto> UpdateTable(TableDto table);
    }
}
