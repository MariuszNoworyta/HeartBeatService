using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using Timer = System.Timers.Timer;
using System.Threading;


namespace SimpleHeartBeatService
{
    public class HeartBeat
    {

        private readonly Timer _timer;
        private FileHelper FileHelperObj;

        public HeartBeat()
        {

            _timer = new Timer(1000) { AutoReset = true };
            _timer.Elapsed += TimerElapsed;

            FileHelperObj = new FileHelper();

        }

        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                if (FileHelperObj.GetRemainingTimeInSec() < 0)
                {
                    _timer.Stop();
                    Console.WriteLine("Time is over.");
                }
                else
                {
                    FileHelperObj.SetElapsedTime();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        public void Start()
        {
            _timer.Start();
        }

        public void Stop()
        {
            _timer.Stop();
        }

    }
}
