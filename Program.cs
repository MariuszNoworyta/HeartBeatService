using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;


namespace SimpleHeartBeatService
{
    class Program
    {
        static void Main(string[] args)
        {

            TopshelfExitCode exitCode = HostFactory.Run(x =>
            {
                x.Service<HeartBeat>(s =>
                {
                    s.ConstructUsing(hb => new HeartBeat());
                    s.WhenStarted(hb => hb.Start());
                    s.WhenStopped(hb => hb.Stop());
                });

                x.RunAsLocalSystem();
                x.SetServiceName("HearBeat");
                x.SetDisplayName("HeartBeat Service");
            }
            );

            int exitCodeValue = (int)Convert.ChangeType(exitCode, typeof(TopshelfExitCode));
            Environment.ExitCode = exitCodeValue;


        }
    }
}
