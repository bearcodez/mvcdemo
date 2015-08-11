# mvcdemo

This project used nopCommerce MVC framework. The Views folder contains all the UI view template that will be use to translate into web UI. The data listing is using kendoGrid which is a third-party ajax datagrid and retrieve data from the controllers file.

The interesting part is in the View.cshtml file. The kendoGrid has multiple different type of column such as date, label, text, and button. Clicking on the button on each data row will promp a new modal dialog window and show more data of the selected row. The column also can define own HTML code such as the "Action" column where there is HTML link define for each if/else condition.

The model data of the columns are define in the Models/ERPIntegrationProcessModel.cs
