using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;



namespace SimpleHeartBeatService
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {

            using(HeartBeat hb = new HeartBeat())
            {
                hb.Start();

                while (true) {
                    Thread.Sleep(100000);
                };
            }
        }
    }
}
