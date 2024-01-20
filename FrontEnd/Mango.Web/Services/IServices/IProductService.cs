using Mango.Web.Models;

namespace Mango.Web.Services.IServices
{

    public interface IProductService: IBaseService
    {

        Task<T> GetAllProductsAsync<T>(string token);
        Task<T> GetProductByIdAsync<T>(int id, string tokens);
        Task<T> CreateProductAsync<T>(ProductDto productDto, string tokens);
        Task<T> UpdateProductAsync<T>(ProductDto productDto, string token);
        Task<T> DeleteProductAsync<T>(int id, string token);

    }
}
