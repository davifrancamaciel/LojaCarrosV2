using LojaCarrosV2.Domain.Entidade;
using LojaCarrosV2.Infra.Repositorio;
using LojaCarrosV2.PainelWeb.classes;
using LojaCarrosV2.PainelWeb.Models;
using LojaCarrosV2.Utilidade;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Security;

namespace LojaCarrosV2.PainelWeb.Controllers
{
    public class EsqueceuController : BaseController
    {
        UsuarioDal usuarioDal;
        public EsqueceuController()
        {
            usuarioDal = new UsuarioDal();
        }
        public PartialViewResult Index()
        {
            return PartialView();
        }

        public JsonResult EnvioEmail(string email)
        {

            var usuario = usuarioDal.Listar().Where(u => u.Email.Equals(email)).FirstOrDefault();
            if (usuario != null)
            {
                if (ModelState.IsValid)
                {

                    //string smtpUserName = "contato@enriqueautomoveis.com.br";
                    //string smtpPassword = "147258mudar";
                    //string smtpHost = "smtp.enriqueautomoveis.com.br";
                    //int smtpPort = 587;//gmail a porta é 25

                    string smtpUserName = "daviemailparateste@gmail.com";
                    string smtpPassword = "daviemailparateste12";
                    string smtpHost = "smtp.gmail.com";
                    int smtpPort = 587;//587 gmail a porta é 25

                    string texto = Guid.NewGuid().ToString() + usuario.Email + "id" + usuario.IdUsuario + Guid.NewGuid().ToString();
                    usuario.TokenUsuario = Criptografia.Encriptar(texto) + Criptografia.Encriptar(Guid.NewGuid().ToString()) + Guid.NewGuid().ToString();
                    usuario.DataCriacao = DateTime.Now;
                    usuario.ValidadeTokenUsuario = 30;

                    string TokenUsuario = usuario.TokenUsuario;

                    usuarioDal.Salvar(usuario);

                    string url = string.Format("{0}/{1}?token={2}",
                       Request.Url.GetLeftPart(UriPartial.Authority), "esqueceu/resetpassword",
                       TokenUsuario);

                    #region Corpo e envio do email
                    var linkRedefinir = string.Format("<a href='" + url + "'>clique aqui</a>");
                    var mensagem = new StringBuilder();

                    mensagem.Append("<h3>Redefinição de Senha</h3>");
                    mensagem.Append("<p>Recebemos uma soliticação de redefinição ");
                    mensagem.Append("da senha de acesso ao nosso portal.</p>");
                    mensagem.Append("<p>Se foi realmente você que fez a solicitação, ");
                    mensagem.AppendFormat("{0} para redefinir sua senha agora mesmo.</p>", linkRedefinir);
                    mensagem.Append("<p>Caso não tenha feito essa solicitação, ignore esta mensagem.</p>");
                    mensagem.Append("<p>Atenciosamente,<br/>");
                    mensagem.Append("Equipe Web Conectus</p>");

                    string emailTo = email; // Quando o contato será enviado para o seu e-mail
                    string subject = "Redefinição de Senha";
                    string body = string.Format(mensagem.ToString());

                    EnviarEmail servico = new EnviarEmail();

                    bool kq = servico.Enviar(smtpUserName, smtpPassword, smtpHost, smtpPort,
                        emailTo, subject, body);
                    //bool kq = true;
                    if (kq) TempData["Mensagem"] = "Foi enviada uma mensagem para seu e-mail informando como proceder para redefinir sua senha.";
                    else TempData["Mensagem"] = "Ocorreu um erro aoenviar mensagem tente novamente.";
                    #endregion
                }
            }
            else
                TempData["Mensagem"] = "O usuario, [ " + email + " ] não consta em nossa base de dados.";

            ModelState.Clear();
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ResetPassword(string token)
        {

            if (!string.IsNullOrEmpty(token))
            {
                Aviso();

                var usuario = usuarioDal.Listar().Where(u => u.TokenUsuario.Equals(token)).FirstOrDefault();
                if (usuario != null)
                {
                    ViewBag.DominioAppCliente = Diretorio.DominioAppCliente;

                    TimeSpan data = Convert.ToDateTime(DateTime.Now).Subtract(Convert.ToDateTime(usuario.DataCriacao));

                    if (data <= TimeSpan.Parse("00:" + usuario.ValidadeTokenUsuario + ":00.0000000"))
                    {
                        UsuarioRessetSenhaVM model = new UsuarioRessetSenhaVM();
                        model.Email = usuario.Email;
                        model.IdUsuario = usuario.IdUsuario;
                        model.PermissaoValor = usuario.Permissao.Valor;
                        model.IdEmpresa = usuario.IdEmpresa;

                        return View(model);
                    }
                    else
                    {
                        TempData["Mensagem"] = "O Token de verificação está expirado acesse o link ESQUECI MINHA SENHA <br/> e tente novamente.";
                        return RedirectToAction("index", "login");
                    }
                }
            }
            TempData["Mensagem"] = "O Token de verificação é inválido.";
            return RedirectToAction("index", "login");
        }

        [HttpPost]
        public ActionResult ResetPassword(UsuarioRessetSenhaVM model)
        {
            try
            {
                Aviso();
                ViewBag.DominioAppCliente = Diretorio.DominioAppCliente;

                if (ModelState.IsValid)
                {

                    Usuario usuario = new Usuario();
                    usuario.Permissao = new Permissao();
                    usuario.Empresa = new Empresa();

                    if (model.Senha.Equals(model.SenhaConf))
                    {
                        EmpresaDal d = new EmpresaDal();

                        usuario.IdUsuario = model.IdUsuario;
                        usuario.Senha = Criptografia.Encriptar(model.Senha);
                        usuario.Email = model.Email;
                        usuario.IdEmpresa = model.IdEmpresa;
                        usuario.Permissao.Valor = model.PermissaoValor;
                        usuarioDal.Salvar(usuario);
                        usuario.Empresa = d.ListarPorId(model.IdEmpresa);
                        usuario.Senha = "";
                        TempData["Mensagem"] = "Senha atualizada com sucesso!";
                        var jsonUsuario = JsonConvert.SerializeObject(usuario);
                        FormsAuthentication.SetAuthCookie(jsonUsuario, false);


                        return RedirectToAction("index", "veiculos");
                    }
                    else
                    {
                        TempData["Mensagem"] = "As senhas não conferem!";
                        return View(model);
                    }

                }
                return View(model);
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}