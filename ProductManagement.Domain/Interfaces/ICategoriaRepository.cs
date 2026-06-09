using MovieManagement.Domain.Entities;
using System.Collections.Generic;

namespace MovieManagement.Domain.Interfaces
{
    public interface ICategoriaRepository
    {
        void Adicionar(Categoria categoria);
        List<Categoria> ObterTodas();
        Categoria? ObterPorNome(string nome);
        Categoria? ObterPorId(int id);
        bool Remover(int id);
    }
}