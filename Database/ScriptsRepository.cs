using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using DBModels;

namespace Database
{
    public class ScriptsRepository:IScriptsRepository
    {
        private Scripts FromReader(SqlDataReader sqlDataReader)
        {
            return new Scripts
            {
                Id = (int)sqlDataReader["Id"],
                Name = sqlDataReader["Name"].ToString(),
                Script = sqlDataReader["Script"].ToString(),
                Header = (bool)sqlDataReader["Header"],
                Footer = (bool)sqlDataReader["Footer"]
            };
        }

        public int Create(Scripts script)
        {
            using (var con = new SqlConnection(SQLHelper.ConnectionString))
            {
                var proc = con.CreateCommand();
                proc.CommandType = System.Data.CommandType.StoredProcedure;
                proc.CommandText = "Scripts_Create";
                con.Open();
                proc.Parameters.AddWithValue("@Name", script.Name);
                proc.Parameters.AddWithValue("@Script", script.Script);
                proc.Parameters.AddWithValue("@Header", script.Header);
                proc.Parameters.AddWithValue("@Footer", script.Footer);
                return (int)proc.ExecuteScalar();
            }
        }

        public void Delete(int id)
        {
            using (var con = new SqlConnection(SQLHelper.ConnectionString))
            {
                var proc = con.CreateCommand();
                proc.CommandType = System.Data.CommandType.StoredProcedure;
                proc.CommandText = "Scripts_Delete";
                con.Open();
                proc.Parameters.AddWithValue("@Id", id);
                using (var reader = proc.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        proc.ExecuteScalar();
                    }
                }

            }
        }

        public Scripts GetById(int id)
        {
            using (var con = new SqlConnection(SQLHelper.ConnectionString))
            {
                var proc = con.CreateCommand();
                proc.CommandType = System.Data.CommandType.StoredProcedure;
                proc.CommandText = "Scripts_GetById";
                con.Open();
                proc.Parameters.AddWithValue("@Id", id);
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

        public int Update(Scripts script)
        {
            using (var con = new SqlConnection(SQLHelper.ConnectionString))
            {
                var proc = con.CreateCommand();
                proc.CommandType = System.Data.CommandType.StoredProcedure;
                proc.CommandText = "Scripts_Update";
                con.Open();
                proc.Parameters.AddWithValue("@Id", script.Id);
                proc.Parameters.AddWithValue("@Name", script.Name);
                proc.Parameters.AddWithValue("@Script", script.Script);
                proc.Parameters.AddWithValue("@Header", script.Header);
                proc.Parameters.AddWithValue("@Footer", script.Footer);
                return (int)proc.ExecuteScalar();
            }
        }

        public List<Scripts> GetAll()
        {
            using (var con = new SqlConnection(SQLHelper.ConnectionString))
            {
                var proc = con.CreateCommand();
                proc.CommandType = System.Data.CommandType.StoredProcedure;
                proc.CommandText = "Scripts_GetAll";
                con.Open();
                List<Scripts> list = null;
                using (var reader = proc.ExecuteReader())
                {
                    list = new List<Scripts>();
                    while (reader.Read())
                    {
                        list.Add(FromReader(reader));
                    }
                }
                return list;
            }
        }

        public List<Scripts> GetHeaders()
        {
            using (var con = new SqlConnection(SQLHelper.ConnectionString))
            {
                var proc = con.CreateCommand();
                proc.CommandType = System.Data.CommandType.StoredProcedure;
                proc.CommandText = "Scripts_GetHeaders";
                con.Open();
                List<Scripts> list = null;
                using (var reader = proc.ExecuteReader())
                {
                    list = new List<Scripts>();
                    while (reader.Read())
                    {
                        list.Add(FromReader(reader));
                    }
                }
                return list;
            }
        }

        public List<Scripts> GetFooters()
        {
            using (var con = new SqlConnection(SQLHelper.ConnectionString))
            {
                var proc = con.CreateCommand();
                proc.CommandType = System.Data.CommandType.StoredProcedure;
                proc.CommandText = "Scripts_GetFooters";
                con.Open();
                List<Scripts> list = null;
                using (var reader = proc.ExecuteReader())
                {
                    list = new List<Scripts>();
                    while (reader.Read())
                    {
                        list.Add(FromReader(reader));
                    }
                }
                return list;
            }
        }
    }

    public interface IScriptsRepository
    {
        int Create(Scripts script);
        void Delete(int id);
        Scripts GetById(int id);
        int Update(Scripts script);
        List<Scripts> GetAll();
        List<Scripts> GetHeaders();
        List<Scripts> GetFooters();
    }

}
