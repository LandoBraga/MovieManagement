using MovieManagement.Business.Services;
using MovieManagement.Data.Repositories;
using MovieManagement.Domain.Entities;
using System;

namespace MovieManagementUI
{
    internal class Program
    {
        // Instanciação global dos repositórios e serviços em memória
        private static readonly FilmeRepository _filmeRepository = new FilmeRepository();
        private static readonly MovieService _movieService = new MovieService(_filmeRepository);

        private static readonly CategoriaRepository _categoriaRepository = new CategoriaRepository();
        private static readonly CategoriaService _categoriaService = new CategoriaService(_categoriaRepository);

        private static readonly RealizadorRepository _realizadorRepository = new RealizadorRepository();
        private static readonly RealizadorService _realizadorService = new RealizadorService(_realizadorRepository);

        static void Main(string[] args)
        {
            bool continuar = true;

            while (continuar)
            {
                Console.Clear();
                Console.WriteLine("=====================================");
                Console.WriteLine("     MOVIE MANAGEMENT SYSTEM         ");
                Console.WriteLine("=====================================");
                Console.WriteLine("1. Gestão de Filmes");
                Console.WriteLine("2. Gestão de Categorias");
                Console.WriteLine("3. Gestão de Realizadores");
                Console.WriteLine("0. Sair da Aplicação");
                Console.WriteLine("=====================================");
                Console.Write("Escolha uma opção: ");

                string opcao = Console.ReadLine() ?? "";

                switch (opcao)
                {
                    case "1":
                        MenuFilmes();
                        break;
                    case "2":
                        MenuCategorias();
                        break;
                    case "3":
                        MenuRealizadores();
                        break;
                    case "0":
                        continuar = false;
                        Console.WriteLine("\nA fechar a aplicação... Bom trabalho!");
                        break;
                    default:
                        Console.WriteLine("\nOpção inválida! Pressione qualquer tecla para tentar novamente.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        #region MENU FILMES
        static void MenuFilmes()
        {
            bool voltar = false;
            while (!voltar)
            {
                Console.Clear();
                Console.WriteLine("--- GESTÃO DE FILMES ---");
                Console.WriteLine("1. Adicionar Filme");
                Console.WriteLine("2. Listar Todos os Filmes");
                Console.WriteLine("3. Pesquisar por Título");
                Console.WriteLine("4. Remover Filme");
                Console.WriteLine("0. Voltar ao Menu Principal");
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

                            _movieService.AdicionarFilme(titulo, ano, lingua, classificacao);
                            Console.WriteLine("\nFilme adicionado com sucesso!");
                            break;

                        case "2":
                            Console.Clear();
                            Console.WriteLine("--- Lista de Filmes Catálogados ---");
                            var lista = _movieService.ListarTodos();
                            if (lista.Count == 0) Console.WriteLine("Nenhum filme registado.");
                            else
                            {
                                foreach (var f in lista)
                                    Console.WriteLine($"ID: {f.Id} | {f.Titulo} ({f.Ano}) - {f.Lingua} | Nota: {f.Classificacao}/5");
                            }
                            break;

                        case "3":
                            Console.Clear();
                            Console.WriteLine("--- Pesquisar Filme ---");
                            Console.Write("Introduza o título: ");
                            string termo = Console.ReadLine() ?? "";
                            Filme? fEncontrado = _movieService.ProcurarPorTitulo(termo);
                            if (fEncontrado != null)
                                Console.WriteLine($"\nEncontrado -> ID: {fEncontrado.Id} | {fEncontrado.Titulo} ({fEncontrado.Ano})");
                            else Console.WriteLine("\nFilme não encontrado.");
                            break;

                        case "4":
                            Console.Clear();
                            Console.WriteLine("--- Remover Filme ---");
                            Console.Write("ID do filme a remover: ");
                            int idRemover = int.Parse(Console.ReadLine() ?? "0");
                            _movieService.RemoverFilme(idRemover);
                            Console.WriteLine("\nFilme removido com sucesso!");
                            break;

                        case "0":
                            voltar = true;
                            break;
                        default:
                            Console.WriteLine("\nOpção inválida!");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"\nErro: {ex.Message}");
                    Console.ResetColor();
                }

                if (opcao != "0")
                {
                    Console.WriteLine("\nPressione qualquer tecla para continuar...");
                    Console.ReadKey();
                }
            }
        }
        #endregion

        #region MENU CATEGORIAS
        static void MenuCategorias()
        {
            bool voltar = false;
            while (!voltar)
            {
                Console.Clear();
                Console.WriteLine("--- GESTÃO DE CATEGORIAS ---");
                Console.WriteLine("1. Adicionar Categoria");
                Console.WriteLine("2. Listar Categorias");
                Console.WriteLine("3. Remover Categoria");
                Console.WriteLine("0. Voltar ao Menu Principal");
                Console.Write("Escolha uma opção: ");

                string opcao = Console.ReadLine() ?? "";
                try
                {
                    switch (opcao)
                    {
                        case "1":
                            Console.Clear();
                            Console.WriteLine("--- Adicionar Nova Categoria ---");
                            Console.Write("Nome da Categoria: ");
                            string nome = Console.ReadLine() ?? "";
                            _categoriaService.AdicionarCategoria(nome);
                            Console.WriteLine("\nCategoria adicionada com sucesso!");
                            break;

                        case "2":
                            Console.Clear();
                            Console.WriteLine("--- Lista de Categorias ---");
                            var lista = _categoriaService.ListarTodas();
                            if (lista.Count == 0) Console.WriteLine("Nenhuma categoria registada.");
                            else
                            {
                                foreach (var c in lista) Console.WriteLine($"ID: {c.Id} | Nome: {c.Nome}");
                            }
                            break;

                        case "3":
                            Console.Clear();
                            Console.WriteLine("--- Remover Categoria ---");
                            Console.Write("ID da categoria a remover: ");
                            int id = int.Parse(Console.ReadLine() ?? "0");
                            _categoriaService.RemoverCategoria(id);
                            Console.WriteLine("\nCategoria removida com sucesso!");
                            break;

                        case "0":
                            voltar = true;
                            break;
                        default:
                            Console.WriteLine("\nOpção inválida!");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"\nErro: {ex.Message}");
                    Console.ResetColor();
                }

                if (opcao != "0")
                {
                    Console.WriteLine("\nPressione qualquer tecla para continuar...");
                    Console.ReadKey();
                }
            }
        }
        #endregion

        #region MENU REALIZADORES
        static void MenuRealizadores()
        {
            bool voltar = false;
            while (!voltar)
            {
                Console.Clear();
                Console.WriteLine("--- GESTÃO DE REALIZADORES ---");
                Console.WriteLine("1. Adicionar Realizador");
                Console.WriteLine("2. Listar Realizadores");
                Console.WriteLine("3. Remover Realizador");
                Console.WriteLine("0. Voltar ao Menu Principal");
                Console.Write("Escolha uma opção: ");

                string opcao = Console.ReadLine() ?? "";
                try
                {
                    switch (opcao)
                    {
                        case "1":
                            Console.Clear();
                            Console.WriteLine("--- Adicionar Novo Realizador ---");
                            Console.Write("Nome do Realizador: ");
                            string nome = Console.ReadLine() ?? "";
                            Console.Write("País de Origem: ");
                            string pais = Console.ReadLine() ?? "";

                            _realizadorService.AdicionarRealizador(nome, pais);
                            Console.WriteLine("\nRealizador adicionado com sucesso!");
                            break;

                        case "2":
                            Console.Clear();
                            Console.WriteLine("--- Lista de Realizadores ---");
                            var lista = _realizadorService.ListarTodos();
                            if (lista.Count == 0) Console.WriteLine("Nenhum realizador registado.");
                            else
                            {
                                foreach (var r in lista) Console.WriteLine($"ID: {r.Id} | Nome: {r.Nome} ({r.Pais})");
                            }
                            break;

                        case "3":
                            Console.Clear();
                            Console.WriteLine("--- Remover Realizador ---");
                            Console.Write("ID do realizador a remover: ");
                            int id = int.Parse(Console.ReadLine() ?? "0");
                            _realizadorService.RemoverRealizador(id);
                            Console.WriteLine("\nRealizador removido com sucesso!");
                            break;

                        case "0":
                            voltar = true;
                            break;
                        default:
                            Console.WriteLine("\nOpção inválida!");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"\nErro: {ex.Message}");
                    Console.ResetColor();
                }

                if (opcao != "0")
                {
                    Console.WriteLine("\nPressione qualquer tecla para continuar...");
                    Console.ReadKey();
                }
            }
        }
        #endregion
    }
}