using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace WEBACA2.Classes
{
    public class ProductManager
    {
        public List<object> getAllProductInfo()
        {
            DbConnection dbConn = new DbConnection();
            List<object> ProdInfoList = new List<object>();
            dbConn.Cmd.CommandText = " select ProductID,ProductName,ProductCode,Product.Description,BrandName,SubBrandName, " +
                                    " ViewablebyPublic,PublishedToPublicViewAt,Product.CreatedAt,Product.UpdatedAt from " +
                                    " Product INNER JOIN Brand on Product.BrandID = Brand.BrandID LEFT JOIN SubBrand " +
                                    " on SubBrand.SubBrandID = Product.SubBrandID where Product.DeletedAt is NULL";
            try
            {
                dbConn.Fill();
                foreach (DataRow dr in dbConn.Dt.Rows)
                {
                    var genericObj = new
                    {
                        ProductID = Int32.Parse(dr["ProductID"].ToString()),
                        ProductName = dr["ProductName"].ToString(),
                        ProductCode = dr["ProductCode"].ToString(),
                        Description = dr["Description"].ToString(),
                        BrandName = dr["BrandName"].ToString(),
                        SubBrandName = dr["SubBrandName"].ToString(),
                        ViewablebyPublic = dr["ViewablebyPublic"].ToString() == "True" ? "Yes" : "No",
                        PublishedToPublicViewAt = dr["PublishedToPublicViewAt"].ToString(),
                        CreatedAt = DateTime.Parse(dr["CreatedAt"].ToString()),
                        UpdatedAt = DateTime.Parse(dr["UpdatedAt"].ToString())
                    };
                    ProdInfoList.Add(genericObj);

                }
            }
            catch (SqlException ex)
            {
                //throw new System.ArgumentException(ex.Message);
                throw new System.ArgumentException(" Please report the error to administrator ");
            }
            return ProdInfoList;
        }//end of getallProductInfo
        public List<object> getallProductWithProdPriceInfo()
        {

            List<object> ProdPriceInfoList = new List<object>();
            DbConnection dbConn = new DbConnection();

            dbConn.Cmd.CommandText = " Select Product.ProductID,ProductName,ProductCode,InitalPrice,Price.PromotionPrice,StockQuantities,Price.CreatedAt,Price.UpdatedAt " +
                                     " from product LEFT JOIN price on Product.ProductID = Price.ProductID LEFT JOIN Stock " +
                                     " on Stock.ProductID = Product.ProductID WHERE Product.DeletedAt IS NULL ";
            try
            {
                dbConn.Fill();




                foreach (DataRow dr in dbConn.Dt.Rows)
                {


                    var genericObj = new
                    {
                        ProductID = Int32.Parse(dr["ProductID"].ToString()),
                        ProductName = dr["ProductName"].ToString(),
                        ProductCode = dr["ProductCode"].ToString(),
                        InitalPrice = Decimal.Parse(dr["InitalPrice"].ToString()),
                        PromotionPrice = Decimal.Parse(dr["PromotionPrice"].ToString()) == 0.00m ? (object)DBNull.Value : Decimal.Parse(dr["PromotionPrice"].ToString()),
                        StockQuantities = Int32.Parse(dr["StockQuantities"].ToString()),
                        CreatedAt = DateTime.Parse(dr["CreatedAt"].ToString()),
                        UpdatedAt = DateTime.Parse(dr["UpdatedAt"].ToString())
                    };
                    ProdPriceInfoList.Add(genericObj);

                }
            }
            catch (SqlException ex)
            {

                //throw new System.ArgumentException(ex.Message);
                throw new System.ArgumentException(" Please report the error to administrator ");
            }
            return ProdPriceInfoList;
        }//end of getallProductWithProdPriceInfo
        public List<object> getallProductWithProdPriceInfobySearchTxt(string inSearchTxt)
        {
            List<object> ProdPriceInfoList = new List<object>();
            DbConnection dbConn = new DbConnection();

            dbConn.Cmd.CommandText = " SELECT Product.ProductID,ProductName,ProductCode,InitalPrice,Price.PromotionPrice,StockQuantities,Price.CreatedAt,Price.UpdatedAt " +
                                     " FROM product LEFT JOIN price on Product.ProductID = Price.ProductID LEFT JOIN Stock " +
                                     " on Stock.ProductID = Product.ProductID WHERE Product.DeletedAt IS NULL AND (ProductName like @inSearchTxt or InitalPrice like " +
                                     " @inSearchTxt or Price.PromotionPrice like @inSearchTxt or StockQuantities like @inSearchTxt) ";
            dbConn.Cmd.Parameters.Add("@inSearchTxt", SqlDbType.VarChar, 100).Value = "%" + inSearchTxt + "%";
            try
            {
                dbConn.Fill();
                foreach (DataRow dr in dbConn.Dt.Rows)
                {
                    var genericObj = new
                    {
                        ProductID = Int32.Parse(dr["ProductID"].ToString()),
                        ProductName = dr["ProductName"].ToString(),
                        ProductCode = dr["ProductCode"].ToString(),
                        InitalPrice = Decimal.Parse(dr["InitalPrice"].ToString()),
                        PromotionPrice = Decimal.Parse(dr["PromotionPrice"].ToString()) == 0.00m ? (object)DBNull.Value : Decimal.Parse(dr["PromotionPrice"].ToString()),
                        StockQuantities = Int32.Parse(dr["StockQuantities"].ToString()),
                        CreatedAt = DateTime.Parse(dr["CreatedAt"].ToString()),
                        UpdatedAt = DateTime.Parse(dr["UpdatedAt"].ToString())
                    };
                    ProdPriceInfoList.Add(genericObj);
                }
            }
            catch (SqlException ex)
            {
                throw new System.ArgumentException(ex.Message);
            }
            return ProdPriceInfoList;


        }//end of getallProductWithProdPriceInfobySearchTxt
        public object getOneProductWithProdPriceInfo(string inProductID)
        {
            DbConnection dbConn = new DbConnection();
            dbConn.Cmd.CommandText = " Select Product.ProductID,ProductName,ProductCode,InitalPrice,Price.PromotionPrice,StockQuantities,Price.CreatedAt,Price.UpdatedAt " +
                                     " from product LEFT JOIN price on Product.ProductID = Price.ProductID LEFT JOIN Stock " +
                                     " on Stock.ProductID = Product.ProductID WHERE Product.ProductID = @inProductID";
            dbConn.Cmd.Parameters.Add("@inProductID", SqlDbType.Int).Value = inProductID;


            dbConn.Fill();

            DataRow dr = dbConn.Dt.Rows[0];
            var genericObj = new
                               {
                                   ProductID = Int32.Parse(dr["ProductID"].ToString()),
                                   ProductName = dr["ProductName"].ToString(),
                                   ProductCode = dr["ProductCode"].ToString(),
                                   InitalPrice = Decimal.Parse(dr["InitalPrice"].ToString()),
                                   PromotionPrice = Decimal.Parse(dr["PromotionPrice"].ToString()) == 0.00m ? (object)DBNull.Value : Decimal.Parse(dr["PromotionPrice"].ToString()),
                                   StockQuantities = dr["StockQuantities"].ToString(),
                                   CreatedAt = DateTime.Parse(dr["CreatedAt"].ToString()),
                                   UpdatedAt = DateTime.Parse(dr["UpdatedAt"].ToString())
                               };


            return genericObj;
        }//end of getOneProductWithProdPriceInfo

        //public object getOneProduct(string inProductID)
        //{
        //    DbConnection dbConn = new DbConnection();
        //    dbConn.Cmd.CommandText = " Select Product.ProductID,ProductName,ProductCode,InitalPrice,Price.PromotionPrice,StockQuantities,Price.CreatedAt,Price.UpdatedAt " +
        //                             " from product LEFT JOIN price on Product.ProductID = Price.ProductID LEFT JOIN Stock " +
        //                             " on Stock.ProductID = Product.ProductID WHERE Product.ProductID = @inProductID";
        //    dbConn.Cmd.Parameters.Add("@inProductID", SqlDbType.Int).Value = inProductID;


        //    dbConn.Fill();

        //    DataRow dr = dbConn.Dt.Rows[0];
        //    var genericObj = new
        //    {
        //        ProductID = Int32.Parse(dr["ProductID"].ToString()),
        //        ProductName = dr["ProductName"].ToString(),
        //        ProductCode = dr["ProductCode"].ToString(),
        //        InitalPrice = Decimal.Parse(dr["InitalPrice"].ToString()),
        //        PromotionPrice = Decimal.Parse(dr["PromotionPrice"].ToString()) == 0.00m ? (object)DBNull.Value : Decimal.Parse(dr["PromotionPrice"].ToString()),
        //        StockQuantities = dr["StockQuantities"].ToString(),
        //        CreatedAt = DateTime.Parse(dr["CreatedAt"].ToString()),
        //        UpdatedAt = DateTime.Parse(dr["UpdatedAt"].ToString())
        //    };


        //    return genericObj;
        //}//end of getOneProductWithProdPriceInfo

        public ProductImage getProductImage(string inProductImageID)
        {

            DbConnection dbConn = new DbConnection();
            ProductImage productImage = new ProductImage();

            
            dbConn.Cmd.CommandText = " SELECT ProductImageID, ProductImageName, ProductImageData, UpdatedAt, CreatedAt, " +
                                     " DeletedAt, ProductID " +
                                     " FROM ProductImage WHERE ProductImageID = @inProductImageID and DeletedAt is null ";

            dbConn.Cmd.Parameters.Add("@inProductImageID", SqlDbType.Int).Value = inProductImageID;

            dbConn.Fill();
            DataRow dr = dbConn.Dt.Rows[0];
            productImage.ProductImageID= Int32.Parse(dr["ProductImageID"].ToString());
            productImage.ProductImageName = dr["ProductImageName"].ToString();
            productImage.ProductImageData = (byte[])dr["ProductImageData"];
            productImage.CreatedAt = DateTime.Parse(dr["CreatedAt"].ToString());
            productImage.UpdatedAt = DateTime.Parse(dr["UpdatedAt"].ToString());
            productImage.ProductID = Int32.Parse(dr["ProductID"].ToString());

            return productImage;
        }

        public bool DeleteOneImage(string inProductImageID)
        {

            DbConnection dbConn = new DbConnection();
            int rowAffected = 0;
            dbConn.Cmd.CommandText = " UPDATE Productimage SET deletedat = getdate() WHERE ProductImageID = @inProductImageID ";

            dbConn.Cmd.Parameters.Add("@inProductImageID", SqlDbType.Int).Value = inProductImageID;
            dbConn.Conn.Open();
            rowAffected = dbConn.Cmd.ExecuteNonQuery();
            dbConn.Conn.Close();

            if (rowAffected == 0)
                return false;
            else
                return true;

        }

        public List<object> getAllImagesByProductID(string inProductID)
        {

            DbConnection dbConn = new DbConnection();
            List<object> ImageList = new List<object>();
            dbConn.Cmd.CommandText = " SELECT ProductImageID, ProductImageName, ProductImageData, CreatedAt, UpdatedAt, ProductID " +
                                   " FROM ProductImage WHERE ProductID = @inProductID AND DeletedAt is null ";

            dbConn.Cmd.Parameters.Add("@inProductID", SqlDbType.Int).Value = inProductID;
            dbConn.Fill();
            foreach (DataRow dr in dbConn.Dt.Rows)
            {
                var image = new
                {

                    ProductImageID = Int32.Parse(dr["ProductImageID"].ToString()),
                    ProductImageName = dr["ProductImageName"].ToString(),
                    //ProductImageData = (Byte[])dr["ProductImageData"], 
                    ProductImageCreatedAt = DateTime.Parse(dr["CreatedAt"].ToString()),
                    ProductImageUpdatedAt = DateTime.Parse(dr["UpdatedAt"].ToString()),
                    ProductID = Int32.Parse(dr["ProductID"].ToString())
                };
                ImageList.Add(image);

            };


            return ImageList;

        }
        public List<object> getallProductWithProdStockInfo()
        {

            List<object> ProdStockInfoList = new List<object>();
            DbConnection dbConn = new DbConnection();
            dbConn.Cmd.CommandText = " SELECT Product.ProductID,ProductName,ProductCode,StockQuantities,Availability, " +
                                           " ZeroQuantityAlert,Stock.CreatedAt,Stock.UpdatedAt from Product LEFT JOIN stock " +
                                           " on Product.ProductID=stock.ProductID ";
            try
            {
                dbConn.Fill();
                foreach (DataRow dr in dbConn.Dt.Rows)
                {
                    var genericObj = new
                    {
                        ProductID = Int32.Parse(dr["ProductID"].ToString()),
                        ProductName = dr["ProductName"].ToString(),
                        ProductCode = dr["ProductCode"].ToString(),
                        StockQuantities = Int32.Parse(dr["StockQuantities"].ToString()),
                        Availability = dr["Availability"].ToString(),
                        ZeroQuantityAlert = dr["ZeroQuantityAlert"].ToString(),
                        CreatedAt = DateTime.Parse(dr["CreatedAt"].ToString()),
                        UpdatedAt = DateTime.Parse(dr["UpdatedAt"].ToString())
                    };
                    ProdStockInfoList.Add(genericObj);

                }
            }
            catch (SqlException ex)
            {
                throw new System.ArgumentException(ex.Message);
            }
            return ProdStockInfoList;


        }//end of getallProductWithProdStockInfo
        public List<object> getallProductWithProdStockInfobySearchTxt(string inSearchTxt)
        {

            List<object> ProdPriceInfoList = new List<object>();
            DbConnection dbConn = new DbConnection();

            dbConn.Cmd.CommandText = " SELECT Product.ProductID,ProductName,ProductCode,StockQuantities,Availability, " +
                                     " ZeroQuantityAlert,Stock.CreatedAt,Stock.UpdatedAt from Product LEFT JOIN stock " +
                                     " on Product.ProductID=stock.ProductID WHERE Product.DeletedAt IS NULL AND (ProductName LIKE @inSearchTxt or " +
                                     " ProductCode LIKE @inSearchTxt or StockQuantities LIKE @inSearchTxt or Availability LIKE @inSearchTxt " +
                                     " or ZeroQuantityAlert LIKE @inSearchTxt) ";
            dbConn.Cmd.Parameters.Add("@inSearchTxt", SqlDbType.VarChar, 100).Value = "%" + inSearchTxt + "%";
            try
            {
                dbConn.Fill();
                foreach (DataRow dr in dbConn.Dt.Rows)
                {
                    var genericObj = new
                    {
                        ProductID = Int32.Parse(dr["ProductID"].ToString()),
                        ProductName = dr["ProductName"].ToString(),
                        ProductCode = dr["ProductCode"].ToString(),
                        StockQuantities = Int32.Parse(dr["StockQuantities"].ToString()),
                        Availability = dr["Availability"].ToString(),
                        ZeroQuantityAlert = dr["ZeroQuantityAlert"].ToString(),
                        CreatedAt = DateTime.Parse(dr["CreatedAt"].ToString()),
                        UpdatedAt = DateTime.Parse(dr["UpdatedAt"].ToString())
                    };
                    ProdPriceInfoList.Add(genericObj);
                }
            }
            catch (SqlException ex)
            {
                throw new System.ArgumentException(ex.Message);
            }
            return ProdPriceInfoList;



        }//end of getallProductWithProdStockInfo 
        public object getOneProductWithProdStockInfo(string inProductID)
        {
            DbConnection dbConn = new DbConnection();
            dbConn.Cmd.CommandText = " SELECT Product.ProductID,ProductName,ProductCode,StockQuantities,Availability, " +
                                           " ZeroQuantityAlert,Stock.CreatedAt,Stock.UpdatedAt from Product LEFT JOIN stock " +
                                           " on Product.ProductID=stock.ProductID WHERE Product.ProductID= @inProductID ";
            dbConn.Cmd.Parameters.Add("@inProductID", SqlDbType.Int).Value = inProductID;
            dbConn.Fill();
            DataRow dr = dbConn.Dt.Rows[0];

            var genericObj = new
            {
                ProductID = Int32.Parse(dr["ProductID"].ToString()),
                ProductName = dr["ProductName"].ToString(),
                ProductCode = dr["ProductCode"].ToString(),
                StockQuantities = Int32.Parse(dr["StockQuantities"].ToString()),
                Availability = dr["Availability"].ToString(),
                ZeroQuantityAlert = dr["ZeroQuantityAlert"].ToString(),
                CreatedAt = DateTime.Parse(dr["CreatedAt"].ToString()),
                UpdatedAt = DateTime.Parse(dr["UpdatedAt"].ToString())
            };


            return genericObj;

        }//end of getOneProductWithProdStockInfo
        public bool DeleteOneProduct(string inProductID)
        {

            DbConnection dbConn = new DbConnection();
            int rowAffected = 0;
            dbConn.Cmd.CommandText = " UPDATE Product SET DeletedAt = getdate() WHERE ProductID = @inProductID ";
            dbConn.Cmd.Parameters.Add("@inProductID", SqlDbType.Int).Value = inProductID;

                dbConn.Conn.Open();
                rowAffected = dbConn.Cmd.ExecuteNonQuery();
                dbConn.Conn.Close();
      
            if (rowAffected == 0)
                return false;
            else
                return true;
        }//end of DeleteOneProduct

        public bool[] UpdateOneProduct(string inProductID, string inProductName, string inProductCode, string inDescription, string inViewablebyPublic, string inAlertOutOfStock)
        {
            DbConnection dbConn = new DbConnection();

            //if there is productname constraint,set to true
            bool checkConstraint = false;

            //if int rowaffected = 1 ; set bool rowaffected to true,else false. Objective is to return bool[] consisting of [rowAffected,checkConstraint]
            bool rowAffected = false;
            int rowaffected = 0;
            dbConn.Cmd.CommandText = " UPDATE Product SET ProductCode=@inProductCode , ProductName=@inProductName, " +
                                     " Description=@inDescription, " +
                                     " ViewablebyPublic=@inViewablebyPublic, " +
                                     " AlertOutOfStock=@inAlertOutOfStock WHERE ProductID = @inProductID ";

            dbConn.Cmd.Parameters.Add("@inProductID", SqlDbType.Int).Value = inProductID;
            dbConn.Cmd.Parameters.Add("@inProductName", SqlDbType.VarChar,100).Value = inProductName;
            dbConn.Cmd.Parameters.Add("@inProductCode", SqlDbType.VarChar,100).Value = inProductCode;
            dbConn.Cmd.Parameters.Add("@inDescription", SqlDbType.NVarChar,2000).Value = inDescription;
            dbConn.Cmd.Parameters.Add("@inViewablebyPublic", SqlDbType.Int).Value = inViewablebyPublic;
            dbConn.Cmd.Parameters.Add("@inAlertOutOfStock", SqlDbType.Int).Value = inAlertOutOfStock;

            try
            {
                dbConn.Conn.Open();
                rowaffected = dbConn.Cmd.ExecuteNonQuery();
                dbConn.Conn.Close();
            }
            catch (SqlException ex)
            {
                if (ex.Message.Contains("ProductNameUniqueConstraint") == true)
                {
                    checkConstraint = true;
                }
            }
            if (rowaffected != 0)
                rowAffected = true;

            bool[] status = { rowAffected, checkConstraint };
            return status;
        }//end of UpdateOneProduct
        public bool UpdateProductPrice(string inProductID, string inProductPrice, string inPromotionPrice)
        {
            DbConnection dbConn = new DbConnection();
            int rowAffected = 0;
            dbConn.Cmd.CommandText = "UPDATE Price SET InitalPrice= @inProductPrice ,PromotionPrice= @inPromotionPrice WHERE productid= @inProductID";
            dbConn.Cmd.Parameters.Add("@inProductPrice", SqlDbType.Decimal).Value = inProductPrice;
            if (string.IsNullOrEmpty(inPromotionPrice))
                inPromotionPrice = "0.00";
            dbConn.Cmd.Parameters.Add("@inPromotionPrice", SqlDbType.Decimal).Value = inPromotionPrice;
            dbConn.Cmd.Parameters.Add("@inProductID", SqlDbType.Int).Value = inProductID;

            try
            {
                dbConn.Conn.Open();
                rowAffected = dbConn.Cmd.ExecuteNonQuery();
                dbConn.Conn.Close();
            }
            catch (SqlException ex)
            {
                throw new System.ArgumentException(ex.Message);
            }
            if (rowAffected == 0)

                return false;
            else
                return true;
        }//end of UpdateProductPrice
        public bool UpdateProductPoint(string inProductID,string inPointsEarned, string inPointsNeeded){
            DbConnection dbConn = new DbConnection();
            int rowAffected = 0;
            dbConn.Cmd.CommandText = " UPDATE Point SET PointsEarned= @inPointsEarned ,PointsNeeded= @inPointsNeeded,UpdatedAt =GETDATE() WHERE productid= @inProductID ";
            dbConn.Cmd.Parameters.Add("@inProductID", SqlDbType.Decimal).Value = inProductID;
            dbConn.Cmd.Parameters.Add("@inPointsEarned", SqlDbType.Decimal).Value = inPointsEarned;
            dbConn.Cmd.Parameters.Add("@inPointsNeeded", SqlDbType.Decimal).Value = inPointsNeeded;

            try
            {
                dbConn.Conn.Open();
                rowAffected = dbConn.Cmd.ExecuteNonQuery();
                dbConn.Conn.Close();
            }
            catch (SqlException ex)
            {
                throw new System.ArgumentException(ex.Message);
            }
            if (rowAffected == 0)

                return false;
            else
                return true;
        }//end of UpdateProductPrice
        
        public bool UpdateProductStock(string inStockQuantities, string inAvailability, string inZeroQuantityAlert, string inProductID)//end of UpdateProductStock
        {

            DbConnection dbConn = new DbConnection();
            int rowAffected = 0;
            dbConn.Cmd.CommandText = "UPDATE STOCK SET StockQuantities= @inStockQuantities ,Availability= @inAvailability,ZeroQuantityAlert= @inZeroQuantityAlert WHERE productid= @inProductID";
            if (string.IsNullOrEmpty(inStockQuantities))
                inStockQuantities = "0";
            dbConn.Cmd.Parameters.Add("@inStockQuantities", SqlDbType.Int).Value = inStockQuantities;
            dbConn.Cmd.Parameters.Add("@inAvailability", SqlDbType.Int).Value = inAvailability;
            dbConn.Cmd.Parameters.Add("@inZeroQuantityAlert", SqlDbType.Int).Value = inZeroQuantityAlert;
            dbConn.Cmd.Parameters.Add("@inProductID", SqlDbType.Int).Value = inProductID;

            try
            {
                dbConn.Conn.Open();
                rowAffected = dbConn.Cmd.ExecuteNonQuery();
                dbConn.Conn.Close();
            }
            catch (SqlException ex)
            {
                throw new System.ArgumentException(ex.Message);
            }
            if (rowAffected == 0)

                return false;
            else
                return true;
        }//end of UpdateProductPrice

        //product_SubCategory table
        public bool UpdateProductSubCategory(string inProductID,int[] SubCategoryIDArray) {
            DbConnection dbConn = new DbConnection();
            int rowAffected = 0;
            int delrowAffected = 0;
            //execute for delete statement
            dbConn.Cmd.CommandText = "DELETE FROM Product_SubCategory where productID = @inProductID";
            dbConn.Cmd.Parameters.Add("@inProductID", SqlDbType.Int).Value = inProductID;
            dbConn.Conn.Open();
            delrowAffected += dbConn.Cmd.ExecuteNonQuery();
            dbConn.Conn.Close();
            dbConn.Cmd.Parameters.Clear();

         
                //loop according to array and insert multiple time to product_subcategory table
                for (int i = 0; i < SubCategoryIDArray.Length; i++)
                {
                    dbConn.Cmd.CommandText = "INSERT INTO Product_SubCategory " +
                                             " (ProductID,SubCategoryID) " +
                                             " values " +
                                             " (@inProductID,@inSubCategoryID) ";
                    dbConn.Cmd.Parameters.Add("@inProductID", SqlDbType.Int).Value = inProductID;
                    dbConn.Cmd.Parameters.Add("@inSubCategoryID", SqlDbType.Int).Value = SubCategoryIDArray[i];

                    try
                    {
                        dbConn.Conn.Open();
                        rowAffected += dbConn.Cmd.ExecuteNonQuery();
                        dbConn.Conn.Close();
                    }
                    catch (SqlException ex)
                    {
                        throw new System.ArgumentException(ex.Message);
                    }

                    dbConn.Cmd.Parameters.Clear();
                }

            //combine delete statement returned row + insert statement return row
                rowAffected += delrowAffected;

            //validation check 
                if (rowAffected != SubCategoryIDArray.Length + delrowAffected)

                return false;
            else
                return true;      
        }

        //product_SubCategory table
        public bool UpdateProductCategory(string inProductID, int[] CategoryIDArray)
        {
            DbConnection dbConn = new DbConnection();
            int rowAffected = 0;
            int delrowAffected = 0;
            //execute for delete statement
            dbConn.Cmd.CommandText = "DELETE FROM Product_Category where productID = @inProductID";
            dbConn.Cmd.Parameters.Add("@inProductID", SqlDbType.Int).Value = inProductID;
            dbConn.Conn.Open();
            delrowAffected += dbConn.Cmd.ExecuteNonQuery();
            dbConn.Conn.Close();
            dbConn.Cmd.Parameters.Clear();


            //loop according to array and insert multiple time to product_category table
            for (int i = 0; i < CategoryIDArray.Length; i++)
            {
                dbConn.Cmd.CommandText = "INSERT INTO Product_Category " +
                                         " (ProductID,CategoryID) " +
                                         " values " +
                                         " (@inProductID,@inCategoryID) ";
                dbConn.Cmd.Parameters.Add("@inProductID", SqlDbType.Int).Value = inProductID;
                dbConn.Cmd.Parameters.Add("@inCategoryID", SqlDbType.Int).Value = CategoryIDArray[i];

                try
                {
                    dbConn.Conn.Open();
                    rowAffected += dbConn.Cmd.ExecuteNonQuery();
                    dbConn.Conn.Close();
                }
                catch (SqlException ex)
                {
                    throw new System.ArgumentException(ex.Message);
                }

                dbConn.Cmd.Parameters.Clear();
            }

            //combine delete statement returned row + insert statement return row
            rowAffected += delrowAffected;

            //validation check 
            if (rowAffected != CategoryIDArray.Length + delrowAffected)

                return false;
            else
                return true;
        }

        public string AddImageToOneProduct(string inProductImageName, Byte[] inProductImagedata,string inProductID)
        {

            DbConnection dbConn = new DbConnection();
            string duplicateimagename = string.Empty;

            //get last productID Identity for the newly product inserted
            dbConn.Cmd.CommandText = " SELECT max(productID) FROM product ";
            dbConn.Conn.Open();
            string productID = dbConn.Cmd.ExecuteScalar().ToString();
            dbConn.Conn.Close();


            dbConn.Cmd.CommandText = " INSERT INTO ProductImage (ProductImageName,ProductImagedata,ProductID) " +
                                     " VALUES (@inProductImageName,@inProductImagedata,@inProductID) ";

            dbConn.Cmd.Parameters.Add("@inProductImageName", SqlDbType.VarChar).Value = inProductImageName;
            dbConn.Cmd.Parameters.Add("@inProductImagedata", SqlDbType.Binary).Value = inProductImagedata;
            dbConn.Cmd.Parameters.Add("@inproductID", SqlDbType.Int).Value = inProductID;
            try
            {
                dbConn.Conn.Open();
                dbConn.Cmd.ExecuteNonQuery();
                dbConn.Conn.Close();
            }
            catch (SqlException ex)
            {
                //prevent showing error message to user, if uniqueconstraint exist,do nth
                if (ex.Message.Contains("ProductImageNameUniqueConstraint") == true)
                {

                    duplicateimagename += inProductImageName + "/n  Image Name already existed. Please upload with a different image name.";
                    

                }
            }

            return duplicateimagename;
        }
        public bool AddOneProduct(string inProductCode, string inProductName, string inDescription,
                                  string inBrandID, string inSubBrandID, bool inViewableByPublic,
                                  Nullable<DateTime> inPublishedtoPublicViewAt, bool inAlertOutOfStock,string inAgeGroupID,
                                  string inSubCategoryID,string inInitalPrice,string inCategoryID,
                                  string inPointsNeeded, string inPointsEarned, string inStockQuantities,
                                  bool inAvailability
                                  )
        {
            DbConnection dbConn = new DbConnection();
            string productID = string.Empty;
            bool checkConstraint = true;
            try
            {
                //product table
                dbConn.Cmd.CommandText = " INSERT INTO Product (ProductCode,ProductName,Description,BrandID,SubBrandID,ViewableByPublic,PublishedtoPublicViewAt,AlertOutOfStock, " +
                                         " AgeGroupID,SubCategoryID) VALUES (@inProductCode,@inProductName,@inDescription,@inBrandID,@inSubBrandID," +
                                         " @inViewableByPublic,@inPublishedtoPublicViewAt,@inAlertOutOfStock,@inAgeGroupID,@inSubCategoryID) ";

                dbConn.Cmd.Parameters.Add("@inProductCode", SqlDbType.VarChar, 100).Value = inProductCode;
                dbConn.Cmd.Parameters.Add("@inProductName", SqlDbType.VarChar, 100).Value = inProductName;
                dbConn.Cmd.Parameters.Add("@inDescription", SqlDbType.NVarChar, 2000).Value = inDescription;
                dbConn.Cmd.Parameters.Add("@inBrandID", SqlDbType.Int).Value = inBrandID;
                dbConn.Cmd.Parameters.Add("@inSubBrandID", SqlDbType.Int).Value = inSubBrandID;
                dbConn.Cmd.Parameters.Add("@inViewableByPublic", SqlDbType.Bit).Value = inViewableByPublic;

                if (inPublishedtoPublicViewAt == null)
                    dbConn.Cmd.Parameters.Add("@inPublishedtoPublicViewAt", SqlDbType.DateTime).Value = DBNull.Value;
                else
                dbConn.Cmd.Parameters.Add("@inPublishedtoPublicViewAt", SqlDbType.DateTime).Value = inPublishedtoPublicViewAt;

                dbConn.Cmd.Parameters.Add("@inAlertOutOfStock", SqlDbType.Bit).Value = inAlertOutOfStock;
                dbConn.Cmd.Parameters.Add("@inAgeGroupID", SqlDbType.Int).Value = inAgeGroupID;
                dbConn.Cmd.Parameters.Add("@inSubCategoryID", SqlDbType.Int).Value = inSubCategoryID;
                dbConn.Conn.Open();
                dbConn.Cmd.ExecuteNonQuery();
                dbConn.Conn.Close();


                //get last productID Identity for the newly product inserted
                dbConn.Cmd.CommandText = " SELECT max(productID) FROM product ";
                dbConn.Conn.Open();
                productID = dbConn.Cmd.ExecuteScalar().ToString();
                dbConn.Conn.Close();

                //price table
                dbConn.Cmd.CommandText = " INSERT INTO Price (InitalPrice,ProductID) VALUES (@inInitalPrice,@inProductID) ";
                dbConn.Cmd.Parameters.Add("@inInitalPrice", SqlDbType.Decimal).Value = inInitalPrice;
                dbConn.Cmd.Parameters.Add("@inProductID", SqlDbType.Int).Value = productID;
                dbConn.Conn.Open();
                dbConn.Cmd.ExecuteNonQuery();
                dbConn.Conn.Close();

                //remove all sql object from sqlparametersconnection.
                dbConn.Cmd.Parameters.Clear();


                //product_Category Table
                dbConn.Cmd.CommandText = " INSERT INTO Product_Category (ProductID,CategoryID) VALUES (@inProductID,@inCategoryID)";
                dbConn.Cmd.Parameters.Add("@inCategoryID", SqlDbType.Int).Value = inCategoryID;
                dbConn.Cmd.Parameters.Add("@inProductID", SqlDbType.Int).Value = productID;
                dbConn.Conn.Open();
                dbConn.Cmd.ExecuteNonQuery();
                dbConn.Conn.Close();
                dbConn.Cmd.Parameters.Clear();

                ////point table
                dbConn.Cmd.CommandText = "INSERT INTO Point (PointsNeeded,PointsEarned,ProductID) VALUES (@inPointsNeeded,@inPointsEarned,@inProductID) ";
                dbConn.Cmd.Parameters.Add("@inPointsNeeded", SqlDbType.Int).Value = inPointsNeeded;
                dbConn.Cmd.Parameters.Add("@inPointsEarned", SqlDbType.Int).Value = inPointsEarned;
                dbConn.Cmd.Parameters.Add("@inProductID", SqlDbType.Int).Value = productID;
                dbConn.Conn.Open();
                dbConn.Cmd.ExecuteNonQuery();
                dbConn.Conn.Close();
                dbConn.Cmd.Parameters.Clear();

                ////stock table
                dbConn.Cmd.CommandText = " INSERT INTO Stock (StockQuantities,Availability,ProductID) VALUES (@inStockQuantities,@inAvailability,@inProductID) ";
                dbConn.Cmd.Parameters.Add("@inStockQuantities", SqlDbType.Int).Value = inStockQuantities;
                dbConn.Cmd.Parameters.Add("@inAvailability", SqlDbType.Bit).Value = inAvailability;
                dbConn.Cmd.Parameters.Add("@inProductID", SqlDbType.Int).Value = productID;
                dbConn.Conn.Open();
                dbConn.Cmd.ExecuteNonQuery();
                dbConn.Conn.Close();

            }//end of try block
            catch (SqlException ex)
            {
                if (ex.Message.Contains("ProductNameUniqueConstraint") == true)
                {
                    checkConstraint = false;
                }
              
            }

            return checkConstraint;
        }//end of AddOneProduct

        public object getOneProduct(string inProductID)
        {
            DbConnection dbConn = new DbConnection();
            dbConn.Cmd.CommandText = " SELECT Product.ProductID,Product.ProductCode, Product.ProductName, " +
            " Product.Description, Price.InitalPrice, Point.PointsNeeded, " +
                                      " Point.PointsEarned, Stock.StockQuantities, Product.ViewablebyPublic, Stock.Availability," +
                                      " Stock.ZeroQuantityAlert, Product_Category.CategoryID, Product_SubCategory.SubCategoryID " +
                                      " FROM Product LEFT JOIN " +
                                      " Price ON Product.PriceID = Price.PriceID LEFT JOIN " +
                                      " Point ON Product.ProductID = Point.ProductID LEFT JOIN " +
                                      " Product_Category ON Product.ProductID = Product_Category.ProductID LEFT JOIN " +
                                      " Product_SubCategory ON Product.ProductID = Product_SubCategory.ProductID LEFT JOIN " +
                                      " Stock ON Product.StockID = Stock.StockID WHERE Product.ProductID = @inProductID";
            dbConn.Cmd.Parameters.Add("@inProductID", SqlDbType.Int).Value = inProductID;


            dbConn.Fill();

            DataRow dr = dbConn.Dt.Rows[0];

            var genericObj = new
            {
                ProductID = Int32.Parse(dr["ProductID"].ToString()),
                ProductCode = dr["ProductCode"].ToString(),
                ProductName = dr["ProductName"].ToString(),
                Description = dr["Description"].ToString(),
                ProductPrice = dr["InitalPrice"].ToString(),
                PointsNeeded = dr["PointsNeeded"].ToString(),
                PointsEarned = dr["PointsEarned"].ToString(),
                StockQuantities = Int32.Parse(dr["StockQuantities"].ToString()),
                ViewableByPublic = dr["ViewableByPublic"].ToString(),
                Availability = dr["Availability"].ToString(),
                ZeroQuantityAlert = dr["ZeroQuantityAlert"].ToString(),
                CategoryID = dr["CategoryID"].ToString(),
                SubCategoryID = dr["SubCategoryID"].ToString()
            };


            return genericObj;
        }
    }
}