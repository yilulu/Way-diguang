using System;
using System.Collections.Generic;
using System.Text;

namespace DTcms.Model
{
    public class image
    {
        public int id { get; set; }
        public string title { get; set; }
        public int sort { get; set; }
        public string img_url { get; set; }
        public string link_url { get; set; }
        public int Typeid { get; set; }
        public string Vipids { get; set; }
    }
}
