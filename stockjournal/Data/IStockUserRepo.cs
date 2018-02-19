using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StockJournal.Models;

namespace StockJournal.Data
{
   public interface IStockUserRepo
    {
        StockUser AddUser(StockUser newUser);
        void DeleteUser(StockUser oldUser);
        StockUser GetUserInfo(string email); //use email to get user
        void UpdateUser(StockUser user);
        List<StockTrans> RetrieveStockTrans(StockUser user);
        List<CashTrans> RetrieveCashTrans(StockUser user);
        void CombineTrans(List<CashTrans> cash, List<StockTrans> stock);
        void UpdateBalances(string typeBal, double amt, int userID);
    }
}
