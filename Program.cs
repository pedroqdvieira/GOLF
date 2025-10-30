using System.ComponentModel;
System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
const double GRAVIDADE = 9.80668;

double velocidadeBola = 10;
double velocidadeArremesso = 0;
double angulo;
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
    System.Console.Write("Selecione uma opção: ");
    opcao = Convert.ToInt32(Console.ReadLine());
    Console.Clear();

    switch (opcao)
    {
        case 1:
            //lançamento
            do
            {
                System.Console.Write("informe o ângulo(entre 0 e 1): ");
                angulo = Convert.ToDouble(Console.ReadLine());

                if (angulo > 1 || angulo < 0)
                    System.Console.WriteLine("Valor inválido");
            } while (angulo > 1 || angulo < 0);

            double realAngulo = angulo * 90;
            double anguloRadianos = realAngulo * (Math.PI / 180.0);

            distancia = (Math.Pow(velocidadeBola + velocidadeArremesso, 2) * Math.Sin(2 * anguloRadianos)) / GRAVIDADE;


            for (int i = 1; i <= quiques; i++)
            {
                System.Console.WriteLine($"Quicou {i} vezes.");
                distancia += (Math.Pow((velocidadeBola + velocidadeArremesso) * elasticidade, 2) * Math.Sin(2 * anguloRadianos)) / GRAVIDADE;
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
                System.Console.WriteLine("Deseja tentar multiplicar(1 para sim. 0 para não)? ");
                if (Convert.ToInt32(Console.ReadLine()) == 1)
                {
                    int porcentagem = random.Next(1, 101);
                    if (porcentagem <= 65)
                    {
                        double moedasMult = moedas * 10;
                        System.Console.WriteLine($"Você conseguiu {moedas.ToString("C")} x 10 = {moedasMult.ToString("C")} Moedas!!!");
                    }
                    else if (porcentagem <= 95)
                    {
                        double moedasMult = moedas * 20;
                        System.Console.WriteLine($"Você conseguiu {moedas.ToString("C")} x 20 = {moedasMult.ToString("C")} Moedas!!!");
                    }
                    else if (porcentagem <= 99)
                    {
                        double moedasMult = moedas * 30;
                        System.Console.WriteLine($"Você conseguiu {moedas.ToString("C")} x 30 = {moedasMult.ToString("C")} Moedas!!!");
                    }
                    else if (porcentagem == 100)
                    {
                        double moedasMult = moedas * 100;
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
                System.Console.WriteLine($"3 - Elasticidade da bola - Nível {nivelUpgradeArremesso} - Valor: {valorUpgradeArremesso.ToString("C")}");
                System.Console.WriteLine("0 - Voltar");
                System.Console.WriteLine($"###Total de moedas: {totalMoedas.ToString("C")}###");
                System.Console.Write("Selecione uma opção: ");
                upgradeOpcao = Convert.ToInt32(Console.ReadLine());

                switch (upgradeOpcao)
                {
                    case 1:
                        if (totalMoedas >= valorUpgradeArremesso)
                        {
                            System.Console.Write("Deseja comprar este upgrade? (1 para sim, 0 para não): ");
                            if (Convert.ToInt32(Console.ReadLine()) == 1)
                            {
                                totalMoedas -= valorUpgradeVelocidade;
                                velocidadeBola *= 1.05; // Aumenta a velocidade
                                nivelUpgradeVelocidade++;
                                valorUpgradeVelocidade = 10 * Math.Pow(1.15, nivelUpgradeVelocidade - 1); // Aumenta o custo para o próximo nível
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
                            System.Console.Write("Deseja comprar este upgrade? (1 para sim, 0 para não): ");
                            if (Convert.ToInt32(Console.ReadLine()) == 1)
                            {
                                totalMoedas -= valorUpgradeArremesso;
                                velocidadeArremesso += 1.05; // Aumenta a velocidade
                                nivelUpgradeArremesso++;
                                valorUpgradeArremesso = 10 * Math.Pow(1.15, nivelUpgradeArremesso - 1); // Aumenta o custo para o próximo nível
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
                            System.Console.Write("Deseja comprar este upgrade? (1 para sim, 0 para não): ");
                            if (Convert.ToInt32(Console.ReadLine()) == 1)
                            {
                                totalMoedas -= valorUpgradeElasticidade;
                                elasticidade += 0.5;
                                quiques++;
                                nivelUpgradeElasticidade++;
                                valorUpgradeElasticidade = 10 * Math.Pow(1.15, nivelUpgradeElasticidade - 1); // Aumenta o custo para o próximo nível
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