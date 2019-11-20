using LojaCarrosV2.Infra.Repositorio;
using LojaCarrosV2.Utilidade;
using LojaCarrosV2.Web.classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LojaCarrosV2.Web.Controllers
{
    public class BaseController : Controller
    {
        // GET: Base
        public void Aviso()
        {
            if (TempData["Mensagem"] != null)
            {
                var msg = TempData["Mensagem"];
                ViewBag.Mensagem = msg;
                Util.Alertar(ViewBag.Mensagem);
            }
        }
        public void Diretorios()
        {
            ViewBag.Diretorio = Constantes.DiretorioArquivo;
            ViewBag.DiretorioMin = Constantes.ArquivoMin;
        }

        public JsonResult ListMarca(string tipo)
        {
            try
            {
                //Thread.Sleep(1500);
                MarcaDal md = new MarcaDal();
                return Json(md.ListarByTipo(tipo, true, Constantes.IDEMPRESA).OrderBy(m => m.Nome), JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(false);
            }
        }
        public JsonResult ListAnoFiltro(string marca)
        {
            try
            {
                AnoModeloDal amd = new AnoModeloDal();
                return Json(amd.ListarAno(marca).OrderBy(z => z.AnoLista), JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(false);
            }
        }
        public JsonResult ListAnoMax(int anoMin, string marca)
        {
            try
            {
                AnoModeloDal amd = new AnoModeloDal();
                if (anoMin != null)
                    return Json(amd.ListarAnoFim(anoMin, marca), JsonRequestBehavior.AllowGet);
                return Json(true);
            }
            catch (Exception)
            {
                return Json(false);
            }
        }        
    }
}