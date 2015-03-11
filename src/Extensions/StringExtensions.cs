using System.Linq;

namespace Ltht.TechTest.Extensions
{
    public static class StringExtensions
    {
        public static bool IsPalindrome(this string input)
        {
            input = input.Replace(" ", string.Empty).ToLower();
            var reversed = new string(input.Reverse().ToArray());
            return input == reversed;
        }     
    }
}