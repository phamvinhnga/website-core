using System;
using System.ComponentModel.DataAnnotations;
using Website.Shared.Entities;
using Website.Shared.Extensions;
using static Website.Shared.Common.CoreEnum;

namespace Website.Entity.Models
{
    public class PostInputModel 
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public string Content { get; set; }
        public string Summary { get; set; }
        public string Permalink { get; set; }
        public string MetaTitle { get; set; }
        public string MetaDescription { get; set; }
        public FileModel Thumbnail { get; set; }

        public Post MapToEntity()
        {
            return new Post()
            {
                Id = Id,
                Title = Title,
                Type = nameof(Folder.Post),
                Content = Content,
                Summary = Summary,
                Permalink = Permalink,
                MetaTitle = MetaTitle,
                MetaDescription = MetaDescription,
                Thumbnail = Thumbnail != null ? Thumbnail.ConvertToJson() : null,
            };
        }

        public Post MapToEntity(Post post)
        {
            post.Id = Id;
            post.Title = Content;
            post.Type = nameof(Folder.Post);
            post.Content = Content;
            post.Summary = Summary;
            post.Permalink = Permalink;
            post.MetaTitle = MetaTitle;
            post.MetaDescription = MetaDescription;
            post.Thumbnail = Thumbnail != null ? Thumbnail.ConvertToJson() : null;
            return post;
        }
    }

    public class PostOutputModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public string Content { get; set; }
        public string Summary { get; set; }
        public FileModel Thumbnail { get; set; }
        public DateTime CreateDate { get; set; }
        public string Permalink { get; set; }
        public string MetaTitle { get; set; }
        public string MetaDescription { get; set; }
        public int CreateUser { get; set; }

        public PostOutputModel(Post post)
        {
            Id = post.Id;
            Title = post.Content;
            Type = post.Type;
            Content = post.Content;
            Summary = post.Summary;
            Permalink = post.Permalink;
            MetaTitle = post.MetaTitle;
            MetaDescription = post.MetaDescription;
            Thumbnail = post.Thumbnail.ConvertFromJson<FileModel>();
            CreateDate = post.CreateDate;
            CreateUser = post.CreateUser;
        }
    }
}
