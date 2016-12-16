using Xunit;
using System;
using System.Data;
using System.Collections.Generic;
using BandTracker.Startup;
using BandTracker.Objects;

namespace BandTracker.Tests
{
  public class VenueTests : IDisposable
  {
    public void VenueTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=band_tracker_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void Test_DatabaseReturnsEmptyAtFirst()
    {
      Assert.Equal(0, Venue.GetAll().Count);
    }

    [Fact]
    public void Test_Equals_ComparesTwoObjects()
    {
      Venue testVenue = new Venue("Hawthorne Theater");
      Venue testVenue2 = new Venue("Hawthorne Theater");

      Assert.Equal(testVenue, testVenue2);
    }

    [Fact]
    public void Test_SavesVenueToDatabase()
    {
        Venue testVenue = new Venue("Wonder Ballroom");
        testVenue.Save();

        List<Venue> result = Venue.GetAll();
        List<Venue> testList = new List<Venue>{testVenue};

        Assert.Equal(testList, result);
    }

    [Fact]
    public void Test_FindVenueInDatabase()
    {
        Venue newVenue = new Venue("Test Venue");
        newVenue.Save();

        Venue foundVenue = Venue.Find(newVenue.Id);

        Assert.Equal(newVenue, foundVenue);
    }

    [Fact]
    public void Test_UpdateVenueInDatabase()
    {
        Venue newVenue = new Venue("Vew Nenue");
        newVenue.Save();

        string newName = "New Venue";
        newVenue.Update(newName);
        string result = newVenue.Name;

        Assert.Equal(newName, result);
    }

    public void Dispose()
    {
        Venue.DeleteAll();
    }
  }
}
