# OregonTrail
Blazor and Mobile Application to handle the Oregon Trail in real life!
Blazor WASM application used to assist History Colorado in their annual Oregon Trail IRL Event(https://www.historycolorado.org/event/oregon-trailr-irl/2020/01/25)

### Prerequisites
https://portal.azure.com/
1 storage account
1 Azure SQL DB
1 SendGrid acount

Not required, but highly recommended to download Visual Studio 2019 Preview -
(https://visualstudio.microsoft.com/vs/preview/) as of 2020/04/20 this is the only way to debug the OregonTrail.UI.Client project.

### Setup
In the OregonTrail.UI.Server project create a new JSON Configuration file > "appsettings.json"

Update the blank "AzureStorage" connection string with the one provided from azure:
```
"ConnectionStrings": {
  "AzureStorage":""
  }
```
To integrate with SendGrid utilize the SendGrid definition section of the new appsettings (some of these will require you to create the SendGrid account first):
```
"SendGrid": {
    "APIKey": "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",
    "TemplateId": "d-6d675b8fdf9c4ff69e0c0afbc43afcec",
    "FromEmail": "no-reply@OregonTrailIRL.com"
  },
```
Setup the IdentityServer to recognize the client and that this is being ran as a development project:
```
  "IdentityServer": {
    "Key": {
      "Type": "Development"
    },
    "Clients": {
      "OregonTrail.UI.Client": {
        "Profile": "IdentityServerSPA"
      }
    }
  }
```
If you want an Admin user to be populated at run-time to ensure at least one user has administrative access to the solution provide the following configuration section:
```
"Admin": {
    "UserName": "",
    "Password": "",
    "Email": ""
  }
```

In the OregonTrail.Data project create a new JSON configuration file > "data_appsettings.json"
Update the "{}" sections with your information. The initial catalog should be called "OregonTrail" for the migrations to run as expected.
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
Navigation from the home page to Items setup and User overview.
These views still require styling but all CRUD actions on the items page have been mapped and are working as expected.
Items page ->
 1) Toasts success
 2) Warns on deletion
 3) Modals for editing/inserting

Users page ->
 1) Brings up a basic view model representing IdentityServer4 in a single table.
 2) Deletion of users - requires warning still.
 3) Invitation of new users (Utilizing SendGrid), will create email with basic format and URL to follow back to confirm on website.
 4) Navigation from confirmation email to new page with an OregonTrail logo.
 
Roadmap ->
 1) Confirmation page finalization
 2) Expansion to other permutations to ensure confirmation is not a blocker
 3) Login Page
 4) Implementation of Authorization on pages + Server APIs (JWT tokens to carry forward the roles)
 5) Deploy as free Azure Web App to see if any complications need to be worked out


Please reach out if you have any questions @taherkhanijosh@gmail.com  
