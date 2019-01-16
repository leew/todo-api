using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// including the M2Mqtt Library

using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace TodoApi.Services
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public class MQTTClientService: IMQTTClientService
    {
        MqttClient client;
        string clientId;
        // this code runs when the main window opens (start of the app)
        public MQTTClientService()
        {
            Console.WriteLine("Hello -> Constructor");
            string BrokerAddress = "localhost";
            client = new MqttClient(BrokerAddress);
            // register a callback-function (we have to implement, see below) which is called by the library when a message was received
            client.MqttMsgPublishReceived += client_MqttMsgPublishReceived;
            // use a unique id as client id, each time we start the application
            clientId = Guid.NewGuid().ToString();
            byte code = client.Connect(clientId, null, null);
            Console.WriteLine("Connect response " + code);
            client.Subscribe(new string[] { "/hivemq/test" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE }); 
        }
        /* Replace with code in destructor */
        // // this code runs when the main window closes (end of the app)
        // protected override void OnClosed(EventArgs e)
        // {
        //     client.Disconnect();
        //     base.OnClosed(e);
        //     App.Current.Shutdown();
        // }
        // this code runs when the button "Subscribe" is clicked
        public void publish(object sender, string message)
        {    
            if (message != "")
            {
                // whole topic
                string Topic = "/hivemq/test";
                // publish a message with QoS 2
                client.Publish(Topic, Encoding.UTF8.GetBytes(message), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
            }
        }
        // this code runs when a message was received
        void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            Console.WriteLine("Message Received");
            string ReceivedMessage = Encoding.UTF8.GetString(e.Message);
            Console.WriteLine("Message Received");
            Console.WriteLine(ReceivedMessage);
            Console.WriteLine("=========================================");

        }


    }
}