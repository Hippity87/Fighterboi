# Fighterboi
second attempt at my fighter 1v1 game, now with unity!

## Overview
Fighterboi is a 2D 1v1 fighter game developed using Unity 6, featuring two cute animal characters (a cat and a dog) with unique controls for movement, jumping, attacking, and projectile throwing. The game includes a menu system with a main menu and pause menu, random background maps (e.g., Temple Jungle, Castle), and is in active development for a university project.

## Tech Stack
- **Engine**: Unity 6 (latest version)
- **Language**: C#
- **UI**: Unity UI System with TextMeshPro
- **Physics**: Unity 2D Physics
- **Assets**:
  - Sprites: Custom `.png` files for characters, projectiles, and backgrounds
  - Prefabs: Projectile and background objects
- **Version Control**: Git (this repository)


## Features
- **Gameplay**: Two-player fighting with jump, attack, and projectile mechanics.
- **Menus**: Main menu (Start, Options, Exit) and pause menu (Resume, Options, Exit to Main Menu).
- **Levels**: Randomly loaded backgrounds (Temple Jungle, Castle, etc.) with potential for power-ups.
- **Graphics**: Pixel-art style with dynamic backgrounds.

## Getting Started
1. Clone the repository: `git clone <repository-url>`
2. Open the project in Unity 6.
3. Ensure the TextMeshPro package is imported (via Package Manager).
4. Set up Build Profiles with `MainMenu` (scene 0) and `Game` (scene 1).
5. Play to start in the MainMenu scene.

## Current Status
- The game loads random backgrounds successfully.
- Menu system is implemented but has issues: starts in `Game` scene despite `MainMenu` redirect.
- Fighters activate in `Game`, but main menu display is inconsistent.

## Future Plans
- Fix scene loading to start in `MainMenu`.
- Add power-ups tied to level types.
- Enhance UI with animations or styling.
- Integrate ECS for projectiles (university project goal).

## Contributing
This is a personal project and not open source at this time. Contributions are not accepted.

## Contact
For questions, reach out to the developer.

---
