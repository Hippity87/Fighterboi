# Fighterboi

Second attempt at my fighter 1v1 game, now with Unity!

## Overview

Fighterboi is a 2D 1v1 fighter game developed using Unity 6, featuring two cute animal characters (a cat and a dog) with unique controls for movement, jumping, and projectile throwing. The game includes a menu system with a main menu, character selection, a loading screen, pause menu, and options menu, random background maps (e.g., Temple Jungle, Castle), and a health system with a win condition. It is in active development for a university project, with a focus on implementing core gameplay mechanics and exploring Unity’s Entity Component System (ECS).

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

- **Gameplay**:
  - Two-player fighting with movement, jumping, and projectile throwing.
  - **Player 1 Controls**:
    - Move: `A` (left), `D` (right)
    - Jump: `W`
    - Throw Projectile: `Space`
  - **Player 2 Controls**:
    - Move: `Left Arrow` (left), `Right Arrow` (right)
    - Jump: `Up Arrow`
    - Throw Projectile: `Right Shift`
- **Health System**:
  - Each player starts with 100 HP.
  - Projectiles deal 10 damage per hit.
  - Health bars with a lerping backdrop effect for smooth visuals.
- **Win Condition**:
  - When a player’s HP reaches 0, the other player wins, and the game returns to the `MainMenu` scene.
- **Menus and Scenes**:
  - **Main Menu**: Start the game, access options, or exit.
  - **Character Select**: Choose characters (e.g., cat or dog) for Player 1 and Player 2.
  - **Loading Screen**: Displays a transition screen before loading the gameplay scene.
  - **Pause Menu**: Toggled with `F1` during gameplay, with Resume, Options, and Exit to Main Menu buttons.
  - **Options Menu**: Sliders for Master Volume, Music Volume, SFX Volume, and Brightness, accessible from the main menu or pause menu.
- **Levels**:
  - The `Game` scene dynamically loads random backgrounds (e.g., Temple Jungle, Castle) for visual variety.
- **Graphics**:
  - Pixel-art style with dynamic backgrounds.

## Getting Started

1. Clone the repository: `git clone <repository-url>`
2. Open the project in Unity 6.
3. Ensure the TextMeshPro package is imported (via Package Manager).
4. Verify the scene setup in Build Settings:
   - Go to `File > Build Settings` in Unity.
   - Ensure the scenes are added in the following order:
     - Index 0: `Assets/Scenes/MainMenu.unity` (entry point with the main menu).
     - Index 1: `Assets/Scenes/CharacterSelect.unity` (character selection for players).
     - Index 2: `Assets/Scenes/LoadingScreen.unity` (transition screen before gameplay).
     - Index 3: `Assets/Scenes/Game.unity` (main gameplay scene with dynamic backgrounds).
   - If the scenes are not in this order, drag them to reorder, or add them by clicking "Add Open Scenes" after opening each scene in the Editor.
5. Run the game:
   - Press the Play button in the Unity Editor to start the game. It should begin in the `MainMenu` scene (index 0), where you can select "Start" to proceed to the `CharacterSelect` scene, followed by the `LoadingScreen`, and finally the `Game` scene to begin playing.
   - If the game does not start in the `MainMenu` scene, double-check the Build Settings to ensure `MainMenu.unity` is at index 0.
   - To test a specific scene (e.g., `Game` scene directly), open the desired scene (e.g., `Assets/Scenes/Game.unity`) in the Editor and press Play, but note that the game is designed to start from `MainMenu` for proper initialization of the game flow (e.g., character selection).

## Current Status

### What’s Working

- **Gameplay Mechanics**:
  - Movement, jumping, and projectile throwing for both players.
  - Projectiles correctly deal 10 damage to the opponent (not the shooter).
  - Health system fully functional: HP decreases, health bars update with a lerping backdrop, and the backdrop snaps to 0 when HP is 0.
- **Win Condition**:
  - When a player’s HP reaches 0, the game declares the winner and returns to the `MainMenu` scene.
- **UI and Menus**:
  - Countdown timer (3, 2, 1, Fight!) at the start of the `Game` scene.
  - Pause menu toggles with `F1` in the `Game` scene and includes Resume, Options, and Exit to Main Menu buttons.
  - Options menu sliders update `GameState` settings (volume, brightness).
  - Player names are displayed above health bars (set via `GameState`).
- **Levels**:
  - Random background loading (Temple Jungle, Castle, etc.) works as expected in the `Game` scene.

### What’s Not Working

- **Melee Attacks**:
  - Melee attacks (planned for `Q` for Player 1 and `P` for Player 2) are not yet implemented.
- **Win Screen**:
  - A proper win screen is not implemented; the game currently returns to the `MainMenu` scene after a win.
- **Power-Ups**:
  - Power-ups tied to level types (e.g., speed boost, extra damage) are not implemented.
- **Sound Effects**:
  - No sound effects for actions (e.g., jumping, throwing projectiles, taking damage).

### What’s Partially Working

- **Scene Loading**:
  - The game starts in the `MainMenu` scene and progresses through `CharacterSelect` and `LoadingScreen` to the `Game` scene, but there may be occasional inconsistencies in the main menu or character select display (e.g., UI elements not aligning properly or fighters activating prematurely in the `Game` scene during transitions).
- **ECS Integration**:
  - The game uses an ECS-inspired OOP approach, where GameObjects act as entities, MonoBehaviours (e.g., `Fighter`, `ProjectileComponent`) act as components, and MonoBehaviour methods (e.g., `Update`, `OnCollisionEnter2D`) act as systems. This provides modularity and separation of concerns, aligning with ECS principles.
  - However, it does not yet implement Unity’s pure ECS framework (DOTS) for projectiles or other systems. The current GameObject-based projectile system works but may not scale well for many projectiles due to instantiation/destruction overhead.

## Current Development Goals

- **Immediate Goals**:
  - Implement melee attacks for `Q` (Player 1) and `P` (Player 2) to add variety to combat.
  - Create a proper win screen to display the winner, with options to replay or return to the main menu.
  - Design and implement power-up mechanics tied to level types (e.g., speed boost in Temple Jungle, extra damage in Castle).
  - Add sound effects for key actions (jumping, projectile throwing, taking damage, winning).
- **Long-Term Goals**:
  - Transition to Unity 6’s pure Entity Component System (ECS) using the Data-Oriented Tech Stack (DOTS) for projectiles and potentially fighters, improving performance for scenarios with many entities.
  - Enhance UI with animations and improved styling (e.g., animated health bar transitions, menu transitions).
  - Fix any remaining scene loading inconsistencies to ensure the main menu and character select scenes display correctly, and fighters only activate in the `Game` scene.

## Contributing

This is a personal project and not open source at this time. Contributions are not accepted.

## Contact

For questions, reach out to the developer.

---
