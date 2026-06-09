using Microsoft.Data.Sqlite;
using MovieManagement.Domain.Entities;
using MovieManagement.Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace MovieManagement.Data.Repositories
{
    public class RealizadorSqLiteRepository : IRealizadorRepository
    {
        private const string ConnectionString = "Data Source=moviemanagement.db";

        public RealizadorSqLiteRepository()
        {
            using (var connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();
                string query = @"
                    CREATE TABLE IF NOT EXISTS Realizadores (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Nome TEXT NOT NULL,
                        Pais TEXT NOT NULL
                    );";
                using (var command = new SqliteCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Adicionar(Realizador realizador)
        {
            using (var connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();
                string query = "INSERT INTO Realizadores (Nome, Pais) VALUES ($nome, $pais); SELECT last_insert_rowid();";
                using (var command = new SqliteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("$nome", realizador.Nome);
                    command.Parameters.AddWithValue("$pais", realizador.Pais);
                    realizador.Id = Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }

        public List<Realizador> ObterTodos()
        {
            var lista = new List<Realizador>();
            using (var connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();
                string query = "SELECT Id, Nome, Pais FROM Realizadores;";
                using (var command = new SqliteCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new Realizador { Id = reader.GetInt32(0), Nome = reader.GetString(1), Pais = reader.GetString(2) });
                    }
                }
            }
            return lista;
        }

        public Realizador? ObterPorId(int id)
        {
            using (var connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();
                string query = "SELECT Id, Nome, Pais FROM Realizadores WHERE Id = $id;";
                using (var command = new SqliteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("$id", id);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Realizador { Id = reader.GetInt32(0), Nome = reader.GetString(1), Pais = reader.GetString(2) };
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
                string query = "DELETE FROM Realizadores WHERE Id = $id;";
                using (var command = new SqliteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("$id", id);
                    return command.ExecuteNonQuery() > 0;
                }
            }
        }
    }
}