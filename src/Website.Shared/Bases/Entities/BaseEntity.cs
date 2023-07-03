using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Website.Shared.Bases.Interfaces;

namespace Website.Shared.Bases.Entities
{
    public class BaseEntity<TPrimaryKey> : IBaseEntity<TPrimaryKey> where TPrimaryKey : struct
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public TPrimaryKey Id { get; set; }
        public virtual DateTime CreateDate { get; set; }
        public virtual int CreateUser { get; set; }

        [AllowNull]
        public virtual DateTime? ModifyDate { get; set; }

        [AllowNull]
        public virtual int? ModifyUser { get; set; }

        public virtual void SetCreateDefault(int createUser, DateTime? createDate = null)
        {
            CreateUser = createUser;
            CreateDate = createDate ?? DateTime.UtcNow;
        }

        public virtual void SetModifyDefault(int modifyUser, DateTime? modifyDate = null)
        {
            ModifyUser = modifyUser;
            ModifyDate = modifyDate ?? DateTime.UtcNow;
        }
    }

    public abstract class BaseTreeEntity<TPrimaryKey> : BaseEntity<TPrimaryKey> where TPrimaryKey : struct
    {
        public TPrimaryKey ParentId { get; set; }
        public virtual string Type { get; set; }
        public virtual int Index { get; set; }
        public virtual int Order { get; set; }
        public virtual string CodeData { get; set; }
    }
}
