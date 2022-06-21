
using Newtonsoft.Json;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;

class Program 
{
    internal class Jokes
    {
        private const string _url = "http://rzhunemogu.ru/RandJSON.aspx?CType=12";
        public string _json = "";
        public string encod = "";
        public void GetJson() 
        {
            using (WebClient client = new WebClient())
            {
                _json = client.DownloadString(_url);
                Console.WriteLine(_json);
                byte[] bytes = Encoding.UTF8.GetBytes(_json);
                encod = Encoding.UTF8.GetString(bytes);
            }
        }
    }

    internal class JokesAnswer 
    {
        public string Content { get; set; }
    }

    internal class Calendar
    {
        private string _url = "https://www.calend.ru/img/export/informer.png?";
        public string Month { get; set; }
        public string CalendarPath { get; set; }

        public void GetMonth() 
        {
            int intMonth;
            intMonth = (int)DateTime.Now.Date.Month;
            if (intMonth < 10)
            {
                Month = 0 + intMonth.ToString();
                CalendarPath = _url + DateTime.Now.Date.Year.ToString() + Month + DateTime.Now.Date.Day.ToString();
            }
            else
            {
                CalendarPath = _url + DateTime.Now.Date.Year.ToString() + intMonth.ToString() + DateTime.Now.Date.Day.ToString();
            }
        }
    }

    internal class Currency 
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public string NumCode { get; set; }
    }

    internal class CurrenceMarket 
    {
        private string _url = "http://www.cbr.ru/scripts/XML_daily.asp";
        private string _json = "";
        public  List<Currency> Currencies { get; set; }
        public void GetCurrencies()
        {
            using (WebClient client = new WebClient())
            {
                _json = client.DownloadString(_url);
                var currency = JsonConvert.DeserializeObject<List<Currency>>(_json);
                Currencies = currency;
            }
        }
    }


    public static void Main(string[] args) 
    {
        CurrenceMarket market = new CurrenceMarket();
        market.GetCurrencies();

        //Calendar calendar = new Calendar();
        //calendar.GetMonth();
        //Console.WriteLine(calendar.CalendarPath);
        Console.WriteLine();
        //Jokes jokes = new Jokes();
        //jokes.GetJson();
        //var jokeAnswer = JsonConvert.DeserializeObject<JokesAnswer>(jokes._json);
        //Console.WriteLine(jokeAnswer.Content);
        //Console.WriteLine();
    }
}
