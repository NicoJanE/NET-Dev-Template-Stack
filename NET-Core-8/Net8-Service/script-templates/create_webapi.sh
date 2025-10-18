#!/bin/bash
set -e

# =============================================================================
# WEB API (REST API)
# Creates a RESTful web service that returns JSON data (no HTML pages).
# Use for: Backend services, microservices, mobile app backends, API endpoints
# Output: HTTP endpoints that return JSON, tested via Postman/curl/Swagger
# Example: GET /api/users returns [{"id":1,"name":"John"}]
# =============================================================================

# First argument may be the application name, if not it uses the default specified here is used
APP_NAME=${1:-app-webapi}
# Second argument may be the target directory, if not it uses the default specified here is used
TARGET_DIR=${2:-/hostmount/workspace}

# Create webapi app in target directory
dotnet new webapi -o "$TARGET_DIR/$APP_NAME"
cd "$TARGET_DIR/$APP_NAME"
dotnet restore
echo ".NET 8 WebAPI app: '$APP_NAME' created in directory: $TARGET_DIR "
echo "Run the app with the command:"
echo "dotnet run --urls \"http://0.0.0.0:5000\""

# Call syntax
# Default: ./create_webapi.sh                      => (uses defaults: app-webapi in /hostmount/workspace)
# option 1:./create_webapi.sh  my-api              => (custom name, default location)
# option 2:./create_webapi.sh  my-api /home/work   => (custom name and location)
