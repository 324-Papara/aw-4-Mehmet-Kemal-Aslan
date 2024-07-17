create migration SQL Server
	 dotnet ef migrations add Initial -s ../Para.Api/ --context ParaMsSqlDbContext
db guncelleme MsSql 
	 dotnet ef database update --startup-project "../Para.Api/" --context ParaMsSqlDbContext
create migration PostgreSQL
	 dotnet ef migrations add Initial -s ../Para.Api/ --context ParaPostgreDbContext
db guncelleme Postgre
	 dotnet ef database update --startup-project "../Para.Api/" --context ParaPostgreDbContext