using Dapper;
using Projeto.Core.Entity;
using Projeto.Core.Infrastructure.Database;
using Projeto.Data.SQLite;
using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace Projeto.Core.Services
{
    public class TarefaService : ICrudService<Tarefa>
    {
        private SQLiteConnection _connection;

        public TarefaService(DatabaseConnection databaseConnection)
        {
            _connection = (SQLiteConnection)databaseConnection.CreateConnection();
        }

        public void DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public Tarefa GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Insert(Tarefa entity)
        {
            _connection.Execute("INSERT INTO tarefa (tipo, descricao, concluida) VALUES(@Tipo, @Descricao, @Concluida)", entity);
        }

        public void Update(Tarefa entity)
        {
            _connection.Execute("UPDATE tarefa SET concluida = @Concluida WHERE id = @Id", entity);
        }
        public void Update(List<Tarefa> entity)
        {
            _connection.Execute("UPDATE tarefa SET concluida = @Concluida WHERE id = @Id", entity);
        }

        public List<Tarefa> GetAll()
        {
            string query = "SELECT id, tipo, descricao, concluida FROM tarefa";
            return _connection.Query<Tarefa>(query).AsList();
        }

    }
}
