using System;
using System.Text;
using System.Security.Cryptography;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Routing;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections.Generic;
using System.Configuration;


namespace LojaCarrosV2.Utilidade
{



    #region Criptigrafia de senhas
    public static class Criptografia
    {
        /// <summary>
        /// Classe de criptrografia utilizada para criptografar senhas 
        /// criada em 24-04-2015
        /// </summary>
        public static string Encriptar(string senha)
        {
            try
            {
                MD5 md5 = new MD5CryptoServiceProvider();

                md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(senha));

                byte[] criptografia = md5.Hash;

                StringBuilder resultado = new StringBuilder();

                for (int i = 0; i < criptografia.Length; i++)
                {

                    resultado.Append(criptografia[i].ToString("x"));
                }

                return resultado.ToString();
            }
            catch
            {
                throw;
            }
        }
    }
    #endregion


    #region Envia email
    public class EnviarEmail
    {
        /// <summary>
        /// davi franca maciel 17-04-2015
        /// </summary>
        /// <param name="smtpUserName">Email mailing usuário: eg tuanitpro</param>
        /// <param name="smtpPassword">Enviar e-mail senha</param>
        /// <param name="smtpHost">Hospedar-mail. vd smtp.gmail.com</param>
        /// <param name="smtpPort">Porta vd: 465</param>
        /// <param name="toEmail">Email recebido por exemplo nomenome@gmail.com</param>
        /// <param name="subject">objeto a</param>
        /// <param name="smtpClient.EnableSsl"> = false;//true envia local com gmail , false envio no servidor com dominio do cliente</param>
        /// <param name="body">O conteúdo da carta para</param>
        /// <returns>False-Failure Success-Verdadeiro</returns>
        public bool Enviar(string smtpUserName, string smtpPassword, string smtpHost, int smtpPort,
                    string toEmail, string subject, string body)
        {
            try
            {
                using (var smtpClient = new SmtpClient())
                {
                    smtpClient.EnableSsl = true;
                    smtpClient.Host = smtpHost;
                    smtpClient.Port = smtpPort;
                    smtpClient.UseDefaultCredentials = true;
                    smtpClient.Credentials = new NetworkCredential(smtpUserName, smtpPassword);
                    var msg = new MailMessage
                    {
                        IsBodyHtml = true,
                        BodyEncoding = Encoding.UTF8,
                        From = new MailAddress(smtpUserName),
                        Subject = subject,
                        Body = body,
                        Priority = MailPriority.Normal,
                    };

                    msg.To.Add(toEmail);

                    smtpClient.Send(msg);
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
    #endregion


    #region Util


    public static class Util
    {       
        public static void Alertar(string mensagem)
        {
            HttpContext.Current.Session["alert"] = mensagem;
        }


        public static string GerarSenhaAleatoria(int tamanho)
        {
            string caracteresPermitidos = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789!@$?_-*&#+";
            char[] senhaGerada = new char[tamanho];
            Random rd = new Random();
            for (int i = 0; i < tamanho; i++)
            {
                senhaGerada[i] = caracteresPermitidos[rd.Next(0, caracteresPermitidos.Length)];
            }
            return new string(senhaGerada);
        }


        public static Bitmap GetResizedImage(string lcFilename, int lnWidth, int lnHeight)
        {
            System.Drawing.Bitmap bmpOut = null;
            decimal lnRatio;
            int lnNewWidth = 0;
            int lnNewHeight = 0;
            try
            {
                Bitmap loBMP = new Bitmap(lcFilename);
                ImageFormat loFormat = loBMP.RawFormat;

                //*** If the image is smaller than a thumbnail just return it
                if (loBMP.Width == lnWidth && loBMP.Height == lnHeight)
                    return loBMP;

                if (loBMP.Width > loBMP.Height)
                {
                    lnRatio = (decimal)lnWidth / loBMP.Width;
                    lnNewWidth = lnWidth;
                    decimal lnTemp = loBMP.Height * lnRatio;
                    lnNewHeight = (int)lnTemp;
                }
                else
                {
                    lnRatio = (decimal)lnHeight / loBMP.Height;
                    lnNewHeight = lnHeight;
                    decimal lnTemp = loBMP.Width * lnRatio;
                    lnNewWidth = (int)lnTemp;
                }
                if (lnNewWidth > lnWidth)
                {
                    lnRatio = (decimal)lnWidth / lnNewWidth;
                    lnNewWidth = lnWidth;
                    decimal lnTemp = lnHeight * lnRatio;
                    lnNewHeight = (int)lnTemp;
                }
                if (lnNewHeight > lnHeight)
                {
                    lnRatio = (decimal)lnHeight / lnNewHeight;
                    lnNewHeight = lnHeight;
                    decimal lnTemp = lnNewWidth * lnRatio;
                    lnNewWidth = (int)lnTemp;
                }


                bmpOut = new Bitmap(lnNewWidth, lnNewHeight);
                Graphics g = Graphics.FromImage(bmpOut);

                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.FillRectangle(Brushes.Transparent, 0, 0, lnNewWidth, lnNewHeight);

                g.DrawImage(loBMP, 0, 0, lnNewWidth, lnNewHeight);
                loBMP.Dispose();
                g.Dispose();
            }

            catch
            {

                return null;

            }
            return bmpOut;
        }
    }
    #endregion


    #region Estados Brasil
    public class UnidadeFederativa
    {
        public string UF { get; set; }
        public static List<UnidadeFederativa> Listar()
        {
            return new List<UnidadeFederativa>{
                
                    new UnidadeFederativa{UF="AC"},
                    new UnidadeFederativa{UF="AL"},
                    new UnidadeFederativa{UF="AM"},
                    new UnidadeFederativa{UF="AP"},
                    new UnidadeFederativa{UF="BA"},
                    new UnidadeFederativa{UF="CE"},
                    new UnidadeFederativa{UF="DF"},
                    new UnidadeFederativa{UF="ES"},
                    new UnidadeFederativa{UF="GO"},
                    new UnidadeFederativa{UF="MA"},
                    new UnidadeFederativa{UF="MG"},
                    new UnidadeFederativa{UF="MS"},
                    new UnidadeFederativa{UF="MT"},
                    new UnidadeFederativa{UF="PA"},
                    new UnidadeFederativa{UF="PB"},
                    new UnidadeFederativa{UF="PE"},
                    new UnidadeFederativa{UF="PI"},
                    new UnidadeFederativa{UF="PR"},
                    new UnidadeFederativa{UF="RJ"},
                    new UnidadeFederativa{UF="RN"},
                    new UnidadeFederativa{UF="RO"},
                    new UnidadeFederativa{UF="RR"},
                    new UnidadeFederativa{UF="RS"},
                    new UnidadeFederativa{UF="SC"},
                    new UnidadeFederativa{UF="SE"},
                    new UnidadeFederativa{UF="SP"},
                    new UnidadeFederativa{UF="TO"}
            };
        }
    }
    #endregion


    #region remove caracteres especiais
    public static class NormalizeTextExtension
    {
        /// <summary>
        /// Remove caracteres especiais de uma string, substituindo os mesmos
        /// por letras quando possível.
        /// exemplo de uso //u.Nome = model.Nome.RemoveSpecialCharacters().ToString();
        /// </summary>
        /// <param name="text">Texto a ser tratado.</param>
        /// <returns>Nova string com caracteres especiais removidos.</returns>
        //public static string RemoveSpecialCharacters1(this string text)
        //{
        //    StringBuilder sbReturn = new StringBuilder();
        //    var arrayText =
        //        text.Normalize(NormalizationForm.FormD).ToCharArray();

        //    foreach (char letter in arrayText)
        //    {
        //        if (CharUnicodeInfo.GetUnicodeCategory(letter) !=
        //            UnicodeCategory.NonSpacingMark)
        //            sbReturn.Append(letter);
        //    }
        //    return sbReturn.ToString();
        //}
        public static string RemoveSpecialCharacters(string str)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < str.Length; i++)
            {
                if ((str[i] >= '0' && str[i] <= '9')
                    || (str[i] >= 'A' && str[i] <= 'z'
                        || (str[i] == ' ' || str[i] == '_' || str[i] == '-')))
                {
                    sb.Replace(" ", "-");

                    sb.Append(str[i]).Replace(" ", "-");
                }
            }

            return sb.ToString();
        }

    }
    #endregion


    #region Hepers
    public static class HtmlHelperExtension
    {
        public static MvcHtmlString LinkVoltar(
            this HtmlHelper html, string idLink,
            string textoLink = "Voltar")
        {
            string strLink = String.Format(
                "<a id=\"{0}\" href=\"javascript:history.go(-1);\" class = 'btn btn-default'>{1}</a>",
                idLink, textoLink); return new MvcHtmlString(strLink);
        }


       
        // As the text the: "<span class='glyphicon glyphicon-plus'></span>" can be entered
        public static MvcHtmlString ActionLinkDavi(this HtmlHelper htmlHelper,
                                             string text, string title, string action,
                                             string controller,
                                             object voloresRota = null,
                                             object atributosHtml = null)
        {
            UrlHelper urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);

            TagBuilder builder = new TagBuilder("a");
            builder.InnerHtml = text;
            builder.Attributes["title"] = title;
            builder.Attributes["href"] = urlHelper.Action(action, controller, voloresRota);
            builder.MergeAttributes(new RouteValueDictionary(HtmlHelper.AnonymousObjectToHtmlAttributes(atributosHtml)));

            return MvcHtmlString.Create(builder.ToString());
        }
    }
    #endregion
}
