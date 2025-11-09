using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// 5 - Activamos las referencias
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Capa_Entidad;

namespace Capa_Datos
{
    public class C_Mantenimiento
    {
        //6 - Creamos una variable que mantendra la interacion con la base de datos:
        SqlConnection connection =
            new SqlConnection(ConfigurationManager.ConnectionStrings["sql"].ConnectionString);

        //7 - Creamos un metodo del tipo DataTable y dentro ponemos el codigo que interara con la BD:
        public DataTable D_listar_contactos()
        {
            SqlCommand cmd = new SqlCommand("sp_mostrar_contactos", connection);//este es quien ejecuta la consulta SQL, y la procesa con la variable 'cn'
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);//Adapta la consultaa una tabla de C#
            DataTable dataTable = new DataTable();//Recibe lo adaptado...
            adapter.Fill(dataTable);//Se usa Fill para llenar la tabla
            return dataTable;
        }

        //Metedo que busca en la BD:
        public DataTable D_buscar_Contacto(C_Entidad obj)
        {
            SqlCommand cmd = new SqlCommand("sp_buscar_contacto", connection);//ejecutar el stored procedure sp_buscar_contacto.
            cmd.CommandType = CommandType.StoredProcedure; //
            connection.Open();
            cmd.Parameters.AddWithValue("@telefono", obj.Telefono);// Pasar parámetro al SP
            SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);// Adaptador para llenar un DataTable
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            connection.Close();
            return dataTable;
        }

        //Metodo que inserta un contacto nuevo
        public bool D_insertar_Contacto(C_Entidad obj)
        {
            SqlCommand cmd = new SqlCommand("sp_insertar_contacto", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            connection.Open();
            cmd.Parameters.AddWithValue("@Nombre", obj.Nombre);
            cmd.Parameters.AddWithValue("@Telefono", obj.Telefono);
            cmd.Parameters.AddWithValue("@Email", obj.Email);

            int filasAfectadas = cmd.ExecuteNonQuery();
            connection.Close(); 

            return filasAfectadas > 0;
        }

        //Metodo que modifica un contacto
        public bool D_modificar_Contacto(C_Entidad obj)
        {
            SqlCommand cmd = new SqlCommand("sp_modificar_contacto", connection);
            cmd.CommandType = CommandType.StoredProcedure;

            connection.Open();

            cmd.Parameters.AddWithValue("@idContacto", obj.Id);
            cmd.Parameters.AddWithValue("@Nombre", obj.Nombre);
            cmd.Parameters.AddWithValue("@Telefono", obj.Telefono);
            cmd.Parameters.AddWithValue("@Email", obj.Email);

            int filasAfectadas = cmd.ExecuteNonQuery();
            connection.Close();

            return filasAfectadas > 0;
        }

        //Metedo que elimina contacto en la BD:
        public bool D_eliminar_Contacto(C_Entidad obj)
        {
            SqlCommand cmd = new SqlCommand("sp_eliminar_contacto", connection);//ejecutar el stored procedure sp_buscar_contacto.
            cmd.CommandType = CommandType.StoredProcedure; //

            connection.Open();
            cmd.Parameters.AddWithValue("@idContacto", obj.Id);// Pasar parámetro al SP

            int filasAfectadas = cmd.ExecuteNonQuery();//Ejecuta
            connection.Close();//Cerramos la conexion

            return filasAfectadas > 0;// Si al menos una fila fue eliminada, devolvemos true  
        }

        //Logica del CRUD...
    }
}
