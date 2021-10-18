using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DinningHall.DTOs;

namespace DinningHall.Data.Interfaces
{
    public interface ITableRepository
    {
        Task<GetTableDto> CreateTable(CreateTableDto table);

        Task DeleteTable(Guid tableId);

        Task<GetTableDto> GetTableById(Guid tableId);

        Task<IEnumerable<GetTableDto>> GetAllTables();

        Task<IEnumerable<GetTableDto>> CreateNTables(int nr);
    }
}
