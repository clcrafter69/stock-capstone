using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StockJournal.Models
{
    public class StockUser
    {

        public int Id { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LName { get; set; }

        [Required]
        public string Email { get; set; }


        
        [Display(Name = "Total Cash Balance")]
        public double TotCash { get; set; }  //cash balance

       
        [Display(Name = "Total Equity Balance")]
        public double TotEquity { get; set; }  //total equity balance

       
        [Display(Name = "Total Portfolio Balance")]
        public double TotPortfolio { get; set; }


        //navigation properties

        public List<StockTrans> StockTrans { get; set; } = new List<StockTrans>();




    }
}
