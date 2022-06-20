using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot
{
    internal class Calendar
    {
        private string _url = "https://www.calend.ru/img/export/informer.png?";
        public string Month { get; set; }
        public string CalendarPath { get; set; }

        public void GetUrl()
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
}
