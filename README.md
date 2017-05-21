# True-Collector
3D Unity game to collect items around a world by completing puzzles.

# Programming Course PRG08
This game has been made in the Unity Engine and is required to run it.

# Important Notes Halfway Review 23/5/2017
baseUrl -> /Assets/scripts/
- Interfaces are located in the baseURL/Interfaces/ folder
- Static utility methods are used in the Singleton.cs file which is located in baseURL/Singleton/ folder
- Singleton Pattern can be found in the baseURL/Singleton/ folder and is also used in the game.cs file in the baseURL folder
- Strategy Pattern can be found in the baseURL/Behaviours/ folder and is also used in the player.cs file in the baseURL folder
- The remaining requirements for this halfway review can be found in most if not all script files

# Installation Guide
TBA

# Current functionality included are:
- Equipment System
  - Weapon
  - Offhand
  - Helmet
  - Armour
  - Boots
- Inventory System
- 3D World
- Movement
  - Alternating between running and walking is possible
- Player-Object interaction

# Equipment System Character Stats
- Health
- Power
- Speed
- Armour
- Ammo

# Patch Notes
# 21/5/2017
- Fixed a bug where the player could trip and shake the camera uncontrollably
- Player can now alternate between Running an Walking by pressing 'N'
  - Strategy Pattern (Behaviour)
- Added Game as a Game Object so this can keep track of achievements later
  - Singleton Pattern
- Added base for the player dying


# 18/5/2017
- Flashlight
- Base for shooting with a gun
  - Ammo
  - Reloading
- Throwing Rock

# 16/5/2017
- Movement
- World Interaction
  - Opening a door
- Collision
- Added textures to environment behind door
  - Small mountains
  - Grass Textures
