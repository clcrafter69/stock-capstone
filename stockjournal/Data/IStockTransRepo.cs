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
        List<StockTrans> FilterStockTrans(int Id,string stockticker, string transDate);
        List<StockTrans> FilterStockTrans(int Id, string transDate);
        void UpdateStock(StockTrans changedStock);
        void DeleteStock(StockTrans deletedStock);
        //retrieve list based on criteria



    }
}
