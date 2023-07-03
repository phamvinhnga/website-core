using System.Threading.Tasks;
using Website.Shared.Bases.Entities;
using Website.Shared.Bases.Models;

namespace Website.Shared.Bases.Interfaces
{
    public interface IBaseManager<TEntity, TInputModel, TOutputModel, TPrimaryKey> 
        where TEntity : BaseEntity<TPrimaryKey>
        where TInputModel : class 
        where TOutputModel : class  
        where TPrimaryKey : struct
    {
        Task<(int statusCode, string message, TOutputModel output)> CreateAsync(TInputModel input, int userId);
        Task<(int statusCode, string message, TOutputModel output)> UpdateAsync(TPrimaryKey id, TInputModel input, int userId);
        Task<(int statusCode, string message, TOutputModel ouput)> GetByIdAsync(TPrimaryKey id);
        Task<BasePaginationOutputModel<TOutputModel>> GetListAsync(BasePaginationInputModel input);
        Task<(int statusCode, string message)> DeleteAsync(TPrimaryKey id);
    }
}
