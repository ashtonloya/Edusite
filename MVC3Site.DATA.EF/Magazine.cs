//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MVC3Site.DATA.EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class Magazine
    {
        public int MagazineID { get; set; }
        public string MagazineTitle { get; set; }
        public int IssuesPerYear { get; set; }
        public decimal PricePerYear { get; set; }
        public string Category { get; set; }
        public Nullable<int> Circulation { get; set; }
        public string PublishRate { get; set; }
    }
}
