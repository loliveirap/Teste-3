using AutoMapper;
using Objetivo.Conveniadas.Entidades.Entidades;
using Objetivo.Conveniadas.Servico.Base;
using Objetivo.Conveniadas.Site.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Unip.GFA.Site.Models.Base;

namespace Objetivo.Conveniadas.Site.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IUnidadeFisicaServico _servicoUnidadeFisica;

        public HomeController(IUnidadeFisicaServico servicoUnidadeFisica)
        {
            _servicoUnidadeFisica = servicoUnidadeFisica;
        }

        MapperConfiguration config = new MapperConfiguration(cfq =>
        {
            cfq.CreateMap<UnidadeFisicaModel, UnidadeFisica>();
            cfq.CreateMap<UnidadeFisica, UnidadeFisicaModel>();
        });
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Pesquisar(string txtFiltro, string optTipoFiltro)
        {
            ViewData["inputTxtFiltro"] = txtFiltro;

            var _tipoFiltro = new FiltroModel();
            ViewBag.optTipoFiltro = _tipoFiltro.Filtro(optTipoFiltro);

            if (txtFiltro != null && txtFiltro != "" && optTipoFiltro != "0")
            {
                IMapper mapper = config.CreateMapper();
                var registros = mapper.Map<IEnumerable<UnidadeFisica>, IEnumerable<UnidadeFisicaModel>>(_servicoUnidadeFisica.Listar(txtFiltro, optTipoFiltro));

                if (registros.Count() == 0)
                {
                    TempData.Add("Nulo", "Nenhum registro encontrado.");
                }
                return View(registros);
            }
            else
            {
                var registros = new List<UnidadeFisicaModel>();
                return View(registros);
            }
        }

        #region POST


        [HttpPost]
        [ActionName("Pesquisar")]
        public ActionResult PesquisarPost(string txtFiltro, string optTipoFiltro)
        {
            return this.Pesquisar(txtFiltro, optTipoFiltro);
        }
        #endregion
    }
}