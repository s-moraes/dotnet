It is part of "Entity Framework in Depth: The Complete Guide" Udemy course  


1) Start sqlserver  
2) Import sql script  
3) scaffold db using: dotnet ef dbcontext scaffold "Server=localhost;DataBase=Pluto;Uid=sa;Pwd=123SQLserver" Microsoft.EntityFrameworkCore.SqlServer -o Models -f -c DemoDbContext  
