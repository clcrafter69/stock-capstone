using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StockJournal.Models
{
    public class StockTrans
    {

        public int Id { get; set; }

        public string TransType { get; set; }  //type--buy or sell
        public double Amount { get; set; }  //total amount of stock
        public int NumShares { get; set; }  //number of shares

        public string StockName { get; set; }
        public string Ticker { get; set; }
        public double StopLoss { get; set; }  //in percentage or amount but stored in dollars

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime TransDate { get; set; }  //date of transaction


        //navigation properties

        public int StockUserId { get; set; }
        public StockUser StockUser { get; set; }
    }
}
