using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace XamarinGeolocalizacion
{
    public class DatosPais
    {
        public string Nombre { get; set; }
        public string Capital { get; set; }
        public string Continente { get; set; }
        public string ExtensionGeo { get; set; }
        public string Idiomas { get; set; }
        public string Monedas { get; set; }
    }
}