using LojaCarrosV2.Domain.Entidade;
using LojaCarrosV2.Infra.Repositorio;
using LojaCarrosV2.PainelWeb.Controllers;
using LojaCarrosV2.PainelWeb.Filters;
using LojaCarrosV2.PainelWeb.Models;
using LojaCarrosV2.Utilidade;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using PagedList;

namespace LojaCarrosV2.PainelWeb.Areas.master.Controllers
{
    [Authorize]
    [MeuFiltroDeAutorizacao(TipoAcesso = "master")]
    public class UsuarioController : BaseController
    {
        UsuarioDal usuarioDal;
        PermissaoDal permissaoDal;
        EmpresaDal empresaDal;
        public UsuarioController()
        {
            usuarioDal = new UsuarioDal();
            permissaoDal = new PermissaoDal();
            empresaDal = new EmpresaDal();
        }

        #region Listar

        public ActionResult Index(string q, int? pagina, string so, string cs, int? pt, int? idEmpresa)
        {
            if (q == null)
                q = "";
            var usuarioAutenticado = UsuarioCorrente();
            var lista = usuarioDal.Listar(idEmpresa).Where(x => x.IdUsuario != usuarioAutenticado.IdUsuario && x.Email.ToLower().Contains(q.ToLower()) || x.Empresa.Nome.ToLower().Contains(q.ToLower()));
            int paginaTamanho = (pt ?? 10);
            int paginaNumero = (pagina ?? 1);

            ViewBag.Action = ActionCorrente();
            ViewBag.Pagina = pagina;
            ViewBag.PaginaTamanho = pt;
            ViewBag.CurrentSort = so;
            ViewBag.SortOrder = so;
            ViewBag.Query = q;
            ViewBag.idEmpresa = idEmpresa;

            Aviso();


            switch (so)
            {
                case "empresa":

                    if (so.Equals(cs))
                        return View(lista.OrderByDescending(x => x.Empresa.Nome).ToPagedList(paginaNumero, paginaTamanho));
                    else
                        return View(lista.OrderBy(x => x.Empresa.Nome).ToPagedList(paginaNumero, paginaTamanho));
                    break;
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
                            return RedirectToAction("Alterar");
                        }
                        else
                        {
                            TempData["Mensagem"] = "As senhas não conferem!";
                            return RedirectToAction("Alterar");
                        }
                    }
                    else
                    {
                        TempData["Mensagem"] = "A Senha atual está incorreta.";
                        return RedirectToAction("Alterar");
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


        public ActionResult Cadastro()
        {
            DropDowns();
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

                            u.ValidaSenha(model.Senha, model.SenhaConf);
                            u.Email = model.Email;
                            u.Senha = Criptografia.Encriptar(model.Senha);
                            u.TokenUsuario = "";
                            u.ValidadeTokenUsuario = 0;
                            u.DataCriacao = DateTime.Now;
                            u.IdPermissao = model.IdPermissao;
                            u.IdEmpresa = model.IdEmpresa;

                            usuarioDal.Salvar(u);
                            TempData["Mensagem"] = u.Email + ", cadastrado(a) com sucesso!";
                            return RedirectToAction("index");
                        }
                        else
                        {
                            TempData["Mensagem"] = "As senhas não conferem!";
                            Aviso();
                            DropDowns();
                            return View(model);
                        }
                    }
                    else
                    {
                        TempData["Mensagem"] = "Este Email ja se encontra em nossa base de dados por favor utilize outro";
                        Aviso();
                        DropDowns();
                        return View(model);
                    }
                }
                DropDowns();
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

        #region Editar
        public ActionResult Editar(int id)
        {
            try
            {
                DropDowns();
                Usuario usuario = new Usuario();
                UsuarioEditarVM model = new UsuarioEditarVM();

                usuario = usuarioDal.ListarPorId(id);
                model.IdUsuario = usuario.IdUsuario;
                model.Email = usuario.Email;
                model.IdEmpresa = usuario.IdEmpresa;
                model.IdPermissao = usuario.IdPermissao;



                return View(model);
            }
            catch (Exception)
            {
                throw;
            }

        }
        [HttpPost]
        public ActionResult Editar(UsuarioEditarVM model)
        {
            Usuario usuario = new Usuario();

            if (ModelState.IsValid)
            {
                //var usuarioExistente = usuarioDal.ListarPorId(model.IdUsuario);
                //if (!usuarioDal.Existe(model.Email))
                //{
                usuario.IdUsuario = model.IdUsuario;
                usuario.Email = model.Email;
                usuario.IdEmpresa = model.IdEmpresa;
                usuario.IdPermissao = model.IdPermissao;

                usuarioDal.Salvar(usuario);
                TempData["Mensagem"] = "Usuario " + model.Email + " Atualizado com sucesso.";
                return RedirectToAction("index");
                //}
                //else
                //{
                //    TempData["Mensagem"] = "Este Email ja se encontra em nossa base de dados por favor utilize outro";
                //    Aviso();
                //    DropDowns();
                //    return View(model);
                //}
            }
            return View(model);
        }
        #endregion

        #region Excluir

        public JsonResult Excluir(int id)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
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
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                TempData["Mensagem"] = "Ocorreu um erro ao EXCLUIR!";
                return Json(false, JsonRequestBehavior.AllowGet);
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

        public void DropDowns()
        {            
            ViewBag.Permissao = permissaoDal.Listar();            
            ViewBag.Empresa = empresaDal.Listar();
        }

    }
}