using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website.Shared.Bases.Entities;

namespace Website.Shared.Entities
{
    [Table("ClassRoom")]
    public class ClassRoom : BaseEntity<int>
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public int FromAge { get; set; }
        [Required]
        public int ToAge { get; set; }
        public int TotalSeats { get; set; }
        public string Thumbnail { get; set; }
        public decimal TutionFee { get; set; }
        public string TutionType { get; set; }
        [Required]
        public string FromTime { get; set; }
        [Required]
        public string ToTime { get; set; }
        [Required]
        public int Index { get; set; }
        public bool IsDisplayIndexPage { get; set; }
        public bool IsDisplayClassRoomPage { get; set; }
        [Required]
        public bool Status { get; set; }
    }
}
