using System;
using System.Json;
using System.Net;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Widget;
using Android.OS;
using Android.Util;
using Java.IO;
using Plugin.Geolocator;
using Console = System.Console;


namespace XamarinGeolocalizacion
{
    [Activity(Label = "XamarinGeolocalizacion", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);
            // Set our view from the "main" layout resource

            var datos = obtenerReferencia();

        }

        public async Task<DatosPais> obtenerReferencia()
        {
            var georef = new GeoReferenciacion();
            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 50;

            if (!locator.IsGeolocationAvailable || !locator.IsGeolocationEnabled)
            {
                var intent = new Intent(Android.Provider.Settings.ActionLocationSourceSettings);
                StartActivity(intent);
            }
            var position = await locator.GetPositionAsync(10000);
            var pais = FindViewById<TextView>(Resource.Id.textViewPais);
            var capital = FindViewById<TextView>(Resource.Id.textViewCapital);
            var continente = FindViewById<TextView>(Resource.Id.textViewContinente);
            var extGeo = FindViewById<TextView>(Resource.Id.textViewExtGeo);
            var idiomas = FindViewById<TextView>(Resource.Id.textViewIdioma);
            var moneda = FindViewById<TextView>(Resource.Id.textViewMoneda);
            var data = await georef.ObtenerDatos(position.Latitude, position.Longitude);
            try
            {
                
                if (data != null)
                {
                    pais.Text = data.Nombre;
                    capital.Text = data.Capital;
                    continente.Text = data.Continente;
                    extGeo.Text = data.ExtensionGeo;
                    idiomas.Text = data.Idiomas;
                    moneda.Text = data.Monedas;
                }
            }
            catch (Exception)
            {
                pais.Text = "Error con la georeferenciación";
            }
            return data;
        }

    }
}

