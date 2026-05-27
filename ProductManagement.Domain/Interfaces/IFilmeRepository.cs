using MovieManagement.Domain.Entities;
using System.Collections.Generic;

namespace MovieManagement.Domain.Interfaces
{
    public interface IFilmeRepository
    {
        void Adicionar(Filme filme);
        List<Filme> ObterTodos();
        Filme? ObterPorTitulo(string titulo);
        bool Remover(int id);
    }
}