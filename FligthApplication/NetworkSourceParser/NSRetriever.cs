using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using FligthApplication.Models.Interfaces;
using FligthApplication.NetworkSourceParser.Parsers;
using FligthApplication.Utils;
using NetworkSourceSimulator;

namespace FligthApplication.NetworkSourceParser
{
    public class NSRetriever
    {
        private NetworkSourceSimulator.NetworkSourceSimulator ns;
        private List<IFTRObject> objects;

        private readonly string outputPath = Path.Combine(Environment.CurrentDirectory, @"../../../Output");

        private Thread nsThread;
        private readonly object locker = new Object();
        internal CancellationTokenSource cancelToken = new CancellationTokenSource();
        
        public NSRetriever(string ftrFilePath = "FTRfile.txt" , int minOffsetInMs = 1000, int maxOffsetInMS = 1000)
        {
            objects = new List<IFTRObject>();
            ns = new NetworkSourceSimulator.NetworkSourceSimulator(ftrFilePath, minOffsetInMs, maxOffsetInMS);

            this.ns.OnNewDataReady += (sender, content) =>
            {
                var message = ns.GetMessageAt(content.MessageIndex);
                MessageParser(message);
            };
        }

        public void Run()
        {
            nsThread = new Thread(() =>
            {
                while (!cancelToken.IsCancellationRequested)
                {
                    ns.Run();
                }
            });
            nsThread.Start();
            Console.WriteLine("Simulation is starting ...");
        }


        private void MessageParser(NetworkSourceSimulator.Message msg)
        {
            if( msg.MessageBytes != null)
            {
                var data = NSParserBuilder.ParseIFTRObject(msg);
                if( data != null)
                {
                    lock ( locker )
                    {
                        objects.Add(data);
                    }
                }
            }
        }

        public void CreateSnapshot()
        {
            string json = JSONHandler.SerializeObjectsToJson(objects);
            string date = DateTime.Now.ToString("HH_mm_ss");
            string name = $"snapshot{date}.json";
            File.WriteAllText(Path.Combine(outputPath, name), json);
        }

        public void Exit()
        {
            this.ns.OnNewDataReady -= (sender, content) => { };
            Console.WriteLine("Closing app ...");
            cancelToken.Cancel();
            System.Environment.Exit(0);
        }
    }
}

