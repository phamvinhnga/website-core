using Website.Dal.Bases.Interfaces;
using Website.Dal.Bases.Repository;
using Website.Dal.Interfaces;
using Website.Entity.Repositories;
using Website.Shared.Bases.Entities;

namespace Website.Dal.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private readonly Dictionary<Type, object> _repositories;

        public IPostRepository PostRepository { get; private set; }
        public ITeacherRepository TeacherRepository { get; private set; }
        public IParentRepository ParentRepository { get; private set; }
        public IGalleryRepository GalleryRepository { get; private set; }
        public IFacilityRepository FacilityRepository { get; private set; }
        public IClassRoomRepository ClassRoomRepository { get; private set; }

        public UnitOfWork(
            ApplicationDbContext context,
            IPostRepository postRepository,
            IParentRepository parentRepository,
            ITeacherRepository teacherRepository,
            IFacilityRepository facilityRepository,
            IGalleryRepository galleryRepository,
            IClassRoomRepository classRoomRepository
            )
        {
            _context = context;
            _repositories = new Dictionary<Type, object>();
            PostRepository = postRepository;
            ParentRepository = parentRepository;
            TeacherRepository = teacherRepository;
            FacilityRepository = facilityRepository;
            GalleryRepository = galleryRepository;
            ClassRoomRepository = classRoomRepository;
        }

        public IBaseRepository<TEntity, TPrimaryKey> GetRepository<TEntity, TPrimaryKey>() where TEntity : BaseEntity<TPrimaryKey> where TPrimaryKey : struct
        {
            Type entityType = typeof(TEntity);

            if (!_repositories.ContainsKey(entityType))
            {
                var repositoryType = typeof(BaseRepository<,>).MakeGenericType(entityType, typeof(TPrimaryKey));
                var repository = Activator.CreateInstance(repositoryType, _context);
                _repositories.Add(entityType, repository);
            }

            return (IBaseRepository<TEntity, TPrimaryKey>)_repositories[entityType];
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose() => _context.Dispose();
    }
}
