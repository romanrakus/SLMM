using System;
using System.Collections.Generic;
using System.Linq;

namespace SLMM.Common.Extensions
{
    public class Ensure
    {
        public static void NotNull<T>(T argument, string argumentName) where T : class
        {
            if (argument == null)
                throw new ArgumentNullException(argumentName);
        }

        public static void NotEmpty(string argument, string argumentName)
        {
            if (string.IsNullOrEmpty(argument))
                throw new ArgumentNullException(argumentName);
        }

        public static void HasElements<T>(IEnumerable<T> argument, string argumentName)
        {
            if (!argument.Any())
                throw new ArgumentException(argumentName);
        }

        public static void NoNullElements<T>(IEnumerable<T> argument, string argumentName)
        {
            if (argument.Any(t => t == null))
                throw new ArgumentException(argumentName);
        }

        public static void GreaterZero(int argument, string argumentName)
        {
            if (argument <= 0)
                throw new ArgumentException($"'{argumentName}' should be greater than zero.");
        }

        public static void GreaterOrEqualZero(int argument, string argumentName)
        {
            if (argument < 0)
                throw new ArgumentException($"'{argumentName}' should be greater or equal zero.");
        }
    }
}