using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StockJournal.Data;
using StockJournal.Models;
using StockJournal.Entities;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StockJournal.Controllers
{
   
    [Produces("application/json")]
    [Route("api/Analyze")]
    [Authorize]
    public class AnalyzeController : Controller
    {
        private readonly IStockTransRepo _stockTransRepo;
        private readonly IStockUserRepo _stockUserRepo;
        private readonly ICashTransRepo _cashTransRepo;
        private List<TransMap> lstTransMap;
        private const double Commission = 5.00;

        public AnalyzeController(IStockTransRepo stockRepo, IStockUserRepo  userRepo, ICashTransRepo cashRepo )
        {
            _stockTransRepo = stockRepo;
            _stockUserRepo = userRepo;
            _cashTransRepo = cashRepo;
        }

        // GET: api/values
        [HttpGet]
        public IActionResult Get()
        {
            //return new string[] { "value1", "value2" };
            var email = User.Identity.Name;
            StockUser user = new StockUser();
            user = _stockUserRepo.GetUserInfo(email);
            return Ok(user.Id);
        }

        // GET api/values/5
        //get the total performance info for logged in user
        [HttpGet("{id}/{dateParm}/{ticker}")]
        public IActionResult  GetTickerPerform(int id, string dateParm,string ticker)
        {

            //select records from StockTransaction table based on date parameter
            //_stockTransRepo.FilterStockTrans()
            //return _stockTransRepo.FilterStockTrans(id, dateParm,ticker);

            var result =  _stockTransRepo.FilterStockTrans(id, dateParm, ticker);
            TransMap stockTransMap = null;
            lstTransMap = new List<TransMap>();

            foreach (var trans in result)
            {
                stockTransMap = new TransMap()
                {
                    Id = trans.Id,
                    TransType = trans.TransType.ToUpper(),
                    TransDate = trans.TransDate,
                    Amount = trans.Amount,
                    NumShares = trans.NumShares,
                    StockName = trans.StockName,
                    Ticker = trans.Ticker,
                    StopLoss = trans.StopLoss,
                    TotalPortfolio = trans.StockUser.TotPortfolio,
                    TotEquity = trans.StockUser.TotEquity,
                    TotCash = trans.StockUser.TotCash
                };
                lstTransMap.Add(stockTransMap);
            }

            //TODO: DoDataCalculations()

            return Ok(DoDataConversions(lstTransMap));

            //return Ok();
        }

        [HttpGet("{id}/{dateParm}")]
        public IActionResult GetTotalPerform(int id, string dateParm)
        {

            //select records from StockTransaction table based on date parameter
            //_stockTransRepo.FilterStockTrans()
            //return _stockTransRepo.FilterStockTrans(id, dateParm);

            var result = _stockTransRepo.FilterStockTrans(id, dateParm);
            TransMap stockTransMap = null;
            lstTransMap = new List<TransMap>();

            foreach (var trans in result)
            {
                stockTransMap = new TransMap()
                {
                    Id = trans.Id,
                    TransType = trans.TransType.ToUpper(),
                    TransDate = trans.TransDate,
                    Amount = trans.Amount,
                    NumShares = trans.NumShares,
                    StockName = trans.StockName,
                    Ticker = trans.Ticker,
                    StopLoss = trans.StopLoss,
                    TotalPortfolio = trans.StockUser.TotPortfolio,
                    TotEquity = trans.StockUser.TotEquity,
                    TotCash = trans.StockUser.TotCash
                };
                lstTransMap.Add(stockTransMap);
            }

            //TODO: DoDataCalculations()

            return Ok(DoDataConversions(lstTransMap));
        }

        public TransMap DoDataConversions(List<TransMap> conversionMap)
        {
            TransMap newConversionMap = new TransMap();
            //get total portfolio balance
            newConversionMap.TotalPortfolio = conversionMap.First().TotalPortfolio;
            //get equity balance
            newConversionMap.TotEquity = conversionMap.First().TotEquity;
            //get cash balance
            newConversionMap.TotCash = conversionMap.First().TotCash;
            // get user Id
            newConversionMap.Id = conversionMap.First().Id;

            foreach (TransMap trans in conversionMap)
            {
               
                if (trans.TransType == "BUY")
                {
                    newConversionMap.Amount -= (trans.Amount-Commission);        //total profit(loss)
                    newConversionMap.NumSharesPurch += trans.NumShares; //# of buys
                    newConversionMap.TotCash -= (trans.Amount - Commission);
                    newConversionMap.TotEquity += (trans.Amount - Commission);

                }
                else
                {
                    newConversionMap.Amount += (trans.Amount-Commission);            //total profit(loss)
                    newConversionMap.NumSharesSold += trans.NumShares; //# of sells
                    newConversionMap.TotCash += (trans.Amount - Commission);
                    newConversionMap.TotEquity -= (trans.Amount - Commission);
                }

                newConversionMap.TotCommission += Commission;  //total commission
                newConversionMap.StopLoss += trans.StopLoss;
            }
            //calculate total cash

            //newConversionMap.TotCash += newConversionMap.Amount;
            //get average shares
            newConversionMap.AvgShares = newConversionMap.NumSharesPurch / conversionMap.Count;

            //get average risk
            newConversionMap.AvgRisk = newConversionMap.StopLoss / conversionMap.Count;
            //get rate
            newConversionMap.Rate = newConversionMap.Amount / (newConversionMap.TotalPortfolio - newConversionMap.Amount);
            return newConversionMap;
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
