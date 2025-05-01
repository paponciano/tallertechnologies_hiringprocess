using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly AppDbContext _context;

    public ProductsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult> Get()
    {
        var products = await _context.Products.ToListAsync();

        if (products.Count == 0)
            return NotFound();
        else
            return Ok(products);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult> Get(int id)
    {
        var product = await _context.Products.FirstOrDefaultAsync(f => f.Id == id);

        if (product == null)
            return NotFound();
        else
            return Ok(product);
    }

    [HttpPost]
    public async Task<bool> Post(Product newProduct)
    {
        _context.Products.Add(newProduct);
        return await _context.SaveChangesAsync() >= 1;
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<bool> Put(int id, Product updatableProduct)
    {
        var dbProduct = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
        if (dbProduct != null)
        {
            dbProduct.Name = updatableProduct.Name;
            dbProduct.Price = updatableProduct.Price;
        }

        return await _context.SaveChangesAsync() >= 1;
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<bool> Delete(int id)
    {
        var dbProduct = await _context.Products.FirstOrDefaultAsync(f => f.Id == id);

        if (dbProduct == null)
            throw new Exception("Can't read the product.");

        _context.Products.Remove(dbProduct);

        return await _context.SaveChangesAsync() >= 1;
    }
}
