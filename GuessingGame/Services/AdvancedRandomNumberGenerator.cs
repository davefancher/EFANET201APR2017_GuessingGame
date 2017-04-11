using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace GuessingGame.Services
{
    public class AdvancedRandomNumberGenerator
        : IRandomNumberGenerator
    {
        int IRandomNumberGenerator.GetNext(int min, int max)
        {
            var buffer = new byte[4];

            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(buffer);
            }

            var rand = Math.Abs(BitConverter.ToInt32(buffer, 0));

            return Math.Abs(min + (rand % (max - min + 1)));
        }
    }
}