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
        const string NAMETEMPFILE = ".HeartBeatTemp.txt";
        private static string userDirPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        private static string fullPathToTemplateFile = Path.Combine(userDirPath, NAMETEMPFILE);


        public static bool IsLastAccessToday()
        {

            Console.WriteLine(fullPathToTemplateFile);
            if (DateTime.Today.AddDays(0).Day.Equals(File.GetLastWriteTime(fullPathToTemplateFile).Day))
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
                File.WriteAllText(fullPathToTemplateFile, remainingTimeSec.ToString());
            }
            catch (IOException)
            {
                //TODO error to log4net //System.Diagnose
            }
        }

        public static int GetRemainingTime()
        {
            var sec = 60;
            try
            {
                var remainingTimeFromFile = File.ReadLines(fullPathToTemplateFile).Single<string>();
                sec =int.Parse(remainingTimeFromFile);
            }
            catch (IOException ex)
            {
                sec = 60;
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
                min = 5;

                //TODO error to log4net //System.Diagnose

            }
            catch (Exception ex)
            {
                //TODO error to log4net //System.Diagnose
            }
            return min * 60;
        }

        public static int GetExtraTimeFromSettings()
        {
            int min = 0;
            try
            {
                var configDailyTime = ConfigurationManager.AppSettings["ExtraTime"];
                min = Int32.Parse(configDailyTime);
            }
            catch (ConfigurationErrorsException ex)
            {
                min = 5;

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
