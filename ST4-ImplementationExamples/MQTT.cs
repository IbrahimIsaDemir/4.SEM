using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MQTTnet.Extensions;
using MQTTnet.Server.Internal;
using System.IO;
using System.Xml;
using MQTTnet.Client.Unsubscribing;
using MQTTnet.Server.Status;

namespace ST4_ImplementationExamples
{
    public class MQTT
    {
        public MQTT()
        { }
        
        //MQTT vars
        MqttFactory factory;
        MqttClient mqttClient;
        IMqttClientOptions mqttClientOptions;
         MqttClientOptionsBuilder mqttClientOptionsBuilder;
         //connection 
        private async Task Connect()
        {
            //init MQTT vars
            string clientId = Guid.NewGuid().ToString();
            mqttClient = (MqttClient) new MqttFactory().CreateMqttClient();/// used to connection
            mqttClientOptions = new MqttClientOptionsBuilder()// connection Preparation
                .WithCredentials("Jakub","1111")// ti si optional 
                .WithClientId(clientId)
                .WithTcpServer("localhost", 1883) //TCP connection
                .WithCleanSession(true)
                .WithRequestResponseInformation(true)
                
                .WithUserProperty("Bouzan","1993")
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
           
            // receive message on subscribed topic
            mqttClient.UseApplicationMessageReceivedHandler(  e =>
            { Console.WriteLine("### RECEIVED APPLICATION MESSAGE ###");
              // Console.WriteLine($"MQTT Subscribed message: {Encoding.UTF8.GetString(e.ApplicationMessage.Payload)} on topic: {e.ApplicationMessage.Topic}");
              Console.WriteLine($"+ Topic = {e.ApplicationMessage.Topic}");
               Console.WriteLine($"+ Payload = {Encoding.UTF8.GetString(e.ApplicationMessage.Payload)}");
               Console.WriteLine($"+ QoS = {e.ApplicationMessage.QualityOfServiceLevel}");
               Console.WriteLine();
            });
            //connect
            await mqttClient.ConnectAsync(mqttClientOptions);
        }
        public async void Idle()//stand in idle state 
        {
            mqttClientOptionsBuilder = new MqttClientOptionsBuilder();
                    mqttClientOptionsBuilder.WithCommunicationTimeout(TimeSpan.FromSeconds(10));
                    Console.WriteLine("It is in idle state:");
            
                        await SubscribeToTopic("emulator/status");
                        await SubscribeToTopic("emulator/response");
                        while (true){
                   
                            try
                            {  
                                Thread.Sleep(1000);
                                var b = UnsubscribeAsync("emulator/status").Wait(TimeSpan.FromSeconds(9));
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e);
                            }
                        }
                        
        }
        // stand in execution state
        public async Task Execution()
        {
            await OperationRun();
                  //on receive message on subscribed topic
                
                  Console.WriteLine("it is in execution state:"); 
                  await SubscribeToTopic("emulator/response");
                  await SubscribeToTopic("emulator/checkhealth");
                  await SubscribeToTopic("emulator/status");
                 
                  while (true){
                   
                        try
                        {  
                            Thread.Sleep(8300);
                            var b = UnsubscribeAsync("emulator/status").Wait(TimeSpan.FromSeconds(9));
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                        }
                  }
            
                  
        }

        public async void Error()
        {
            Console.WriteLine("There are Errors");
            await UnsubscribeAsync("emulator/status");
        }
        
        //Subscribe messages from the MQTT Broker 
        private async Task SubscribeToTopic(string input,int qos = 1)
        {
            Console.WriteLine("Subscribing to : " + input);
            //define topics
            var topic = new MqttTopicFilterBuilder()
                .WithTopic(input)
                .WithQualityOfServiceLevel((MQTTnet.Protocol.MqttQualityOfServiceLevel)qos)
                .Build();
            //subscribe
            await mqttClient.SubscribeAsync(topic);
        }
        //unSubscribe messages from the MQTT Broker 
        public async Task UnsubscribeAsync(string input)
        {
            var topic1 = new MqttTopicFilterBuilder()
                .WithTopic(input)
                .Build();
           await mqttClient.UnsubscribeAsync(topic1.Topic);
        }
        
        //Publish messages to the MQTT Broker
        public async Task PublishOnTopic(String msg, string topic, int qos = 1)// added
        
        {
            var message =new MqttApplicationMessageBuilder()
                .WithTopic(topic)
                //.WithPayload("publish to broker ")
                .WithQualityOfServiceLevel((MQTTnet.Protocol.MqttQualityOfServiceLevel) qos)
                .WithRetainFlag(true)
                .WithExactlyOnceQoS()
                .Build();
            await mqttClient.PublishAsync(message, CancellationToken.None);
            await mqttClient.PublishAsync(msg,topic);
        }
        
        //runner
        public async Task RunExample()
        { //connect and subscribe
            await Connect();
        }
        public async Task OperationRun()
        {
            //json serializable object
            var msg = new MqttMessage();
            msg.ProcessID =1;
            //run publish
            if (msg.ProcessID!=9999)
            {
                await PublishOnTopic("emulator/operation", JsonConvert.SerializeObject(msg));
            }
            else
            {
                await PublishOnTopic("emulator/operation", JsonConvert.SerializeObject(msg));
                Console.WriteLine("There is an error");

            }
        }
    }
    //class to serialize json objects
    public class MqttMessage
    {
        public int ProcessID { get; set; } 
    }
}

