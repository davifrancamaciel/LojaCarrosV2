using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaCarrosV2.Infra.Interface
{
    public interface IRepositorioBase<T>
    {
        List<T> Listar();

    }
}
