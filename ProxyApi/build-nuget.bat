@echo off
set path=.
if not [%1]==[] set path=%1

echo Creating ProxyApi Package
-- %path%\..\.nuget\nuget pack %path%\ProxyApi.csproj -OutputDirectory %path%\..\Nuget\Packages -Prop Configuration=Release -Prop TargetPath=%path%\bin\Release\ProxyApi.dll

if errorlevel 1 echo Error creating ProxyApi Package

echo Creating ProxyApi.Intellisense Package
-- %path%\..\.nuget\nuget pack %path%\ProxyApi.Intellisense.nuspec -OutputDirectory %path%\..\Nuget\Packages
if errorlevel 1 echo Error creating ProxyApi.Intellisense Package