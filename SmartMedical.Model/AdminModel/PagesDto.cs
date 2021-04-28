using System;
using System.Collections.Generic;
using System.Text;

namespace SmartMedical.Model
{
     public class PagesDto
    {
        public int HomePage { get { return 1; } set { } }
        public int PrePage { get { return PageIndex > 1 ? PageIndex - 1 : 1; } set { } }
        public int NextPage { get { return PageIndex < CountPage ? PageIndex + 1 : 1; } set { } }
        public int LastPage { get { return this.CountPage; } set { } }
        public int Count { get; set; }
        public int PageIndex { get; set; }
        public int CountPage { get; set; }
        public PagesDto(int pageindex, int pagesize, int count)
        {
            this.Count = count;
            this.PageIndex = pageindex;
            this.CountPage = (int)Math.Ceiling(count * 1.0 / pagesize);
        }
    }
}
