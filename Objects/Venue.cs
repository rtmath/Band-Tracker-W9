using System;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace BandTracker.Objects
{
    public class Venue
    {
        public int Id {get; set;}
        public string Name {get; set;}

        public Venue (string name, int id = 0)
        {
            this.Id = id;
            this.Name = name;
        }

        public override bool Equals(System.Object otherVenue)
        {
            if (!(otherVenue is Venue))
            {
                return false;
            }
            else
            {
                Venue newVenue = (Venue)otherVenue;
                bool checkId = (this.Id == newVenue.Id);
                bool checkName = (this.Name == newVenue.Name);
                return (checkId && checkName);
            }
        }

        public override int GetHashCode()
        {
            return this.Name.GetHashCode();
        }

        public static List<Venue> GetAll()
        {
            List<Venue> allVenues = new List<Venue>{};

            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM venues;", conn);
            SqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                int venueId = rdr.GetInt32(0);
                string venueName = rdr.GetString(1);
                Venue newVenue = new Venue(venueName, venueId);
                allVenues.Add(newVenue);
            }

            if (rdr != null)
            {
                rdr.Close();
            }
            if (conn != null)
            {
                conn.Close();
            }

            return allVenues;
        }

        public void Save()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("INSERT INTO venues (name) OUTPUT INSERTED.id VALUES(@VenueName);", conn);
            SqlParameter nameParam = new SqlParameter("@VenueName", this.Name);
            cmd.Parameters.Add(nameParam);

            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                this.Id = rdr.GetInt32(0);
            }

            if (rdr != null)
            {
                rdr.Close();
            }
            if (conn != null)
            {
                conn.Close();
            }
        }

        public static Venue Find(int id)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM venues WHERE id = @VenueId;", conn);
            SqlParameter idParam = new SqlParameter("@VenueId", id.ToString());
            cmd.Parameters.Add(idParam);

            SqlDataReader rdr = cmd.ExecuteReader();

            int foundId = 0;
            string foundName = "";

            while (rdr.Read())
            {
                foundId = rdr.GetInt32(0);
                foundName = rdr.GetString(1);
            }
            if (rdr != null)
            {
                rdr.Close();
            }
            if (conn != null)
            {
                conn.Close();
            }
            Venue foundVenue = new Venue(foundName, foundId);
            return foundVenue;
        }

        public void Update(string newName)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("UPDATE venues SET name = @NewName OUTPUT INSERTED.name WHERE id = @ThisId;", conn);
            SqlParameter nameParam = new SqlParameter("@NewName", newName);
            SqlParameter idParam = new SqlParameter("@ThisId", this.Id);
            cmd.Parameters.Add(nameParam);
            cmd.Parameters.Add(idParam);

            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                this.Name = rdr.GetString(0);
            }
            if (rdr != null)
            {
                rdr.Close();
            }
            if (conn != null)
            {
                conn.Close();
            }
        }

        public static void Delete(int id)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("DELETE FROM venues WHERE id = @VenueId;", conn);
            SqlParameter idParam = new SqlParameter("@VenueId", id);
            cmd.Parameters.Add(idParam);
            cmd.ExecuteNonQuery();
            if (conn != null)
            {
                conn.Close();
            }
        }

        public static void DeleteAll()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("DELETE FROM venues;", conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
    }
}
