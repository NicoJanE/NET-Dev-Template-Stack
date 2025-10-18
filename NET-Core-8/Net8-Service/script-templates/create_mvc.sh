#!/bin/bash
set -e

# =============================================================================
# ASP.NET CORE MVC WEB APPLICATION
# Creates a full-featured web application using Model-View-Controller pattern.
# Use for: Traditional websites, admin dashboards, enterprise web applications
# Output: Complete web pages with controllers, views, models, forms, authentication
# Tech: Server-side rendering, supports complex routing, layouts, partial views
# Note: More structured than Razor Pages, better for complex applications
# =============================================================================

# First argument may be the application name, if not it uses the default specified here is used
APP_NAME=${1:-app-mvc}
# Second argument may be the target directory, if not it uses the default specified here is used
TARGET_DIR=${2:-/hostmount/workspace}

# Create MVC web application in target directory
dotnet new mvc -o "$TARGET_DIR/$APP_NAME"
cd "$TARGET_DIR/$APP_NAME"
dotnet restore
echo ".NET 8 MVC Web App: '$APP_NAME' created in directory: $TARGET_DIR "
echo "Run the app with the command:"
echo "dotnet run --urls \"http://0.0.0.0:5000\""

# Call syntax
# Default: ./create_mvc.sh                      => (uses defaults: app-mvc in /hostmount/workspace)
# option 1:./create_mvc.sh  my-webapp           => (custom name, default location)
# option 2:./create_mvc.sh  my-webapp /home/work => (custom name and location)