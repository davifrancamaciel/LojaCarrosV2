using LojaCarrosV2.Web.Infra;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace LojaCarrosV2.Web.Controllers
{
    public class DetranController : Controller
    {
        private CookieContainer _cookies;
        private readonly string urlBaseDetran;
        private readonly string paginaPrincipalDetran;
        private readonly string paginaCaptchaDetran;

        private readonly string paginaPrincipalDetranConsultaCadastro;
        private readonly string paginaCaptchaDetranConsultaCadastro;
        public DetranController()
        {
            _cookies = new CookieContainer();
            urlBaseDetran = "http://www2.detran.rj.gov.br/portal/";

            paginaPrincipalDetran = "multas/nadaConsta";
            paginaCaptchaDetran = "multas/captcha_image";

            paginaPrincipalDetranConsultaCadastro = "veiculos/consultaCadastro";
            paginaCaptchaDetranConsultaCadastro = "veiculos/captcha_image";
        }

        public ActionResult Rj()
        {
            return View();
        }




        public JsonResult GetCaptchaDetran()
        {

            try
            {
                var htmlResult = string.Empty;

                using (var wc = new Infra.CookieAwareWebClient(_cookies))
                {
                    wc.Headers[HttpRequestHeader.UserAgent] = "Mozilla/4.0 (compatible; Synapse)";
                    wc.Headers[HttpRequestHeader.KeepAlive] = "300";
                    htmlResult = wc.DownloadString(urlBaseDetran + paginaPrincipalDetran);
                }

                if (htmlResult.Length > 0)
                {
                    var wc2 = new Infra.CookieAwareWebClient(_cookies);
                    wc2.Headers[HttpRequestHeader.UserAgent] = "Mozilla/4.0 (compatible; Synapse)";
                    wc2.Headers[HttpRequestHeader.KeepAlive] = "300";
                    byte[] data = wc2.DownloadData(urlBaseDetran + paginaCaptchaDetran);

                    Session["cookies"] = _cookies;

                    return Json("data:image/jpeg;base64," + Convert.ToBase64String(data, 0, data.Length), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {

                throw;
            }
            return null;


        }
        public ActionResult ConsultarDadosDetran(string renavan, string captcha, int id)
        {
            var msg = string.Empty;
            var resp = ObterDadosDetran(renavan, captcha);

            if (resp.Contains("Este veÃ­culo nÃ£o consta no cadastro do Detran-RJ"))
                msg += "Este veículo não consta no cadastro do Detran-RJ";

            if (resp.Contains("CÃ³digo de SeguranÃ§a</b> corretamente!"))
                msg += "Os caracteres não conferem com a imagem";
            if (resp.Contains("NÃ£o hÃ¡ multa para o renavam"))
                msg += "Não há multa para o renavam " + renavan;


            var dadosConsulta = resp.Length > 0 ? FormatarDadosDetran.MontarObjVeiculo(renavan, resp) : null;


            return Json(
                new
                {
                    erro = msg,
                    dados = dadosConsulta
                },
                JsonRequestBehavior.DenyGet);
        }

        private string ObterDadosDetran(string renavan, string aCaptcha)
        {
            _cookies = (CookieContainer)Session["cookies"];

            var request = (HttpWebRequest)WebRequest.Create(urlBaseDetran + paginaPrincipalDetran);
            request.ProtocolVersion = HttpVersion.Version10;
            request.CookieContainer = _cookies;
            request.Method = "POST";

            var postData = string.Empty;
            postData += "&data%5BMultas%5D%5B";
            postData += "renavam%5D=" + new Regex(@"[^\d]").Replace(renavan, string.Empty) + "&";
            postData += "data%5BMultas%5D%5Bcode%5D=" + aCaptcha;


            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = byteArray.Length;

            var dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();

            var stHtml = new StreamReader(request.GetResponse().GetResponseStream(), Encoding.GetEncoding("ISO-8859-1"));
            return stHtml.ReadToEnd();

            //_method=POST&data%5BMultas%5D%5Brenavam%5D=819052329&data%5BMultas%5D%5Bcode%5D=JVOP


        }




        public JsonResult GetCaptchaDetranCadastroVeiculo()
        {

            var htmlResult = string.Empty;

            using (var wc = new Infra.CookieAwareWebClient(_cookies))
            {
                wc.Headers[HttpRequestHeader.UserAgent] = "Mozilla/4.0 (compatible; Synapse)";
                wc.Headers[HttpRequestHeader.KeepAlive] = "300";
                htmlResult = wc.DownloadString(urlBaseDetran + paginaPrincipalDetranConsultaCadastro);
            }

            if (htmlResult.Length > 0)
            {
                var wc2 = new Infra.CookieAwareWebClient(_cookies);
                wc2.Headers[HttpRequestHeader.UserAgent] = "Mozilla/4.0 (compatible; Synapse)";
                wc2.Headers[HttpRequestHeader.KeepAlive] = "300";
                byte[] data = wc2.DownloadData(urlBaseDetran + paginaCaptchaDetranConsultaCadastro);

                Session["cookies"] = _cookies;

                return Json("data:image/jpeg;base64," + Convert.ToBase64String(data, 0, data.Length), JsonRequestBehavior.AllowGet);
            }

            return null;

        }

        public ActionResult ConsultarDadosDetranCadastroVeiculo(string placa, string captcha, int id)
        {
            var msg = string.Empty;
            var resp = ObterDadosDetranCadastroVeiculo(placa, captcha);

            if (resp.Contains("Este veÃ­culo nÃ£o consta no cadastro do Detran-RJ"))
                msg += "Este veículo não consta no cadastro do Detran-RJ";

            if (resp.Contains("CÃ³digo de SeguranÃ§a</b> corretamente!"))
                msg += "Os caracteres não conferem com a imagem";
            if (resp.Contains("NÃ£o hÃ¡ multa para o renavam"))
                msg += "Não há multa para o renavam " + placa;


            //var dadosConsulta = resp.Length > 0 ? FormatarDadosDetran.MontarObjVeiculo(placa, resp) : null;

            //if (!string.IsNullOrEmpty(dadosConsulta.QtdMultas) && id > 0)
            //{
            //    int idConsulta = ConsultaDal.Salvar(Mapear(dadosConsulta, id));
            //    foreach (var item in dadosConsulta.Multas)
            //    {
            //        item.IdConsulta = idConsulta;
            //        MultaDal.Salvar(MapearMulta(item));
            //    }
            //}
            return Json(
                new
                {
                    erro = msg,
                    dados = resp//dadosConsulta
                },
                JsonRequestBehavior.DenyGet);
        }

        private string ObterDadosDetranCadastroVeiculo(string placa, string aCaptcha)
        {
            _cookies = (CookieContainer)Session["cookies"];

            var request = (HttpWebRequest)WebRequest.Create(urlBaseDetran + paginaPrincipalDetranConsultaCadastro);
            request.ProtocolVersion = HttpVersion.Version10;
            request.CookieContainer = _cookies;
            request.Method = "POST";

            var postData = string.Empty;
            postData += "&data%5placa%5D=" + placa;
            //postData += "renavam%5D=" + new Regex(@"[^\d]").Replace(renavan, string.Empty) + "&";
            postData += "&data%5Bcode%5D=" + aCaptcha;


            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = byteArray.Length;

            var dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();

            var stHtml = new StreamReader(request.GetResponse().GetResponseStream(), Encoding.GetEncoding("ISO-8859-1"));
            return stHtml.ReadToEnd();

            //_method=POST&data%5BMultas%5D%5Brenavam%5D=819052329&data%5BMultas%5D%5Bcode%5D=JVOP
            //data%5Bplaca%5D=kmx6260&data%5Bcode%5D=CEJN

        }
    }
}