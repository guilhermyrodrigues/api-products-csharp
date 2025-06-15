using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ProductController : ControllerBase
{
    private readonly AppDbContext _context;
    public ProductController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetAllProducts()
    {
        var products = _context.Products.ToList();
        return Ok(products);
    }

    [HttpGet("{id}")]
    public IActionResult GetProductById(int id)
    {
        var product = _context.Products.Find(id);
        if (product == null)
        {
            return NotFound("Produto não encontrado.");
        }
        return Ok(product);
    }

    [HttpPost]
    public IActionResult CreateProduct([FromBody] Product product)
    {
        if (product == null || string.IsNullOrEmpty(product.Name) || product.Price <= 0)
        {
            return BadRequest("Dados inválidos.");
        }

        _context.Products.Add(product);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateProduct(int id, [FromBody] Product product)
    {
        if (product == null || string.IsNullOrEmpty(product.Name) || product.Price <= 0)
        {
            return BadRequest("Dados inválidos.");
        }

        var existingProduct = _context.Products.Find(id);
        if (existingProduct == null)
        {
            return NotFound("Produto não encontrado.");
        }

        existingProduct.Name = product.Name;
        existingProduct.Price = product.Price;
        existingProduct.Description = product.Description;

        _context.Products.Update(existingProduct);
        _context.SaveChanges();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteProduct(int id)
    {
        var product = _context.Products.Find(id);
        if (product == null)
        {
            return NotFound("Produto não encontrado.");
        }

        _context.Products.Remove(product);
        _context.SaveChanges();
        return NoContent();
    }
}