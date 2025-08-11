using ProductManagement.Domain.Entities;
using ProductManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using ProductManagement.Domain.Repositories;

namespace ProductManagement.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products.AsNoTracking().ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _context.Products.AsNoTracking()
                .FirstOrDefaultAsync(prd => prd.Id == id);
        }

        public async Task<Product?> AddAsync(Product product)
        {
            await _context.Products.AddAsync(product); //Added state(Entity state)
            await _context.SaveChangesAsync(); //Insert into Database
            return product;
        }

        public async Task UpdateAsync(Product product)
        {
            _context.Products.Update(product); //Modified state
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product); //Deleted state
                await _context.SaveChangesAsync();
            }
        }
    }
}