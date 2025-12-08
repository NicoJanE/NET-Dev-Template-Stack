---
layout: default_c

RefPages:
  - Setup

TableCont:
  - Introduction
  - Setup

--- 
<br>

# .NET Development Containers <span style="color: #409EFF; font-size: 0.6em; font-style: italic;"> -  Docker Container</span>

<a id="Introduction"></a>

## Introduction

This repository contains Docker-based development environments for different .NET container environments. Currently we have only the latest **.NET 8.0** included (see sub-folder <span class="nje-cmd-inline-sm">NET-Core-8</span>) with support for additional .NET versions planned for future as needed.

### 🎯 What's Included

Each .NET development environment provides:

- **.NET SDK** with all runtimes (console, web, desktop)
- **Project templates generation scripts:** Console, Web API, MVC, Blazor, gRPC, Tests, and more
- **Cross-platform development** (Windows ↔ Linux containers)
- **Development tools** and utilities for streamlined workflows, including Visual Studio code task and launch files

### 📁 Repository Structure

```text
NET-Dev-Template-Stack/
├── NET-Core-8/                         # .NET 8.0 development environment
│   ├── Net8-Service/                   # Main service container
│   │   ├── script-templates/           # Project creation scripts
│   │   ├── Dockerfile_netcore_cont     # Container definition
│   │   └── compose_netcore_cont.yml    # Docker Compose
│   ├── Howtos/                         # Documentation and guides
│   └── README.md                       # .NET 8.0 specific documentation
├── assets/                             # Documentation assets
└── README.md                           # This file
```

<a id="Setup"></a>

## Setup

This refers to the .NET 8.0 development container, the only .Net Container currently. To setup refer to this page:

- **📖🚀 Enhanced Documentation .NET core 8.0**  
  <span class="nje-indent1">View full documentation with better navigation: [Documentation Site](https://nicojane.github.io/NET-Dev-Template-Stack//NET-Core-8/)
  </span>

<span style="color: #6d757dff; font-size: 10px; font-style: italic;"> <br>
This file is part of: **Net-Core-Template Stack**
Copyright (c) 2025 Nico Jan Eelhart. This source code is licensed under the MIT License found in the  'LICENSE.md' file in the root directory of this source tree.</span>

<center>─── ✦ ───</center>
