#!/bin/bash
set -e

# =============================================================================
# 
#   Adds the Scaffold CLI to a project
#
#       Call syntax: ./create_Scaffold.sh <project directory>
#
# =============================================================================

# First argument is the project directory
PROJECT_DIR=${1}

# Validate that project directory was provided
if [ -z "$PROJECT_DIR" ]; then
    echo "Error: Project directory not provided"
    echo "Usage: ./create_Scaffold.sh <project directory>"
    exit 1
fi

# Navigate to the project directory
cd "$PROJECT_DIR"

# Try to install/update the tool globally (safe to run multiple times)
echo "Installing scaffolding tool..."
dotnet tool update -g dotnet-aspnet-codegenerator --version 8.0.0 > /dev/null 2>&1 || \
dotnet tool install -g dotnet-aspnet-codegenerator --version 8.0.0 > /dev/null 2>&1 || \
echo "Warning: Tool installation failed, but packages will still be added"

# Add .NET tools to PATH permanently if not already there
if ! grep -q '.dotnet/tools' ~/.bashrc 2>/dev/null; then
    echo 'export PATH="$PATH:$HOME/.dotnet/tools"' >> ~/.bashrc
    ADDED_TO_BASHRC=true
fi

# Add the required scaffolding packages to the project (NET 8 compatible versions)
echo "Adding scaffolding packages..."
dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design --version 8.0.0 > /dev/null 2>&1
dotnet add package Microsoft.EntityFrameworkCore.Design --version 8.0.0 > /dev/null 2>&1
dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 8.0.0 > /dev/null 2>&1

if [ $? -eq 0 ]; then
    echo "✓ Scaffold setup completed successfully in: $PROJECT_DIR"
    if [ "$ADDED_TO_BASHRC" = "true" ]; then
        echo ""
        echo "  ⚠ PATH updated. Run this once in your terminal:"
        echo "  source ~/.bashrc"
    fi
else
    echo "✗ Scaffold setup failed"
    exit 1
fi

