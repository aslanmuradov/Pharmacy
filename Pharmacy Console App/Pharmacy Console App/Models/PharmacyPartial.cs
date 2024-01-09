using Aptek_Console_App.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aptek_Console_App.Models
{
    partial class Pharmacy
    {
        public void AddDrugToPharmacy()
        {
            addDrug:
            Drug drug = new Drug();

            
            drugName:
            Helper.Print(ConsoleColor.Blue, "Ilacın ismini giriniz:");
            string drugName = Console.ReadLine();
            if (drugName.TrimStart().Count() == 0)
            {
                Helper.Print(ConsoleColor.Red, "Ilacin ismi bos birakilamaz!");
                goto drugName;
            }
            drug.Name = drugName;

            
            drugType:
            Helper.Print(ConsoleColor.Blue, "Ilacin tipini giriniz:");
            string typeName = Console.ReadLine();
            if (typeName.TrimStart().Count() == 0)
            {
                Helper.Print(ConsoleColor.Red, "Ilacin tipi bos birakilamaz!");
                goto drugType;
            }
            bool checkType = false;
            foreach (DrugType type in _types)
            {
                if (type.TypeName == typeName)
                {
                    checkType = true;
                    drug.Type = type;
                }
            }
            if (!checkType)
            {
                DrugType drugType = new DrugType(typeName);
                drug.Type = drugType;
                _types.Add(drugType);
            }

            
            if (_drugs.Count() == 0)
            {
                Helper.Print(ConsoleColor.Blue, "Ilacin fiyatini giriniz:");
                int intDrugPrice = Helper.ConsoleReadLineInt();
                drug.Price = intDrugPrice;
            }
            foreach (Drug item in _drugs)
            {
                if (item.Name == drugName && item.Type.TypeName == typeName)
                {
                    drug.Price = item.Price;
                    break;
                }
                else
                {
                    Helper.Print(ConsoleColor.Blue, "Ilacin fiyatini giriniz:");
                    int intDrugPrice = Helper.ConsoleReadLineInt();
                    drug.Price = intDrugPrice;
                    break;
                }
            }
            
            
            Helper.Print(ConsoleColor.Blue, "Ilacin sayini giriniz:");
            int intDrugCount = Helper.ConsoleReadLineInt();
            drug.Count = intDrugCount;

            
            bool checkAdd = true;
            foreach (Drug item in _drugs)
            {
                if (item.Name == drug.Name && item.Type == drug.Type )
                {
                    item.Count += drug.Count;
                    checkAdd = false;
                }
            }
            if (checkAdd)            
                _drugs.Add(drug);            
            Helper.Print(ConsoleColor.Green, "Ilac eklendi.");

            
            Helper.Print(ConsoleColor.DarkCyan, "Eklenecek baska ilac var mi?");
            bool chechAddDrug = Helper.YesNoCheck();
            if (chechAddDrug)
                goto addDrug;
        }

        public void InfoDrug()
        {
            infoDrug:
            Helper.Print(ConsoleColor.Blue, "Bilgi almak istediginiz ilacin ismini giriniz:");
            string drugName = Console.ReadLine();
            if (drugName.TrimStart().Count() == 0)
            {
                Helper.Print(ConsoleColor.Red, "Ilacin ismini yazmadiniz!");
                goto infoDrug;
            }

            
            foreach (Drug drug in _drugs.FindAll(drug => drug.Name.ToUpper().Contains(drugName.ToUpper())))
            {
                Helper.Print(ConsoleColor.Yellow, drug.ToString());
            }

            
            if (_drugs.FindAll(drug => drug.Name.ToUpper().Contains(drugName.ToUpper())).Count() == 0)
            {
                Helper.Print(ConsoleColor.Red, "Girdiginiz isimde ilac yok!");
                Helper.Print(ConsoleColor.DarkCyan, "Bilgi almak istediginiz baska ilac var mi?");
                bool checkInfoDrug = Helper.YesNoCheck();
                if (checkInfoDrug)
                    goto infoDrug;             
            }
        }

        public void ShowDrugItems()
        {
            foreach (DrugType type in _types)
            {
                Helper.Print(ConsoleColor.Magenta, type.ToString());
                foreach (Drug drug in _drugs)
                {
                    if (drug.Type == type)
                        Helper.Print(ConsoleColor.Yellow, "    " + drug.ToString());
                }
            }
            if(_drugs.Count()==0)
                Helper.Print(ConsoleColor.Red, "Eczanede ilac yok");
            Helper.Print(ConsoleColor.Green, "Tum ilaclarla ilgili bilgi verildi.");
        }

        public void SaleDrug()
        {
            List<Drug> sellingDrugList = new List<Drug>();
            List<int> drugCountList = new List<int>();

            
            sellDrug:
            Helper.Print(ConsoleColor.Blue, "Satilicak ilacin ismini giriniz:");
            string sellingDrug = Console.ReadLine();
            if (sellingDrug.TrimStart().Count() == 0)
            {
                Helper.Print(ConsoleColor.Red, "Ilacin isimini girmediniz!");
                goto sellDrug;
            }

            chooseDrug:
            if (_drugs.FindAll(drug => drug.Name.ToUpper().Contains(sellingDrug.ToUpper())).Count() == 0)
                Helper.Print(ConsoleColor.Red, "Eczanede bu veya buna benzer bir ilac yok!");

            if (_drugs.FindAll(drug => drug.Name.ToUpper().Contains(sellingDrug.ToUpper())).Count() != 0)
            {
                Helper.Print(ConsoleColor.DarkCyan, "Istedugunuz ilac asagidakilerden hangisi?");
                foreach (Drug drug in _drugs.FindAll(drug => drug.Name.ToUpper().Contains(sellingDrug.ToUpper())))
                {
                    Helper.Print(ConsoleColor.Yellow, $"({drug.Id}) Ilacin ismi: {drug.Name} | Ilacin tipi: {drug.Type.TypeName}");
                }
            }

            Helper.Print(ConsoleColor.DarkGray, "Yukarda (istediginiz) ilac yoksa 'S' harfini giriniz.");

            Helper.Print(ConsoleColor.DarkGray, "Satisi iptal etmek icin 'L' harfini giriniz.");
            string drugIdStr = Console.ReadLine();
            if (drugIdStr.ToUpper() == "S")
                goto sellDrug;
            if (drugIdStr.ToUpper() == "L")
            {
                Helper.Print(ConsoleColor.DarkRed, "Satis iptal edildi!!!");
                return;
            }
            if (int.TryParse(drugIdStr, out int drugIdInt))
            {
                foreach (Drug drug in _drugs.FindAll(drug => drug.Name.ToUpper().Contains(sellingDrug.ToUpper())))
                {
                    if (drug.Id == drugIdInt)
                        sellingDrug = drug.Name;
                }

                List<int> idList = new List<int>();
                foreach (Drug drug in _drugs.FindAll(drug => drug.Name.ToUpper().Contains(sellingDrug.ToUpper())))
                {
                    idList.Add(drug.Id);
                }
                if (!idList.Contains(drugIdInt))
                {
                    Helper.Print(ConsoleColor.Red, "Yanlis sembolu girdiniz!");
                    goto chooseDrug;
                }
            }
            else
            {
                Helper.Print(ConsoleColor.Red, "Yanlis sembolu girdiniz!");
                goto chooseDrug;
            }

            
            if (_drugs.FindAll(drug => drug.Name.ToUpper().Contains(sellingDrug.ToUpper())).Count() == 0)
            {
                Helper.Print(ConsoleColor.Red, "Bu isimde ilac yok!");
                Helper.Print(ConsoleColor.DarkCyan, "Satilicak baska ilac var mi?");
                bool checkSellDrug = Helper.YesNoCheck();
                if (checkSellDrug)
                    goto sellDrug;
                else
                    return;
            }

            
            Helper.Print(ConsoleColor.Blue, "Satilicak ilacin sayisini giriniz:");
            int drugCount = Helper.ConsoleReadLineInt();

            
            bool checkSameDrug = false;
            foreach (Drug _drug in sellingDrugList)
            {
                if (_drug.Name == sellingDrug)
                {
                    checkSameDrug = true;
                }
            }
            foreach (Drug drug in _drugs)
            {   
                if (drug.Name.ToUpper() == sellingDrug.ToUpper() && !checkSameDrug)
                    sellingDrugList.Add(drug);
            }

            
            if (checkSameDrug)
            {
                foreach (Drug drug in sellingDrugList)
                {
                    if (drug.Name == sellingDrug)
                    {
                        drugCountList[sellingDrugList.IndexOf(drug)] += drugCount;
                    }
                }
            }
            if(!checkSameDrug)
                drugCountList.Add(drugCount);

            
            Helper.Print(ConsoleColor.DarkCyan, "Satilicak baska ilac var mi?");
            bool checkAnotherDrug = Helper.YesNoCheck();
            if (checkAnotherDrug)
                goto sellDrug;

            
            checkDrugInPharmacy:
            foreach (Drug drug in sellingDrugList)
            {
                if (drug.Count == 0)
                {
                    Helper.Print(ConsoleColor.Red, $"{drug.Name} isimli ilac bitmis!");
                    drugCountList.Remove(drugCountList[sellingDrugList.IndexOf(drug)]);
                    sellingDrugList.Remove(drug);                                           
                }

                if (sellingDrugList.Count() == 0)
                {
                    Helper.Print(ConsoleColor.DarkRed, "Satis iptal edildi!!!");
                    return;
                }
            }
            foreach (Drug drug in sellingDrugList)
            {
                if (0 < drug.Count && drug.Count < drugCountList[sellingDrugList.IndexOf(drug)])
                {
                    Helper.Print(ConsoleColor.DarkRed, $"{drug.Name} isimli ilacadan sadece {drug.Count} adet kalmis. Musteri almak istiyor mu?");
                    bool checkDrugCount = Helper.YesNoCheck();
                    if (checkDrugCount)
                        drugCountList[sellingDrugList.IndexOf(drug)] = drug.Count;
                    else
                    {
                        drugCountList.Remove(drugCountList[sellingDrugList.IndexOf(drug)]);
                        sellingDrugList.Remove(drug);
                        goto checkDrugInPharmacy;
                    }
                }
            }

            
            int priceOfDrugs = 0;
            foreach (Drug drug in sellingDrugList)
            {
                int drugPrice = drug.Price * drugCountList[sellingDrugList.IndexOf(drug)];
                priceOfDrugs += drugPrice;
                Helper.Print(ConsoleColor.Yellow, $"({drug.Id}) İlacin ismi: {drug.Name} " +
                    $"| Satilicak ilacin sayi: {drugCountList[sellingDrugList.IndexOf(drug)]} " +
                    $"| İlacin toplam fiyati: {drugPrice} TL");
            }
            Helper.Print(ConsoleColor.Yellow, $"Toplam fiyat: {priceOfDrugs} TL");

            
            inputMenu:
            Helper.Print(ConsoleColor.DarkYellow, "Asagidaki islemlerden yapmak istediginizin onundeki rakami giriniz!\n" +
                "(1)Satisa devam etemek icin\n" +
                "(2)Satisi iptal etmek icin\n");            
            int menu = Helper.ConsoleReadLineInt();
            switch (menu)
            {
                case 1:
                    case1:
                    foreach (Drug drug in _drugs)
                    {
                        foreach (Drug item in sellingDrugList)
                        {
                            if (drug.Name == item.Name && drug.Type.TypeName == item.Type.TypeName)
                                drug.Count -= drugCountList[sellingDrugList.IndexOf(item)];
                        }
                    }
                    Helper.Print(ConsoleColor.Blue, "Musterinin verdigi miktari giriniz:");
                    int cash = Helper.ConsoleReadLineInt();
                    if (priceOfDrugs > cash || cash < 0)
                    {
                        Helper.Print(ConsoleColor.Red, "Girinlen miktar toplam fiyata esit veya buyuk olmali!");
                        goto case1;
                    }
                    Helper.Print(ConsoleColor.DarkGreen, $"Para ustunun miktari: {cash-priceOfDrugs} TL");
                    break;
                case 2:
                    Helper.Print(ConsoleColor.DarkRed, "Satis iptal edildi!!!");
                    return;
                default:
                    Helper.Print(ConsoleColor.Red, "Menude olan bir rakami giriniz!");
                    goto inputMenu;
            }
        }
    }
}
