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
    
    public partial class BookAuthor
    {
        public int BookAuthorID { get; set; }
        public int BookID { get; set; }
        public int AuthorID { get; set; }
        public Nullable<byte> AuthorOrder { get; set; }
    
        public virtual Author Author { get; set; }
        public virtual Book Book { get; set; }
    }
}
