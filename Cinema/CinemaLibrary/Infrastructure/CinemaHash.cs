using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CinemaLibrary
{
    internal static class CinemaHash
    {
        public static string HashPass(string input)
        {
            var algorithm = new SHA256CryptoServiceProvider();
            Byte[] inputBytes = Encoding.UTF8.GetBytes(input);

            Byte[] hashedBytes = algorithm.ComputeHash(inputBytes);

            return BitConverter.ToString(hashedBytes);
        }        
    }
}
