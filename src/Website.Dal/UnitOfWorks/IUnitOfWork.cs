using Website.Dal.Bases.Interfaces;
using Website.Dal.Interfaces;
using Website.Shared.Bases.Entities;

namespace Website.Dal.UnitOfWorks
{
    public interface IUnitOfWork
    {
        IPostRepository PostRepository { get; }
        ITeacherRepository TeacherRepository { get; }
        IParentRepository ParentRepository { get; }
        IGalleryRepository GalleryRepository { get; }
        IFacilityRepository FacilityRepository { get; }
        IClassRoomRepository ClassRoomRepository { get; }
        ISpecializedRepository SpecializedRepository { get; }
        IBaseRepository<TEntity, TPrimaryKey> GetRepository<TEntity, TPrimaryKey>() where TEntity : BaseEntity<TPrimaryKey> where TPrimaryKey : struct;
        Task<int> CompleteAsync();
    }
}
