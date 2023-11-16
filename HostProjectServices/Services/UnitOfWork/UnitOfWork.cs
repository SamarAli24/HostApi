using Data.Models;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        #region fields
        private hosteduContext _context;
        #endregion

        #region ctor
        public UnitOfWork(hosteduContext context)
        {
            _context = context; 
        }
        #endregion

        #region methods

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        public IDbContextTransaction BeginTransaction()
        {
            return _context.Database.BeginTransaction();
        }
        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _context.Database.BeginTransactionAsync();
        }

        public Task<int> SaveChangesCheckerApprovalAsync()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
