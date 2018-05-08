using DBModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Database
{
    public class ContactFormRepository : IContactFormRepository
    {
        private ContactForm FromReader(SqlDataReader sqlDataReader)
        {
            return new ContactForm
            {
                Id = (int)sqlDataReader["Id"],
                FirstName = sqlDataReader["FirstName"].ToString(),
                LastName = sqlDataReader["LastName"].ToString(),
                EmailAddress = sqlDataReader["EmailAddress"].ToString(),
                Message = sqlDataReader["Message"].ToString()

            };
        }

        public int Create(ContactForm contactForm)
        {
            using (var con = new SqlConnection(SQLHelper.ConnectionString))
            {
                var proc = con.CreateCommand();
                proc.CommandType = System.Data.CommandType.StoredProcedure;
                proc.CommandText = "ContactForm_Create";
                con.Open();
                proc.Parameters.AddWithValue("@FirstName", contactForm.FirstName);
                proc.Parameters.AddWithValue("@LastName", contactForm.LastName);
                proc.Parameters.AddWithValue("@EmailAddress", contactForm.EmailAddress);
                proc.Parameters.AddWithValue("@Message", contactForm.Message);
                return (int)proc.ExecuteScalar();               
            }
        }

        public void Delete (int Id)
        {
            using (var con = new SqlConnection(SQLHelper.ConnectionString))
            {
                var proc = con.CreateCommand();
                proc.CommandType = System.Data.CommandType.StoredProcedure;
                proc.CommandText = "ContactForm_Delete";
                con.Open();
                proc.Parameters.AddWithValue("@Id", Id);
                using (var reader = proc.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        proc.ExecuteScalar();
                    }
                }
}
        }
        public List<ContactForm> GetAll()
        {
            using (var con = new SqlConnection(SQLHelper.ConnectionString))
            {
                var proc = con.CreateCommand();
                proc.CommandType = System.Data.CommandType.StoredProcedure;
                proc.CommandText = "ContactForm_GetAll";
                con.Open();
                List<ContactForm> list = null;
                using (var reader = proc.ExecuteReader())
                {
                    list = new List<ContactForm>();
                    while (reader.Read())
                    {
                        list.Add(FromReader(reader));
                    }
                }
                return list;
            }
        }

        public ContactForm GetById(int Id)
        {
            using (var con = new SqlConnection(SQLHelper.ConnectionString))
            {
                var proc = con.CreateCommand();
                proc.CommandType = System.Data.CommandType.StoredProcedure;
                proc.CommandText = "ContactForm_GetById";
                con.Open();
                proc.Parameters.AddWithValue("@Id", Id);
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

        public int Update (ContactForm contactForm)
        {
            using (var con = new SqlConnection(SQLHelper.ConnectionString))
            {
                var proc = con.CreateCommand();
                proc.CommandType = System.Data.CommandType.StoredProcedure;
                proc.CommandText = "ContactForm_Update";
                con.Open();
                proc.Parameters.AddWithValue("@Id", contactForm.Id);
                proc.Parameters.AddWithValue("@FirstName", contactForm.FirstName);
                proc.Parameters.AddWithValue("@LastName", contactForm.LastName);
                proc.Parameters.AddWithValue("@EmailAddress", contactForm.EmailAddress);
                proc.Parameters.AddWithValue("@Message", contactForm.Message);
                return (int)proc.ExecuteScalar();
            }
        }



    }


    public interface IContactFormRepository {
        int Create(ContactForm contactForm);
        void Delete(int Id);
        ContactForm GetById(int Id);
        int Update(ContactForm contactForm);
        List<ContactForm> GetAll();
        
    }
}
