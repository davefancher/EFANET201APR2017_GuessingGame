using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuessingGame.Services
{
    public class BasicRandomNumberGenerator
        : IRandomNumberGenerator
    {
        private static readonly Random _rng = new Random();

        int IRandomNumberGenerator.GetNext(int min, int max) =>
            _rng.Next(min, max);
    }
}