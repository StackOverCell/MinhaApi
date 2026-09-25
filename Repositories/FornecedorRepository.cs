using MinhaApi.Models;
using MinhaApi.Repository;
using MySqlConnector;

public class FornecedorRepository: IFornecedorRepository
{
    private readonly string _connectionString;

    public FornecedorRepository(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("DefaultConnection");
    }

    public IEnumerable<Fornecedor> GetAll()
    {
        var lista = new List<Fornecedor>();
        using var conn = new MySqlConnection(_connectionString);
        conn.Open();

        String sql = "SELECT id, nome, cnpj, email, telefone, ativo FROM fornecedor";
        using var cmd = new MySqlCommand(sql, conn);
        using var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            lista.Add(new Fornecedor
            {
                Id = reader.GetInt32("id"),
                Nome = reader.GetString("nome"),
                Cnpj = reader.GetString("cnpj"),
                Email = reader.GetString("email"),
                Ativo = reader.GetBoolean("ativo")
            });
        }
        return lista;
    }

    public Fornecedor? GetById(int id)
    {
        using var conn = new MySqlConnection(_connectionString);
        conn.Open();

        string sql = @"SELECT id, nome, cnpj, email, ativo
                       FROM fornecedor
                       WHERE id = @Id";
        using var cmd = new MySqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@Id", id);

        using var reader = cmd.ExecuteReader();

        if (reader.Read())
        {
            return new Fornecedor
            {
                Id = reader.GetInt32("id"),
                Nome = reader.GetString("nome"),
                Cnpj = reader.GetString("cnpj"),
                Email = reader.GetString("email"),
                Ativo = reader.GetBoolean("ativo")
            };
        }
        return null;
    }

    public void Add(Fornecedor f)
    {
        using var conn = new MySqlConnection(_connectionString);
        conn.Open();
        string sql = @"INSERT INTO fornecedor (nome, cnpj, email, ativo)
                       VALUES (@Nome, @Cnpj, @Email, @Ativo);
                       SELECT LAST_INSERT_ID()";
        using var cmd = new MySqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@Nome", f.Nome);
        cmd.Parameters.AddWithValue("@Cnpj", f.Cnpj);
        cmd.Parameters.AddWithValue("@Email", f.Email);
        cmd.Parameters.AddWithValue("@Ativo", f.Ativo);

        var idGerado = cmd.ExecuteScalar();
        f.Id = Convert.ToInt32(idGerado);
    }

    public void Update(Fornecedor f)
    {
        using var conn = new MySqlConnection(_connectionString);
        conn.Open();
        string sql = @"UPDATE fornecedor
                       SET nome = @Nome, cnpj = @Cnpj, email = @Email, ativo = @Ativo
                       WHERE id = @Id";
        using var cmd = new MySqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@Id", f.Id);
        cmd.Parameters.AddWithValue("@Nome", f.Nome);
        cmd.Parameters.AddWithValue("@Cnpj", f.Cnpj);
        cmd.Parameters.AddWithValue("@Email", f.Email);
        cmd.Parameters.AddWithValue("@Ativo", f.Ativo);
        cmd.ExecuteNonQuery();
    }

    public void Delete(int id)
    {
        using var conn = new MySqlConnection(_connectionString);
        conn.Open();
        string sql = @"DELETE FROM fornecedor WHERE id = @Id";
        using var cmd = new MySqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@Id", id);
        cmd.ExecuteNonQuery();
    }

}