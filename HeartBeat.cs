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
        private int dailyTimeInSec;
 
        public HeartBeat()
        {

            _timer = new Timer(1000) { AutoReset = true };
            _timer.Elapsed += TimerElapsed;

            if (FileHelper.IsLastAccessToday())
            {
                dailyTimeInSec = FileHelper.GetRemainingTime();
            }
            else
            {
                dailyTimeInSec = FileHelper.GetDailyTimeInSekFromSettings();
            }

        }

        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                if (dailyTimeInSec> 0)
                {
                    dailyTimeInSec--;
                    FileHelper.SetRemainingTime(dailyTimeInSec);
                }
                else
                {
                    _timer.Stop();
                    Console.WriteLine("Time is over.");
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
