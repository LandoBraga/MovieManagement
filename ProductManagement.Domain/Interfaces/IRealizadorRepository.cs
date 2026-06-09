using MovieManagement.Domain.Entities;
using System.Collections.Generic;

namespace MovieManagement.Domain.Interfaces
{
    public interface IRealizadorRepository
    {
        void Adicionar(Realizador realizador);
        List<Realizador> ObterTodos();
        Realizador? ObterPorId(int id);
        bool Remover(int id);
    }
}