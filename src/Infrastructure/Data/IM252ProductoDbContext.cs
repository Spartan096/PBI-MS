using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Domain;

namespace Infrastructure
{
    public class ProductosDbContext
    {
        private readonly string _connectionString;

        public ProductosDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }

public List<IM252Producto> List()
{
    var data = new List<IM252Producto>();
    var con = new SqlConnection(_connectionString);
    var cmd = new SqlCommand("SELECT [Id],[Descripcion],[Precio],[Cantidad],[Foto] FROM [IM252Producto]", con);
    try
    {
        con.Open();
        var dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            data.Add(new IM252Producto
            {
                Id = (Guid)dr["Id"],
                Descripcion = dr["Descripcion"] as string ?? string.Empty,
                Precio = Convert.ToSingle(dr["Precio"]), // Cambio aquí
                Cantidad = Convert.ToInt32(dr["Cantidad"]), // Cambio aquí
                Foto = dr["Foto"] as string ?? string.Empty
            });
        }
        return data;
    }
    catch (Exception)
    {
        throw;
    }
    finally
    {
        con.Close();
    }
}


        public IM252Producto Details(Guid id)
        {
            var data = new IM252Producto();
            var con = new SqlConnection(_connectionString);
            var cmd = new SqlCommand("SELECT [Id],[Descripcion],[Precio],[Cantidad],[Foto] FROM [IM252Producto] WHERE [Id] = @id", con);
            cmd.Parameters.Add("@id", SqlDbType.UniqueIdentifier).Value = id;
            try
            {
                con.Open();
                var dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    data.Id = (Guid)dr["Id"];
                    data.Descripcion = dr["Descripcion"] as string ?? string.Empty;
                    data.Precio = Convert.ToSingle(dr["Precio"]); 
                    data.Cantidad = Convert.ToInt32(dr["Cantidad"]);
                    data.Foto = dr["Foto"] as string ?? string.Empty;
                }
                return data;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
        }

        public void Create(IM252Producto data)
        {
            var con = new SqlConnection(_connectionString);
            var cmd = new SqlCommand("INSERT INTO [IM252Producto] ([Id],[Descripcion],[Precio],[Cantidad],[Foto]) VALUES (@id,@descripcion,@precio,@cantidad,@foto)", con);
            cmd.Parameters.Add("id", SqlDbType.UniqueIdentifier).Value = Guid.NewGuid();
            cmd.Parameters.Add("descripcion", SqlDbType.NVarChar, 256).Value = data.Descripcion;
            cmd.Parameters.Add("precio", SqlDbType.Float).Value = data.Precio;
            cmd.Parameters.Add("cantidad", SqlDbType.Int).Value = data.Cantidad;
            cmd.Parameters.Add("foto", SqlDbType.NVarChar).Value = (object)data.Foto ?? DBNull.Value;
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
        }

        public void Edit(IM252Producto data)
        {
            var con = new SqlConnection(_connectionString);
            var cmd = new SqlCommand("UPDATE [IM252Producto] SET [Descripcion] = @descripcion, [Precio] = @precio, [Cantidad] = @cantidad, [Foto] = @foto WHERE [Id] = @id", con);
            cmd.Parameters.Add("id", SqlDbType.UniqueIdentifier).Value = data.Id;
            cmd.Parameters.Add("descripcion", SqlDbType.NVarChar, 256).Value = data.Descripcion;
            cmd.Parameters.Add("precio", SqlDbType.Float).Value = data.Precio;
            cmd.Parameters.Add("cantidad", SqlDbType.Int).Value = data.Cantidad;
            cmd.Parameters.Add("foto", SqlDbType.NVarChar).Value = (object)data.Foto ?? DBNull.Value;
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
        }

        public void Delete(Guid id)
        {
            var con = new SqlConnection(_connectionString);
            var cmd = new SqlCommand("DELETE FROM [IM252Producto] WHERE [Id] = @id", con);
            cmd.Parameters.Add("id", SqlDbType.UniqueIdentifier).Value = id;
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
        }
    }
}
