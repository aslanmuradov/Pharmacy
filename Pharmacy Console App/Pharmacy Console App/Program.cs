using Aptek_Console_App.Models;
using Aptek_Console_App.Utils;
using System;

namespace Aptek_Console_App
{
    class Program
    {
        static void Main(string[] args)
        {
            Pharmacy pharmacy = new Pharmacy("Eczane");
            while (true)
            {
                Helper.Print(ConsoleColor.Green, $"{pharmacy.Name} Program Basladi.");
                menu:
                Helper.Print(ConsoleColor.DarkYellow, "Asagidaki islemlerden yapmak istediginizin onundeki rakami giriniz.\n" +
                    "(1)Ilac eklemek\n" +
                    "(2)Ilacla ilgili bilgi almak\n" +
                    "(3)Tum ilaclarin bilgisini yazdirmak\n" +
                    "(4)Satis islemi baslatmak\n" +
                    "(5)Programi sonlandir");
                int menu = Helper.ConsoleReadLineInt();
                
                switch (menu)
                {
                    case 1:
                        pharmacy.AddDrugToPharmacy();
                        goto menu;
                    case 2:
                        pharmacy.InfoDrug();
                        goto menu;
                    case 3:
                        pharmacy.ShowDrugItems();
                        goto menu;
                    case 4:
                        pharmacy.SaleDrug();
                        goto menu;
                    case 5:
                        Helper.Print(ConsoleColor.DarkGreen, "Program sonlandirildi.");
                        break;
                    default:
                        Helper.Print(ConsoleColor.Red, "Menude olmayan bir sayi girdiniz!");
                        goto menu;
                }
                break;
            }
        }
    }
}
