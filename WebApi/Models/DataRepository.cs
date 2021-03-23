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

        // Список пользователей
        public List<User> GetUsers()
        {
            string sqlUserList = $@"SELECT id, login, is_blocked, required_password_change, wrong_login_count, begin_date, end_date, last_login_date FROM users";
            using (IDbConnection db = new NpgsqlConnection(connectionString))
            {
                List<User> result = db.Query<User>(sqlUserList).ToList();
                return result;
            }
        }

        // Добавление пользователя
        public User CreateNewUser(User user)
        {
            try
            {
                long result = 0;
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("login", user.Login);
                parameters.Add("isBlocked", user.IsBlocked);
                parameters.Add("requiredPasswordChange", user.RequiredPasswordChange);
                parameters.Add("wrongLoginCount", user.WrongLoginCount);
                parameters.Add("beginDate", user.BeginDate);
                parameters.Add("endDate", user.EndDate);
                parameters.Add("lastLoginDate", user.LastLoginDate);
                parameters.Add("id", result, null, ParameterDirection.InputOutput);

                string sqlQuery = $@"INSERT INTO users
                    (login, is_blocked, required_password_change, wrong_login_count, begin_date, end_date, last_login_date)
                    Values
                    (:login, :isBlocked, :requiredPasswordChange, :wrongLoginCount, :beginDate, :endDate, :lastLoginDate)
                    returning id";


                using (IDbConnection db = new NpgsqlConnection(connectionString))
                {                    
                    int rowsAffected = db.Execute(sqlQuery, user);
                    //result = parameters.Get<long>("id");
                    result = (long)db.ExecuteScalar(sqlQuery, user);
                    return GetUserById(result);
                }

            }
            catch(Exception e)
            {
                throw;
            }
        }

        // Пользователь по id
        public User GetUserById(long id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("id", id);
            using (IDbConnection db = new NpgsqlConnection(connectionString))
            {
                string sqlQuery = $@"SELECT id, login, is_blocked, required_password_change, wrong_login_count, begin_date, end_date, last_login_date
                                    FROM users where id = :id";
                return db.Query<User>(sqlQuery, parameters).FirstOrDefault();
            }
        }

        // Список групп
        public List<Group> GetGroups()
        {
            string sqlGroupList = "select * from public.\"group\";";
            using (IDbConnection db = new NpgsqlConnection(connectionString))
            {                
                return db.Query<Group>(sqlGroupList).ToList();
            }
        }

        // Добавление группы
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


        
    }
}
