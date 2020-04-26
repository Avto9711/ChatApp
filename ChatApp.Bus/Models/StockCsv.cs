using System;
using System.Collections.Generic;
using System.Text;

namespace ChatApp.Bot.Models
{
    public class StockCsv
    {
        public string Symbol { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public double Open { get; set; }
        public double High { get; set; }
        public double Low { get; set; }
        public double Close { get; set; }
        public int Volume { get; set; }
    }
}
