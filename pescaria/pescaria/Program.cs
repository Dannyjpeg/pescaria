using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        Random rand = new Random();

        Console.Write("Quantos peixes existem no lago? ");
        int nPeixes = int.Parse(Console.ReadLine());

        Console.Write("Quantos jogadores vão jogar? ");
        int nJogadores = int.Parse(Console.ReadLine());

        List<string> jogadores = new List<string>();
        List<int> tentativas = new List<int>();
        for (int i = 0; i < nJogadores; i++)
        {
            Console.Write($"Nome do jogador {i + 1}: ");
            jogadores.Add(Console.ReadLine());

            Console.Write($"Quantas iscas/tentativas o jogador {jogadores[i]} tem? ");
            tentativas.Add(int.Parse(Console.ReadLine()));
        }

        HashSet<int> peixesPosicoes = new HashSet<int>();
        Dictionary<int, (string, double)> peixes = new Dictionary<int, (string, double)>();

        while (peixesPosicoes.Count < nPeixes)
        {
            int pos = rand.Next(50);
            if (!peixesPosicoes.Contains(pos))
            {
                peixesPosicoes.Add(pos);
                string tipo = EscolherTipoPeixe(rand);
                double peso = GerarPesoPeixe(tipo, rand);
                peixes[pos] = (tipo, peso);
            }
        }

        Dictionary<string, double> pesoTotalPescado = new Dictionary<string, double>();
        Dictionary<string, int> quantidadePeixesPescados = new Dictionary<string, int>();

        for (int i = 0; i < nJogadores; i++)
        {
            pesoTotalPescado[jogadores[i]] = 0;
            quantidadePeixesPescados[jogadores[i]] = 0;

            Console.WriteLine($"\nJogador {jogadores[i]} começa a pescar!");
            for (int j = 0; j < tentativas[i]; j++)
            {
                Console.Write($"Tentativa {j + 1}: Escolha uma posição de 0 a 49 para lançar a isca: ");
                int tentativa = int.Parse(Console.ReadLine());

                if (peixesPosicoes.Contains(tentativa))
                {
                    var (tipo, peso) = peixes[tentativa];
                    Console.WriteLine($"Você pescou uma {tipo} de {peso}kg na posição {tentativa}!");
                    peixesPosicoes.Remove(tentativa);
                    pesoTotalPescado[jogadores[i]] += peso;
                    quantidadePeixesPescados[jogadores[i]]++;
                }
                else
                {
                    Console.WriteLine("Não pegou nenhum peixe nessa tentativa.");
                }
            }
        }

        double maiorPeso = 0;
        int maiorQuantidade = 0;

        foreach (var jogador in pesoTotalPescado)
        {
            if (jogador.Value > maiorPeso)
            {
                maiorPeso = jogador.Value;
            }
        }

        foreach (var jogador in quantidadePeixesPescados)
        {
            if (jogador.Value > maiorQuantidade)
            {
                maiorQuantidade = jogador.Value;
            }
        }

        var vencedoresPorPeixe = new List<string>();
        var vencedoresPorPeso = new List<string>();

        foreach (var jogador in quantidadePeixesPescados)
        {
            if (jogador.Value == maiorQuantidade)
            {
                vencedoresPorPeixe.Add(jogador.Key);
            }
        }

        foreach (var jogador in pesoTotalPescado)
        {
            if (jogador.Value == maiorPeso)
            {
                vencedoresPorPeso.Add(jogador.Key);
            }
        }

        if (vencedoresPorPeso.Count == 1)
        {
            Console.WriteLine($"\nO vencedor por peso é {vencedoresPorPeso[0]} com {maiorPeso}kg de peixe(s)!");
        }
        else
        {
            if (vencedoresPorPeixe.Count == 1)
            {
                Console.WriteLine($"\nO vencedor por quantidade de peixes é {vencedoresPorPeixe[0]} com {maiorQuantidade} peixe(s)!");
            }
            else
            {
                Console.WriteLine("\nDeu empate! Ninguém pescou mais peso ou mais peixes do que os outros.");
            }
        }
    }

    static string EscolherTipoPeixe(Random rand)
    {
        int tipo = rand.Next(3);
        switch (tipo)
        {
            case 0: return "Tilápia";
            case 1: return "Pacu";
            case 2: return "Tambaqui";
            default: return "Tilápia";
        }
    }

    static double GerarPesoPeixe(string tipo, Random rand)
    {
        switch (tipo)
        {
            case "Tilápia": return rand.NextDouble() * (3 - 1) + 1;
            case "Pacu": return rand.NextDouble() * (5 - 2) + 2;
            case "Tambaqui": return rand.NextDouble() * (6 - 3) + 3;
            default: return 0;
        }
    }
}
