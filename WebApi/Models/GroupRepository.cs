using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Npgsql;

namespace WebApi.Models
{

    public interface IGroupRepository
    {
        JsonResult CreateGroup(Group group);
        void Delete(int id);
        Group get(int id);
        List<Group> GetGroups();
        void Update(Group group);
    }

    public class GroupRepository : IGroupRepository
    {
        string connectionString = null;
        
        public GroupRepository(string conn)
        {
            connectionString = conn;
        }


        public List<Group> GetGroups()
        {
            using (IDbConnection db = new NpgsqlConnection(connectionString))
            {
                string sqlGroupList = "select * from public.\"group\";";
                return db.Query<Group>(sqlGroupList).ToList();
            }
        }

        public JsonResult CreateGroup([FromBody] Group group)
        {
            try
            {
                using (IDbConnection db = new NpgsqlConnection(connectionString))
                {
                    string sqlQuery = "Insert into public.\"group\"(name, description, parent_id)" +
                        "Values(@Name, @Description, @ParentId)";
                    int rowsAffected = db.Execute(sqlQuery, group);                    
                }
                
            }
            catch
            {
                throw;
            }

            return new JsonResult(group);
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Group get(int id)
        {
            throw new NotImplementedException();
        }

        

        public void Update(Group group)
        {
            throw new NotImplementedException();
        }
    }
}
