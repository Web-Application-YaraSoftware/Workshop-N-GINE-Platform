using YARA.WorkshopNGine.API.Inventory.Domain.Model.Aggregates;
using YARA.WorkshopNGine.API.Inventory.Domain.Model.Commands;
using YARA.WorkshopNGine.API.Inventory.Domain.Repositories;
using YARA.WorkshopNGine.API.Inventory.Domain.Services;
using YARA.WorkshopNGine.API.Shared.Domain.Repositories;

namespace YARA.WorkshopNGine.API.Inventory.Application.Internal.CommandServices;

public class ProductCommandService(IProductRepository productRepository, IUnitOfWork unitOfWork) : IProductCommandService
{
    public async Task<Product?> Handle(CreateProductCommand command)
    {
        // TODO: Validate the workshop id exists
        // TODO: Only the role owner is able to do this
        var product = new Product(command);
        try
        {
            await productRepository.AddAsync(product);
            await unitOfWork.CompleteAsync();
            return product;
        }
        catch (Exception e)
        {
            throw new Exception($"An error occurred while creating the product: {e.Message}");
            return null;

        }
    }

    public async Task<Product?> Handle(long productId, UpdateProductCommand command)
    {
        var product = await productRepository.FindByIdAsync(productId);
        if(product == null)
            throw new Exception($"Product with the id '{productId}' does not exist.");
        product.Update(command);
        try
        {
            productRepository.Update(product);
            await unitOfWork.CompleteAsync();
            return product;
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occurred while updating the product: {e.Message}");
            return null;
        }
    }

    public async Task<long?> Handle(DeleteProductCommand command)
    {
        var product = await productRepository.FindByIdAsync(command.ProductId);
        if(product == null)
            throw new Exception($"Product with the id '{command.ProductId}' does not exist.");
        try
        {
            productRepository.Remove(product);
            await unitOfWork.CompleteAsync();
            return product.Id;
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occurred while deleting the product: {e.Message}");
            return null;
        }
    }
}