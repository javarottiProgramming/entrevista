using Dapper;
using Projeto.Core.Entity;
using Projeto.Core.Infrastructure.Database;
using Projeto.Core.Infrastructure.Exceptions;
using Projeto.Data.SQLite;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;

namespace Projeto.Core.Services
{
    public class UsuarioService : ICrudService<Usuario>
    {
        private SQLiteConnection _connection;

        public UsuarioService(DatabaseConnection databaseConnection)
        {
            _connection = (SQLiteConnection)databaseConnection.CreateConnection();
        }

        public void DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Usuario> GetAll()
        {
            return _connection.Query<Usuario>("SELECT id, login, nome, administrador, ativo FROM usuario")
              .ToList();
        }

        public Usuario GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Insert(Usuario entity)
        {
            string sql = "INSERT INTO usuario (login, senha, nome, administrador, ativo) " +
                    "Values (@Login, @Senha, @Nome, @Administrador, @Ativo);";

            _connection.Execute(sql, entity);
        }

        public void Update(Usuario entity)
        {
            throw new NotImplementedException();
        }

        public void Update(List<Usuario> entity)
        {
            throw new NotImplementedException();
        }
    }
}
