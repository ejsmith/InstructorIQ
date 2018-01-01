$workingDirectory = Resolve-Path -Path .\
$buildDirectory = Resolve-Path -Path $workingDirectory\build

If((Test-Path $buildDirectory)) {
    Remove-Item $buildDirectory -Recurse -Force
} Else{
    New-Item -ItemType Directory -Force -Path $buildDirectory
}

# build client
Write-Host "*** Build Client ***"

Push-Location $workingDirectory\client\

& npm install aurelia-cli -g
& npm install
& au build --env prod

Pop-Location

# build database
Write-Host "*** Build Database ***"
& msbuild $workingDirectory\database\InstructorIQ.sln /t:Build /p:Configuration=Release  /p:OutputPath=$buildDirectory\database

# build service
Write-Host "*** Build Service ***"
& dotnet publish $workingDirectory\service\src\InstructorIQ.Web\InstructorIQ.Web.csproj -c Release -o $buildDirectory\service

# create package
Write-Host "*** Create Packages ***"
Copy-Item -Path $workingDirectory\client\dist -Destination $buildDirectory\service\wwwroot -recurse -Force

# zip package
Write-Host "*** Zip Packages ***"
Compress-Archive -Path $buildDirectory\service\* -DestinationPath $buildDirectory\InstructorIQ.Website.zip