using AutoMapper;
using Objetivo.Conveniadas.Entidades.Entidades;
using Objetivo.Conveniadas.Servico.Base;
using Objetivo.Conveniadas.Site.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Objetivo.Conveniadas.Site.Controllers
{
    public class UnidadeFisicaController : Controller
    {
        private readonly IUnidadeFisicaServico _servicoUnidadeFisica;

        public UnidadeFisicaController(IUnidadeFisicaServico servicoUnidadeFisica)
        {
            _servicoUnidadeFisica = servicoUnidadeFisica;
        }

        MapperConfiguration config = new MapperConfiguration(cfq =>
        {
            cfq.CreateMap<UnidadeFisicaModel, UnidadeFisica>();
            cfq.CreateMap<UnidadeFisica, UnidadeFisicaModel>();
        });
        public ActionResult CadastroObjetivoCentral(string token)
        {
            try
            {
                if (token == null)
                    return View();

                var ws = new br.unip.sistemasead.wsSessao();

                var s = ws.ValidaSessao("001ab9cdd78844ff9db4e391721d0b47", token);

                if (s.IsValida)
                {
                    IMapper mapper = config.CreateMapper();
                    var info = mapper.Map<UnidadeFisica, UnidadeFisicaModel>(_servicoUnidadeFisica.DetealhesObjetivoCentral(s.IdUsuario));
                    
                    return View(info);
                }
                else
                    return View();
            }
            catch (Exception error)
            {
                TempData.Add("Class", "alert alert-danger alert-dismissible fade in");
                TempData.Add("Atencao", error.Message);
                return View();                
            }            
        }

        [HttpPost]
        public ActionResult CadastroObjetivoCentral(UnidadeFisicaModel dados)
        {
            try
            {
                if (_servicoUnidadeFisica.ObterPermissaoUsuario(dados.Usuario.Id))
                {
                    if (_servicoUnidadeFisica.AtualizarUnidadeObjetivoCentral(dados))
                    {
                        IMapper mapper = config.CreateMapper();
                        var info = mapper.Map<UnidadeFisica, UnidadeFisicaModel>(_servicoUnidadeFisica.DetealhesObjetivoCentral(dados.ID_UnidadeFisica));

                        TempData.Add("Class", "alert alert-success alert-dismissible fade in");
                        TempData.Add("Atencao", "Dados alterardos com sucesso.");

                        return View(info);
                    }
                    else
                    {
                        TempData.Add("Class", "alert alert-danger alert-dismissible fade in");
                        TempData.Add("Atencao", "Não foi possivel alterar os dados. Verifique suas permissões com o administrador do sistema.");                        
                        return View(dados);
                    }
                        
                    
                }
                else
                {
                    TempData.Add("Class", "alert alert-danger alert-dismissible fade in");
                    TempData.Add("Atencao", "Não foi possivel alterar os dados. Verifique suas permissões com o administrador do sistema.");
                    return View(dados);
                }
            }
            catch (Exception error)
            {
                TempData.Add("Class", "alert alert-danger alert-dismissible fade in");
                TempData.Add("Atencao", error.Message);
                return View(dados);
            }                       
        }
    }
}