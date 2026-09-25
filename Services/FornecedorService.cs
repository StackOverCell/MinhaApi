using MinhaApi.Models;
using MinhaApi.Services;
using MinhaApi.Repository;

public class FornecedorService : IFornecedorService
{
    private readonly IFornecedorRepository _repo;

    public FornecedorService(IFornecedorRepository repo)
        => _repo = repo;

    public IEnumerable<Fornecedor> GetAll()
        => _repo.GetAll();

    public Fornecedor? GetById(int id)
        => _repo.GetById(id);

    public Fornecedor Create(Fornecedor fornecedor)
    {
        if (fornecedor.Nome == null)
            throw new ArgumentException("Fornecedor inválido");

        _repo.Add(fornecedor);
        return fornecedor;
    }

    public Fornecedor? Update(int id, Fornecedor f)
    {
        if (f.Nome == null)
            throw new ArgumentException("Fornecedor inválido");

        if (_repo.GetById(id) == null) return null;

        f.Id = id;
        _repo.Update(f);
        return f;
    }

    public bool Delete(int id)
    {
        var fornecedor = _repo.GetById(id);
        if (fornecedor == null) return false;

        _repo.Delete(id);
        return true;
    }
}