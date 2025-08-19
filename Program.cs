using System;
using System.Linq;

namespace MatrizQuadrada
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== PROGRAMA AVANÇADO DE MATRIZ QUADRADA NxN ===");
            Console.WriteLine("=== CÁLCULO DE DIAGONAIS E OPERAÇÕES AVANÇADAS ===\n");

            bool continuar = true;

            while (continuar)
            {
                Console.Clear();
                ExibirMenuPrincipal();

                var opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        ProcessarMatrizManual();
                        break;
                    case "2":
                        ProcessarMatrizAleatoria();
                        break;
                    case "3":
                        TestarMatrizesPredefinidas();
                        break;
                    case "4":
                        Console.WriteLine("Obrigado por usar o programa!");
                        continuar = false;
                        break;
                    default:
                        Console.WriteLine("Opção inválida! Pressione qualquer tecla para continuar...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        static void ExibirMenuPrincipal()
        {
            Console.WriteLine("╔══════════════════════════════════════╗");
            Console.WriteLine("║          MENU PRINCIPAL             ║");
            Console.WriteLine("╠══════════════════════════════════════╣");
            Console.WriteLine("║ 1. Matriz Manual                    ║");
            Console.WriteLine("║ 2. Matriz Aleatória                 ║");
            Console.WriteLine("║ 3. Matrizes Predefinidas (Testes)   ║");
            Console.WriteLine("║ 4. Sair                             ║");
            Console.WriteLine("╚══════════════════════════════════════╝");
            Console.Write("\nEscolha uma opção: ");
        }

        static void ProcessarMatrizManual()
        {
            int n = SolicitarTamanhoMatriz();
            int[,] matriz = CriarEPreencherMatrizManual(n);
            ProcessarMatriz(matriz, "MATRIZ MANUAL");
            AguardarContinuacao();
        }

        static void ProcessarMatrizAleatoria()
        {
            int n = SolicitarTamanhoMatriz();
            int min = SolicitarValor("Digite o valor mínimo para números aleatórios: ", -1000, 1000);
            int max = SolicitarValor("Digite o valor máximo para números aleatórios: ", min + 1, 10000);

            int[,] matriz = GerarMatrizAleatoria(n, min, max);
            ProcessarMatriz(matriz, "MATRIZ ALEATÓRIA");
            AguardarContinuacao();
        }

        static void TestarMatrizesPredefinidas()
        {
            Console.WriteLine("\n=== MATRIZES PREDEFINIDAS PARA TESTE ===");

            // Matriz 2x2
            int[,] matriz2x2 = { { 1, 2 }, { 3, 4 } };
            ProcessarMatriz(matriz2x2, "MATRIZ 2x2 PREDEFINIDA");

            // Matriz 3x3
            int[,] matriz3x3 = { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };
            ProcessarMatriz(matriz3x3, "MATRIZ 3x3 PREDEFINIDA");

            // Matriz 4x4
            int[,] matriz4x4 = { { 1, 0, 0, 0 }, { 0, 2, 0, 0 }, { 0, 0, 3, 0 }, { 0, 0, 0, 4 } };
            ProcessarMatriz(matriz4x4, "MATRIZ DIAGONAL 4x4");

            AguardarContinuacao();
        }

        static int SolicitarValor(string mensagem, int min, int max)
        {
            int valor;
            while (true)
            {
                Console.Write(mensagem);
                if (int.TryParse(Console.ReadLine(), out valor) && valor >= min && valor <= max)
                {
                    return valor;
                }
                Console.WriteLine($"Valor inválido! Digite um número entre {min} e {max}.");
            }
        }

        static int[,] GerarMatrizAleatoria(int n, int min, int max)
        {
            int[,] matriz = new int[n, n];
            Random random = new Random();

            Console.WriteLine($"\nGerando matriz {n}x{n} com valores entre {min} e {max}...");

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    matriz[i, j] = random.Next(min, max + 1);
                }
            }

            return matriz;
        }

        static void ProcessarMatriz(int[,] matriz, string titulo)
        {
            Console.WriteLine($"\n=== {titulo} ===");
            ExibirMatriz(matriz);

            var resultado = CalcularDiagonais(matriz);
            ExibirResultadosDiagonais(resultado);

            ExibirInformacoesAdicionais(matriz);
        }

        static (int somaPrincipal, int somaSecundaria, int[] elementosPrincipal, int[] elementosSecundaria)
            CalcularDiagonais(int[,] matriz)
        {
            int n = matriz.GetLength(0);
            int somaPrincipal = 0;
            int somaSecundaria = 0;
            int[] elementosPrincipal = new int[n];
            int[] elementosSecundaria = new int[n];

            for (int i = 0; i < n; i++)
            {
                somaPrincipal += matriz[i, i];
                elementosPrincipal[i] = matriz[i, i];

                somaSecundaria += matriz[i, n - 1 - i];
                elementosSecundaria[i] = matriz[i, n - 1 - i];
            }

            return (somaPrincipal, somaSecundaria, elementosPrincipal, elementosSecundaria);
        }

        static void ExibirResultadosDiagonais(
            (int somaPrincipal, int somaSecundaria, int[] elementosPrincipal, int[] elementosSecundaria) resultado)
        {
            Console.WriteLine("\n=== RESULTADOS DAS DIAGONAIS ===");

            Console.Write("Elementos da Diagonal Principal: ");
            ExibirArrayColorido(resultado.elementosPrincipal, ConsoleColor.Green);
            Console.WriteLine($"Soma da Diagonal Principal: {resultado.somaPrincipal}");

            Console.Write("Elementos da Diagonal Secundária: ");
            ExibirArrayColorido(resultado.elementosSecundaria, ConsoleColor.Blue);
            Console.WriteLine($"Soma da Diagonal Secundária: {resultado.somaSecundaria}");

            Console.WriteLine($"Diferença entre diagonais: {Math.Abs(resultado.somaPrincipal - resultado.somaSecundaria)}");

            if (resultado.somaPrincipal == resultado.somaSecundaria)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("★ As diagonais têm a mesma soma! ★");
                Console.ResetColor();
            }
        }

        static void ExibirInformacoesAdicionais(int[,] matriz)
        {
            int n = matriz.GetLength(0);

            Console.WriteLine("\n=== INFORMAÇÕES ADICIONAIS ===");
            Console.WriteLine($"Tamanho da matriz: {n}x{n}");
            Console.WriteLine($"Total de elementos: {n * n}");

            int somaTotal = 0;
            int min = int.MaxValue;
            int max = int.MinValue;

            foreach (int elemento in matriz)
            {
                somaTotal += elemento;
                if (elemento < min) min = elemento;
                if (elemento > max) max = elemento;
            }

            Console.WriteLine($"Soma total de todos elementos: {somaTotal}");
            Console.WriteLine($"Valor mínimo: {min}");
            Console.WriteLine($"Valor máximo: {max}");
            Console.WriteLine($"Média dos valores: {(double)somaTotal / (n * n):F2}");

            // Verificar se é uma matriz identidade
            if (EMatrizIdentidade(matriz))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("✓ Esta é uma matriz identidade!");
                Console.ResetColor();
            }
        }

        static bool EMatrizIdentidade(int[,] matriz)
        {
            int n = matriz.GetLength(0);

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (i == j)
                    {
                        if (matriz[i, j] != 1) return false;
                    }
                    else
                    {
                        if (matriz[i, j] != 0) return false;
                    }
                }
            }

            return true;
        }

        static void ExibirArrayColorido(int[] array, ConsoleColor cor)
        {
            Console.Write("[");
            Console.ForegroundColor = cor;
            for (int i = 0; i < array.Length; i++)
            {
                Console.Write(array[i]);
                if (i < array.Length - 1)
                    Console.Write(", ");
            }
            Console.ResetColor();
            Console.WriteLine("]");
        }

        static void AguardarContinuacao()
        {
            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
        }

        // Métodos mantidos da versão anterior (com pequenas melhorias)
        static int SolicitarTamanhoMatriz()
        {
            return SolicitarValor("Digite o tamanho da matriz quadrada (N): ", 1, 10);
        }

        static int[,] CriarEPreencherMatrizManual(int n)
        {
            int[,] matriz = new int[n, n];

            Console.WriteLine($"\nDigite os elementos da matriz {n}x{n}:");

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    matriz[i, j] = SolicitarValor($"Elemento [{i},{j}]: ", -10000, 10000);
                }
            }

            return matriz;
        }

        static void ExibirMatriz(int[,] matriz)
        {
            int n = matriz.GetLength(0);

            // Calcular a largura máxima necessária para formatação
            int maxWidth = matriz.Cast<int>().Max().ToString().Length;
            int minWidth = matriz.Cast<int>().Min().ToString().Length;
            int cellWidth = Math.Max(maxWidth, minWidth) + 2;

            Console.WriteLine("\n" + new string('═', n * (cellWidth + 1) + 1));

            for (int i = 0; i < n; i++)
            {
                Console.Write("║");
                for (int j = 0; j < n; j++)
                {
                    // Colorir as diagonais
                    if (i == j) Console.ForegroundColor = ConsoleColor.Green;
                    else if (i + j == n - 1) Console.ForegroundColor = ConsoleColor.Blue;

                    Console.Write(matriz[i, j].ToString().PadLeft(cellWidth));
                    Console.ResetColor();
                }
                Console.WriteLine(" ║");
            }
        }
    }
}