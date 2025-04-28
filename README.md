Fighterboi
Second attempt at my fighter 1v1 game, now with Unity!
Overview
Fighterboi is a 2D 1v1 fighter game developed using Unity 6, featuring two cute animal characters (a cat and a dog) with unique controls for movement, jumping, and projectile throwing. The game includes a menu system with a main menu, pause menu, and options menu, random background maps (e.g., Temple Jungle, Castle), and a health system with a win condition. It is in active development for a university project, with a focus on implementing core gameplay mechanics and exploring Unity’s Entity Component System (ECS).
Tech Stack

Engine: Unity 6 (latest version)
Language: C#
UI: Unity UI System with TextMeshPro
Physics: Unity 2D Physics
Assets:
Sprites: Custom .png files for characters, projectiles, and backgrounds
Prefabs: Projectile and background objects

Version Control: Git (this repository)

Features

Gameplay:
Two-player fighting with movement, jumping, and projectile throwing.
Player 1 Controls:
Move: A (left), D (right)
Jump: W
Throw Projectile: Space

Player 2 Controls:
Move: Left Arrow (left), Right Arrow (right)
Jump: Up Arrow
Throw Projectile: Right Shift

Health System:
Each player starts with 100 HP.
Projectiles deal 10 damage per hit.
Health bars with a lerping backdrop effect for smooth visuals.

Win Condition:
When a player’s HP reaches 0, the other player wins, and the game returns to the MainMenu scene.

Menus:
Main menu (Start, Options, Exit).
Pause menu (Resume, Options, Exit to Main Menu), toggled with F1.
Options menu with sliders for Master Volume, Music Volume, SFX Volume, and Brightness.

Levels:
Randomly loaded backgrounds (Temple Jungle, Castle, etc.).

Graphics:
Pixel-art style with dynamic backgrounds.

Getting Started

Clone the repository: git clone <repository-url>
Open the project in Unity 6.
Ensure the TextMeshPro package is imported (via Package Manager).
Set up Build Profiles with MainMenu (scene 0) and Game (scene 1).
Play to start in the MainMenu scene.

Current Status
What’s Working

Gameplay Mechanics:
Movement, jumping, and projectile throwing for both players.
Projectiles correctly deal 10 damage to the opponent (not the shooter).
Health system fully functional: HP decreases, health bars update with a lerping backdrop, and the backdrop snaps to 0 when HP is 0.

Win Condition:
When a player’s HP reaches 0, the game declares the winner and returns to the MainMenu scene.

UI and Menus:
Countdown timer (3, 2, 1, Fight!) at the start of the game.
Pause menu toggles with F1 and includes Resume, Options, and Exit to Main Menu buttons.
Options menu sliders update GameState settings (volume, brightness).
Player names are displayed above health bars (set via GameState).

Levels:
Random background loading (Temple Jungle, Castle, etc.) works as expected.

What’s Not Working

Melee Attacks:
Melee attacks (planned for Q for Player 1 and P for Player 2) are not yet implemented.

Win Screen:
A proper win screen is not implemented; the game currently returns to the MainMenu scene after a win.

Power-Ups:
Power-ups tied to level types (e.g., speed boost, extra damage) are not implemented.

Sound Effects:
No sound effects for actions (e.g., jumping, throwing projectiles, taking damage).

What’s Partially Working

Scene Loading:
The game now starts in the MainMenu scene, but there may still be occasional inconsistencies in the main menu display (e.g., UI elements not aligning properly or fighters activating prematurely in the Game scene during transitions).

ECS Integration:
The game uses an ECS-inspired OOP approach, where GameObjects act as entities, MonoBehaviours (e.g., Fighter, ProjectileComponent) act as components, and MonoBehaviour methods (e.g., Update, OnCollisionEnter2D) act as systems. This provides modularity and separation of concerns, aligning with ECS principles.
However, it does not yet implement Unity’s pure ECS framework (DOTS) for projectiles or other systems. The current GameObject-based projectile system works but may not scale well for many projectiles due to instantiation/destruction overhead.

Current Development Goals

Immediate Goals:
Implement melee attacks for Q (Player 1) and P (Player 2) to add variety to combat.
Create a proper win screen to display the winner, with options to replay or return to the main menu.
Design and implement power-up mechanics tied to level types (e.g., speed boost in Temple Jungle, extra damage in Castle).
Add sound effects for key actions (jumping, projectile throwing, taking damage, winning).

Long-Term Goals:
Transition to Unity 6’s pure Entity Component System (ECS) using the Data-Oriented Tech Stack (DOTS) for projectiles and potentially fighters, improving performance for scenarios with many entities.
Enhance UI with animations and improved styling (e.g., animated health bar transitions, menu transitions).
Fix any remaining scene loading inconsistencies to ensure the main menu displays correctly and fighters only activate in the Game scene.

Contributing
This is a personal project and not open source at this time. Contributions are not accepted.
Contact
For questions, reach out to the developer.
