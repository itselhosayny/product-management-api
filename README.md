# Product Managment API

Product management api using .NET 5.

## How to run using Visual Studio

Clone the project
```bash
git clone https://github.com/itselhosayny/product-management-api.git
```
go to ProductManagement.API folder inside the cloned project and update the appsetting.Developpement file with your own connection string.

Open the solution Productmanagment.sln in VS and then go to "Tools > Package Manager Nuget > Nuget Package Console" and run this command to create the database 

```bash
Update-Database
```
Then run the project you will see a swagger page with all the possible endpoints.