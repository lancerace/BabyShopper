<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ADMGetAllAgeGroup.aspx.cs" Inherits="WEBACA2.LittleShopperManagement.ADMAddAgeGroup" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Content/bootstrap.min.css" rel="stylesheet" />
    <link href="../Content/bootstrap-fileinput/css/fileinput.min.css" rel="stylesheet" />
    <script src="../Scripts/jquery-2.1.4.min.js"></script>
    <script src="../Scripts/fileinput.min.js"></script>
    <script src="../Scripts/bootstrap.min.js"></script>


</head>
<body>
   <table id="ageGroupTable" class="table table-condensed table-hover">
    <thead>
      <tr>
        <th>Age Group ID</th>
        <th>Age Group Name</th>
         <th>Minimum Age</th>
         <th>Maximum Age</th>
         <th>Created At</th>
         <th>Updated At</th>
         <th>Deleted At</th>
      </tr>
    </thead>
    <tbody>
    </tbody>
  </table>
    <div id="AgeGroupContainer"></div>
    <script>
        $.ajax({
            type: 'POST',
            url: 'ADMGetAllAgeGroup.aspx/getAllAgeGroup',
            data: "",
            contentType: 'application/json; charset=utf-8',
            dataType: 'json'
        }).done(function (data, textStatus, jqXHR) {
            console.log(data);
            console.log(data.d);
            var ageGroupList = data.d;
            var AgeGroupID = '';
            var AgeGroupName = '';
            var MinimumAge = '';
            var MaximumAge = '';
            var CreatedAt = '';
            var UpdatedAt = '';
            var DeletedAt = '';
            var $tableElement = null;
            var $cellElement = null;
            var $rowElement = null;
            //incomplete
            $tableElement=$('#ageGroupTable');
            for (i = 0; i < ageGroupList.length; i++) {
                AgeGroupID = ageGroupList[i].AgeGroupID;
                AgeGroupName = ageGroupList[i].AgeGroupName;
                MaximumAge = ageGroupList[i].MaximumAge;
                MinimumAge = ageGroupList[i].MinimumAge;
                CreatedAt = ageGroupList[i].CreatedAt;
                var dateString = CreatedAt.substr(6);
                var currentTime = new Date(parseInt(dateString));
                var month = currentTime.getMonth() + 1;
                var day = currentTime.getDate();
                var year = currentTime.getFullYear();
                var CreatedAt = day + "/" + month + "/" + year;
                UpdatedAt = ageGroupList[i].UpdatedAt;
                var dateString = UpdatedAt.substr(6);
                var currentTime = new Date(parseInt(dateString));
                var month = currentTime.getMonth() + 1;
                var day = currentTime.getDate();
                var year = currentTime.getFullYear();
                var UpdatedAt = day + "/" + month + "/" + year;
                DeletedAt = ageGroupList[i].DeletedAt;
                var dateString = DeletedAt.substr(6);
                var currentTime = new Date(parseInt(dateString));
                var month = currentTime.getMonth() + 1;
                var day = currentTime.getDate();
                var year = currentTime.getFullYear();
                var DeletedAt = day + "/" + month + "/" + year;
                $rowElement = $('<tr></tr>');
                //Create the 1st cell element which display AgeGroupID.
                $cellElement = $('<td></td>', { text: AgeGroupID });
                $rowElement.append($cellElement);
                //Create the 2nd cell element which display AgeGroupName
                $cellElement = $('<td></td>', { text: AgeGroupName });
                $rowElement.append($cellElement);
                $cellElement = $('<td></td>', { text: MinimumAge });
                $rowElement.append($cellElement);
                $cellElement = $('<td></td>', { text: MaximumAge });
                $rowElement.append($cellElement);
                $cellElement = $('<td></td>', { text: CreatedAt });
                $rowElement.append($cellElement);
                $cellElement = $('<td></td>', { text: DeletedAt });
                $rowElement.append($cellElement);
                $cellElement = $('<td></td>', { text: UpdatedAt });
                $rowElement.append($cellElement);
                //Insert the $rowElement into the table element represented by $tableElement.
                $tableElement.append($rowElement);
            }

        });
        
    </script>
  </body>
</html>
