﻿using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using WarehouseService;

namespace ST4_ImplementationExamples
{
    public class SOAP
    {
        public SOAP()
        {
        }

        //Check out the 'Connected Services>WarehouseService>Reference.cs' file to see autogenerated code. Read green text at the top.

        //runner
        public async Task RunExample()
        {
            //instatiate web service from 'Connected Services' reference through Visual Studio tool
            //var service = new EmulatorServiceClient();

            //print response of GetInventoryAsync()
            //var response = await service.GetInventoryAsync();
            //Console.WriteLine(response);

            //var service = new EmulatorServiceClient();

            /*bool run = true;
            while (run)
            {
                Console.WriteLine("Select an option.");
                Console.WriteLine("1> Get Inventory");
                Console.WriteLine("2> Pick Item");
                Console.WriteLine("3> Insert Item");
                Console.WriteLine("4> Exit");

                //int input = Convert.ToInt32(Console.ReadLine());

                string input = Console.ReadLine();
                int number;
                Int32.TryParse(input, out number);

                switch (number)
                {
                    case 2:
                        Console.WriteLine("Input Item Number:");
                        int pickItemNum = Convert.ToInt32(Console.ReadLine());
                        var pickRe = await service.PickItemAsync(pickItemNum);
                        Console.WriteLine(pickRe);
                        break;
                    case 3:
                        Console.WriteLine("Input Item to Insert & Name");
                        int insertNum = Convert.ToInt32(Console.ReadLine());
                        string insertName = Console.ReadLine();
                        var insertRe = await service.InsertItemAsync(insertNum, insertName);
                        Console.WriteLine(insertRe);
                        break;
                    case 4:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid Input!");
                        break;
                }*/

            /*while (true)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:8082/v1/status/");

                    HttpResponseMessage response = client.GetAsync("").Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var contents = await response.Content.ReadAsStringAsync();
                        Console.WriteLine(contents);
                        string s = contents;
                        string[] values = s.Split(',');
                        var operation = values[1].Trim(new[] {'"', ':'}).Remove(0, 15);
                        var state = Convert.ToInt32(values[2].Remove(0, 8));

                        Console.WriteLine("Operation: " + operation + "\n" + "State: " + state);
                    }
                }


                Thread.Sleep(2000);
            }*/
        }
        public async void GetStatus()
        {
            var service = new EmulatorServiceClient();
            var response = await service.GetInventoryAsync();
            Console.WriteLine(response);
        }

        public async void PickSingleItem()
        {
            var service = new EmulatorServiceClient();
            Console.WriteLine("Input Item Number:");
            int pickItemNum = Convert.ToInt32(Console.ReadLine());
            var pickRe = await service.PickItemAsync(pickItemNum);
            Console.WriteLine(pickRe);
        }

        public async void InsertSingleItem()
        {
            var service = new EmulatorServiceClient();
            Console.WriteLine("Input Item to Insert & Name");
            int insertNum = Convert.ToInt32(Console.ReadLine());
            string insertName = Convert.ToString(Console.ReadLine());
            var insertRe = await service.InsertItemAsync(insertNum, insertName);
            Console.WriteLine(insertRe);

        }


        private int itemNum = 0;
        public async void PickAndInsertItem()
        {
            var service = new EmulatorServiceClient();
            
            var name = "Inserted";
            
            itemNum = itemNum + 1;
            if (itemNum > 10)
            {
                itemNum = 1;
            }
            
            
            Console.WriteLine("Item number and name inserted: " + itemNum + " " + name);
            var pickInsertPick = await service.PickItemAsync(itemNum);
            var pickInsertItem = await service.InsertItemAsync(itemNum, name);
            Console.WriteLine(pickInsertPick + " & " + pickInsertItem);
        }
    }
}