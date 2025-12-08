---
layout: default_c
RefPages:
 - Setup

TableCont:
 - Introduction
 - Setup
 - Create-Start .NET container
 - Troubleshooting
 - Using Scripts to create Sample Apps
 - Create the Sample Apps using .NET
 - Use with VS Code
 - Quick install

--- 
<br>

# .NET 8.0 Development Container <span style="color: #409EFF; font-size: 0.6em; font-style: italic;"> -  Docker Setup & Usage Guide</span>

<a id="Introduction"></a>

## Introduction

This guide walks you through setting up a Docker container with a **.NET 8.0 development environment**, including:

- Pre-configured scripts for generating .NET project templates
- **tasks.json** and **launch.json** for Visual Studio Code integration
- All .NET 8.0 SDKs and runtimes

**✅ Prerequisites**  
  <span class="nje-indent1"> - Docker Desktop installed and running </span>  
  <span class="nje-indent1"> - Repository is cloned to local machine </span>  
  <span class="nje-indent1"> - External Docker network (configured in ***.env*** file) </span>

<a id="Setup"></a>

---

## Setup

Follow these steps to set up your .NET 8.0 development container.

<a id="Create-Start .NET container"></a>

### Create and Start the .NET Container

**Navigate to the service directory:**

Open PowerShell and navigate to the <span class="nje-cmd-inline-sm">.\NET-Core-8\Net8-Service</span> directory, then follow these steps:

**1. Create the external network**

   Because this service uses an **external network**, you must ensure the network is created **before** creating the container. The network configuration is defined in the `.env` file. Create the network with this command:
   <pre class="nje-cmd-multi-line-sm"> docker network create --subnet=172.40.0.0/24 dev1-net
    # This subnet is defined in `.env`</pre>
   <div class="nje-br2"> </div>
   <span class="nje-colored-block" style="--nje-bgcolor:gray; --nje-textcolor:white; ">
   If you get an error message that the network already exists, you're probably good to go!
   </span>

**2. Create the container**

   <pre class="nje-cmd-multi-line-sm">
    docker-compose -f compose_netcore_cont.yml up -d
    docker-compose -f compose_netcore_cont.yml up -d --build --force-recreate
   </pre>
   After that, you should have a Docker Desktop container called: **`net8-service-net-core8-img-1`**.

**3. To start a CLI in this container:**

   <pre class="nje-cmd-one-line-sm-indent1"> docker exec -it net8-service-net-core8-img-1 bash </pre>

<div class="nje-br2"> </div>
<a id="Troubleshooting"></a>

#### Troubleshooting: Container is Not Starting

If the container fails to start or exits unexpectedly, check the logs:

<pre class="nje-cmd-multi-line-sm">
# Get ID of container
docker ps           # Only returns running containers!
docker ps -a        # Includes stopped containers! (-a => all)
docker logs [ID]    # See what's going on
</pre>

<div class="nje-br2"> </div>

#### Verify .NET Installation

To verify the .NET SDK is installed correctly:

<pre class="nje-cmd-one-line-sm-indent1">
docker exec -it net8-service-net-core8-img-1 bash
</pre>

Then inside the container:
<pre class="nje-cmd-multi-line-sm">
dotnet --list-sdks
dotnet --list-runtimes
</pre>
<div class="nje-br2"> </div>

---
       
<a id="Using Scripts to create Sample Apps"></a>

### Creating Applications with the Scripts

Assuming that the Docker container has been successfully created and can be started with:<span class="nje-cmd-inline-sm">  docker exec -it net8-service-net-core8-img-1 bash </span>
we will show here how you can create a .NET 8.0 Template application with the different scripts that are provided.

#### Quick Start with Script Templates

This container includes **11 ready-to-use script templates** for creating different types of .NET 8 applications. When you start the docker container, you will automatically enter the **host mount workspace** (`/hostmount/workspace`) in your **Bash** shell, if not, <span class="nje-cmd-inline-sm">cd</span> to that directory.

**To access the scripts:**  
Execute these commands:  

<pre class="nje-cmd-multi-line-sm">
cd scripts                   # Navigate to script templates
ls -la                       # List all available scripts
</pre>
<span class="nje-expect-block"> The scripts will create the applications in directory: **/hostmount/workspace** </span>

#### Available Script Templates

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

#### Using the Scripts

**Basic Usage:**

<pre class="nje-cmd-multi-line-sm">
cd scripts
./create_console.sh                    # Creates 'app-console' in workspace
./create_webapi.sh my-api              # Creates 'my-api' in workspace  
./create_mvc.sh my-site /custom/path   # Creates 'my-site' in custom location
</pre>
<div class="nje-br2"> </div>
<div class="nje-text-block" style="background-color:gray; color:yellow">

**Script Parameters:**

- **First parameter**: Application name (optional, uses default if omitted)
- **Second parameter**: Target directory (optional, defaults to:  <span class="nje-cmd-inline-sm">/hostmount/workspace</span>)

</div>

<div class="nje-br2"> </div>
<details class="nje-back-box">
  <summary>Why Applications are Created in Host Workspace
  </summary>

### Why Applications are Created in Host Workspace

**By default, all applications are created in `/hostmount/workspace`** because:

- ✅ **Accessible from both Windows host and container**
- ✅ **Persistent** - survives container restarts
- ✅ **Easy editing** - use Windows tools (VS Code, Visual Studio)
- ✅ **Source control** - git repositories work seamlessly

