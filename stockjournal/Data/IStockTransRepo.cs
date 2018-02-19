using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StockJournal.Models;

namespace StockJournal.Data
{
    public interface IStockTransRepo
    {
        StockTrans AddStockTrans(StockTrans stock);
        StockTrans GetStockTrans(int Id);
        List<StockTrans> ListAllTrans(int userId);
        List<StockTrans> FilterStockTrans(string stockticker, string transDate, string transType);
        //retrieve list based on criteria



    }
}
