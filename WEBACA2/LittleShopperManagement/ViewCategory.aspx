  <%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewCategory.aspx.cs" Inherits="WEBACA2.LittleShopperManagement.ViewCategory" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>View Category</title>
  <script src="../Scripts/jquery-2.1.4.min.js"></script>
    <script src="../Scripts/jquery-ui-1.11.4.min.js"></script>
    <script src="../Scripts/bootstrap.min.js"></script>
    <script src="../Scripts/notify.min.js"></script>
    <script src="../Scripts/fileinput.min.js"></script>
    <link href="../Style/bootstrap.min.css" rel="stylesheet" />
    <link href="../Style/bootstrap-theme.min.css" rel="stylesheet" />
    <link href="../Style/fileinput.min.css" rel="stylesheet" />
    <link href="../Style/MyStyle.css" rel="stylesheet" />
</head>
<body>
    <div class="container">
        <div class="modal-header-primary">
            <h2>View Category</h2>
        </div>

        <table id="categoryTable" class="table table-condensed table-bordered table-hover table-responsive">
            <thead>
                <tr>
                    <th>Category ID</th>
                    <th>Category Name</th>
                    <th>Date Created</th>
                    <th>Date Updated</th>
                </tr>
            </thead>
            <tbody></tbody>

        </table>

        <input type="button" id='addButton' value="Add" class="btn btn-primary" data-toggle="modal" data-target="#categoryModal" />
        <%--data-toggle='Modal' create a modal window--%>
        <%--data-target point to id of modal--%>


        <%--data-trackbutton to store which button review this popup--%>
        <%--data-id to store categoryid. Retrieve editButton ['data-id': categoryID] value of the particular row clicked--%> 
        <div id="categoryModal" class="modal" data-trackbutton="" data-id="">
            <div class="modal-dialog">

                <%--Modal Content--%>
                <div class="modal-content">

                    <%--header--%>
                    <div class="modal-header modal-header-primary">
                        Manage Category
                    </div>


                    <div class="form-group">
                        <div class="modal-body">
                            Category Name:
                            <input type="text" id="categoryNameText" class="form-control" />
                        </div>
                        <div class="modal-footer">
                            <input type="button" value="Submit" id="submitButton" class="btn btn-default" data-dismiss="modal" />
                            <input type="button" value="Close" class="btn btn-default" data-dismiss="modal" />
                            <%--data-dismiss attribute close modal popup--%>
                        </div>
                    </div>

                </div>
                <%-- end of modalContent--%>
            </div>
        </div>
        <%--end of categoryModal--%>
    </div>
    <!--end of container!-->

    <script>


        displayPageInfo();



        //show.bs.modal & data-* attribute technique is use to vary content of a single modal shared by multiple button
        //show.bs.modal event is fire whenever modal is visible to the user ,similiar to page_load
        $('#categoryModal').on('show.bs.modal', function (event) {
            categoryNameText.value = '';
            //using event.relatedTarget and HTML data-* attributes to vary content in modal
            var button = $(event.relatedTarget);        //get related button that trigger the modal

            if (button.attr('id') == 'editButton') {//if button clicked contain id=editButton,execute ajax getOneCategory
                $('#categoryModal').data('trackButton', 'editButton'); //use data attribute to track that modal is clicked by edit button              
                var getCategoryID = button.data('id');
                //store categoryID from button derived from event.relatedTarget to modal. This is for ajax UpdateOneCategory usage
                $('#categoryModal').data('id', getCategoryID);

                $.ajax({
                    type: 'POST',
                    url: 'ViewCategory.aspx/getOneCategory',
                    data: "{'categoryID' : '" + getCategoryID + "'}",  //passing data to server side in ajax call
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json',
                }).done(function (data, textStatus, jqXHR) {
                    var category = data.d;
                    var categoryName = category.CategoryName;
                    categoryNameText.value = categoryName;
                })//end of ajax call
            }//end if

        })//end of on 'show.bs.modal'

        //create a class with javascript constructor function "WebFormData" for Category Class
        function WebFormData(inCategoryId, inCategoryName) {
            this.CategoryId = inCategoryId;
            this.CategoryName = inCategoryName;
        }

        //modify modal trackButton data attribute value as editButton
        $('#editButton').on('click', function () {
            $('#categoryModal').data('trackButton', 'editButton');
        })

        //modify modal trackButton data attribute value as addButton
        $('#addButton').on('click', function () {
            $('#categoryModal').data('trackButton', 'addButton');
        })

        //based on data-trackButton attribute value. if value is editButton,execute ajax updateOneCategory,else,addOneCategory
        $('#submitButton').on('click', function (event) {

            var collectedCategoryName = $('#categoryNameText').val();
            //get categoryname from user input

            if ($('#categoryModal').data('trackButton') != "editButton") // execute addOneCategory if add button is clicked
            {
                //call upon javascript constructor
                var webformData = new WebFormData("", collectedCategoryName);
                //webformData constructor

                var stringifiedWebFormData = JSON.stringify(webformData);//serialize and send to server side

                //function ajax(methodName, inStringifiedwebFormData)
                ajax('addOneCategory', stringifiedWebFormData);

            }//end if
            else {
                
                var getCategoryIdFromModal = $('#categoryModal').data('id');
                //call upon javascript constructor
                var webformData = new WebFormData(getCategoryIdFromModal, collectedCategoryName);
               
                var stringifiedWebFormData = JSON.stringify(webformData);//serialize and send to server side

                //function ajax(methodName, inStringifiedWebFormData)
                ajax('updateOneCategory', stringifiedWebFormData);
            }//end if

            //remove tabledata of category table
            $('td').remove();
            displayPageInfo();


        })//end click function



        //loadData,table creation
        function displayPageInfo() {
            $.ajax({
                type: 'POST',
                //getallCategory ,returned by 'object=reponse'
                url: 'ViewCategory.aspx/getAllCategory',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                async: false
            }).done(function (data, textStatus, jqXHR) { //upon success, create table and display information
                //Variable Declaration
                var categoryList = data.d;
                var categoryID = '';
                var categoryName = '';
                var createdAt = '';
                var updatedAt = '';

                var convertedJavaScriptDateCreatedAt = '';
                var convertedJavaScriptDateUpdatedAt = '';
                //JavaScript Date Object Container


                var convertedCreatedAt = '';
                var convertedUpdatedAt = '';
                //Date Final Convertion

                var $tableElement = null;
                var $cellElement = null;
                var $rowElement = null;

                //$tableElement representing table tag
                $tableElement = $('#categoryTable');

                //loop through each category in CategoryList that was returned from (data.d)getAllCategory Method
                for (index = 0; index < categoryList.length; index++) {

                    categoryID = categoryList[index].CategoryID;
                    categoryName = categoryList[index].CategoryName;

                    createdAt = categoryList[index].CreatedAt;
                    updatedAt = categoryList[index].UpdatedAt;
                    //Date are in Microsoft json date format e.g /Date(1198908717056)/

                    convertedJavaScriptDateCreatedAt = new Date(parseInt(createdAt.substr(6)));
                    //substr(6) = Extract the first 6 letter. ending up with 1198908718056)/
                    //parseInt convert 1198908718056)/ to 1198908718056
                    //Constructor format for date = Date(dateString);
                    //toLocateDateString() + toLocateTimeString();  - > take the date format as dd/mm/yy + time
                    convertedJavaScriptDateUpdatedAt = new Date(parseInt(updatedAt.substr(6)));

                    convertedCreatedAt = convertedJavaScriptDateCreatedAt.toLocaleDateString() + " " + convertedJavaScriptDateCreatedAt.toLocaleTimeString();
                    convertedUpdatedAt = convertedJavaScriptDateUpdatedAt.toLocaleDateString() + " " + convertedJavaScriptDateUpdatedAt.toLocaleTimeString();

                    $rowElement = $('<tr></tr>');

                    $cellElement = $('<td></td>', { text: categoryID });
                    $rowElement.append($cellElement);
                    //append cell in row element


                    $cellElement = $('<td></td>', { text: categoryName });
                    //Create the 2nd column cell element which display CategoryName
                    $rowElement.append($cellElement);

                    $cellElement = $('<td></td>', { text: convertedCreatedAt });
                    //Create the 3rd column cell element which display Createdat
                    $rowElement.append($cellElement);

                    $cellElement = $('<td></td>', { text: convertedUpdatedAt });
                    //Create the 4th column cell element which display Updatedat
                    $rowElement.append($cellElement);

                    //$updatedCategoryLink = $('<a>Edit Category</a>');
                    $updatedCategoryLink = $('<input type="button" id="editButton" value="Edit Category" />');
                    $updatedCategoryLink.addClass('btn btn-primary');

                    //multiple attribute from .attr() 
                    //{attribute:value,attribute:value}
                    $updatedCategoryLink.attr({                        //for each button, set value attribute to store value of categoryID 
                        'data-toggle': 'modal',
                        'data-target': '#categoryModal',               //each button share the same modal
                        'data-id': categoryID                         
                    });
                    $updatedSubCategoryLink = $('<a>Manage SubCategory</a>').attr('href', 'ADMViewSubCategory.aspx?CategoryID=' + categoryID);
                    $updatedSubCategoryLink.addClass('btn btn-primary');


                    $cellElement = $('<td></td>');                  
                    $cellElement.append($updatedCategoryLink);
                    //append <a></a> link button to cell
                    $rowElement.append($cellElement);


                    //managesubcategory btn
                    $cellElement = $('<td></td>');
                    $cellElement.append($updatedSubCategoryLink);
                    $rowElement.append($cellElement);


                    //cellelement will be contained in rowElement <tr></tr>
                    $tableElement.append($rowElement);

                }//end of for loop

            })//end of the done() method;
        }


        //PostCondition
        //inMethodName = url: 'ViewCategory.aspx/' + inMethodName,  
        //inStringifiedWebFormData = data: "{'WebFormDataParameter' : '" + inStringifiedWebFormData + "'}",
        function ajax(inMethodName, inStringifiedWebFormData) {
            $.ajax({
                type: 'POST',
                url: 'ViewCategory.aspx/' + inMethodName,
                data: "{'WebFormDataParameter' : '" + inStringifiedWebFormData + "'}",
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                async: false
            }).done(function (data, textStatus, jqXHR) {
                responseObject = data.d;
                if (responseObject.status == "success") {
                    $.notify('Category Record Submission Success!', 'success');
                } else {
                    $.notify('Category Record Submission Failed!', 'danger');
                }
            })//end done
             .fail(function (jqXHR, textStatus, errorThrown) {
                 console.log('The function attached to the ajax\'s fail() method has been executed.')
                 console.log('The jqXRH Status Error Code is : ' + jqXHR.status);
                 console.log(jqXHR.responseJSON.Message);
                 $.notify(jqXHR.responseJSON.Message, 'Error! Please contact the administrator if problem still exist.');
             })//end fail     
        }//end ajax




    </script>
