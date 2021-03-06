﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StockJournal.Models
{
    public class StockTrans
    {

        public int Id { get; set; }

        [Display(Name = "Transaction Type")]
        public string TransType { get; set; }  //type--buy or sell
        public double Amount { get; set; }  //total amount of stock

        [Display(Name = "Shares")]
        public int NumShares { get; set; }  //number of shares

        [Display(Name = "Name")]
        public string StockName { get; set; }
        public string Ticker { get; set; }

        [Display(Name = "Stop Loss")]
        public double StopLoss { get; set; }  //in percentage or amount but stored in dollars

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        [Display(Name = "Transaction Date")]
        public DateTime TransDate { get; set; }  //date of transaction


        //navigation properties

        public int StockUserId { get; set; }
        public StockUser StockUser { get; set; }
    }
}
