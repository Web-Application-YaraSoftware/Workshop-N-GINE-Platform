using YARA.WorkshopNGine.API.Inventory.Domain.Model.Aggregates;
using YARA.WorkshopNGine.API.Inventory.Domain.Model.Commands;
using YARA.WorkshopNGine.API.Inventory.Domain.Repositories;
using YARA.WorkshopNGine.API.Inventory.Domain.Services;
using YARA.WorkshopNGine.API.Shared.Domain.Repositories;

namespace YARA.WorkshopNGine.API.Inventory.Application.Internal.CommandServices;

public class ProductRequestCommandService(IProductRequestRepository productRequestRepository, IProductRepository productRepository, IUnitOfWork unitOfWork) : IProductRequestCommandService
{
    public async Task<ProductRequest?> Handle(CreateProductRequestCommand command)
    {
        // TODO: Validate the workshop id exists
        // TODO: Validate the task id exists
        // TODO: Only the role owner and mechanic assistant is able to do this
       var product = await productRepository.FindByIdAsync(command.ProductId.Value); 
       if(product == null)
           throw new Exception($"Product with the id '{command.ProductId.Value}' does not exist.");
       if(!product.IsAvailableRequest(command.RequestedQuantity))
              throw new Exception($"The requested quantity '{command.RequestedQuantity}' is not available.");
       var productRequest = new ProductRequest(command);
       try
       {
            await productRequestRepository.AddAsync(productRequest);
            await unitOfWork.CompleteAsync();
            return productRequest;
       }
       catch (Exception e)
       {
            throw new Exception($"An error occurred while creating the product request: {e.Message}");
            return null;
       }
    }

    public async Task<ProductRequest?> Handle(long productRequestId, UpdateProductRequestCommand command)
    {
        var productRequest = await productRequestRepository.FindByIdAsync(productRequestId);
        if(productRequest == null)
            throw new Exception($"Product request with the id '{productRequestId}' does not exist.");
        var product = await productRepository.FindByIdAsync(command.ProductId.Value);
        if(product == null)
            throw new Exception($"Product with the id '{command.ProductId.Value}' does not exist.");
        if(!product.IsAvailableRequest(command.RequestedQuantity))
            throw new Exception($"The requested quantity '{command.RequestedQuantity}' is not available.");
        productRequest.Update(command);
        try
        {
            productRequestRepository.Update(productRequest);
            await unitOfWork.CompleteAsync();
            return productRequest;
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occurred while updating the product request: {e.Message}");
            return null;
        }
    }

    public async Task<long?> Handle(AcceptProductRequestCommand command)
    {
        var productRequest = await productRequestRepository.FindByIdAsync(command.ProductRequestId);
        if(productRequest == null)
            throw new Exception($"Product request with the id '{command.ProductRequestId}' does not exist.");
        var product = await productRepository.FindByIdAsync(productRequest.ProductId.Value);
        if(product == null)
            throw new Exception($"Product with the id '{productRequest.ProductId.Value}' does not exist.");
        if(!product.IsAvailableRequest(productRequest.RequestedQuantity))
            throw new Exception($"The requested quantity '{productRequest.RequestedQuantity}' is not available.");
        product.Request(productRequest.RequestedQuantity);
        productRequest.Accept();
        try
        {
            productRepository.Update(product);
            productRequestRepository.Update(productRequest);
            await unitOfWork.CompleteAsync();
            return productRequest.Id;
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occurred while accepting the product request: {e.Message}");
            return null;
        }
    }

    public async Task<long?> Handle(RejectProductRequestCommand command)
    {
        var productRequest = await productRequestRepository.FindByIdAsync(command.ProductRequestId);
        if(productRequest == null)
            throw new Exception($"Product request with the id '{command.ProductRequestId}' does not exist.");
        productRequest.Reject();
        try
        {
            productRequestRepository.Update(productRequest);
            await unitOfWork.CompleteAsync();
            return productRequest.Id;
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occurred while rejecting the product request: {e.Message}");
            return null;
        }
    }
}