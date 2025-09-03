
using ProjetoTarefas;
namespace ProjetoTarefas;


using Microsoft.EntityFrameworkCore;

class BancoDados : DbContext
{

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source = bdtarefas.sqlite");
        base.OnConfiguring(optionsBuilder);
    }

    public DbSet<Tarefa> TabelaTarefas { get; set; } = null!;
}