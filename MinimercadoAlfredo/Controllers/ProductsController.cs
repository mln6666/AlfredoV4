using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MinimercadoAlfredo.Context;
using MinimercadoAlfredo.Models;

namespace MinimercadoAlfredo.Controllers
{
    public class ProductsController : Controller
    {
        private AlfredoContext db = new AlfredoContext();

        // GET: Products
        public ActionResult Index(int? lista)
        {
            IEnumerable<Product> products;



            if (lista == 1)
            {
                products = (from p in db.Products
                            where p.State == false
                            select p);

                return View("OffProducts", products.ToList());
            }
            else
            {
                if (lista == 2)
                {
                    products = db.Products.Include(p => p.Category);

                    return View("Record", products.ToList());
                }
                else
                {
                    if (lista == 3)
                    {
                        products = (from p in db.Products
                                        where p.Stock <= p.Minimum
                                        select p);

                        return View("Minimum", products.ToList());
                    }else
                    {

                        products = db.Products.ToList();

                        return View(products);
                    }
                }
            }
            
        }        

        //GET
        public ActionResult Deactivate(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.idCategory = new SelectList(db.Categories, "IdCategory", "CategoryName");
            return View();
        }

        // POST: Products/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdProduct,ProductDescription,ArticleNumber,Cost,WholeSalePrice,PublicPrice,UploadDate,Stock,Minimum,State,Image,idCategory")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idCategory = new SelectList(db.Categories, "IdCategory", "CategoryName", product.idCategory);
            return View(product);
        }

        [HttpPost, ActionName("Deactivate")]
        [ValidateAntiForgeryToken]
        public ActionResult DeactivateConfirmation(int? idProd)
        {
            Product prod = new Product();
            prod = db.Products.Find(idProd);

            if (ModelState.IsValid)
            {
                if (prod.State == true)
                {
                    prod.State = false;                    
                    ViewBag.Success = "El Producto se ha desactivado correctamente";
                }
                else
                {
                    prod.State = true;                    
                    ViewBag.Success = "El Producto se ha activado correctamente";
                }
                db.Entry(prod).State = EntityState.Modified;
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.idCategory = new SelectList(db.Categories.OrderBy(c => c.CategoryName), "IdCategory", "CategoryName", product.idCategory);
            return View(product);
        }

        // POST: Products/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdProduct,ProductDescription,ArticleNumber,Cost,WholeSalePrice,PublicPrice,UploadDate,Stock,Minimum,State,Image,idCategory")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idCategory = new SelectList(db.Categories, "IdCategory", "CategoryName", product.idCategory);
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Record");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
