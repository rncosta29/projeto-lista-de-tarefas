using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend_dotnetcore.Model
{
    public class UserModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public long Cpf { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public ICollection<TaskModel> TaskModel { get; set; }

        public UserModel(string id, string nome, string email, long cpf, string senha, string confirmarSenha)
        {
            Id = id;
            Name = nome;
            Email = email;
            Cpf = cpf;
            Password = senha;
            ConfirmPassword = confirmarSenha;
        }

        public UserModel() { }
    }
}
