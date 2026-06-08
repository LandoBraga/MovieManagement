using MovieManagement.Business.Services;
using MovieManagement.Data.Repositories;
using MovieManagement.Domain.Entities;
using System;

namespace MovieManagementUI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Instanciação
            FilmeRepository repositorioMemoria = new FilmeRepository();
            MovieService movieService = new MovieService(repositorioMemoria);

            bool continuar = true;

            while (continuar)
            {
                Console.Clear();
                Console.WriteLine("=== MOVIE MANAGEMENT SYSTEM ===");
                Console.WriteLine("1. Adicionar Filme");
                Console.WriteLine("2. Listar Todos os Filmes");
                Console.WriteLine("3. Pesquisar Filme por Título");
                Console.WriteLine("4. Remover Filme");
                Console.WriteLine("0. Sair");
                Console.Write("Escolha uma opção: ");

                string opcao = Console.ReadLine() ?? "";

                try
                {
                    switch (opcao)
                    {
                        case "1":
                            Console.Clear();
                            Console.WriteLine("--- Adicionar Novo Filme ---");
                            Console.Write("Título: ");
                            string titulo = Console.ReadLine() ?? "";

                            Console.Write("Ano de Lançamento: ");
                            int ano = int.Parse(Console.ReadLine() ?? "0");

                            Console.Write("Língua: ");
                            string lingua = Console.ReadLine() ?? "";

                            Console.Write("Classificação (0 a 5): ");
                            int classificacao = int.Parse(Console.ReadLine() ?? "-1");

                            movieService.AdicionarFilme(titulo, ano, lingua, classificacao);
                            Console.WriteLine("\nFilme adicionado com sucesso!");
                            break;

                        case "2":
                            Console.Clear();
                            Console.WriteLine("--- Lista de Filmes Catálogados ---");
                            var lista = movieService.ListarTodos();

                            if (lista.Count == 0)
                            {
                                Console.WriteLine("Nenhum filme registado de momento.");
                            }
                            else
                            {
                                foreach (var f in lista)
                                {
                                    Console.WriteLine($"ID: {f.Id} | {f.Titulo} ({f.Ano}) - {f.Lingua} | Nota: {f.Classificacao}/5");
                                }
                            }
                            break;

                        case "3":
                            Console.Clear();
                            Console.WriteLine("--- Pesquisar Filme ---");
                            Console.Write("Introduza o título a procurar: ");
                            string termoBusca = Console.ReadLine() ?? "";

                            Filme? filmeEncontrado = movieService.ProcurarPorTitulo(termoBusca);
                            if (filmeEncontrado != null)
                            {
                                Console.WriteLine($"\nFilme Encontrado:\nID: {filmeEncontrado.Id}\nTítulo: {filmeEncontrado.Titulo}\nAno: {filmeEncontrado.Ano}\nLíngua: {filmeEncontrado.Lingua}\nClassificação: {filmeEncontrado.Classificacao}/5");
                            }
                            else
                            {
                                Console.WriteLine("\nNenhum filme encontrado com esse título.");
                            }
                            break;

                        case "4":
                            Console.Clear();
                            Console.WriteLine("--- Remover Filme ---");
                            Console.Write("Introduza o ID do filme a remover: ");
                            int idRemover = int.Parse(Console.ReadLine() ?? "0");

                            movieService.RemoverFilme(idRemover);
                            Console.WriteLine("\nFilme removido com sucesso!");
                            break;

                        case "0":
                            continuar = false;
                            Console.WriteLine("A fechar a aplicação...");
                            break;

                        default:
                            Console.WriteLine("Opção inválida! Tente novamente.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    // Captura dos erros das regras de negócio 
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"\nErro: {ex.Message}");
                    Console.ResetColor();
                }

                if (opcao != "0")
                {
                    Console.WriteLine("\nPressione qualquer tecla para voltar ao menu...");
                    Console.ReadKey();
                }
            }
        }
    }
}