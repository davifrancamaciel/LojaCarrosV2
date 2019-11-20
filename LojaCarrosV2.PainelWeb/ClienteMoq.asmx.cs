using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace LojaCarrosV2.PainelWeb
{
    /// <summary>
    /// Summary description for ClienteMoq
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class ClienteMoq : System.Web.Services.WebService
    {

        [WebMethod]
        public List<LojaCarrosV2.Infra.Repositorio.ClienteMoq> Lista()
        {
            LojaCarrosV2.Infra.Repositorio.ClienteMoqDal infra = new LojaCarrosV2.Infra.Repositorio.ClienteMoqDal();
            return infra.Listar();
        }
    }


    
}
