using System;
using System.Net;
using CSCore.CoreAudioAPI;

namespace AudioDetector
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new WebClient();
            if (IsAudioPlaying(GetDefaultRenderDevice()))
            {
                var content = client.DownloadString("https://slack.com/api/users.setPresence?token=xoxp-54759295125-54769515010-68598852724-7d87d91594&presence=away");
            }
            else
            {
                var content = client.DownloadString("https://slack.com/api/users.setPresence?token=xoxp-54759295125-54769515010-68598852724-7d87d91594&presence=auto");
            }
            Console.WriteLine(IsAudioPlaying(GetDefaultRenderDevice()));
            Console.ReadLine();
        }

        public static MMDevice GetDefaultRenderDevice()
        {
            using (var enumerator = new MMDeviceEnumerator())
            {
                return enumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Console);
            }
        }

        public static bool IsAudioPlaying(MMDevice device)
        {
            using (var meter = AudioMeterInformation.FromDevice(device))
            {
                return meter.PeakValue > 0;
            }
        }
    }
}
