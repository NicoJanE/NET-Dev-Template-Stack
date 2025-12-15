#!/bin/bash
set -e

# =============================================================================
# ASP.NET CORE WEB APP (RAZOR PAGES)
# Creates a page-focused web application using Razor Pages (simpler than MVC).
# Use for: Content-heavy websites, blogs, documentation, simpler web applications
# Output: Web pages with server-side rendering, forms, and page-based routing
# Tech: Page-centric approach, each page has its own model and handler methods
# Note: Microsoft's recommended approach for page-focused apps, simpler than MVC
# =============================================================================

# First argument may be the application name, if not it uses the default specified here is used
APP_NAME=${1:-app-webapp}
# Second argument may be the target directory, if not it uses the default specified here is used
TARGET_DIR=${2:-/hostmount/workspace}

# Create Razor Pages web application in target directory
dotnet new webapp -o "$TARGET_DIR/$APP_NAME"
cd "$TARGET_DIR/$APP_NAME"
dotnet restore

# Call scaffold script to generate additional scaffolding (scripts are in same folder)
cd -
./env_scaffold.sh "$TARGET_DIR/$APP_NAME"

echo ".NET 8 Razor Pages Web App: '$APP_NAME' created in directory: $TARGET_DIR "
echo "Run the app with the command:"
echo "dotnet run --urls \"http://0.0.0.0:5000\""

# Call syntax
# Default: ./create_webapp.sh                      => (uses defaults: app-webapp in /hostmount/workspace)
# option 1:./create_webapp.sh  my-website          => (custom name, default location)
# option 2:./create_webapp.sh  my-website /home/work => (custom name and location)