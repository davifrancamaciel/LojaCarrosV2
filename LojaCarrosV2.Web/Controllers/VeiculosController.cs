using System.Web.Mvc;
using PagedList;
using System;
using System.Linq;
using LojaCarrosV2.Infra.Repositorio;
using LojaCarrosV2.Domain.Entidade;
using LojaCarrosV2.Web.classes;

namespace LojaCarrosV2.Web.Controllers
{
    public class VeiculosController : BaseController
    {

        VeiculoDal veiculoDal = new VeiculoDal();

        public ActionResult Index(string tipo, string marca, int? ai, int? af, int? pagina)
        {
            try
            {
                var e = this.RouteData.Values;
                string actionName = (string)e["action"];

                ViewBag.Action = actionName;
                ViewBag.Pagina = pagina;
                ViewBag.MarcaF = marca;
                ViewBag.TipoF = tipo;
                ViewBag.AnoInicioF = ai;
                ViewBag.AnoFimF = af;
                Diretorios();

                TipoDal td = new TipoDal();
                ViewBag.Tipo = td.Listar();

                //var lista = veiculoDal.ListarByFilto(tipo, marca, ai, af);
                Random rnd = new Random();
                IOrderedEnumerable<Veiculo> lista = veiculoDal.ListarByFilto(tipo, marca, ai, af, Constantes.IDEMPRESA).AsEnumerable().OrderBy((i => rnd.Next()));
                int paginaTamanho = 12;
                int paginaNumero = (pagina ?? 1);

                Aviso();
                return View(lista.ToPagedList(paginaNumero, paginaTamanho));
            }
            catch (Exception)
            {
                throw;
            }
        }


        public ActionResult Detalhes(string modelo, int id)
        {
            try
            {

                Diretorios();
                Random rnd = new Random();
                IOrderedEnumerable<Veiculo> Items = veiculoDal.Listar(null, true, Constantes.IDEMPRESA).AsEnumerable().OrderBy((i => rnd.Next()));
                ViewBag.Popular = Items.Where(x => x.Arquivo.Nome != "_semfoto.jpg").Take(4);

                ArquivoDal ad = new ArquivoDal();
                ViewBag.Arquivos = ad.ListarArquivosByIdVeiculo(id);
                VeiculoDal vd = new VeiculoDal();
                Veiculo veiculo = new Veiculo();

                veiculo = vd.ListarById(id, true, Constantes.IDEMPRESA);

                if (veiculo == null)
                {
                    TempData["Mensagem"] = "Este Veiculo não está mais disponivel ou foi vendido.";
                    return RedirectToAction("index");
                }
                veiculo.QtdAcesso = veiculo.QtdAcesso + 1;
                veiculoDal.Salvar(veiculo);
                return View(veiculo);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}