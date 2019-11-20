using LojaCarrosV2.Domain.Entidade;
using LojaCarrosV2.Infra.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using System.Web.Http.Cors;

namespace LojaCarrosV2.Api.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/v1")]
    public class VeiculoController : ApiController
    {
        


        private VeiculoDal db = new VeiculoDal();



        // GET: api/Product clocar no plural pois é boa pratica
        [Route("empresa/{idEmpresa:int?}/veiculos")]
        public HttpResponseMessage GetVeiculos(int? idEmpresa)
        {
            var result = db.ListarByFilto(null, null, null, null, idEmpresa).ToList();

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        //public HttpResponseMessage GetVeiculos(string tipo, string marca, int? anoInicio, int? anoFim)
        //{
        //    var result = db.ListarByFilto(tipo, marca, anoInicio, anoFim).ToList();

        //    return Request.CreateResponse(HttpStatusCode.OK, result);
        //}

        // GET: api/Product/5
        
        [Route("empresa/{idEmpresa:int?}/veiculo/{idVeiculo}")]
        public HttpResponseMessage GetVeiculos(int? idEmpresa, int idVeiculo)
        {
            List<string> list = new List<string>();



            var veiculo = db.ListarById(idVeiculo, true, idEmpresa);

            ArquivoDal ad = new ArquivoDal();
            var arquivos = ad.ListarArquivosByIdVeiculo(idVeiculo);

            Random rnd = new Random();
            IOrderedEnumerable<Veiculo> Items = db.Listar(null, true, idEmpresa).AsEnumerable().OrderBy((i => rnd.Next()));
            var populares = Items.Where(x => x.Arquivo.Nome != "_semfoto.jpg").Take(4);

            //var result = ad.ListarArquivosByIdVeiculo(idVeiculo);
            return Request.CreateResponse(HttpStatusCode.OK, new { veiculo, arquivos, populares });
        }
        
        [Route("empresa/{idEmpresa:int?}/veiculosdestaque")]
        public HttpResponseMessage GetVeiculosDestaque(int? idEmpresa)
        {

            MarcaDal mdc = new MarcaDal();
            var marcaCarro = mdc.ListarByTipo("carro", true, idEmpresa);
            MarcaDal mdm = new MarcaDal();
            var marcaMoto = mdm.ListarByTipo("moto", true, idEmpresa);


            Random rnd = new Random();
            VeiculoDal vd = new VeiculoDal();
            IOrderedEnumerable<Veiculo> Items = vd.Listar(null, true,idEmpresa).AsEnumerable().OrderBy((i => rnd.Next()));

            var destaques = Items.Where(x => x.Arquivo.Nome != "_semfoto.jpg").Where(x => x.Destaque == true).Take(4);


            return Request.CreateResponse(HttpStatusCode.OK, new { marcaCarro, marcaMoto, destaques });
        }
        
        [Route("tipos")]
        public HttpResponseMessage GetTipos()
        {
            TipoDal d = new TipoDal();
            var result = d.Listar().OrderBy(m => m.Nome);

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        
        [Route("empresa/{idEmpresa:int?}/tipo/{tipo}/marcas")]
        public HttpResponseMessage GetMarcas(string tipo,int? idEmpresa)
        {
            //Thread.Sleep(7000);
            MarcaDal d = new MarcaDal();
            var result = d.ListarByTipo(tipo, true, idEmpresa);

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        
        [Route("combustiveis")]
        public HttpResponseMessage GetCombustiveis()
        {
            CombustivelDal d = new CombustivelDal();
            var result = d.Listar().OrderBy(m => m.Nome);

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }


        [Route("marcas/{marca}/veiculos")]
        public HttpResponseMessage GetVeiculosByMarca(string marca)
        {
            var result = db.ListarByFilto(null, marca, null, null,null);

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
























        /// <summary>
        /// exemplo para persistir os dados pelo fidler
        /// </summary>
        /// <param name="product"></param>
        /// <returns>
        /// form metode post
        /// Content-Type: application/json;charset-utf8
        /// 
        ///  {
        /// "id": 0,
        /// "title": "forno microondas",
        /// "price": 299,
        /// "acquireDate": "2015-12-16T22:38:32.273",
        /// "isActive": true,
        /// "categoryId": 6
        /// }
        /// </returns>
        [HttpPost]
        [Route("veiculo")]
        public HttpResponseMessage PostVeiculo(Veiculo veiculo)
        {
            if (veiculo == null)
                return Request.CreateResponse(HttpStatusCode.BadRequest);

            try
            {
                db.Salvar(veiculo);

                var result = veiculo;
                //retornar OK ou Created
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, "falha ao incluir veiculo");
            }
        }



        // quando vc vai atualizar parcialmente uma classe 
        [HttpPatch]
        [Route("veiculo")]
        public HttpResponseMessage PatchVeiculo(Veiculo veiculo)
        {
            if (veiculo == null)
                return Request.CreateResponse(HttpStatusCode.BadRequest);

            try
            {

                db.Salvar(veiculo);

                var result = veiculo;
                //retornar OK ou Created
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, "falha ao incluir produto");
            }
        }

        // put altera Update
        [HttpPut]
        [Route("veiculo")]
        public HttpResponseMessage PutVeiculo(Veiculo veiculo)
        {
            if (veiculo == null)
                return Request.CreateResponse(HttpStatusCode.BadRequest);

            try
            {

                db.Salvar(veiculo);
                var result = veiculo;
                //retornar OK ou Created
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, "falha ao incluir produto");
            }
        }



        [HttpDelete]
        [Route("veiculos")]
        public HttpResponseMessage DeleteVeiculo(int idVeiculo)
        {
            if (idVeiculo <= 0)
                return Request.CreateResponse(HttpStatusCode.BadRequest);

            try
            {
                db.Excluir(idVeiculo);


                //retornar OK ou Created
                return Request.CreateResponse(HttpStatusCode.OK, "Veiculo excluido");
            }
            catch (Exception)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, "falha ao incluir veiculo");
            }
        }


        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
