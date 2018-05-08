using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using DBModels;

namespace Database
{
    public class CMSRepository : ICMSRepository
    {
        private CMS FromReader(SqlDataReader sqlDataReader)
        {
            return new CMS
            {
                Id = (int)sqlDataReader["Id"],
                Name = sqlDataReader["Name"].ToString(),
                MenuItemId = (int)sqlDataReader["MenuItemId"],
                HtmlType = (int)sqlDataReader["HtmlType"],
                Content = sqlDataReader["Content"].ToString()
            };
        }

        public int Create(CMS cms)
        {
            using (var con = new SqlConnection(SQLHelper.ConnectionString))
            {
                var proc = con.CreateCommand();
                proc.CommandType = System.Data.CommandType.StoredProcedure;
                proc.CommandText = "CMS_Create";
                con.Open();
                proc.Parameters.AddWithValue("@Name", cms.Name);
                proc.Parameters.AddWithValue("@MenuItemId", cms.MenuItemId);
                proc.Parameters.AddWithValue("@HtmlType", cms.HtmlType);
                proc.Parameters.AddWithValue("@Content", cms.Content);
                return (int)proc.ExecuteScalar();
            }
        }

        public void Delete(int id)
        {
            using (var con = new SqlConnection(SQLHelper.ConnectionString))
            {
                var proc = con.CreateCommand();
                proc.CommandType = System.Data.CommandType.StoredProcedure;
                proc.CommandText = "CMS_Delete";
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

        public CMS GetById(int id)
        {
            using (var con = new SqlConnection(SQLHelper.ConnectionString))
            {
                var proc = con.CreateCommand();
                proc.CommandType = System.Data.CommandType.StoredProcedure;
                proc.CommandText = "CMS_GetById";
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

        public List<CMS> GetByMenuItemId(int id)
        {
            using (var con = new SqlConnection(SQLHelper.ConnectionString))
            {
                var proc = con.CreateCommand();
                proc.CommandType = System.Data.CommandType.StoredProcedure;
                proc.CommandText = "CMS_GetByMenuItemId";
                con.Open();
                proc.Parameters.AddWithValue("@Id", id);
                List<CMS> list = null;
                using (var reader = proc.ExecuteReader())
                {
                    list = new List<CMS>();
                    while (reader.Read())
                    {
                        list.Add(FromReader(reader));
                    }
                }
                return list;

            }
        }

        public int Update(CMS cms)
        {
            using (var con = new SqlConnection(SQLHelper.ConnectionString))
            {
                var proc = con.CreateCommand();
                proc.CommandType = System.Data.CommandType.StoredProcedure;
                proc.CommandText = "CMS_Update";
                con.Open();
                proc.Parameters.AddWithValue("@Id", cms.Id);
                proc.Parameters.AddWithValue("@Name", cms.Name);
                proc.Parameters.AddWithValue("@MenuItemId", cms.MenuItemId);
                proc.Parameters.AddWithValue("@HtmlType", cms.HtmlType);
                proc.Parameters.AddWithValue("@Content", cms.Content);
                return (int)proc.ExecuteScalar();
            }
        }

        public List<CMS> GetAll()
        {
            using (var con = new SqlConnection(SQLHelper.ConnectionString))
            {
                var proc = con.CreateCommand();
                proc.CommandType = System.Data.CommandType.StoredProcedure;
                proc.CommandText = "CMS_GetAll";
                con.Open();
                List<CMS> list = null;
                using (var reader = proc.ExecuteReader())
                {
                    list = new List<CMS>();
                    while (reader.Read())
                    {
                        list.Add(FromReader(reader));
                    }
                }
                return list;
            }
        }
    }

    public interface ICMSRepository
    {
        int Create(CMS cms);
        void Delete(int id);
        CMS GetById(int id);
        int Update(CMS cms);
        List<CMS> GetAll();
        List<CMS> GetByMenuItemId(int id);
    }
}
