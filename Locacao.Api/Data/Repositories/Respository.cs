using Locacao.Api.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Locacao.Api.Data.Repositories;

public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    protected ApplicationDbContext Db;
    protected readonly DbSet<TEntity> DbSet;
    
    protected Repository(ApplicationDbContext context)
    {
        Db = context;
        DbSet = Db.Set<TEntity>();
    }
    public void Adicionar(TEntity obj)
    {
        if (obj == null)
            return;

        DbSet.Add(obj);
    }
    public void Atualizar(TEntity obj)
    {
        if (obj == null)
            return;

        DbSet.Update(obj);
    }
    public void Remover(int id)
    {
        DbSet.Remove(DbSet.Find(id));
    }
    public virtual TEntity BuscarPorId(int id)
    {
        return DbSet.Find(id);
    }
    public IQueryable<TEntity> BuscarTodos()
    {
        return DbSet;
    }
    public int Salvar()
    {
        return Db.SaveChanges();
    }
    
}

