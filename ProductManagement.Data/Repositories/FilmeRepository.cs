using System;
using System.Collections.Generic;
using System.Text;

namespace MovieManagement.Data.Repositories
{
    public class FilmeRepository:IFilmeRepository
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
            
        }
    }
}
