using LojaCarrosV2.Infra.Repositorio;
using LojaCarrosV2.PainelWeb.Models;
using System;
using System.Web.Mvc;
using System.Web.Security;
using LojaCarrosV2.Utilidade;
using LojaCarrosV2.PainelWeb.classes;
using LojaCarrosV2.Domain.Entidade;
using Newtonsoft.Json;

namespace LojaCarrosV2.PainelWeb.Controllers
{
    public class LoginController : BaseController
    {
        UsuarioDal usuarioDal;
        public LoginController()
        {
            usuarioDal = new UsuarioDal();
        }
        public ActionResult Index()
        {
            try
            {
                ViewBag.DominioAppCliente = Diretorio.DominioAppCliente;
                UsuarioModelLogin model = new UsuarioModelLogin();
                Aviso();
                string url = Request.QueryString["ReturnUrl"];
                model.Url = url;
                return View(model);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public ActionResult Index(UsuarioModelLogin model)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    Usuario usuario = usuarioDal.Autenticar(model.Email, Criptografia.Encriptar(model.Senha));

                    if (usuario != null)
                    {

                        // conceder permissão ao usuario   // tickete de acesso
                        // false define que o tiket e destruido quando o navegador for fechado
                        //FormsAuthentication.SetAuthCookie(u.Email, false);
                        var jsonUsuario = JsonConvert.SerializeObject(usuario);
                        FormsAuthentication.SetAuthCookie(jsonUsuario, model.Remember);

                        // armazenar os dados do usuario em sessao//a sessao e um espaco  de memorioa mantido enquanto o navegador estiver aberto
                        //Session.Add("usuario", usuario);

                        if (model.Url != null)
                            return Redirect(model.Url);
                        if (usuario.Permissao.Valor.Contains("master"))
                            return RedirectToAction("index", "veiculos", new { area = "master" });
                        else
                            return RedirectToAction("index", "veiculos");
                    }
                    else
                    {
                        TempData["Mensagem"] = "Acesso negado! Verifique se seu Email ou Senha estão corretos.";
                        return RedirectToAction("index");
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return View("Index");
        }
    }
}