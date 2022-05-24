using System;
using System.Threading;

namespace ST4_ImplementationExamples
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //REST
            REST rest = new REST();
            MQTT mqtt = new MQTT();
            SOAP soap = new SOAP();
            //_ = rest.RunExample();
            /*
            
            //MQTT
            _ = mqtt.RunExample();
            Console.WriteLine("Select an option.");
            Console.WriteLine("1> Idle");
            Console.WriteLine("2> Executing");
            Console.WriteLine("3> Error");
            Console.WriteLine("4> gogo");
            Console.WriteLine("5> Exit");
            while (true)
            {
                string input = Console.ReadLine();
                int number;
                Int32.TryParse(input, out number);
                
                switch (number)
                {
                    case 1:
                    {
                      mqtt.Idle();
                        
                        
                    }
                        break;
                    case 2:
                    {  // stand in execution state
                        mqtt.Execution();
                        break;

                    }
                    case 3:
                    {
                        mqtt.Error();
                        break;
                    }
                    case 4:
                    {
                        mqtt.UnsubscribeAsync("emulator/status");
                        Console.WriteLine("go");
                        break;
                    }
                    case 5:
                    {
                        Environment.Exit(0);
                        break;
                    }
                  
                    default:
                    {
                        mqtt.UnsubscribeAsync("emulator/status");
                        Console.WriteLine("Invalid Input!");
                        break;
                    }
                }
            }

            //SOAP
            _ = soap.RunExample();
            */
            /*
            Console.WriteLine("Welcome to the AGV");
            Console.WriteLine("Please pick a program");
            Console.WriteLine("1: move to warehouse");
            Console.WriteLine("2: Pick items from warehouse");
            Console.WriteLine("3: put items into warehouse");
            Console.WriteLine("4: move to assembly station");
            Console.WriteLine("5: put items into assemblystation");
            Console.WriteLine("6: pick items from assembly station");
            Console.WriteLine("7: Move to charger");
            */
            rest.ChooseOperation(1);
            rest.GetStatus();
            rest.execute();
            rest.GetStatus();
            Thread.Sleep(8000);
            rest.GetStatus();
            rest.ChooseOperation(2);
            rest.GetStatus();
            rest.execute();
            Thread.Sleep(8000);
            rest.GetStatus();
            rest.ChooseOperation(4);
            rest.GetStatus();
            rest.execute();
            rest.GetStatus();
            Thread.Sleep(8000);
            rest.GetStatus();
            rest.ChooseOperation(5);
            rest.GetStatus();
            rest.execute();
            rest.GetStatus();
            Thread.Sleep(8000);
            rest.GetStatus();
            
            //Her skal koden for assemblyStation sættes ind
            
            rest.ChooseOperation(6);
            rest.GetStatus();
            rest.execute();
            Thread.Sleep(8000);
            rest.GetStatus();
            rest.ChooseOperation(1);
            rest.GetStatus();
            rest.execute();
            rest.GetStatus();
            Thread.Sleep(8000);
            rest.GetStatus();
            rest.ChooseOperation(3);
            rest.GetStatus();
            rest.execute();
            Thread.Sleep(8000);
            rest.GetStatus();
            
            //Her skal warehouse koden sættes ind

            /*
            while (true)
            {
                int i = int.Parse(Console.ReadLine());
                int maximumOptions = 7;
                
                if (i>maximumOptions)
                {
                    Console.WriteLine($"Please pick a value between 1 and {maximumOptions}");
                }
                
                if (i<=maximumOptions)
                {
                    rest.ChooseOperation(i);
                    //rest.GetStatus();
                }
                Console.ReadKey();
            }
            */
        }
    }
}