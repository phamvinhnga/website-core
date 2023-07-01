﻿using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Website.Shared.Bases.Entities
{
    public abstract class BaseEntity<TPrimaryKey> where TPrimaryKey : struct
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public TPrimaryKey Id { get; set; }

        public virtual DateTime CreateDate { get; set; }

        public int CreateUser { get; set; }

        public virtual DateTime ModifyDate { get; set; }

        public int ModifyUser { get; set; }

        public virtual void SetCreateDefault(int createUser, DateTime? createDate = null)
        {
            CreateUser = createUser;
            CreateDate = createDate ?? DateTime.Now;
        }

        public virtual void SetModifyDefault(int modifyUser, DateTime? modifyDate = null)
        {
            ModifyUser = modifyUser;
            ModifyDate = modifyDate ?? DateTime.Now;
        }
    }

    public abstract class BaseTreeEntity<TPrimaryKey> where TPrimaryKey : struct
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public TPrimaryKey Id { get; set; }
        public TPrimaryKey ParentId { get; set; }
        public virtual string Type { get; set; }
        public virtual int Index { get; set; }
        public virtual int Order { get; set; }
        public virtual string CodeData { get; set; }

        public virtual DateTime CreateDate { get; set; }

        public int CreateUser { get; set; }

        [AllowNull]
        public virtual DateTime? ModifyDate { get; set; }

        [AllowNull]
        private int? ModifyUser { get; set; }

        public virtual void SetCreateDefault(int createUser, DateTime? createDate = null)
        {
            CreateUser = createUser;
            CreateDate = createDate ?? DateTime.Now;
        }

        public virtual void SetModifyDefault(int modifyUser, DateTime? modifyDate = null)
        {
            ModifyUser = modifyUser;
            ModifyDate = modifyDate ?? DateTime.Now;
        }
    }
}
