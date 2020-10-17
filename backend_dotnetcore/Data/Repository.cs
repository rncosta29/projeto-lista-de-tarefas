using backend_dotnetcore.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend_dotnetcore.Data
{
    public class Repository : IRepository
    {
        private readonly Context _context;

        public Repository(Context context)
        {
            _context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<TaskModel[]> GetAllByPeriod(string id)
        {
            IQueryable<TaskModel> query = _context.Tasks;

            query = query.AsNoTracking()
                .OrderBy(task => task.When)
                .Where(t => t.UserId == id);

            return await query.ToArrayAsync();
        }

        public async Task<UserModel[]> GetAllUsers()
        {
            IQueryable<UserModel> query = _context.Usuario;

            query = query.AsNoTracking()
                .OrderBy(user => user.Name);

            return await query.ToArrayAsync();
        }

        public async Task<TaskModel> GetTaskById(string id)
        {
            IQueryable<TaskModel> query = _context.Tasks;

            query = query.AsNoTracking()
                .OrderBy(task => task.Id)
                .Where(task => task.Id == id);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<UserModel> GetUserByCpf(long cpf)
        {
            IQueryable<UserModel> query = _context.Usuario;

            query = query.AsNoTracking()
                .OrderBy(user => user.Id)
                .Where(user => user.Cpf == cpf);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<UserModel> GetUserById(string id)
        {
            IQueryable<UserModel> query = _context.Usuario;

            query = query.AsNoTracking()
                .OrderBy(user => user.Id)
                .Where(user => user.Id == id);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }
    }
}
