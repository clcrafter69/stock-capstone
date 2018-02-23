using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StockJournal.Entities
{
    public class TransMap
    {
        public int Id { get; set; }

        public string TransType { get; set; }  //type--buy or sell
        public double Amount { get; set; }  //total amount of stock
        public int NumShares { get; set; }  //number of shares

        public int NumSharesPurch { get; set; } //#of buys }
        public int NumSharesSold { get; set; } //# of sells
        public string StockName { get; set; }
        public string Ticker { get; set; }
        public double StopLoss { get; set; }  //in percentage or amount but stored in dollars

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime TransDate { get; set; }  //date of transaction

        public double TotCash { get; set; }
        public double TotCommission { get; set; }

        [Display(Name = "Total Portfolio Balance")]
        public double TotalPortfolio { get; set; }

        [Display(Name = "Total Equity Balance")]
        public double TotEquity { get; set; }  //total equity balance

        public double Rate { get; set; }
        public double AvgShares { get; set; }
        public double AvgRisk { get; set; } //sum(stoploss)/# of buys
        
        //public double TotPortfolio { get; set; }
    }
}
