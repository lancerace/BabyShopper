<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UpdateOneProduct.aspx.cs" Inherits="WEBACA2.LittleShopperManagement.UpdateOneProduct" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Update Product</title>


    <script src="../Scripts/jquery-2.1.4.min.js"></script>
    <script src="../Scripts/jquery-ui-1.11.4.min.js"></script>
    <script src="../Scripts/bootstrap.min.js"></script>


    <script src="../Scripts/notify.min.js"></script>
    <script src="../Scripts/fileinput.min.js"></script>
    <link href="../Style/jqx.base.css" rel="stylesheet" />
    <script src="../Scripts/jqxcore.js"></script>
    <script src="../Scripts/jqxbuttons.js"></script>
    <script src="../Scripts/jqxscrollbar.js"></script>
    <script src="../Scripts/jqxpanel.js"></script>
    <script src="../Scripts/jqxtree.js"></script>
    <script src="../Scripts/jqxcheckbox.js"></script>




    <link href="../Style/bootstrap.min.css" rel="stylesheet" />
    <link href="../Style/bootstrap-theme.min.css" rel="stylesheet" />
    <link href="../Style/fileinput.min.css" rel="stylesheet" />

    <link href="../Style/jqx.bootstrap.css" rel="stylesheet" />
    <link href="../Style/MyStyle.css" rel="stylesheet" />

