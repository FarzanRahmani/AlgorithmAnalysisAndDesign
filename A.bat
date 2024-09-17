mkdir E2
cd E2
mkdir E2
mkdir E2.Tests
cd E2
dotnet new console
dotnet add reference ..\..\TestCommon
cd ..\E2.Tests
dotnet new mstest
dotnet add reference ..\..\TestCommon
dotnet add reference ..\E2
cd ..
dotnet new sln
dotnet sln add E2 E2.Tests ..\TestCommon
dotnet test
mkdir Coursera