using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot
{
    internal class CatPicture
    {
        private const string _catPicturePreLink = "https://aws.random.cat/view/";
        public string RandomCatFullLink { get; set; }

        public CatPicture()
        {
            Random random = new Random();
            string randomStringNumber = random.Next(1, 1100).ToString();
            RandomCatFullLink = _catPicturePreLink + randomStringNumber;
        }

        public void GetPicture() 
        {
            WebRequest request = WebRequest.Create(RandomCatFullLink);
            WebResponse response = request.GetResponse();
            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    string line = "";
                    while ((line = reader.ReadLine()) != null)
                    {
                        Console.WriteLine(line);
                    }
                }
            }
            response.Close();
        }
        
        
    }
}
