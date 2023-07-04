﻿using Website.Entity.Models;
using Website.Shared.Entities;
using Website.Shared.Extensions;

namespace Website.Shared.Models
{
    public class GalleryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public FileModel Thumbnail { get; set; }
        public int Index { get; set; }
        public int CategoryId { get; set; }
        public bool IsDisplayGalleryPage { get; set; }
    }

    public class GalleryInputModel : GalleryModel
    {
        public GalleryInputModel() { }

        public Gallery MapToEntity()
        {
            return new Gallery 
            { 
                Id = Id,
                Name = Name,
                Thumbnail = Thumbnail != null ? Thumbnail.ConvertToJson() : null,
                Index = Index,
                CategoryId = CategoryId,
                IsDisplayGalleryPage = IsDisplayGalleryPage
            };
        }

        public Gallery MapToEntity(Gallery entity)
        {
            entity.Name = Name;
            entity.Thumbnail = Thumbnail != null ? Thumbnail.ConvertToJson() : null;
            entity.Index = Index;
            entity.CategoryId = CategoryId;
            entity.IsDisplayGalleryPage = IsDisplayGalleryPage;
            return entity;
        }
    }

    public class GalleryOutputModel : GalleryModel
    {
        public GalleryOutputModel(Gallery entity) 
        {
            Id = entity.Id;
            Name = entity.Name;
            Thumbnail = Thumbnail != null ? entity.Thumbnail.ConvertFromJson<FileModel>() : null;
            Index = entity.Index;
            CategoryId = entity.CategoryId;
            IsDisplayGalleryPage = entity.IsDisplayGalleryPage;
        }
    }
}
