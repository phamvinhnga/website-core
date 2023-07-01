
using System;
using System.ComponentModel.DataAnnotations;
using Website.Shared.Entities;
using Website.Shared.Extensions;

namespace Website.Entity.Model
{
    public class TeacherInputModel
    {
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public string Instagram { get; set; }
        public int SpecializedId { get; set; }
        public FileModel Thumbnail { get; set; }
        public int Index { get; set; }
        public bool IsDisplayIndexPage { get; set; }
        public bool IsDisplayTeacherPage { get; set; }

        public Teacher MapToEntity()
        {
            var entity = this.JsonMapTo<Teacher>();
            entity.Thumbnail = this.Thumbnail != null ? this.Thumbnail.ConvertToJson() : null;
            return entity;
        }

        public Teacher MapToEntity(Teacher entity)
        {
            entity.Id = Id;
            entity.Surname = Surname;
            entity.Name = Name;
            entity.Facebook = Facebook;
            entity.Twitter = Twitter;
            entity.Instagram = Instagram;
            entity.SpecializedId = SpecializedId;
            entity.Thumbnail = Thumbnail != null ? Thumbnail.ConvertToJson() : null;
            entity.Index = Index;
            entity.IsDisplayIndexPage = IsDisplayIndexPage;
            entity.IsDisplayTeacherPage = IsDisplayTeacherPage;
            return entity;
        }
    }

    public class TeacherOutputModel
    {
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public string Instagram { get; set; }
        public int SpecializedId { get; set; }
        public FileModel Thumbnail { get; set; }
        public SpecializedModel Specialized { get; set; }
        public DateTime CreateDate { get; set; }
        public int CreateUser { get; set; }
        public int Index { get; set; }
        public bool IsDisplayIndexPage { get; set; }
        public bool IsDisplayTeacherPage { get; set; }

        public TeacherOutputModel()
        {

        }

        public TeacherOutputModel(Teacher entity)
        {
            Id = entity.Id;
            Surname = entity.Surname;
            Name = entity.Name;
            FullName = entity.FullName;
            Facebook = entity.Facebook;
            Twitter = entity.Twitter;
            Instagram = entity.Instagram;
            SpecializedId = entity.SpecializedId;
            Thumbnail = entity.Thumbnail != null ? entity.Thumbnail.ConvertFromJson<FileModel>() : null;
            SpecializedId = entity.SpecializedId;
            CreateDate =  entity.CreateDate;
            CreateUser = entity.CreateUser;
            Index = entity.Index;
            IsDisplayIndexPage = entity.IsDisplayIndexPage;
            IsDisplayTeacherPage = entity.IsDisplayTeacherPage;
        }
    }
}
