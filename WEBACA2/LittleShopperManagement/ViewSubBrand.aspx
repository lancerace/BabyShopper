<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewSubBrand.aspx.cs" Inherits="WEBACA2.LittleShopperManagement.ViewSubBrand" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>View SubBrand</title>
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
            <h2>View SubBrand</h2>
        </div>

        <table id="subBrandTable" class="table table-condensed table-bordered table-hover table-responsive">
            <thead>
                <tr>
                    <th>SubBrand ID</th>
                    <th>SubBrand Name</th>
                    <th>Description</th>
                    <th>Date Created</th>
                    <th>Date Updated</th>
                    <th>Delete</th>
                </tr>
            </thead>
            <tbody></tbody>

        </table>

        <input type="button" id='addButton' value="Add" class="btn btn-primary" data-toggle="modal" data-target="#subBrandModal" />
        <%--data-toggle='Modal' create a modal window--%>
        <%--data-target point to id of modal--%>



        <%--data-trackbutton to store which button review this popup--%>
        <%--data-id to store subBrandid. Retrieve editButton ['data-id': subBrandID] value of the particular row clicked--%>
        <div id="subBrandModal" class="modal" data-trackbutton="" data-id="">
            <div class="modal-dialog">

                <%--Modal Content--%>
                <div class="modal-content">

                    <%--header--%>
                    <div class="modal-header modal-header-primary">
                        Manage SubBrand
                    </div>

                    <div class="panel-body">
                        <%--with some padding around its content--%>

                        <div class="form-horizontal">
                            <div class="modal-body">

                                <div class="form-group">
                                    <label class="control-label">Brand Name:</label>
                                    <u id="brandNameText">yo</u>
                                </div>

                                <div class="form-group">
                                    <label class="control-label">SubBrand Name:</label>
                                    <input type="text" id="subBrandNameText" class="form-control" />
                                </div>

                                <div class="form-group">
                                    <label class="control-label">Description:</label>
                                    <%--           <input type="text" id="descriptionText" class="form-control" />--%>
                                    <textarea id="descriptionText" rows="5" class="form-control" style="resize:none"></textarea>
                                </div>

                                <div class="form-group">
                                    <label class="control-label">VideoLink:</label>
                                    <input type="text" id="videoLinkText" class="form-control" />
                                </div>


                                <div class="form-group">
                                    <%--bootstrap-fileinput jQuery plugin correspond to --%>
                                    <%--ajax upload of multiple images ( name = "photoInput[]" )--%>
                                    <%--http://krajeesolutions.tumblr.com/--%>
                                    <input id="photoInput" name="photoInput[]" type="file" class="file input-group-lg" multiple="true" /><br />
                                </div>
                            </div>

                        </div>
                        <div class="modal-footer">
                            <input type="button" value="Submit" id="submitButton" class="btn btn-default" data-dismiss="modal" />
                            <input type="button" value="Close" class="btn btn-default" data-dismiss="modal" />
                            <%--data-dismiss attribute close modal popup--%>
                        </div>
                    </div>
                </div>
                <%--end of panel-body--%>
            </div>
            <%-- end of modalContent--%>
        </div>
        <%--end of subBrandModal--%>
    </div>
     <div id="deleteSubBrandModal" class="modal" data-id="">
            <div class="modal-dialog">

                <%--Modal Content--%>
                <div class="modal-content">

                    <%--header--%>
                    <div class="modal-header modal-header-primary">
                        Remove Brand
                    </div>
                    <div class="form-group">
                        <div class="modal-body">
                            <div id="deleteSubBrandLabel"></div>

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

        window.$brandID = GetQueryString()['BrandID'];
      
        //window denote variable as global scope. 
        window.$photoInputElement = $('#photoInput');
        window.newSubBrandID = '';




        displayPageInfo();




        //default template found at http://plugins.krajee.com/file-input#options
        var overloadActionsTemplate = '<div class="file-actions">\n' +
        //'<div align="center">Primary</div>' +
        //'<div align="center"><input type="radio" id="primaryPhoto"></div>' +
        '<div class="file-footer-buttons">\n' +
        '    {delete}' +
        '    </div>\n' +
        '    <div class="file-upload-indicator" tabindex="-1" title="{indicatorTitle}">{indicator}</div>\n' +
        '    <div class="clearfix"></div>\n' +
        '</div>';


        //initalize fileinput plugin with various options
        //showUpload:false disable upload Button, showCaption:false prevent filename from displaying
        //layoutTemplates: configuration for rendering layout of bootstrap fileinput
        //actions: template of the thumbnail of file action buttons within footer 
        //remove upload thumbnail
        //browseClass = CSS class for browse button. Defaults = btn btn-primary
        window.$photoInputElement.fileinput({
            uploadUrl: 'addSubBrandPhoto_Handler.ashx',
            uploadAsync: false,
            //only allow image file
            allowedFileTypes: ['image'],
            showUpload: false,
            showCaption: false,
            browseClass: 'btn btn-primary pull-right',
            previewFileIcon: '<i class="glyphicon glyphicon-king"></i>',
            maxFileCount: 3,
            overwriteInitial: false,
            type: 'post',
            layoutTemplates: {
                actions: overloadActionsTemplate
            },
            //post to server with extra data. 
            uploadExtraData: function () {
                var out = {};
                out['subBrandID'] = window.newSubBrandID;
                return out;
            },
           
        });



        $('#deleteSubBrandModal').on('show.bs.modal', function (event) {
            var button = $(event.relatedTarget);
            var getSubBrandId = button.data('id');
            $('#deleteSubBrandLabel').text('Are you sure you want to  remove SubBrandID:' + getSubBrandId);
           // $('#deleteSubBrandModal').data('id', getSubBrandId);
            $('#deleteButton').on('click', function (event) {
                $.ajax(
                      {
                          type: 'POST',
                          url: 'ViewSubBrand.aspx/DeleteOneSubBrand',
                          data: "{'inSubBrandId' : '" + getSubBrandId + "'}",
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
                              $.notify('Unable to delete a SubBrand record.', 'danger');
                          } else {
                              $.notify('You have delete a new SubBrand record.', 'success');
                          }

                          $('td').remove();
                          displayPageInfo();

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







        //show.bs.modal & data-* attribute technique is use to vary content of a single modal shared by multiple button
        //show.bs.modal event is fire whenever modal is visible to the user ,similar to page_load
        $('#subBrandModal').on('show.bs.modal', function (event) {
            //clean popup content
            subBrandNameText.value = '';
            descriptionText.value = '';
            videoLinkText.value = '';
            window.$photoInputElement.fileinput('clear');
            
            //using event.relatedTarget and HTML data-* attributes to vary content in modal
            var button = $(event.relatedTarget);        //get related button that trigger the modal

            //not needed anymore
            if (button.attr('id') == 'editButton') {//if button clicked contain id=editButton,execute ajax getOneSubBrand
                $('#subBrandModal').data('trackButton', 'editButton'); //use data attribute to track that modal is clicked by edit button              
                var getSubBrandID = button.data('id');
                //store categoryID from button derived from event.relatedTarget to modal. This is for ajax UpdateOneCategory usage
                $('#subBrandModal').data('id', getSubBrandID);

                $.ajax({
                    type: 'POST',
                    url: 'ViewSubBrand.aspx/GetOneSubBrandWVideoLinkByRecordId',
                    data: "{'subBrandID' : '" + getSubBrandID + "'}",  //passing data to server side in ajax call
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json',
                }).done(function (data, textStatus, jqXHR) {
                    //retrieve response object
                    var subBrand = data.d;
                    var subBrandName = subBrand.SubBrandName;
                    var description = subBrand.Description;
                    var subBrandVideoLink = subBrand.SubBrandVideoLink;

                    subBrandNameText.value = subBrandName;
                    descriptionText.value = description;
                    videoLinkText.value = subBrandVideoLink;
                })//end of ajax call
            }//end if

        })//end of on 'show.bs.modal'

        //create a class with javascript constructor function "WebFormData" for SubBrand Class
        function WebFormData(inSubBrandId,inBrandID, inSubBrandName, inDescription, inSubBrandVideoLink) {
            this.subBrandId = inSubBrandId;
            this.subBrandName = inSubBrandName;
            this.description = inDescription;
            this.subBrandVideoLink = inSubBrandVideoLink;
            this.getSubBrandID = inBrandID;
        }

        ////modify modal trackButton data attribute value as editButton
        //$('#editButton').on('click', function () {
        //    $('#subBrandModal').data('trackButton', 'editButton');
        //})

        //modify modal trackButton data attribute value as addButton
        $('#addButton').on('click', function () {
            $('#subBrandModal').data('trackButton', 'addButton');


        })


        $('#submitButton').on('click', function (event) {
            var collectedSubBrandName = $('#subBrandNameText').val();
            var collectedDescription = $('#descriptionText').val();
            var collectedVideoLinkText = $('#videoLinkText').val();
            //get SubBrand from user input
            if ($('#subBrandModal').data('trackButton') != "editButton") // execute addOneCategory if add button is clicked
            {
                //call upon javascript constructor
                var webformData = new WebFormData("",window.$brandID, collectedSubBrandName, collectedDescription, collectedVideoLinkText);
                //webformData constructor

                var stringifiedWebFormData = JSON.stringify(webformData);//serialize and send to server side
                //ajax('addOneSubBrand', stringifiedWebFormData);
                $.ajax({
                    type: 'POST',
                    url: 'ViewSubBrand.aspx/addOneSubBrand',
                    data: "{'WebFormDataParameter' : '" + stringifiedWebFormData + "'}",
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json',
                    async: false,
                }).done(function (data, textStatus, jqXHR) { //An alternative to the deprecated .success callback option
                    responseObject = data.d;
                    if (responseObject.uniqueConstraint == 'False') {
                        //store newsubbrandID to global variable for adding photo   
                        window.newSubBrandID = responseObject.subBrandID;
                        $.notify('SubBrand Record Submission Success!', 'success');
                        //adding subbrand image
                        window.$photoInputElement.fileinput('uploadBatch');
                        //if upload image succeeded,display message from ashx handler that sent the json response

                        window.$photoInputElement.on('filebatchuploadsuccess', function (event, data) {
                            var response = data.response;
                            $.notify(response.message, 'success');
                        });
                    }//end if
                    else {
                        $.notify('SubBrand already existed!, please use a different SubBrand Name', 'danger');
                    }
                })//end done         
        .fail(function (jqXHR, textStatus, errorThrown) {//An alternative to the deprecated .error callback option
            console.log('The function attached to the ajax\'s fail() method has been executed.')
            console.log('The jqXRH Status Error Code is : ' + jqXHR.status);
            console.log(jqXHR.responseJSON.Message);
            console.log(responseObject.status);
            $.notify(jqXHR.responseJSON.Message, 'Error! Please contact the administrator if problem still exist.');
        })//end fail    

            }//end if
            else {
                var collectedSubBrandId = $('#subBrandModal').data('id');
                //call upon javascript constructor
                var webformData = new WebFormData(collectedSubBrandId, collectedSubBrandName, collectedDescription, collectedVideoLinkText);

                var stringifiedWebFormData = JSON.stringify(webformData);//serialize and send to server side

                //function ajax(methodName, inStringifiedWebFormData)
                ajax('UpdateOneSubBrandByRecordId', stringifiedWebFormData);
            }//end if

            //remove tabledata of #subBrandTable 
            $('td').remove();

            //display info again
            displayPageInfo();


        })//end click function


        //window.$brandID
        //loadData,table creation
        function displayPageInfo() {
            $.ajax({
                type: 'POST',
                //getallCategory ,returned by 'object=reponse'
                data: "{'BrandID' : '" + window.$brandID + "'}",
                url: 'ViewSubBrand.aspx/GetAllSubBrandbyBrandId',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                async: false
            }).done(function (data, textStatus, jqXHR) { //upon success, create table and display information
                //Variable Declaration
                var subBrandList = data.d;
                var subBrandID = '';
                var subBrandName = '';
                var description = '';
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
                $tableElement = $('#subBrandTable');

                //loop through each category in CategoryList that was returned from (data.d)getAllCategory Method
                for (index = 0; index < subBrandList.length; index++) {

                    subBrandID = subBrandList[index].SubBrandID;
                    subBrandName = subBrandList[index].SubBrandName;
                    description = subBrandList[index].Description;
                    createdAt = subBrandList[index].CreatedAt;
                    updatedAt = subBrandList[index].UpdatedAt;
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

                    $cellElement = $('<td></td>', { text: subBrandID });
                    $rowElement.append($cellElement);
                    //append cell in row element


                    $cellElement = $('<td></td>', { text: subBrandName });
                    //Create the 2nd column cell element which display subBrandName
                    $rowElement.append($cellElement);

                    $cellElement = $('<td></td>', { text: description });
                    //Create the 3rd column cell element which display Description
                    $rowElement.append($cellElement);


                    $cellElement = $('<td></td>', { text: convertedCreatedAt });
                    //Create the 4th column cell element which display Createdat
                    $rowElement.append($cellElement);

                    $cellElement = $('<td></td>', { text: convertedUpdatedAt });
                    //Create the 5th column cell element which display UpdatedAt
                    $rowElement.append($cellElement);


                    $updatedSubBrandLink = $('<a>Edit SubBrand</a>').attr('href', 'UpdateSubBrand.aspx?subBrandID=' + subBrandID);

          
                    //$updatedSubBrandLink = $('<input type="button" id="editButton" value="Edit SubBrand" />');
                    $updatedSubBrandLink.addClass('btn btn-primary');

                    //multiple attribute from .attr() 
                    //$updatedSubBrandLink.attr({                        //for each button, set value attribute to store value of subBrandID
                    //    'data-toggle': 'modal',
                    //    'data-target': '#subBrandModal',               //each button share the same modal
                    //    'data-id': subBrandID
                    //});


                    $cellElement = $('<td></td>');
                    $cellElement.append($updatedSubBrandLink);
                    //append <a></a> link button to cell
                    $rowElement.append($cellElement);
                    //cellelement will be contained in rowElement <tr></tr>


                    $deleteSubBrandLink = $("<img src='../images/bin.png'/>");
                    $deleteSubBrandLink.addClass('btn-primary');
                    $deleteSubBrandLink.attr({
                        'data-toggle': 'modal',
                        'data-target': '#deleteSubBrandModal',
                        'data-id': subBrandID
                    })

                    $cellElement = $('<td></td>');
                    $cellElement.append($deleteSubBrandLink);
                    $rowElement.append($cellElement);
                    $tableElement.append($rowElement);

                }//end of for loop

            })//end of the done() method;
        }


        //PostCondition
        //inMethodName = url: 'ViewSubBrand.aspx/' + inMethodName,  
        //inStringifiedWebFormData = data: "{'WebFormDataParameter' : '" + inStringifiedWebFormData + "'}",
        function ajax(inMethodName, inStringifiedWebFormData) {
            $.ajax({
                type: 'POST',
                url: 'ViewSubBrand.aspx/' + inMethodName,
                data: "{'WebFormDataParameter' : '" + inStringifiedWebFormData + "'}",
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                async: false,
                //An alternative to the deprecated .success callback option
            }).done(function (data, textStatus, jqXHR) {
                responseObject = data.d;
                if (responseObject.uniqueConstraint == 'False') {
                    $.notify('SubBrand Record Submission Success!', 'success');
                    //store newsubbrandID to global variable for adding photo
                    window.newSubBrandID = responseObject.subBrandID;
                } else {
                    $.notify('SubBrand already existed!, please use a different SubBrand Name', 'danger');
                }
            })//end done
                //An alternative to the deprecated .error callback option
             .fail(function (jqXHR, textStatus, errorThrown) {
                 console.log('The function attached to the ajax\'s fail() method has been executed.')
                 console.log('The jqXRH Status Error Code is : ' + jqXHR.status);
                 console.log(jqXHR.responseJSON.Message);
                 console.log(responseObject.status);
                 $.notify(jqXHR.responseJSON.Message, 'Error! Please contact the administrator if problem still exist.');
             })//end fail     
        }//end ajax




    </script>
</body>
</html>
