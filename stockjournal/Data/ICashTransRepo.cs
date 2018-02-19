using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StockJournal.Models;

namespace StockJournal.Data
{
   public interface ICashTransRepo
    {
        CashTrans AddCashTrans(CashTrans stock);
        CashTrans GetCashTrans(int Id);
        List<CashTrans> ListAllTrans();
        List<CashTrans> FilterCashTrans( string transDate, string transType);
        //retrieve list based on criteria
    }
}
