﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;//Metadata
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC3Site.DATA.EF//.Metadata - Namespace must match the .tt files
{
    //If you were creating a controller for UserDetails (and the associated views) you would want
    //Metadata for the UserDetail Class here.

    #region Author Metadata
    public class AuthorMetadata
    {

        //public int AuthorID { get; set; } - PK not displayed
        [Display(Name = "First Name")]
        [Required(ErrorMessage = "*")]
        [StringLength(15, ErrorMessage = "* First name must be 15 characters or less.")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "*")]
        [StringLength(15, ErrorMessage = "* Last name must be 15 characters or less.")]
        public string LastName { get; set; }

        [StringLength(50, ErrorMessage = "* City must be 50 characters or less.")]
        [DisplayFormat(NullDisplayText = "[-N/A-]")]
        public string City { get; set; }

        [StringLength(2, ErrorMessage = "* State must be 2 characters.")]
        [DisplayFormat(NullDisplayText = "[-N/A-]")]
        public string State { get; set; }

        [Display(Name = "Zip Code")]
        [StringLength(6, ErrorMessage = "* Zip Code must be 2 characters or less.")]
        [DisplayFormat(NullDisplayText = "[-N/A-]")]
        public string ZipCode { get; set; }

        [StringLength(30, ErrorMessage = "* Country must be 30 characters or less.")]
        [DisplayFormat(NullDisplayText = "[-N/A-]")]
        public string Country { get; set; }
    }

    [MetadataType(typeof(AuthorMetadata))]
    public partial class Author
    {
        //custom property to show author full name
        [Display(Name = "Author")]
        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }
    }

    #endregion

    #region Book Metadata
    public class BookMetadata
    {
        //public int BookID { get; set; } - PK not used
        [DisplayFormat(NullDisplayText = "[-N/A-]")]
        //regexlib.com isbn 10 or 13, top rated submitted by Santiago Neira
        [RegularExpression(@"/((978[\--– ])?[0-9][0-9\--– ]{10}[\--– ][0-9xX])|((978)?[0-9]{9}[0-9Xx])/",
            ErrorMessage = "ISBN needs to be in correct format.")]
        public string ISBN { get; set; }

        [Display(Name = "Title")]
        [Required(ErrorMessage = "* Required")]
        [StringLength(100, ErrorMessage = "* Book Title must be 100 characters or less.")]
        public string BookTitle { get; set; }

        [DisplayFormat(NullDisplayText = "[-N/A-]")]
        [UIHint("MultilineText")]
        public string Description { get; set; }
        //only thing for foreign keys is ErrorMessage on required field IF REQUIRED - all other metadata is retrieved
        //from the lookup table itself
        //public Nullable<int> GenreID { get; set; }
        [DisplayFormat(DataFormatString = "{0:c}", NullDisplayText = "[-N/A-]")]
        public Nullable<decimal> Price { get; set; }

        [Display(Name = "Units Sold")]
        [DisplayFormat(NullDisplayText = "[-N/A-]")]
        [Range(0, int.MaxValue, ErrorMessage = "* Number must be greater than zero and a whole number.")]
        public Nullable<int> UnitsSold { get; set; }

        [Display(Name = "Published Date")]
        //DataformatString changed from {0:d} to what it is now to fuction with Datatables.net for sorting
        [DisplayFormat(NullDisplayText = "[-N/A-]", DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> PublishDate { get; set; }

        //fk
        [Required(ErrorMessage = "* Publisher is required")]
        public int PublisherID { get; set; }

        //no validation, no null display text will be cared for with file upload
        [Display(Name = "Cover")]
        public string BookImage { get; set; }

        [Display(Name = "Site Feature")]
        public bool IsSiteFeature { get; set; }

        [Display(Name = "Genre Feature")]
        public bool IsGenreFeature { get; set; }

        //fk
        [Required(ErrorMessage = "* Book Status is required.")]
        public int BookStatusID { get; set; }
    }
    [MetadataType(typeof(BookMetadata))]
    public partial class Book { }
    #endregion

    #region BookAuthor Metadata
    public class BookAuthorMetadata
    {
        //public int BookAuthorID { get; set; } - PK

        //fK
        [Required(ErrorMessage = "* Book Is Required")]
        public int BookID { get; set; }

        //fK
        [Required(ErrorMessage = "* Author Is Required")]
        public int AuthorID { get; set; }

        [Display(Name = "Orders")]
        [Range(0, byte.MaxValue, ErrorMessage = "* Orders must be a value of 255 or less.")]
        public Nullable<byte> AuthorOrder { get; set; }
    }
    [MetadataType(typeof(BookAuthorMetadata))]
    public partial class BookAuthor { }

    #endregion

    #region BookRoyalty Metadata
    public class BookRoyaltyMetadata
    {
        //public int BookID { get; set; } - pk

        [Display(Name = "Royalty Rate")]
        [DisplayFormat(DataFormatString = "{0:P}", NullDisplayText = "[-N/A-]")]
        public Nullable<decimal> RoyaltyRate { get; set; }

        [DisplayFormat(DataFormatString = "{0:c}", NullDisplayText = "[-N/A-]")]
        public Nullable<decimal> Advance { get; set; }
    }
    [MetadataType(typeof(BookRoyaltyMetadata))]
    public partial class BookRoyalty { }

    #endregion

    #region BookStatus Metadata
    public class BookStatusMetadata
    {
        //public int BookStatusID { get; set; }
        [Display(Name="Status")]
        [Required(ErrorMessage ="* Status Name is Required.")]
        [StringLength(25,ErrorMessage ="* Status Name must be 25 characters or less.")]
        public string BookStatusName { get; set; }

        [DisplayFormat(NullDisplayText = "[-N/A-]")]
        [UIHint("MutlilineText")]
        public string Notes { get; set; }

    }
    [MetadataType(typeof(BookStatusMetadata))]
    public partial class BookStatus { }
    #endregion

    #region Genre Metadata
    public class GenreMetadata
    {
        //public int GenreID { get; set; }
        [Display(Name ="Genre")]
        [StringLength(50, ErrorMessage ="* Genre Name must be 50 characters or less.")]
        public string GenreName { get; set; }
    }
    [MetadataType(typeof(GenreMetadata))]
    public partial class Genre { }
    #endregion

    #region Magazine Metadata
    public class MagazineMetadata
    {
        //public int MagazineID { get; set; }
        [Display(Name ="Magazine")]
        [Required(ErrorMessage ="* Magazine Title is required.")]
        [StringLength(50,ErrorMessage ="* Magazine title must be 50 characters or less.")]
        public string MagazineTitle { get; set; }

        [Display(Name ="Issues/Yr")]
        [Range(1,365, ErrorMessage ="* Issues per year must be a minumum of 1 and maximum of 365.")]
        [Required(ErrorMessage ="* Issues per year is required.")]
        public int IssuesPerYear { get; set; }

        [Display(Name = "Price/Yr")]
        [DisplayFormat(DataFormatString = "{0:c}")]
        [Required(ErrorMessage ="* Price per year is required.")]
        public decimal PricePerYear { get; set; }

        [StringLength(50, ErrorMessage = "* Category must be 50 characters or less.")]
        [DisplayFormat(NullDisplayText ="[-N/A-]")]
        public string Category { get; set; }

        [Range(0,int.MaxValue, ErrorMessage ="* Value must be a whole number.")]
        [DisplayFormat(NullDisplayText = "[-N/A-]")]
        public Nullable<int> Circulation { get; set; }

        [DisplayFormat(NullDisplayText = "[-N/A-]")]
        [StringLength(20,ErrorMessage ="* Frequency must be 20 characters or less.")]
        [Display(Name ="Frequency")]
        public string PublishRate { get; set; }
    }
    [MetadataType(typeof(MagazineMetadata))]
    public partial class Magazine { }
    #endregion

    #region Publisher Metadata
    public class PublisherMetadata
    {
        //public int PublisherID { get; set; }
        [Display(Name ="Publisher")]
        [StringLength(50,ErrorMessage ="* Publisher name must be 50 characters or less.")]
        public string PublisherName { get; set; }

        [StringLength(20, ErrorMessage = "* City must be 20 characters or less.")]
        [DisplayFormat(NullDisplayText ="[-N/A-]")]
        public string City { get; set; }

        [StringLength(2, ErrorMessage = "* State must be 2 characters.")]
        [DisplayFormat(NullDisplayText = "[-N/A-]")]
        public string State { get; set; }

        [Display(Name ="Active")]
        public bool IsActive { get; set; }
    }
    [MetadataType(typeof(PublisherMetadata))]
    public partial class Publisher { }
    #endregion

}
