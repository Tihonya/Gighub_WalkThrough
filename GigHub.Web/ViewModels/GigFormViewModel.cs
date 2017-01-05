using GigHub.Core;
using System;
using System.Collections.Generic;

namespace GigHub.Web.ViewModels
{
    public class GigFormViewModel
    {
        public string Venue { get; set; }
        public string Date { get; set; }

        public string Time { get; set; }
        public byte Genre { get; set; }

        public DateTime DateTime
        {
            get { return DateTime.Parse(string.Format("{0} {1}", Date, Time)); }
        }


        public IEnumerable<Genre> Genres { get; set; }
    }
}