using backend_dotnetcore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend_dotnetcore.Data
{
    public interface IRepository
    {
        void Add<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveChangesAsync();

        Task<TaskModel[]> GetAllByPeriod(string id);
        Task<TaskModel> GetTaskById(string id);

        Task<UserModel[]> GetAllUsers();
        Task<UserModel> GetUserById(string id);
        Task<UserModel> GetUserByCpf(long cpf);
    }
}
