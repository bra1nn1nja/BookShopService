# BookShopService
Book shop MSA service
=====================

How to run
----------

You can build the solution using Visual Studio / VS Code (.Net 6) and run via the IDE or command line.
Alternatively I've provided a docker image here: https://hub.docker.com/r/brainninja/bookshopservice
which you can pull down and run via docker desktop or your preferred platform.

Once loaded you should be able to navigate to the swagger page by adding /swagger after the hostname.
This will allow you to test all the API methods.

In the development environment the API uses an in memory database data which is automatically seeded in memory. 
You do not need to install any database or run any scripts.

In the production environment the API uses SQL Server. You will need to have SQL Server installed and complete 
the connection string in the appsettings.production.json file.
Enitity framework migrations will automatically populate the SQL server database on first run. You don't need to do anything.

Issues
------
There were two issues with the swagger documentation

- The get all books endpoint description sortby parameter description appears twice.
- The post books endpoint bad request example is missing

These are because of supporting package issues which will be fixed in upcoming releases.
I tried a couple of workarounds but couldn't find a suitable one.

Todo
----
- Missing unit tests for the facade and repository.
- Naming for some of the DTOs could be better.
- The ErrorHelper.ExtractFromState is a bit of a cop out and should have been done in automapper.
- .Net 6 merges the programs entry point and startup into one program.cs file. 
	This is good for small programs but not for larger ones with lots of declarations and, in this case, 
	should probably be refactored into smaller dedicated units.
- CORS handling is missing.



