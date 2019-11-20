using AutoMapper;
using LojaCarrosV2.Domain.Entidade;
using LojaCarrosV2.PainelWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LojaCarrosV2.PainelWeb.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public override string ProfileName
        {
            get
            {
                return "DomainToViewModelMappingProfile";
            }
        }
        protected override void Configure()
        {
            Mapper.CreateMap<Consulta, ConsultaVM>();
            Mapper.CreateMap<Multa, MultaVM>();
        }
    }
}