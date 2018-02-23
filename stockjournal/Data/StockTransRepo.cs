using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StockJournal.Models;
using Microsoft.EntityFrameworkCore;

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
           

            int dayNumber = DateTime.Now.DayOfYear;
            int year = DateTime.Now.Year;
            DateTime theDate = new DateTime(year, 1, 1).AddDays(dayNumber - 1);

            //30 days
            DateTime month = DateTime.Today.AddDays(-29);
            //7 days
            DateTime week = DateTime.Today.AddDays(-6);
            //today
            DateTime present = DateTime.Today;

            _dbStockContext.Stock.Add(stock);
            _dbStockContext.SaveChanges();

            return stock;
            //throw new NotImplementedException();
        }

        public DateTime determineDate(string dateParm)
        {
            DateTime convertDate = new DateTime();
            switch (dateParm)
            {
                case "YTD":
                    int dayNumber = DateTime.Now.DayOfYear;
                    int year = DateTime.Now.Year;
                    //convertDate= new DateTime(year, 1, 1).AddDays(dayNumber - 1);
                    convertDate = new DateTime(year, 1, 1);
                    break;
                case "MONTH":
                    //30 days
                    convertDate = DateTime.Today.AddDays(-29);
                    break;
                case "WEEK":
                    //7 days
                    convertDate = DateTime.Today.AddDays(-6);
                    break;
                case "TODAY":
                    //today
                    convertDate = DateTime.Today;
                    break;
                default:
                    break;
            }
            return convertDate;
        }
        public List<StockTrans> FilterStockTrans(int userId, string transDate,string stockTicker)
        {
            DateTime convertDate;
            if (transDate == "YTD") {
                convertDate = determineDate(transDate);
                return _dbStockContext.Stock.Include(a => a.StockUser).Where(a =>  a.Ticker == stockTicker && a.TransDate <= convertDate && a.StockUserId == userId).OrderBy(a => a.TransDate)
                  .ToList();
            }
            else if(transDate == "MONTH")
            {
                convertDate = determineDate(transDate);
                return _dbStockContext.Stock.Include(a => a.StockUser).Where(a => a.Ticker == stockTicker &&(a.TransDate >= convertDate && a.TransDate <= DateTime.Today) && a.StockUserId == userId).OrderBy(a => a.TransDate)
                  .ToList();
            }
            else if (transDate == "WEEK")
            {
                convertDate= determineDate(transDate);
                return _dbStockContext.Stock.Include(a => a.StockUser).Where(a => a.Ticker == stockTicker && (a.TransDate >= convertDate && a.TransDate <= DateTime.Today) && a.StockUserId == userId).OrderBy(a => a.TransDate)
                  .ToList(); 
            }
            else if(transDate =="TODAY")
            {
                convertDate = determineDate(transDate);
                return _dbStockContext.Stock.Include(a => a.StockUser).Where(a => a.Ticker == stockTicker && (a.TransDate >= convertDate && a.TransDate <= DateTime.Today) && a.StockUserId == userId).OrderBy(a => a.TransDate)
                  .ToList();
            }
            else  ///just return transactions for today
            {
                convertDate = determineDate(transDate);
                return _dbStockContext.Stock.Include(a => a.StockUser).Where(a => a.Ticker == stockTicker && (a.TransDate >= convertDate && a.TransDate <= DateTime.Today) && a.StockUserId == userId).OrderBy(a => a.TransDate)
                  .ToList();
            }

            //return null;

            //throw new NotImplementedException();
        }

        public List<StockTrans> FilterStockTrans(int userId, string transDate)
        {
            //TODO: create calculation for date interpretation
            //TODO: create 

            //DateTime realDate = DateTime.Now;
            //return _dbStockContext.Stock.Where(a => a.TransDate == realDate ).OrderBy(a => a.TransDate)
            //      .ToList();

            //throw new NotImplementedException();

            DateTime convertDate;
            if (transDate == "YTD")
            {
                //Include(a => a.Answers).Where(a => a.Id == id)
                convertDate = determineDate(transDate);
                return _dbStockContext.Stock.Include(a => a.StockUser).Where(a => a.TransDate <= convertDate && a.StockUserId == userId).OrderBy(a => a.TransDate)
                  .ToList();

                //return _dbStockContext.User.
            }
            else if (transDate == "MONTH")
            {
                convertDate = determineDate(transDate);
                return _dbStockContext.Stock.Include(a => a.StockUser).Where(a => (a.TransDate >= convertDate && a.TransDate <= DateTime.Today) && a.StockUserId == userId).OrderBy(a => a.TransDate)
                  .ToList();
            }
            else if (transDate == "WEEK")
            {
                convertDate = determineDate(transDate);
                return _dbStockContext.Stock.Include(a => a.StockUser).Where(a => (a.TransDate >= convertDate && a.TransDate <= DateTime.Today) && a.StockUserId == userId).OrderBy(a => a.TransDate)
                  .ToList();
            }
            else if (transDate == "TODAY")
            {
                convertDate = determineDate(transDate);
                return _dbStockContext.Stock.Include(a => a.StockUser).Where(a => a.TransDate == convertDate && a.StockUserId == userId).OrderBy(a => a.TransDate)
                  .ToList();
            }
            else  ///just return transactions for all
            {
                convertDate = determineDate(transDate);
                return _dbStockContext.Stock.Include(a => a.StockUser).Where(a => a.StockUserId == userId).OrderBy(a => a.TransDate)
                  .ToList();
            }

        }

        public StockTrans GetStockTrans(int Id)
        {
            return _dbStockContext.Stock.Find(Id);
            throw new NotImplementedException();
        }

        public void UpdateStock(StockTrans changedStock)
        {
            _dbStockContext.Entry<StockTrans>(changedStock).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _dbStockContext.SaveChanges();
        }
        public List<StockTrans> ListAllTrans(int userId)
        {
            return _dbStockContext.Stock.Where(a => a.StockUserId == userId).OrderByDescending(a => a.TransDate)
                .ToList();
            throw new NotImplementedException();
        }


        public void DeleteStock(StockTrans deletedStock){

            _dbStockContext.Stock.Remove(deletedStock);
            _dbStockContext.SaveChanges();
        }
    }
}
