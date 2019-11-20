using LojaCarrosV2.PainelWeb.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;
using PagedList;
using System.Web;
using LojaCarrosV2.Utilidade;
using LojaCarrosV2.Infra.Repositorio;
using LojaCarrosV2.Domain.Entidade;
using System.Drawing;
using System.Linq;
using LojaCarrosV2.PainelWeb.classes;
using LojaCarrosV2.PainelWeb.Filters;
using System.Threading;

namespace LojaCarrosV2.PainelWeb.Controllers
{

    [Authorize]
    public class VeiculosController : BaseController
    {
        VeiculoDal veiculoDal;
        CombustivelDal combustivelDal;
        TipoDal tipoDal;
        AnoModeloDal anoModeloDal;
        MarcaDal marcaDal;
        ArquivoDal arquivoDal;
        public VeiculosController()
        {
            veiculoDal = new VeiculoDal();
            combustivelDal = new CombustivelDal();
            tipoDal = new TipoDal();
            anoModeloDal = new AnoModeloDal();
            marcaDal = new MarcaDal();
            arquivoDal = new ArquivoDal();
        }

        #region Diretorios
        /// <summary>
        /// diretorios para dentro da aplicacao pois é desta maneira
        /// porque o metodo de redimencionar imagens nao funcioma com caminhos http só c: ...
        /// </summary>
        public string arquivosNormais = "~/arquivos/normais/";
        public string arquivosMin = "~/arquivos/miniaturas/";
        public string arquivosRedimencionados = "~/arquivos/redimensionados/";
        #endregion


        #region Listar

