using LojaCarrosV2.PainelWeb.Models;
using System;
using System.Web.Mvc;
using System.Web.Security;
using LojaCarrosV2.Utilidade;
using PagedList;
using LojaCarrosV2.Infra.Repositorio;
using LojaCarrosV2.Domain.Entidade;
using LojaCarrosV2.PainelWeb.Filters;
using System.Linq;

namespace LojaCarrosV2.PainelWeb.Controllers
{
    [Authorize]
    public class UsuarioController : BaseController
    {
        UsuarioDal usuarioDal;
        PermissaoDal permissaoDal;
        public UsuarioController()
        {
            usuarioDal = new UsuarioDal();
            permissaoDal = new PermissaoDal();
        }

        #region Listar

        public ActionResult Index(string q, int? pagina, string so, string cs, int? pt)
        {
            if (q == null)
                q = "";
            var usuarioAutenticado = UsuarioCorrente();
            var lista = usuarioDal.Listar(UsuarioCorrente().IdEmpresa).Where(x => x.IdUsuario != usuarioAutenticado.IdUsuario && x.Email.ToLower().Contains(q.ToLower()));
            int paginaTamanho = (pt ?? 10);
            int paginaNumero = (pagina ?? 1);

            ViewBag.Action = ActionCorrente();
            ViewBag.Pagina = pagina;
            ViewBag.PaginaTamanho = pt;
            ViewBag.CurrentSort = so;
            ViewBag.SortOrder = so;
            ViewBag.Query = q;


            Aviso();


            switch (so)
            {
                case "email":

                    if (so.Equals(cs))
                        return View(lista.OrderByDescending(x => x.Email).ToPagedList(paginaNumero, paginaTamanho));
                    else
                        return View(lista.OrderBy(x => x.Email).ToPagedList(paginaNumero, paginaTamanho));
                    break;

                default:
                    return View(lista.ToPagedList(paginaNumero, paginaTamanho));
                    break;
            }
        }
        #endregion

        #region AlterarSenha
        public ActionResult Alterar()
        {
            try
            {
                Aviso();
                if (User.Identity.IsAuthenticated)
                {
                    Usuario usuario = new Usuario();
                    UsuarioModelAlterarSenha model = new UsuarioModelAlterarSenha();

                    var usuarioAltenticado = UsuarioCorrente();

                    usuario = usuarioDal.ListarPorId(usuarioAltenticado.IdUsuario);
                    model.IdUsuario = usuario.IdUsuario;
                    model.Email = usuario.Email;

                    return View(model);
                }
                else
                    return Logout();

            }
            catch (Exception)
            { throw; }
        }

        [HttpPost]
        public ActionResult Alterar(UsuarioModelAlterarSenha model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (usuarioDal.VerificarSenhaAtual(Criptografia.Encriptar(model.SenhaAtual), model.IdUsuario))
                    {
                        if (model.Senha.Equals(model.SenhaConf))
                        {
                            Usuario u = new Usuario();

                            u.IdUsuario = model.IdUsuario;
                            u.Senha = Criptografia.Encriptar(model.Senha);
                            usuarioDal.Salvar(u);

                            TempData["Mensagem"] = model.Email + ", Alterado(a) com sucesso!";
                            return RedirectToAction("index", "veiculos");
                        }
                        else
                        {
                            TempData["Mensagem"] = "As senhas não conferem!";
                            return RedirectToAction("alterar");
                        }
                    }
                    else
                    {
                        TempData["Mensagem"] = "A Senha atual está incorreta.";
                        return RedirectToAction("alterar");
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return View("Alterar");

        }
        #endregion

        #region Cadastrar

        [MeuFiltroDeAutorizacao(TipoAcesso = "admin,master")]
        public ActionResult Cadastro()
        {
            Permissao();
            return View(new UsuarioModelCadastroAdminstrador());
        }
        [HttpPost]
        public ActionResult Cadastro(UsuarioModelCadastroAdminstrador model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (!usuarioDal.Existe(model.Email))
                    {
                        if (model.Senha.Equals(model.SenhaConf))
                        {
                            Usuario u = new Usuario();
                            var usuarioCorrente = UsuarioCorrente();

                            u.ValidaSenha(model.Senha, model.SenhaConf);
                            u.Email = model.Email;
                            u.Senha = Criptografia.Encriptar(model.Senha);
                            u.TokenUsuario = "";
                            u.ValidadeTokenUsuario = 0;
                            u.DataCriacao = DateTime.Now;
                            u.IdPermissao = model.IdPermissao;
                            u.IdEmpresa = usuarioCorrente.IdEmpresa;

                            usuarioDal.Salvar(u);
                            TempData["Mensagem"] = u.Email + ", cadastrado(a) com sucesso!";
                            return RedirectToAction("index");
                        }
                        else
                        {
                            TempData["Mensagem"] = "As senhas não conferem!";
                            Aviso();
                            Permissao();
                            return View(model);
                        }
                    }
                    else
                    {
                        TempData["Mensagem"] = "Este Email ja se encontra em nossa base de dados por favor utilize outro";
                        Aviso();
                        Permissao();
                        return View(model);
                    }
                }
                Permissao();
            }
            catch (Exception ex)
            {
                throw;
            }
            ModelState.Clear();
            Aviso();

            return View("Cadastro");
        }
        #endregion

        //#region Editar
        //public ActionResult Editar()
        //{
        //    try
        //    {

        //        Usuario usuario = new Usuario();
        //        UsuarioModelEditar model = new UsuarioModelEditar();

        //        var usuarioAltenticado = UsuarioCorrente();

        //        usuario = usuarioDal.ListarPorId(usuarioAltenticado.IdUsuario);
        //        model.IdUsuario = usuario.IdUsuario;
        //        model.Email = usuario.Email;


        //        return View(model);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }

        //}
        //[HttpPost]
        //public ActionResult Editar(UsuarioModelEditar model)
        //{
        //    Usuario usuario = new Usuario();

        //    if (ModelState.IsValid)
        //    {
        //        usuario.IdUsuario = model.IdUsuario;
        //        usuario.Email = model.Email;

        //        usuarioDal.Salvar(usuario);

        //        return RedirectToAction("index");

        //    }
        //    return View(model);
        //}
        //#endregion

        #region Excluir
        [MeuFiltroDeAutorizacao(TipoAcesso = "admin,master")]
        public JsonResult Excluir(int id)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    var usuarioSerExcluido = usuarioDal.ListarPorId(id);
                    if (usuarioSerExcluido.IdEmpresa == UsuarioCorrente().IdEmpresa)
                    {
                        var usuarioAltenticado = UsuarioCorrente();

                        if (usuarioAltenticado.IdUsuario != id)
                        {
                            usuarioDal.Excluir(id);
                            TempData["Mensagem"] = "Usuário EXCLUIDO com sucesso!";
                        }
                        else
                        {
                            TempData["Mensagem"] = "Ocorreu um erro! Você não pode se excluir!";
                            Logout();
                        }
                    }
                    else { TempData["Mensagem"] = "Este usuario não pertence a sua Empresa!"; }
                }
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                TempData["Mensagem"] = "Ocorreu um erro ao EXCLUIR!";
                return Json(false);
            }
        }

        #endregion

        #region Logout

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Remove("usuario");
            Session.Abandon();
            Session.Clear();
            return RedirectToAction("index", "login", new { area = "", controller = "login" });
        }

        #endregion

        public void Permissao()
        {            
            ViewBag.Permissao = permissaoDal.Listar().Where(x => x.Valor != "master");
        }
    }
}