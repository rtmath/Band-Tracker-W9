using Xunit;
using System;
using System.Data;
using System.Collections.Generic;
using BandTracker.Startup;
using BandTracker.Objects;

namespace BandTracker.Tests
{
  public class BandTests : IDisposable
  {
    public void BandTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=band_tracker_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void Test_DatabaseReturnsEmptyAtFirst()
    {
      Assert.Equal(0, Band.GetAll().Count);
    }

    [Fact]
    public void Test_Equals_ComparesTwoObjects()
    {
      Band testBand = new Band("ACDC");
      Band testBand2 = new Band("ACDC");

      Assert.Equal(testBand, testBand2);
    }

    [Fact]
    public void Test_SavesBandToDatabase()
    {
        Band testBand = new Band("Led Zeppelin");
        testBand.Save();

        List<Band> result = Band.GetAll();
        List<Band> testList = new List<Band>{testBand};

        Assert.Equal(testList, result);
    }

    [Fact]
    public void Test_FindBandInDatabase()
    {
        Band newBand = new Band("Def Leopard");
        newBand.Save();

        Band foundBand = Band.Find(newBand.Id);

        Assert.Equal(newBand, foundBand);
    }

    [Fact]
    public void Test_RetrievesVenuesFromBand()
    {
        Band testBand = new Band("Black Dahlia Murder");
        testBand.Save();
        Venue testVenue1 = new Venue("Roseland Theater");
        testVenue1.Save();
        Venue testVenue2 = new Venue("Wonder Ballroom");
        testVenue2.Save();

        testBand.AddVenue(testVenue1);
        testBand.AddVenue(testVenue2);
        List<Venue> expected = new List<Venue> { testVenue1, testVenue2 };
        List<Venue> actual = testBand.GetVenues();

        Assert.Equal(expected, actual);
    }

    public void Dispose()
    {
        Band.DeleteAll();
        Venue.DeleteAll();
    }
  }
}
