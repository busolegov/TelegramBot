using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot
{
    internal class AnswerImage
    {
        public string Image { get; set; }
        public string Answer { get; set; }

        private string _path = Environment.CurrentDirectory + "\\pic\\";

        public void ImageDownload()
        {
            WebClient client = new WebClient();
            client.DownloadFile(Image, $"{_path}/answerimg.gif");
        }
    }
    internal class YesNo
    {
        private const string _url = "https://yesno.wtf/api";
        public string _json = "";

        public void GetJson()
        {
            using (WebClient client = new WebClient())
            {
                _json = client.DownloadString(_url);
            }
        }
    }
}
