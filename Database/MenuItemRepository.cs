using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using DBModels;

namespace Database
{
    
    public class MenuItemRepository: IMenuItemRepository
    {
        private MenuItem FromReader(SqlDataReader sqlDataReader)
        {
            return new MenuItem {
                ItemId = (int)sqlDataReader["ItemId"],
                Name = sqlDataReader["Name"].ToString(),
                OrderIndex = (int)sqlDataReader["OrderIndex"],
                Section = sqlDataReader["Section"].ToString(),
                IsVisible = (Boolean)sqlDataReader["IsVisible"]
            };
            
        }

        public int Create(MenuItem menuItem)
        {
            using (var con = new SqlConnection(SQLHelper.ConnectionString))
            {
                var proc = con.CreateCommand();
                proc.CommandType = System.Data.CommandType.StoredProcedure;
                proc.CommandText = "MenuItems_Create";
                con.Open();
                proc.Parameters.AddWithValue("@Name", menuItem.Name);
                proc.Parameters.AddWithValue("@OrderIndex", menuItem.OrderIndex);
                proc.Parameters.AddWithValue("@Section", menuItem.Section);
                proc.Parameters.AddWithValue("@IsVisible", menuItem.IsVisible);
                return (int)proc.ExecuteScalar();
            }
        }

        public void Delete(int itemId)
        {
            using (var con = new SqlConnection(SQLHelper.ConnectionString))
            {
                var proc = con.CreateCommand();
                proc.CommandType = System.Data.CommandType.StoredProcedure;
                proc.CommandText = "MenuItems_Delete";
                con.Open();
                proc.Parameters.AddWithValue("@ItemId", itemId);
                using (var reader = proc.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        proc.ExecuteScalar();
                    }
                }

            }
        }

        public MenuItem GetById(int itemId)
        {
            using (var con = new SqlConnection(SQLHelper.ConnectionString))
            {
                var proc = con.CreateCommand();
                proc.CommandType = System.Data.CommandType.StoredProcedure;
                proc.CommandText = "MenuItems_GetById";
                con.Open();
                proc.Parameters.AddWithValue("@ItemId", itemId);
                using (var reader = proc.ExecuteReader()) {
                    if (reader.Read()) {
                        return FromReader(reader);
                    }
                    else
                    {
                        return null;
                    }
                }
                
            }
        }

        public int Update(MenuItem menuItem)
        {
            using (var con = new SqlConnection(SQLHelper.ConnectionString))
            {
                var proc = con.CreateCommand();
                proc.CommandType = System.Data.CommandType.StoredProcedure;
                proc.CommandText = "MenuItems_Update";
                con.Open();
                proc.Parameters.AddWithValue("@ItemId", menuItem.ItemId);
                proc.Parameters.AddWithValue("@Name", menuItem.Name);
                proc.Parameters.AddWithValue("@OrderIndex", menuItem.OrderIndex);
                proc.Parameters.AddWithValue("@Section", menuItem.Section);
                proc.Parameters.AddWithValue("@IsVisible", menuItem.IsVisible);
                return (int)proc.ExecuteScalar();
            }
        }

        public List<MenuItem> GetAll()
        {
            using (var con = new SqlConnection(SQLHelper.ConnectionString))
            {
                var proc = con.CreateCommand();
                proc.CommandType = System.Data.CommandType.StoredProcedure;
                proc.CommandText = "dbo.MenuItems_GetAll";
                con.Open();
                List<MenuItem> list = null;
                using (var reader = proc.ExecuteReader())
                {
                    list = new List<MenuItem>();
                    while (reader.Read())
                    {
                        list.Add(FromReader(reader));
                    }
                }
                return list;
            }
        }

        public List<MenuItem> GetAllSortedAndVisible()
        {
            using (var con = new SqlConnection(SQLHelper.ConnectionString))
            {
                var proc = con.CreateCommand();
                proc.CommandType = System.Data.CommandType.StoredProcedure;
                proc.CommandText = "MenuItems_GetAllSortedAndVisible";
                con.Open();
                List<MenuItem> list = null;
                using (var reader = proc.ExecuteReader())
                {
                    list = new List<MenuItem>();
                    while (reader.Read())
                    {
                        list.Add(FromReader(reader));
                    }
                }
                return list;
            }
        }

        
    }
    public interface IMenuItemRepository
    {        
        int Create(MenuItem menuItem);
        void Delete(int itemId);
        MenuItem GetById(int itemId);
        int Update(MenuItem menuItem);
        List<MenuItem> GetAll();
        List<MenuItem> GetAllSortedAndVisible();

    }
}
