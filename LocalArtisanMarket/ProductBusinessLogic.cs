using System;
using System.Data;
using System.Collections.Generic;

namespace LocalArtisanMarket
{
    
    public class ProductBusinessLogic
    {
        private readonly ProductRepository _repository;

        public ProductBusinessLogic()
        {
            _repository = new ProductRepository();
        }

        
        public List<ProductDTO> GetCatalog()
        {
            try
            {
                
                DataTable dt = _repository.GetAllProducts();

                List<ProductDTO> catalogList = new List<ProductDTO>();

                foreach (DataRow row in dt.Rows)
                {
                    ProductDTO dto = new ProductDTO(
                        Convert.ToInt32(row["ProductID"]),
                        row["ProductName"].ToString(),
                        Convert.ToDecimal(row["Price"]),
                        row["Description"].ToString(),
                        Convert.ToInt32(row["Stock"]),
                        row["OriginHub"].ToString(),
                        row["CraftTechnique"].ToString(),
                        Convert.ToDecimal(row["MoistureMetric"]),
                        row["ProcessingStage"].ToString()
                    );

                    catalogList.Add(dto);
                }

                return catalogList;
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

      
        private void ValidateStateChangeInvariants(ProductDTO product)
        {

            if (string.IsNullOrWhiteSpace(product.ProductName))
                throw new ArgumentException("State validation failure: ProductName configuration parameter cannot be null or blank.");


            if (product.Stock < 0)
                throw new ArgumentException("State validation failure: Inventory rule violation. Stock parameters cannot accept mathematical negatives below 0.");


            if (product.Price <= 0)
                throw new ArgumentException("State validation failure: Market valuation parameters must strictly resolve to a positive, non-zero asset value.");


            if (decimal.Round(product.Price, 2) != product.Price)
                throw new ArgumentException("State validation failure: Price variable violates precision constraints. Values cannot extend beyond two decimal places to prevent SQL Server truncation errors.");

            if (product.Price > 999999.99m)
                throw new ArgumentException("State validation failure: Asset boundary threshold overflow. Price value exceeds enterprise storage configuration scale parameters.");
        }
    }
}
