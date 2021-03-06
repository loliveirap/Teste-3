﻿using AutoMapper;
using Objetivo.Conveniadas.Entidades.Entidades;
using Objetivo.Conveniadas.Servico.Base;
using Objetivo.Conveniadas.Servico.Servicos;
using Objetivo.Conveniadas.Site.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Objetivo.Conveniadas.Site.Controllers
{
    [Authorize]
    public class ConveniadaController : Controller
    {
        private readonly IUnidadeFisicaServico _servicoUnidadeFisica;

        public ConveniadaController(IUnidadeFisicaServico servicoUnidadeFisica)
        {
            _servicoUnidadeFisica = servicoUnidadeFisica;
        }

        MapperConfiguration config = new MapperConfiguration(cfq =>
        {
            cfq.CreateMap<UnidadeFisicaModel, UnidadeFisica>();
            cfq.CreateMap<UnidadeFisica, UnidadeFisicaModel>();
        });

        #region GET
        public ActionResult Cadastrar()
        {
            return View();
        }

        public ActionResult Editar(int id, string tipo)
        {
            IMapper mapper = config.CreateMapper();
            var info = mapper.Map<UnidadeFisica, UnidadeFisicaModel>(_servicoUnidadeFisica.Detealhes(id, tipo));
            return View(info);
        }

        public ActionResult Excluir()
        {
            return View();
        }

        public ActionResult GeraPDF(int id)
        {
            var dados = _servicoUnidadeFisica.Detealhes(id, "Conveniada");
            return File(PDFServico.ReplacePdfForm(dados), "Application/pdf");
        }
        #endregion

        #region POST

        [HttpPost]
        public ActionResult Cadastrar(UnidadeFisicaModel dadosTela)
        {
            if (_servicoUnidadeFisica.CadastrarConveniada(dadosTela))
            {
                TempData.Add("Class", "alert alert-success alert-dismissible fade in");
                TempData.Add("Atencao", "Conveniada cadastrada com sucesso.");
                return RedirectToAction("Cadastrar", "Conveniada");
            }
            else
            {
                TempData.Add("Class", "alert alert-danger alert-dismissible fade in");
                TempData.Add("Atencao", "Não foi possivel realizar o cadastro!");
                return View(dadosTela);
            }
        }

        [HttpPost]
        public ActionResult Editar(UnidadeFisicaModel dadosTela)
        {
            if (_servicoUnidadeFisica.AtualizarConveniada(dadosTela))
            {
                TempData.Add("Class", "alert alert-success alert-dismissible fade in");
                TempData.Add("Atencao", "Dados alterados com sucesso.");
                return this.Editar(dadosTela.Id, dadosTela.Tipo);
            }
            else
            {
                TempData.Add("Class", "alert alert-danger alert-dismissible fade in");
                TempData.Add("Atencao", "Não foi possivel realizar as alterações!");
                return View(dadosTela);
            }
        }
        #endregion
    }
}