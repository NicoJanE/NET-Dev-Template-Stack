#!/bin/bash
set -e

# =============================================================================
# CLASS LIBRARY (DLL)
# Creates a reusable code library that can be shared across multiple projects.
# Use for: Business logic, utilities, NuGet packages, shared components
# Output: Creates .dll file that works on both Linux and Windows (.NET cross-platform)
# Tech: Compiled library, no executable - referenced by other projects
# Note: Essential building block for larger applications and code reuse
# =============================================================================

# First argument may be the application name, if not it uses the default specified here is used
APP_NAME=${1:-app-classlib}
# Second argument may be the target directory, if not it uses the default specified here is used
TARGET_DIR=${2:-/hostmount/workspace}

# Create class library in target directory
dotnet new classlib -o "$TARGET_DIR/$APP_NAME"
cd "$TARGET_DIR/$APP_NAME"
dotnet restore
echo ".NET 8 Class Library: '$APP_NAME' created in directory: $TARGET_DIR "
echo "Build the library with the command:"
echo "dotnet build"
echo "Note: This creates a .dll that can be referenced by other projects"


# Copy the VS Code settings, User must check them, for the correct paths
mkdir -p .vscode
cp "../../workspace/.vscode-templates/tasks.json" ".vscode/tasks.json"
cp "../../workspace/.vscode-templates/settings.json" ".vscode/settings.json"
cp "../../workspace/.vscode-templates/launch_classlib.json" ".vscode/launch.json"
cp "../../workspace/.vscode-templates/Directory.Build.props" "./Directory.Build.props"


# Call syntax
# Default: ./create_classlib.sh                      => (uses defaults: app-classlib in /hostmount/workspace)
# option 1:./create_classlib.sh  my-library          => (custom name, default location)
# option 2:./create_classlib.sh  my-library /home/work => (custom name and location)