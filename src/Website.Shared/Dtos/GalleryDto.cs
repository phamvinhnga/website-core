﻿using System.ComponentModel.DataAnnotations;

namespace Website.Shared.Dtos
{
    public class GalleryDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public FileImgeDto Thumbnail { get; set; }
        [Required]
        public int Index { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public bool IsDisplayGalleryPage { get; set; }
    }

    public class GalleryInputDto : GalleryDto
    {
        public GalleryInputDto() { }
    }

    public class GalleryOutputDto : GalleryDto
    {
    }
}
