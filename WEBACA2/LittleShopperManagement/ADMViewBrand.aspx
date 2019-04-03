<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ADMViewBrand.aspx.cs" Inherits="WEBACA2.LittleShopperManagement.ADMViewBrand" %> 

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>View Brand</title>
    <%--    <link href="../Content/MyStyle.css" rel="stylesheet" />--%>
    <script src="../Scripts/jquery-2.1.4.min.js"></script>
    <script src="../Scripts/jquery-ui-1.11.4.min.js"></script>
    <script src="../Scripts/bootstrap.min.js"></script>   
    <link href="../Content/bootstrap.min.css" rel="stylesheet" />
    <link href="../Content/bootstrap-theme.min.css" rel="stylesheet" />
    <link href="../Content/MyStyle.css" rel="stylesheet" />
    <script src="../Scripts/notify.min.js"></script>
    <link href="../Content/bootstrap-fileinput/css/fileinput.min.css" rel="stylesheet" />
    <script src="../Scripts/fileinput.min.js"></script>

</head>
<body>
   
    <div class="container">

        <div class="modal-header-primary">
            <h2>View Brand</h2>
        </div>

        <table id="brandTable" class="table table-condensed table-bordered table-hover table-responsive">
            <thead>
                <tr>
                    <th>Brand ID</th>
                    <th>Brand Name</th>
                    <th>Brand Description</th>
                    <th>Date Created</th>
                    <th>Date Updated</th>
                </tr>
            </thead>
            <tbody></tbody>

        </table>

        <input type="button" value="Add" class="btn btn-primary" data-toggle="modal" data-target="#brandModal" />


        <div id="brandModal" class="modal">
            <div class="modal-dialog">

                <%--Modal Content--%>
                <div class="modal-content">

                    <%--header--%>
                    <div class="modal-header modal-header-primary">
                        Manage Brand
                    </div>


                    <div class="form-group">
                        <div class="modal-body">
                            Brand Name:
                            <input type="text" id="brandNameText" class="form-control" />
                        </div>
                        <div class="modal-body">
                            Brand Description:
                            <input type="text" id="brandDescriptionText" class="form-control" />
                        </div>
                        <label for="inputProductImageFiles" class="col-md-2 control-label">Brand Photo</label>
                        <div class="col-md-10">
                            <label class="control-label">File</label>
                            <input id="brandPhotoInput" name="brandPhotoInput[]" type="file" multiple="true" class="file-loading" />

                            <%--<input id="brandPhotoInput" name="brandPhotoInput[]" class="file" type="file" multiple data-preview-file-type="any" data-preview-file-icon="" />--%>
                        </div>
                        <div class="modal-footer">
                            <input type="button" value="Submit" id="submitButton" class="btn btn-default" data-dismiss="modal" />
                            <input type="button" value="Close" id="closeButton" class="btn btn-default" data-dismiss="modal" />
                            <%--data-dismiss attribute to close modal popup--%>
                        </div>
                    </div>

                </div>
                <%-- end of modalContent--%>
            </div>
        </div>
        <%--end of categoryModal--%>

        <div id="deleteBrandModal" class="modal" data-id="">
            <div class="modal-dialog">

                <%--Modal Content--%>
                <div class="modal-content">

                    <%--header--%>
                    <div class="modal-header modal-header-primary">
                        Remove Brand
                    </div>
                    <div class="form-group">
                        <div class="modal-body">
                            <div id="deleteBrandLabel"></div>

                        </div>
                        <div class="modal-footer">
                            <input type="button" value="Yes" id="deleteButton" class="btn btn-default" data-dismiss="modal" />
                            <span id="deleteButtonToolTipIcon" data-toggle="tooltip" 
                          data-placement="top" title="" class="glyphicon glyphicon-question-sign" >&nbsp;</span> 
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
     
        collectedBrandId = '';
      
        var brandsize
        var brandImages;
        var InitialPreviewArray = new Array();
        var InitialPreviewConfigArray = new Array();

        var overloadActionsTemplate = '<div class="file-actions">\n' +
      //'<div align="center">Primary</div>' +
      //'<div align="center"><input type="radio" id="primaryPhoto"></div>' +
      '<div class="file-footer-buttons">\n' +
      '    {delete}' +
      '    </div>\n' +
      '    <div class="file-upload-indicator" tabindex="-1" title="{indicatorTitle}">{indicator}</div>\n' +
      '    <div class="clearfix"></div>\n' +
      '</div>';

        //----function to retrieve image on update --------
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
        

        //--end of fucntion
        jQuery.ajax({
            type: 'POST',
            //getallCategory ,returned by 'object=reponse'
            url: 'ADMViewBrand.aspx/getAllBrandExcludingDeletedData',
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            async: false
        }).done(function (data, textStatus, jqXHR) {

            var brandList = data.d;
            brandsize= brandList;
            var brandID = '';
            var brandName = '';
            var brandDescription = '';
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
            $tableElement = $('#brandTable');
            //-------- Begin creating <tr> and <td> HTML element ------
            //loop through each CategoryList that was returned from getAllCategory Method
            for (index = 0; index < brandList.length; index++) {

                brandID = brandList[index].BrandID
                brandName = brandList[index].BrandName;
                brandDescription = brandList[index].BrandDescription;
                createdAt = brandList[index].CreatedAt;
                updatedAt = brandList[index].UpdatedAt;

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

                $cellElement = $('<td></td>', { text: brandID });
                $rowElement.append($cellElement);
                //append cell in row element


                $cellElement = $('<td></td>', { text: brandName });
                //Create the 2nd column cell element which display CategoryName
                $rowElement.append($cellElement);

                $cellElement = $('<td></td>', { text: brandDescription });
                //Create the 2nd column cell element which display CategoryName
                $rowElement.append($cellElement);

                $cellElement = $('<td></td>', { text: convertedCreatedAt });
                //Create the 3rd column cell element which display Createdat
                $rowElement.append($cellElement);

                $cellElement = $('<td></td>', { text: convertedUpdatedAt });
                //Create the 3rd column cell element which display Createdat
                $rowElement.append($cellElement);

               // $updatedBrandLink = $('<input type="button" id="editButton" value="Edit Brand" />');
                $updatedBrandLink = $('<input type="button" id="editButton" value="Edit Brand" />');
      //.attr('href', 'ADMUpdateOneCourse_Version2.aspx?courseid=' + courseId);
                ////.attr('data-target', '#categoryModal');
                ////.attr('data-target', '#categoryModal');
                $updatedBrandLink.addClass('btn btn-primary');
                $updatedBrandLink.attr({                        //for each button, set value attribute to store value of categoryID 
                    'data-toggle': 'modal',
                    'data-target': '#brandModal',               //each hyperlink button share the same modal
                    'data-id': brandID//can use data-something as long is 'data-'
                    //bind categoryID to button for each loop with data-* attribute
                });
                //$updatedCategoryLink.id('updateButton');

                $cellElement = $('<td></td>');
                $cellElement.append($updatedBrandLink);
                //append <a></a> link button to cell
                $rowElement.append($cellElement);



                $manageSubBrandLink = $('<a>Manage SubBrand</a>').attr('href', 'ViewSubBrand.aspx?BrandID=' + brandID);
                $manageSubBrandLink.addClass('btn btn-primary');

                $cellElement = $('<td></td>');
                $cellElement.append($manageSubBrandLink);
                $rowElement.append($cellElement);






                //cellelement will be contained in rowElement <tr></tr>
                $deleteBrandLink = $("<img src='../images/bin.png'/>");
                $deleteBrandLink.addClass('btn-primary');
                $deleteBrandLink.attr({
                    'data-toggle': 'modal',
                    'data-target': '#deleteBrandModal',
                    'data-id': brandID
                })

                $cellElement = $('<td></td>');
                $cellElement.append($deleteBrandLink);
                $rowElement.append($cellElement);


                $tableElement.append($rowElement);
            }

            //----End of creating <tr> and <td> HTML element ------


        }//end of JavaScript anonymous function
      )//end of the done() method;

        var newBrandId = '';
        //$('#brandPhotoInput').fileinput({
        //    uploadUrl: 'AddBrand_PhotoHandler.ashx',//need get brand ashx
        //    uploadAsync: false,
        //    maxFileCount: 5,
        //    showUpload: false, /*disable the upload button*/
        //    overwriteInitial: false,
        //    uploadExtraData: function () {  // callback example
        //        var out = {};
        //        out['BrandId'] = newBrandId;//Must send this student id information too.
        //        return out;
        //    }

        //});
        //$('#brandPhotoInput').fileinput({
        //    uploadUrl: 'AddBrand_PhotoHandler.ashx',
        //    uploadAsync: false,
        //    maxFileCount: 1,
        //    type: 'post',
        //    overwriteInitial: false,
        //    showUpload: false,
        //    showCaption: false,
        //    browseClass: 'btn btn-primary btn-lg',
        //    fileType: 'any',
        //    previewFileIcon: '<i class="glyphicon glyphicon-king"></i>'
        //});
        $("#brandDescriptionText").replaceWith('<textarea id="brandDescriptionText" name="brandDescriptionText" style="width:100%;"></textarea>');

        function WebFormData(inBrandId,inBrandName, inBrandDescription) {
            this.BrandId = inBrandId;
            this.BrandName = inBrandName;
            this.BrandDescription = inBrandDescription;
        }

        function ajax(inMethodName, inStringifiedWebFormData) {
            $.ajax(
                        {
                            type: 'POST',
                            url: 'ADMViewBrand.aspx/'+ inMethodName,
                            data: "{'WebFormData' : '" + inStringifiedWebFormData + "'}",
                            //If single quote is removed from webFormDataInJason, you are passing the data 
                            //as an object. The server side web method need to declare an object parameter.
                            //data: "{'WebFormData' : " + studentDataInJson + "}",
                            contentType: 'application/json; charset=utf-8',
                            dataType: 'json',
                            async: false,
                            success: function (response) {
                                var responseObject = response.d;
                                newBrandId = responseObject.newBrandId;
                                console.log(responseObject.status);
                                if (responseObject.status == "success") {
                                    $.notify('Brand Record Submission Success!', 'success');
                                } else {
                                    $.notify('Brand Record Submission Failed!', 'danger');
                                }
                            },
                            error: function (xhr, status, errorThrown) {

                                $.notify(jqXHR.responseJSON.Message, 'Error! Please contact the administrator if problem still exist.');


                            }
                        }).done(function () {
                            //If there is any server error, this section will still execute.
                            //Therefore, need to have an if statement block to control whether the client logic
                            //can upload photo. I used the newStudentId variable to check.
                            if (newBrandId != '') {
                                //Reference: http://stackoverflow.com/questions/11448011/jquery-ajax-methods-async-option-deprecated-what-now
                                $('#brandPhotoInput').fileinput('uploadBatch');
                            }
                            //Placed this line of code here so that I can notice that this section was executed.
                            console.log('ajax().done() section was executed');
                        });;//end of $.ajax(...)
        }

        $('#submitButton').on('click', function (event) {
            var collectedBrandName = $('#brandNameText').val();
            var collectedBrandDescription = $('#brandDescriptionText').val();

            //get categoryname from user input

            if ($('#brandModal').data('trackButton') != "editButton") // execute addOneCategory if add button is clicked
            {
                //call upon javascript constructor
                var webformData = new WebFormData("", collectedBrandName,collectedBrandDescription);
                //webformData constructor

                var stringifiedWebFormData = JSON.stringify(webformData);//serialize and send to server side

                //function ajax(methodName, inStringifiedwebFormData)
                ajax('AddOneBrand', stringifiedWebFormData);

            }//end if
            else {

                var getBrandIdFromModal = $('#BrandModal').data('id');
                //call upon javascript constructor
                var webformData = new WebFormData(getBrandIdFromModal, collectedBrandName, collectedBrandDescription);

                var stringifiedWebFormData = JSON.stringify(webformData);//serialize and send to server side

                //function ajax(methodName, inStringifiedWebFormData)
                ajax('updateOneBrand', stringifiedWebFormData);
               

                


            }//end if

        });//end of $('#saveButton').on(....
        $('#brandModal').on('show.bs.modal', function (event) {
            brandNameText.value = '';
            brandDescriptionText = '';
            //using event.relatedTarget and HTML data-* attributes to vary content in modal
            var button = $(event.relatedTarget);        //get related button that trigger the modal

            if (button.attr('id') == 'editButton') {//if button clicked contain id=editButton,execute ajax getOneCategory
                $('#brandModal').data('trackButton', 'editButton'); //use data attribute to track that modal is clicked by edit button              
                var getBrandID = button.data('id');
                //store categoryID from button derived from event.relatedTarget to modal. This is for ajax UpdateOneCategory usage
                $('#brandModal').data('id', getBrandID);

                $.ajax({
                    type: 'POST',
                    url: 'ADMViewBrand.aspx/getOneBrand',
                    data: "{'brandID' : '" + getBrandID + "'}",  //passing data to server side in ajax call
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json',
                }).done(function (data, textStatus, jqXHR) {
                    var brand = data.d;
                    var brandName = brand.BrandName;
                    var brandDescription = brand.BrandDescription;
                    brandNameText.value = brandName;
                    //need check later
                    $('#brandDescriptionText').val(brandDescription);
                    //brandDescriptionText.value = brandDescription;
                })//end of ajax call
            
            //display image

            //$fileProductPhotosElement = $("#brandPhotoInput");
            //loop through the image for display
            $.ajax({
                type: 'POST',
                async: false,
                url: 'ADMViewBrand.aspx/GetBrandImages',
                data: "{'inBrandId' : '" + getBrandID + "'}",  //passing data to server side
                contentType: 'application/json; charset=utf-8',
                dataType: 'json'

            }).done(function (data, textStatus, jqXHR) {
                brandImages = data.d;

            });//end of ajax call
            for (index = 0; index < brandImages.PhotoList.length; index++) {
                //generic handler will process all image in term of subbrandimageid to img src for preview
                var tempInitialPreview = '<img src="GetOneBrandPhoto.ashx?id=' + brandImages.PhotoList[index].BrandImageId + '" class="file-preview-image" alt="" title="">';
                InitialPreviewArray.push(tempInitialPreview);

                var tempInitialPreviewConfig = {
                    caption: brandImages.PhotoList[index].ImageFileName,
                    width: '',
                    url: 'http://localhost:19689/admin/DeleteBrandImage.ashx',
                    key: brandImages.PhotoList[index].BrandImageId
                    //extra: { id: image.SubBrandImageID }
                }
                InitialPreviewConfigArray.push(tempInitialPreviewConfig);
            }//end for 
                //end of loop thought
                   

            $('#brandPhotoInput').fileinput({
                  
                    uploadUrl: 'UpdateOneBrandImage.ashx',
                    deleteUrl: 'DeleteBrandImage.ashx',
                    uploadAsync: false,
                    overwriteInitial: true,
                    initialPreviewCount: 0,
                    ajaxDeleteSettings: { method: 'POST' },
                    //only allow image file
                    allowedFileTypes: ['image'],
                    showUpload: false,
                    showCaption: false,
                    showUploadedThumbs: true,
                    browseClass: 'btn btn-primary pull-right',
                    previewFileIcon: '<i class="glyphicon glyphicon-king"></i>',
                    maxFileCount: 5,
                    layoutTemplates: {

                        actions: actionsTemplate
                    },
                    //initialPreview: '<img src="GetSubBrandPhoto_Handler.ashx?id=' + 4 + '" class="file-preview-image" alt="" title="">',
                    initialPreview: InitialPreviewArray,
                    ////initialpreviewConfig has to be set in order for thumbnail 'delete' to show on picture
                    initialPreviewConfig: InitialPreviewConfigArray,
                    
                    
                 
            });
            }//end if

        })//end of on 'show.bs.modal'
        

        var actionsTemplate = '<div class="file-actions">\n' +
        //'<div align="center">Primary</div>' +
        //'<div align="center"><input type="radio" id="primaryPhoto"></div>' +
        '<div class="file-footer-buttons">\n' +
        '    {delete}' +
        '    </div>\n' +
        '    <div class="file-upload-indicator" tabindex="-1" title="{indicatorTitle}">{indicator}</div>\n' +
        '    <div class="clearfix"></div>\n' +
        '</div>';
      
            
        
            
        $('#deleteBrandModal').on('show.bs.modal', function (event) {

            var button = $(event.relatedTarget);
            var getBrandId = button.data('id');
            $('#deleteBrandLabel').text('Are you sure you want to  remove BrandID:' + getBrandId);
            $('#deleteBrandModal').data('id', getBrandId);
            $('#deleteButton').prop('disabled', false);// to reset the delete button!
            $.ajax({
                type: 'POST',
                url: 'ADMViewBrand.aspx/CountSubBrand',
                data: "{'inBrandId' : '" + getBrandId + "'}",
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
            }).done(function (data, textStatus, jqXHR) {
                var numberOfSubBrand = data.d;
                if (numberOfSubBrand > 0) {
                    $('#deleteButton').prop('disabled', true);
                    $('#deleteButtonToolTipIcon').show();//unhide the <span> element which displays the question mark symbol.
                    $('#deleteButtonToolTipIcon').tooltip({
                        title: 'Cannot delete this course because there are ' + numberOfSubBrand + ' subBrand records linked to it.'
                    });
                } else {
                    $('#deleteButtonToolTipIcon').hide();//hide the <span> element which displays the question mark symbol.


                }
            });
            $('#deleteButton').on('click', function (event) {
                $.ajax(
                      {
                          type: 'POST',
                          url: 'ADMViewBrand.aspx/deleteOneBrand',
                          data: "{'WebFormData' : '" + getBrandId + "'}",
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
                              $.notify('Unable to delete a brand record.', 'danger');
                          } else {
                              $.notify('You have delete a new brand record.', 'success');
                          }
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

    </script>
</body>
</html>