using Projeto.Core.Infrastructure.Database;
using System.ComponentModel.DataAnnotations;

namespace Projeto.Core.Entity
{
    public class Usuario : IEntity
    {
        public Usuario()
        {
            
        }

        public Usuario(string login, string senha, string nome, bool administrador, bool ativo)
        {
            this.Login = login;
            this.Senha = senha;
            this.Nome = nome;
            this.Administrador = administrador;
            this.Ativo = ativo;
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [MaxLength(15, ErrorMessage = "O limite máximo de caracteres para o campo foi atingido.")]
        [Display(Name = "Login")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [MaxLength(15, ErrorMessage = "O limite máximo de caracteres para o campo foi atingido.")]
        [Display(Name = "Senha")]
        public string Senha { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [MaxLength(100, ErrorMessage = "O limite máximo de caracteres para o campo foi atingido.")]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Display(Name = "Administrador")]
        public bool Administrador { get; set; }

        [Display(Name = "Ativo")]
        public bool Ativo { get; set; }
    }
}