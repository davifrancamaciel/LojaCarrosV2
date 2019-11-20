using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace LojaCarrosV2.Web.classes
{
    public static class Constantes
    {
        
        public static int IDEMPRESA
        {
            get
            {
                return Convert.ToInt32( ConfigurationManager.AppSettings["IDEMPRESA"]);
            }
        }
        public static string DiretorioArquivo
        {
            get
            {
                return ConfigurationManager.AppSettings["DiretorioArquivo"].ToString();
            }
        }
        public static string DiretorioPainel
        {
            get
            {
                return ConfigurationManager.AppSettings["DiretorioPainel"].ToString();
            }
        }
        public static string ArquivoMin
        {
            //diretorio de arquivos normais 
            get
            {
                return ConfigurationManager.AppSettings["DiretorioArquivoMin"].ToString();
            }
        }
        
    }
}