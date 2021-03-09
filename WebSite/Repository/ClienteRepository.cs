using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using WebSite.Models;

namespace WebSite.Repository
{
    public class ClienteRepository
    {
        private string connString = ConfigurationManager.ConnectionStrings["DBTeste"].ConnectionString;

        public long GetTotalRecords(string search)
        {
            long totalRecords = 0;
            using(SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SP_GetTotalRecords", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@search", search);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    totalRecords = Convert.ToInt64(reader["TotalRecords"]);
                }
            }
            return totalRecords;
        }

        public List<Cliente> List(int start, int length, string search)
        {
            List<Cliente> clientes = new List<Cliente>();
            using(SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SP_ListarClientes", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@start", start);
                cmd.Parameters.AddWithValue("@length", length);
                cmd.Parameters.AddWithValue("@search", search);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    clientes.Add(new Cliente {
                        ClienteId = Convert.ToInt64(reader["ClienteId"]),
                        NomeCliente = reader["Cliente"].ToString(),
                        TipoCliente = reader["TipoCliente"].ToString(),
                        NomeContato = reader["NomeContato"].ToString(),
                        TelefoneContato = reader["TelefoneContato"].ToString(),
                        Cidade = reader["Cidade"].ToString(),
                        Bairro = reader["Bairro"].ToString(),
                        Logradouro = reader["Logradouro"].ToString(),
                        DataCadastro = reader["DataCadastro"].ToString(),
                        DataAtualizacao = reader["DataAtualizacao"].ToString()
                   });
                }
            }
            return clientes;
        }

        public int Add(Cliente cliente)
        {
            int affectedRow;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SP_AdicionarCliente", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Cliente", cliente.NomeCliente);
                cmd.Parameters.AddWithValue("@TipoCliente", cliente.TipoCliente);
                cmd.Parameters.AddWithValue("@NomeContato", cliente.NomeContato);
                cmd.Parameters.AddWithValue("@TelefoneContato", cliente.TelefoneContato);
                cmd.Parameters.AddWithValue("@Cidade", cliente.Cidade);
                cmd.Parameters.AddWithValue("@Bairro", cliente.Bairro);
                cmd.Parameters.AddWithValue("@Logradouro", cliente.Logradouro);
                cmd.Parameters.AddWithValue("@DataCadastro", DateTime.Now.ToString("yyyy-MM-dd"));
                affectedRow = cmd.ExecuteNonQuery();
            }
            return affectedRow;
        }

        public Cliente Find(long clienteId)
        {
            Cliente cliente = null;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SP_ObterCliente", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ClienteId", clienteId);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    cliente = new Cliente
                    {
                        ClienteId = Convert.ToInt64(reader["ClienteId"]),
                        NomeCliente = reader["Cliente"].ToString(),
                        TipoCliente = reader["TipoCliente"].ToString(),
                        NomeContato = reader["NomeContato"].ToString(),
                        TelefoneContato = reader["TelefoneContato"].ToString(),
                        Cidade = reader["Cidade"].ToString(),
                        Bairro = reader["Bairro"].ToString(),
                        Logradouro = reader["Logradouro"].ToString(),
                        DataCadastro = reader["DataCadastro"].ToString(),
                        DataAtualizacao = reader["DataAtualizacao"].ToString()
                    };
                }
            }
            return cliente;
        }

        public int Update(Cliente cliente)
        {
            int affectedRow;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SP_AtualizarCliente", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ClienteId", cliente.ClienteId);
                cmd.Parameters.AddWithValue("@Cliente", cliente.NomeCliente);
                cmd.Parameters.AddWithValue("@TipoCliente", cliente.TipoCliente);
                cmd.Parameters.AddWithValue("@NomeContato", cliente.NomeContato);
                cmd.Parameters.AddWithValue("@TelefoneContato", cliente.TelefoneContato);
                cmd.Parameters.AddWithValue("@Cidade", cliente.Cidade);
                cmd.Parameters.AddWithValue("@Bairro", cliente.Bairro);
                cmd.Parameters.AddWithValue("@Logradouro", cliente.Logradouro);
                cmd.Parameters.AddWithValue("@DataAtualizacao", DateTime.Now.ToString("yyyy-MM-dd"));
                affectedRow = cmd.ExecuteNonQuery();
            }
            return affectedRow;
        }

        public int Delete(long id)
        {
            int affectedRow;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SP_ExcluirCliente", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ClienteId", id);
                affectedRow = cmd.ExecuteNonQuery();
            }
            return affectedRow;
        }
    }
}