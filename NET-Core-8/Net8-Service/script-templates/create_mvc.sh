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

## BEGIN
## Changes required because we like to have the: ./controlls, ./Models, ./Views and ./Data 
## Into the folder ./Source
#
# Move MVC folders into ./Source
mkdir -p Source
for folder in Controllers Models Views; do
	if [ -d "$folder" ]; then
		mv "$folder" "Source/"
	fi
done
#
# Add reusable MVC setup extension + update Program.cs to use it
CS_PROJ=$(ls *.csproj 2>/dev/null | head -n 1)
ROOT_NAMESPACE=""
if [ -n "$CS_PROJ" ]; then
	ROOT_NAMESPACE=$(grep -oP '(?<=<RootNamespace>).*?(?=</RootNamespace>)' "$CS_PROJ" | head -n 1)
fi
if [ -z "$ROOT_NAMESPACE" ]; then
	ROOT_NAMESPACE=$(echo "$APP_NAME" | sed 's/[^a-zA-Z0-9_]/_/g')
fi
#
mkdir -p Source/Extensions
EXT_FILE="Source/Extensions/ServiceCollectionExtensions.cs"
if [ ! -f "$EXT_FILE" ]; then
	cat > "$EXT_FILE" <<EOF
using Microsoft.Extensions.DependencyInjection;

namespace ${ROOT_NAMESPACE};

public static class ServiceCollectionExtensions
{
    public static IMvcBuilder AddMvcWithSourceViews(this IServiceCollection services)
    {
        return services.AddControllersWithViews()
            .AddRazorOptions(options =>
            {
                options.ViewLocationFormats.Add("/Source/Views/{1}/{0}.cshtml");
                options.ViewLocationFormats.Add("/Source/Views/Shared/{0}.cshtml");
            });
    }
}
EOF
fi

if [ -f "Program.cs" ] && ! grep -q "AddMvcWithSourceViews" Program.cs && ! grep -q "/Source/Views/" Program.cs; then
	if ! grep -q "using ${ROOT_NAMESPACE};" Program.cs; then
		awk -v ns="${ROOT_NAMESPACE}" 'NR==1 { print "using " ns ";"; print "" } { print }' Program.cs > Program.cs.tmp && mv Program.cs.tmp Program.cs
	fi
	if grep -q "AddControllersWithViews" Program.cs; then
		sed -i 's/AddControllersWithViews()[[:space:]]*;/AddMvcWithSourceViews();/' Program.cs
	fi
fi

## END Move files to ./Source

# Call scaffold script to generate additional scaffolding (scripts are in same folder)
cd -
./env_scaffold.sh "$TARGET_DIR/$APP_NAME"

echo ".NET 8 MVC Web App: '$APP_NAME' created in directory: $TARGET_DIR "
echo "Run the app with the command:"
echo "dotnet run --urls \"http://0.0.0.0:5000\""

# Call syntax
# Default: ./create_mvc.sh                      => (uses defaults: app-mvc in /hostmount/workspace)
# option 1:./create_mvc.sh  my-webapp           => (custom name, default location)
# option 2:./create_mvc.sh  my-webapp /home/work => (custom name and location)