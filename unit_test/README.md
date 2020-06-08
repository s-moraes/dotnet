Unit Testing for C# Developers  
from udemy

run:
- dotnet test  

- dotnet test  --filter Name~Add --logger "console;verbosity=detailed"  

Code Corevage:  
- dotnet add package coverlet.msbuild  
- dotnet test /p:CollectCoverage=true  
