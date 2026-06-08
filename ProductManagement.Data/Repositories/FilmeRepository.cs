using MovieManagement.Domain.Entities;
using MovieManagement.Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace MovieManagement.Data.Repositories
{
    public class FilmeRepository : IFilmeRepository
    {
        private readonly List<Filme> _filmes;
        private int _proximoId;

        public FilmeRepository()
        {
            _filmes = new List<Filme>();
            _proximoId = 1;
        }

        public void Adicionar(Filme filme)
        {
            filme.Id = _proximoId;
            _proximoId++;
            _filmes.Add(filme);
        }

        public List<Filme> ObterTodos()
        {
            return new List<Filme>(_filmes);
        }

        public Filme? ObterPorTitulo(string titulo)
        {
            foreach (var f in _filmes)
            {
                if (f.Titulo.Equals(titulo.Trim(), StringComparison.OrdinalIgnoreCase))
                {
                    return f;
                }
            }
            return null;
        }

        public bool Remover(int id)
        {
            Filme? filmeParaRemover = null;
            foreach (var f in _filmes)
            {
                if (f.Id == id)
                {
                    filmeParaRemover = f;
                    break;
                }
            }

            if (filmeParaRemover != null)
            {
                _filmes.Remove(filmeParaRemover);
                return true;
            }
            return false;
        }
    }
}