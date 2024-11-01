using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Data.SqlClient;
using Dapper;
using System.Threading.Tasks;
using test_maleki.Entities;

namespace test_maleki.Dapper
{
    public class DapperRepository: IDapperRepository
    {
        private static string connectionString = "Server=DESKTOP-SJNEDPF\\SQLEXPRESS;Database=Cw 10;User Id=sa;Password=Nimak1640;TrustServerCertificate=True;";
        public List<User> GetUsers()
        {

            using var cn = new SqlConnection(connectionString);
            string sql = $"Select * from dbo.Userss";
            var cmd = new CommandDefinition(sql);
            var result = cn.Query<User>(cmd);
            return result.ToList();
        }
        public void AddUser(User user)
        {
            using var cn = new SqlConnection(connectionString);
            string sql = "INSERT INTO dbo.Userss (Username, Password, Status) VALUES (@Username, @Password, @Status)";
            var cmd = new CommandDefinition(sql, new { user.Username, user.Password, user.Status });
            cn.Execute(cmd);
        }
        public void UpdateUser(User oldUser, User newUser)
        {
            using var cn = new SqlConnection(connectionString);


            string sql = @"UPDATE dbo.Userss
                   SET Username = @NewUsername, 
                       Password = @NewPassword, 
                       Status = @NewStatus
                   WHERE Username = @OldUsername 
                     AND Password = @OldPassword 
                     AND Status = @OldStatus";


            var parameters = new
            {
                NewUsername = newUser.Username,
                NewPassword = newUser.Password,
                NewStatus = newUser.Status,
                OldUsername = oldUser.Username,
                OldPassword = oldUser.Password,
                OldStatus = oldUser.Status
            };


            var cmd = new CommandDefinition(sql, parameters);


            cn.Execute(cmd);
        }
    }
}
