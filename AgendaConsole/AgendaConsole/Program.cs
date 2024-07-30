using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaConsole
{
    internal class Program
    {
        static int ExibirMenu()
        {
            int op = 0;
            Console.Clear();
            Console.WriteLine("Agenda Modo Console");
            Console.WriteLine("Exibir Contatos - 1");
            Console.WriteLine("Inserir Contato -2");
            Console.WriteLine("Alterar Contato -3");
            Console.WriteLine("Excluir Contato - 4");
            Console.WriteLine("Localizar Contato - 5");
            Console.WriteLine("Sair - 6");
            Console.Write("Opção: ");
            op = Convert.ToInt32(Console.ReadLine());
            Console.Clear();
            return op;
        }

        static void ExibirContatos(String[] nome, String[] email, int tl)
        {
            Console.WriteLine("Exibir Contatos");
            for (int i = 0; i < tl; i++)
            {
                Console.WriteLine("Nome: {0} - E-mail: {1}", nome[i], email[i]);
            }
            Console.ReadKey();
        }

        static void InserirContato(ref String[] nome, ref String[] email, ref int tl)
        {
            try
            {
                if (tl >= 200)
                {
                    Console.WriteLine("Tamanho máximo atingido!!!!");
                }
                else
                {
                    Console.WriteLine("Inserir Contato");
                    Console.Write("Nome: ");
                    nome[tl] = Console.ReadLine();
                    Console.Write("E-mail: ");
                    email[tl] = Console.ReadLine();
                    int pos = LocalizarContato(email, tl, email[tl]);
                    if (pos == -1)
                    {
                        tl++;
                        Console.WriteLine("Contato inserido.");
                    }
                    else
                    {
                        Console.WriteLine("Contato já cadastrado.");
                    }
                    Console.ReadKey();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Erro:" + e.Message);
                Console.ReadKey();
            }
            
        }

        static void AlterarContato(ref String[] nome, ref String[] email, ref int tl)
        {
            try
            {
                Console.WriteLine("Alterar Contato");
                Console.Write("E-mail: ");
                string emailContato = Console.ReadLine();
                int pos = LocalizarContato(email, tl, emailContato);
                if (pos != -1)
                {
                    Console.WriteLine("Novos dados do Contato");
                    Console.Write("Nome: ");
                    string novoNome = Console.ReadLine();
                    Console.Write("E-mail: ");
                    string novoEmail = Console.ReadLine();
                    int posV = LocalizarContato(email, tl, novoEmail);
                    if (posV == -1 || posV == pos)
                    {
                        nome[pos] = novoNome;
                        email[pos] = novoEmail;
                        Console.WriteLine("Contato alterado.");
                    }
                    else
                    {
                        Console.WriteLine("Já existe um contato com este e-mail");
                    }
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("Contato não encontrado");
                    Console.ReadKey();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Erro:" + e.Message);
                Console.ReadKey();
            }
        }

        static Boolean ExcluirContato(ref String[] nome, ref String[] email, 
            ref int tl, String emailContato)
        {
            Boolean excluiu = false;
            int pos = -1;
            pos = LocalizarContato(email, tl, emailContato);
            //[1,5,7,9,9]
            //pos = 1
            if (pos != -1)
            {
                for (int i = pos; i < tl-1; i++)
                {
                    nome[i] = nome[i + 1];
                    email[i] = email[i + 1];
                }
                excluiu = true;
                tl--;
            }
            return excluiu;
        }
        static int LocalizarContato(String[] email, int tl, String emailContato)
        {
            int pos = -1;
            int i = 0;
            while(i<tl && email[i]!= emailContato)
            {
                i++;
            }
            if (i < tl) pos = i;
            return pos;
        }
        static void Main(string[] args)
        {
            //armazana os dados da agenda
            String[] nome = new string[200];
            String[] email = new string[200];     
            //tamanha lógico
            int tl = 0;
            int op = 0;
            int pos = 0;
            String emailContato = "";

            //carregar dados do arquivo texto
            BackupAgenda.nomeArquivo = "dados.txt";
            BackupAgenda.RestaurarDados(ref nome,ref email,ref tl);

            while (op != 6)
            {
                op = ExibirMenu();
                switch (op)
                {
                    case 1:
                        //exibir os contatos
                        ExibirContatos(nome,email,tl);
                        break;
                    case 2:
                        //inserir um contato
                        InserirContato(ref nome, ref email, ref tl);
                        break;
                    case 3:
                        //alterar um contato
                        AlterarContato(ref nome, ref email, ref tl);
                        break;
                    case 4:
                        //excluir um contato
                        Console.WriteLine("Excluir um contato");
                        Console.Write("E-mail: ");
                        emailContato = Console.ReadLine();
                        if (ExcluirContato(ref nome, ref email, ref tl, emailContato))
                        {
                            Console.WriteLine("Contado excluido");
                        }
                        else
                        {
                            Console.WriteLine("Contado Não encontrado");
                        }
                        Console.ReadKey();
                        break;
                    case 5:
                        //localizar um contato
                        Console.WriteLine("Localizar um contato");
                        Console.Write("E-mail: ");
                        emailContato = Console.ReadLine();
                        pos = LocalizarContato(email,tl,emailContato);
                        if(pos != -1)
                        {
                            Console.WriteLine("Nome: {0} - E-mail: {1}", nome[pos], email[pos]);  
                        }
                        else
                        {
                            Console.WriteLine("Contato não encontrado");
                        }
                        Console.ReadKey();
                        break;
                }
            }
            //salvar dados no arquivo texto
            BackupAgenda.SalvarDados(ref nome, ref email, ref tl);
        }
    }
}
