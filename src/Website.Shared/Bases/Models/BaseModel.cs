using System;
using System.Collections.Generic;

namespace Website.Shared.Bases.Models
{
    public abstract class AuditUserModel
    {
        public virtual DateTime CreateDate { get; set; }
        public virtual int CreateUser { get; set; }
        public virtual DateTime? ModifyDate { get; set; }
        public virtual int? ModifyUser { get; set; }
    }

    public abstract class BaseModel<TPrimaryKey> : AuditUserModel where TPrimaryKey : struct
    {
        public virtual TPrimaryKey Id { get; set; }
    }

    public abstract class BaseTreeModel<TPrimaryKey> : AuditUserModel where TPrimaryKey : struct
    {
        public TPrimaryKey Id { get; set; }
        public TPrimaryKey ParentId { get; set; }
        public virtual string Type { get; set; }
        public virtual int Index { get; set; }
        public virtual int Order { get; set; }
        public virtual string CodeData { get; set; }
    }
}
