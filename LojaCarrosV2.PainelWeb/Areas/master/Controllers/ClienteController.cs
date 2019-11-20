using LojaCarrosV2.PainelWeb.Models;
using System;
using System.Web.Mvc;
using PagedList;
using LojaCarrosV2.Infra.Repositorio;
using LojaCarrosV2.Domain.Entidade;
using System.Linq;
using LojaCarrosV2.PainelWeb.Controllers;
using LojaCarrosV2.PainelWeb.Filters;

namespace LojaCarrosV2.PainelWeb.Areas.master.Controllers
{

    [Authorize]
    [MeuFiltroDeAutorizacao(TipoAcesso = "master")]
    public class ClienteController : BaseController
    {
        ClienteDal clienteDal;

        public ClienteController()
        {
            clienteDal = new ClienteDal();
        }

        #region Listar

        public ActionResult Index(string q, int? pagina, string so, string cs, int? pt, BuscaModel model, int? idEmpresa)
        {
            Aviso();
            if (q == null)
                q = "";
            model.Termo = q;
            var lista = clienteDal.Listar(model.Termo, idEmpresa).Where(x => x.Empresa.Nome.ToLower().Contains(q.ToLower()));
            int paginaTamanho = (pt ?? 10);
            int paginaNumero = (pagina ?? 1);


            ViewBag.Action = ActionCorrente();
            ViewBag.Pagina = pagina;
            ViewBag.PaginaTamanho = pt;
            ViewBag.CurrentSort = so;
            ViewBag.SortOrder = so;
            ViewBag.Query = q;
            ViewBag.idEmpresa = idEmpresa;

            switch (so)
            {
                case "nome":

                    if (so.Equals(cs))
                        return View(lista.OrderByDescending(x => x.Nome).ToPagedList(paginaNumero, paginaTamanho));
                    else
                        return View(lista.OrderBy(x => x.Nome).ToPagedList(paginaNumero, paginaTamanho));
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

        #region Cadastro

        public ActionResult Cadastro()
        {
            ListarEstado();
            Aviso();
            CerregaEmpresa();

            return View(new ClienteVM());
        }
        [HttpPost]
        public ActionResult Cadastro(ClienteVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Cliente cliente = new Cliente();

                    cliente.IdCliente = model.IdCliente;
                    cliente.Nome = model.Nome;
                    cliente.Email = model.Email;
                    cliente.CEP = model.CEP;
                    cliente.Logradouro = model.Logradouro;
                    cliente.Cidade = model.Cidade;
                    cliente.Bairro = model.Bairro;
                    cliente.Telefone1 = model.Telefone1;
                    cliente.Telefone2 = model.Telefone2;
                    cliente.Estado = model.Estado;
                    cliente.DataCadastro = DateTime.Now;
                    cliente.IdEmpresa = model.IdEmpresa;

                    clienteDal.Salvar(cliente);

                    ModelState.Clear();
                    if (cliente.IdCliente <= 0)
                        TempData["Mensagem"] = cliente.Nome + ", cadastrado(a) com sucesso!";
                    else
                        TempData["Mensagem"] = cliente.Nome + ", Alterado(a) com sucesso!";

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

            var cliente = clienteDal.ListarPorId(id, null);
            if (cliente != null)
            {

                ClienteVM model = new ClienteVM();

                model.IdCliente = cliente.IdCliente;
                model.Nome = cliente.Nome;
                model.Email = cliente.Email;
                model.CEP = cliente.CEP;
                model.Logradouro = cliente.Logradouro;
                model.Bairro = cliente.Bairro;
                model.Cidade = cliente.Cidade;
                model.Estado = cliente.Estado;
                model.Telefone1 = cliente.Telefone1;
                model.Telefone2 = cliente.Telefone2;
                model.DataCadastro = cliente.DataCadastro;
                model.IdEmpresa = cliente.IdEmpresa;
                ListarEstado();
                CerregaEmpresa();

                return View(model);
            }
            else
            {
                TempData["Mensagem"] = "Este cliente não pertence a sua empresa ou nao existe.";

                return RedirectToAction("Index");
            }

        }

        #endregion

        #region Detalhes
        public ActionResult Detalhes(int id)
        {

            var cliente = clienteDal.ListarPorId(id, null);
            ClienteVM model = new ClienteVM();
            if (cliente != null)
            {
                model.IdCliente = cliente.IdCliente;
                model.Nome = cliente.Nome;
                model.Email = cliente.Email;
                model.CEP = cliente.CEP;
                model.Logradouro = cliente.Logradouro;
                model.Bairro = cliente.Bairro;
                model.Cidade = cliente.Cidade;
                model.Estado = cliente.Estado;
                model.Telefone1 = cliente.Telefone1;
                model.Telefone2 = cliente.Telefone2;
                model.DataCadastro = cliente.DataCadastro;
                model.IdEmpresa = cliente.IdEmpresa;

                Aviso();
                return View(model);
            }
            else
            {
                TempData["Mensagem"] = "Este cliente não pertence a sua empresa ou nao existe.";

                return RedirectToAction("Index");
            }
        }

        #endregion

        #region Excluir
        public JsonResult Excluir(int id)
        {
            try
            {
                var cliente = clienteDal.ListarPorId(id, null);
                if (cliente != null)
                {
                    clienteDal.Excluir(cliente.IdCliente);
                    TempData["Mensagem"] = "Cliente EXCLUIDO com sucesso!";
                }
                else
                {
                    TempData["Mensagem"] = "Este cliente não pertence a sua empresa ou nao existe.";
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
        public void CerregaEmpresa()
        {
            EmpresaDal empresaDal = new EmpresaDal();
            ViewBag.Empresa = empresaDal.Listar();
        }
    }
}