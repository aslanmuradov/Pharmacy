using Aptek_Console_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aptek_Console_App.Utils
{
    public static class Helper
    {
        public static void Print(ConsoleColor color, string text)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        public static int ConsoleReadLineInt()
        {
            inputStr:
            string input = Console.ReadLine();
            bool isInt = int.TryParse(input, out int result);
            if (!isInt)
            {
                Print(ConsoleColor.Red, "Rakam giriniz!");
                goto inputStr;
            }                
            else
                return result;            
        }

        public static bool YesNoCheck()
        {            
            askNewSaleTry:
            Print(ConsoleColor.Cyan, "(Y/N)");
            string result = Console.ReadLine();
            if (result.ToUpper() == "Y")
                return true;
            else if (result.ToUpper() == "N")
                return false;
            else
                Print(ConsoleColor.Red, "Yanlis sembol girdiniz asagidaki sembollerden birisini giriniz!");
            goto askNewSaleTry;
        }
    }
}
