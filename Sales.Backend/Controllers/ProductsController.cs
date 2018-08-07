namespace Sales.Backend.Controllers
{
    using System;
    using System.Data.Entity;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using Common.Models;
    using Models;
    using Sales.Backend.Helpers;

    public class ProductsController : Controller
    {
        private LocalDataContext db = new LocalDataContext();

        public async Task<ActionResult> Index()
        {
            return View(await db.Products.ToListAsync());
        }

        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var product = await db.Products.FindAsync(id);

            if (product == null)
            {
                return HttpNotFound();
            }

            return View(product);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ProductView view)
        {
            if (ModelState.IsValid)
            {
                var pic = string.Empty;
                var folder = "~/Content/Products";

                if (view.Image != null)
                {
                    pic = FileHelper.UploadPhoto(view.Image, folder);
                    pic = string.Format("{0}/{1}", folder, pic);
                }

                view.ImagePath = pic;
                var product = this.ToProduct(view);
                db.Products.Add(product);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(view);
        }

        private Product ToProduct(ProductView view)
        {
            return new Product
            {
                Description = view.Description,
                ImagePath = view.ImagePath,
                IsAvailable = view.IsAvailable,
                Price = view.Price,
                ProductId = view.ProductId,
                PublishOn = view.PublishOn,
            };
        }

        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var product = await db.Products.FindAsync(id);

            if (product == null)
            {
                return HttpNotFound();
            }

            var view = this.ToView(product);
            return View(view);
        }

        private ProductView ToView(Product product)
        {
            return new ProductView
            {
                Description = product.Description,
                ImagePath = product.ImagePath,
                IsAvailable = product.IsAvailable,
                Price = product.Price,
                ProductId = product.ProductId,
                PublishOn = product.PublishOn,
            };
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ProductView view)
        {
            if (ModelState.IsValid)
            {
                var pic = view.ImagePath;
                var folder = "~/Content/Products";

                if (view.Image != null)
                {
                    pic = FileHelper.UploadPhoto(view.Image, folder);
                    pic = string.Format("{0}/{1}", folder, pic);
                }

                view.ImagePath = pic;
                var product = this.ToProduct(view);
                db.Entry(product).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(view);
        }

        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var product = await db.Products.FindAsync(id);

            if (product == null)
            {
                return HttpNotFound();
            }

            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var product = await db.Products.FindAsync(id);
            db.Products.Remove(product);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
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