### Performance

Note: For CPU-intensive builds, you can copy projects to the container:

<span class="nje-cmd-inline-sm"> cp -r ./my-app /cworkspace/ </span>

<p align="center" style="padding:20px;">─── ✦ ───</p>
</details>
<div class="nje-br4"> </div>

<a id="Create the Sample Apps using .NET"></a>

### Create the Sample Apps by Using .NET (Alternative)

If your preferred .NET template isn't scripted, use the official .NET CLI in the **Bash shell**

**ASP.NET Core Example:**  
For example to create an ASP.NET Core program (manually) follow these steps:

<div class="nje-indent1" style="margin-bottom:-30px;">
  <span class="nje-colored-block" > Note: The resulting application will be created in the host mount folder: **/hostmount/workspace** </span>
</div>

- Create the app <br>
  <pre class="nje-cmd-inline-sm">
  dotnet new web -n MyAspNetApp
  </pre>

- Get the NuGet packages required
  <pre class="nje-cmd-multi-line-sm">
  cd MyAspNetApp
  dotnet restore
  </pre>

- Run the app
  <pre class="nje-cmd-multi-line-sm">
  # Start the application server
  dotnet run --urls "http://0.0.0.0:5000" &
  </pre>

- Access the app on the host
  Because in the previous command we specified 0.0.0.0 as the listening IP address, we can reach the web page on the **host** via localhost! (see also 'Note 1' below) So from the host, open a browser and navigate to: <span class="nje-cmd-inline-sm"> http://localhost:5000/</span>

<div class="nje-br2"> </div>
<details class="nje-note-box">
  <summary>Getting Container IP Address
  </summary>
If you need the IP address of the container to access the page via the host:
- Get the name or ID of the container: <span class="nje-cmd-inline-sm">docker ps</span>
- Execute this to get the IP:
  <div class="nje-br2"> </div>
  <pre class="nje-cmd-multi-line-sm">{% raw %}docker inspect -f '{{range .NetworkSettings.Networks}}{{.IPAddress}}{{end}}' [container_id_or_name]
  # For this specific container:
  docker inspect -f '{{range .NetworkSettings.Networks}}{{.IPAddress}}{{end}}' net8-service-net-core8-img-1
  {% endraw %}
  </pre>

Use the found IP to access the web page on the host. Start your browser on the **host** and navigate to: <span class="nje-cmd-inline-sm">http://[found_IP]:5000</span>

</details>
<div class="nje-br4"> </div>
<div class="nje-br2"> </div>

---

<a id="Use with VS Code"></a>

## Build Template projects with Visual Studio Code

<span style="color: #ea7602ff; font-size: 1.0em; font-style: italic;"> *Work In Progress -> Coming soon</span>











<br>
<a id="Quick install"></a>

<details>
<summary class="clickable-summary"> <span class="summary-icon"></span>
    <span style="color: #097df1ff; font-size: 26px;">Appendix I</span> <span style="color: #409EFF; font-size: 16px; font-style: italic;"> -  Quick Start Guide </span>
</summary>

## 🚀 Get up and running with .NET 8 development in under 5 minutes!

### Prerequisites

1. **Create network:**
   <pre class="nje-cmd-inline-sm">
   docker network create --subnet=172.40.0.0/24 dev1-net
   </pre>

2. **Create/Start container:**
   <pre class="nje-cmd-inline-sm">
   docker-compose -f compose_netcore_cont.yml up -d
   </pre>

### Create Your First App

1. **Enter container:**
   <pre class="nje-cmd-inline-sm">
   docker exec -it net8-service-net-core8-img-1 bash
   </pre>

2. **Create an application** (choose one):
    <pre class="nje-cmd-multi-line-sm">
   cd scripts
   ./create_console.sh my-first-app      # Console application
   ./create_webapi.sh my-api             # REST API
   ./create_mvc.sh my-website            # Web application
   ./create_blazorwasm.sh my-spa         # Single Page App
   </pre>

3. **Run your application:**
   <pre class="nje-cmd-multi-line-sm">
   cd ../my-first-app    # or whatever you named it
   dotnet run
   </pre>

### That's It! 🎉

<div class="nje-expect-multi-lines-indent2" style="margin-left:4px;">
- **Your code is in:** <span class="nje-cmd-inline-sm">/hostmount/workspace/ </span> (accessible from Windows)
- **Available scripts:** 11 different .NET 8 project templates
- **Need help?** Check the full [documentation above](#setup)
</div>
<div class="nje-br4"> </div>

---

#### Quick Reference - All Available Templates

| **Command** | **Creates** | **Perfect For** |
|:---------|:---------|:-------------|
| `create_console.sh` | Console App | Learning, CLI tools |
| `create_webapi.sh` | REST API | Backend services |
| `create_mvc.sh` | Web App | Traditional websites |
| `create_blazorwasm.sh` | SPA | Modern web apps |
| `create_classlib.sh` | Library | Reusable code |
| `create_xunit.sh` | Tests | Testing your apps |

</details>



<span style="color: #6d757dff; font-size: 10px; font-style: italic;"> <br>
This file is part of: **Net-Core-Template Stack**
Copyright (c) 2025 Nico Jan Eelhart. This source code is licensed under the MIT License found in the  'LICENSE.md' file in the root directory of this source tree.</span>

<center>─── ✦ ───</center>
