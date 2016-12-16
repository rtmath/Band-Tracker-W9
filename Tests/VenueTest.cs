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
    public void Test_DBReturnsEmptyAtFirst()
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

    public void Dispose()
    {

    }
  }
}
