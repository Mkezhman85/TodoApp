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

    public interface IDataRepository
    {
        long CreateGroup(Groups group);
        void Delete(int id);
        Groups GetGroupById(int id);
        List<Groups> GetGroups();
        void Update(Groups group);

        List<User> GetUsers();
        User CreateNewUser(User user);
        User GetUserById(long id);

        List<User> GetUsersMulti();

    }
}