        public ActionResult Index(string q, int? pagina, string so, string cs, int? pt)
        {

            try
            {
                Aviso();
                if (q == null)
                    q = "";
                var lista = veiculoDal.Listar(null, null, UsuarioCorrente().IdEmpresa).Where(x => x.Modelo.ToLower().Contains(q.ToLower()) || x.Marca.Nome.ToLower().Contains(q.ToLower()));
                Diretorios();
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
                    case "modelo":

                        if (so.Equals(cs))
                            return View(lista.OrderByDescending(x => x.Modelo).ToPagedList(paginaNumero, paginaTamanho));
                        else
                            return View(lista.OrderBy(x => x.Modelo).ToPagedList(paginaNumero, paginaTamanho));
                        break;

                    case "ano":

                        if (so.Equals(cs))
                            return View(lista.OrderByDescending(x => x.AnoFabricacao).ToPagedList(paginaNumero, paginaTamanho));
                        else
                            return View(lista.OrderBy(x => x.AnoFabricacao).ToPagedList(paginaNumero, paginaTamanho));
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
                CarregarDropDowns();
            }
            catch (Exception)
            {
                throw;
            }

            return View(new VeiculoVM());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cadastro(VeiculoVM model)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    Veiculo veiculo = new Veiculo();
                    veiculo.Tipo = new Tipo();
                    veiculo.Marca = new Marca();
                    veiculo.Combustivel = new Combustivel();

                    veiculo.IdVeiculo = model.IdVeiculo;
                    veiculo.DataCadastro = DateTime.Now;
                    veiculo.Modelo = NormalizeTextExtension.RemoveSpecialCharacters(model.Modelo);
                    veiculo.AnoFabricacao = model.AnoFabricacao;
                    veiculo.AnoModelo = Convert.ToInt32(Request.Form["anoModelo"]);
                    veiculo.Valor = model.Valor;
                    veiculo.Descricao = model.Descricao;
                    veiculo.Tipo.IdTipo = model.IdTipo;
                    veiculo.Marca.IdMarca = Convert.ToInt32(Request.Form["marca"]);
                    veiculo.Ativo = model.Ativo;
                    veiculo.Destaque = model.Destaque;
                    veiculo.Combustivel.IdCombustivel = model.IdCombustivel;
                    veiculo.Renavan = model.Renavan;
                    veiculo.IdEmpresa = UsuarioCorrente().IdEmpresa;
                    veiculo.ExibeValor = model.ExibeValor;

                    veiculoDal.Salvar(veiculo);

                    int id = veiculo.IdVeiculo;
                    if (model.IdVeiculo == 0)
                    {
                        return RedirectToAction("uploadgaleria/" + id);
                    }
                    else
                    {
                        TempData["Mensagem"] = "Veiculo <span class='text-danger'>" + model.Modelo + "</span> EDITADO com sucesso!";
                        return RedirectToAction("index");

                    }
                }
                CarregarDropDowns();
                return View(model);

            }
            catch (Exception)
            {
                throw;
            }

        }
        #endregion


        #region Editar
        public ActionResult Editar(int id, int? p)
        {
            try
            {
                Diretorios();

                var veiculo = veiculoDal.ListarById(id, null, UsuarioCorrente().IdEmpresa);
                VeiculoVM model = new VeiculoVM();
                if (veiculo != null)
                {

                    model.IdVeiculo = veiculo.IdVeiculo;
                    model.Modelo = veiculo.Modelo;
                    model.Descricao = veiculo.Descricao;
                    model.Ativo = veiculo.Ativo;
                    model.Destaque = veiculo.Destaque;
                    model.AnoFabricacao = veiculo.AnoFabricacao;
                    model.AnoModelo = veiculo.AnoModelo;
                    model.Valor = veiculo.Valor;
                    model.IdCombustivel = veiculo.Combustivel.IdCombustivel;
                    model.IdTipo = veiculo.Marca.Tipo.IdTipo;
                    model.IdMarca = veiculo.Marca.IdMarca;
                    model.DataCadastro = veiculo.DataCadastro;
                    model.Renavan = veiculo.Renavan;
                    model.ExibeValor = veiculo.ExibeValor;

                    CarregarDropDowns();
                    //ArquivoDal ad = new ArquivoDal();
                    ViewBag.Arquivos = arquivoDal.ListarArquivosByIdVeiculo(id);

                    //MarcaDal md = new MarcaDal();
                    ViewBag.Marcas = marcaDal.ListarByIdTipo(model.IdTipo).OrderBy(m => m.Nome);
                    return View(model);
                }
                else
                {
                    TempData["Mensagem"] = "Este Veiculo não pertence a sua Empresa ou não existe.";
                    return RedirectToAction("index", new { pagina = p });
                }
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
                Aviso();
                //Diretorios();
                //ArquivoDal ad = new ArquivoDal();
                //ViewBag.Arquivos = ad.ListarArquivosByIdVeiculo(id);
                Veiculo veiculo = new Veiculo();
                veiculo = veiculoDal.ListarById(id, null, UsuarioCorrente().IdEmpresa);
                VeiculoVM model = new VeiculoVM();
                if (veiculo != null)
                {
                    model.IdVeiculo = veiculo.IdVeiculo;
                    model.Modelo = veiculo.Modelo;
                    model.Descricao = veiculo.Descricao;
                    model.Ativo = veiculo.Ativo;
                    model.Destaque = veiculo.Destaque;
                    model.AnoFabricacao = veiculo.AnoFabricacao;
                    model.AnoModelo = veiculo.AnoModelo;
                    model.Valor = veiculo.Valor;
                    model.IdCombustivel = veiculo.Combustivel.IdCombustivel;
                    model.IdTipo = veiculo.Marca.Tipo.IdTipo;
                    model.IdMarca = veiculo.Marca.IdMarca;
                    model.DataCadastro = veiculo.DataCadastro;
                    model.Renavan = veiculo.Renavan;
                    model.ExibeValor = veiculo.ExibeValor;

                    ViewBag.Consultas = MontaConsulta(id);


                    return View(model);
                }
                else
                {
                    TempData["Mensagem"] = "Este Veiculo não pertence a sua Empresa ou não existe.";
                    return RedirectToAction("index");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }


        #endregion


        #region Excluir
        [MeuFiltroDeAutorizacao(TipoAcesso = "admin,master")]
        public JsonResult Excluir(int id)
        {
            try
            {
                var veiculo = veiculoDal.ListarById(id, null, UsuarioCorrente().IdEmpresa);
                if (veiculo != null)
                {

                    var consultas = ConsultaDal.Listar(veiculo.IdVeiculo);
                    if (consultas.Count == 0)
                    {
                        List<Arquivo> lista = new List<Arquivo>();
                        //ArquivoDal ad = new ArquivoDal();
                        lista = arquivoDal.ListarArquivosByIdVeiculo(veiculo.IdVeiculo);
                        foreach (var item in lista)
                        {
                            AcessoPastaArquivos(item.Nome);
                        }

                        veiculoDal.Excluir(veiculo.IdVeiculo);
                        TempData["Mensagem"] = "Veiculo EXCLUIDO com sucesso!";
                        return Json(true, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        TempData["Mensagem"] = "Ocorreu um erro ao EXCLUIR! </br>Certifique-se que tenha excluido todas as consultas relacionadas a este veiculo!";
                        return Json(false);
                    }
                }
                else { TempData["Mensagem"] = "Este Veiculo não pertence a sua Empresa!"; }
                return Json(true, JsonRequestBehavior.AllowGet);
            }

            catch (Exception ex)
            {
                TempData["Mensagem"] = "Ocorreu um erro ao EXCLUIR!";
                if (ex.Message.Contains("CONSTRAINT `FK_Consulta_veiculo`"))
                {
                    TempData["Mensagem"] += "</br>Certifique-se que tenha excluido todas as consultas relacionadas a este veiculo!";
                }
                return Json(false);
            }
        }


        public ActionResult ExcluirArquivo(int id, string nome, int idVeiculo,int p)
        {
            try
            {
                AcessoPastaArquivos(nome);
                //ArquivoDal arquivoDal = new ArquivoDal();
                arquivoDal.Excluir(id);

                return RedirectToAction("editar", new { id = idVeiculo, p = p });
            }
            catch (Exception)
            {

                throw;
            }
        }


        #endregion


        #region Upload

        public ActionResult UploadGaleria(int id)
        {
            try
            {
                VeiculoVM model = new VeiculoVM();
                model.IdVeiculo = id;

                Aviso();
                return View(model);
            }
            catch (Exception)
            {

                throw;
            }

            return View();
        }
        [HttpPost]
        public ActionResult Upload(int id)
        {
            try
            {
                //Thread.Sleep(7000);
                HttpPostedFileBase file = Request.Files["myfile"];
                string nome = Guid.NewGuid().ToString() + ".jpg";
                int tamanho = file.ContentLength;
                string nomeAnterior = file.FileName;
                int idVeiculo = id;
                string extensao = Path.GetExtension(file.FileName);

                extensao.ToLower();

                if (extensao.Equals(".jpg") || extensao.Equals(".jpeg") || extensao.Equals(".gif") || extensao.Equals(".png"))
                {
                    if (file.ContentLength <= Math.Pow(1024, 2) * 2)
                    {
                        Arquivo a = new Arquivo
                        {
                            Nome = nome,
                            NomeAnterior = nomeAnterior,
                            Tamanho = tamanho,
                            DataCadastro = DateTime.Now,
                            IdVeiculo = idVeiculo
                        };
                        //ArquivoDal d = new ArquivoDal();
                        arquivoDal.Inserir(a);

                        //file.SaveAs(Server.MapPath("~/") + "arquivos/normais/" + nome);
                        file.SaveAs(Server.MapPath(arquivosNormais) + nome);  //esse é o que funfa
                        //file.SaveAs(Server.MapPath(Diretorio.ArquivoNormais.ToString()) + nome);

                        RedimensionarImagens(nome, 600, 400, "arquivos\\redimensionados\\");
                        RedimensionarImagens(nome, 300, 200, "arquivos\\miniaturas\\");
                        ExcluiArquivosNormais(nome);
                    }
                    else
                    {
                        TempData["Mensagem"] = "Sua imagem ultrapassou o limite de 2 MB suportado pelo sistema.<br /> Referente ao VEÍCULO <label class='label label-danger'>" + idVeiculo + "</label>";
                        return Json(false);
                    }
                }
                else
                {
                    TempData["Mensagem"] = "Algum arquivo não foi gravado com sucesso.<br /> Verifique se o mesmo possui as extenções de imagem (.jpeg, .png, .jpg ou .gif) que são suportadas pelo sistema.<br />Referente ao VEÍCULO <label class='label label-danger'>ID " + idVeiculo + "</label>";
                    return Json(false);
                }

                return Json(new { success = true });

            }
            catch (Exception)
            {
                throw;
            }
        }


        #endregion


        #region metodos

        public JsonResult ListMarca(int tipo)
        {
            try
            {
                //Thread.Sleep(7000);
                //MarcaDal md = new MarcaDal();
                return Json(marcaDal.ListarByIdTipo(tipo).OrderBy(m => m.Nome), JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(false);
            }
        }

        public JsonResult ListAnoMax(int anoMin)
        {
            try
            {
                //Thread.Sleep(7000);
                //AnoModeloDal am = new AnoModeloDal();
                return Json(anoModeloDal.ListarAno1(anoMin), JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(false);
            }
        }
        public void RedimensionarImagens(string nome, int width, int height, string caminho)
        {
            Size tamanhos = new Size();
            tamanhos.Height = height;// 400;
            tamanhos.Width = width;//600;
            string filePath = Server.MapPath(arquivosNormais.ToString()) + nome;
            // string filePath = Server.MapPath(arquivosRedimencionados.ToString()) + nome; // descomentar para o metoddo robo
            //string filePath = Server.MapPath(Diretorio.ArquivoNormais) + nome;

            Bitmap b = Util.GetResizedImage(filePath, tamanhos.Width, tamanhos.Height);
            System.Drawing.Image resizedimg2 = (System.Drawing.Image)b;
            string imageSavePath = caminho; //"arquivos\\redimencionados\\"; //funfa            
            //string imageSavePath = Diretorio.ArquivoRedimencionados;
            string relativePath = imageSavePath + nome;
            resizedimg2.Save(Request.PhysicalApplicationPath + relativePath);//fim
            //resizedimg2.Save(relativePath);//fim

        }



        public JsonResult RoboRedimencionador()
        {
            try
            {
                var lista = arquivoDal.Listar();
                foreach (var item in lista)
                {
                    RedimensionarImagens(item.Nome, 300, 200, "arquivos\\miniaturas\\");
                    //Thread.Sleep(2000);
                }
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json(false + " " + ex.Message, JsonRequestBehavior.AllowGet);
            }
        }
        public void AcessoPastaArquivos(string nome)
        {
            //AcessoPastaArquivos para exclusao dos mesmos na pasta
            string fileName = nome;
            //string targetPath = Diretorio.ArquivoNormais;
            string targetPath = arquivosMin.ToString();
            string destFile = Path.Combine(targetPath, fileName);
            System.IO.File.Delete(Server.MapPath(destFile));

            string targetPathRedimencionados = arquivosRedimencionados;
            //string targetPathRedimencionados = Diretorio.ArquivoRedimencionados;
            string destFileRedimencionados = Path.Combine(targetPathRedimencionados, fileName);
            System.IO.File.Delete(Server.MapPath(destFileRedimencionados));

        }
        public void ExcluiArquivosNormais(string nome)
        {
            //AcessoPastaArquivos para exclusao dos mesmos na pasta
            string fileName = nome;
            //string targetPath = Diretorio.ArquivoNormais;
            string targetPath = arquivosNormais.ToString();
            string destFile = Path.Combine(targetPath, fileName);
            System.IO.File.Delete(Server.MapPath(destFile));
        }


        public void CarregarDropDowns()
        {
            ViewBag.Combustivel = combustivelDal.Listar().OrderBy(m => m.Nome);
            ViewBag.Tipo = tipoDal.Listar().OrderBy(m => m.Nome);
            ViewBag.Ano = anoModeloDal.ListarAno2();
            ViewBag.AnoFabricacao = anoModeloDal.ListarAnoFabricacao();
        }


        public void Diretorios()
        {
            ViewBag.Diretorio = Diretorio.ArquivoRedimencionados;
            ViewBag.DiretorioMin = Diretorio.ArquivoMin;
            ViewBag.DominioAppCliente = Diretorio.DominioAppCliente;
        }



        public List<Consulta> MontaConsulta(int idVeiculo)
        {
            List<Consulta> novalista = new List<Consulta>();

            novalista = ConsultaDal.Listar(idVeiculo);
            foreach (var item in novalista)
            {
                item.Multas = MultaDal.Listar(item.IdConsulta);
            }
            return novalista;
        }
        #endregion

    }
}