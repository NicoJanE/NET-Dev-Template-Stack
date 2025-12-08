# .NET Development Container <span style="color: #409EFF; font-size: 0.6em; font-style: italic;"> -  Docker Container</span>

## Introduction

This repository contains Docker-based development environments for different .NET container environments. Currently we have only the latest **.NET 8.0** included (see sub-folder `NET-Core-8` with support for additional .NET versions planned for future as needed.

### 🎯 What's Included

Each .NET version template typically contains:

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

## Setup

This refers to the .NET 8.0 development container, the only .Net Container Currently. To setup refer to this page:

- **🚀 .NET core 8.0**  
  <span class="nje-indent1">Installation  instructions for the [.NET 8.0 container service](https://nicojane.github.io/NET-Dev-Template-Stack/NET-Core-8/)
  </span>
- **🚀 Site page**  
  <span class="nje-indent1">Main page [this site ](https://nicojane.github.io/NET-Dev-Template-Stack/NET-Core-8/)
  </span>

<br>
<p align="center">
  <a href="https://nicojane.github.io/Docker-Template-Stacks-Home/">
    <img src="assets/images/DTSfooter.svg" alt="DTS Template Stacks" width="400" />
  </a>
</p>

<sub> <i>This file is part of: **Net-Core-Template Stack**
Copyright (c) 2025 Nico Jan Eelhart. This source code is licensed under the MIT License found in the  'LICENSE.md' file in the root directory of this source tree.</i>
</sub>

<p align="center">─── ✦ ───</p>
