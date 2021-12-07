using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DinningHall.Data.Interfaces;
using DinningHall.DTOs;
using DinningHall.Models;
using DinningHall.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace DinningHall.Data
{
    public class TableRepository : ITableRepository
    {
        private readonly DbContextFactory _contextFactory;
        private readonly IMapper _mapper;

        public TableRepository(DbContextFactory contextFactory, IMapper mapper)
        {
            _contextFactory = contextFactory;
            _mapper = mapper;
        }
        public Task<TableDto> CreateTable(CreateTableDto table)
        {
            var context = _contextFactory.Create();
            var tableToCreate = _mapper.Map<Table>(table);
            context.Tables.Add(tableToCreate);
            context.SaveChanges();
            return Task.Run(() => _mapper.Map<TableDto>(tableToCreate));
        }

        public Task DeleteTable(Guid tableId)
        {
            var context = _contextFactory.Create();
            var tableToDelete = context.Tables.FirstOrDefault(t => t.Id == tableId);
            if (tableToDelete == null)
            {
                throw new ArgumentException($"Table with id:{tableId} not found.");
            }

            context.Tables.Remove(tableToDelete);
            context.SaveChanges();
            return Task.CompletedTask;
        }

        public Task<TableDto> GetTableById(Guid tableId)
        {
            var context = _contextFactory.Create();
            var tableToReturn = context.Tables.FirstOrDefault(t => t.Id == tableId);
            if (tableToReturn == null)
            {
                throw new ArgumentException($"Table with id:{tableId} not found.");
            }

            return Task.Run(() => _mapper.Map<TableDto>(tableToReturn));
        }

        public Task<IEnumerable<TableDto>> GetAllTables()
        {
            var context = _contextFactory.Create();
            return Task.Run(() => _mapper.Map<IEnumerable<TableDto>>(context.Tables.Include(t => t.Order)));
        }

        public Task<IEnumerable<TableDto>> CreateNTables(int nr)
        {
            var context = _contextFactory.Create();
            var tables = new List<Table>();
            while(nr > 0)
            {
                tables.Add(context.Tables.Add(new Table {IsFree = true, TableStatus = TableStatus.WaitToOrder}).Entity);
                nr--;
            }

            context.SaveChanges();
            return Task.Run(() =>_mapper.Map<IEnumerable<TableDto>>(tables));
        }

        public Task<TableDto> UpdateTable(TableDto table)
        {
            var context = _contextFactory.Create();
            var tableToUpdate = context.Tables.FirstOrDefault(t => t.Id == table.Id);
            if (tableToUpdate == null)
            {
                throw new ArgumentException($"Table with id: {table.Id} not found.");
            }

            tableToUpdate.IsFree = table.IsFree;
            tableToUpdate.TableStatus = tableToUpdate.TableStatus;
            context.SaveChanges();
            return Task.Run(() => _mapper.Map<TableDto>(tableToUpdate));
        }
    }
}
