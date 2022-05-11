using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MQTTnet.Extensions;
using MQTTnet.Extensions.ManagedClient;
using MQTTnet.Server.Internal;
using System.IO; 
namespace ST4_ImplementationExamples
{
    public class MQTT
    {
        public MQTT()
        { }
        
        //MQTT vars
        MqttFactory factory;
        MqttClient mqttClient; //interface:: added 
        IMqttClientOptions mqttClientOptions;
        ManagedMqttClient ManagedMqttClient;
        //IMqttClient client11;
        private async Task Connect()
        {
            //init MQTT vars
            string clientId = Guid.NewGuid().ToString();//// added 
          
            /*factory = new MqttFactory();
            client = factory.CreateMqttClient();*/// used to connection 
            mqttClient = (MqttClient) new MqttFactory().CreateMqttClient();/// used to connection
            mqttClientOptions = new MqttClientOptionsBuilder()// connection Preparation
                .WithCredentials("Jakub","1111")// added 
                .WithClientId(clientId)
                .WithTcpServer("localhost", 1883) //TCP connection
                .WithCleanSession(true)
                .WithRequestResponseInformation(true)
                .WithUserProperty("Bouzan","1993")
               // .WithCommunicationTimeout(TimeSpan.FromSeconds(10))
                .Build();

            //the handlers of MQTTnet are very useful when working with an event-based communication
            //on established connection
            mqttClient.UseConnectedHandler(e =>
            {
                if (mqttClient.IsConnected)
                {
                    Console.WriteLine("Connected successfully with MQTT Brokers.");
                }
              
            });
          
            
            //on lost connection
            mqttClient.UseDisconnectedHandler(e =>
            {
                try
                {
                      if (mqttClient.IsConnected == false) 
                          Console.WriteLine("Disconnected from MQTT Brokers.");
                      Task.Delay(TimeSpan.FromSeconds(5)); 
                                    
                      mqttClient.ConnectAsync(mqttClientOptions); // Since 3.0.5 with CancellationToken
                }
                catch
                {
                    Console.WriteLine("### RECONNECTING FAILED ###");
                }
            });
           
            //on receive message on subscribed topic
            mqttClient.UseApplicationMessageReceivedHandler( e =>
            { Console.WriteLine("### RECEIVED APPLICATION MESSAGE ###");
              // Console.WriteLine($"MQTT Subscribed message: {Encoding.UTF8.GetString(e.ApplicationMessage.Payload)} on topic: {e.ApplicationMessage.Topic}");
              Console.WriteLine($"+ Topic = {e.ApplicationMessage.Topic}");
               Console.WriteLine($"+ Payload = {Encoding.UTF8.GetString(e.ApplicationMessage.Payload)}");
               Console.WriteLine($"+ QoS = {e.ApplicationMessage.QualityOfServiceLevel}");
               //Console.WriteLine($"+ ResponseTopic = {e.ApplicationMessage.ResponseTopic}");
               //Console.WriteLine($"+ Retain = {e.ApplicationMessage.Retain}");
               Console.WriteLine();
            });
                //connect
                await mqttClient.ConnectAsync(mqttClientOptions);
            Console.WriteLine("Select an option.");
            Console.WriteLine("1> Idle");
            Console.WriteLine("2> Executing");
            Console.WriteLine("3> Error");
            Console.WriteLine("4> Exit");
            string input = Console.ReadLine();
            int number;
            Int32.TryParse(input, out number);

            switch (number)
            {
                case 1:
                {
                    Console.WriteLine("It is in idle state:");
                    //stand in idle state 
                    SubscribeToTopic("emulator/status");
                    SubscribeToTopic("emulator/response");
                    mqttClientOptions = new MqttClientOptionsBuilder()
                        .WithCommunicationTimeout(TimeSpan.FromSeconds(1))
                        .Build();
                    break;
                    
                }
                case 2:
                {
                    Console.WriteLine("it is in execution state:");
                    // stand in excution state
                    SubscribeToTopic("emulator/status");
                        SubscribeToTopic("emulator/response");
                        SubscribeToTopic("emulator/checkhealth");
                        break;
                        
                }
                case 3:
                {


                    Console.WriteLine("Input Item to Insert & Name");
                    break;
                }
                case 4:
                {
                    Environment.Exit(0);
                    break;
                }
                default:
                {
                    Console.WriteLine("Invalid Input!");
                    break;
                }
            }
            
          
            
        }

       

        //Subscribe messages from the MQTT Broker 
        public async void SubscribeToTopic(string input,int qos = 1)
        {
            //printout
            Console.WriteLine("Subscribing to : " + input);

            //define topics
            var topic = new MqttTopicFilterBuilder()
                .WithTopic(input)
                .WithQualityOfServiceLevel((MQTTnet.Protocol.MqttQualityOfServiceLevel)qos)// added
                .Build();

            //subscribe
            await mqttClient.SubscribeAsync(topic);
        }
        
        //Publish messages to the MQTT Broker
        public async Task PublishOnTopic(String msg, string topic, int qos = 1)// added
        {
            // it is not necessary 
            var message =new MqttApplicationMessageBuilder()
                .WithTopic(topic)
                //.WithPayload("publish to broker ")
                .WithQualityOfServiceLevel((MQTTnet.Protocol.MqttQualityOfServiceLevel) qos)
                .WithRetainFlag(true)
                .WithExactlyOnceQoS()
                .Build();
           await mqttClient.PublishAsync(message, CancellationToken.None);
            // it is not necessary
           await mqttClient.PublishAsync(msg,topic);
        }


        //runner
        public async Task RunExample()
        { //connect and subscribe
            await Connect();
            //json serializable object
            var msg = new MQTTMessage();
            msg.ProcessID =30;
            //run publish
            await PublishOnTopic("emulator/operation", JsonConvert.SerializeObject(msg));
        }
        
    }

    //class to serialize json objects
    public class MQTTMessage
    {
        public int ProcessID { get; set; } 
    }
}

