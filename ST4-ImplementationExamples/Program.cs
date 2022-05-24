using System;
using System.Security.Policy;

namespace ST4_ImplementationExamples
{
    public class Program
    {
        static void Main(string[] args)
        {
            //REST
            //REST rest = new REST();
            //_ = rest.RunExample();

            //MQTT
            //MQTT mqtt = new MQTT();
            //_ = mqtt.RunExample();

            //SOAP
            SOAP soap = new SOAP();
            _ = soap.RunExample();
            
            
            Console.WriteLine("Select an option.");
            Console.WriteLine("1> Get Inventory");
            Console.WriteLine("2> Pick Single Item");
            Console.WriteLine("3> Insert Single Item");
            Console.WriteLine("4> Pick and Insert (auto increment)");
            Console.WriteLine("5> Exit");

            while (true)
            {
                var number = Convert.ToInt32(Console.ReadLine());

                switch (number)
                {
                    case 1:
                        soap.GetStatus();
                        break;
                    case 2:
                        soap.PickSingleItem();
                        break;
                    case 3:
                        soap.InsertSingleItem();
                        break;
                    case 4:
                        soap.PickAndInsertItem();
                        break;
                    case 5:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid input!");
                        break;
                }
            }
            
            
            Console.ReadKey();
        }
    }
}