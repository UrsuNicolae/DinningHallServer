using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DinningHall.Data.Interfaces;
using DinningHall.DTOs;
using DinningHall.Models;
using DinningHall.Models.Enums;

namespace DinningHall.Data
{
    public class TableRepository : ITableRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public TableRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public Task<TableDto> CreateTable(CreateTableDto table)
        {
            var tableToCreate = _mapper.Map<Table>(table);
            _context.Tables.Add(tableToCreate);
            _context.SaveChanges();
            return Task.Run(() => _mapper.Map<TableDto>(tableToCreate));
        }

        public Task DeleteTable(Guid tableId)
        {
            var tableToDelete = _context.Tables.FirstOrDefault(t => t.Id == tableId);
            if (tableToDelete == null)
            {
                throw new ArgumentException($"Table with id:{tableId} not found.");
            }

            _context.Tables.Remove(tableToDelete);
            _context.SaveChanges();
            return Task.CompletedTask;
        }

        public Task<TableDto> GetTableById(Guid tableId)
        {
            var tableToReturn = _context.Tables.FirstOrDefault(t => t.Id == tableId);
            if (tableToReturn == null)
            {
                throw new ArgumentException($"Table with id:{tableId} not found.");
            }

            return Task.Run(() => _mapper.Map<TableDto>(tableToReturn));
        }

        public Task<IEnumerable<TableDto>> GetAllTables()
        {
            return Task.Run(() => _mapper.Map<IEnumerable<TableDto>>(_context.Tables));
        }

        public Task<IEnumerable<TableDto>> CreateNTables(int nr)
        {
            var tables = new List<Table>();
            while(nr > 0)
            {
                tables.Add(_context.Tables.Add(new Table {IsFree = true, TableStatus = TableStatus.WaitToOrder}).Entity);
                nr--;
            }

            _context.SaveChanges();
            return Task.Run(() =>_mapper.Map<IEnumerable<TableDto>>(tables));
        }

        public Task<TableDto> UpdateTable(TableDto table)
        {
            var tableToUpdate = _context.Tables.FirstOrDefault(t => t.Id == table.Id);
            if (tableToUpdate == null)
            {
                throw new ArgumentException($"Table with id: {table.Id} not found.");
            }

            tableToUpdate.IsFree = table.IsFree;
            tableToUpdate.TableStatus = tableToUpdate.TableStatus;
            _context.SaveChanges();
            return Task.Run(() => _mapper.Map<TableDto>(tableToUpdate));
        }
    }
}
