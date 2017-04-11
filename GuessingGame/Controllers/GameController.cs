using GuessingGame.Models;
using GuessingGame.Services;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuessingGame.Controllers
{
    public class GameController : Controller
    {
        // Backing field to hold the injected IRandomNumberGenerator
        private readonly IRandomNumberGenerator _rng;

        // Constructor accepts an instance of something that implements
        // the IRandomNumberGenerator interface. The Named attribute tells
        // ninject to locate a binding named "AdvancedRNG"
        public GameController(
            [Named("AdvancedRNG")]
            IRandomNumberGenerator rng)
        {
            _rng = rng;
        }

        /// <summary>
        /// Determine whether a guess matches what is stored in the user's session.
        /// </summary>
        /// <param name="guess"></param>
        /// <remarks>
        /// Syntax without the curly braces and return is called an expression-bodied member. The logic
        /// assumes a value named &quot;Answer&quot; is already stored in the session and casts the value
        /// to an integer for comparison purposes. If the session has expired this can throw so a good
        /// exercise would be to better handle any exceptions that occur if the session expires.
        /// </remarks>
        /// <returns>A value indicating whether the guess matches the value stored in the user's session</returns>
        private bool GuessWasCorrect(int guess) =>
            guess == (int)Session["Answer"];

        public ActionResult Index()
        {
            // Generate a random number between 1 and 10 and store it
            // in the user's session. This is unique for each user.
            Session["Answer"] = _rng.GetNext(1, 10);

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(GameModel model)
        {
            // Before getting this far in execution, the MVC framework evaluates the various
            // validation rules we've defined against the model. If any of them fail then
            // ModelState.IsValid will return false thus preventing us from checking for
            // whether the guess matches what's stored in the user's session.
            if (ModelState.IsValid)
            {
                ViewBag.Win = GuessWasCorrect(model.Guess);
            }

            return View(model);
        }
    }
}