using System;
using System.Collections.Generic;

public class Livro
{
    public string Titulo { get; set; }
    public int Quantidade { get; set; }

    public Livro(string titulo, int quantidade)
    {
        Titulo = titulo;
        Quantidade = quantidade;
    }
}

public class Program
{
    static List<Livro> livros = new List<Livro>();
    static Dictionary<string, int> usuarios = new Dictionary<string, int>();

    public static void Main()
    {
        
        Console.WriteLine("Nome do usuário:");
        string usuario = Console.ReadLine();

        while (true)
        {
            Console.WriteLine("\n1. Cadastrar Livro");
            Console.WriteLine("2. Emprestar Livro");
            Console.WriteLine("3. Devolver Livro");
            Console.WriteLine("4. Sair");
            string opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1":
                    CadastrarLivro();
                    break;
                case "2":
                    EmprestarLivro(usuario);
                    break;
                case "3":
                    DevolverLivro(usuario);
                    break;
                case "4":
                    return;
                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }
        }
    }

    static void CadastrarLivro()
    {
        Console.Write("Título do livro: ");
        string titulo = Console.ReadLine();
        Console.Write("Quantidade (máx. 3): ");
        int quantidade = int.Parse(Console.ReadLine());

    
        if (quantidade > 3)
        {
            Console.WriteLine("Erro: A quantidade não pode ser maior que 3.");
            return;
        }

        livros.Add(new Livro(titulo, quantidade));
        Console.WriteLine("Livro cadastrado.");
    }

    static void EmprestarLivro(string usuario)
    {
        if (usuarios.GetValueOrDefault(usuario, 0) >= 3)
        {
            Console.WriteLine("Erro: Você já possui 3 livros emprestados.");
            return;
        }

        Console.Write("Título do livro para emprestar: ");
        string titulo = Console.ReadLine();
        var livro = livros.Find(l => l.Titulo.Equals(titulo, StringComparison.OrdinalIgnoreCase));

        if (livro != null && livro.Quantidade > 0)
        {
            livro.Quantidade--;
            usuarios[usuario] = usuarios.GetValueOrDefault(usuario, 0) + 1;
            Console.WriteLine("Livro emprestado.");
        }
        else
        {
            Console.WriteLine("Livro não disponível.");
        }
    }

    static void DevolverLivro(string usuario)
    {
        if (!usuarios.ContainsKey(usuario) || usuarios[usuario] <= 0)
        {
            Console.WriteLine("Nenhum livro emprestado.");
            return;
        }

        Console.Write("Título do livro para devolver: ");
        string titulo = Console.ReadLine();
        var livro = livros.Find(l => l.Titulo.Equals(titulo, StringComparison.OrdinalIgnoreCase));

        if (livro != null)
        {
            livro.Quantidade++;
            usuarios[usuario]--;
            Console.WriteLine("Livro devolvido.");
        }
        else
        {
            Console.WriteLine("Livro não encontrado.");
        }
       
}

}
