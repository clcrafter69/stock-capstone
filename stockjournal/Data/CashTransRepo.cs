using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StockJournal.Models;
using Microsoft.EntityFrameworkCore;

namespace StockJournal.Data
{
    public class CashTransRepo : ICashTransRepo
    {
        protected readonly StockContext _dbCashContext;

        public CashTransRepo(StockContext dbCashContext)
        {
            _dbCashContext = dbCashContext;
        }

        public CashTrans AddCashTrans(CashTrans cash)
        {
            _dbCashContext.Cash.Add(cash);
            _dbCashContext.SaveChanges();

            return cash;

            //throw new NotImplementedException();
        }

        public List<CashTrans> FilterCashTrans( string dateString, string transType)
        {

            //TODO: create calculation for date interpretation
            return _dbCashContext.Cash.Where(a => a.TransType == transType ).OrderBy(a => a.TransDate)
                  .ToList();

            //throw new NotImplementedException();
        }

        public CashTrans GetCashTrans(int Id)
        {
            return _dbCashContext.Cash.Find(Id);
            throw new NotImplementedException();
        }

        public List<CashTrans> ListAllTrans()
        {
            return _dbCashContext.Cash.OrderBy(a => a.TransDate)
                  .ToList();
            throw new NotImplementedException();
        }
    }
}
