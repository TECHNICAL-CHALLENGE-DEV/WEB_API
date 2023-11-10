using System;
using System.Collections.Generic;

namespace Cirkula.API.Data.Models;

public partial class Store
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string BannerUrl { get; set; } = null!;

    public double Latitude { get; set; }

    public double Longitude { get; set; }

    public TimeSpan OpenTime { get; set; }

    public TimeSpan CloseTime { get; set; }
}
