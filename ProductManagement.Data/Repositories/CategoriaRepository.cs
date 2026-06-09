using MovieManagement.Domain.Entities;
using MovieManagement.Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace MovieManagement.Data.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly List<Categoria> _categorias;
        private int _proximoId;

        public CategoriaRepository()
        {
            _categorias = new List<Categoria>();
            _proximoId = 1;
        }

        public void Adicionar(Categoria categoria)
        {
            categoria.Id = _proximoId;
            _proximoId++;
            _categorias.Add(categoria);
        }

        public List<Categoria> ObterTodas()
        {
            return new List<Categoria>(_categorias);
        }

        public Categoria? ObterPorNome(string nome)
        {
            foreach (var c in _categorias)
            {
                if (c.Nome.Equals(nome.Trim(), StringComparison.OrdinalIgnoreCase))
                {
                    return c;
                }
            }
            return null;
        }

        public Categoria? ObterPorId(int id)
        {
            foreach (var c in _categorias)
            {
                if (c.Id == id) return c;
            }
            return null;
        }

        public bool Remover(int id)
        {
            Categoria? categoriaParaRemover = ObterPorId(id);
            if (categoriaParaRemover != null)
            {
                _categorias.Remove(categoriaParaRemover);
                return true;
            }
            return false;
        }
    }
}