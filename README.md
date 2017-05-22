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
You can find the downloadable files at:
- Windows
  - stud.hosted.hr.nl/0876190/games/True-Collector/Windows
- OSX
  - TBA

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
# 22/5/2017
- Temporarily disabled Main Menu background due to responsiveness
- Added a controls page
- Changed player into a 3D human
  - Animations not functional yet
  - Edited physics
- Added functionality for shooting through a gun
  - Bullets not yet visible but colission is
  - Better shooting mechanics
    - Can now support both single firing mode & full automatic
- Added a better visible ammo counter
- Fixed a bug where your weapon ammo could go over its maximum
- Edited 3D world

# 21/5/2017
- Fixed a bug where the player could trip and shake the camera uncontrollably
- Player can now alternate between Running an Walking by pressing 'N'
  - Strategy Pattern (Behaviour)
- Added Game as a Game Object so this can keep track of achievements later
  - Singleton Pattern
- Added base for the player dying
- Added base for a Main Menu
  - Placeholder Start Button
  - Placeholder Exit Button
  - Placeholder Logo
  - Placeholder Background


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
