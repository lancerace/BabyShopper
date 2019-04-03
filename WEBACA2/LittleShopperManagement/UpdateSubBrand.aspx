<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UpdateSubBrand.aspx.cs" Inherits="WEBACA2.LittleShopperManagement.updateSubBrand" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Update SubBrand</title>
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
        <div class="row">
            <div class="col-md-offset-1 col-md-8">
                <div class="panel panel-info">
                    <div class="modal-header-primary">
                        <h3>Update SubBrand</h3>
                    </div>
                    <div class="panel-body">

                        <form class="form-horizontal" role="form">

                            <div class="form-group">
                                <label class="col-md-2 control-label">BrandName:</label>
                                <div class="col-md-10">
                                    <u>MyBrand</u>
                                </div>
                            </div>


                            <div class="form-group">
                                <label class="col-md-2 control-label">SubBrand Name:</label>
                                <div class="col-md-10">
                                    <%--form-control make width to 100%--%>
                                    <input type="text" class="form-control" name="fullNameInput"
                                        id="subBrandNameText" placeholder="SubBrand Name" />
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="col-md-2 control-label">Description:</label>
                                <%--           <input type="text" id="descriptionText" class="form-control" />--%>
                                <div class="col-md-10">
                                    <textarea id="descriptionText" rows="10" class="form-control" style="resize: none"></textarea>
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="col-md-2 control-label">VideoLink:</label>
                                <div class="col-md-10">
                                    <input type="text" class="form-control" name="fullNameInput"
                                        id="videoLinkText" placeholder="http://www...." />
                                </div>
                            </div>

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
        //get QueryString of SubBrandID
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

        //Variable Field =======================================================
        //window denote variable as global variable.
        window.collectedsubBrandId = GetQueryString()['subBrandID'];
        //window.collectedsubBrandId = '4';
        $photoInputElement = $('#photoInput');

        //store images from getAllImagesBySubBrandID
        var subBrandImages;
        var InitialPreviewArray = new Array();
        var InitialPreviewConfigArray = new Array();

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
        //==========================================================================================================================================================================




        // Method Field===============================================================================================================================
       
        function GetAllImage() {
            $.ajax({
                type: 'POST',
                async: false,
                url: 'UpdateSubBrand.aspx/getAllImagesBySubBrandID',
                data: "{'inSubBrandID' : '" + window.collectedsubBrandId + "'}",  //passing data to server side
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',

            }).done(function (data, textStatus, jqXHR) {
                subBrandImages = data.d;

            });//end of ajax call
        }


        //for 
        function ForUpdateDetail(inUpdateArray) {
            inUpdateArray["SubBrandID"] = window.collectedsubBrandId;
            inUpdateArray["SubbrandName"] = $("#subBrandNameText").val();
            inUpdateArray["Description"] = $("#descriptionText").val();
            inUpdateArray["SubBrandVideoLink"] = $("#videoLinkText").val();

            return inUpdateArray;
        }


        function getOneSubBrand() {

            $.ajax({
                type: 'POST',
                url: 'UpdateSubBrand.aspx/GetOneSubBrandWVideoLinkByRecordId',
                data: "{'subBrandID' : '" + window.collectedsubBrandId + "'}",  //passing data to server side in ajax call
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
            }).done(function (data, textStatus, jqXHR) {
                //retrieve response object

                       var subBrand= data.d;
                       console.log(subBrand);
                       subBrandNameText.value = subBrand.SubBrandName;
                       descriptionText.value=subBrand.Description;
                       videoLinkText.value = subBrand.SubBrandVideoLink;
            })
        }


        //==========================================================================================================================================================================




        //process of step for this webform

        //1st: retrieve querystring of subbrandID
        // get all image
        //get the id and let handler process each images
        //put it in initialpreviewarray and configure file properties for each file thumbnail using initialpreviewconfig\
        //window.collectedsubBrandId = '4';
        //window.collectedsubBrandId = GetQueryString()['subBrandID'];
           
        //prepare form data
        getOneSubBrand();
        GetAllImage();

        //setup initial preview and initialpreviewconfig before initializing fileinput
        for (index = 0; index < subBrandImages.length; index++) {
            var image = subBrandImages[index];

            //generic handler will process all image in term of subbrandimageid to img src for preview
            var tempInitialPreview = '<img src="GetSubBrandPhoto_Handler.ashx?id=' + image.SubBrandImageID + '" class="file-preview-image" alt="" title="">';
            InitialPreviewArray.push(tempInitialPreview);

            var tempInitialPreviewConfig = {
                caption: image.SubBrandImageName,
                width: '',
                url: 'http://localhost:18348/LittleShopperManagement/DeleteSubBrandPhoto_Handler.ashx',
                key: image.SubBrandImageID,
                extra: { id: image.SubBrandImageID }
            }
            InitialPreviewConfigArray.push(tempInitialPreviewConfig);
        }//end for 

        //finally,initialize the fileinput 
        setupFileInput();
        //==========================================================================================================================================================================







        $('#saveButton').on('click', function () {
            $photoInputElement.fileinput("uploadBatch");

        });

        $('#input-id').on('filebatchuploadsuccess', function(event, data, previewId, index) {

            console.log(data.response);
        })





        //initalize fileinput plugin with various options
        //showUpload:false disable upload Button, showCaption:false prevent filename from displaying
        //layoutTemplates: configuration for rendering layout of bootstrap fileinput
        //actions: template of the thumbnail of file action buttons within footer 
        //remove upload thumbnail
        //browseClass = CSS class for browse button. Defaults = btn btn-primary
        function setupFileInput() {
            $photoInputElement.fileinput({
                uploadUrl: 'UpdateSubBrandPhoto_Handler.ashx',
                deleteUrl: 'DeleteSubBrandPhoto_Handler.ashx',
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
                //initialPreview: '<img src="GetSubBrandPhoto_Handler.ashx?id=' + 4 + '" class="file-preview-image" alt="" title="">',
                initialPreview: InitialPreviewArray,
                //////initialpreviewConfig has to be set in order for thumbnail 'delete' to show on picture
                initialPreviewConfig: InitialPreviewConfigArray,


                //post to server with extra data. 
                //extra data for ashx to update subbrand detail
                uploadExtraData: function () {

                    var subBrandID = window.collectedsubBrandId;
                    var out = {}, subBrandID;


                    out = ForUpdateDetail(out);

                    console.log(out);
                    return out;
                },
            });
        }//end of setupFileInput



        //if dustbin thumbnail clicked,execute
        $photoInputElement.on('filedeleted', function (event, key, data) {

            //in json format,need to destringify
            var responsetxt = data.responseText;
            var parsedResponseTxt = JSON.parse(responsetxt);


            $.notify(parsedResponseTxt.status + ' ' + parsedResponseTxt.message);
        })






    </script>
</body>
</html>
