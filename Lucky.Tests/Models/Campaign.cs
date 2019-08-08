using System;
using System.Collections.Generic;
using System.Text;

namespace Lucky.Tests.Models
{
    public class Campaign
    {
        public string Company { get; set; }
        public Location Location { get; set; }
        public Audience Audience { get; set; }
        public ImageSize ImageSize { get; set; }
        public string src { get; set; }
    }

    public class Location
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Radius { get; set; } // miles
    }

    public class Audience
    {
        public Gender Gender { get; set; }
        public int MinAge { get; set; } // inclusive
        public int MaxAge { get; set; } // inclusive
    }

    public class ImageSize
    {
        public int Height { get; set; }
        public int Width { get; set; }
    }

    public enum Gender
    {
        Female,
        Male
    }
}
