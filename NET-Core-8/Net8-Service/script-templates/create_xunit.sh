#!/bin/bash
set -e

# =============================================================================
# XUNIT TEST PROJECT
# Creates a unit testing project using xUnit framework (most popular .NET testing framework).
# Use for: Unit testing, integration testing, TDD (Test-Driven Development)
# Output: Test project that can test other projects, runs with 'dotnet test'
# Tech: xUnit framework with assertions, test discovery, parallel execution
# Note: Essential for professional development - tests your application logic
# =============================================================================

# First argument may be the application name, if not it uses the default specified here is used
APP_NAME=${1:-app-tests}
# Second argument may be the target directory, if not it uses the default specified here is used
TARGET_DIR=${2:-/hostmount/workspace}

# Create xUnit test project in target directory
dotnet new xunit -o "$TARGET_DIR/$APP_NAME"
cd "$TARGET_DIR/$APP_NAME"
dotnet restore
echo ".NET 8 xUnit Test Project: '$APP_NAME' created in directory: $TARGET_DIR "
echo "Run tests with the command:"
echo "dotnet test"
echo "Note: Add project references to test other projects with 'dotnet add reference ../MyProject'"

# Call syntax
# Default: ./create_xunit.sh                      => (uses defaults: app-tests in /hostmount/workspace)
# option 1:./create_xunit.sh  my-tests            => (custom name, default location)
# option 2:./create_xunit.sh  my-tests /home/work => (custom name and location)