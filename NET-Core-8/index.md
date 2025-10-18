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

# .NET 8 Development Container

A complete .NET 8 development environment with **11 ready-to-use project templates** for rapid application development.

## ⚡ Quick Start

🚀 **Get coding in 5 minutes:** [Quick Start Guide](./Howtos/setup.md#appendix-i-quick-start-guide)

## 🎯 What's Included

- **.NET 8 SDK** with all runtimes (console, web, desktop)
- **11 project templates:** Console, Web API, MVC, Blazor, gRPC, Tests, and more
- **Cross-platform development** (Windows ↔ Linux containers)
- **Instant project creation** with pre-configured scripts

## 📚 Documentation

- **Complete Setup Guide:** [setup.md](./Howtos/setup.md)
- **All Templates Overview:** [Available Scripts](./Howtos/setup.md#available-script-templates)

## 🛠️ Example Usage

```bash
docker exec -it net8-service-net-core8-img-1 bash
cd scripts
./create_webapi.sh my-api        # Create REST API
./create_blazorwasm.sh my-spa    # Create SPA
./create_mvc.sh my-website       # Create web app
```

---

<small><small><small>

Version: 1.04 • Released: October 2025

</small></small></small>