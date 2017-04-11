using GuessingGame.Models;
using GuessingGame.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuessingGame.Controllers
{
    public class GameController : Controller
    {
        private readonly IRandomNumberGenerator _rng = new BasicRandomNumberGenerator();

        public ActionResult Index()
        {
            Session["Answer"] = _rng.GetNext(1, 10);

            return View();
        }

        private bool GuessWasCorrect(int guess) =>
            guess == (int)Session["Answer"];

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(GameModel model)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Win = GuessWasCorrect(model.Guess);
            }

            return View(model);
        }
    }
}