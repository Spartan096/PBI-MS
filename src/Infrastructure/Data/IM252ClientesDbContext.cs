using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Domain;

namespace Infrastructure
{
    public class IM252ClientesDbContext : DbContext
    {
        private readonly string _connectionString;

        public IM252ClientesDbContext(DbContextOptions<IM252ClientesDbContext> options)
            : base(options)
        {
            // Asegúrate de que la cadena de conexión se inicializa correctamente
            var connection = Database.GetDbConnection();
            _connectionString = connection.ConnectionString;
        }

        public DbSet<IM252Cliente> IM252Cliente { get; set; } // Para Entity Framework Core

        [Obsolete]
        public List<IM252Cliente> List()
        {
            var clientes = new List<IM252Cliente>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("SELECT * FROM [IM252Cliente]", connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        clientes.Add(new IM252Cliente
                        {
                            ID = Guid.Parse(reader["ID"].ToString()),
                            Nombre = reader["Nombre"].ToString(),
                            Direccion = reader["Direccion"].ToString(),
                            Telefono = reader["Telefono"].ToString(),
                            Correo = reader["Correo"].ToString()
                        });
                    }
                }
            }

            return clientes;
        }

public IM252Cliente Details(Guid id)
{
    IM252Cliente cliente = null;

    using (var connection = new SqlConnection(_connectionString))
    using (var command = new SqlCommand("SELECT [ID], [Nombre], [Direccion], [Telefono], [Correo] FROM [IM252Cliente] WHERE ID = @ID", connection))
    {
        command.Parameters.AddWithValue("@ID", id);
        connection.Open();

        using (var reader = command.ExecuteReader())
        {
            if (reader.Read())
            {
                cliente = new IM252Cliente
                {
                    ID = reader.GetGuid(reader.GetOrdinal("ID")), // Cast directo sin .ToString()
                    Nombre = reader["Nombre"] as string ?? string.Empty,
                    Direccion = reader["Direccion"] as string ?? string.Empty,
                    Telefono = reader["Telefono"] as string ?? string.Empty,
                    Correo = reader["Correo"] as string ?? string.Empty
                };
            }
        }
    }

    return cliente;
}


        public void Create(IM252Cliente cliente)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("INSERT INTO [IM252Cliente] (ID, Nombre, Direccion, Telefono, Correo) VALUES (@ID, @Nombre, @Direccion, @Telefono, @Correo)", connection);
                command.Parameters.AddWithValue("@ID", cliente.ID);
                command.Parameters.AddWithValue("@Nombre", cliente.Nombre);
                command.Parameters.AddWithValue("@Direccion", cliente.Direccion);
                command.Parameters.AddWithValue("@Telefono", cliente.Telefono);
                command.Parameters.AddWithValue("@Correo", cliente.Correo);
                command.ExecuteNonQuery();
            }
        }

        public void Edit(IM252Cliente cliente)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("UPDATE [IM252Cliente] SET Nombre = @Nombre, Direccion = @Direccion, Telefono = @Telefono, Correo = @Correo WHERE ID = @ID", connection);
                command.Parameters.AddWithValue("@ID", cliente.ID);
                command.Parameters.AddWithValue("@Nombre", cliente.Nombre);
                command.Parameters.AddWithValue("@Direccion", cliente.Direccion);
                command.Parameters.AddWithValue("@Telefono", cliente.Telefono);
                command.Parameters.AddWithValue("@Correo", cliente.Correo);
                command.ExecuteNonQuery();
            }
        }

        public void Delete(Guid id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("DELETE FROM [IM252Cliente] WHERE ID = @ID", connection);
                command.Parameters.AddWithValue("@ID", id);
                command.ExecuteNonQuery();
            }
        }
    }
}

