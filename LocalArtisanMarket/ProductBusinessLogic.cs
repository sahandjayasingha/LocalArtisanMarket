using System;
using System.Data;
using System.Collections.Generic;

namespace LocalArtisanMarket
{
    public class ProductBusinessLogic
    {
        private static List<ProductDTO> _dummyCatalog = new List<ProductDTO>()
        {
            new ProductDTO(1, "Molagoda Traditional Clay Pot", 15.50m, "Authentic Sri Lankan clay pot", 10, "Molagoda Hub", "Pottery", 12.50m, "Baked", ""),
            new ProductDTO(2, "Radawadunna Cane Basket", 25.00m, "Handcrafted durable cane basket", 5, "Radawadunna Hub", "Weaving", 8.20m, "Ready", ""),
            new ProductDTO(3, "Handwoven Dumbara Mat", 35.00m, "Traditional design Dumbara mat", 8, "Kandy Hub", "Handloom", 5.00m, "Raw", "")
        };

        private static int _nextProductId = 4;

        public ProductBusinessLogic()
        {
        }

        public List<ProductDTO> GetCatalog()
        {
            try
            {
                return new List<ProductDTO>(_dummyCatalog);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("System encountered an isolated thread data retrieval error. Please refresh the dashboard viewport.", ex);
            }
        }

        public void ProcessProductCreation(ProductDTO product)
        {
            ValidateStateChangeInvariants(product);

            try
            {
                ProductDTO productWithId = new ProductDTO(
                    _nextProductId++,
                    product.ProductName,
                    product.Price,
                    product.Description,
                    product.Stock,
                    product.OriginHub,
                    product.CraftTechnique,
                    product.MoistureMetric,
                    product.ProcessingStage,
                    product.ImagePath
                );
                _dummyCatalog.Add(productWithId);
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

            ValidateStateChangeInvariants(product);

            try
            {
                var existingIndex = _dummyCatalog.FindIndex(p => p.ProductID == product.ProductID);
                if (existingIndex != -1)
                {
                    _dummyCatalog[existingIndex] = product;
                }
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
                var existing = _dummyCatalog.Find(p => p.ProductID == productId);
                if (existing != null)
                {
                    _dummyCatalog.Remove(existing);
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Enterprise Database Engine failed to completely execute delisting process instructions.", ex);
            }
        }

        private void ValidateStateChangeInvariants(ProductDTO product)
        {
            if (string.IsNullOrWhiteSpace(product.ProductName))
                throw new ArgumentException("State validation failure: ProductName configuration parameter cannot be null or blank.");

            if (product.Stock < 0)
                throw new ArgumentException("State validation failure: Inventory rule violation. Stock parameters cannot accept mathematical negatives below 0.");

            if (product.Price <= 0)
                throw new ArgumentException("State validation failure: Market valuation parameters must strictly resolve to a positive, non-zero asset value.");

            if (decimal.Round(product.Price, 2) != product.Price)
                throw new ArgumentException("State validation failure: Price variable violates precision constraints. Values cannot extend beyond two decimal places.");

            if (product.Price > 999999.99m)
                throw new ArgumentException("State validation failure: Asset boundary threshold overflow. Price value exceeds enterprise storage configuration scale parameters.");
        }
    }
}