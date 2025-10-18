#!/bin/bash
set -e

# =============================================================================
# WORKER SERVICE (Background Service)
# Creates a long-running background service that runs continuously.
# Use for: Scheduled tasks, message queue processing, monitoring, data synchronization
# Output: Runs in background (no UI), processes tasks continuously, logs to console
# Example: Email sender, file processor, database cleanup, health checks
# =============================================================================

# First argument may be the application name, if not it uses the default specified here is used
APP_NAME=${1:-app-worker}
# Second argument may be the target directory, if not it uses the default specified here is used
TARGET_DIR=${2:-/hostmount/workspace}

# Create worker service app in target directory
dotnet new worker -o "$TARGET_DIR/$APP_NAME"
cd "$TARGET_DIR/$APP_NAME"
dotnet restore
echo ".NET 8 Worker Service app: '$APP_NAME' created in directory: $TARGET_DIR "
echo "Run the app with the command:"
echo "dotnet run"

# Call syntax
# Default: ./create_worker.sh                      => (uses defaults: app-worker in /hostmount/workspace)
# option 1:./create_worker.sh  my-worker           => (custom name, default location)
# option 2:./create_worker.sh  my-worker /home/work => (custom name and location)
