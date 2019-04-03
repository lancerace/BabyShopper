<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ADMAddOneAgeGroup.aspx.cs" Inherits="WEBACA2.LittleShopperManagement.ADMAddOneAgeGroup" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title>Add Student with AJAX Technique</title>
    <link href="../Content/bootstrap.min.css" rel="stylesheet" />
    <link href="../Content/bootstrap-fileinput/css/fileinput.min.css" rel="stylesheet" />
      <script src="../Scripts/jquery-2.1.4.min.js"></script>
    <script src="../Scripts/fileinput.min.js"></script>
    <script src="../Scripts/bootstrap.min.js"></script>
    <style>
        body{margin-top:65px;}
    </style>
</head>
<body>
    <div class="container">
        <div class="row">
            <div class="col-md-offset-2 col-md-8">
                <div class="panel panel-info">
                    <div class="panel-heading"><h3>Add Age Group</h3></div>
                    <div class="panel-body">
                        <div class="alert alert-info" id="messageBox">Enter Age Group details.</div>
                        <form class="form-horizontal" role="form">
                            <div class="form-group">
                                <label for="ageGroupNameInput" class="col-md-2 control-label">Age Group Name</label>
                                <div class="col-md-10">
                                    <input type="text" class="form-control" name="ageGroupNameInput"
                                        id="ageGroupNameInput" placeholder="Age Group Name" />
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="maximumAgeInput" class="col-md-2 control-label">Maximum Age</label>
                                <div class="col-md-10">
                                    <input type="text" class="form-control" name="maximumAgeInput"
                                        id="maximumAgeInput" placeholder="MaximumAge Id" />
                                </div>
                            </div>
                           <div class="form-group">
                                <label for="minimumAgeInput" class="col-md-2 control-label">Minimum Age</label>
                                <div class="col-md-10">
                                    <input type="text" class="form-control" name="minimumAgeInput"
                                        id="minimumAgeInput" placeholder="MinimumAge Id" />
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-offset-2 col-md-10">
                                    <button type="button" id="saveButton" class="btn btn-default">Save</button>
                                </div>
                            </div>
                        </form>

                    </div>
                </div>

            </div>
        </div>
    </div>
    <script>
        var newStudentId = '';
        var $messageBoxElement = $('#messageBox');
        
        function WebFormData(inAgeGroupName,inMaximumAge,inMinimumAge) {
            this.ageGroupName = inAgeGroupName;
            this.minimumAge = inMinimumAge;
            this.maximumAge = inMaximumAge;
        }

        function collectAgeGroupDataAndSave() {
            //Collect data from the respective form elements
            var ageGroupName = $('#ageGroupNameInput').val();
            var minimumAge = $('#minimumAgeInput').val();
            var maximumAge = $('#maximumAgeInput').val();
            //Initialize a Student class object, studentData
            var webFormData = new WebFormData(ageGroupName, minimumAge, maximumAge);
            var webFormDataInJson = JSON.stringify(webFormData);
            //console.log(jsonText);
            $.ajax(
                        {
                            type: 'POST',
                            url: 'ADMAddOneAgeGroup.aspx/AddOneAgeGroup',
                            data: "{'WebFormData' : '" + webFormDataInJson + "'}",
                            //If single quote is removed from webFormDataInJason, you are passing the data 
                            //as an object. The server side web method need to declare an object parameter.
                            //data: "{'WebFormData' : " + studentDataInJson + "}",
                            contentType: 'application/json; charset=utf-8',
                            dataType: 'json',
                            async: false,
                            success: function (response) {
                                if (responseObject.status == 'success') {
                                    $messageBoxElement.text(responseObject.message);            
                                } else {
                                    $messageBoxElement.text('Error : ' + responseObject.message);
                                }
                            },
                            error: function (xhr, status, errorThrown) {
                               
                                //I copied these error: complete: and success: from http://learn.jquery.com/ajax/jquery-ajax-methods/
                                //.These are called settings. Ajax() method has a lot of settings.
                                //Learn to use console.log
                                //console.log('Error: ' + errorThrown);
                                //console.log('Status: ' + status);
                                $messageBoxElement.text('Error Message : ' + errorThrown);

                            }
                        })
        }

            $('#saveButton').on('click', function (event) {
            collectAgeGroupDataAndSave();
            

        });//end of $('#saveButton').on(....

    </script>
</body>
</html>