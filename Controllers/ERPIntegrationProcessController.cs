@model OrderModel

@{
    ViewBag.Title = "View ERP Integration Process";
}
<div class="section-header">
    <div class="title">
        <img src="@Url.Content("~/Administration/Content/images/ico-sales.png")" alt="" />
        View ERP Integration Process Details @Html.ActionLink("(back to ERP integration process list)", "List")
    </div>
</div>
<table class="adminContent">
    <tr>
        <td class="adminTitle">
            @Html.NopLabelFor(model => model.Id):
        </td>
        <td class="adminData">
            @Model.Id
        </td>
    </tr>
    <tr>
        <td class="adminTitle">
            @Html.NopLabelFor(model => model.OrderStatus):
        </td>
        <td class="adminData">
            <strong>
                @Model.OrderStatus
            </strong>
        </td>
    </tr>
    <tr>
        <td class="adminTitle">
            @Html.NopLabelFor(model => model.PaymentStatus):
        </td>
        <td class="adminData">
            <strong>
                @Model.PaymentStatus
            </strong>
        </td>
    </tr>
    <tr>
        <td class="adminTitle">
            @Html.NopLabelFor(model => model.ShippingStatus):
        </td>
        <td class="adminData">
            <strong>
                @Model.ShippingStatus
            </strong>
        </td>
    </tr>
    <tr>
        <td class="adminTitle">
            @Html.NopLabelFor(model => model.OrderInternalStatus):
        </td>
        <td class="adminData">
            <strong>
                @Model.OrderInternalStatus
            </strong>
        </td>
    </tr>
    <tr>
        <td class="adminTitle">
            @Html.NopLabelFor(model => model.CustomerInfo):
        </td>
        <td class="adminData">
            @Model.CustomerInfo
        </td>
    </tr>
    <tr>
        <td class="adminTitle">
            @Html.NopLabelFor(model => model.CreatedOn):
        </td>
        <td class="adminData">
            @Html.DisplayFor(model => model.CreatedOn)
        </td>
    </tr>
</table>
<br />
<div id="orders-grid"></div>
<div id="detailsQueueRequestData"></div>
<div id="detailsQueueResponseData"></div>
<div id="detailsGetStatusRequestData"></div>
<div id="detailsGetStatusResponseData"></div>
<div id="detailsGetResponseRequestData"></div>
<div id="detailsGetResponseResponseData"></div>

<script type="text/x-kendo-template" id="templateQueueRequestData">
    <div id="details-containerQueueRequestData">
        #= QueueLastRequestData #
    </div>
</script>

<script type="text/x-kendo-template" id="templateQueueResponseData">
    <div id="details-containerQueueResponseData">
        #= QueueLastResponseData #
    </div>
</script>

<script type="text/x-kendo-template" id="templateGetStatusRequestData">
    <div id="details-containerGetStatusRequestData">
        #= GetStatusLastRequestData #
    </div>
</script>

<script type="text/x-kendo-template" id="templateGetStatusResponseData">
    <div id="details-containerGetStatusResponseData">
        #= GetStatusLastResponseData #
    </div>
</script>

<script type="text/x-kendo-template" id="templateGetResponseRequestData">
    <div id="details-containerGetResponseRequestData">
        #= GetResponseLastRequestData #
    </div>
</script>

<script type="text/x-kendo-template" id="templateGetResponseResponseData">
    <div id="details-containerGetResponseResponseData">
        #= GetResponseLastResponseData #
    </div>
</script>

<style type="text/css">
    #details-containerQueueRequestData, #details-containerQueueResponseData,
    #details-containerGetStatusRequestData, #details-containerGetStatusResponseData,
    #details-containerGetResponseRequestData, #details-containerGetResponseResponseData {
        padding: 10px;
    }

        #details-containerQueueRequestData, #details-containerQueueResponseData,
        #details-containerGetStatusRequestData, #details-containerGetStatusResponseData,
        #details-containerGetResponseRequestData, #details-containerGetResponseResponseData h2 {
            margin: 0;
        }

        #details-containerQueueRequestData, #details-containerQueueResponseData,
        #details-containerGetStatusRequestData, #details-containerGetStatusResponseData,
        #details-containerGetResponseRequestData, #details-containerGetResponseResponseData em {
            color: #8c8c8c;
        }

        #details-containerQueueRequestData, #details-containerQueueResponseData,
        #details-containerGetStatusRequestData, #details-containerGetStatusResponseData,
        #details-containerGetResponseRequestData, #details-containerGetResponseResponseData dt {
            margin: 0;
            display: inline;
        }
