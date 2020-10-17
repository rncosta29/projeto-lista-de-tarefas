using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace backend_dotnetcore.Model
{
    public class UserModel
    {
        public string Id { get; set; }

        // [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Email { get; set; }

        // [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public long Cpf { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Password { get; set; }

        // [Required(ErrorMessage = "O campo {0} é obrigatório")]
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
