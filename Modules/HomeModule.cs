using Nancy;
using System.Collections.Generic;
using BandTracker.Objects;

namespace BandTracker.Modules
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      Get["/"] = _ => View["index.cshtml"];
      Get["/band/add"] = _ =>
      {
          return View["new_band.cshtml"];
      };
      Get["/venue/add"] = _ =>
      {
          return View["new_venue.cshtml"];
      };
      Post["/band/add"] = _ =>
      {
          Band newBand = new Band(Request.Form["new-band"]);
          newBand.Save();
          return View["index.cshtml"];
      };
      Post["/venue/add"] = _ =>
      {
          Venue newVenue = new Venue(Request.Form["new-venue"]);
          newVenue.Save();
          return View["index.cshtml"];
      };
      Get["/band/view"] = _ =>
      {
          List<Band> allBands = Band.GetAll();
          return View["/view_bands.cshtml", allBands];
      };

      Get["/venue/view"] = _ =>
      {
          List<Venue> allVenues = Venue.GetAll();
          return View["/view_venues.cshtml", allVenues];
      };


      Get["/band/{id}"] = parameters =>
      {
          Dictionary<string, object> myDict = new Dictionary<string, object>{};
          Band currentBand = Band.Find(parameters.id);
          myDict.Add("band", currentBand);
          List<Venue> bandVenues = currentBand.GetVenues();
          myDict.Add("venues", bandVenues);
          return View["band_details.cshtml", myDict];
      };

      Get["/venue/{id}"] = parameters =>
      {
          Dictionary<string, object> myDict = new Dictionary<string, object>{};
          Venue currentVenue = Venue.Find(parameters.id);
          myDict.Add("venue", currentVenue);
          List<Band> venueBands = currentVenue.GetBands();
          myDict.Add("bands", venueBands);
          return View["venue_details.cshtml", myDict];
      };

      Get["/band/add/venue/{id}"] = parameters =>
      {
          Dictionary<string, object> myDict = new Dictionary<string, object>{};
          Band newBand = Band.Find(parameters.id);
          myDict.Add("band", newBand);
          List<Venue> allVenues = Venue.GetAll();
          myDict.Add("venues", allVenues);
          return View["assign_venue.cshtml", myDict];
      };

      Post["/band/add/venue/{id}"] = parameters =>
      {
          Band currentBand = Band.Find(parameters.id);
          Venue selectedVenue = Venue.Find(Request.Form["new-venue"]);
          currentBand.AddVenue(selectedVenue);
          Dictionary<string, object> myDict = new Dictionary<string, object>{};
          myDict.Add("band", currentBand);
          List<Venue> bandVenues = currentBand.GetVenues();
          myDict.Add("venues", bandVenues);
          return View["band_details.cshtml", myDict];
      };

      Get["/venue/add/band/{id}"] = parameters =>
      {
          Dictionary<string, object> myDict = new Dictionary<string, object>{};
          Venue newVenue = Venue.Find(parameters.id);
          myDict.Add("venue", newVenue);
          List<Band> allBands = Band.GetAll();
          myDict.Add("bands", allBands);
          return View["assign_band.cshtml", myDict];
      };

      Post["/venue/add/band/{id}"] = parameters =>
      {
          Venue currentVenue = Venue.Find(parameters.id);
          Band selectedBand = Band.Find(Request.Form["new-band"]);
          currentVenue.AddBand(selectedBand);
          Dictionary<string, object> myDict = new Dictionary<string, object>{};
          myDict.Add("venue", currentVenue);
          List<Band> venueBands = currentVenue.GetBands();
          myDict.Add("bands", venueBands);
          return View["venue_details.cshtml", myDict];
      };
    }
  }
}
