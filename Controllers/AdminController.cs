using Newtonsoft.Json;
using ProjectEcommerce.DAL;
using ProjectEcommerce.Models;
using ProjectEcommerce.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectEcommerce.Controllers
{
    public class AdminController : Controller
    {
        #region GenericUnitOfWork

        // GET: Admin
        public GenericUnitOfWork _unitOfWork = new GenericUnitOfWork();

        #endregion

        #region "DASHBOARD"
        public ActionResult Dashboard()
        {
            return View();
        }
        #endregion

        #region "ACTION RESULT CATEGORIA"

        public List<SelectListItem> GetCategory()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            var cat = _unitOfWork.GetRepositoryInstance<Tbl_Category>().GetAllRecords();
            foreach (var item in cat)
            {
                list.Add(new SelectListItem { Value = item.CategoryId.ToString(), Text = item.CategoryName });
            }
            return list;
        }
        public ActionResult Categorias(Tbl_Category tbl)
        {
            List<Tbl_Category> allcategories = _unitOfWork.GetRepositoryInstance<Tbl_Category>()
                                                .GetAllRecordsIQueryable()
                                                .Where(i => i.IsDelete == false).ToList();

            return View(allcategories);
        }
        public ActionResult AddCategory()
        {
            //ViewBag.CategoryList = GetCategory();
            return UpdateCategory(0);
        }
        [HttpPost]
        public ActionResult AddCategory(Tbl_Category tbl)
        {
            _unitOfWork.GetRepositoryInstance<Tbl_Category>().Add(tbl);
            return RedirectToAction("Categorias");
        }
        public ActionResult UpdateCategory(int categoryId)
        {
            CategoryDetail cd;

            if (categoryId != null)
            {
                cd = JsonConvert.DeserializeObject<CategoryDetail>
                    (JsonConvert.SerializeObject(_unitOfWork.GetRepositoryInstance<Tbl_Category>()
                    .GetFirstorDefault(categoryId)));
            }
            else
            {
                cd = new CategoryDetail();
            }
            return View("UpdateCategory", cd);
        }
        public ActionResult CategoriaEdit(int catId)
        {
            return View(_unitOfWork.GetRepositoryInstance<Tbl_Category>().GetFirstorDefault(catId));
        }
        [HttpPost]
        public ActionResult CategoriaEdit(Tbl_Category tbl)
        {
            _unitOfWork.GetRepositoryInstance<Tbl_Category>().Update(tbl);
            return RedirectToAction("Categorias");
        }
        #endregion

        #region "ACTION RESULT PRODUTO"
        public ActionResult Produto()
        {
            return View(_unitOfWork.GetRepositoryInstance<Tbl_Product>().GetProduct());
        }
        public ActionResult ProdutoEdit(int productId)
        {
            ViewBag.CategoryList = GetCategory();
            return View(_unitOfWork.GetRepositoryInstance<Tbl_Product>().GetFirstorDefault(productId));
        }

        [HttpPost]
        public ActionResult ProdutoEdit(Tbl_Product tbl, HttpPostedFileBase file)
        {
            string imagem = null;
            if (file != null)
            {
                imagem = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(
                                            Server.MapPath("~/ProductImages/"), imagem);
                file.SaveAs(path);
            }
            tbl.ProductImage = file != null ? imagem : tbl.ProductImage;
            tbl.CreatedDate = DateTime.Now;
            _unitOfWork.GetRepositoryInstance<Tbl_Product>().Update(tbl);
            return RedirectToAction("Produto");
        }
        public ActionResult ProductAdd()
        {
            ViewBag.CategoryList = GetCategory();
            return View();
        }

        [HttpPost]
        public ActionResult ProductAdd(Tbl_Product tbl, HttpPostedFileBase file)
        {
            string imagem = null;
            if (file != null)
            {
                imagem = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(
                                            Server.MapPath("~/ProductImages"), imagem);
                file.SaveAs(path);
            }
            tbl.ProductImage = imagem;
            tbl.CreatedDate = DateTime.Now;
            _unitOfWork.GetRepositoryInstance<Tbl_Product>().Add(tbl);
            return RedirectToAction("Produto");
        }
    }
    #endregion
}