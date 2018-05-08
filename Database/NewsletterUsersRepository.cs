using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using DBModels;

namespace Database
{
    public class NewsletterUsersRepository: INewsletterUsersRepository
    {
        private NewsletterUsers FromReader(SqlDataReader sqlDataReader)
        {
            return new NewsletterUsers
            {
                Id = (int)sqlDataReader["Id"],
                Email = sqlDataReader["Email"].ToString(),
                Date = (DateTime)sqlDataReader["Date"],
                Ip = sqlDataReader["Ip"].ToString()
            };
        }

        public int Create(string email, DateTime date, string ip)
        {
            using (var con = new SqlConnection(SQLHelper.ConnectionString))
            {
                var proc = con.CreateCommand();
                proc.CommandType = System.Data.CommandType.StoredProcedure;
                proc.CommandText = "NewsletterUsers_Create";
                con.Open();
                proc.Parameters.AddWithValue("@Email", email);
                proc.Parameters.AddWithValue("@Date", date);
                proc.Parameters.AddWithValue("@Ip", ip);
                return (int)proc.ExecuteScalar();
            }
        }

        public void Delete(int userId)
        {
            using (var con = new SqlConnection(SQLHelper.ConnectionString))
            {
                var proc = con.CreateCommand();
                proc.CommandType = System.Data.CommandType.StoredProcedure;
                proc.CommandText = "NewsletterUsers_Delete";
                con.Open();
                proc.Parameters.AddWithValue("@Id", userId);
                using (var reader = proc.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        proc.ExecuteScalar();
                    }
                }
            }
        }

        public NewsletterUsers GetById(int userId)
        {
            using (var con = new SqlConnection(SQLHelper.ConnectionString))
            {
                var proc = con.CreateCommand();
                proc.CommandType = System.Data.CommandType.StoredProcedure;
                proc.CommandText = "NewsletterUsers_GetById";
                con.Open();
                proc.Parameters.AddWithValue("@Id", userId);
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

        public NewsletterUsers GetByEmail(string email)
        {
            using (var con = new SqlConnection(SQLHelper.ConnectionString))
            {
                var proc = con.CreateCommand();
                proc.CommandType = System.Data.CommandType.StoredProcedure;
                proc.CommandText = "NewsletterUsers_GetByEmail";
                con.Open();
                proc.Parameters.AddWithValue("@Email", email);
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

        public List<NewsletterUsers> GetAll()
        {
            using (var con = new SqlConnection(SQLHelper.ConnectionString))
            {
                var proc = con.CreateCommand();
                proc.CommandType = System.Data.CommandType.StoredProcedure;
                proc.CommandText = "NewsletterUsers_GetAll";
                con.Open();
                List<NewsletterUsers> list = null;
                using (var reader = proc.ExecuteReader())
                {
                    list = new List<NewsletterUsers>();
                    while (reader.Read())
                    {
                        list.Add(FromReader(reader));
                    }
                }
                return list;
            }
        }
    }

    public interface INewsletterUsersRepository
    {
        int Create(string email,DateTime date,string ip);
        void Delete(int userId);
        NewsletterUsers GetById(int userId);
        List<NewsletterUsers> GetAll();
        NewsletterUsers GetByEmail(string email);
    }
}
