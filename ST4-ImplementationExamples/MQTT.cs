using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading.Tasks;
using MQTTnet.Extensions.ManagedClient;
using MQTTnet.Server.Internal;

namespace ST4_ImplementationExamples
{
    public class MQTT
    {
        public MQTT()
        { }
        
        //MQTT vars
        MqttFactory factory;
        MqttClient client; //interface:: added 
        IMqttClientOptions messageBuilder;
        //IMqttClient client11;
        private async Task Connect() 
        {
            //init MQTT vars
            string clientId = Guid.NewGuid().ToString();//// added 
            /*factory = new MqttFactory();
            client = factory.CreateMqttClient();*/// used to connection 
            client = (MqttClient) new MqttFactory().CreateMqttClient();/// used to connection
            messageBuilder = new MqttClientOptionsBuilder()//how to connect 
                //.WithCredentials("Jakub","1111")// added
                //.WithTls()// added
                .WithClientId(clientId)
                .WithTcpServer("localhost", 1883) 
                .WithCleanSession(true)
                .Build();

            //the handlers of MQTTnet are very useful when working with an event-based communication
            //on established connection
            client.UseConnectedHandler(e =>
            {
                Console.WriteLine("Connected successfully with MQTT Brokers."); 
                SubscribeToTopic("emulator/operation");
                SubscribeToTopic("emulator/status");
                //SubscribeToTopic("emulator/echo");
                SubscribeToTopic("emulator/checkhealth");// added
            });

            //on lost connection
            client.UseDisconnectedHandler(e =>
            {
                Console.WriteLine("Disconnected from MQTT Brokers.");
            });

            //on receive message on subscribed topic
            MqttClientExtensions.UseApplicationMessageReceivedHandler(client, e =>
            {
                Console.WriteLine($"MQTT Subscribed message: {Encoding.UTF8.GetString(e.ApplicationMessage.Payload)} on topic: {e.ApplicationMessage.Topic}");
            });

            //connect
            await client.ConnectAsync(messageBuilder);
        }
        
            //Subscribe messages from the MQTT Broker // added
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
            await client.SubscribeAsync(topic);
        }

        //publish method /*call in RunExample()*/
        
        public async Task PublishOnTopic(string msg, string topic)
        {
            
            await client.PublishAsync(msg, topic);
        }

        //Publish messages to the MQTT Broker//added
        public async Task PublishOnTopic1(String msg, string topic, bool retainFlag = true, int qos = 1)// added
        {
            await client.PublishAsync(msg,topic);
            await client.PublishAsync(new MqttApplicationMessageBuilder()
                .WithTopic(topic)
                //.WithPayload(payload)
                .WithQualityOfServiceLevel((MQTTnet.Protocol.MqttQualityOfServiceLevel)qos)
                .WithRetainFlag(retainFlag)
                .Build());
        }


        //runner
        public async Task RunExample()
        {
            //connect and subscribe
            await Connect();

            //json serializable object
            var msg = new MQTTMessage();
            msg.ProcessID =11;
            //run publish
            await PublishOnTopic1("emulator/operation", JsonConvert.SerializeObject(msg));
             
        }
        
    }

    //class to serialize json objects
    public class MQTTMessage
    {
       
        public int ProcessID { get; set; } /*call in RunExample()*/
    }
}

