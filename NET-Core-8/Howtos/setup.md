---
layout: default_c
RefPages:
 - Setup
--- 

<small>
<br><br>
_This file is part of: **Net-Core-Template Stack**_
_Copyright (c) 2025 Nico Jan Eelhart_
_This source code is licensed under the MIT License found in the  'LICENSE.md' file in the root directory of this source tree._
</small>
<br><br>

# .NET 8 Development Container Setup Guide

> **🚀 In a hurry?** Jump to the [Quick Start Guide](#appendix-i-quick-start-guide) for immediate setup.

## Table of Contents

- [Setup](#setup)
  - [Create and Use a .NET Container](#create-and-use-a-net-container)
  - [Troubleshooting](#troubleshooting)
  - [Creating Applications with CLI](#creating-applications-in-the-container-with-cli)
- [Quick Start Guide](#appendix-i-quick-start-guide)

## Setup

### Create and Use a .NET Container

To start the container in Docker Desktop, execute this command from the **`.\Net8-Service>`** directory:

### Prerequisites

1. **External network**
   Because this service uses an **external network**, you must ensure that the network is created **before** you create the container. All commands can be found in the `.env` file. The command to create the network is displayed below for convenience:

   <pre class="nje-cmd-multi-line"> docker network create --subnet=172.40.0.0/24 dev1-net
    # This subnet is defined in `.env`</pre>

   If you get an error message that the network already exists, you're probably good to go!

2. **Create the container**

   <pre class="nje-cmd-multi-line">
    docker-compose -f compose_netcore_cont.yml up -d
    docker-compose -f compose_netcore_cont.yml up -d --build --force-recreate
   </pre>
   After that, you should have a Docker Desktop container called: **`net8-service-net-core8-img-1`**.

3. **To start a CLI in this container:**
   <pre class="nje-cmd-one-line"> docker exec -it net8-service-net-core8-img-1 bash </pre>


## Troubleshooting

### Container Issues

When the container is not starting or exiting unexpectedly, check the logs:

<pre class="nje-cmd-multi-line">
# Get ID of container
docker ps           # Only returns running containers!
docker ps -a        # Includes stopped containers! (-a => all)
docker logs [ID]    # See what's going on
</pre>	

### Verify .NET Installation

To check which version of .NET is available, start a CLI in the container and run these commands:

<pre class="nje-cmd-one-line">
docker exec -it net8-service-net-core8-img-1 bash
</pre>

Then inside the container:
<pre class="nje-cmd-multi-line">
dotnet --list-sdks
dotnet --list-runtimes
</pre>

## Creating Applications in the Container with CLI

### Quick Start with Script Templates

This container includes **11 ready-to-use script templates** for creating different types of .NET 8 applications. When you start the container CLI, you automatically enter the **host mount workspace** (`/hostmount/workspace`).

**To access the scripts:**  
Note: the scripts will create the application directory: `/hostmount/workspace`
<pre class="nje-cmd-multi-line">
cd scripts                   # Navigate to script templates
ls -la                       # List all available scripts
</pre>

### Available Script Templates

|**Script** | **Creates** | **Use For** |
|:--------|:---------|:---------|
| `create_console.sh` | Console Application | CLI tools, utilities, learning |
| `create_webapi.sh` | REST API Service | Backend services, microservices |
| `create_webapiaot.sh` | High-Performance API | Ultra-fast APIs, cloud-native |
| `create_mvc.sh` | MVC Web Application | Traditional websites, admin panels |
| `create_webapp.sh` | Razor Pages App | Content-heavy sites, blogs |
| `create_blazorserver.sh` | Blazor Server App | Interactive web apps (server-side) |
| `create_blazorwasm.sh` | Blazor WebAssembly | Client-side web apps (browser) |
| `create_grpc.sh` | gRPC Service | High-performance service communication |
| `create_worker.sh` | Background Service | Scheduled tasks, message processing |
| `create_classlib.sh` | Class Library (DLL) | Reusable code, NuGet packages |
| `create_xunit.sh` | Unit Test Project | Testing your applications |

### Using the Scripts

**Basic Usage:**

<pre class="nje-cmd-multi-line">
cd scripts
./create_console.sh                    # Creates 'app-console' in workspace
./create_webapi.sh my-api              # Creates 'my-api' in workspace  
./create_mvc.sh my-site /custom/path   # Creates 'my-site' in custom location
</pre>

**Script Parameters:**

- **First parameter**: Application name (optional, uses default if omitted)
- **Second parameter**: Target directory (optional, defaults to `/hostmount/workspace`)

### Why Applications are Created in Host Workspace

**By default, all applications are created in `/hostmount/workspace`** because:

- ✅ **Accessible from both Windows host and container**
- ✅ **Persistent** - survives container restarts
- ✅ **Easy editing** - use Windows tools (VS Code, Visual Studio)
- ✅ **Source control** - git repositories work seamlessly

**Performance Note:** For CPU-intensive builds, you can copy projects to the container:

<pre class="nje-cmd-one-line">
cp -r ./my-app /cworkspace/
</pre>

### Manual Application Creation

If your preferred template isn't scripted, use the official .NET CLI:

**ASP.NET Core Example:**
To create an ASP.NET Core program manually, follow these steps:

> **Note:** The resulting application will be created in the host mount folder: **`/hostmount/workspace`**

- Create the app

<pre class="nje-cmd-one-line">
dotnet new web -n MyAspNetApp
</pre>

- Get the NuGet packages required

<pre class="nje-cmd-multi-line">
cd MyAspNetApp
dotnet restore
</pre>

- Run the app

<pre class="nje-cmd-multi-line">
# Start the application server
dotnet run --urls "http://0.0.0.0:5000" &
</pre>

- Access the app on the host
  Because in the previous command we specified 0.0.0.0 as the listening IP address, we can reach the web page on the **host** via localhost! (see also 'Note 1' below) So from the host, open a browser and navigate to:

<pre class="nje-cmd-one-line">
http://localhost:5000/
</pre><br>

>**Note 1: Getting Container IP Address**
>If you need the IP address of the container to access the page via the host:
>- Get the name or ID of the container:
>
>  <pre class="nje-cmd-one-line">
>  docker ps
>  </pre>
>- Execute this to get the IP:
>  <pre class="nje-cmd-multi-line">
>  {% raw %}
>  <small>docker inspect -f '{{range .NetworkSettings.Networks}}{{.IPAddress}}{{end}}' [container_id_or_name]
> # For this specific container:
> docker inspect -f '{{range .NetworkSettings.Networks}}{{.IPAddress}}{{end}}' net8-service-net-core8-img-1 </small>
>  {% endraw %}
>  </pre>
>
>Use the found IP to access the web page on the host.
>Start your browser on the **host** and navigate to: 
><pre class="nje-cmd-one-line">http://[found_IP]:5000</pre>

---

<br>

## Appendix I Quick Start Guide

**🚀 Get up and running with .NET 8 development in under 5 minutes!**

### Prerequisites (2 minutes)

1. **Create network:**

   <pre class="nje-cmd-one-line">
   docker network create --subnet=172.40.0.0/24 dev1-net
   </pre>

2. **Start container:**

   <pre class="nje-cmd-one-line">
   docker-compose -f compose_netcore_cont.yml up -d
   </pre>

### Create Your First App (1 minute)

1. **Enter container:**

   <pre class="nje-cmd-one-line">
   docker exec -it net8-service-net-core8-img-1 bash
   </pre>

2. **Create an application** (choose one):

    <pre class="nje-cmd-multi-line">
   cd scripts
   ./create_console.sh my-first-app      # Console application
   ./create_webapi.sh my-api             # REST API
   ./create_mvc.sh my-website            # Web application
   ./create_blazorwasm.sh my-spa         # Single Page App
   </pre>

3. **Run your application:**

   <pre class="nje-cmd-multi-line">
   cd ../my-first-app    # or whatever you named it
   dotnet run
   </pre>

### That's It! 🎉

- **Your code is in:** `/hostmount/workspace/` (accessible from Windows)
- **Available scripts:** 11 different .NET 8 project templates
- **Need help?** Check the full [documentation above](#setup)

### Quick Reference - All Available Templates

| **Command** | **Creates** | **Perfect For** |
|:---------|:---------|:-------------|
| `create_console.sh` | Console App | Learning, CLI tools |
| `create_webapi.sh` | REST API | Backend services |
| `create_mvc.sh` | Web App | Traditional websites |
| `create_blazorwasm.sh` | SPA | Modern web apps |
| `create_classlib.sh` | Library | Reusable code |
| `create_xunit.sh` | Tests | Testing your apps |
