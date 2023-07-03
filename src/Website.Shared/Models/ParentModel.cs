using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website.Shared.Entities;
using Website.Shared.Extensions;

namespace Website.Entity.Model
{
    public class ParentInputModel
    {
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Profession { get; set; }
        public string Feedback { get; set; }
        public FileModel Thumbnail { get; set; }
        public int Index { get; set; }
        public bool IsDisplayIndexPage { get; set; }

        public Parent MapToEntity()
        {
            return new Parent()
            {
                Id = Id,
                Surname = Surname,
                Name = Name,
                Profession = Profession,
                Feedback = Feedback,
                IsDisplayIndexPage = IsDisplayIndexPage,
                Index = Index,
                Thumbnail = Thumbnail != null ? Thumbnail.ConvertToJson() : null,
            };
        }
        public Parent MapToEntity(Parent enity)
        {
            enity.Id = Id;
            enity.Surname = Surname;
            enity.Name = Name;
            enity.Profession = Profession;
            enity.Feedback = Feedback;
            enity.IsDisplayIndexPage = IsDisplayIndexPage;
            enity.Index = Index;
            enity.Thumbnail = Thumbnail != null ? Thumbnail.ConvertToJson() : null;
            return enity;
        }
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

        public ParentOutputModel(Parent entity)
        {
            Id = entity.Id;
            Surname = entity.Surname;
            Name = entity.Name;
            FullName = entity.FullName;
            Profession = entity.Profession;
            Feedback = entity.Feedback;
            Thumbnail = entity.Thumbnail.ConvertFromJson<FileModel>();
            CreateDate = entity.CreateDate;
            CreateUser = entity.CreateUser;
            Index = entity.Index;
            IsDisplayIndexPage = entity.IsDisplayIndexPage;
        }
    }
}