</head>
<body>

    <div class="container">
        <div class="row">
            <div class="col-md-offset-1 col-md-8">
                <div class="panel panel-info">
                    <div class="modal-header-primary">
                        <h3>Update Product</h3>
                    </div>
                    <div class="panel-body">

                        <form class="form-horizontal" role="form">

                            <div class="form-group">
                                <label class="col-md-3 control-label">Product Name:</label>
                                <div class="col-md-9">
                                    <%--form-control make width to 100%--%>
                                    <input type="text" class="form-control" name="fullNameInput"
                                        id="productNameText" placeholder="Product Name" />
                                </div>
                            </div>


                            <div class="form-group">
                                <label class="col-md-3 control-label">Product Code</label>
                                <div class="col-md-9">
                                    <%--form-control make width to 100%--%>
                                    <input type="text" class="form-control" name="fullNameInput"
                                        id="productCodeText" placeholder="Product Code" />
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="col-md-3 control-label">Product Price</label>
                                <div class="col-md-9">
                                    <%--form-control make width to 100%--%>
                                    <input type="text" class="form-control" name="fullNameInput"
                                        id="ProductPriceText" placeholder="Product Price" />
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="col-md-3 control-label">Description:</label>

                                <div class="col-md-9">
                                    <textarea id="descriptionText" rows="10" class="form-control" style="resize: none"></textarea>
                                </div>
                            </div>


                            <div class="form-group">
                                <label class="col-md-3 control-label">Points Needed</label>
                                <div class="col-md-9">
                                    <%--form-control make width to 100%--%>
                                    <input type="text" class="form-control" name="fullNameInput"
                                        id="PointsNeededText" placeholder="Points Needed" />
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="col-md-3 control-label">Points Earned</label>
                                <div class="col-md-9">
                                    <%--form-control make width to 100%--%>
                                    <input type="text" class="form-control" name="fullNameInput"
                                        id="PointsEarnedText" placeholder="Points Earned" />
                                </div>
                            </div>


                            <div class="form-group">
                                <label class="col-md-3 control-label">Quantites</label>
                                <div class="col-md-9">
                                    <%--form-control make width to 100%--%>
                                    <input type="text" class="form-control" name="fullNameInput"
                                        id="QuantitesText" placeholder="SubBrand Name" />
                                </div>
                            </div>


                            <div class="form-group">
                                <label class="col-md-3 control-label">Stock Availability</label>
                                <div class="col-md-9">
                                    <fieldset>
                                        Yes
                                        <input type="radio" name="stock" value="1" id="stockAvailabilityRadioBtn1" checked="true" />
                                        No
                                        <input type="radio" name="stock" value="1" id="sstockAvailabilityRadioBtn2" />
                                    </fieldset>
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="col-md-3 control-label">Viewable By Public</label>
                                <div class="col-md-9">
                                    <fieldset>
                                        Yes
                                        <input type="radio" name="viewable" value="1" id="ViewableByPublicBtn1" checked="true" />
                                        No
                                        <input type="radio" name="viewable" value="1" id="ViewableByPublicBtn2" />
                                    </fieldset>
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="col-md-3 control-label">Alert Me If Quantity Drop To Zero</label>
                                <div class="col-md-9">
                                    <fieldset>
                                        Yes
                                        <input type="radio" name="alert" value="1" id="AlertBtn1" checked="true" />
                                        No
                                        <input type="radio" name="alert" value="1" id="AlertBtn2" />
                                    </fieldset>
                                </div>
                            </div>

                            <div class="form-group">
                                <div>
                                    <div class="col-md-3 control-label">
                                        Brand:
                                    </div>
                                    <select class="dropdown" id="BrandListBox"></select>


                                </div>
                            </div>

                            <div class="form-group">
                                <div>
                                    <div class="col-md-3 control-label">
                                        SubBrand:
                                    </div>
                                    <select class="dropdown" id="SubBrandListBox"></select>
                                </div>
                            </div>


                            <div class="form-group">
                                <div class="col-md-3">
                                    <div id="CategoryTree" class="col-md-9 ">
                                    </div>
                                </div>
                            </div>




                            <div id="selectedValue"></div>


                            <div class="form-group">
                                <%--bootstrap-fileinput jQuery plugin correspond to --%>
                                <%--ajax upload of multiple images ( name = "photoInput[]" )--%>
                                <%--http://krajeesolutions.tumblr.com/--%>
                                <input id="photoInput" name="photoInput[]" type="file" class="file input-group-lg" multiple="true" />
                            </div>

                            <div class="form-group">
                                <div class="col-md-3 .col-md-offset-3">
                                    <button type="button" id="saveButton" class="btn btn-default">Save</button>
                                </div>
                            </div>
                        </form>

                    </div>
                </div>
            </div>
            <!-- end of div having class="col-md-offset-1 col-md-8" which has a panel containing the form -->
        </div>
        <!-- end of the div having the class="row" -->
    </div>

    <script>
        //window (global variable) to represent treeview 
        window.$CategoryTreeView = $('#CategoryTree');
        window.$photoInputElement = $('#photoInput');

        //Use ajax call to get all Cate,sub-cate,brand and sub-brand data,store all the object in array for treeView preparation
        var CategoryArray = new Array();
        var SubCategoryArray = new Array();
        var BrandArray = new Array();
        var SubBrandArray = new Array();
        var ProductImages;
        var InitialPreviewArray = new Array();
        var InitialPreviewConfigArray = new Array();

         
        window.$collectedProductID = GetQueryString()['ProductID'];
        //window.$collectedProductID = 1;
        GetAllImage();

        var overloadActionsTemplate = '<div class="file-actions">\n' +
        //'<div align="center">Primary</div>' +
        //'<div align="center"><input type="radio" id="primaryPhoto"></div>' +
        '<div class="file-footer-buttons">\n' +
        '    {delete}' +
        '    </div>\n' +
        '    <div class="file-upload-indicator" tabindex="-1" title="{indicatorTitle}">{indicator}</div>\n' +
        '    <div class="clearfix"></div>\n' +
        '</div>';

        function GetAllImage() {
            $.ajax({
                type: 'POST',
                async: false,
                url: 'UpdateOneProduct.aspx/getAllImagesByProductID',
                data: "{'inProductID' : '" + window.$collectedProductID + "'}",  //passing data to server side
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',

            }).done(function (data, textStatus, jqXHR) {
                ProductImages = data.d;

            });//end of ajax call
        }




        function setupFileInput() {
            window.$photoInputElement.fileinput({
                uploadUrl: 'UpdateProductPhoto_Handler.ashx',
                deleteUrl: 'DeleteProductPhoto_Handler.ashx',
                uploadAsync: false,
                overwriteInitial: true,
                initialPreviewCount: 0,
                //set this in order to post to server side deletesubbrandphoto_handler. without this.error will be shown in file input
                ajaxDeleteSettings: { method: 'POST' },
                //only allow image file
                //only accept image file .validation
                allowedFileTypes: ['image'],
                showUpload: false,
                showCaption: false,
                showUploadedThumbs: true,
                browseClass: 'btn btn-primary pull-right',
                previewFileIcon: '<i class="glyphicon glyphicon-king"></i>',
                maxFileCount: 5,
                layoutTemplates: {
                    actions: overloadActionsTemplate
                },
        
                initialPreview: InitialPreviewArray,
                //////initialpreviewConfig has to be set in order for thumbnail 'delete' to show on picture
                initialPreviewConfig: InitialPreviewConfigArray,

                //post to server with extra data. 
                //extra data for ashx to update subbrand detail
                uploadExtraData: function () {

                    var out = {};
                            out['ProductID'] = window.$collectedProductID;
                            return out;


                    //var ProductID = window.$collectedProductID;
                    ////var out = {}, ProductID;
                    //var out = ProductID;

                    //return out;
                },
            });
        }//end of setupFileInput




        function GetQueryString() {
            var vars = [], hash;
            var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
            for (var i = 0; i < hashes.length; i++) {
                hash = hashes[i].split('=');
                vars.push(hash[0]);
                vars[hash[0]] = hash[1];
            }
            return vars;
        }

        //get productID from querystring first
        //asume productid = 4
        //window.$collectedProductID = '4'
       

        //setup initial preview and initialpreviewconfig before initializing fileinput
        for (index = 0; index < ProductImages.length; index++) {
            var image = ProductImages[index];

            var tempInitialPreview = '<img src="GetOneProductPhoto.ashx?id=' + image.ProductImageID + '" class="file-preview-image" alt="" title="">';
            InitialPreviewArray.push(tempInitialPreview);

            var tempInitialPreviewConfig = {
                caption: image.ProductImageName,
                width: '',
                url: 'http://localhost:18348/LittleShopperManagement/DeleteProductPhoto_Handler.ashx',
                key: image.ProductImageID,
                extra: { id: image.ProductImageID }
            }
            InitialPreviewConfigArray.push(tempInitialPreviewConfig);
        }//end for 

        //finally,initialize the fileinput 
        setupFileInput();



      
            $.ajax({
                type: 'POST',
                async: false,
                url: 'UpdateOneProduct.aspx/GetAllBrand',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
            }).done(function (data, textStatus, jqXHR) {
                var BrandList = data.d;

                for (index = 0; index < BrandList.length ; index++) {

                    var collectedBrandID = BrandList[index].BrandID;
                    var collectedBrandName = BrandList[index].BrandName;

                    //an object of Brand per loop
                    var tempBrand =
                    {
                        BrandID: collectedBrandID,
                        BrandName: collectedBrandName
                    }
                    //push to BrandArray
                    BrandArray.push(tempBrand);
                }
            })

            $.ajax({
                type: 'POST',
                async: false,
                url: 'UpdateOneProduct.aspx/GetAllSubBrand',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
            }).done(function (data, textStatus, jqXHR) {
                var SubBrandList = data.d;

                for (index = 0; index < SubBrandList.length ; index++) {


                    var collectedSubBrandID = SubBrandList[index].SubBrandID;
                    var collectedSubBrandName = SubBrandList[index].SubBrandName;
                    var collectedBrandID = SubBrandList[index].BrandID;
                    //an object of SubBrand per loop

                    var tempSubBrand =
                    {
                        SubBrandID: collectedSubBrandID,
                        SubBrandName: collectedSubBrandName,
                        BrandID: collectedBrandID
                    }
                    //push to SubBrandArray
                    SubBrandArray.push(tempSubBrand);
                }
            })



            $.ajax({
                type: 'POST',
                async: false,
                url: 'UpdateOneProduct.aspx/GetAllCategory',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
            }).done(function (data, textStatus, jqXHR) {
                var categoryList = data.d;

                for (index = 0; index < categoryList.length ; index++) {
                    var collectedCategoryID = categoryList[index].CategoryID;
                    var collectedCategoryName = categoryList[index].CategoryName;

                    //an object of category per loop
                    var tempCategory =
                    {
                        CategoryID: collectedCategoryID,
                        CategoryName: collectedCategoryName
                    }
                    //push to categoryArray
                    CategoryArray.push(tempCategory);
                }
            })


            $.ajax({
                type: 'POST',
                async: false,
                url: 'UpdateOneProduct.aspx/GetAllSubCategory',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',

            }).done(function (data, textStatus, jqXHR) {
                var subCategoryList = data.d;

                for (index = 0; index < subCategoryList.length ; index++) {
                    var collectedSubCategoryID = subCategoryList[index].SubCategoryID;
                    var collectedSubCategoryName = subCategoryList[index].SubCategoryName;
                    var collectedCategoryID = subCategoryList[index].CategoryID;

                    var tempSubCategory =
                  {
                      SubCategoryID: collectedSubCategoryID,
                      SubCategoryName: collectedSubCategoryName,
                      CategoryID: collectedCategoryID

                  }
                    //push to SubCategoryArray
                    SubCategoryArray.push(tempSubCategory);
                }

            })






        function prepareFormDetail() {

            $.ajax({
                type: 'POST',
                async: false,
                url: 'UpdateOneProduct.aspx/GetOneProduct',
                data: "{'inProductID' : '" + window.$collectedProductID + "'}",
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
            }).done(function (data, textStatus, jqXHR) {


                //category treeview
                var $ulMainElement = $('<ul></ul>');
                //var $ulCategoryElement = $('<ul></ul>');
                for (index = 0 ; index < CategoryArray.length ; index++) {
                    var tempCategory = CategoryArray[index];
                    var collectedCategoryID = tempCategory.CategoryID;

                    //bind information to list item
                    var $liCategoryElement = $('<li></li>', { text: tempCategory.CategoryName }, { label: 'yo' });
                    $liCategoryElement.attr('item-value','Category:' + collectedCategoryID);
              

                    var $ulSubCategoryElement = $('<ul></ul>');


                    //loop each subcategory object,
                    //add all subcategory to associated category
                    for (indexx = 0 ; indexx < SubCategoryArray.length ; indexx++) {
                        var tempSubCategory = SubCategoryArray[indexx];

                        //bind information to list item
                        var $liSubCategoryElement = $('<li></li>', { text: tempSubCategory.SubCategoryName });
                        $liSubCategoryElement.attr('item-value','SubCategory:' + tempSubCategory.SubCategoryID)
                        $liSubCategoryElement.attr('label', 'SubCategory');


                        //if the brand id is = tempCategory.CategoryID, append to $ulSubCategoryElement
                        if (tempSubCategory.CategoryID == collectedCategoryID)
                            $ulSubCategoryElement.append($liSubCategoryElement);


                    }//end for  
                    $liCategoryElement.append($ulSubCategoryElement);
                    $ulMainElement.append($liCategoryElement);
                }
                window.$CategoryTreeView.append($ulMainElement);
                //initalize
                window.$CategoryTreeView.jqxTree({ height: '400px', hasThreeStates: true, checkboxes: true, width: '330px' });
                window.$CategoryTreeView.css('visibility', 'visible');


                ////dropdownlist
                $('#BrandListBox').append($('<option>', {
                    value: '',
                    text: '--Select Brand--'
                }));
                $('#SubBrandListBox').append($('<option>', {
                    value: '',
                    text: '--Select SubBrand--'
                }));

                for (index = 0 ; index < BrandArray.length ; index++) {
                    var tempBrand = BrandArray[index];
                    var collectedBrandID = tempBrand.BrandID;
                    $BrandOptionElement = $('<option></option>', { value: collectedBrandID, text: tempBrand.BrandName });
                    $('#BrandListBox').append($BrandOptionElement);
                }
                for (indexx = 0 ; indexx < SubBrandArray.length ; indexx++) {
                    var tempSubBrand = SubBrandArray[indexx];
                    $optionElement = $('<option></option>', { value: tempSubBrand.SubBrandID, text: tempSubBrand.SubBrandName });
                    $('#SubBrandListBox').append($optionElement);
                }

                var product = data.d;
                productNameText.value = product.ProductName;
                productCodeText.value = product.ProductCode;
                ProductPriceText.value = product.ProductPrice;
                descriptionText.value = product.Description;
                PointsNeededText.value = product.PointsNeeded;
                PointsEarnedText.value = product.PointsEarned;
                QuantitesText.value = product.StockQuantities;
                if (product.Availability == "False")
                    $('#stockAvailabilityRadioBtn2').attr('checked', 'checked');
                else
                    $('#stockAvailabilityRadioBtn1').attr('checked', 'checked');

                if (product.ViewableByPublic == "False")
                    $('#ViewableByPublicBtn2').attr('checked', 'checked');
                else
                    $('#ViewableByPublicBtn1').attr('checked', 'checked');

                if (product.ZeroQuantityAlert=="False")
                $('#AlertBtn2').attr('checked', 'checked');
                else
                    $('#AlertBtn1').attr('checked', 'checked');


            }).fail(function (jqXHR, textStatus, errorThrown) {//An alternative to the deprecated .error callback option
                console.log('The function attached to the ajax\'s fail() method has been executed.')
                console.log('The jqXRH Status Error Code is : ' + jqXHR.status);
                console.log(jqXHR.responseJSON.Message);
                console.log(responseObject.status);
                $.notify(jqXHR.responseJSON.Message, 'Error! Please contact the administrator if problem still exist.');
            })//end fail    
        }//end of prepareFormDetail

        prepareFormDetail();

        var overloadActionsTemplate = '<div class="file-actions">\n' +
   //'<div align="center">Primary</div>' +
   //'<div align="center"><input type="radio" id="primaryPhoto"></div>' +
   '<div class="file-footer-buttons">\n' +
   '    {delete}' +
   '    </div>\n' +
   '    <div class="file-upload-indicator" tabindex="-1" title="{indicatorTitle}">{indicator}</div>\n' +
   '    <div class="clearfix"></div>\n' +
   '</div>';

        //hasThreeStates =checked, unchecked and indeterminate.
        //checkboxes: true = each item display with checkbox


       

        //create a class with javascript constructor function "WebFormData" for ProductClass
        function WebFormData(inProductID, inproductNameText, inproductCodeText, inProductPriceText, inproductPointsEarned, inproductPointsNeeded, indescriptionText, inStockQuantities, inStockAvailability, inAlertOutOfStock, inViewableByPublic, inCategoryID, insubCategoryID) {
            this.productID = inProductID;
            this.productNameText = inproductNameText;
            this.productCodeText = inproductCodeText;
            this.productPriceText = inProductPriceText;
            this.productPointsEarned = inproductPointsEarned;
            this.productPointsNeeded = inproductPointsNeeded;
            this.description = indescriptionText;
            this.StockQuantities = inStockQuantities;
            this.StockAvailability = inStockAvailability;
            this.AlertOutOfStock = inAlertOutOfStock;
            this.ViewableByPublic = inViewableByPublic;
            this.CategoryID = inCategoryID;
            this.SubCategoryID = insubCategoryID;
        }

        $('#saveButton').on('click', function (event) {



            //get all data from user input
            collectedproductNameText = productNameText.value;
            collectedproductCodeText = productCodeText.value;
            collectedProductPriceText = ProductPriceText.value;
            collectedPointsNeededText = PointsNeededText.value;
            collectedPointsEarnedText = PointsEarnedText.value;
            collecteddescriptionText = descriptionText.value;
            collectedQuantitesText = QuantitesText.value;
            collectedstockAvailability = 0;
            collectedViewableByPublic = 0;
            collectedAlert = 0;
            if ($('#stockAvailabilityRadioBtn1').checked) {
                collectedstockAvailability = stockAvailabilityRadioBtn1.value;
            }
            if ($('#ViewableByPublicBtn1').checked) {
                collectedViewableByPublic = ViewableByPublicBtn1.value;
            }
            if ($('#AlertBtn1').checked) {
                collectedAlert = AlertBtn1.value;
            }
            var collectedCategoryOption = $('#categoryListBox').find(":selected").value;
            var collectedSubCategoryOption = $('#SubcategoryListBox').find(":selected").value;
             

            var itemsChecked = window.$CategoryTreeView.jqxTree('getCheckedItems');
            var collectedIDarray = new Array();
            //collect all checked items
            console.log('itemChecked'+ itemsChecked);
            for (i = 0 ; i < itemsChecked.length ; i++) {
                // add/push to collectedCheckSubCategoryIDarray
                collectedIDarray.push(itemsChecked[i].value);
            }
  
            ////collect checked Category
            //var collectedCheckCategoryIDarray = new Array();
            ////gets the item's LI tag. with ('getCheckedItems', 'html element')
            //var CheckedCategoryLiElements = window.$CategoryTreeView.jqxTree('getCheckedItems', 'parentElement');
            //for (i = 0 ; i < CheckedCategoryLiElements.length ; i++) {
            //    // add/push to collectedCheckSubCategoryIDarray
            //    collectedCheckCategoryIDarray.push(CheckedCategoryLiElements[i].value);
            //}

            //console.log(CheckedCategoryLiElements);

            //var collectedCheckSubCategoryIDarray = new Array();
            ////gets the item's LI tag. with ('getCheckedItems', 'html element')
            //var CheckedSubCategoryLiElements = window.$CategoryTreeView.jqxTree('getCheckedItems', 'element');
            //for (i = 0 ; i < CheckedSubCategoryLiElements.length ; i++) {
            //    // add/push to collectedCheckSubCategoryIDarray
            //    collectedCheckSubCategoryIDarray.push(CheckedSubCategoryLiElements[i].value);
            //}


            //console.log(collectedCheckCategoryIDarray);
            //console.log(collectedCheckSubCategoryIDarray);

            //call upon javascript constructor
            var webformData = new WebFormData(window.$collectedProductID, collectedproductNameText, collectedproductCodeText,
                collectedProductPriceText, collectedPointsEarnedText, collectedPointsNeededText, collecteddescriptionText,
                collectedQuantitesText, collectedstockAvailability, collectedAlert, collectedViewableByPublic);
            //webformData constructor

            var stringifiedWebFormData = JSON.stringify(webformData);//serialize and send to server side
            //var stringifedSubCategoryIDarray = JSON.stringify(collectedCheckSubCategoryIDarray);
            var stringifiedItemsChecked = JSON.stringify(collectedIDarray);

            $.ajax({
                type: 'POST',
                url: 'UpdateOneProduct.aspx/UpdateProduct',               
                data: "{'WebFormDataParameter' : '[" + stringifiedWebFormData + ',' + stringifiedItemsChecked + "]'}",
                contentType: 'application/json; charset=utf-8',
                dataType:'json',
                async: false,
            }).done(function (data, textStatus, jqXHR) { //An alternative to the deprecated .success callback option
                responseObject = data.d;
               
                if (responseObject.status == 'success') {

                    $.notify("Product Updated Successfully", 'success');

                }//end if
                else {
                    $.notify("Product Update Failed!!", 'danger');
                }
            })//end done         
    .fail(function (jqXHR, textStatus, errorThrown) {//An alternative to the deprecated .error callback option
        console.log('The function attached to the ajax\'s fail() method has been executed.')
        console.log('The jqXRH Status Error Code is : ' + jqXHR.status);
        console.log(jqXHR.responseJSON.Message);
        console.log(responseObject.status);
        $.notify(jqXHR.responseJSON.Message, 'Error! Please contact the administrator if problem still exist.');
    })//end fail    


            
          
            window.$photoInputElement.fileinput("uploadBatch");

        })//end of  $('#saveButton').on('click', function (event)

        //==============================================================================================================================







        window.$photoInputElement.on('filedeleted', function (event, key, data) {

            //in json format,need to destringify
            var responsetxt = data.responseText;
            var parsedResponseTxt = JSON.parse(responsetxt);


            $.notify(parsedResponseTxt.status + ' ' + parsedResponseTxt.message);
        })









    </script>


</body>
</html>
