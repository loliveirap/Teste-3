using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Objetivo.Conveniadas.Site.Controllers
{
    public class AutenticacaoController : Controller
    {
        #region Action = GET()
        public ActionResult LogOn()
        {
            return View();
        }
        #endregion

        #region Action = POST()

        [HttpPost]
        public ActionResult LogOn(string txtUsuario, string txtSenha)
        {
            if(txtUsuario == "convenios" || txtUsuario == "CONVENIOS" || txtUsuario == "Convenios" && txtSenha == "admin2017")
            {
                FormsAuthentication.SetAuthCookie(txtUsuario, false);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData.Add("Erro", "Usuário ou senha incorretos.");
                return View();
            }
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("LogOn", "Autenticacao");
        }
        #endregion
    }
}