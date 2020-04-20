# OregonTrail
Blazor and Mobile Application to handle the Oregon Trail in real life!
Blazor WASM application used to assist History Colorado in their annual Oregon Trail IRL Event(https://www.historycolorado.org/event/oregon-trailr-irl/2020/01/25)

### Prerequisites
https://portal.azure.com/
1 storage account
1 Azure SQL DB

Not required, but highly recommended to download Visual Studio 2019 Preview -
(https://visualstudio.microsoft.com/vs/preview/) as of 2020/04/20 this is the only way to debug the OregonTrail.UI.Client project.

### Setup
In the OregonTrail.UI.Server project navigate to the appsettings.json
Update the blank "AzureStorage" connection string with the one provided from azure:
```
"ConnectionStrings": {
  "AzureStorage":"DefaultEndpointsProtocol=https;AccountName={your account name};AccountKey={your account key};EndpointSuffix=core.windows.net"
  },
```

In the OregonTrail.Data project navigate to the data_appsettings.json
Update the "{}" sections with your information. The initial catalog should be called "OregonTrail" to run as expected.
```
"ConnectionStrings": {
    "AzureConnection": "Server={your server};Initial Catalog=OregonTrail;Persist Security Info=False;User ID={your user id};Password={your password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
}
```

Open Package Manager Console with the project 'OregonTrail.Data' selected and run the following command:
```
update-database
```

You should be good to open the website by starting the OregonTrail.UI.Server project!

### Functionality
Functionality only exists in the /items razor page where the user can Create/View/Edit the Items in their Azure database.
The project as of 4/20/2020 utilizes the following blazor tools for visualization:
https://blazor.radzen.com/ - 
  DataGrid + dialogs representing modals + primarly css styling for buttons (backbone)
https://www.nuget.org/packages/CurrieTechnologies.Blazor.SweetAlert2/0.1.4-preview - 
  Toasting on successful item creation - might go to Blazored Toast in the future depending on user feedback
  Alerts w/ confirmation on item deletion.

Please reach out if you have any questions @taherkhanijosh@gmail.com  
