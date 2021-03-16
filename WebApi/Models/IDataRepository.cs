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
        long CreateGroup(Group group);
        void Delete(int id);
        Group GetGroupById(int id);
        List<Group> GetGroups();
        void Update(Group group);

        List<User> GetUsers();
        User CreateNewUser(User user);
        User GetUserById(long id);

    }
}
