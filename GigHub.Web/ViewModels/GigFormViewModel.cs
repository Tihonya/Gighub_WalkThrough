using GigHub.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GigHub.Web.ViewModels
{
    public class GigFormViewModel
    {
        [Required]
        public string Venue { get; set; }
        [Required]
        [FutureDate]
        public string Date { get; set; }

        [Required]
        [ValidTime]
        public string Time { get; set; }
        [Required]

        public byte Genre { get; set; }

        public DateTime GetDateTime()
        {
             return DateTime.Parse(string.Format("{0} {1}", Date, Time));
        }


        public IEnumerable<Genre> Genres { get; set; }
    }
}