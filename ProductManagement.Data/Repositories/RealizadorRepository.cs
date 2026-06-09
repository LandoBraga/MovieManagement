using MovieManagement.Domain.Entities;
using MovieManagement.Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace MovieManagement.Data.Repositories
{
    public class RealizadorRepository : IRealizadorRepository
    {
        private readonly List<Realizador> _realizadores;
        private int _proximoId;

        public RealizadorRepository()
        {
            _realizadores = new List<Realizador>();
            _proximoId = 1;
        }

        public void Adicionar(Realizador realizador)
        {
            realizador.Id = _proximoId;
            _proximoId++;
            _realizadores.Add(realizador);
        }

        public List<Realizador> ObterTodos()
        {
            return new List<Realizador>(_realizadores);
        }

        public Realizador? ObterPorId(int id)
        {
            foreach (var r in _realizadores)
            {
                if (r.Id == id) return r;
            }
            return null;
        }

        public bool Remover(int id)
        {
            Realizador? realizadorParaRemover = ObterPorId(id);
            if (realizadorParaRemover != null)
            {
                _realizadores.Remove(realizadorParaRemover);
                return true;
            }
            return false;
        }
    }
}