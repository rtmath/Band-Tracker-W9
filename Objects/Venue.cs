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
  }
}
