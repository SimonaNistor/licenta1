using DBModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Database
{
    public class LinksRepository : ILinksRepository
    {
        private Links FromReader(SqlDataReader sqlDataReader)
        {
            return new Links
            {
                id = (int)sqlDataReader["id"],
                limbaj = sqlDataReader["limbaj"].ToString(),
                tags = sqlDataReader["tags"].ToString(),
                link = sqlDataReader["link"].ToString()
            };
        }

        public int Create(Links link)
        {
            using (var con = new SqlConnection(SQLHelper.ConnectionString))
            {
                var proc = con.CreateCommand();
                proc.CommandType = System.Data.CommandType.StoredProcedure;
                proc.CommandText = "Links_Create";
                con.Open();
                proc.Parameters.AddWithValue("@limbaj", link.limbaj);
                proc.Parameters.AddWithValue("@tags", link.tags);
                proc.Parameters.AddWithValue("@link", link.link);
                return (int)proc.ExecuteScalar();
            }
        }

        public void Delete(int id)
        {
            using (var con = new SqlConnection(SQLHelper.ConnectionString))
            {
                var proc = con.CreateCommand();
                proc.CommandType = System.Data.CommandType.StoredProcedure;
                proc.CommandText = "Links_Delete";
                con.Open();
                proc.Parameters.AddWithValue("@id", id);
                using (var reader = proc.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        proc.ExecuteScalar();
                    }
                }

            }
        }

        public Links GetById(int id)
        {
            using (var con = new SqlConnection(SQLHelper.ConnectionString))
            {
                var proc = con.CreateCommand();
                proc.CommandType = System.Data.CommandType.StoredProcedure;
                proc.CommandText = "Links_GetById";
                con.Open();
                proc.Parameters.AddWithValue("@id", id);
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

        public List<Links> GetByLanguage(string language)
        {
            using (var con = new SqlConnection(SQLHelper.ConnectionString))
            {
                var proc = con.CreateCommand();
                proc.CommandType = System.Data.CommandType.StoredProcedure;
                proc.CommandText = "Links_GetByLanguage";
                con.Open();
                proc.Parameters.AddWithValue("@limbaj", language);
                List<Links> list = null;
                using (var reader = proc.ExecuteReader())
                {
                    list = new List<Links>();
                    while (reader.Read())
                    {
                        list.Add(FromReader(reader));
                    }
                }
                return list;

            }
        }

        public int Update(Links link)
        {
            using (var con = new SqlConnection(SQLHelper.ConnectionString))
            {
                var proc = con.CreateCommand();
                proc.CommandType = System.Data.CommandType.StoredProcedure;
                proc.CommandText = "Links_Update";
                con.Open();
                proc.Parameters.AddWithValue("@id", link.id);
                proc.Parameters.AddWithValue("@limbaj", link.limbaj);
                proc.Parameters.AddWithValue("@tags", link.tags);
                proc.Parameters.AddWithValue("@link", link.link);
                return (int)proc.ExecuteScalar();
            }
        }

        public List<Links> GetAll()
        {
            using (var con = new SqlConnection(SQLHelper.ConnectionString))
            {
                var proc = con.CreateCommand();
                proc.CommandType = System.Data.CommandType.StoredProcedure;
                proc.CommandText = "Links_GetAll";
                con.Open();
                List<Links> list = null;
                using (var reader = proc.ExecuteReader())
                {
                    list = new List<Links>();
                    while (reader.Read())
                    {
                        list.Add(FromReader(reader));
                    }
                }
                return list;
            }
        }
    }

    public interface ILinksRepository
    {
        int Create(Links link);
        void Delete(int id);
        Links GetById(int id);
        List<Links> GetByLanguage(string language);
        int Update(Links link);
        List<Links> GetAll();
    }
}
