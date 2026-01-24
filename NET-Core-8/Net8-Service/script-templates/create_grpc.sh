#!/bin/bash
set -e

# =============================================================================
# gRPC SERVICE
# Creates a high-performance RPC (Remote Procedure Call) service using Protocol Buffers.
# Use for: Microservice communication, internal APIs, high-performance service-to-service calls
# Output: Binary protocol service (faster than REST), mainly for backend-to-backend communication
# Tech: Uses .proto files to define contracts, supports streaming, very efficient
# =============================================================================

# First argument may be the application name, if not it uses the default specified here is used
APP_NAME=${1:-app-grpc}
# Second argument may be the target directory, if not it uses the default specified here is used
TARGET_DIR=${2:-/hostmount/workspace}

# Create grpc service app in target directory
dotnet new grpc -o "$TARGET_DIR/$APP_NAME"
cd "$TARGET_DIR/$APP_NAME"
dotnet restore
echo ".NET 8 gRPC Service app: '$APP_NAME' created in directory: $TARGET_DIR "
echo "Run the app with the command:"
echo "dotnet run --urls \"http://0.0.0.0:5000\""


# Copy the VS Code settings, User must check them, for the correct paths
mkdir -p .vscode
cp "../../workspace/.vscode-templates/tasks.json" ".vscode/tasks.json"
cp "../../workspace/.vscode-templates/settings.json" ".vscode/settings.json"
cp "../../workspace/.vscode-templates/launch_grpc.json" ".vscode/launch.json"
cp "../../workspace/.vscode-templates/Directory.Build.props" "./Directory.Build.props"

# Call syntax
# Default: ./create_grpc.sh                      => (uses defaults: app-grpc in /hostmount/workspace)
# option 1:./create_grpc.sh  my-grpc             => (custom name, default location)
# option 2:./create_grpc.sh  my-grpc /home/work  => (custom name and location)
