using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LojaCarrosV2.PainelWeb.Controllers;
using LojaCarrosV2.Infra.Repositorio;
using PagedList;
using LojaCarrosV2.PainelWeb.Models;
using LojaCarrosV2.PainelWeb.Areas.master.Models;
using LojaCarrosV2.Domain.Entidade;
using LojaCarrosV2.PainelWeb.Filters;

namespace LojaCarrosV2.PainelWeb.Areas.master.Controllers
{
    [MeuFiltroDeAutorizacao(TipoAcesso = "master")]
    public class EmpresaController : BaseController
    {
        EmpresaDal empresaDal;
        VeiculoDal veiculoDal;
        UsuarioDal usuarioDal;
        ClienteDal clienteDal;


        public EmpresaController()
        {
            empresaDal = new EmpresaDal();
            veiculoDal = new VeiculoDal();
            usuarioDal = new UsuarioDal();
            clienteDal = new ClienteDal();
        }
        #region Listar

        public ActionResult Index(string q, int? pagina, string so, string cs, int? pt)
        {

            try
            {
                if (q == null)
                    q = "";
                Aviso();
                var lista = empresaDal.Listar().Where(x => x.Nome.ToLower().Contains(q.ToLower()) || x.CNPJ.ToLower().Contains(q.ToLower()));

                int paginaTamanho = (pt ?? 10);
                int paginaNumero = (pagina ?? 1);

                ViewBag.Action = ActionCorrente();
                ViewBag.Pagina = pagina;
                ViewBag.PaginaTamanho = pt;
                ViewBag.CurrentSort = so;
                ViewBag.SortOrder = so;
                ViewBag.Query = q;


                switch (so)
                {
                    case "nome":

                        if (so.Equals(cs))
                            return View(lista.OrderByDescending(x => x.Nome).ToPagedList(paginaNumero, paginaTamanho));
                        else
                            return View(lista.OrderBy(x => x.Nome).ToPagedList(paginaNumero, paginaTamanho));
                        break;

                    case "cnpj":

                        if (so.Equals(cs))
                            return View(lista.OrderByDescending(x => x.CNPJ).ToPagedList(paginaNumero, paginaTamanho));
                        else
                            return View(lista.OrderBy(x => x.CNPJ).ToPagedList(paginaNumero, paginaTamanho));
                        break;
                    default:
                        return View(lista.ToPagedList(paginaNumero, paginaTamanho));
                        break;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion


        #region Cadastro

        public ActionResult Cadastro()
        {
            try
            {
                Aviso();
                ListarEstado();
            }
            catch (Exception)
            {
                throw;
            }

            return View(new EmpresaVM());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cadastro(EmpresaVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Empresa empresa = new Empresa();

                    empresa.IdEmpresa = model.IdEmpresa;
                    empresa.CNPJ = model.CNPJ;
                    empresa.Ativa = model.Ativa;
                    empresa.DiaVencimento = model.DiaVencimento;
                    empresa.URL = model.URL;
                    empresa.Nome = model.Nome;
                    empresa.Email = model.Email;
                    empresa.CEP = model.CEP;
                    empresa.Logradouro = model.Logradouro;
                    empresa.Cidade = model.Cidade;
                    empresa.Bairro = model.Bairro;
                    empresa.Telefone1 = model.Telefone1;
                    empresa.Telefone2 = model.Telefone2;
                    empresa.Estado = model.Estado;
                    empresa.DataCadastro = DateTime.Now;
                    empresa.Observacoes = model.Observacoes;

                    empresaDal.Salvar(empresa);

                    ModelState.Clear();
                    if (empresa.IdEmpresa <= 0)
                        TempData["Mensagem"] = empresa.Nome + ", cadastrado(a) com sucesso!";
                    else
                        TempData["Mensagem"] = empresa.Nome + ", Alterado(a) com sucesso!";

                    return RedirectToAction("Index");
                }
                ListarEstado();
                return View(model);
            }
            catch (Exception)
            {
                throw;
            }

        }
        #endregion


        #region Editar
        public ActionResult Editar(int id)
        {
            try
            {

                var empresa = empresaDal.ListarPorId(id);
                EmpresaVM model = new EmpresaVM();

                model.IdEmpresa = empresa.IdEmpresa;
                model.URL = empresa.URL;
                model.CNPJ = empresa.CNPJ;
                model.DiaVencimento = empresa.DiaVencimento;
                model.Ativa = empresa.Ativa;
                model.Observacoes = empresa.Observacoes;
                model.Nome = empresa.Nome;
                model.Email = empresa.Email;
                model.CEP = empresa.CEP;
                model.Logradouro = empresa.Logradouro;
                model.Bairro = empresa.Bairro;
                model.Cidade = empresa.Cidade;
                model.Estado = empresa.Estado;
                model.Telefone1 = empresa.Telefone1;
                model.Telefone2 = empresa.Telefone2;
                model.DataCadastro = empresa.DataCadastro;

                ListarEstado();

                return View(model);
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion


        #region Detalhes
        public ActionResult Detalhes(int id)
        {
            try
            {
                var empresa = empresaDal.ListarPorId(id);
                EmpresaVM model = new EmpresaVM();

                model.IdEmpresa = empresa.IdEmpresa;
                model.URL = empresa.URL;
                model.CNPJ = empresa.CNPJ;
                model.DiaVencimento = empresa.DiaVencimento;
                model.Ativa = empresa.Ativa;
                model.Observacoes = empresa.Observacoes;
                model.Nome = empresa.Nome;
                model.Email = empresa.Email;
                model.CEP = empresa.CEP;
                model.Logradouro = empresa.Logradouro;
                model.Bairro = empresa.Bairro;
                model.Cidade = empresa.Cidade;
                model.Estado = empresa.Estado;
                model.Telefone1 = empresa.Telefone1;
                model.Telefone2 = empresa.Telefone2;
                model.DataCadastro = empresa.DataCadastro;


                ViewBag.Veiculos = veiculoDal.Listar(null, null, model.IdEmpresa);
                ViewBag.Usuarios = usuarioDal.Listar(model.IdEmpresa);
                ViewBag.Clientes = clienteDal.Listar("", model.IdEmpresa);

                return View(model);
            }
            catch (Exception)
            {

                throw;
            }
        }


        #endregion


        #region Excluir
        public JsonResult Excluir(int id)
        {
            try
            {
                empresaDal.Excluir(id);
                TempData["Mensagem"] = "Empresa EXCLUIDA com sucesso!";
                return Json(true, JsonRequestBehavior.AllowGet);
            }

            catch (Exception ex)
            {
                TempData["Mensagem"] = "Ocorreu um erro ao EXCLUIR!";
                if (ex.Message.Contains("CONSTRAINT"))
                {
                    TempData["Mensagem"] += "</br>Certifique-se que tenha excluido todos os itens relacionados a esta Empresa <br/> como Veiculos, Usuarios e Clientes.";
                }
                return Json(true, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
    }
}