using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockJournal.Models;
using StockJournal.Data;
using Microsoft.AspNetCore.Authorization;

namespace StockJournal.Controllers
{
    [Authorize]
    public class StockController : Controller
    {
        private readonly IStockTransRepo _stockTransRepo;
        private readonly ICashTransRepo _cashTransRepo;
        private readonly IStockUserRepo _stockUserRepo;
        public StockController( IStockTransRepo stockTransRepo, ICashTransRepo cashTransRepo, IStockUserRepo stockUserRepo)
        {
            _stockTransRepo = stockTransRepo;
            _cashTransRepo = cashTransRepo;
            _stockUserRepo = stockUserRepo;
        }

        // GET: Stock
        public ActionResult Index(int userId)
        {
            StockViewModel stock = new StockViewModel();
            List<StockTrans> stockTransIndex = new List<StockTrans>();
            stockTransIndex = _stockTransRepo.ListAllTrans(userId);
            StockTrans stockFiller = new StockTrans();
            //stockFiller.StockUserId = userId;
            //stockTransIndex.Add(stockFiller);
            //foreach (StockTrans item in stockTransIndex)
            //{
            //    item.StockUserId = userId;
            //}
            //stock.userJournalId = userId;
            //stock.Stock.AddRange(stocks);

            return View(stockTransIndex);
        }

        // GET: Stock/Details/5
        public ActionResult Details(int id)
        {
            StockTrans stock = _stockTransRepo.GetStockTrans(id);
            StockViewModel stockview = new StockViewModel();
            stockview.userJournalId = stock.StockUserId;
            stockview.Stock = stock;
            return View(stockview);
        }

        // GET: Stock/Create
        public ActionResult Create(int userId)
        {
            StockViewModel stock = new StockViewModel();
            stock.userJournalId = userId;
            return View(stock);
            //return View();
        }

        // POST: Stock/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(StockViewModel newStock, IFormCollection collection)
        {
            try
            {
                //newUser.user.Email = newUser.loginUserId;
                // TODO: Add insert logic here
                //newStock = this.User.Identity.Name;
                if (ModelState.IsValid)
                {
                    _stockTransRepo.AddStockTrans(newStock.Stock);

                    //update stock & total portfolio balances
                    _stockUserRepo.UpdateBalances("stockBal", newStock.Stock.Amount, newStock.Stock.StockUserId);
                   
                    ModelState.Clear();
                }
                var newStockId= newStock.Stock.StockUserId;
                StockViewModel newStockView = new StockViewModel();

                newStockView.userJournalId = newStockId;
                return View(newStockView);
                //return View();

                //return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Stock/Edit/5
        public ActionResult Edit(int id)
        {
            
            StockViewModel newStockView = new StockViewModel();
            StockTrans stock = _stockTransRepo.GetStockTrans(id);
            var newStockId = stock.StockUserId;
            newStockView.Stock = stock;
            newStockView.userJournalId = newStockId;

            return View(newStockView);
        }



        //public ActionResult Edit(int id, StockTrans changedStock, IFormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here
        //        _stockTransRepo.UpdateStock(changedStock);
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
        // POST: Stock/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, StockViewModel changedStock, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                if (ModelState.IsValid)
                {
                    _stockTransRepo.UpdateStock(changedStock.Stock);
                    // return RedirectToAction(nameof(Index));
                    //ModelState.Clear();

                }

                StockViewModel newStockView = new StockViewModel();
                newStockView.userJournalId = changedStock.Stock.StockUserId;
                //newStockView.Stock = changedStock;
                return View(newStockView);
            }
            catch
            {
                return View();
            }
        }

        // GET: Stock/Delete/5
        public ActionResult Delete(int id)
        {
            StockTrans stock = _stockTransRepo.GetStockTrans(id);
            StockViewModel stockview = new StockViewModel();
            stockview.userJournalId = stock.StockUserId;
            stockview.Stock = stock;
            return View(stockview);
        }

        // POST: Stock/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                StockTrans stock = _stockTransRepo.GetStockTrans(id);
                StockViewModel stockview = new StockViewModel();
                stockview.userJournalId = stock.StockUserId;
                //stockview.Stock = stock;
                if (ModelState.IsValid)
                {
                    
                    _stockTransRepo.DeleteStock(_stockTransRepo.GetStockTrans(id));
                    ModelState.Clear();
                }
                
                return View(stockview);

                //return RedirectToAction(nameof(Index), new { id = stockview.userJournalId });
            }
            catch
            {
                return View();
            }
        }
    }
}