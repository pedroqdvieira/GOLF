using System.ComponentModel;
using System.Diagnostics;
System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
const double GRAVIDADE = 9.80668;

double velocidadeBola = 10;
double velocidadeArremesso = 0;
double angulo = 0;
double distancia;
double moedas = 0;
double totalMoedas = 0;
double banco = 0;
int bancoContador = 0;
bool podeMultiplicar = false;
int opcao;
double elasticidade = 0;
int quiques = 0;
double valorUpgradeVelocidade = 10;
double valorUpgradeArremesso = 10;
double valorUpgradeElasticidade = 10;
double nivelUpgradeVelocidade = 1;
double nivelUpgradeArremesso = 1;
double nivelUpgradeElasticidade = 1;

Random random = new Random();

do
{

    System.Console.WriteLine("\n--- MENU ---");
    System.Console.WriteLine("1 - Lançamento");
    System.Console.WriteLine("2 - Upgrades");
    System.Console.WriteLine("0 - Sair");
    opcao = LerInteiro("Selecione uma opção: ");
    Console.Clear();

    switch (opcao)
    {
        case 1:
            //lançamento
            System.Console.WriteLine("Pressione Enter para definir o ângulo...");
            var stopwatch = Stopwatch.StartNew();
            double velocidadeAnimacao = 2.0; // Controla a velocidade da barra

            while (!Console.KeyAvailable || Console.ReadKey(true).Key != ConsoleKey.Enter)
            {
                // Calcula um valor oscilante suave entre -1 e 1 usando o tempo
                double tempo = stopwatch.Elapsed.TotalSeconds * velocidadeAnimacao;
                double seno = Math.Sin(tempo);

                // Mapeia o valor do seno (de -1 a 1) para o intervalo do ângulo (de 0 a 1)
                angulo = (seno + 1.0) / 2.0;

                Console.SetCursorPosition(0, Console.CursorTop);
                int barWidth = 30;
                int filledWidth = (int)(angulo * barWidth);
                string bar = "[" + new string('█', filledWidth) + new string(' ', barWidth - filledWidth) + "]";
                Console.Write($"Ângulo: {bar} {angulo:F2}");

                System.Threading.Thread.Sleep(16); // Atualiza a tela ~60 vezes por segundo
            }
            stopwatch.Stop();
            Console.WriteLine(); // Pula para a próxima linha após definir o ângulo

            double realAngulo = angulo * 90;
            double anguloRadianos = realAngulo * (Math.PI / 180.0);

            distancia = (Math.Pow(velocidadeBola + velocidadeArremesso, 2) * Math.Sin(2 * anguloRadianos)) / GRAVIDADE;


            double velocidadeAtualParaQuique = velocidadeBola + velocidadeArremesso;
            for (int i = 1; i <= quiques; i++)
            {
                velocidadeAtualParaQuique *= elasticidade;
                System.Console.WriteLine($"Quicou {i} vezes.");
                distancia += (Math.Pow(velocidadeAtualParaQuique, 2) * Math.Sin(2 * anguloRadianos)) / GRAVIDADE;
            }

            if (distancia < 1)
            {
                System.Console.WriteLine(distancia.ToString("F3") + " Metros!");
            }
            else if (distancia < 10000)
            {
                System.Console.WriteLine(distancia.ToString("F2") + " Metros!");
            }
            else //if (distancia < 1000000)
            {
                System.Console.WriteLine((distancia / 1000).ToString("F1") + "K Metros!");
            }

            moedas = distancia - distancia * 0.1;
            banco = distancia * 0.1;
            bancoContador++;
            System.Console.WriteLine($"Você ganhou {moedas.ToString("C")} moedas");

            if (bancoContador == 10)
            {
                podeMultiplicar = true;
            }

            if (podeMultiplicar == true)
            {
                if (LerInteiro("Deseja tentar multiplicar(1 para sim. 0 para não)? ") == 1)
                {
                    int porcentagem = random.Next(1, 101);
                    if (porcentagem <= 65)
                    {
                        double moedasMult = moedas * 10;
                        moedas = moedasMult;
                        System.Console.WriteLine($"Você conseguiu {moedas.ToString("C")} x 10 = {moedasMult.ToString("C")} Moedas!!!");
                    }
                    else if (porcentagem <= 95)
                    {
                        double moedasMult = moedas * 20;
                        moedas = moedasMult;
                        System.Console.WriteLine($"Você conseguiu {moedas.ToString("C")} x 20 = {moedasMult.ToString("C")} Moedas!!!");
                    }
                    else if (porcentagem <= 99)
                    {
                        double moedasMult = moedas * 30;
                        moedas = moedasMult;
                        System.Console.WriteLine($"Você conseguiu {moedas.ToString("C")} x 30 = {moedasMult.ToString("C")} Moedas!!!");
                    }
                    else if (porcentagem == 100)
                    {
                        double moedasMult = moedas * 100;
                        moedas = moedasMult;
                        System.Console.WriteLine($"Você conseguiu {moedas.ToString("C")} x 100 = {moedasMult.ToString("C")} Moedas!!!");
                    }
                    bancoContador = 0;
                    podeMultiplicar = false;
                }
            }

            totalMoedas += moedas;
            break;
        case 2:
            int upgradeOpcao;
            do
            {
                System.Console.WriteLine("\n--- UPGRADES ---");
                System.Console.WriteLine($"1 - Velocidade da bola - Nível {nivelUpgradeVelocidade} - Valor: {valorUpgradeVelocidade.ToString("C")}");
                System.Console.WriteLine($"2 - Força do arremesso - Nível {nivelUpgradeArremesso} - Valor: {valorUpgradeArremesso.ToString("C")}");
                System.Console.WriteLine($"3 - Elasticidade da bola - Nível {nivelUpgradeElasticidade} - Valor: {valorUpgradeElasticidade.ToString("C")}");
                System.Console.WriteLine("0 - Voltar");
                System.Console.WriteLine($"###Total de moedas: {totalMoedas.ToString("C")}###");
                upgradeOpcao = LerInteiro("Selecione uma opção: ");

                switch (upgradeOpcao)
                {
                    case 1:
                        if (totalMoedas >= valorUpgradeVelocidade)
                        {
                            if (LerInteiro("Deseja comprar este upgrade? (1 para sim, 0 para não): ") == 1)
                            {
                                totalMoedas -= valorUpgradeVelocidade;
                                velocidadeBola *= 1.10; // Aumenta a velocidade
                                nivelUpgradeVelocidade++;
                                valorUpgradeVelocidade = 10 * Math.Pow(1.25, nivelUpgradeVelocidade - 1); // Aumenta o custo para o próximo nível
                                System.Console.WriteLine("Upgrade de velocidade comprado com sucesso!");
                            }
                        }
                        else
                        {
                            System.Console.WriteLine("Moedas insuficientes para comprar este upgrade.");
                        }
                        break;
                    case 2:
                        if (totalMoedas >= valorUpgradeArremesso)
                        {
                            if (LerInteiro("Deseja comprar este upgrade? (1 para sim, 0 para não): ") == 1)
                            {
                                totalMoedas -= valorUpgradeArremesso;
                                velocidadeArremesso += velocidadeBola * 0.05; // Aumenta a velocidade
                                nivelUpgradeArremesso++;
                                valorUpgradeArremesso = 10 * Math.Pow(1.25, nivelUpgradeArremesso - 1); // Aumenta o custo para o próximo nível
                                System.Console.WriteLine("Upgrade de Arremesso comprado com sucesso!");
                            }
                        }
                        else
                        {
                            System.Console.WriteLine("Moedas insuficientes para comprar este upgrade.");
                        }
                        break;
                    case 3:
                        if (totalMoedas >= valorUpgradeElasticidade)
                        {
                            if (LerInteiro("Deseja comprar este upgrade? (1 para sim, 0 para não): ") == 1)
                            {
                                totalMoedas -= valorUpgradeElasticidade;
                                elasticidade += 0.05;
                                quiques++;
                                nivelUpgradeElasticidade++;
                                valorUpgradeElasticidade = 10 * Math.Pow(1.25, nivelUpgradeElasticidade - 1); // Aumenta o custo para o próximo nível
                                System.Console.WriteLine("Upgrade de elasticidade comprado com sucesso!");
                            }
                        }
                        else
                        {
                            System.Console.WriteLine("Moedas insuficientes para comprar este upgrade.");
                        }
                        break;
                    case 0:
                        break;
                    default:
                        System.Console.WriteLine("Opção inválida.");
                        break;
                }
            } while (upgradeOpcao != 0);

            break;
        case 0:
            System.Console.WriteLine("Saindo do programa...");
            break;
        default:
            System.Console.WriteLine("Opção inválida. Tente novamente.");
            break;
    }
} while (opcao != 0);

static int LerInteiro(string prompt)
{
    int valor;
    while (true)
    {
        System.Console.Write(prompt);
        string? input = Console.ReadLine();
        if (int.TryParse(input, out valor))
        {
            return valor;
        }
        System.Console.WriteLine("Entrada inválida. Por favor, digite um número inteiro.");
    }
}

static double LerDouble(string prompt)
{
    double valor;
    while (true)
    {
        System.Console.Write(prompt);
        string? input = Console.ReadLine();
        if (double.TryParse(input, out valor))
        {
            return valor;
        }
        System.Console.WriteLine("Entrada inválida. Por favor, digite um número.");
    }
}