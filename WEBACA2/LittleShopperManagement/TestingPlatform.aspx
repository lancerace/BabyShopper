<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestingPlatform.aspx.cs" Inherits="WEBACA2.LittleShopperManagement.TestingPlatform" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>

    <script src="../Scripts/jquery-2.1.4.min.js"></script>
    <script src="../Scripts/jquery-ui-1.11.4.min.js"></script>
  <link href="../Style/jqx.base.css" rel="stylesheet" />
    <script src="../Scripts/bootstrap.min.js"></script>
    <script src="../Scripts/notify.min.js"></script>
    <script src="../Scripts/fileinput.min.js"></script>
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

  <%--  <img src="GetOneProductPhoto.ashx?ProductID=1" />--%>

    <img src="GetSubBrandPhoto_Handler.ashx?id=1" />
    <div class="form-group">
        <div class="col-md-3">
            <div id="jqxTree" class="col-md-8 ">
            </div>
        </div>
    </div>


    <script>


        var tempInitialPreview = '<img src="GetOneProductPhoto.ashx?id=' + 1;


        window.$treeView = $('#jqxTree');
        var CategoryArray = new Array();
        var SubCategoryArray = new Array();

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




        var a = $('<ul></ul>');

        var $ulMainElement = $('<ul></ul>');
        var $ulCategoryElement = $('<ul></ul>');
        for (index = 0 ; index < CategoryArray.length ; index++) {
            var tempCategory = CategoryArray[index];
            var collectedCategoryID = tempCategory.CategoryID;

            var $liCategoryElement = $('<li></li>', { text: tempCategory.CategoryName });

            //$liCategoryElement.attr('item-value', collectedCategoryID);

            var $ulSubCategoryElement = $('<ul></ul>');
            //loop each subcategory object,
            for (indexx = 0 ; indexx < SubCategoryArray.length ; indexx++) {
                var tempSubCategory = SubCategoryArray[indexx];
                var $liSubCategoryElement = $('<li></li>', { text: tempSubCategory.SubCategoryName });

                //if the brand id is = tempCategory.CategoryID, append to $ulSubCategoryElement
                if (tempSubCategory.CategoryID == collectedCategoryID)
                    $ulSubCategoryElement.append($liSubCategoryElement);
            }//end for  
            $liCategoryElement.append($ulSubCategoryElement);

            $ulMainElement.append($liCategoryElement);
        }

      



        window.$treeView.append($ulMainElement);
        //initalize
        $('#jqxTree').jqxTree({ height: '400px', hasThreeStates: true, checkboxes: true, width: '330px' });
        $('#jqxTree').css('visibility', 'visible');



    </script>

</body>
</html>
