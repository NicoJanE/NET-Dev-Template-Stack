#!/bin/bash
set -e

# =============================================================================
# CONSOLE APPLICATION
# Creates a simple command-line application that runs in a terminal.
# Use for: CLI tools, batch processing, scripts, utilities, learning C#
# Output: Runs in terminal, no web interface
# =============================================================================

# First argument may be the application name, if not it uses the default specified here is used
APP_NAME=${1:-app-console}
# Second argument may be the target directory, if not it uses the default specified here is used
TARGET_DIR=${2:-/hostmount/workspace}


# Create console app in target directory
dotnet new console -o "$TARGET_DIR/$APP_NAME"
cd "$TARGET_DIR/$APP_NAME"
dotnet restore
echo ".NET 8 Console app: '$APP_NAME' created in directory: $TARGET_DIR "
echo "Run the app with the command:"
echo "dotnet run"

# Copy the VS Code settings, User must check them, for the correct paths
mkdir -p .vscode
cp "../../workspace/.vscode-templates/tasks.json" ".vscode/tasks.json"
cp "../../workspace/.vscode-templates/settings.json" ".vscode/settings.json"
cp "../../workspace/.vscode-templates/launch_console.json" ".vscode/launch.json"
cp "../../workspace/.vscode-templates/Directory.Build.props" "./Directory.Build.props"


# Call syntax
# Default: ./create_console.sh                      => (uses defaults: app-console in /hostmount/workspace)
# option 1:./create_console.sh  my-app              => (custom name, default location)
# option 2:./create_console.sh  my-app /home/work   => (custom name and location)

