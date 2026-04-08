# Unity-Asset-Automator
A Unity AssetPostprocessor tool that automatically applies optimized import settings based on asset naming conventions.  Designed to reduce manual setup, enforce consistency, and improve mobile performance across projects.

Project Automator (Unity)

This tool automatically configures import settings for assets in Unity based on naming conventions.


###  Purpose

Reduce manual work
Enforce consistency across the project
Improve runtime performance (CPU, GPU, memory)

###  Naming Convention

- `TS_` → UI Sprites (no mipmaps, alpha optimized)  
- `T_` → Default textures (ASTC compressed)  
- `SM_` → Static meshes (no animation, no extra data)  
- `RM_` → Rigged meshes (optimized animation settings)  
- `SFX_` → Memory-efficient audio  
- `Music_` → Streaming audio  

###  Key Features

- Fully automatic on import  
- Platform-specific texture optimization (ASTC)  
- Prevents incorrect import settings  
- Uses internal marker system to avoid reprocessing  
