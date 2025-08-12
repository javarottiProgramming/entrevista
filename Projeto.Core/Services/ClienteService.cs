using Dapper;
using Projeto.Core.Entity;
using Projeto.Core.Infrastructure.Database;
using Projeto.Core.Infrastructure.Exceptions;
using Projeto.Data.SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SQLite;
using System.Linq;

namespace Projeto.Core.Services
{
    public class ClienteService : ICrudService<Cliente>
    {
        private SQLiteConnection _connection;

        public ClienteService(DatabaseConnection databaseConnection)
        {
            _connection = (SQLiteConnection)databaseConnection.CreateConnection();
        }

        /// <summary>
        /// Método para validar as regras de negócio antes de inserir ou atualizar  um cliente.
        /// </summary>
        /// <param name="entity"></param>
        /// <exception cref="BusinessException"></exception>
        private void Validate(Cliente entity)
        {
          
            var cliente = _connection.Query<Cliente>(
                "SELECT email FROM cliente WHERE email = @Email and id <> @Id",
                new { entity.Email, entity.Id }).FirstOrDefault();

            if( cliente != null && cliente.Email == entity.Email)
            {
                throw new BusinessException("O e-mail informado já está cadastrado");
            }
        }

        public void DeleteById(int id)
        {
            _connection.Execute("DELETE FROM cliente WHERE id = @Id", new { Id = id });
        }

        public Cliente GetById(int id)
        {
            return _connection.Query<Cliente>("SELECT id, nome, data_nascimento as dataNascimento, email, telefone, cidade, genero FROM cliente where id = @id", new { id }).First();
        }

        public void Insert(Cliente entity)
        {
            Validate(entity);
            _connection.Execute("INSERT INTO cliente (nome, data_nascimento, email, telefone, cidade, genero) Values (@Nome, @DataNascimento, @Email, @Telefone, @Cidade, @Genero);", entity);
        }

        public void Update(Cliente entity)
        {
            Validate(entity);
            _connection.Execute("UPDATE cliente SET nome = @Nome, email = @Email, data_nascimento = @DataNascimento, telefone = @Telefone, cidade = @Cidade, genero = @Genero WHERE id = @Id", entity);
        }

        public List<Cliente> GetAll()
        {
            return _connection.Query<Cliente>("SELECT id, nome, data_nascimento as dataNascimento, email, telefone, cidade, genero FROM cliente")
                .ToList();
        }

        public void Update(List<Cliente> entity)
        {
            throw new NotImplementedException();
        }
    }
}
