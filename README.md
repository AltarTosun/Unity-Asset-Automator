# Unity-Asset-Automator

A Unity AssetPostprocessor tool that automatically applies optimized import settings based on asset naming conventions.  Designed to reduce manual setup, enforce consistency, and improve mobile performance across projects.

##  Purpose

Reduce manual work
Enforce consistency across the project
Improve runtime performance (CPU, GPU, memory)

##  Naming Convention

- `TS_` → UI Sprites (no mipmaps, alpha optimized)  
- `T_` → Default textures (ASTC compressed)  
- `SM_` → Static meshes (no animation, no extra data)  
- `RM_` → Rigged meshes (optimized animation settings)  
- `SFX_` → Memory-efficient audio  
- `Music_` → Streaming audio  

##  Key Features

- Fully automatic on import  
- Platform-specific texture optimization (ASTC)  
- Prevents incorrect import settings  
- Uses internal marker system to avoid reprocessing  

## What Happens Automatically

###  Textures

- **TS_** → Imported as Sprite  
  - No mipmaps  
  - Alpha preserved  
  - Optimized for UI  

- **T_** → Imported as Default Texture  
  - Compressed (mobile-friendly)  
  - Optimized for performance  


###  Models

- **SM_** (Static Mesh)  
  - No animation, cameras, or lights imported  
  - Materials not auto-generated  
  - Optimized for runtime performance  

- **RM_** (Rigged Mesh)  
  - Animation optimized  
  - Unnecessary data removed  


###  Audio

- **SFX_**  
  - Converted to mono  
  - Optimized for fast playback  
  - Lower memory usage  

- **Music_**  
  - Streaming enabled  
  - Higher quality compression  
  - Optimized for longer playback  
