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
    public class MarcaController : BaseController
    {
        MarcaDal marcaDal;
        public MarcaController()
        {
            marcaDal = new MarcaDal();
        }
        public ActionResult Index(string q, int? pagina, string so, string cs, int? pt)
        {
            Aviso();
            TipoDal d = new TipoDal();
            ViewBag.Tipo = d.Listar();
            if (q == null)
                q = "";

            var lista = marcaDal.Listar(null).Where(x => x.Nome.ToLower().Contains(q.ToLower()));
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
                case "marca":

                    if (so.Equals(cs))
                        return View(lista.OrderByDescending(x => x.Nome).ToPagedList(paginaNumero, paginaTamanho));
                    else
                        return View(lista.OrderBy(x => x.Nome).ToPagedList(paginaNumero, paginaTamanho));
                    break;

                case "tipo":

                    if (so.Equals(cs))
                        return View(lista.OrderByDescending(x => x.Tipo.Nome).ToPagedList(paginaNumero, paginaTamanho));
                    else
                        return View(lista.OrderBy(x => x.Tipo.Nome).ToPagedList(paginaNumero, paginaTamanho));
                    break;
                default:
                    return View(lista.ToPagedList(paginaNumero, paginaTamanho));
                    break;
            }
        }
        [HttpPost]
        public ActionResult Cadastro(FormCollection model)
        {
            try
            {
                if (string.IsNullOrEmpty(Request.Form["tipoMarca"]) ||
                    string.IsNullOrEmpty(Request.Form["txtNome"]) ||
                    string.IsNullOrEmpty(Request.Form["hddIdMarca"]))
                {
                    TempData["Mensagem"] = "Preecha todos os campos!";
                    return RedirectToAction("index");
                }
                else
                {
                    Marca marca = new Marca();
                    marca.Tipo = new Tipo();

                    marca.Tipo.IdTipo = Convert.ToInt32(Request.Form["tipoMarca"]);
                    marca.Nome = Convert.ToString(Request.Form["txtNome"]).Replace(" ", "-");
                    marca.IdMarca = Convert.ToInt32(Request.Form["hddIdMarca"]);

                    marcaDal.Salvar(marca);
                    if (marca.IdMarca == 0)
                        TempData["Mensagem"] = "Marca " + marca.Nome + " cadastrada com sucesso!";
                    else
                        TempData["Mensagem"] = "Marca " + marca.Nome + " alterada com sucesso!";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {
                throw;
            }

        }

        public JsonResult Editar(int id)
        {
            try
            {
                return Json(marcaDal.Listar(id), JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(false);
            }
        }

        public ActionResult Excluir(int id)
        {
            try
            {
                marcaDal.Excluir(id);
                TempData["Mensagem"] = "Marca EXCLUIDA com sucesso!";
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                TempData["Mensagem"] = "Certifique-se que tenha excluido todos veiculos relacionados a esta Marca. E tente novamente.";
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }
    }
}