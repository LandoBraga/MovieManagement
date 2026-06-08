using MovieManagement.Domain.Entities;
using MovieManagement.Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace MovieManagement.Data.Repositories
{
    public class FilmeSqLiteRepository : IFilmeRepository
    {
        
        public void Adicionar(Filme filme) { }
        public List<Filme> ObterTodos() { return new List<Filme>(); }
        public Filme? ObterPorTitulo(string titulo) { return null; }
        public bool Remover(int id) { return false; }
    }
}