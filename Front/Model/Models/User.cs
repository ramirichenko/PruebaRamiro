using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Front.Model.Models
{
    public class User
    {
        public string name { get; set; }
        public string token { get; set; }
        public bool authenticated { get; set; }
    }    
}