using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleHeartBeatService
{
    public static class FileHelper
    {
        const string NAMETEMPFILE = "HeartBeatTemp.txt";

        public static bool IsLastAccessToday()
        {
            if (DateTime.Today.AddDays(0).Day.Equals(File.GetLastAccessTime(NAMETEMPFILE).Day))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public static void SetRemainingTime(int remainingTimeSec)
        {
            try
            {
                File.WriteAllText(NAMETEMPFILE, remainingTimeSec.ToString());
            }
            catch (IOException)
            {
                //TODO error to log4net //System.Diagnose
            }
        }

        public static int GetRemainingTime()
        {
            var sec = 1000000;
            try
            {
                var remainingTimeFromFile = File.ReadLines(NAMETEMPFILE).Single<string>();
                sec =int.Parse(remainingTimeFromFile);
            }
            catch (IOException ex)
            {
                //TODO error to log4net //System.Diagnose
            }
            catch (Exception ex)
            {
                //TODO error to log4net //System.Diagnose
            }
            return sec;
        }

        public static int GetDailyTimeInSekFromSettings()
        {
            int min=0;
            try
            {
                var configDailyTime = ConfigurationManager.AppSettings["DailyTimeMinutes"];
                min = Int32.Parse(configDailyTime);

            }
            catch (ConfigurationErrorsException ex)
            {

                //TODO error to log4net //System.Diagnose

            }
            catch (Exception ex)
            {
                //TODO error to log4net //System.Diagnose
            }
            return min * 60;
        }

    }
}
