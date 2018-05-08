using System;
using System.Collections.Generic;
using System.Text;
using DBModels;
using System.Data.SqlClient;

namespace Database
{
   public class HtmlTypesRepository :IHtmlTypesRepository
    {
        private HtmlTypes fromReader(SqlDataReader reader)
        {
            return new HtmlTypes
            {
                Id = (int)reader["Id"],
                Name = reader["Name"].ToString()
            };
        }

        public int Create(HtmlTypes htmlTypes)
        {
            using (var con = new SqlConnection(SQLHelper.ConnectionString)) {
                var cmd = con.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "HtmlTypes_Create";

                con.Open();
                cmd.Parameters.AddWithValue("@Name", htmlTypes.Name);
                return (int)cmd.ExecuteScalar();
            }
        }

        public void Delete(int id)
        {
            using (var con = new SqlConnection(SQLHelper.ConnectionString))
            {
                var cmd = con.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "HtmlTypes_Delete";
                con.Open();
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
                
            }
        }

        public List<HtmlTypes> GetAll()
        {
            using (var con = new SqlConnection(SQLHelper.ConnectionString))
            {
                var cmd = con.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "HtmlTypes_GetAll";
                con.Open();
                List <HtmlTypes> list = null;
                using (var reader = cmd.ExecuteReader()) 
                {
                    list = new List<HtmlTypes>();
                    while (reader.Read())
                    {
                        list.Add(fromReader(reader));
                    }
                }
                return list;
            }

        }

        public HtmlTypes GetById(int id)
        {
            using (var con = new SqlConnection(SQLHelper.ConnectionString))
            {
                var cmd = con.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "HtmlTypes_GetById";
                con.Open();
                cmd.Parameters.AddWithValue("@Id", id);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                        return fromReader(reader);
                    else
                        return null;
                }

            }
        }

        public int Update(HtmlTypes htmlTypes)
        {
            using (var con = new SqlConnection(SQLHelper.ConnectionString))
            {
                var cmd = con.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "HtmlTypes_Update";
                con.Open();
                cmd.Parameters.AddWithValue("@Id", htmlTypes.Id);
                cmd.Parameters.AddWithValue("@Name", htmlTypes.Name);
                return (int)cmd.ExecuteScalar();
            }
        }

    }

    public interface IHtmlTypesRepository
    {
        int Create(HtmlTypes htmlTypes);
        void Delete(int id);
        HtmlTypes GetById(int id);
        int Update(HtmlTypes htmlTypes);
        List<HtmlTypes> GetAll();

    }
}
