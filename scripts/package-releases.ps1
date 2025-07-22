param (
    [string]$Directory,
    [string]$Version,
    [string]$ApiKey
)

$ProjectFilter = "*.csproj"

if ($Version -like "v*") {
    $Version = $Version.Substring(1)
} elseif ($Version -like "input*") {
    $Version = $Version.Substring(5)
    $ProjectFilter = "Orbit.Input.csproj"
} elseif ($Version -like "engine*") {
    $Version = $Version.Substring(6)
    $ProjectFilter = "Orbit.Engine.csproj"
}

Get-ChildItem -Path $Directory -Recurse -Filter $ProjectFilter |
    Where-Object { $_.Name -notlike "*Test*" -and $_.FullName -notmatch "games" } |
    ForEach-Object {
        Write-Host "Restoring $($_.FullName)"
        dotnet restore $_

        Write-Host "Building $($_.FullName)"
        dotnet build --configuration Release --no-restore $_ /p:Version=$Version

        Write-Host "Packing $($_.FullName)"
        dotnet pack $_ -c Release /p:Version=$Version --no-build --output .

        $PackageWithVersion = "Bijington.$($_.Directory.Name).$Version"

        Write-Host "Pushing $PackageWithVersion.nupkg"
        dotnet nuget push "$PackageWithVersion.nupkg" -s https://api.nuget.org/v3/index.json -k $ApiKey

        Write-Host "Pushing $PackageWithVersion.snupkg"
        dotnet nuget push "$PackageWithVersion.snupkg" -s https://api.nuget.org/v3/index.json -k $ApiKey
    }
