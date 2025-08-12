using Dapper;
using Projeto.Core.Entity;
using System.Data;
using System.Data.SQLite;

namespace Projeto.Data.SQLite
{
    public class DatabaseConnection
    {
        private readonly string _connectionString;

        public DatabaseConnection(string databaseName)
        {
            _connectionString = $"Data Source={databaseName}";

            if (!System.IO.File.Exists(databaseName))
            {
                SQLiteConnection.CreateFile(databaseName);
                EnsureDatabaseExists();
            }


        }

        public IDbConnection CreateConnection()
        {
            return new SQLiteConnection(_connectionString);
        }

        private void EnsureDatabaseExists()
        {
            using var connection = CreateConnection();

            var migrationTable = connection.Query<string>("SELECT name FROM sqlite_master WHERE type='table' AND name = 'migration'").FirstOrDefault();

            if (string.IsNullOrEmpty(migrationTable))
            {
                connection.Execute("CREATE TABLE migration (code TEXT PRIMARY KEY)");
            }

            //A aplicação controla as instruções através da tabela "migration", que armazena os códigos das migrações já aplicadas. Antes de executar uma migração, o código verifica se o código correspondente já está presente na tabela.
            //Se não estiver, a migração é executada e o código é inserido na tabela para indicar que a migração foi aplicada.

            var migrations = connection.Query<string>("SELECT code FROM migration").ToList();

            if (!migrations.Contains("m1"))
            {
                connection.Execute(
                    @"CREATE TABLE cliente (
                       id INTEGER PRIMARY KEY AUTOINCREMENT,
                       nome             TEXT        NOT NULL,
                       data_nascimento  DATETIME    NOT NULL
                    )");

                connection.Execute("INSERT INTO migration VALUES('m1')");
            }

            if (!migrations.Contains("m2"))
            {
                connection.Execute(@"ALTER TABLE cliente ADD email TEXT");

                connection.Execute("INSERT INTO migration VALUES('m2')");
            }

            if (!migrations.Contains("m3"))
            {
                connection.Execute(@"ALTER TABLE cliente ADD telefone INTEGER;");
                connection.Execute(@"ALTER TABLE cliente ADD cidade TEXT;");
                connection.Execute(@"ALTER TABLE cliente ADD genero CHAR;");

                connection.Execute("INSERT INTO migration VALUES('m3')");
            }

            //if(!migrations.Contains("m3"))
            //{
            //    connection.Execute(
            //        @"ALTER TABLE cliente ADD usuario_responsavel INTEGER");
            //    connection.Execute("INSERT INTO migration VALUES('m3')");
            //}

            //if (!migrations.Contains("m4"))
            //{
            //    connection.Execute(
            //        @"CREATE TABLE usuario (
            //           id INTEGER PRIMARY KEY AUTOINCREMENT,
            //           login TEXT NOT NULL,
            //           senha TEXT NOT NULL,
            //           nome TEXT NOT NULL,
            //           administrador BOOLEAN NOT NULL,
            //           ativo BOOLEAN NOT NULL
            //        )");
            //    connection.Execute("INSERT INTO migration VALUES('m4')");
            //}

            //if (!migrations.Contains("m5"))
            //{
            //    connection.Execute(
            //        @"CREATE TABLE log (
            //           id INTEGER PRIMARY KEY AUTOINCREMENT,
            //           data_hora DATETIME NOT NULL,
            //           usuario_id INTEGER NOT NULL,
            //           acao TEXT NOT NULL,
            //           tabela TEXT NOT NULL,
            //           registro_id INTEGER NOT NULL
            //        )");
            //    connection.Execute("INSERT INTO migration VALUES('m5')");
            //}

            //if(!migrations.Contains("m6"))
            //{
            //    connection.Execute(
            //        @"CREATE TABLE usuario_cliente (
            //           id INTEGER PRIMARY KEY AUTOINCREMENT,
            //           usuario_id INTEGER NOT NULL,
            //           cliente_id INTEGER NOT NULL
            //        )");
            //    connection.Execute("INSERT INTO migration VALUES('m6')");
            //}

            if (!migrations.Contains("seed1"))
            {
                string sql = "INSERT INTO cliente (nome, data_nascimento, email, telefone, cidade, genero) " +
                    "Values (@Nome, @DataNascimento, @Email, @Telefone, @Cidade, @Genero);";

                connection.Execute(sql,
                    new[]
                    {
                        new Cliente() { Nome = "Joaquim Ferreira", Email = "joaquim.ferreira@evup.com.br", DataNascimento = new DateTime(1990, 01, 17), Cidade = "São Paulo", Telefone = "11984845454", Genero = "M" },
                        new Cliente() { Nome = "Maria Fernandes", Email = "maria.fernandes@evup.com.br", DataNascimento = new DateTime(1993, 03, 21), Cidade = "São Paulo", Telefone = "11984845454", Genero = "F" },
                        new Cliente() { Nome = "Guilmar Moreira", Email = "joaquimguilmar.moreira@evup.com.br", DataNascimento = new DateTime(1994, 05, 05), Cidade = "São Paulo", Telefone = "11984845454", Genero = "M" },
                        new Cliente() { Nome = "Robson Silvano", Email = "robson.silvano@evup.com.br", DataNascimento = new DateTime(2000, 05, 10) , Cidade = "São Paulo", Telefone = "11984845454", Genero = "M"},
                        new Cliente() { Nome = "Anderson da Silva", Email = "anderson.silva@evup.com.br", DataNascimento = new DateTime(2001, 08, 17) , Cidade = "São Paulo", Telefone = "11984845454", Genero = "M"},
                        new Cliente() { Nome = "Elaine Juscen", Email = "elaine.juscen@evup.com.br", DataNascimento = new DateTime(2001, 10, 21) , Cidade = "São Paulo", Telefone = "11984845454", Genero = "F"},
                        new Cliente() { Nome = "Beatriz Filippa", Email = "beatriz.filippa@evup.com.br", DataNascimento = new DateTime(2002, 01, 11), Cidade = "São Paulo", Telefone = "11984845454", Genero = "F" },
                        new Cliente() { Nome = "Dorotti Galvão", Email = "dorotti.galvao@evup.com.br", DataNascimento = new DateTime(2002, 02, 01) , Cidade = "São Paulo", Telefone = "11984845454", Genero = "F"},
                    });

                connection.Execute("INSERT INTO migration VALUES('seed1')");
            }

            if (!migrations.Contains("tarefa"))
            {

                connection.Execute(
                    @"CREATE TABLE tarefa (
                       id INTEGER PRIMARY KEY AUTOINCREMENT,
                       tipo       TEXT       NOT NULL,
                       descricao  TEXT       NOT NULL,
                       concluida  BOOLEAN    NOT NULL
                    )");

                string sql = "INSERT INTO tarefa (tipo, descricao, concluida) Values (@Tipo, @Descricao, false);";

                connection.Execute(sql, new[]
                {
                    new Tarefa("💄style", "O formulário de edição de clientes está sendo exibido na direita da tela. Coloque-o logo abaixo do título;"),
                    new Tarefa("💄style", "No formulário de exclusão de clientes, altere a cor do botão de azul para vermelho;"),
                    new Tarefa("🐛bug", "Na edição do cadastro de clientes, o campo e-mail está com uma restrição para aceitar apenas 12 caracteres, aumente para 120;"),
                    new Tarefa("🐛bug", "Na listagem do cadastro de clientes, a edição está apresentando um comportamento estranho, os dados apresentados nem sempre representam o registro selecionado. Corrija esse problema;"),
                    new Tarefa("🐛bug", "Na edição do cadastro de clientes, o campo data de nascimento não está sendo atualizado. Corrija este problema;"),
                    new Tarefa("✨feature", "O cadastro de clientes está incompleto, inclua os campos telefone, cidade e gênero;"),
                    new Tarefa("✨feature", "Inclua as colunas faltantes na exibição da listagem de clientes (data de nascimento, telefone, cidade e gênero);"),
                    new Tarefa("♻️refactor", "As migrations (controle de criação e atualização das tabelas do banco de dados) estão em um local inapropriado no código fonte. Faça um melhor gerenciamento para isso;"),
                    new Tarefa("♻️refactor", "Encapsule todo código fonte inerente a regras de negócio dentro de classes de serviço (reaproveite o máximo de código que puder);"),
                    new Tarefa("♻️refactor", "A conexão com o banco de dados em toda a aplicação está sendo feita de forma redundante. Faça um melhor gerenciamento para isso;"),
                    new Tarefa("♻️refactor", "Aplique injeção de dependência para fornecer os serviços para a aplicação;"),
                    new Tarefa("✨feature", "Inclua a funcionalidade para cadastrar um novo cliente;"),
                    new Tarefa("💡comments", "Existem TODOs (tarefas pendentes) espalhadas pelo código fonte (bem como adicionar comentários em classes ou tratar algum erro). Localize esses itens e implemente a solução;"),
                    new Tarefa("✨feature", "Crie um sistema de log para a aplicação. Grave todas as vezes que um registro for alterado no sistema;"),
                    new Tarefa("✨feature", "Crie um cadastro de usuários para o sistema. O cadastro deve ter login, senha, nome, se é administrador ou não e se está ativo ou inativo. (não é necessário implementar edição ou exclusão de registros);"),
                    new Tarefa("✨feature", "Crie um campo de usuário responsável no cadastro de clientes, deve ser do tipo seleção e refletir os dados do cadastro de usuários;"),
                    new Tarefa("✨feature", "Implemente o login da aplicação, fazendo a autenticação dos usuários com base no cadastro de usuários;"),
                    new Tarefa("✨feature", "Garanta que apenas usuários administradores possam dar manutenção no cadastro de usuários;")
                });


                connection.Execute("INSERT INTO migration VALUES('tarefa')");
            }

        }
    }
}