using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StockJournal.Models;

namespace StockJournal.Data
{
    public class StockTransRepo : IStockTransRepo
    {
        protected readonly StockContext _dbStockContext;

        public StockTransRepo(StockContext dbStockContext)
        {
            _dbStockContext = dbStockContext;
        }
        public StockTrans AddStockTrans(StockTrans stock)
        {
            _dbStockContext.Stock.Add(stock);
            _dbStockContext.SaveChanges();

            return stock;
            //throw new NotImplementedException();
        }

        public List<StockTrans> FilterStockTrans(string stockTicker, string transDate, string transType)
        {
            //TODO: create calculation for date interpretation
            //TODO: create 
            return _dbStockContext.Stock.Where(a => a.TransType == transType && a.Ticker == stockTicker).OrderBy(a => a.TransDate)
                  .ToList();

            //throw new NotImplementedException();
        }

        public StockTrans GetStockTrans(int Id)
        {
            return _dbStockContext.Stock.Find(Id);
            throw new NotImplementedException();
        }

        public List<StockTrans> ListAllTrans(int userId)
        {
            return _dbStockContext.Stock.Where(a => a.StockUserId == userId).OrderByDescending(a => a.TransDate)
                .ToList();
            throw new NotImplementedException();
        }
    }
}
