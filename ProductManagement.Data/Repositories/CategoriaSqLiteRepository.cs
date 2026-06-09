using Microsoft.Data.Sqlite;
using MovieManagement.Domain.Entities;
using MovieManagement.Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace MovieManagement.Data.Repositories
{
    public class CategoriaSqLiteRepository : ICategoriaRepository
    {
        private const string ConnectionString = "Data Source=moviemanagement.db";

        public CategoriaSqLiteRepository()
        {
            using (var connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();
                string query = @"
                    CREATE TABLE IF NOT EXISTS Categorias (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Nome TEXT NOT NULL UNIQUE
                    );";
                using (var command = new SqliteCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Adicionar(Categoria categoria)
        {
            using (var connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();
                string query = "INSERT INTO Categorias (Nome) VALUES ($nome); SELECT last_insert_rowid();";
                using (var command = new SqliteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("$nome", categoria.Nome);
                    categoria.Id = Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }

        public List<Categoria> ObterTodas()
        {
            var lista = new List<Categoria>();
            using (var connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();
                string query = "SELECT Id, Nome FROM Categorias;";
                using (var command = new SqliteCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new Categoria { Id = reader.GetInt32(0), Nome = reader.GetString(1) });
                    }
                }
            }
            return lista;
        }

        public Categoria? ObterPorNome(string nome)
        {
            using (var connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();
                string query = "SELECT Id, Nome FROM Categorias WHERE LOWER(Nome) = LOWER($nome) LIMIT 1;";
                using (var command = new SqliteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("$nome", nome.Trim());
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Categoria { Id = reader.GetInt32(0), Nome = reader.GetString(1) };
                        }
                    }
                }
            }
            return null;
        }

        public Categoria? ObterPorId(int id)
        {
            using (var connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();
                string query = "SELECT Id, Nome FROM Categorias WHERE Id = $id;";
                using (var command = new SqliteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("$id", id);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Categoria { Id = reader.GetInt32(0), Nome = reader.GetString(1) };
                        }
                    }
                }
            }
            return null;
        }

        public bool Remover(int id)
        {
            using (var connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();
                string query = "DELETE FROM Categorias WHERE Id = $id;";
                using (var command = new SqliteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("$id", id);
                    return command.ExecuteNonQuery() > 0;
                }
            }
        }
    }
}