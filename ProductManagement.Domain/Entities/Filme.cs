namespace MovieManagement.Domain.Entities
{
    public class Filme
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public int Ano { get; set; }
        public string Lingua { get; set; }
        public int Classificacao { get; set; }

        // Novas propriedades para as relações da Parte 3
        public int CategoriaId { get; set; }
        public int RealizadorId { get; set; }

        public Filme()
        {
            Titulo = string.Empty;
            Lingua = string.Empty;
        }
    }
}