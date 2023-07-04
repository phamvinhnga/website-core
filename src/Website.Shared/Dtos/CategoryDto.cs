using System.ComponentModel.DataAnnotations;

namespace Website.Shared.Dtos
{
    public class CategoryDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public int Index { get; set; }
        [Required]
        public int Status { get; set; }
    }

    public class CategoryInputDto : CategoryDto
    {

    }

    public class CategoryOutputDto : CategoryDto
    {

    }
}
