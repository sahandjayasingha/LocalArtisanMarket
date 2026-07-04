using System;
using System.Data;

namespace LocalArtisanMarket
{
    // MODULE 1: Deterministic State Validation Engine
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

        // Transactional State Change Factory for Creations
        public void ProcessProductCreation(ProductDTO product)
        {
            // Enforcing state invariants before passing data to the DAL
            ValidateStateChangeInvariants(product);

            try
            {
                _repository.AddProduct(product);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Enterprise Database Engine failed to record transaction entry safely.", ex);
            }
        }

        // Transactional State Change Factory for Updates
        public void ProcessProductUpdate(ProductDTO product)
        {
            if (product.ProductID <= 0)
                throw new ArgumentException("Invalid state transition tracking reference ID.");

            // Enforcing state invariants before passing data to the DAL
            ValidateStateChangeInvariants(product);

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

        /// <summary>
        /// Strict Execution Boundary Validation Engine
        /// </summary>
        private void ValidateStateChangeInvariants(ProductDTO product)
        {
            // MAPPING CRITERIA: Serializing explicit domain fields
            if (string.IsNullOrWhiteSpace(product.ProductName))
                throw new ArgumentException("State validation failure: ProductName configuration parameter cannot be null or blank.");

            // CONSTRAINT CHECK 1: Stock values must undergo checks preventing mathematical negatives.
            if (product.Stock < 0)
                throw new ArgumentException("State validation failure: Inventory rule violation. Stock parameters cannot accept mathematical negatives below 0.");

            // CONSTRAINT CHECK 2: Price custom business rule check to prevent decimal precision truncations in SQL Server.
            // SQL Server DECIMAL(18,2) scales must be strictly positive and fit safely within standard database precision matrix boundaries.
            if (product.Price <= 0)
                throw new ArgumentException("State validation failure: Market valuation parameters must strictly resolve to a positive, non-zero asset value.");

            // Ensure the value does not have fractional pennies that SQL Server DECIMAL(18,2) would truncate/round unexpectedly
            if (decimal.Round(product.Price, 2) != product.Price)
                throw new ArgumentException("State validation failure: Price variable violates precision constraints. Values cannot extend beyond two decimal places to prevent SQL Server truncation errors.");

            if (product.Price > 999999.99m)
                throw new ArgumentException("State validation failure: Asset boundary threshold overflow. Price value exceeds enterprise storage configuration scale parameters.");
        }
    }
}
