using LojaCarrosV2.Infra.Repositorio;
using LojaCarrosV2.Domain.Entidade;
using System;
using System.Linq;
using System.Web.Mvc;
using LojaCarrosV2.Web.classes;

namespace LojaCarrosV2.Web.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            try
            {

                TipoDal td = new TipoDal();
                ViewBag.Tipo = td.Listar();
                MarcaDal mdc = new MarcaDal();
                ViewBag.MarcaCarro = mdc.ListarByTipo("carro", true,Constantes.IDEMPRESA);
                MarcaDal mdm = new MarcaDal();
                ViewBag.MarcaMoto = mdm.ListarByTipo("moto", true, Constantes.IDEMPRESA);
                Diretorios();

                Random rnd = new Random();
                VeiculoDal vd = new VeiculoDal();
                IOrderedEnumerable<Veiculo> Items = vd.Listar(null, true, Constantes.IDEMPRESA).AsEnumerable().OrderBy((i => rnd.Next()));
                ViewBag.Popular = Items.Where(x => x.Arquivo.Nome != "_semfoto.jpg").Where(x => x.Destaque == true).Take(4);
            }
            catch (Exception)
            {
                throw;
            }

            return View();
        }

        public ActionResult Sobre()
        {
            return View();
        }
    }
}