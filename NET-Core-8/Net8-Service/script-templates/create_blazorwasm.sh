#!/bin/bash
set -e

# =============================================================================
# BLAZOR WEBASSEMBLY (WASM) APPLICATION 
# Creates a Blazor app that runs entirely in the browser using WebAssembly.
# Use for: SPAs (Single Page Apps), client-side web apps, offline-capable apps
# Output: Runs completely in browser (like React/Angular), can work offline
# Tech: C# code compiled to WebAssembly, runs in browser without server
# Note: Different from Blazor Server - this runs client-side, not server-side
# =============================================================================

# First argument may be the application name, if not it uses the default specified here is used
APP_NAME=${1:-app-blazorwasm}
# Second argument may be the target directory, if not it uses the default specified here is used
TARGET_DIR=${2:-/hostmount/workspace}

# Create blazor webassembly app in target directory
dotnet new blazorwasm -o "$TARGET_DIR/$APP_NAME"
cd "$TARGET_DIR/$APP_NAME"
dotnet restore

# Call scaffold script to generate additional scaffolding (scripts are in same folder)
cd -
./env_scaffold.sh "$TARGET_DIR/$APP_NAME"

echo ".NET 8 Blazor WebAssembly app: '$APP_NAME' created in directory: $TARGET_DIR "
echo "Run the app with the command:"
echo "dotnet run --urls \"http://0.0.0.0:5000\""

# Call syntax
# Default: ./create_blazorwasm.sh                      => (uses defaults: app-blazorwasm in /hostmount/workspace)
# option 1:./create_blazorwasm.sh  my-wasm             => (custom name, default location)
# option 2:./create_blazorwasm.sh  my-wasm /home/work  => (custom name and location)