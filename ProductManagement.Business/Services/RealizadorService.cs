using MovieManagement.Domain.Entities;
using MovieManagement.Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace MovieManagement.Business.Services
{
    public class RealizadorService
    {
        private readonly IRealizadorRepository _repository;

        public RealizadorService(IRealizadorRepository repository)
        {
            _repository = repository;
        }

        public void AdicionarRealizador(string nome, string pais)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new Exception("O nome do realizador é obrigatório.");

            if (string.IsNullOrWhiteSpace(pais))
                throw new Exception("O país de origem do realizador é obrigatório.");

            Realizador novoRealizador = new Realizador
            {
                Nome = nome.Trim(),
                Pais = pais.Trim()
            };

            _repository.Adicionar(novoRealizador);
        }

        public List<Realizador> ListarTodos()
        {
            return _repository.ObterTodos();
        }

        public Realizador? ProcurarPorId(int id)
        {
            return _repository.ObterPorId(id);
        }

        public void RemoverRealizador(int id)
        {
            bool removido = _repository.Remover(id);
            if (!removido)
                throw new Exception("Realizador não encontrado.");
        }
    }
}