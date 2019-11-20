using LojaCarrosV2.PainelWeb.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace LojaCarrosV2.PainelWeb.Controllers
{
    public class OlxController : Controller
    {
        // GET: Olx
        // [Route("code")]
        public ActionResult Index(string state, string code)
        {
            ViewBag.Token = code;
            ViewBag.State = state;

            string client_id = "f78b32a2df237759e36b8e9f078bd991c16687e4";
            string client_secret = "adb6804fa6e15e5d880dd54b16843628";

            //POST /oauth/token HTTP/1.1
            //Host: auth.olx.com.br
            //ContentType:
            //application/xwwwformurlencoded
            //code=4/P7q7W91aoMsCeLvIaQm6bTrgtp7&
            //client_id= 1055d3e698d289f2af8663725127bd4b &
            //client_secret={sua_chave_de_segurança}&redirect_uri=https://yourserver.com/code&
            //grant_type=authorization_code
            if (!string.IsNullOrEmpty(code))
            {
                // para requisicoes via rest desta maneira é preciso instalar o rest sharp  "Install-Package RestSharp"
                //será https://auth.olx.com.br/oauth/token
                var cliente = new RestClient("https://auth.olx.com.br");
                var request = new RestRequest("/oauth/token", Method.POST);
                request.AddParameter("code", code);
                request.AddParameter("client_id", client_id);
                request.AddParameter("client_secret", client_secret);
                request.AddParameter("redirect_uri", "http://www.painel.enriqueautomoveis.com.br/code");
                request.AddParameter("grant_type", "authorization_code");
                request.AddHeader("Accept", "application/x-www-form-urlencoded");


                IRestResponse<TokenViewModel> response = cliente.Execute<TokenViewModel>(request);
                var token = response;
                ViewBag.TokenOAuthOLX = token;
                //if (!string.IsNullOrEmpty(token))
                //    FormsAuthentication.SetAuthCookie(token, false);
            }

            //return Redirect(state);
            //return View();
            return Redirect("http://www.apiolx.enriqueautomoveis.com.br/home/olx?state=" + state + "&code=" + code);
        }

    }

}