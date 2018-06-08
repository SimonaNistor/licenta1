using DBModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Database
{
    public class AdvancedSearchRepository : IAdvancedSearchRepository
    {
        private AdvancedSearch FromReader(SqlDataReader sqlDataReader)
        {
            return new AdvancedSearch
            {
                Id = (int)sqlDataReader["Id"],
                HtmlTypeId = (int)sqlDataReader["HtmlTypeId"],
                Value = sqlDataReader["Value"].ToString()
            };

        }

        public int Create(AdvancedSearch menuItem)
        {
            using (var con = new SqlConnection(SQLHelper.ConnectionString))
            {
                var proc = con.CreateCommand();
                proc.CommandType = System.Data.CommandType.StoredProcedure;
                proc.CommandText = "AdvancedSearch_Create";
                con.Open();
                proc.Parameters.AddWithValue("@HtmlTypeId", menuItem.HtmlTypeId);
                proc.Parameters.AddWithValue("@Value", menuItem.Value);
                return (int)proc.ExecuteScalar();
            }
        }

        public void Delete(int itemId)
        {
            using (var con = new SqlConnection(SQLHelper.ConnectionString))
            {
                var proc = con.CreateCommand();
                proc.CommandType = System.Data.CommandType.StoredProcedure;
                proc.CommandText = "AdvancedSearch_Delete";
                con.Open();
                proc.Parameters.AddWithValue("@Id", itemId);
                using (var reader = proc.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        proc.ExecuteScalar();
                    }
                }

            }
        }

        public AdvancedSearch GetById(int itemId)
        {
            using (var con = new SqlConnection(SQLHelper.ConnectionString))
            {
                var proc = con.CreateCommand();
                proc.CommandType = System.Data.CommandType.StoredProcedure;
                proc.CommandText = "AdvancedSearch_GetById";
                con.Open();
                proc.Parameters.AddWithValue("@Id", itemId);
                using (var reader = proc.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return FromReader(reader);
                    }
                    else
                    {
                        return null;
                    }
                }

            }
        }

        public List<AdvancedSearch> GetByHtmlTypeId(int itemId)
        {
            using (var con = new SqlConnection(SQLHelper.ConnectionString))
            {
                var proc = con.CreateCommand();
                proc.CommandType = System.Data.CommandType.StoredProcedure;
                proc.CommandText = "AdvancedSearch_GetByHtmlTypeId";
                con.Open();
                proc.Parameters.AddWithValue("@HtmlTypeId", itemId);
                List<AdvancedSearch> lista = new List<AdvancedSearch>();
                using (var reader = proc.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(FromReader(reader));
                    }
                }
                return lista;

            }
        }

        public int Update(AdvancedSearch menuItem)
        {
            using (var con = new SqlConnection(SQLHelper.ConnectionString))
            {
                var proc = con.CreateCommand();
                proc.CommandType = System.Data.CommandType.StoredProcedure;
                proc.CommandText = "AdvancedSearch_Update";
                con.Open();
                proc.Parameters.AddWithValue("@Id", menuItem.Id);
                proc.Parameters.AddWithValue("@HtmlTypeId", menuItem.HtmlTypeId);
                proc.Parameters.AddWithValue("@Value", menuItem.Value);
                return (int)proc.ExecuteScalar();
            }
        }

        public List<AdvancedSearch> GetAll()
        {
            using (var con = new SqlConnection(SQLHelper.ConnectionString))
            {
                var proc = con.CreateCommand();
                proc.CommandType = System.Data.CommandType.StoredProcedure;
                proc.CommandText = "AdvancedSearch_GetAll";
                con.Open();
                List<AdvancedSearch> list = null;
                using (var reader = proc.ExecuteReader())
                {
                    list = new List<AdvancedSearch>();
                    while (reader.Read())
                    {
                        list.Add(FromReader(reader));
                    }
                }
                return list;
            }
        }

        
    }

    public interface IAdvancedSearchRepository
    {
        int Create(AdvancedSearch menuItem);
        void Delete(int itemId);
        AdvancedSearch GetById(int itemId);
        int Update(AdvancedSearch menuItem);
        List<AdvancedSearch> GetAll();
        List<AdvancedSearch> GetByHtmlTypeId(int itemId);
    }
}
