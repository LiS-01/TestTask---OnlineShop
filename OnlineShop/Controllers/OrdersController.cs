using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data;
using OnlineShop.Models;

namespace OnlineShop.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ShopContext _context;

        public OrdersController(ShopContext context)
        {
            _context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            using (_context)
            {
                var orders = from o in _context.Orders
                             select new Order
                             {
                                 ID = o.ID,
                                 ClientID = o.ClientID,
                                 Client = o.Client,
                                 ProductID = o.ProductID,
                                 Product = o.Product,
                                 Quantity = o.Quantity,
                                 Status = o.Status,
                                 OrderSum = o.Quantity * o.Product.Price,
                             };
                return View(await orders.ToListAsync());
            }
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Client)
                .Include(o => o.Product)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            ClientDropDownList();
            ProductDropDownList();
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("ID,ClientID,ProductID,ProductTitle,Quantity,Status")] Order order)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var orders = _context.Add(order);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                ClientDropDownList(order.ClientID);
                ProductDropDownList(order.ProductID);
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to save changes");
            }
            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
            if (order == null)
            {
                return NotFound();
            }
            ClientDropDownList(order.ClientID);
            ProductDropDownList(order.ProductID);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderToUpdate = await _context.Orders
                .Include(o => o.Client)
                .Include(o => o.Product)
                .FirstOrDefaultAsync(o => o.ID == id);

            if (await TryUpdateModelAsync<Order>(orderToUpdate,
                "",
                o => o.Client, o => o.ClientID, o => o.Product, o => o.ProductID, o => o.Quantity, o => o.Status, o => o.OrderSum))
            {
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
                return RedirectToAction(nameof(Index));
            }


            /*ViewData["ClientID"] = new SelectList(_context.Clients, "ID", "Name", order.ClientID);
            ViewData["ProductID"] = new SelectList(_context.Products, "ID", "Title", order.ProductID);*/
            ClientDropDownList(orderToUpdate.ClientID);
            ProductDropDownList(orderToUpdate.ProductID);
            return View(orderToUpdate);
        }

        private void ClientDropDownList(object selectedClient = null)
        {
            var clientsQuery = from c in _context.Clients
                               orderby c.Name
                               select c;
            ViewBag.ClientID = new SelectList(clientsQuery.AsNoTracking(), "ID", "Name", selectedClient);
        }
        private void ProductDropDownList(object selectedProduct = null)
        {
            var productsQuery = from p in _context.Products
                               orderby p.Title
                               select p;
            ViewBag.ProductID = new SelectList(productsQuery.AsNoTracking(), "ID", "Title", selectedProduct);
        }
        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Client)
                .Include(o => o.Product)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.ID == id);
        }
    }
}
