using System;
using System.Threading;
using MQTTnet.Client;

namespace ST4_ImplementationExamples
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //REST
            REST rest = new REST();
            _ = rest.RunExample();
            //MQTT
            MQTT mqtt = new MQTT();
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
            SOAP soap = new SOAP();
            _ = soap.RunExample();
            Console.ReadKey();
        }
    }
}
