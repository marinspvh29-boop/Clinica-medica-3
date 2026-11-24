using System;
using System.Collections.Generic;

class Paciente
{
    public int Codigo;
    public string Nome;
    public int Idade;
    public int Prioridade;

    public Paciente(int codigo, string nome, int idade, int prioridade)
    {
        Codigo = codigo;
        Nome = nome;
        Idade = idade;
        Prioridade = prioridade;
    }
}

class Program
{
    static List<Paciente> pacientes = new List<Paciente>();

    static void Main()
    {
        int opcao = 0;
        do
        {
            Console.WriteLine("\n=== CLINICA MEDICA ===");
            Console.WriteLine("1 - Cadastrar paciente");
            Console.WriteLine("2 - Listar pacientes");
            Console.WriteLine("3 - Buscar por codigo");
            Console.WriteLine("4 - Buscar por nome");
            Console.WriteLine("5 - Ordenar por prioridade");
            Console.WriteLine("6 - Ordenar por idade");
            Console.WriteLine("7 - Sair");
            Console.Write("Escolha: ");

            if (!int.TryParse(Console.ReadLine(), out opcao))
            {
                Console.WriteLine("Opcao invalida!");
                continue;
            }

            switch (opcao)
            {
                case 1: CadastrarPaciente(); break;
                case 2: ListarPacientes(); break;
                case 3: BuscarPorCodigo(); break;
                case 4: BuscarPorNome(); break;
                case 5: OrdenarPorPrioridade(); break;
                case 6: OrdenarPorIdade(); break;
                case 7: Console.WriteLine("Sistema encerrado!"); break;
                default: Console.WriteLine("Opcao invalida!"); break;
            }
        } while (opcao != 7);
    }

    static void CadastrarPaciente()
    {
        Console.Write("Codigo: ");
        if (!int.TryParse(Console.ReadLine(), out int codigo) || codigo <= 0)
        {
            Console.WriteLine("Codigo invalido!");
            return;
        }

        foreach (Paciente p in pacientes)
        {
            if (p.Codigo == codigo)
            {
                Console.WriteLine("Codigo ja cadastrado!");
                return;
            }
        }

        Console.Write("Nome: ");
        string nome = Console.ReadLine() ?? "";
        if (string.IsNullOrWhiteSpace(nome))
        {
            Console.WriteLine("Nome invalido!");
            return;
        }

        Console.Write("Idade: ");
        if (!int.TryParse(Console.ReadLine(), out int idade) || idade < 0 || idade > 120)
        {
            Console.WriteLine("Idade invalida!");
            return; // <-- Movido para dentro do if
        }

        Console.WriteLine("Prioridade:");
        Console.WriteLine("1 - Normal");
        Console.WriteLine("2 - Moderada");
        Console.WriteLine("3 - Alta");
        Console.Write("Escolha: ");
        if (!int.TryParse(Console.ReadLine(), out int prioridade) || prioridade < 1 || prioridade > 3)
        {
            Console.WriteLine("Prioridade invalida!");
            return;
        }

        pacientes.Add(new Paciente(codigo, nome.Trim(), idade, prioridade));
        Console.WriteLine("Paciente cadastrado com sucesso!");
    }

    static void ListarPacientes()
    {
        if (pacientes.Count == 0)
        {
            Console.WriteLine("Nenhum paciente cadastrado.");
            return;
        }

        foreach (Paciente p in pacientes)
            MostrarPaciente(p);
    }

    static void MostrarPaciente(Paciente p)
    {
        Console.WriteLine($"Codigo: {p.Codigo} | Nome: {p.Nome} | Idade: {p.Idade} | Prioridade: {p.Prioridade}");
    }

    static void BuscarPorCodigo()
    {
        Console.Write("Digite o codigo: ");
        if (!int.TryParse(Console.ReadLine(), out int codigo))
        {
            Console.WriteLine("Codigo invalido!");
            return;
        }

        foreach (Paciente p in pacientes)
        {
            if (p.Codigo == codigo)
            {
                Console.WriteLine("Paciente encontrado!");
                MostrarPaciente(p);
                return;
            }
        }
        Console.WriteLine("Paciente nao encontrado.");
    }

    static void BuscarPorNome()
    {
        Console.Write("Digite o nome: ");
        string nome = Console.ReadLine() ?? "";
        if (string.IsNullOrWhiteSpace(nome))
        {
            Console.WriteLine("Nome invalido!");
            return;
        }

        bool encontrado = false;
        foreach (Paciente p in pacientes)
        {
            if (p.Nome.Contains(nome.Trim(), StringComparison.OrdinalIgnoreCase))
            {
                MostrarPaciente(p);
                encontrado = true;
            }
        }

        if (!encontrado)
            Console.WriteLine("Paciente nao encontrado.");
    }

    static void OrdenarPorPrioridade()
    {
        for (int i = 0; i < pacientes.Count - 1; i++)
        {
            int maior = i;
            for (int j = i + 1; j < pacientes.Count; j++)
            {
                if (pacientes[j].Prioridade > pacientes[maior].Prioridade)
                    maior = j;
            }
            Paciente temp = pacientes[i];
            pacientes[i] = pacientes[maior];
            pacientes[maior] = temp; // <-- Movido para dentro do for
        }
        Console.WriteLine("Ordenado por prioridade!");
        ListarPacientes();
    } // <-- Chave de fechamento ajustada

    static void OrdenarPorIdade()
    {
        for (int i = 0; i < pacientes.Count - 1; i++)
        {
            int menor = i;
            for (int j = i + 1; j < pacientes.Count; j++)
            {
                if (pacientes[j].Idade < pacientes[menor].Idade)
                    menor = j;
            }
            Paciente temp = pacientes[i];
            pacientes[i] = pacientes[menor];
            pacientes[menor] = temp;
        }
        Console.WriteLine("Ordenado por idade!");
        ListarPacientes();
    }
}
