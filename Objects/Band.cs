using System;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace BandTracker.Objects
{
    public class Band
    {
        public int Id {get; set;}
        public string Name {get; set;}

        public Band (string name, int id = 0)
        {
            this.Id = id;
            this.Name = name;
        }

        public override bool Equals(System.Object otherBand)
        {
            if (!(otherBand is Band))
            {
                return false;
            }
            else
            {
                Band newBand = (Band)otherBand;
                bool checkId = (this.Id == newBand.Id);
                bool checkName = (this.Name == newBand.Name);
                return (checkId && checkName);
            }
        }

        public override int GetHashCode()
        {
            return this.Name.GetHashCode();
        }

        public static List<Band> GetAll()
        {
            List<Band> allBands = new List<Band>{};

            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM bands;", conn);
            SqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                int bandId = rdr.GetInt32(0);
                string bandName = rdr.GetString(1);
                Band newBand = new Band(bandName, bandId);
                allBands.Add(newBand);
            }

            if (rdr != null)
            {
                rdr.Close();
            }
            if (conn != null)
            {
                conn.Close();
            }

            return allBands;
        }

        public void Save()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("INSERT INTO bands (name) OUTPUT INSERTED.id VALUES(@BandName);", conn);
            SqlParameter nameParam = new SqlParameter("@BandName", this.Name);
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

        public static Band Find(int id)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM bands WHERE id = @BandId;", conn);
            SqlParameter idParam = new SqlParameter("@BandId", id.ToString());
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
            Band foundBand = new Band(foundName, foundId);
            return foundBand;
        }

        public void AddVenue(Venue venue)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("INSERT INTO bands_venues (band_id, venue_id) VALUES (@BandId, @VenueId);", conn);
            SqlParameter bandParam = new SqlParameter("@BandId", this.Id);
            SqlParameter venueParam = new SqlParameter("@VenueId", venue.Id);
            cmd.Parameters.Add(bandParam);
            cmd.Parameters.Add(venueParam);
            cmd.ExecuteNonQuery();
        }

        public List<Venue> GetVenues()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT venues.* FROM bands_venues JOIN venues ON (bands_venues.venue_id = venues.id) WHERE bands_venues.band_id = @BandId;", conn);
            SqlParameter idParam = new SqlParameter("@BandId", this.Id);
            cmd.Parameters.Add(idParam);

            List<Venue> returnedVenues = new List<Venue>{};
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                int id = rdr.GetInt32(0);
                string name = rdr.GetString(1);
                Venue result = new Venue(name, id);
                returnedVenues.Add(result);
            }

            if (rdr != null)
            {
                rdr.Close();
            }
            if (conn != null)
            {
                conn.Close();
            }
            return returnedVenues;
        }

        public static void Delete(int id)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("DELETE FROM bands WHERE id = @BandId;", conn);
            SqlParameter idParam = new SqlParameter("@BandId", id);
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
            SqlCommand cmd = new SqlCommand("DELETE FROM bands;", conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
    }
}
