using System;
using System.IO.Ports;
using System.Threading;
using System.Threading.Tasks;
using CommDemo;

namespace ConsoleApp1
{
    class Program
    {
        static CommProcess commProcess = new CommProcess();

        static void Main(string[] args)
        {
            //开始通讯
            commProcess.Start(new
            {
                PortName = "COM1",
                StopBits = StopBits.One
            });

            Task t = Task.Factory.StartNew(() =>
            {
                while (true)
                {
                  Thread.Sleep(1000);
                    Console.Clear();
                    for (int i = 0; i < 10; i++)
                    {
                        Console.WriteLine($"ID:{i}  Data:{(int)commProcess.GetData(i)}");
                    }
                }
            });

            Console.ReadLine();
        }
    }
}