using System;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Text;
using Newtonsoft.Json;
namespace Stock_account_management
{
    class StockAccountManager
    {
        //File path for JSON file
        private string FILE_PATH = @"C:\Users\DELL\source\repos\StockAccountManagement\StockAccountManagement\StockSummary.json";

        //Variable
        internal int userInputForStocks;

        //Intialise Stocks List
        List<Stock> stocks = new List<Stock>();

        //Constructor
        public StockAccountManager()
        {
            //Display Options and take from User
            Console.WriteLine("Choose an Option : ");
            Console.WriteLine("------------------------");
            Console.WriteLine("Press 1 : Add Stock");
            Console.WriteLine("Press 2 : Display Stocks");
            Console.WriteLine("Press 3 : Exit");
            Console.WriteLine("------------------------");
            userInputForStocks = int.Parse(Console.ReadLine());

            switch (userInputForStocks)
            {
                case 1:
                    AddStock(); //Calling Function AddStock
                    Console.WriteLine("-------------------------------");
                    Console.WriteLine("Do you Want to Add More stock .?");
                    Console.WriteLine("Press 1 for : Yes");
                    Console.WriteLine("Press 2 for : No");
                    Console.WriteLine("-------------------------------");
                    int input = int.Parse(Console.ReadLine());


                    if (input == 1)
                        AddStock();
                    else if (input == 2)
                    {
                        Console.WriteLine("Thank You !");
                        StockAccountManager stockAccountManager1 = new StockAccountManager();
                    }
                    else
                        Console.WriteLine("Invalid Input : ");
                    StockAccountManager stockAccountManager = new StockAccountManager();
                    break;

                case 2: //Display All Stocks
                    Display();
                    break;

                case 3: //Exit
                    System.Environment.Exit(0);
                    break;

                default:
                    Console.WriteLine("Invalid Input!! Try Again");
                    StockAccountManager smDefault = new StockAccountManager();
                    break;
            }
        }
        public void AddStock()
        {
            //taking User Inputs for stocks
            Console.WriteLine("Enter Stock Name : ");
            string stockName = Console.ReadLine();

            Console.WriteLine("Enter Number of Stocks : ");
            int stockQuantity = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter Price Of a Stock : ");
            double stockPrice = double.Parse(Console.ReadLine());

            //Deserialize Data of JSON file
            string datafile = File.ReadAllText(FILE_PATH);
            var returnDataObj = JsonConvert.DeserializeObject<List<Stock>>(datafile);

            //Set value in Object
            Stock stockObject = new Stock { _stockName = stockName, _stockPrice = stockPrice, _stockQuantity = stockQuantity, _stockTotalPrice = (stockPrice * stockQuantity) };
            stocks.Add(stockObject); // Add data in list

            if (returnDataObj != null)
            {
                foreach (var item in returnDataObj)
                {
                    Stock stockObject1 = new Stock
                    {
                        _stockName = item._stockName,
                        _stockPrice = item._stockPrice,
                        _stockQuantity = item._stockQuantity,
                        _stockTotalPrice = (item._stockPrice * item._stockQuantity)
                    };
                    //JSON file data into list
                    stocks.Add(stockObject1);
                }

            }

            //Serialize JSON 
            string json = JsonConvert.SerializeObject(stocks);
            File.WriteAllText(FILE_PATH, json);
            Console.WriteLine("-------------------------------");
            Console.WriteLine(stockName + " Added Successfully");
        }

        public void Display()
        {
            //variable
            int index = 0;
            double totalInvestment = 0;

            //Deserialize Data of JSON file
            string datafile = File.ReadAllText(FILE_PATH);
            List<Stock> returnDataObj = JsonConvert.DeserializeObject<List<Stock>>(datafile);

            Console.WriteLine("------------------------");
            Console.WriteLine(" :: STOCKS REPORT :: ");
            Console.WriteLine("------------------------");

            //Display Stocks
            foreach (var item in returnDataObj)
            {
                index++;
                Console.WriteLine("Stock Number {0}", index);
                Console.WriteLine("-----------------");
                Console.WriteLine("Stock Name : " + item._stockName);
                Console.WriteLine("Stock Price : " + item._stockPrice);
                Console.WriteLine("Stock Quantity : " + item._stockQuantity);
                Console.WriteLine("Stock Total Price : " + item._stockTotalPrice);
                Console.WriteLine("------------------------------");

                //Calculating Total Investment
                totalInvestment += item._stockTotalPrice;
            }
            //Display Total Investment
            Console.WriteLine("Total investment in Stocks : " + totalInvestment);
            Console.WriteLine("------------------------------------------------");
        }
    }
}

