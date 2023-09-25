@echo off
setlocal enabledelayedexpansion

echo === Local Development Setup ===

:: Step 0:  Check if .NET 6 runtime is installed
dotnet --version | findstr /C:"6." > nul
if errorlevel 1 (
    echo Error: .NET 6 runtime is not installed. Please install .NET 6 before running this script.
    goto :exit
) else (
    echo .NET 6 runtime is installed.
)

:: Step 1: Check if dotnet-ef is installed
dotnet tool list --global | findstr /C:"dotnet-ef " > nul
if errorlevel 1 (
    echo Installing dotnet-ef tool globally...
    dotnet tool install --global dotnet-ef
    if errorlevel 1 (
        echo Error: Failed to install dotnet-ef tool globally.
        goto :exit
    )
) else (
    echo dotnet-ef tool is already installed. Proceeding with the update...
)

:: Step 2: Update dotnet-ef tool globally
echo Updating dotnet-ef tool globally...
dotnet tool update --global dotnet-ef
if errorlevel 1 (
    echo Error: Failed to update dotnet-ef tool globally.
    goto :exit
)

:: Step 3: Apply EF Core database migrations
echo Applying EF Core database migrations...
dotnet ef database update -p "./BookImporter.Repositories" -s "./BookImporter.Web"
if errorlevel 1 (
    echo Error: Failed to apply EF Core database migrations.
    goto :exit
)

echo === Setup completed successfully ===
goto :exit

:exit
echo Press any key to exit...
pause > nul
