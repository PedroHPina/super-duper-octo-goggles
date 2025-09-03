using ProjetoTarefas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


//Tabelatarefas salvo na pasta debug

class Program
{
    static BancoDados Banco = new();
    static void Main(string[] args)
    {
        
        
        int opcao = 0;
        Console.Clear();
        Console.Write("1- CADASTRAR\n2- LISTAR\n3- EDITAR\n4- APAGAR\n5- CONCLUIR\nDIGITE A OPÇÃO DESEJADA: ");
        opcao = Convert.ToInt32(Console.ReadLine());
        switch(opcao)
        {
            case 1:
                IncluirTarefa();
                break;
            case 2:
                ListarTarefas();
                break;
            case 3:
                EditarTarefa();
                break;
            case 4:
                ExcluirTarefa();
                break;
            case 5:
                ConcluirTarefa();
                break;
        }
        if(opcao >= 1 && opcao <= 5) Main(args);

    }

    static void IncluirTarefa()
    {
        string? nome, endereco;
        int? status;
        Console.Clear();
        Console.Write("Digite o nome da Tarefa: ");
        nome = Console.ReadLine();
        Console.Write("Digite o status da Tarefa(0- Pendente | 1-Concluida): ");
        status= Convert.ToInt32(Console.ReadLine());
        Banco.TabelaTarefas.Add(new Tarefa
        {
            Nome = nome,
            Status = status
        });
        Banco.SaveChanges();
        Console.WriteLine("Tarefa cadastrada com sucesso!");
        Console.ReadKey();
    }

    static void ListarTarefas()
    {
        List<Tarefa> lista = [];
        lista = Banco.TabelaTarefas.OrderBy(t => t.Nome).ToList();
        if (lista.Count == 0) Console.WriteLine("Nenhuma tarefa cadastrada!");
        else
        {
            foreach(Tarefa cli in lista)
            {
                Console.WriteLine($"{cli.Nome} - {cli.Status}");
            }
        }
        Console.ReadKey();
    }

    static void EditarTarefa()
    {
        List<Tarefa> lista = [];
        lista = Banco.TabelaTarefas.OrderBy(t => t.Nome).ToList();
        if (lista.Count == 0) Console.WriteLine("Nenhuma tarefa cadastrada!");
        else
        {
            foreach (Tarefa cli in lista)
            {
                Console.WriteLine($"{cli.Nome} - {cli.Status}");
            }

            int id;
            Console.Write("Digite o ID para editar: ");
            id = Convert.ToInt32(Console.ReadLine());
            Tarefa? registro = lista.SingleOrDefault(t => t.Id == id);
            Console.Write("Digite o nome da Tarefa: ");
            registro!.Nome = Console.ReadLine();
            Console.Write("Digite o status da Tarefa(0- Pendente | 1-Concluida): ");
            registro!.Status = Convert.ToInt32(Console.ReadLine());
            Banco.TabelaTarefas.Update(registro);
            Banco.SaveChanges();
            Console.WriteLine("Tarefa atualizada com sucesso!");
        }
        Console.ReadKey();
    }
    
    static void ExcluirTarefa()
    {
        List<Tarefa> lista = [];
        lista = Banco.TabelaTarefas.OrderBy(t => t.Nome).ToList();
        if (lista.Count == 0) Console.WriteLine("Nenhuma tarefa cadastrada!");
        else
        {
            foreach(Tarefa cli in lista)
            {
                Console.WriteLine($"{cli.Nome} - {cli.Status}");
            }
            int id;
            char resp;
            Console.Write("Digite o ID para excluir a tarefa:");
            id = Convert.ToInt32(Console.ReadLine());
            Tarefa? registro = lista.SingleOrDefault(t => t.Id == id);
            Console.Write("Tem certeza que deseja excluir a tarefa?(Resposta: sim-S|nao-N): ");
            resp = Console.ReadLine()[0];
            if (resp == 's' || resp == 'S')
            {
                Banco.TabelaTarefas.Remove(registro!);
                Banco.SaveChanges();
                Console.WriteLine("Tarefa excluida com sucesso");
                
            }
        }
        Console.ReadKey();
    }

    static void ConcluirTarefa()
    {
        List<Tarefa> lista = [..Banco.TabelaTarefas.Where(t => t.Status == 0).OrderBy(t => t.Nome)];
        if (lista.Count == 0) Console.WriteLine("Nenhuma tarefa cadastrada!");
        else
        {
            foreach (Tarefa cli in lista)
            {
                Console.WriteLine($"{cli.Nome} - {cli.Status}");
            }
            int id;
            Console.Write("Digite o ID para concluir a tarefa:");
            id = Convert.ToInt32(Console.ReadLine());
            Tarefa? registro = lista.SingleOrDefault(t => t.Id == id);
            if (registro != null)
            {
                registro.Status = 1;
                Banco.SaveChanges();
                Console.WriteLine("Tarefa concluida com sucesso");
            }
        }
        Console.ReadKey();
    }

}
