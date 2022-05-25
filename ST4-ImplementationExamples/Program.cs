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
            _ = mqtt.RunExample();
            SOAP soap = new SOAP();
            //_ = rest.RunExample();

            
            //Warehouse
            soap.PickAndInsertItem();

            
            rest.CheckBattery();


            rest.ChooseOperation(1);//MoveToStorageOperation
            rest.GetStatus();
            rest.execute();
            rest.GetStatus();
            Thread.Sleep(8000);
            rest.GetStatus();
            rest.ChooseOperation(2);//PickWarehouseOperation
            rest.GetStatus();
            rest.execute();
            Thread.Sleep(8000);
            rest.GetStatus();
            rest.ChooseOperation(4);//MoveToAssemblyOperation
            rest.GetStatus();
            rest.execute();
            rest.GetStatus();
            Thread.Sleep(8000);
            rest.GetStatus();
            
            
            mqtt.Idle();// idle state:
            Thread.Sleep(8000);
            
            rest.ChooseOperation(5);//PutAssemblyOperation
            rest.GetStatus();
            rest.execute();
            rest.GetStatus();
            
            Thread.Sleep(8000);
            rest.GetStatus();

            //Her skal koden for assemblyStation sættes ind
            
            mqtt.Execution();//execution state
            Thread.Sleep(8000);

            rest.ChooseOperation(6);//PickAssemblyOperation
            rest.GetStatus();
            rest.execute();
            Thread.Sleep(8000);
            rest.GetStatus();
            rest.ChooseOperation(1);//MoveToStorageOperation
            rest.GetStatus();
            rest.execute();
            rest.GetStatus();
            Thread.Sleep(8000);
            rest.GetStatus();
            rest.ChooseOperation(3);//PutWarehouseOperation
            rest.GetStatus();
            rest.execute();
            Thread.Sleep(8000);
            rest.GetStatus();
        }
    }
}