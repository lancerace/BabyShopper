<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ADMViewSubCategory.aspx.cs" Inherits="WEBACA2.LittleShopperManagement.ADMViewSubCategory" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>View SubCategory</title>
    <%--    <link href="../Content/MyStyle.css" rel="stylesheet" />--%>
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

        <div class="modal-header-primary"><h2>View SubCategory</h2></div>

        <table id="subCategoryTable" class="table table-condensed table-bordered table-hover table-responsive">
            <thead>
                <tr>
                    <th>SubCategory ID</th>
                    <th>Category Name</th>
                    <th>SubCategory Name</th>
                    <th>Date Created</th>
                    <th>Date Updated</th>
                    <th>Update</th>
                    <th>Delete</th>
                </tr>
            </thead>
            <tbody></tbody>

        </table>

        <input type="button" id="addSubCategory" value="Add" class="btn btn-primary" data-toggle="modal" data-target="#subCategoryModal" />


        <div id="subCategoryModal" class="modal" data-trackButton="" data-id="">
            <div class="modal-dialog">

                <%--Modal Content--%>
                <div class="modal-content">

                    <%--header--%>
                    <div class="modal-header modal-header-primary">
                        Manage SubCategory
                    </div>


                    <div class="form-group">
                        <div class="modal-body">
                            Category Name:
                            <select class="dropdown" id="categoryListBox"></select>
                        </div>
                        <div class="modal-body">
                            SubCategory Name:
                            <input type="text" id="subCategoryNameText" class="form-control" />
                        </div>
                        <div class="modal-footer">
                            <input type="button" value="Submit" id="submitButton" class="btn btn-default" data-dismiss="modal" />
                            <input type="button" value="Close" class="btn btn-default" data-dismiss="modal" />
                            <%--data-dismiss attribute to close modal popup--%>
                        </div>
                    </div>

                </div>
                <%-- end of modalContent--%>
            </div>
        </div>
        <%--end of categoryModal--%>

    <div id="deleteSubCategoryModal" class="modal" data-id="">
            <div class="modal-dialog">

                <%--Modal Content--%>
                <div class="modal-content">

                    <%--header--%>
                    <div class="modal-header modal-header-primary">
                        Remove SubCategory
                    </div>
                    <div class="form-group">
                        <div class="modal-body">
                            <div id="deleteSubCategoryLabel"></div>
                            
                        </div>                     
                        <div class="modal-footer">
                            <input type="button" value="Yes" id="deleteButton" class="btn btn-default" data-dismiss="modal" />
                            <input type="button" value="No" class="btn btn-default" data-dismiss="modal" />
                            <%--data-dismiss attribute to close modal popup--%>
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



        var CollectedCategoryID = GetQueryString()['CategoryID'];

        function displayinfo() {
            jQuery.ajax({
                type: 'POST',
                //getallCategory ,returned by 'object=reponse'
                url: 'ADMViewSubCategory.aspx/getAllSubCategory',
                data: "{'CategoryID' : '" + CollectedCategoryID + "'}",
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                async: false
            }).done(function (data, textStatus, jqXHR) {

                var subCategoryList = data.d;
                var subCategoryID = '';
                var categoryName = '';
                var subCategoryName = '';
                var createdAt = '';
                var updatedAt = '';
                var convertedJavaScriptDateCreatedAt = '';
                //JavaScript Date Object Container
                var convertedJavaScriptDateUpdatedAt = '';
                //JavaScript Date Object Container
                var convertedCreatedAt = '';
                var convertedUpdatedAt = '';
                var $tableElement = null;
                var $cellElement = null;
                var $rowElement = null;

                //$tableElement representing table tag
                $tableElement = $('#subCategoryTable');

                //-------- Begin creating <tr> and <td> HTML element ------
                //loop through each CategoryList that was returned from getAllCategory Method
                for (index = 0; index < subCategoryList.length; index++) {

                    subCategoryID = subCategoryList[index].SubCategoryID;
                    categoryName = subCategoryList[index].CategoryName;
                    subCategoryName = subCategoryList[index].SubCategoryName;
                    createdAt = subCategoryList[index].CreatedAt;
                    updatedAt = subCategoryList[index].UpdatedAt;
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

                    $cellElement = $('<td></td>', { text: subCategoryID });
                    $rowElement.append($cellElement);
                    //append cell in row element


                    $cellElement = $('<td></td>', { text: categoryName });
                    //Create the 2nd column cell element which display CategoryName
                    $rowElement.append($cellElement);

                    $cellElement = $('<td></td>', { text: subCategoryName });
                    //Create the 2nd column cell element which display CategoryName
                    $rowElement.append($cellElement);

                    $cellElement = $('<td></td>', { text: convertedCreatedAt });
                    //Create the 3rd column cell element which display Createdat
                    $rowElement.append($cellElement);

                    $cellElement = $('<td></td>', { text: convertedUpdatedAt });
                    //Create the 3rd column cell element which display Createdat
                    $rowElement.append($cellElement);


                    //$updatedSubCategoryLink = $('<a>Update SubCategory</a>');
                    $updatedSubCategoryLink = $("<input type='button' id='editButton' value='Edit SubCategory' />");
                    $updatedSubCategoryLink.addClass('btn btn-primary');
                    //$updatedCategoryLink.id('updateButton');
                    $updatedSubCategoryLink.attr({                        //for each button, set value attribute to store value of categoryID 
                        'data-toggle': 'modal',
                        'data-target': '#subCategoryModal',               //each hyperlink button share the same modal
                        'data-id': subCategoryID//can use data-something as long is 'data-'
                        //bind categoryID to button for each loop with data-* attribute
                    });


                    $cellElement = $('<td></td>');
                    $cellElement.append($updatedSubCategoryLink);
                    //append <a></a> link button to cell
                    $rowElement.append($cellElement);
                    //cellelement will be contained in rowElement <tr></tr>

                    $deleteSubCategoryLink = $("<img src='../images/bin.png'/>");
                    $deleteSubCategoryLink.addClass('btn-primary');
                    $deleteSubCategoryLink.attr({
                        'data-toggle': 'modal',
                        'data-target': '#deleteSubCategoryModal',
                        'data-id': subCategoryID
                    })

                    $cellElement = $('<td></td>');
                    $cellElement.append($deleteSubCategoryLink);
                    $rowElement.append($cellElement);

                    $tableElement.append($rowElement);
                }

                //----End of creating <tr> and <td> HTML element ------


            }//end of JavaScript anonymous function
              )//end of the done() method;
        }


        displayinfo();


        function categoryDropDownList() {
            $.ajax({
                type: 'POST',
                url: 'ADMViewSubCategory.aspx/getAllCategory',
                data: '{}',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
            }).done(function (data, textStatus, jqXHR) {
                var categoryList = data.d;
                //Reference: http://stackoverflow.com/questions/740195/adding-options-to-a-select-using-jquery-javascript
                $('#categoryListBox').append($('<option>', {
                    value: '',
                    text: '--Select Course--'
                }));

                //Reference: http://stackoverflow.com/questions/740195/adding-options-to-a-select-using-jquery-javascript
                $selectElement = $('#categoryListBox');
                var index = 0;
                for (index = 0; index < categoryList.length; index++) {
                    categoryId = categoryList[index].CategoryID;
                    categoryName = categoryList[index].CategoryName;
                    $optionElement = $('<option></option>', { value: categoryId, text: categoryName });
                    $selectElement.append($optionElement);
                }//end of for loop
            });
        }
    


    categoryDropDownList();

    function WebFormData(inSubCategoryId, inCategoryId, inSubCategoryName) {
        this.SubCategoryId = inSubCategoryId;
        this.CategoryId = inCategoryId;
        this.SubCategoryName = inSubCategoryName;
    }


    $('#subCategoryModal').on('show.bs.modal', function (event) {
        subCategoryNameText.value = '';
        categoryListBox.value = 0;
        var button = $(event.relatedTarget);
        if (button.attr('id') == 'editButton') {
            $('#SubCategoryModal').data('trackButton', 'editButton');
            var getSubCategoryID = button.data('id')
            $('#subCategoryModal').data('id', getSubCategoryID);

            $.ajax({
                type: 'POST',
                url: 'ADMViewSubCategory.aspx/getOneSubCategory',
                data: "{'subCategoryID' : '" + getSubCategoryID + "'}",  //passing data to server side in ajax call
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
            }).done(function (data, textStatus, jqXHR) {
                var subCategory = data.d;
                $('#categoryListBox').val(subCategory[0].CategoryID);
                subCategoryNameText.value = subCategory[0].SubCategoryName;
            })//end of ajax call
        }//end if
    })//end of on 'show.bs.modal'

    $('#editButton').on('click', function () {
        $.notify('sdsd');
        $('#subCategoryModal').data('trackButton', 'editButton');
    })

    //modify modal trackButton data attribute value as addButton
    $('#addSubCategory').on('click', function () {
        $('#subCategoryModal').data('trackButton', 'addSubCategoryButton');
    })

    $('#submitButton').on('click', function (event) {
        //$.notify('submit btn');
        
        
        var collectedCategoryId = $('#categoryListBox').val();
        var collectedSubCategoryName = $('#subCategoryNameText').val();
        if($('#subCategoryModel').data('trackButton')!='editButton')
        {
            var webFormData = new WebFormData("",collectedCategoryId, collectedSubCategoryName);
            var stringifiedWebformData = JSON.stringify(webFormData);
            ajax('addOneSubCategory', stringifiedWebformData);
            
            //to test update working or not.
            //var getSubCategoryIdFromModal = $('#subCategoryModal').data('id');
            //var webformData = new WebFormData(getSubCategoryIdFromModal, collectedCategoryId, collectedSubCategoryName);
            //var stringifiedWebFormData = JSON.stringify(webformData);
            //ajax('updateOneSubCategory', stringifiedWebFormData);
        }
        else 
        {
            $.notify('dsd');
            var getSubCategoryIdFromModal = $('#subCategoryModal').data('id');
            var webformData = new WebFormData(getSubCategoryIdFromModal, collectedCategoryId, collectedSubCategoryName);
            var stringifiedWebFormData = JSON.stringify(webformData);
            ajax('updateOneSubCategory', stringifiedWebFormData);
         
        }

        $('td').remove();
        displayinfo();

        //reload page not done!
    });

    $('#deleteSubCategoryModal').on('show.bs.modal', function (event) {

        var button = $(event.relatedTarget);
        var getSubCategoryId=button.data('id');
        $('#deleteSubCategoryLabel').text('Are you sure you want to  removeSubCategoryID:' + getSubCategoryId);
        $('#deleteSubCategoryModel').data('id', getSubCategoryId);
        $('#deleteButton').on('click', function (event) {
            $.ajax(
                  {
                      type: 'POST',
                      url: 'ADMViewSubCategory.aspx/deleteOneSubCategory',
                      data: "{'WebFormData' : '" + getSubCategoryId + "'}",
                      //If single quote is removed from webFormDataInJason, you are passing the data 
                      //as an object. The server side web method need to declare an object parameter.
                      //data: "{'WebFormData' : " + studentDataInJson + "}",
                      contentType: 'application/json; charset=utf-8',
                      dataType: 'json',
                      async: false
                  }).done(function (data, textStatus, jqXHR) {

                      //The data parameter captures the response from the server-side web method, AddOneCourse
                      responseObject = data.d;
                      if (responseObject.status == "fail") {
                          $.notify('Unable to delete a sub category record.', 'danger');
                      } else {
                          $.notify('You have delete a new sub category record.', 'success');
                      }

                      $('td').remove();
                      displayinfo();

                  }).fail(function (jqXHR, textStatus, errorThrown) {

                      console.log('The function attached to the ajax\'s fail() method has been executed.')
                      console.log('The jqXRH Status Error Code is : ' + jqXHR.status);
                      console.log(jqXHR.responseJSON.Message);
                      //How I got the following command is, by using the console.log or console.dir to
                      //inspect the jqXHR object, textStatus and errorThrown.
                      // Display the specific error raised by the server.
                      $.notify(jqXHR.responseJSON.Message, 'error');


                  });//end of $.ajax(...)
        });
    });
    function ajax(inMethodName, inStringifiedWebFormData) {
        $.ajax({
            type: 'POST',
            url: 'ADMViewSubCategory.aspx/' + inMethodName,
            data: "{'WebFormData' : '" + inStringifiedWebFormData + "'}",
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            async: false
        }).done(function (data, textStatus, jqXHR) {
            responseObject = data.d;
            if (responseObject.status == "success") {
                $.notify('SubCategory Record Submission Success!', 'success');
            } else {
                $.notify('SubCategory Record Submission Failed!', 'danger');
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
