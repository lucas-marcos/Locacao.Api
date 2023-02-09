namespace Locacao.Api.Data.Interfaces;

public interface IRepository<TEntity>
{
    /// <summary>
    /// Irá adicionar o objeto no em memória
    /// OBS: PRA SALVAR NO BANCO, CHAME O MÉTODO Salvar()
    /// </summary>
    public void Adicionar(TEntity obj);

    /// <summary>
    /// Irá atualizar o objeto no em memória
    /// OBS: PRA SALVAR NO BANCO, CHAME O MÉTODO Salvar()
    /// </summary>
    public void Atualizar(TEntity obj);

    public TEntity BuscarPorId(int id);
    public IQueryable<TEntity> BuscarTodos();
    public int Salvar();
    void Remover(int id);
}