</body>
</html>



<%--documentation--%>
<%--  //    $.ajax({
            //        type: 'POST',
            //        url: 'ViewCategory.aspx/updateOneCategory',
            //        data: "{'WebFormDataParameter' : '" + StringifiedwebFormData + "'}",
            //        contentType: 'application/json; charset=utf-8',
            //        dataType: 'json',
            //        async: false
            //    }).done(function (data, textStatus, jqXHR) {
            //        responseObject = data.d;
            //        if (responseObject.status == "success") {
            //            $.notify('Category Record Submission Success!', 'success');
            //        } else {
            //            $.notify('Category Record Submission Failed!', 'danger');
            //        }
            //    })//end done
            //    .fail(function (jqXHR, textStatus, errorThrown) {
            //        console.log('The function attached to the ajax\'s fail() method has been executed.')
            //        console.log('The jqXRH Status Error Code is : ' + jqXHR.status);
            //        console.log(jqXHR.responseJSON.Message);
            //        $.notify(jqXHR.responseJSON.Message, 'Error! Please contact the administrator if problem still exist.');
            //    })//end fail--%>



<%--  //$.ajax({
                //    type: 'POST',
                //    url: 'ViewCategory.aspx/addOneCategory',
                //    data: "{'WebFormDataParameter' : '" + StringifiedwebFormData + "'}",
                //    contentType: 'application/json; charset=utf-8',
                //    dataType: 'json',
                //    async: false
                //}).done(function (data, textStatus, jqXHR) {
                //    responseObject = data.d;
                //    if (responseObject.status == "success") {
                //        $.notify('Category Record Submission Success!', 'success');
                //    } else {
                //        $.notify('Category Record Submission Failed!', 'danger');
                //    }
                //})//end done
                //    .fail(function (jqXHR, textStatus, errorThrown) {
                //        console.log('The function attached to the ajax\'s fail() method has been executed.')
                //        console.log('The jqXRH Status Error Code is : ' + jqXHR.status);
                //        console.log(jqXHR.responseJSON.Message);
                //        $.notify(jqXHR.responseJSON.Message, 'Error! Please contact the administrator if problem still exist.');
                //    })//end fail--%>