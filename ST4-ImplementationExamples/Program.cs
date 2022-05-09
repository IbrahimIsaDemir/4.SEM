using System;

namespace ST4_ImplementationExamples
{
    public class Program
    {
        static void Main(string[] args)
        {
            //REST
            REST rest = new REST();
            //_ = rest.RunExample();
            /*
            //MQTT
            MQTT mqtt = new MQTT();
            _ = mqtt.RunExample();

            //SOAP
            SOAP soap = new SOAP();
            _ = soap.RunExample();
            */
            
            Console.WriteLine("Welcome to the AGV");
            Console.WriteLine("Please pick a program");
            Console.WriteLine("1: move to warehouse");
            Console.WriteLine("2: Pick items from warehouse");
            Console.WriteLine("3: put items into warehouse");
            Console.WriteLine("4: move to assembly station");
            Console.WriteLine("5: put items into assemblystation");
            Console.WriteLine("6: pick items from assembly station");
            Console.WriteLine("7: Move to charger");
            
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
                    rest.GetStatus();
                }
                Console.ReadKey();
            }
        }
    }
}
