using System.Diagnostics.CodeAnalysis;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Website.Shared.Bases.Interfaces
{
    public interface IBaseEntity<TPrimaryKey> where TPrimaryKey : struct
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public TPrimaryKey Id { get; set; }
        public DateTime CreateDate { get; set; }
        public int CreateUser { get; set; }
        [AllowNull]
        public DateTime? ModifyDate { get; set; }
        [AllowNull]
        public int? ModifyUser { get; set; }

        public void SetCreateDefault(int createUser, DateTime? createDate = null);

        public void SetModifyDefault(int modifyUser, DateTime? modifyDate = null);
    }
}
