using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleHeartBeatService
{
    public class FileHelper
    {
        const int DEFAULTDAILYTIMEMIN = 5;
        const string NAMETEMPFILE = "HeartBeatTemp.txt";
        private int DailyTimeInSec;
        private int ElapsedTimeSec;

        public FileHelper()
        {
            DailyTimeInSec = GetDailyTimeInSekFromSettings();

            if (!DateTime.Today.AddDays(0).Day.Equals(File.GetLastAccessTime(NAMETEMPFILE).Day))
            {
                ElapsedTimeSec = 0;
            }
            else
            {
                ElapsedTimeSec = GetElapsedTime();
            }
        }


        public int GetRemainingTimeInSec()
        {
            try
            {
                return DailyTimeInSec - ElapsedTimeSec;
                
            }
            catch (Exception)
            {

                //TODO error to log4net //System.Diagnose
            }
            return 0;
        }

        public void SetElapsedTime()
        {
            try
            {
                ElapsedTimeSec++;
                File.WriteAllText(NAMETEMPFILE, ElapsedTimeSec.ToString());
            }
            catch (IOException)
            {
                //TODO error to log4net //System.Diagnose
            }
        }

        private int GetElapsedTime()
        {
            int sec = DailyTimeInSec;
            try
            {
                var elapsedTimeFromFile = File.ReadLines(NAMETEMPFILE).Single<string>();
                sec =int.Parse(elapsedTimeFromFile);
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

        private static int GetDailyTimeInSekFromSettings()
        {
            int min = DEFAULTDAILYTIMEMIN;
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
