# .NET Development Container Templates

This repository contains Docker-based development environments for different .NET versions. Currently we have included .NET 8.0 in the sub-folder `NET-Core-8`, with support for additional .NET versions planned for future releases.

## 🎯 What's Included

Each .NET version template typically contains:

- **.NET SDK** with all runtimes (console, web, desktop)
- **Project templates:** Console, Web API, MVC, Blazor, gRPC, Tests, and more
- **Cross-platform development** (Windows ↔ Linux containers)
- **Instant project creation** with pre-configured scripts
- **Development tools** and utilities for streamlined workflows

## 📁 Repository Structure

```text
NET-Dev-Template-Stack/
├── NET-Core-8/            # .NET 8.0 development environment
│   ├── Net8-Service/      # Main service container
│   │   ├── script-templates/  # Project creation scripts
│   │   ├── Dockerfile_netcore_cont  # Container definition
│   │   └── compose_netcore_cont.yml # Docker Compose
│   ├── Howtos/            # Documentation and guides
│   └── README.md          # .NET 8.0 specific documentation
├── _NET core 7.0/         # .NET 7.0 development environment
├── _Older NET versions/   # Legacy .NET versions
├── assets/                # Documentation assets
└── README.md              # This file
```

## ⚡ Quick Links

- 📚 **The index file for .NET 8.0:** [index](./NET-Core-8/index)
- 🚀 **Get coding in 5 minutes with .NET 8.0:** [Quick Start Guide](./NET-Core-8/Howtos/setup.md#appendix-i-quick-start-guide)
- 📚 **Complete Setup Guide for .NET 8.0:** [Setup Documentation](./NET-Core-8/Howtos/setup.md)

---

<small><small><small>
Version: 1.04 • Released: October 2025
</small></small></small>