using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace LojaCarrosV2.Api.Controllers
{
    public class OlxController : Controller
    {
        private readonly string urlBase;
        private readonly string client_id;
        private readonly string client_secret;
        private readonly string contaTeste;
        private readonly string senha;
        private readonly string redirect_uri;
        public OlxController()
        {
            urlBase = "https://auth.olx.com.br/oauth";
            client_id = "f78b32a2df237759e36b8e9f078bd991c16687e4";
            client_secret = "adb6804fa6e15e5d880dd54b16843628";
            contaTeste = "teste-davi@bol.com.br";
            senha = "teste123456";
            redirect_uri = "http://www.painel.enriqueautomoveis.com.br/";
        }
        // GET: Olx
        public ActionResult Index()
        {
            //POST /oauth/token HTTP/1.1
            var postData = string.Empty;
            postData += "Host: auth.olx.com.br";
            postData += "ContentType:";
            postData += "application/xwwwformurlencoded";
            postData += "code=4/P7q7W91aoMsCeLvIaQm6bTrgtp7&";
            postData += "client_id=" + client_id + "&";
            postData += "client_secret=" + client_secret + "&redirect_uri=" + redirect_uri + "code&";
            postData += "grant_type=authorization_cod";
            return View();
        }
        private string Autenticar(string renavan, string aCaptcha)
        {
            //_cookies = (CookieContainer)Session["cookies"];

            var request = (HttpWebRequest)WebRequest.Create(urlBase + client_id);
            request.ProtocolVersion = HttpVersion.Version10;
            //request.CookieContainer = _cookies;
            request.Method = "POST";

            var postData = string.Empty;
            postData += "&data%5BMultas%5D%5B";
            postData += "renavam%5D=" + new Regex(@"[^\d]").Replace(renavan, string.Empty) + "&";
            postData += "data%5BMultas%5D%5Bcode%5D=" + aCaptcha;


            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = byteArray.Length;

            var dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();

            var stHtml = new StreamReader(request.GetResponse().GetResponseStream(), Encoding.GetEncoding("ISO-8859-1"));
            return stHtml.ReadToEnd();

            //_method=POST&data%5BMultas%5D%5Brenavam%5D=819052329&data%5BMultas%5D%5Bcode%5D=JVOP


        }
    }


    public class ClienteOLX
    {
        public string client_id { get; set; }
        public string redirect_uri { get; set; }
        public string scope { get; set; }
        public string state { get; set; }
        public string response_type { get; set; }

    }
}