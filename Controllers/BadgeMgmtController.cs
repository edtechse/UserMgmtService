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
    public class BadgeMgmtController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public BadgeMgmtController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("getBadgeList")]
        public IActionResult GetBadgeList()
        {
            string query = " select BadgeName, BadgeDescription from usr_mgmt.BadgeInfo";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration["DbReadConnectionString"].ToString();
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }
            return Ok(table);
        }

        [HttpPost("addBadge")]
        public IActionResult AddBadge(Badge badge)
        {
            if (string.IsNullOrWhiteSpace(badge.BadgeName) || string.IsNullOrWhiteSpace(badge.BadgeDescription))
            {
                return BadRequest(new Exception("Badge details are empty"));
            }
            string query = "insert into BadgeInfo(BadgeName,BadgeDescription,crt_ts,crt_user) values (@badgeName,@badgeDescription,@currentUtcDate,@CurrentUser) ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration["DbWriteConnectionString"].ToString();
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@badgeName", badge.BadgeName);
                    myCommand.Parameters.AddWithValue("@badgeDescription", badge.BadgeDescription);
                    myCommand.Parameters.AddWithValue("@currentUtcDate", DateTime.UtcNow);
                    myCommand.Parameters.AddWithValue("@CurrentUser", "User API");
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }
            return Created("{0} badge has been created", badge.BadgeName);
        }

        [HttpPut("modifyBadge")]
        public IActionResult ModifyBadge(Badge badge)
        {
            if (string.IsNullOrWhiteSpace(badge.BadgeName) || string.IsNullOrWhiteSpace(badge.BadgeDescription))
            {
                return BadRequest(new Exception("Badge details are empty"));
            }
            string query = "update usr_mgmt.BadgeInfo SET BadgeDescription = @badgeDescription, updt_ts = @updateTs, updt_user = @updateUsr where BadgeName = @BadgeName "; 
            DataTable table = new DataTable();
            string sqlDataSource = _configuration["DbWriteConnectionString"].ToString();
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@badgeDescription", badge.BadgeDescription);
                    myCommand.Parameters.AddWithValue("@updateTs", DateTime.UtcNow);
                    myCommand.Parameters.AddWithValue("@updateUsr", "User API");
                    myCommand.Parameters.AddWithValue("@badgeName", badge.BadgeName);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }
            return Ok(badge.BadgeName +" badge has been modified" );
        }

        [HttpDelete("deleteBadge")]
        public IActionResult DeleteUserProfile([FromQuery] string BadgeName)
        {
            if (string.IsNullOrWhiteSpace(BadgeName))
            {
                return BadRequest(new Exception("Email Id or Username is empty"));
            }
            string query = "delete from usr_mgmt.BadgeInfo where BadgeName = @BadgeName";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration["DbWriteConnectionString"].ToString();
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@badgeName", BadgeName);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }
            return Ok(BadgeName + " badge has been deleted");
        }
    }
}