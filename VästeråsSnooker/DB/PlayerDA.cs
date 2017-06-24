using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using VästeråsSnooker.Models.DataModels;

namespace VästeråsSnooker.DB
{
    public class PlayerDA : IPlayerDA
    {
        private string conString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
        
        public int AddPlayerToDb(IPlayer player)
        {
            string sql = "INSERT INTO dbo.Players(Name) output INSERTED.Id VALUES(@Name)";
            
            int playerId = -1;

            using(SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@Name", player.Name);
                try
                {
                    con.Open();
                    var result = cmd.ExecuteScalar();
                    playerId = int.Parse(result.ToString());
                }

                catch (Exception e)
                {
                    return -1;
                }
            }
            return playerId;
        }

        public List<IPlayer> GetAllPlayers()
        {
            string sql = "SELECT * FROM dbo.Players";
            DataTable results = new DataTable();
            List<IPlayer> allPlayers = new List<IPlayer>();
            
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                using (var da = new SqlDataAdapter(sql, con))
                {
                    da.Fill(results);
                }
                con.Close();
            }

            foreach (DataRow row in results.Rows)
            {
                allPlayers.Add(new Player()
                {
                    Id = int.Parse(row["Id"].ToString()),
                    Name = row["Name"].ToString()
                });
            }

            return allPlayers;
        }

        public IPlayer GetPlayerById (int id)
        {
            string sql = "SELECT * FROM dbo.Players WHERE Id = @id";
            var player = new Player();

            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@id", id);
                con.Open();
                var rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    player.Id = int.Parse(rdr["Id"].ToString());
                    player.Name = rdr["Name"].ToString();
                };
            }

            return player;
        }
    }
}