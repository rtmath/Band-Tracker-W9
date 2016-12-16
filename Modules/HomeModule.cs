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
    }
  }
}