</style>

<script>
    var wndQueueRequestData, detailsTemplateQueueRequestData;
    var wndQueueResponseData, detailsTemplateQueueResponseData;
    var wndGetStatusRequestData, detailsTemplateGetStatusRequestData;
    var wndGetStatusResponseData, detailsTemplateGetStatusResponseData;
    var wndGetResponseRequestData, detailsTemplateGetResponseRequestData;
    var wndGetResponseResponseData, detailsTemplateGetResponseResponseData;

    $(document).ready(function () {
        $("#orders-grid").kendoGrid({
            dataSource: {
                type: "json",
                transport: {
                    read: {
                        url: "@Html.Raw(Url.Action("ViewAllProcess", "ERPIntegrationProcess", new { id = Model.Id }))",
                        type: "POST",
                        dataType: "json",
                    }
                },
                schema: {
                    data: "Data",
                    total: "Total",
                    errors: "Errors"
                },
                requestEnd: function (e) {
                    if (e.type == "read") {
                        var response = e.response;
                        if (response) {
                            //store extra data
                            //reportAggregates = e.response["ExtraData"];
                        }
                    }
                },
                error: function (e) {
                    display_kendoui_grid_error(e);
                    // Cancel the changes
                    this.cancelChanges();
                },
                pageSize: 100,
                serverPaging: false,
                serverFiltering: true,
                serverSorting: true
            },
            pageable: {
                refresh: true
            },
            editable: {
                confirmation: false,
                mode: "inline"
            },
            //details grid
            //detailInit: detailInit,
            scrollable: false,
            //dataBound: onDataBound,
            columns: [
                {
                    field: "Id",
                    title: "Id",
                    width: 30
                },
                {
                    field: "ERPIntegrationSteps",
                    title: "Steps",
                    template: "#: ERPIntegrationSteps #",
                    width: 100
                },
                {
                    field: "ERPIntegrationStatus",
                    title: "Status",
                    width: 50
                },
                {
                    field: "QueueLastRequestDateTime",
                    title: "Queue Request<br/> Date Time",
                    width: 50,
                    type: "date",
                    format: "{0:G}"
                },
                {
                    //field: "LastRequestData",
                    //title: "Last Request Data",
                    command: { name: "QueueRequestData", text: "View Details", click: showDetailsGetData },
                    title: "Queue<br/> Request Data", width: "50px"
                },
                {
                    field: "QueueLastResponseDateTime",
                    title: "Queue Response<br/> Date Time",
                    width: 50,
                    type: "date",
                    format: "{0:G}"
                },
                {
                    //field: "LastResponseData",
                    //title: "Last Response Data",
                    command: { name: "QueueResponseData", text: "View Details", click: showDetailsGetData },
                    title: "Queue<br/> Response Data", width: "50px"
                },
                {
                    field: "QueueStatus",
                    title: "Queue<br/>Status",
                    width: 40,
                    format: "{0:G}"
                },
                {
                    field: "GetStatusLastRequestDateTime",
                    title: "GetStatus Request<br/> Date Time",
                    width: 50,
                    type: "date",
                    format: "{0:G}"
                },
                {
                    //field: "LastRequestData",
                    //title: "Last Request Data",
                    command: { name: "GetStatusRequestData", text: "View Details", click: showDetailsGetData },
                    title: "GetStatus<br/> Request Data", width: "50px"
                },
                {
                    field: "GetStatusLastResponseDateTime",
                    title: "GetStatus Response<br/> Date Time",
                    width: 50,
                    type: "date",
                    format: "{0:G}"
                },
                {
                    //field: "LastResponseData",
                    //title: "Last Response Data",
                    command: { name: "GetStatusResponseData", text: "View Details", click: showDetailsGetData },
                    title: "GetStatus<br/> Response Data", width: "50px"
                },
                {
                    field: "GetStatusStatus",
                    title: "GetStatus<br/>Status",
                    width: 40,
                    format: "{0:G}"
                },
                {
                    field: "GetResponseLastRequestDateTime",
                    title: "GetResponse Request<br/> Date Time",
                    width: 50,
                    type: "date",
                    format: "{0:G}"
                },
                {
                    //field: "LastRequestData",
                    //title: "Last Request Data",
                    
                    command: [{ name: "GetResponseRequestData", text: "View Details", click: showDetailsGetData }],
                    title: "GetResponse<br/> Request Data", width: "50px"
                },
                {
                    field: "GetResponseLastResponseDateTime",
                    title: "GetResponse<br/>Response Date Time",
                    width: 50,
                    type: "date",
                    format: "{0:G}"
                },
                {
                    //field: "LastResponseData",
                    //title: "Last Response Data",
                    command: [{ name: "GetResponseResponseData", text: "View Details", click: showDetailsGetData }],
                    title: "GetResponse<br/> Response Data", width: "50px"
                },
                {
                    field: "GetResponseStatus",
                    title: "GetResponse<br/>Status",
                    width: 40,
                    format: "{0:G}"
                },
                {
                    field: "RetryCount",
                    title: "Retry Count",
                    width: 50
                },
                {
                    field: "Id",
                    title: "Action",
                    width: 150,
                    template: '# if((ERPIntegrationStatusId != 20) && @Model.OrderStatusId != 40) {# <a href="@Url.Content("~/Admin/ERPIntegrationProcess/Retry/")#=Id#" onclick="return confirm(\'@T("Admin.Common.AreYouSure")\');">Retry</a> #}  #  #if((ERPIntegrationStatusId != 20) && @Model.OrderStatusId != 40) { # <div style="height:10px;"><br></div> # }#   # if(ERPIntegrationStatusId != 20 && @Model.OrderStatusId != 40) {# <a href="@Url.Content("~/Admin/ERPIntegrationProcess/MarkAsSuccess/")#=Id#" onclick="return confirm(\'@T("Admin.Common.AreYouSure")\');">Mark As Success</a> #}  #'
                }
            ]
        });

        wndQueueRequestData = $("#detailsQueueRequestData").kendoWindow({
            title: "Queue Request Data",
            modal: true,
            visible: false,
            resizable: true,
            width: 600,
            height: 700,
        }).data("kendoWindow");

        detailsTemplateQueueRequestData = kendo.template($("#templateQueueRequestData").html());

        wndQueueResponseData = $("#detailsQueueResponseData").kendoWindow({
            title: "Queue Response Data",
            modal: true,
            visible: false,
            resizable: true,
            width: 600,
            height: 700,
        }).data("kendoWindow");

        detailsTemplateQueueResponseData = kendo.template($("#templateQueueResponseData").html());

        wndGetStatusRequestData = $("#detailsGetStatusRequestData").kendoWindow({
            title: "GetStatus Request Data",
            modal: true,
            visible: false,
            resizable: true,
            width: 600,
            height: 700,
        }).data("kendoWindow");

        detailsTemplateGetStatusRequestData = kendo.template($("#templateGetStatusRequestData").html());

        wndGetStatusResponseData = $("#detailsGetStatusResponseData").kendoWindow({
            title: "GetStatus Response Data",
            modal: true,
            visible: false,
            resizable: true,
            width: 600,
            height: 700,
        }).data("kendoWindow");

        detailsTemplateGetStatusResponseData = kendo.template($("#templateGetStatusResponseData").html());

        wndGetResponseRequestData = $("#detailsGetResponseRequestData").kendoWindow({
            title: "GetResponse Request Data",
            modal: true,
            visible: false,
            resizable: true,
            width: 600,
            height: 700,
        }).data("kendoWindow");

        detailsTemplateGetResponseRequestData = kendo.template($("#templateGetResponseRequestData").html());

        wndGetResponseResponseData = $("#detailsGetResponseResponseData").kendoWindow({
            title: "GetResponse Response Data",
            modal: true,
            visible: false,
            resizable: true,
            width: 600,
            height: 700,
        }).data("kendoWindow");

        detailsTemplateGetResponseResponseData = kendo.template($("#templateGetResponseResponseData").html());
    });

    function showDetailsQueueRequestData(e) {
        e.preventDefault();

        closeAllWindow();
        var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
        wndQueueRequestData.content(detailsTemplateQueueRequestData(dataItem));
        wndQueueRequestData.center().open();
    }

    function showDetailsQueueResponseData(e) {
        e.preventDefault();

        closeAllWindow();
        var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
        wndQueueResponseData.content(detailsTemplateQueueResponseData(dataItem));
        wndQueueResponseData.center().open();
    }

    function showDetailsGetStatusRequestData(e) {
        e.preventDefault();

        closeAllWindow();
        var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
        wndGetStatusRequestData.content(detailsTemplateGetStatusRequestData(dataItem));
        wndGetStatusRequestData.center().open();
    }

    function showDetailsGetStatusResponseData(e) {
        e.preventDefault();

        closeAllWindow();
        var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
        wndGetStatusResponseData.content(detailsTemplateGetStatusResponseData(dataItem));
        wndGetStatusResponseData.center().open();
    }

    function showDetailsGetResponseRequestData(e) {
        e.preventDefault();
        
        closeAllWindow();
        var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
        alert(dataItem.QueueLastRequestData);
        //wndGetResponseRequestData.content(dataItem.QueueLastRequestData);//(detailsTemplateGetResponseRequestData(dataItem));
        //wndGetResponseRequestData.center().open();
    }

    function showDetailsGetData(e) {
        e.preventDefault();

        closeAllWindow();
        if (e.data.commandName == "QueueRequestData") {
            
            var dataItem = this.dataItem($(e.currentTarget).closest("tr"));

            wndQueueRequestData.content(dataItem.QueueLastRequestData);//detailsTemplateGetResponseResponseData(dataItem));
            wndQueueRequestData.center().open();
        }
        else if (e.data.commandName == "QueueResponseData") {
            var dataItem = this.dataItem($(e.currentTarget).closest("tr"));

            wndQueueResponseData.content(dataItem.QueueLastResponseData);//detailsTemplateGetResponseResponseData(dataItem));
            wndQueueResponseData.center().open();
        }
        else if (e.data.commandName == "GetStatusRequestData") {
            var dataItem = this.dataItem($(e.currentTarget).closest("tr"));

            wndGetStatusResponseData.content(dataItem.GetStatusLastRequestData);//detailsTemplateGetResponseResponseData(dataItem));
            wndGetStatusResponseData.center().open();
        }
        else if (e.data.commandName == "GetStatusResponseData") {
            var dataItem = this.dataItem($(e.currentTarget).closest("tr"));

            wndGetStatusResponseData.content(dataItem.GetStatusLastResponseData);//detailsTemplateGetResponseResponseData(dataItem));
            wndGetStatusResponseData.center().open();
        }
        else if (e.data.commandName == "GetResponseRequestData") {
            var dataItem = this.dataItem($(e.currentTarget).closest("tr"));

            wndGetResponseRequestData.content(dataItem.GetResponseLastRequestData);//detailsTemplateGetResponseResponseData(dataItem));
            wndGetResponseRequestData.center().open();
        }
        else if (e.data.commandName == "GetResponseResponseData") {
            var dataItem = this.dataItem($(e.currentTarget).closest("tr"));

            wndGetResponseResponseData.content(dataItem.GetResponseLastResponseData);//detailsTemplateGetResponseResponseData(dataItem));
            wndGetResponseResponseData.center().open();
        }
    }

    function closeAllWindow()
    {
        wndQueueRequestData.close();
        wndQueueResponseData.close();
        wndGetStatusRequestData.close();
        wndGetStatusResponseData.close();
        wndGetResponseRequestData.close();
        wndGetResponseResponseData.close();
    }
</script>


