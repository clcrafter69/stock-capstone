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
    public class UserController : Controller
    {
        private readonly IStockUserRepo _StockUserRepo;
        private readonly IStockTransRepo _StockTransRepo;
        private readonly ICashTransRepo _CashTransRepo;


        public UserController(IStockUserRepo userRepo, IStockTransRepo stockTransRepo, ICashTransRepo cashTransRepo)
        {
            _StockUserRepo = userRepo;
            _StockTransRepo = stockTransRepo;
            _CashTransRepo = cashTransRepo;
           
        }

        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: User/Create
        public ActionResult Create()
        {
            StockUserViewModel stockUser = new StockUserViewModel();
            stockUser.loginUserId= this.User.Identity.Name;
            stockUser.userTransId = 0;
            // stockUser.user.Id = 0;
            //lookup email--if exists, populate form with data
            StockUser userFound = _StockUserRepo.GetUserInfo(stockUser.loginUserId);

            if(userFound != null)
            {
                //populate view model with stockuser data
                stockUser.user.Id = userFound.Id;
                stockUser.user.FName = userFound.FName;
                stockUser.user.LName = userFound.LName;
                stockUser.user.TotCash = userFound.TotCash;
                stockUser.user.TotEquity = userFound.TotEquity;
                stockUser.user.TotPortfolio = userFound.TotPortfolio;
            }

            return View(stockUser);
        }

        // POST: User/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(StockUserViewModel newUser,IFormCollection collection)
        {
            try
            {
                //newUser.user.Email = newUser.loginUserId;
                // TODO: Add insert logic here
                newUser.loginUserId = this.User.Identity.Name;              
                _StockUserRepo.AddUser( newUser.user);
                newUser.userTransId = newUser.user.Id;

                return View(newUser);

                //return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: User/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: User/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}