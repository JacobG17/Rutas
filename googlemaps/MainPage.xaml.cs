using Maui.GoogleMaps;
using Microsoft.Maui.Controls;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json.Linq;

namespace googlemaps;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();

        string LocatSucursal1 = "25.559250120053928, -108.48454969917489";
        string LocatSucursal2 = "25.587407592495648, -108.47479466848748";
        string LocatSucursal3 = "25.54888223308781, -108.45367655887655";

        pines();
        areas();

        peticionCoordenada(LocatSucursal1, LocatSucursal2);
        peticionCoordenada(LocatSucursal2, LocatSucursal3);
        peticionCoordenada(LocatSucursal3, LocatSucursal1);
    }

    public void pines()
    {
        //Creacion de un Pin con sus parametros para mostrar en el mapa
        Pin sucursal1 = new Pin
        {
            Label = "Sucursal 1",
            Type = PinType.Place,
            Position = new Position(25.559250120053928, -108.48454969917489)
        };


        Pin sucursal2 = new Pin
        {
            Label = "Sucursal 2",
            Type = PinType.Place,
            Position = new Position(25.587407592495648, -108.47479466848748)
        };


        Pin sucursal3 = new Pin
        {
            Label = "Sucursal 3",
            Type = PinType.Place,
            Position = new Position(25.54888223308781, -108.45367655887655)
        };
        //Agregamos el Pin al mapa
        myMap.Pins.Add(sucursal1);
        myMap.Pins.Add(sucursal2);
        myMap.Pins.Add(sucursal3);
    }

    public void areas()
    {
        //Creacion del circulo para resaltar el area del Pin
        Circle area1 = new Circle
        {
            Center = new Position(25.559250120053928, -108.48454969917489),
            Radius = new Distance(250),
            StrokeColor = Color.FromArgb("#88FF0000"),
            StrokeWidth = 1,
            FillColor = Color.FromArgb("#88FFC0CB")
        };

        Circle area2 = new Circle
        {
            Center = new Position(25.587407592495648, -108.47479466848748),
            Radius = new Distance(250),
            StrokeColor = Color.FromArgb("#88FF0000"),
            StrokeWidth = 1,
            FillColor = Color.FromArgb("#88FFC0CB")
        };

        Circle area3 = new Circle
        {
            Center = new Position(25.54888223308781, -108.45367655887655),
            Radius = new Distance(250),
            StrokeColor = Color.FromArgb("#88FF0000"),
            StrokeWidth = 1,
            FillColor = Color.FromArgb("#88FFC0CB")
        };

        //Agregar el area del circulo en el mapa
        myMap.Circles.Add(area1);
        myMap.Circles.Add(area2);
        myMap.Circles.Add(area3);
    }

    public void peticionCoordenada(string ori, string dest)
    {
        List<Position> direccion = new List<Position>();

        Polyline ruta = new Polyline();

        var client = new HttpClient();

        client.DefaultRequestHeaders.Clear();

        var url = $"https://api.mymappi.com/v2/directions/route/car?apikey=64b813a5-d787-426f-af39-64ee8f0f35a7&orig={ori}&dest={dest}&alternatives=false&steps=true&geometries=polyline&overview=simplified";

        var response = client.GetAsync(url).Result;

        var res = response.Content.ReadAsStringAsync().Result;

        // Convertir el archivo JSON en un objeto JObject
        var obj = JObject.Parse(res);

        var locations = obj["data"]["routes"][0]["legs"][0]["steps"]
            .SelectMany(step => step["intersections"]
                .Select(intersection => new {
                    Lat = intersection["location"]["lat"],
                    Lon = intersection["location"]["lon"]
                }))
            .ToList();

        foreach (var location in locations)
        {
            Console.WriteLine($"{location.Lat}\t{location.Lon}");
            var latitud = double.Parse($"{location.Lat}");
            var longitud = double.Parse($"{location.Lon}");
            direccion.Add((new Position(latitud, longitud)));
        }

        foreach (Position punto in direccion)
        {
            ruta.Positions.Add(punto);
        }

        ruta.StrokeColor = Colors.Blue;
        ruta.StrokeWidth = 5;

        myMap.Polylines.Add(ruta);

    }
}



