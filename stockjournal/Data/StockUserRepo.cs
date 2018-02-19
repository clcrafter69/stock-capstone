using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StockJournal.Models;
using Microsoft.EntityFrameworkCore;

namespace StockJournal.Data
{
    public class StockUserRepo : IStockUserRepo
    {
        protected readonly StockContext _dbStockContext;

        public StockUserRepo(StockContext dbContext)
        {
            _dbStockContext = dbContext;
        }

        public StockUser AddUser(StockUser newUser)
        {
            _dbStockContext.User.Add(newUser);
            _dbStockContext.SaveChanges();
            return (newUser);
         //   throw new NotImplementedException();
        }

        public void UpdateUser(StockUser user)
        {
            _dbStockContext.Entry(user).State = EntityState.Modified;
            _dbStockContext.SaveChanges();
            throw new NotImplementedException();
        }

        public void DeleteUser(StockUser oldUser)
        {
            _dbStockContext.User.Remove(oldUser);
            _dbStockContext.SaveChanges();
            throw new NotImplementedException();
        }

        public void CombineTrans(List<CashTrans> cash, List<StockTrans> stock)
        {
           
            throw new NotImplementedException();
        }

        public void UpdateBalances(string typeBal,double amt,int userID)
        {
           var result= _dbStockContext.User.FirstOrDefault(a => a.Id==userID);
            if (result != null)
            {
                //set up switch statements
                switch (typeBal)
                {
                    case "stockBal":
                        result.TotEquity += amt;
                        break;
                    case "cashBal":
                        result.TotCash += amt;
                        break;
                }
                //update total balances

                result.TotPortfolio += amt;
                _dbStockContext.SaveChanges();
            }

        }

        public StockUser GetUserInfo(string email)
        {
            //find user by using unique email address
            ////return _dbStockContext.User.Find(email);
            return _dbStockContext.User.Where(a => a.Email == email).FirstOrDefault();
            throw new NotImplementedException();
        }

        public List<CashTrans> RetrieveCashTrans(StockUser user)
        {
            return _dbStockContext.Cash.Where(a => a.StockUserId == user.Id).OrderBy(a => a.TransDate)
                .ToList();

            throw new NotImplementedException();
        }

        public List<StockTrans> RetrieveStockTrans(StockUser user)
        {
            return _dbStockContext.Stock.Where(a => a.StockUserId == user.Id).OrderBy(a => a.TransDate)
               .ToList();

            throw new NotImplementedException();
        }

       
    }
}
