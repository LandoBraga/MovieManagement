using MovieManagement.Domain.Entities;
using MovieManagement.Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace MovieManagement.Business.Services
{
    public class MovieService
    {
        private readonly IFilmeRepository _filmeRepository;
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly IRealizadorRepository _realizadorRepository;

        // Atualizado o construtor para receber os 3 repositórios
        public MovieService(
            IFilmeRepository filmeRepository,
            ICategoriaRepository categoriaRepository,
            IRealizadorRepository realizadorRepository)
        {
            _filmeRepository = filmeRepository;
            _categoriaRepository = categoriaRepository;
            _realizadorRepository = realizadorRepository;
        }

        // Método atualizado com CategoriaId e RealizadorId
        public void AdicionarFilme(string titulo, int ano, string lingua, int classificacao, int categoriaId, int realizadorId)
        {
            if (string.IsNullOrWhiteSpace(titulo))
                throw new Exception("O título do filme é obrigatório.");

            if (classificacao < 0 || classificacao > 5)
                throw new Exception("A classificação deve ser entre 0 e 5.");

            if (ano < 1888 || ano > DateTime.Now.Year + 2)
                throw new Exception("Insira um ano de lançamento válido.");

            // 1. REGRA DA PARTE 3: Validar se a Categoria existe
            var categoriaExistente = _categoriaRepository.ObterPorId(categoriaId);
            if (categoriaExistente == null)
                throw new Exception($"A categoria com o ID {categoriaId} não existe no sistema.");

            // 2. REGRA DA PARTE 3: Validar se o Realizador existe
            var realizadorExistente = _realizadorRepository.ObterPorId(realizadorId);
            if (realizadorExistente == null)
                throw new Exception($"O realizador com o ID {realizadorId} não existe no sistema.");

            // Validar se já existe um filme com o mesmo título
            Filme? filmeExistente = _filmeRepository.ObterPorTitulo(titulo);
            if (filmeExistente != null)
                throw new Exception("Já existe um filme com este título.");

            Filme novoFilme = new Filme
            {
                Titulo = titulo.Trim(),
                Ano = ano,
                Lingua = lingua,
                Classificacao = classificacao,
                CategoriaId = categoriaId,   // Gravando a relação
                RealizadorId = realizadorId    // Gravando a relação
            };

            _filmeRepository.Adicionar(novoFilme);
        }

        public List<Filme> ListarTodos()
        {
            return _filmeRepository.ObterTodos();
        }

        public Filme? ProcurarPorTitulo(string titulo)
        {
            return _filmeRepository.ObterPorTitulo(titulo);
        }

        public void RemoverFilme(int id)
        {
            bool removido = _filmeRepository.Remover(id);
            if (!removido)
                throw new Exception("Filme não encontrado.");
        }
    }
}