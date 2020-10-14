using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace backend_dotnetcore.Model
{
    public class TaskModel
    {
        public string Id { get; set; }

        // [Required(ErrorMessage = "O campo {0} é obrigatório")]
        // public string MacAddress { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public int Type { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Title { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Description { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public DateTime When { get; set; }

        public bool Done { get; set; }
        public DateTime Created { get; set; }
        public virtual UserModel User { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }

        public TaskModel(string id, int type, string title, string description, DateTime when)
        {
            Id = id;
            Type = type;
            Title = title;
            Description = description;
            When = when;
            Done = false;
        }

        public TaskModel() { }
    }
}
