<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ADMViewProduct.aspx.cs" Inherits="WEBACA2.LittleShopperManagement.ADMViewProduct" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>View Product</title>
    <script src="../Scripts/jquery-2.1.4.min.js"></script>
    <script src="../Scripts/jquery-ui-1.11.4.min.js"></script>
    <script src="../Scripts/bootstrap.min.js"></script>
    <link href="../Content/bootstrap.min.css" rel="stylesheet" />
    <link href="../Content/bootstrap-theme.min.css" rel="stylesheet" />
    <link href="../Content/MyStyle.css" rel="stylesheet" />
    <script src="../Scripts/notify.min.js"></script>
    <script src="../Scripts/jquery.validate.js"></script>
</head>
<body>
   <div class="container">

        <div class="modal-header-primary">
            <h2>View Product</h2>
        </div>

        <table id="productTable" class="table table-condensed table-bordered table-hover table-responsive">
            <thead>
                <tr>
                    <th>Product ID</th>
                    <th>Product Name</th>
                    <th>Product Code</th>
                    <th>Point</th>
                    <th>Product Description</th>
                    <th>Brand Name</th>
                    <th>SubBrand</th>
                    <th>Stock</th>
                    <th>Viewable</th>
                    <th>PublishedToPublicViewAt</th>
                    <th>Date Created</th>
                    <th>Date Updated</th>
                </tr>
            </thead>
        </table>
       <input type="button" value="Add" class="btn btn-primary" data-toggle="modal" data-target="#productModal" />
       </div> 
    <script>
        jQuery.ajax({
            type: 'POST',
            //getallCategory ,returned by 'object=reponse'
            url: 'ADMViewProduct.aspx/getAllProductExcludingDeletedData',
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            async: false
        }).done(function (data, textStatus, jqXHR) {

            var productList = data.d;
            var productID = '';
            var productName = '';
            var ProductCode = '';
            var Point = '';
            var productDescription = '';
            var brandID = '';
            var SubBrandName='';
            var StockQuantities = '';
            var createdAt = '';
            var updatedAt = '';
            var PublishedToPublicViewAt ='';
            var convertedJavaScriptDateCreatedAt = '';
            //JavaScript Date Object Container
            var convertedJavaScriptDateUpdatedAt = '';
            //JavaScript Date Object Container
            var convertedJavaScriptDatePublishedToPublicViewAt='';
            var convertedCreatedAt = '';
            var convertedUpdatedAt = '';
            var convertedPublishedToPublicViewAt = '';
            var ViewablebyPublic = '';
            var $tableElement = null;
            var $cellElement = null;
            var $rowElement = null;

            //$tableElement representing table tag
            $tableElement = $('#productTable');
            //-------- Begin creating <tr> and <td> HTML element ------
            //loop through each CategoryList that was returned from getAllCategory Method
            for (index = 0; index < productList.length; index++) {

                productID = productList[index].ProductID;
                productName = productList[index].ProductName;
                productDescription = productList[index].Description;
                ProductCode = productList[index].ProductCode;
                Point = productList[index].PointsNeeded;
                BrandName = productList[index].BrandName;
                SubBrandName = productList[index].SubBrandName;
                StockQuantities = productList[index].StockQuantities;
                PublishedToPublicViewAt = productList[index].PublishedToPublicViewAt;
                ViewablebyPublic = productList[index].ViewablebyPublic;
                createdAt = productList[index].CreatedAt;
                updatedAt = productList[index].UpdatedAt;

                //Date are in Microsoft json date format e.g /Date(1198908717056)/

                convertedJavaScriptDateCreatedAt = new Date(parseInt(createdAt.substr(6)));
                //substr(6) = Extract the first 6 letter. ending up with 1198908718056)/
                //parseInt convert 1198908718056)/ to 1198908718056
                //Constructor format for date = Date(dateString);
                //toLocateDateString() + toLocateTimeString();  - > take the date format as dd/mm/yy + time
                convertedJavaScriptDateUpdatedAt = new Date(parseInt(updatedAt.substr(6)));
                convertedJavaScriptDatePublishedToPublicViewAt = new Date(parseInt(PublishedToPublicViewAt.substr(6)));
                convertedCreatedAt = convertedJavaScriptDateCreatedAt.toLocaleDateString() + " " + convertedJavaScriptDateCreatedAt.toLocaleTimeString();
                convertedUpdatedAt = convertedJavaScriptDateUpdatedAt.toLocaleDateString() + " " + convertedJavaScriptDateUpdatedAt.toLocaleTimeString();
                convertedPublishedToPublicViewAt = convertedJavaScriptDatePublishedToPublicViewAt.toLocaleDateString() + " " + convertedJavaScriptDatePublishedToPublicViewAt.toLocaleTimeString();
              



                $rowElement = $('<tr></tr>');
                $cellElement = $('<td></td>', { text: productID });
                $rowElement.append($cellElement);
                //append cell in row element


                $cellElement = $('<td></td>', { text: productName });

                $rowElement.append($cellElement);

                $cellElement = $('<td></td>', { text: ProductCode });
         
                $rowElement.append($cellElement);

                $cellElement = $('<td></td>', { text: Point });
          
                $rowElement.append($cellElement);

                $cellElement = $('<td></td>', { text: productDescription });
              
                $rowElement.append($cellElement);

                $cellElement = $('<td></td>', { text: BrandName });
              
                $rowElement.append($cellElement);
                
                $cellElement = $('<td></td>', { text: SubBrandName });
            
                $rowElement.append($cellElement);

                 $cellElement = $('<td></td>', { text: StockQuantities });
            
                $rowElement.append($cellElement);

                $cellElement = $('<td></td>', { text: ViewablebyPublic });
          
                $rowElement.append($cellElement);

                $cellElement = $('<td></td>', { text: convertedPublishedToPublicViewAt });
        
                $rowElement.append($cellElement);

                $cellElement = $('<td></td>', { text: convertedCreatedAt });
                //Create the 3rd column cell element which display Createdat
                $rowElement.append($cellElement);

                $cellElement = $('<td></td>', { text: convertedUpdatedAt });
                //Create the 3rd column cell element which display Createdat
                $rowElement.append($cellElement);

                $updatedProductLink = $('<a>Edit Product</a>').attr('href', 'UpdateOneProduct.aspx?ProductID=' + productID);
     
                $updatedProductLink.addClass('btn btn-primary');
                $cellElement = $('<td></td>');
                $cellElement.append($updatedProductLink);
                $rowElement.append($cellElement);
                $tableElement.append($rowElement);
            }

            //----End of creating <tr> and <td> HTML element ------


        }//end of JavaScript anonymous function
     )//end of the done() method;

    </script>  
</body>
</html>
