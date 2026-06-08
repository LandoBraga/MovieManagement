using MovieManagement.Domain.Entities;
using MovieManagement.Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace MovieManagement.Business.Services
{
    public class MovieService
    {
        private readonly IFilmeRepository _repositorio;

        public MovieService(IFilmeRepository repositorio)
        {
            _repositorio = repositorio;
        }

        public void AdicionarFilme(string titulo, int ano, string lingua, int classificacao)
        {
            if (string.IsNullOrWhiteSpace(titulo))
                throw new Exception("O título do filme é obrigatório.");

            if (classificacao < 0 || classificacao > 5)
                throw new Exception("A classificação deve ser entre 0 e 5.");

            if (ano < 1888 || ano > DateTime.Now.Year + 2)
                throw new Exception("Insira um ano de lançamento válido.");

            Filme? filmeExistente = _repositorio.ObterPorTitulo(titulo);
            if (filmeExistente != null)
                throw new Exception("Já existe um filme com este título.");

            Filme novoFilme = new Filme
            {
                Titulo = titulo.Trim(),
                Ano = ano,
                Lingua = lingua,
                Classificacao = classificacao
            };

            _repositorio.Adicionar(novoFilme);
        }

        public List<Filme> ListarTodos()
        {
            return _repositorio.ObterTodos();
        }

        public Filme? ProcurarPorTitulo(string titulo)
        {
            return _repositorio.ObterPorTitulo(titulo);
        }

        public void RemoverFilme(int id)
        {
            bool removido = _repositorio.Remover(id);
            if (!removido)
                throw new Exception("Filme não encontrado.");
        }
    }
}