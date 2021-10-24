using System;

namespace Net.Core.ConsoleSeries
{
    class Program
    {
        static RepositorioSerie repositorio = new RepositorioSerie();
        static void Main(string[] args)
        {
            string opcaoUsuario = ObterOpcaoUsuario();

			while (opcaoUsuario.ToUpper() != "X")
			{
				switch (opcaoUsuario)
				{
					case "1":
						Console.Clear();
						ListarSeries();
						break;
					case "2":
						Console.Clear();
						InserirSerie();
						break;
					case "3":
						Console.Clear();
						AtualizarSerie();
						break;
					case "4":
						Console.Clear();
						ExcluirSerie();
						break;
					case "5":
						Console.Clear();
						VisualizarSerie();
						break;
					default:
						throw new ArgumentOutOfRangeException();
				}

				opcaoUsuario = ObterOpcaoUsuario();
			}

			System.Console.WriteLine("Obrigado por utilizar nossos serviços.");
			Console.ReadLine();
        }

        private static void ExcluirSerie()
		{
			Console.Write("Digite o id da série: ");
			int indiceSerie = int.Parse(Console.ReadLine());

			repositorio.Exclui(indiceSerie);
		}

        private static void VisualizarSerie()
		{
			Console.Write("Digite o id da série: ");
			int indiceSerie = int.Parse(Console.ReadLine());

			var serie = repositorio.RetornaPorId(indiceSerie);

			Console.WriteLine(serie);
		}

        private static void AtualizarSerie()
		{
			Console.Write("Digite o id da série: ");
			int indiceSerie = int.Parse(Console.ReadLine());

			// https://docs.microsoft.com/pt-br/dotnet/api/system.enum.getvalues?view=netcore-3.1
			// https://docs.microsoft.com/pt-br/dotnet/api/system.enum.getname?view=netcore-3.1
			foreach (int i in Enum.GetValues(typeof(Genero)))
			{
				Console.WriteLine("{0}-{1}", i, Enum.GetName(typeof(Genero), i));
			}
			Console.Write("Digite o gênero entre as opções acima: ");
			int entradaGenero = int.Parse(Console.ReadLine());

			Console.Write("Digite o Título da Série: ");
			string entradaTitulo = Console.ReadLine();

			Console.Write("Digite o Ano de Início da Série: ");
			int entradaAno = int.Parse(Console.ReadLine());

			Console.Write("Digite a Descrição da Série: ");
			string entradaDescricao = Console.ReadLine();

			Serie atualizaSerie = new Serie(id: indiceSerie,
										genero: (Genero)entradaGenero,
										titulo: entradaTitulo,
										ano: entradaAno,
										descricao: entradaDescricao);

			repositorio.Atualiza(indiceSerie, atualizaSerie);
		}
        private static void ListarSeries()
		{
			Console.WriteLine("Lista de séries: \n");

			var lista = repositorio.Lista();

			if (lista.Count == 0)
			{
                Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("Nenhuma série cadastrada.");
                Console.ResetColor();
				return;
			}

			foreach (var serie in lista)
			{
                string excluido = "";
                if(serie.retornaExcluido()){
                    Console.ForegroundColor = ConsoleColor.Red;
                    excluido = "*EXCLUIDO*";
                }
				Console.WriteLine("#ID {0}: - {1} {2}", serie.retornaId(), serie.retornaTitulo(), excluido);
                Console.ResetColor();
                excluido = "";
			}
		}

        private static void InserirSerie()
		{
            Console.ForegroundColor = ConsoleColor.Green;

			Console.WriteLine("Inserir nova série");

			foreach (int i in Enum.GetValues(typeof(Genero)))
			{
				Console.WriteLine("{0}-{1}", i, Enum.GetName(typeof(Genero), i));
			}
			Console.Write("Digite o gênero entre as opções acima: ");
			int entradaGenero = int.Parse(Console.ReadLine());

			Console.Write("Digite o Título da Série: ");
			string entradaTitulo = Console.ReadLine();

			Console.Write("Digite o Ano de Início da Série: ");
			int entradaAno = int.Parse(Console.ReadLine());

			Console.Write("Digite a Descrição da Série: ");
			string entradaDescricao = Console.ReadLine();

			Serie novaSerie = new Serie(id: repositorio.ProximoId(),
										genero: (Genero)entradaGenero,
										titulo: entradaTitulo,
										ano: entradaAno,
										descricao: entradaDescricao);
            Console.ResetColor();
			Console.Clear();
			repositorio.Insere(novaSerie);
		}

        private static string ObterOpcaoUsuario()
		{
            
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            // Console.ForegroundColor = ConsoleColor.Black;
			Console.WriteLine();
			Console.WriteLine("Walker Séries a seu dispor!!!");
			Console.WriteLine("Informe a opção desejada:");

			Console.WriteLine("1- Listar séries");
			Console.WriteLine("2- Inserir nova série");
			Console.WriteLine("3- Atualizar série");
			Console.WriteLine("4- Excluir série");
			Console.WriteLine("5- Visualizar série");
			Console.WriteLine("X- Sair");
            Console.ResetColor();
			Console.WriteLine();

			string opcaoUsuario = Console.ReadLine();
			Console.WriteLine();
			return opcaoUsuario;
		
        }
    }
}
