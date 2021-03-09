using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using WebSite.Models;
using WebSite.Repository;

namespace WebSite.Controllers
{
    public class ClienteController : Controller
    {
        private ClienteRepository repository = new ClienteRepository();
        
        // GET: Cliente
        public ActionResult Index()
        {
            return View();
        }

        // GET: Obtem os clientes registrados
        public JsonResult GetClientes()
        {
            JsonResult result = new JsonResult();

            try
            {
                string search = Request.Form.GetValues("search[value]")[0];
                string draw = Request.Form.GetValues("draw")[0];
                int start = Convert.ToInt32(Request.Form.GetValues("start")[0]);
                int length = Convert.ToInt32(Request.Form.GetValues("length")[0]);

                long totalRecords = repository.GetTotalRecords(search);

                result = Json(new
                {
                    draw = Convert.ToInt32(draw),
                    recordsTotal = totalRecords,
                    recordsFiltered = totalRecords,
                    data = repository.List(start, length, search),
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                result = Json(ex, JsonRequestBehavior.AllowGet);
            }
            return result;
        }

        // POST: Adiciona novo cliente
        [HttpPost]
        public JsonResult Add(Cliente cliente)
        {
            if (!this.ModelState.IsValid)
            {
                List<string> erros = (from item in ModelState.Values
                                      from error in item.Errors
                                      select error.ErrorMessage).ToList();

                Response.StatusCode = 400;
                return Json(string.Join("-", erros));
            }

            return Json(new { status = repository.Add(cliente) });
        }

        // GET: Obtem cliente para atualizar
        public JsonResult Find(long id)
        {
            return Json(repository.Find(id), JsonRequestBehavior.AllowGet);
        }

        // POST: Atualiza cliente
        [HttpPost]
        public JsonResult Update(Cliente cliente)
        {
            if (!this.ModelState.IsValid)
            {
                List<string> erros = (from item in ModelState.Values
                                      from error in item.Errors
                                      select error.ErrorMessage).ToList();

                Response.StatusCode = 400;
                return Json(string.Join("-", erros));
            }

            return Json(new { status = repository.Update(cliente) });
        }

        // POST: Excluir cliente
        [HttpPost]
        public JsonResult Delete(long id)
        {
            return Json(new { status = repository.Delete(id) });
        }
    }
}