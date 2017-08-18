using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Unip.GFA.Site.Models;
using System.Web.Mvc;
using System.Linq;

namespace Unip.GFA.Site.Models.Base
{
    public class FiltroModel
    {
        public IList<SelectListItem> Filtro(string tipoFiltro)
        {
            var optTipoFiltro = new List<SelectListItem>();
            optTipoFiltro.Add(new SelectListItem { Value = "0", Text = "Selecione um Filtro" });
            optTipoFiltro.Add(new SelectListItem { Value = "CNPJ", Text = "CNPJ" });
            optTipoFiltro.Add(new SelectListItem { Value = "Escola", Text = "Escola" });
            optTipoFiltro.Add(new SelectListItem { Value = "Endereço", Text = "Endereço" });
            optTipoFiltro.Add(new SelectListItem { Value = "Razão Social", Text = "Razão Social / Prefeitura" });            
            optTipoFiltro.Add(new SelectListItem { Value = "Todos", Text = "Todos" });

            if (tipoFiltro != null) { optTipoFiltro.First(x => x.Value == tipoFiltro).Selected = true; }

            return optTipoFiltro;
        }
    }
}