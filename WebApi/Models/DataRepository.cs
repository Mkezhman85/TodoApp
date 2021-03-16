using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace WebApi.Models
{
    public class DataRepository : IDataRepository
    {
        string connectionString = null;

        public DataRepository(string conn)
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

        public long CreateGroup(Group group)
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

            return group.Id;
        }


        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Group GetGroupById(int id)
        {
            throw new NotImplementedException();
        }



        public void Update(Group group)
        {
            throw new NotImplementedException();
        }


        public User CreateNewUser(User user)
        {
            try
            {
                long result = default;
                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("login", user.Login);
                parameters.Add("isBlocked", user.IsBlocked);
                parameters.Add("requiredPasswordChange", user.RequiredPasswordChange);
                parameters.Add("wrongLoginCount", user.WrongLoginCount);
                parameters.Add("beginDate", user.BeginDate);
                parameters.Add("endDate", user.EndDate);
                parameters.Add("lastLoginDate", user.LastLoginDate);


                parameters.Add("id", result, null, ParameterDirection.InputOutput);

                
                using (IDbConnection db = new NpgsqlConnection(connectionString))
                {
                    string sqlQuery = "Insert into public.\"user\"(login, is_blocked, required_password_change, wrong_login_count, begin_date, end_date, last_login_date)" +
                        "Values(:login, :isBlocked, :requiredPasswordChange, :wrongLoginCount, :beginDate, :endDate, :lastLoginDate)";

                    int rowsAffected = db.Execute(sqlQuery, user);
                    result = parameters.Get<long>("id");
                    return GetUserById(result);
                }

            }
            catch
            {
                 throw;
            }
        }

        public User GetUserById(long id)

        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("id", id);
            using (IDbConnection db = new NpgsqlConnection(connectionString))
            {
                string sqlQuery = "SELECT id, login, is_blocked, required_password_change, wrong_login_count, begin_date, end_date, last_login_date " +
                    "FROM public.\"user\" where id = :id";
                return db.Query<User>(sqlQuery, parameters).FirstOrDefault();                                
            }
        }


        //[HttpGet("{id}")]
        //[Route("GetUserById")]
        //public async Task<ActionResult<User>> GetUserById(int id)
        //{
        //    var userItem = await _context.user.FindAsync(id);

        //    if (userItem == null)
        //    {
        //        return NotFound();
        //    }

        //    return userItem;
        //}

        public List<User> GetUsers()
        {
            throw new NotImplementedException();
        }
    }
}
