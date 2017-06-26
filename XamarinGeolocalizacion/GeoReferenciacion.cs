using System;
using System.Collections.Generic;
using System.Json;
using System.Linq;
using System.Net;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Threading.Tasks;
using Plugin.Geolocator;

namespace XamarinGeolocalizacion
{
    public class GeoReferenciacion
    {
        
        public async Task<DatosPais> ObtenerDatos(double latitude, double longitude)
        {
            string url = "http://api.geonames.org/countryCodeJSON?lat=" +
                  latitude + "&lng=" + longitude + "&username=camilobu19";
            try
            {
                var pais = await ObtenerPais(url);
                if (!string.IsNullOrEmpty(pais))
                {
                    url = "http://api.geonames.org/countryInfoJSON?country=" +
                  pais + "&username=camilobu19";
                    return await ObtenerDatosPais(url);
                }
                else
                {
                    return new DatosPais() { Nombre = "No se logro definir el pais" };
                }

            }
            catch (Exception)
            {
                return new DatosPais() { Nombre = "Geolocalizacion no lograda" };
            }

        }

        public async Task<String> ObtenerPais(string url)
        {
            try
            {
                var pais = await ConsumirServicio(url);
                return pais["countryCode"];
            }
            catch (Exception)
            {
                return string.Empty;
                throw;
            }
        }

        public async Task<DatosPais> ObtenerDatosPais(string url)
        {
            try
            {
                var datos = await ConsumirServicio(url);
                var datosPais = new DatosPais();
                datosPais.Capital = datos["geonames"][0]["capital"];
                datosPais.Continente = datos["geonames"][0]["continentName"];
                datosPais.ExtensionGeo = datos["geonames"][0]["population"];
                datosPais.Idiomas = datos["geonames"][0]["languages"];
                datosPais.Monedas = datos["geonames"][0]["currencyCode"];
                datosPais.Nombre = datos["geonames"][0]["countryName"];
                return datosPais;
            }
            catch (Exception)
            {
                return default(DatosPais);
            }
        }


        private async Task<JsonValue> ConsumirServicio(string url)
        {
            // Create an HTTP web request using the URL:
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new System.Uri(url));
            request.ContentType = "application/json";
            request.Method = "GET";

            // Send the request to the server and wait for the response:
            using (WebResponse response = await request.GetResponseAsync())
            {
                // Get a stream representation of the HTTP web response:
                using (System.IO.Stream stream = response.GetResponseStream())
                {
                    // Use this stream to build a JSON document object:
                    JsonValue jsonDoc = await Task.Run(() => JsonObject.Load(stream));
                    return jsonDoc;
                }
            }
        }
    }
}