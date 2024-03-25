using NetMQ;
using NetMQ.Sockets;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

// Use NetMQ package
namespace ControlLazerApp
{
    public static class Publisher
    {

        public static CancellationTokenSource CancellationTokenSource = new CancellationTokenSource();
        //Request-Response
        public static void Initialize_Req_Res()
        {
            Task.Run(() =>
            {
                using (var responder = new ResponseSocket())
                {
                    responder.Bind("tcp://*:5557");
                    while (true)
                    {
                        string str = responder.ReceiveFrameString();
                        Console.WriteLine("Received {0}-{1}", str, DateTime.Now);
                        Task.Delay(1000);
                        responder.SendFrame("World");
                    }
                }
            });
        }
        //Subscriber --- use in sebcriber mode (Workstaion mode)
        public static void Initialize_Subcriber(string topic)
        {
            Task.Run(() =>
            {
                using (var subscriber = new SubscriberSocket())
                {
                    subscriber.Connect("tcp://127.0.0.1:5556");
                    subscriber.Subscribe(topic);

                    while (!CancellationTokenSource.IsCancellationRequested) 
                    {
                        string v = subscriber.ReceiveFrameString();
                        var msg = subscriber.ReceiveFrameString();
                        Console.WriteLine("From Publisher: {0} {1}", topic, msg);
                        var content = msg.Split(':');
                        switch (content[0])
                        {
                            case "open_lazer":
                                Form1.OpenLazer();
                                break;
                            case "open_file_print":
                                Form1.OpenCutingFile(content[1], content[2]);
                                break;
                            default:
                                break;
                        }
                    }
                   
                }
            }, CancellationTokenSource.Token);
        }
        //Pubnlisher 
        /// <summary>
        /// Publish message to subsrcibers in network (Workstation node)
        /// </summary>
        /// <param name="mQMessage"></param>
        public static async Task Publisher_MQ(MQMessage mQMessage)
        {
            using (var publisher = new PublisherSocket())
            {
                publisher.Bind("tcp://*:5556");
                await Task.Delay(500);
                if (mQMessage != null && mQMessage.Topic != null && mQMessage.Content != null)
                {
                    publisher
                        .SendMoreFrame(mQMessage.Topic) // Topic
                        .SendFrame(mQMessage.Content); // Message 
                }
            }
        }
    }

    /// <summary>
    /// Object to create instance Message to workstaions node
    /// </summary>
    public class MQMessage
    {
        public string Topic { get; set; }
        public string Content { get; set; }
    }
}
