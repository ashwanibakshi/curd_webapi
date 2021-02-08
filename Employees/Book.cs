namespace Employees
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Book
    {
        public int Id { get; set; }

        [Required]
        public string bname { get; set; }

        public int? authors_Id { get; set; }

        public int? geners_Id { get; set; }

        public virtual Author Author { get; set; }

        public virtual Gener Gener { get; set; }
    }
}
