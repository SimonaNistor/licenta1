using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using DBModels;

namespace Database
{
    public class CMSDetailsRepository : ICMSDetailsRepository
    {
        private CMSDetails FromReader(SqlDataReader sqlDataReader)
        {
            return new CMSDetails
            {
                Id = (int)sqlDataReader["Id"],
                CMSId=(int)sqlDataReader["CMSId"],
                Name = sqlDataReader["Name"].ToString(),
                OrderIndex = (int)sqlDataReader["OrderIndex"],
                HtmlTypeId = (int)sqlDataReader["HtmlTypeId"],
                Content = sqlDataReader["Content"].ToString()
            };
        }

        public int Create(CMSDetails cmsdetails)
        {
            using (var con = new SqlConnection(SQLHelper.ConnectionString))
            {
                var proc = con.CreateCommand();
                proc.CommandType = System.Data.CommandType.StoredProcedure;
                proc.CommandText = "CMSDetails_Create";
                con.Open();
                proc.Parameters.AddWithValue("@CMSId", cmsdetails.CMSId);
                proc.Parameters.AddWithValue("@Name", cmsdetails.Name);
                proc.Parameters.AddWithValue("@OrderIndex", cmsdetails.OrderIndex);
                proc.Parameters.AddWithValue("@HtmlTypeId", cmsdetails.HtmlTypeId);
                proc.Parameters.AddWithValue("@Content", cmsdetails.Content);
                return (int)proc.ExecuteScalar();
            }
        }

        public void Delete(int id)
        {
            using (var con = new SqlConnection(SQLHelper.ConnectionString))
            {
                var proc = con.CreateCommand();
                proc.CommandType = System.Data.CommandType.StoredProcedure;
                proc.CommandText = "CMSDetails_Delete";
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

        public CMSDetails GetById(int id)
        {
            using (var con = new SqlConnection(SQLHelper.ConnectionString))
            {
                var proc = con.CreateCommand();
                proc.CommandType = System.Data.CommandType.StoredProcedure;
                proc.CommandText = "CMSDetails_GetById";
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

        public int Update(CMSDetails cmsdetails)
        {
            using (var con = new SqlConnection(SQLHelper.ConnectionString))
            {
                var proc = con.CreateCommand();
                proc.CommandType = System.Data.CommandType.StoredProcedure;
                proc.CommandText = "CMSDetails_Update";
                con.Open();
                proc.Parameters.AddWithValue("@Id", cmsdetails.Id);
                proc.Parameters.AddWithValue("@CMSId", cmsdetails.CMSId);
              
                proc.Parameters.AddWithValue("@Name", cmsdetails.Name);
                proc.Parameters.AddWithValue("@OrderIndex", cmsdetails.OrderIndex);
                proc.Parameters.AddWithValue("@HtmlTypeId", cmsdetails.HtmlTypeId);
                proc.Parameters.AddWithValue("@Content", cmsdetails.Content);
                return (int)proc.ExecuteScalar();
            }
        }

        public List<CMSDetails> GetAll()
        {
            using (var con = new SqlConnection(SQLHelper.ConnectionString))
            {
                var proc = con.CreateCommand();
                proc.CommandType = System.Data.CommandType.StoredProcedure;
                proc.CommandText = "CMSDetails_GetAll";
                con.Open();
                List<CMSDetails> list = null;
                using (var reader = proc.ExecuteReader())
                {
                    list = new List<CMSDetails>();
                    while (reader.Read())
                    {
                        list.Add(FromReader(reader));
                    }
                }
                return list;
            }
        }

        public List<CMSDetails> GetByCMSId(int id)
        {
            using (var con = new SqlConnection(SQLHelper.ConnectionString))
            {
                var proc = con.CreateCommand();
                proc.CommandType = System.Data.CommandType.StoredProcedure;
                proc.CommandText = "CMSDetails_GetByCMSId";
                con.Open();
                proc.Parameters.AddWithValue("@CMSId", id);
                List<CMSDetails> lista = new List<CMSDetails>();
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
    }

    public interface ICMSDetailsRepository
    {
        int Create(CMSDetails cmsdetails);
        void Delete(int id);
        CMSDetails GetById(int id);
        int Update(CMSDetails cmsdetails);
        List<CMSDetails> GetAll();
        List<CMSDetails> GetByCMSId(int id);
    }
}
