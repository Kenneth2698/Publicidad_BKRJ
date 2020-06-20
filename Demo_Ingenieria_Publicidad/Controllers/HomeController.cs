using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Demo_Ingenieria_Publicidad.Models;
using System.Net;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http;

namespace Demo_Ingenieria_Publicidad.Controllers
{
    public class HomeController : Controller
    {
        private static readonly HttpClient HttpClient = new HttpClient();
        private string ruta = "https://demo-investigacion.azurewebsites.net/";

        /////
        ///////https://localhost:44302/



        public IActionResult Index()
        {

            return View();
        }


        public async Task<HttpStatusCode> PostToExternal()
        {
            Publicidad p = new Publicidad("PublicidadAZURE", "DescripcionPublicidadAZURE", "/assets/img/promociones/kfc.jpg", "www.google.com");

            var serializedRequest = JsonConvert.SerializeObject(p);

            var requestBody = new System.Net.Http.StringContent(serializedRequest);
            requestBody.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            using (var response = await HttpClient.PostAsync(ruta + "RegistrarPublicidad", requestBody))
            {

                
                return response.StatusCode;
            }
        }

        [HttpPost]
        public IActionResult MostrarRegistrarUsuario()
        {

            return View("RegistroUsuario");
        }

        [HttpPost]
        public async Task<IActionResult> RegistrarUsuario(string usuario, string contra, string correo)
        {

            var serializedRequest = JsonConvert.SerializeObject(new Usuario(usuario, contra, correo));

            var requestBody = new System.Net.Http.StringContent(serializedRequest);
            requestBody.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            using (var response = await HttpClient.PostAsync(ruta + "RegistrarUsuario", requestBody))
            {

            }

            return View("Index");

        }

        [HttpPost]
        public async Task<IActionResult> ValidarUsuario(String usuario, String contra)
        {

            var serializedRequest = JsonConvert.SerializeObject(new Usuario(usuario, contra));

            var requestBody = new System.Net.Http.StringContent(serializedRequest);
            requestBody.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            Respuesta respuesta;
            using (var response = await HttpClient.PostAsync(ruta + "ValidarUsuario", requestBody))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                respuesta = JsonConvert.DeserializeObject<Respuesta>(apiResponse);
            }

            if (respuesta.ID == 0)
            {
                return View("Index");
            }
            else
            {
                return View("RegistrarPublicidad", respuesta);
            }

        }

        [HttpPost]
        public async Task<IActionResult> RegistrarPublicidad(Publicidad publicidad, int usuarioid)
        {
            var serializedRequest = JsonConvert.SerializeObject(publicidad);

            var requestBody = new System.Net.Http.StringContent(serializedRequest);
            requestBody.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            Respuesta respuesta = new Respuesta();
            using (var response = await HttpClient.PostAsync(ruta + "RegistrarPublicidad", requestBody))
            {

            }
            respuesta.ID = publicidad.Usuarioid;
            respuesta.Mensaje = "Solicitado correctamente";
            return View("RegistrarPublicidad", respuesta);
        }
    }
}
