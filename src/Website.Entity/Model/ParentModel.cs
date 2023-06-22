using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Website.Entity.Model
{
    public class ParentInputModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(64)]
        public string Surname { get; set; }
        [Required]
        [StringLength(64)]
        public string Name { get; set; }
        public string Profession { get; set; }
        public string Feedback { get; set; }
        public FileModel Thumbnail { get; set; }
        public int Index { get; set; }
        public bool IsDisplayIndexPage { get; set; }
    }

    public class ParentOutputModel
    {
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public string Profession { get; set; }
        public string Feedback { get; set; }
        public FileModel Thumbnail { get; set; }
        public DateTime CreateDate { get; set; }
        public int CreateUser { get; set; }        
        public int Index { get; set; }
        public bool IsDisplayIndexPage { get; set; }
    }
}
