using System;
using System.Threading;

namespace ST4_ImplementationExamples
{
    public class Program
    {
        private static REST _rest ;
        private static MQTT _mqtt;
        private static SOAP soap;

        public static void Main(string[] args)
        {
            //REST
             _rest = new REST();
             _mqtt = new MQTT();
            _mqtt.RunExample();
             soap = new SOAP();
            //_ = rest.RunExample();

            
            //Warehouse
            soap.PickAndInsertItem();

            
            _rest.CheckBattery();


            _rest.ChooseOperation(1);//MoveToStorageOperation
            _rest.execute();
            _rest.GetStatus();
            Thread.Sleep(8000);
            _rest.ChooseOperation(2);//PickWarehouseOperation
            _rest.execute();
            _rest.GetStatus();
            Thread.Sleep(8000);
            _rest.ChooseOperation(4);//MoveToAssemblyOperation
            _rest.execute();
            _rest.GetStatus();
            Thread.Sleep(8000);

            _mqtt.Idle();// idle state:
            Thread.Sleep(8000);
            
            _rest.ChooseOperation(5);//PutAssemblyOperation
            _rest.execute();
            _rest.GetStatus();
            
            Thread.Sleep(8000);

            //Her skal koden for assemblyStation sættes ind
            
            _mqtt.Execution();//execution state
            Thread.Sleep(8000);

            _rest.ChooseOperation(6);//PickAssemblyOperation
            _rest.execute();
            _rest.GetStatus();
            Thread.Sleep(8000);
            _rest.ChooseOperation(1);//MoveToStorageOperation
            _rest.execute();
            _rest.GetStatus();
            Thread.Sleep(8000);
            _rest.ChooseOperation(3);//PutWarehouseOperation
            _rest.execute();
            _rest.GetStatus();
            Thread.Sleep(8000);
            _rest.GetStatus();
        }
    }
}