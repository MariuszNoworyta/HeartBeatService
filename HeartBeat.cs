using System;
using System.Timers;
using Timer = System.Timers.Timer;
using System.Threading;


namespace SimpleHeartBeatService
{
    public class HeartBeat:IDisposable
    {
        private readonly Timer _timer;
        private readonly int dailyMax;
        private int dailyTimeInSec;
        private WindowsHelper wh;


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
                dailyMax =dailyTimeInSec = FileHelper.GetDailyTimeInSekFromSettings();
            }

            if (dailyTimeInSec<=0)
            {
                dailyTimeInSec = FileHelper.GetExtraTimeFromSettings();
            }
            wh = new WindowsHelper();
        }

        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                if (dailyTimeInSec> 0)
                {
                    Console.WriteLine($"TimerElapsed{Thread.CurrentThread.ManagedThreadId}");
                    dailyTimeInSec--;
                    var str = $"Logout in {RemainingTime(dailyMax, dailyTimeInSec)}";
                    wh.DrawTextOnScreenNew(str);
                    if (dailyTimeInSec % 60==0)
                    {
                        FileHelper.SetRemainingTime(dailyTimeInSec);
                    }
                }
                else
                {
                    _timer.Stop();
                    Console.WriteLine("Time is over.");
                    wh.DrawTextOnScreenNew("Time is over.");
                    Thread.Sleep(5000);
                    WindowsHelper.Logout();
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

        public string RemainingTime(int maxTime, int elapsedTime)
        {
            var remainingTime = maxTime -(maxTime - elapsedTime);
            var hours = remainingTime / 3600;
            var min = (remainingTime % 3600) / 60;
            var sek = (remainingTime % 3600) % 60;

            return String.Format("{0:D2}:{1:D2}:{2:D2}", hours, min, sek);
        }

        public void Dispose()
        {
            this.Stop();
        }
    }
}
