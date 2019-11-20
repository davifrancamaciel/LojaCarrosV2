using LojaCarrosV2.Domain.Entidade;
using LojaCarrosV2.Utilidade;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LojaCarrosV2.PainelWeb.Controllers
{
    public class BaseController : Controller
    {
        public void Aviso()
        {
            if (TempData["Mensagem"] != null)
            {
                var msg = TempData["Mensagem"];
                ViewBag.Mensagem = msg;
                Util.Alertar(ViewBag.Mensagem);
            }
        }
        public void Mensagem(string mensagem)
        {
            TempData["Mensagem"] = mensagem;
            if (TempData["Message"] != null)
            {
                var msg = TempData["Message"];
                ViewBag.Mensagem = msg;
                Util.Alertar(ViewBag.Mensagem);
            }

        }
        public List<UnidadeFederativa> ListarEstado()
        {
            return ViewBag.Estado = UnidadeFederativa.Listar();
        }

        public Usuario UsuarioCorrente()
        {
            if (User.Identity.IsAuthenticated)
            {
                var usuarioAltenticado = JsonConvert.DeserializeObject<Usuario>(User.Identity.Name);
                return usuarioAltenticado;
            }
            return null;
        }

        public string ActionCorrente()
        {
            var e = this.RouteData.Values;

            string controllerName = (string)e["controller"];
            string actionName = (string)e["action"];
            return actionName;
        }
    }
}