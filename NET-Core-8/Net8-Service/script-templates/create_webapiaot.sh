#!/bin/bash
set -e

# =============================================================================
# WEB API (NATIVE AOT - AHEAD-OF-TIME COMPILED)
# Creates ultra-high-performance REST API compiled to native machine code.
# Use for: High-performance microservices, cloud-native apps, containerized APIs
# Output: Native executable with faster startup and lower memory usage
# Tech: Compiled to native code (no JIT), optimized for containers and serverless
# Note: Cutting-edge performance - trades some flexibility for maximum speed
# =============================================================================

# First argument may be the application name, if not it uses the default specified here is used
APP_NAME=${1:-app-webapi-aot}
# Second argument may be the target directory, if not it uses the default specified here is used
TARGET_DIR=${2:-/hostmount/workspace}

# Create native AOT Web API in target directory
dotnet new webapiaot -o "$TARGET_DIR/$APP_NAME"
cd "$TARGET_DIR/$APP_NAME"
dotnet restore

# Call scaffold script to generate additional scaffolding (scripts are in same folder)
cd -
./env_scaffold.sh "$TARGET_DIR/$APP_NAME"

echo ".NET 8 Native AOT Web API: '$APP_NAME' created in directory: $TARGET_DIR "
echo "Run the app with the command:"
echo "dotnet run --urls \"http://0.0.0.0:5000\""
echo "For native compilation: dotnet publish -c Release"


# Copy the VS Code settings, User must check them, for the correct paths
mkdir -p .vscode
cp "../../workspace/.vscode-templates/tasks.json" ".vscode/tasks.json"
cp "../../workspace/.vscode-templates/settings.json" ".vscode/settings.json"
cp "../../workspace/.vscode-templates/launch_webapiaot.json" ".vscode/launch.json"
cp "../../workspace/.vscode-templates/Directory.Build.props" "./Directory.Build.props"


# Call syntax
# Default: ./create_webapiaot.sh                      => (uses defaults: app-webapi-aot in /hostmount/workspace)
# option 1:./create_webapiaot.sh  my-fast-api         => (custom name, default location)
# option 2:./create_webapiaot.sh  my-fast-api /home/work => (custom name and location)