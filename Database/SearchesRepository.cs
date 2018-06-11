using DBModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Database
{
    public class SearchesRepository:ISearchesRepository
    {
        private Searches FromReader(SqlDataReader sqlDataReader)
        {
            return new Searches
            {
                Id = (int)sqlDataReader["Id"],
                Keywords = sqlDataReader["Keywords"].ToString(),
                Date = (DateTime)sqlDataReader["Date"],
                Ip = sqlDataReader["Ip"].ToString()
            };

        }

        public int Create(String keywords, DateTime date, String ip)
        {
            using (var con = new SqlConnection(SQLHelper.ConnectionString))
            {
                var proc = con.CreateCommand();
                proc.CommandType = System.Data.CommandType.StoredProcedure;
                proc.CommandText = "Searches_Create";
                con.Open();
                proc.Parameters.AddWithValue("@Keywords", keywords);
                proc.Parameters.AddWithValue("@Date", date);
                proc.Parameters.AddWithValue("@Ip", ip);
                return (int)proc.ExecuteScalar();
            }
        }

        public void Delete(int itemId)
        {
            using (var con = new SqlConnection(SQLHelper.ConnectionString))
            {
                var proc = con.CreateCommand();
                proc.CommandType = System.Data.CommandType.StoredProcedure;
                proc.CommandText = "Searches_Delete";
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

        public Searches GetById(int itemId)
        {
            using (var con = new SqlConnection(SQLHelper.ConnectionString))
            {
                var proc = con.CreateCommand();
                proc.CommandType = System.Data.CommandType.StoredProcedure;
                proc.CommandText = "Searches_GetById";
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

        public int Update(Searches s)
        {
            using (var con = new SqlConnection(SQLHelper.ConnectionString))
            {
                var proc = con.CreateCommand();
                proc.CommandType = System.Data.CommandType.StoredProcedure;
                proc.CommandText = "Searches_Update";
                con.Open();
                proc.Parameters.AddWithValue("@Id", s.Id);
                proc.Parameters.AddWithValue("@Keywords", s.Keywords);
                proc.Parameters.AddWithValue("@Date", s.Date);
                proc.Parameters.AddWithValue("@Ip", s.Ip);
                return (int)proc.ExecuteScalar();
            }
        }

        public List<Searches> GetAll()
        {
            using (var con = new SqlConnection(SQLHelper.ConnectionString))
            {
                var proc = con.CreateCommand();
                proc.CommandType = System.Data.CommandType.StoredProcedure;
                proc.CommandText = "Searches_GetAll";
                con.Open();
                List<Searches> list = null;
                using (var reader = proc.ExecuteReader())
                {
                    list = new List<Searches>();
                    while (reader.Read())
                    {
                        list.Add(FromReader(reader));
                    }
                }
                return list;
            }
        }

        public Searches GetLastEntry()
        {
            using (var con = new SqlConnection(SQLHelper.ConnectionString))
            {
                var proc = con.CreateCommand();
                proc.CommandType = System.Data.CommandType.StoredProcedure;
                proc.CommandText = "Searches_GetLastEntry";
                con.Open();
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
    }

    public interface ISearchesRepository
    {
        int Create(String keywords, DateTime date, String ip);
        void Delete(int itemId);
        Searches GetById(int itemId);
        int Update(Searches s);
        List<Searches> GetAll();
        Searches GetLastEntry();
    }
}
