using ChatApp.Bot.Models;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatApp.Bot.Config
{
    public class StockCsvMap : ClassMap<StockCsv>
    {

        public StockCsvMap()
        {
            Map(m => m.Symbol).Name("Symbol");
            Map(m => m.Date).Name("Date");
            Map(m => m.Time).Name("Time");
            Map(m => m.Open).Name("Open");
            Map(m => m.High).Name("High");
            Map(m => m.Low).Name("Low");
            Map(m => m.Close).Name("Close");
            Map(m => m.Volume).Name("Volume");
        }
    }
}
