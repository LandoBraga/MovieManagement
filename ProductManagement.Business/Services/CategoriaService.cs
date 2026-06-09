using MovieManagement.Domain.Entities;
using MovieManagement.Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace MovieManagement.Business.Services
{
    public class CategoriaService
    {
        private readonly ICategoriaRepository _repository;

        public CategoriaService(ICategoriaRepository repository)
        {
            _repository = repository;
        }

        public void AdicionarCategoria(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new Exception("O nome da categoria é obrigatório.");

            
            Categoria? existente = _repository.ObterPorNome(nome);
            if (existente != null)
                throw new Exception("Já existe uma categoria com este nome.");

            Categoria novaCategoria = new Categoria
            {
                Nome = nome.Trim()
            };

            _repository.Adicionar(novaCategoria);
        }

        public List<Categoria> ListarTodas()
        {
            return _repository.ObterTodas();
        }

        public Categoria? ProcurarPorId(int id)
        {
            return _repository.ObterPorId(id);
        }

        public void RemoverCategoria(int id)
        {
            bool removido = _repository.Remover(id);
            if (!removido)
                throw new Exception("Categoria não encontrada.");
        }
    }
}