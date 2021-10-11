using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using UserManagement_Service.Models;

namespace UserManagement_Service.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserMgmtController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public UserMgmtController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("getProfile")]
        public IActionResult GetUserProfile([FromQuery] string emailId)
        {
            if (string.IsNullOrWhiteSpace(emailId))
            {
                return BadRequest(new Exception("Email Id is empty"));
            }
            string query = " select UserName, UserEmailId, Age, PhoneNumber,badgeIds,genres from usr_mgmt.UserInfo where UserEmailId like @emailId and inUse = @inUse";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration["DbReadConnectionString"].ToString();
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query,mycon))
                {
                    myCommand.Parameters.AddWithValue("@emailId", emailId);
                    myCommand.Parameters.AddWithValue("@inUse", 1);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }
            return Ok(table);
        }

        [HttpPost("addProfile")]
        public IActionResult AddUserProfile(UserInfo user)
        {
            if (string.IsNullOrWhiteSpace(user.UserEmailAddress) || string.IsNullOrWhiteSpace(user.UserName))
            {
                return BadRequest(new Exception("Email Id or Username is empty"));
            }
            string query = "insert into usr_mgmt.UserInfo(UserName, UserEmailId, Age, PhoneNumber,genres, inUse, crt_ts, crt_user) values(@userName, @emailId, @age, @phoneNumber , @genres, @inUse, @currentUtcDate, @CurrentUser) ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration["DbWriteConnectionString"].ToString();
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@userName",user.UserName);
                    myCommand.Parameters.AddWithValue("@emailId", user.UserEmailAddress);
                    myCommand.Parameters.AddWithValue("@age", user.Age);
                    myCommand.Parameters.AddWithValue("@phoneNumber", user.PhoneNumber);
                    myCommand.Parameters.AddWithValue("@genres", user.Genres);
                    myCommand.Parameters.AddWithValue("@inUse", 1);
                    myCommand.Parameters.AddWithValue("@currentUtcDate", DateTime.UtcNow);
                    myCommand.Parameters.AddWithValue("@CurrentUser", "User API");
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }
            return Created("{0} profile has been created", user.UserName);
        }

        [HttpPut("modifyProfile")]
        public IActionResult ModifyUserProfile(UserInfo user)
        {
            if (string.IsNullOrWhiteSpace(user.UserEmailAddress) || string.IsNullOrWhiteSpace(user.UserName))
            {
                return BadRequest(new Exception("Email Id or Username is empty"));
            }
            string query = "update usr_mgmt.UserInfo SET Age = @age,PhoneNumber = @phoneNumber, genres = @genres, badgeIds = @badgeIds, updt_ts= @updateTS ,updt_user = @updateUser where UserEmailId = @emailId";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration["DbWriteConnectionString"].ToString();
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@age", user.Age);
                    myCommand.Parameters.AddWithValue("@phoneNumber", user.PhoneNumber);
                    myCommand.Parameters.AddWithValue("@genres", user.Genres);
                    myCommand.Parameters.AddWithValue("@badgeIds", user.BadgeIds);
                    myCommand.Parameters.AddWithValue("@updateTS", DateTime.UtcNow);
                    myCommand.Parameters.AddWithValue("@updateUser", "User API");
                    myCommand.Parameters.AddWithValue("@emailId", user.UserEmailAddress);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }
            return Ok( user.UserName +" profile has been modified");
        }

        [HttpDelete("deleteProfile")]
        public IActionResult DeleteUserProfile([FromQuery] string emailId)
        {
            if (string.IsNullOrWhiteSpace(emailId))
            {
                return BadRequest(new Exception("Email Id or Username is empty"));
            }
            string query = "update usr_mgmt.UserInfo SET inUse = @inUse,updt_ts= @updateTS ,updt_user = @updateUser where UserEmailId = @emailId";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration["DbWriteConnectionString"].ToString();
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@inUse", 0);
                    myCommand.Parameters.AddWithValue("@updateTS", DateTime.UtcNow);
                    myCommand.Parameters.AddWithValue("@updateUser", "User API");
                    myCommand.Parameters.AddWithValue("@emailId", emailId);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }
            return Ok(emailId + " profile has been deleted");
        }
    }
}