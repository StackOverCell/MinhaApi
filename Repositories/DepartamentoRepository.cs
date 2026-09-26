using MinhaApi.Models;
using MinhaApi.Repository;
using MySqlConnector;

public class DepartamentoRepository: IDepartamentoRepository {
    private readonly string _connectionString;

    public DepartamentoRepository(IConfiguration config)
    => _connectionString = config.GetConnectionString("DefaultConnection")!;

  public IEnumerable<Departamento> GetAll()
  {
      var lista = new List<Departamento>();
      using var conn = new MySqlConnection(_connectionString);
      conn.Open();

      string sql = "SELECT id, id_fornecedor, nome, descricao, ativo FROM departamento";
      using var cmd = new MySqlCommand(sql, conn);
      using var reader = cmd.ExecuteReader();

      while (reader.Read()) {
          lista.Add(new Departamento {
              Id = reader.GetInt32("id"),
              FornecedorId = reader.GetInt32("id_fornecedor"),
              Nome = reader.GetString("nome"),
              Descricao = reader.GetString("descricao"),
              Ativo = reader.GetBoolean("ativo")
          });
      }
      return lista;
  }
  
  public Departamento? GetById(int id)
    {
    using var conn = new MySqlConnection(_connectionString);
    conn.Open();

    string sql = @"SELECT id, id_fornecedor, nome, descricao, ativo FROM departamento WHERE id = @Id;";

    using var cmd = new MySqlCommand(sql, conn);
    cmd.Parameters.AddWithValue("@Id", id);
    using var reader = cmd.ExecuteReader();

    if (reader.Read())
    {
        return new Departamento
        {
            Id = reader.GetInt32("id"),
            FornecedorId = reader.GetInt32("id_fornecedor"),
            Nome = reader.GetString("nome"),
            Descricao = reader.GetString("descricao"),
            Ativo = reader.GetBoolean("ativo")
        };
    }
    return null;
    }
      

  public void Add(Departamento d)
  {
      using var conn = new MySqlConnection(_connectionString);
      conn.Open();

      string sql = @"insert into departamento (id_fornecedor, nome, descricao, ativo) values (@Nome, @Descricao, @Ativo); select last_insert_id();";
    
    using var cmd = new MySqlCommand(sql, conn);
    cmd.Parameters.AddWithValue("@id_fornecedor", d.FornecedorId);
    cmd.Parameters.AddWithValue("@Nome", d.Nome);
    cmd.Parameters.AddWithValue("@Descricao", d.Descricao);
    cmd.Parameters.AddWithValue("@Ativo", d.Ativo);

    var idGerado = cmd.ExecuteScalar();
    d.Id = Convert.ToInt32(idGerado);
  }

  public void Update(Departamento d)
  {
      using var conn = new MySqlConnection(_connectionString);
      conn.Open();
      string sql = @"update departamento SET nome = @Nome, descricao = @Descricao WHERE id = @Id;";

      using var cmd = new MySqlCommand(sql, conn);
      cmd.Parameters.AddWithValue("@Id", d.Id);
      cmd.Parameters.AddWithValue("@Nome", d.Nome);
      cmd.Parameters.AddWithValue("@Email", d.Descricao);
      cmd.ExecuteNonQuery();
  }

  public void Delete(int id)
    {
        using var conn = new MySqlConnection(_connectionString);
        conn.Open();
        string sql = "UPDATE departamento SET ativo = false WHERE id = @Id";
        using var cmd = new MySqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@Id", id);
        cmd.ExecuteNonQuery();
    }
}