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

            
            //Warehouse
            soap.PickAndInsertItem();

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
        }
    }
}