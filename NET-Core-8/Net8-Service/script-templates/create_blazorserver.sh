#!/bin/bash
set -e

# =============================================================================
# BLAZOR SERVER WEB APPLICATION 
# Creates a full web application with HTML pages and interactive UI components.
# Use for: Websites, web dashboards, admin panels, interactive web apps
# Output: Complete web pages with forms, buttons, navigation (like traditional websites)
# Tech: Server-side rendering with real-time updates via SignalR
# =============================================================================

# First argument may be the application name, if not it uses the default specified here is used
APP_NAME=${1:-app-blazorserver}
# Second argument may be the target directory, if not it uses the default specified here is used
TARGET_DIR=${2:-/hostmount/workspace}

# Create blazor server app in target directory
dotnet new blazor -o "$TARGET_DIR/$APP_NAME"
cd "$TARGET_DIR/$APP_NAME"
dotnet restore

# Call scaffold script to generate additional scaffolding (scripts are in same folder)
cd -
./env_scaffold.sh "$TARGET_DIR/$APP_NAME"

echo ".NET 8 Blazor Server app: '$APP_NAME' created in directory: $TARGET_DIR "
echo "Run the app with the command:"
echo "dotnet run --urls \"http://0.0.0.0:5000\""

# Call syntax
# Default: ./create_blazorserver.sh                      => (uses defaults: app-blazorserver in /hostmount/workspace)
# option 1:./create_blazorserver.sh  my-blazor           => (custom name, default location)
# option 2:./create_blazorserver.sh  my-blazor /home/work => (custom name and location)
