using Microsoft.Data.Sqlite;
using MovieManagement.Domain.Entities;
using MovieManagement.Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace MovieManagement.Data.Repositories
{
    public class FilmeSqLiteRepository : IFilmeRepository
    {
        private const string ConnectionString = "Data Source=moviemanagement.db";

        public FilmeSqLiteRepository()
        {
            // Criar a tabela de Filmes se ela não existir ao iniciar
            using (var connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();
                string query = @"
                    CREATE TABLE IF NOT EXISTS Filmes (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Titulo TEXT NOT NULL,
                        Ano INTEGER NOT NULL,
                        Lingua TEXT,
                        Classificacao INTEGER NOT NULL,
                        CategoriaId INTEGER NOT NULL,
                        RealizadorId INTEGER NOT NULL
                    );";
                using (var command = new SqliteCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Adicionar(Filme filme)
        {
            using (var connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();
                string query = @"
                    INSERT INTO Filmes (Titulo, Ano, Lingua, Classificacao, CategoriaId, RealizadorId) 
                    VALUES ($titulo, $ano, $lingua, $classificacao, $categoriaId, $realizadorId);
                    SELECT last_insert_rowid();";

                using (var command = new SqliteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("$titulo", filme.Titulo);
                    command.Parameters.AddWithValue("$ano", filme.Ano);
                    command.Parameters.AddWithValue("$lingua", filme.Lingua ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("$classificacao", filme.Classificacao);
                    command.Parameters.AddWithValue("$categoriaId", filme.CategoriaId);
                    command.Parameters.AddWithValue("$realizadorId", filme.RealizadorId);

                    // Atribui o ID gerado pelo SQLite de volta ao objeto filme
                    filme.Id = Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }

        public List<Filme> ObterTodos()
        {
            var lista = new List<Filme>();
            using (var connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();
                string query = "SELECT Id, Titulo, Ano, Lingua, Classificacao, CategoriaId, RealizadorId FROM Filmes;";
                using (var command = new SqliteCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new Filme
                        {
                            Id = reader.GetInt32(0),
                            Titulo = reader.GetString(1),
                            Ano = reader.GetInt32(2),
                            Lingua = reader.IsDBNull(3) ? "" : reader.GetString(3),
                            Classificacao = reader.GetInt32(4),
                            CategoriaId = reader.GetInt32(5),
                            RealizadorId = reader.GetInt32(6)
                        });
                    }
                }
            }
            return lista;
        }

        public Filme? ObterPorTitulo(string titulo)
        {
            using (var connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();
                string query = "SELECT Id, Titulo, Ano, Lingua, Classificacao, CategoriaId, RealizadorId FROM Filmes WHERE Titulo LIKE $titulo LIMIT 1;";
                using (var command = new SqliteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("$titulo", titulo.Trim());
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Filme
                            {
                                Id = reader.GetInt32(0),
                                Titulo = reader.GetString(1),
                                Ano = reader.GetInt32(2),
                                Lingua = reader.IsDBNull(3) ? "" : reader.GetString(3),
                                Classificacao = reader.GetInt32(4),
                                CategoriaId = reader.GetInt32(5),
                                RealizadorId = reader.GetInt32(6)
                            };
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
                string query = "DELETE FROM Filmes WHERE Id = $id;";
                using (var command = new SqliteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("$id", id);
                    int linhasAfetadas = command.ExecuteNonQuery();
                    return linhasAfetadas > 0;
                }
            }
        }
    }
}