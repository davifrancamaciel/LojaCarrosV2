using LojaCarrosV2.Domain.Entidade;
using LojaCarrosV2.PainelWeb.Controllers;
using Newtonsoft.Json;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;


namespace LojaCarrosV2.PainelWeb.Filters
{
    public class MeuFiltroDeAutorizacao : AuthorizeAttribute
    {
        public string TipoAcesso { get; set; }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var isAuthorized = httpContext.User.Identity.IsAuthenticated;
            if (!isAuthorized)
            {
                return false;
            }

            var usuarioAltenticado = JsonConvert.DeserializeObject<Usuario>(httpContext.User.Identity.Name);

            string privilegeLevels = string.Join("", usuarioAltenticado.Permissao.Valor);

            //if (privilegeLevels.Contains(this.TipoAcesso))
            //{
            //    return true;
            //}
            if (this.TipoAcesso.Contains(privilegeLevels))
            {
                return true;
            }
            else
            {
                ha();
                return false;
            }
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary(
                            new
                            {
                                controller = "veiculos",
                                action = "index"//,
                               //new{ area = ""}
                            }));
        }
        public void ha()
        {

            BaseController b = new BaseController();
            b.Mensagem("nao pode");
        }
    }
}