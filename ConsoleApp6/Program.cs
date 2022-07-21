using System;
using System.Collections.Generic;

namespace ConsoleApp6
{
    internal class Program
    {
        static void Main(string[] args)
        {
            float[] allPrice = new float[] { 43, 45, 44, 42, 41 };
            float [] allKm = new float[] { 0, 400, 800, 1200, 1600 };

            List<Gas> allGas = GetGas(400, 2000,40,100, allPrice, allKm);
            float totalSum = 0;
            foreach (var item in allGas)
            {
                Console.WriteLine($"Номер заправки - {item.IdGas}" +
                    $"\n Всего литров - {item.TotalLiters}" +
                    $"\n На какую сумму заправка - {item.Summa}");
                totalSum += item.Summa;
            }
            Console.WriteLine($"Общая сумма - {totalSum}");
        }
       
        private static List<Gas> GetGas(float fuel, float km, float literPerKm, int Consumption, float[] allPrice, float[] allKm)
        {
            List<Gas> allGas = new List<Gas>();
            float summa = 0;
            float currentKm = fuel / literPerKm * Consumption; 
            int currentPriceGas = 0;
            bool isComplete = false;
            while (!isComplete)
            {
                float minPriceGas = allPrice[0];

                int currentNumberGas = 0;

                for (int i = 0; i < allKm.Length; i++)
                {
                    if (allKm[i] <= currentKm)
                    {
                        if (minPriceGas >= allPrice[i] && currentKm <= allKm[i])
                        {
                            minPriceGas = allPrice[i];

                            currentNumberGas = i;
                        }
                    }
                }
                float addingFuel = 0;
                for (int i = 0; i < allPrice.Length; i++)
                {
                    if (minPriceGas > allPrice[i])
                    {
                        currentPriceGas = i;
                        break;
                    }
                }
                if (currentPriceGas == 0)
                {
                    addingFuel = km / Consumption * literPerKm;
                }
                else
                {

                    if (currentKm == 0)
                    {
                        addingFuel = allKm[currentPriceGas] / Consumption * literPerKm;
                    }
                    else
                    {
                        if ((allKm[currentPriceGas] - currentKm) != 0)
                        {
                            addingFuel = (allKm[currentPriceGas] - currentKm) / Consumption * literPerKm;
                        }
                        else
                        {
                            addingFuel = (km - currentKm) / Consumption * literPerKm;
                        }
                    }
                }
                summa = (minPriceGas * addingFuel) + summa;
                currentKm += addingFuel / literPerKm * Consumption;
                //Console.WriteLine($"Заправка #{currentNumberGas + 1}");
                //Console.WriteLine($"Колличесво литров заправленно - {addingFuel}");
                //Console.WriteLine($"Общая стоимость заправки - {minPriceGas * addingFuel}");

                Gas gas = new Gas()
                {
                    IdGas = currentNumberGas + 1,
                    TotalLiters = addingFuel,
                    Summa = minPriceGas * addingFuel
                };
                

                addingFuel = 0;
                if (currentKm == km)
                {
                    isComplete = true;
                }
                allGas.Add(gas);

            }
            //Console.WriteLine($"Общая сумма - {summa}");
            return allGas;
        }
    }
}
