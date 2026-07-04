using System;
using System.Data;

namespace LocalArtisanMarket
{
    public class ProductBusinessLogic
    {
        private readonly ProductRepository _repository;

        public ProductBusinessLogic()
        {
            _repository = new ProductRepository();
        }

        public DataTable GetCatalog()
        {
            try
            {
                return _repository.GetAllProducts();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("System encountered an isolated thread data retrieval error. Please refresh the dashboard viewport.", ex);
            }
        }

        public void ProcessProductCreation(ProductDTO product)
        {
            ValidateProductRules(product);
            try
            {
                _repository.AddProduct(product);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Enterprise Database Engine failed to record transaction entry safely.", ex);
            }
        }

        public void ProcessProductUpdate(ProductDTO product)
        {
            if (product.ProductID <= 0)
                throw new ArgumentException("Invalid state transition tracking reference ID.");

            ValidateProductRules(product);
            try
            {
                _repository.UpdateProduct(product);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Enterprise Database Engine failed to commit transaction state updates safely.", ex);
            }
        }

        public void ProcessProductDeletion(int productId)
        {
            if (productId <= 0)
                throw new ArgumentException("Invalid state tracking reference ID assigned for inventory removal.");

            try
            {
                _repository.DeleteProduct(productId);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Enterprise Database Engine failed to completely execute delisting process instructions.", ex);
            }
        }

        private void ValidateProductRules(ProductDTO product)
        {
            if (string.IsNullOrWhiteSpace(product.ProductName))
                throw new ArgumentException("Product configuration missing: Name cannot be blank.");

            // Strict rule: stock cannot accept values below 0
            if (product.Stock < 0)
                throw new ArgumentException("Inventory rule violation: Stock state cannot sink below 0 units.");

            // Strict rule: prevent decimal precision truncation
            if (product.Price <= 0)
                throw new ArgumentException("Market valuation rule violation: Price calculation state parameters must remain positive.");

            if (product.Price > 999999.99m)
                throw new ArgumentException("Market validation configuration rule threshold restriction: Out-of-bounds decimal scale protection triggered.");
        }
    }
}
