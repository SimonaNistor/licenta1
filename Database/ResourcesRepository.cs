using DBModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Database
{
    public class ResourcesRepository : IResourceRepository
    {
        private Resources FromReader(SqlDataReader sqlDataReader)
        {
            return new Resources
            {
                Id = (int)sqlDataReader["Id"],
                Code = sqlDataReader["Code"].ToString(),
                Type = sqlDataReader["Type"].ToString()
            };

        }

        public int Create(Resources res)
        {
            using (var con = new SqlConnection(SQLHelper.ConnectionString))
            {
                var proc = con.CreateCommand();
                proc.CommandType = System.Data.CommandType.StoredProcedure;
                proc.CommandText = "Resources_Create";
                con.Open();
                proc.Parameters.AddWithValue("@Code", res.Code);
                proc.Parameters.AddWithValue("@Type", res.Type);
                return (int)proc.ExecuteScalar();
            }
        }

        public void Delete(int itemId)
        {
            using (var con = new SqlConnection(SQLHelper.ConnectionString))
            {
                var proc = con.CreateCommand();
                proc.CommandType = System.Data.CommandType.StoredProcedure;
                proc.CommandText = "Resources_Delete";
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

        public Resources GetById(int itemId)
        {
            using (var con = new SqlConnection(SQLHelper.ConnectionString))
            {
                var proc = con.CreateCommand();
                proc.CommandType = System.Data.CommandType.StoredProcedure;
                proc.CommandText = "Resources_GetById";
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

        public int Update(Resources res)
        {
            using (var con = new SqlConnection(SQLHelper.ConnectionString))
            {
                var proc = con.CreateCommand();
                proc.CommandType = System.Data.CommandType.StoredProcedure;
                proc.CommandText = "Resources_Update";
                con.Open();
                proc.Parameters.AddWithValue("@Id", res.Id);
                proc.Parameters.AddWithValue("@Code", res.Code);
                proc.Parameters.AddWithValue("@Type", res.Type);
                return (int)proc.ExecuteScalar();
            }
        }

        public List<Resources> GetAll()
        {
            using (var con = new SqlConnection(SQLHelper.ConnectionString))
            {
                var proc = con.CreateCommand();
                proc.CommandType = System.Data.CommandType.StoredProcedure;
                proc.CommandText = "Resources_GetAll";
                con.Open();
                List<Resources> list = null;
                using (var reader = proc.ExecuteReader())
                {
                    list = new List<Resources>();
                    while (reader.Read())
                    {
                        list.Add(FromReader(reader));
                    }
                }
                return list;
            }
        }

        public List<Resources> GetByType(string type)
        {
            using (var con = new SqlConnection(SQLHelper.ConnectionString))
            {
                var proc = con.CreateCommand();
                proc.CommandType = System.Data.CommandType.StoredProcedure;
                proc.CommandText = "Resources_GetByType";
                con.Open();
                proc.Parameters.AddWithValue("@Type", type);
                List<Resources> list = null;
                using (var reader = proc.ExecuteReader())
                {
                    list = new List<Resources>();
                    while (reader.Read())
                    {
                        list.Add(FromReader(reader));
                    }
                }
                return list;

            }
        }
    }

    public interface IResourceRepository
    {
        int Create(Resources res);
        void Delete(int itemId);
        Resources GetById(int itemId);
        int Update(Resources res);
        List<Resources> GetAll();
        List<Resources> GetByType(string type);
    }
}
