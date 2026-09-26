using MinhaApi.Models;
using MinhaApi.Services;
using MinhaApi.Repository;

public class DepartamentoService : IDepartamentoService
{
  private readonly IDepartamentoRepository _repo;

  public DepartamentoService(IDepartamentoRepository repo)
      => _repo = repo;

  public IEnumerable<Departamento> GetAll()
      => _repo.GetAll();

  public Departamento? GetById(int id)
      => _repo.GetById(id);

  public Departamento Create(Departamento departamento)
  {
      if (departamento.Nome == null)
          throw new ArgumentException("Departamento inválido");

      _repo.Add(departamento);
      return departamento;
  }

  public Departamento? Update(int id, Departamento d)
  {
      if (d.Nome == null)
          throw new ArgumentException("Departamento inválido");

      if (_repo.GetById(id) == null) return null;

      d.Id = id;
      _repo.Update(d);
      return d;
  }

  public bool Delete(int id)
  {
      if (_repo.GetById(id) == null) return false;

      _repo.Delete(id);
      return true;
  }
}