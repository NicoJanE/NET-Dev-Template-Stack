#!/bin/bash
set -e

# =============================================================================
# WPF application
# 
# =============================================================================

# First argument may be the application name, if not it uses the default specified here is used
APP_NAME=${1:-app-wpf}
# Second argument may be the target directory, if not it uses the default specified here is used
TARGET_DIR=${2:-/hostmount/workspace}

# Create worker service app in target directory , dotnet new wpf -o "wpf-app"
dotnet new wpf -o "$TARGET_DIR/$APP_NAME"
cd "$TARGET_DIR/$APP_NAME"
dotnet restore
echo ".NET 8 Windows WPF  app: '$APP_NAME' created in directory: $TARGET_DIR "
echo "Run the app with the command:"
echo "dotnet run"

# Call syntax
# Default: ./create_wpf.sh                                                   => (uses defaults: app-worker in /hostmount/workspace)
# option 1:./create_wpf.sh  my-wpf-namer                        => (custom name, default location)
# option 2:./create_worker.sh  my-wpf2 /home/work        => (custom name and location)
