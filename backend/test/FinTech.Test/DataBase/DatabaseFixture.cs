using Npgsql;

public class DatabaseFixture : IDisposable
{
    private readonly NpgsqlConnection _connection;

    public DatabaseFixture()
    {
        _connection = new NpgsqlConnection("User ID=postgres;Password=123456;Host=localhost;Port=5432;Database=FinTech;Pooling=true;");
        _connection.Open();
    }

    public NpgsqlTransaction BeginTransaction()
    {
        return _connection.BeginTransaction();
    }

    public void Dispose()
    {
        _connection.Close();
    }
}