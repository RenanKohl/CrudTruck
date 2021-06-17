# CrudTruck
This project represents a trucks registry made using ASP.NET Core + Entity Framework + Angular

default application login 
user: Rkohl
password: 123

How to run this project?
1. Unit Tests and code coverage: run command dotnet test --collect:"XPlat Code Coverage" at Volvo.CrudTruck.UnitTest 
2. Angular Frontend: run npm install command and when it is complete, run npm start at Volvo.CrudTruck.Angular
3. WEB API: run project Volvo.CrudTruck.API and then access https://localhost:5001
4. Swagger Dashboard: access https://localhost:5001/api/swagger
5. WEB API Integration Tests: open Postman and import collection Integration Tests.postman_collection.json at tests/Volvo.CrudTuck.API.IntegrationTest and execute collection
