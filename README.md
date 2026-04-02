# Game Name + Logo
[Name is in progress] [Logo is in progress]

## Overview
This game is a skill-based, top-down action puzzler where the player is fixed at the center of the screen and rotates to face the mouse cursor. The objective is to eliminate all enemies on a grid using a limited number of shuriken throws. Shurikens bounce off walls and obstacles, allowing for creative angles and chain reactions. Success depends on precision, planning, and making the most out of every throw to clear each level as efficiently as possible.

(Insert Gameplay Video or GIF here)

[GameName] was made for a college assignment in collaboration with the [**Diagra 2026 Conference**](https://www.digraconference2026.com/).

### Install/Play:
[**Itch.io**](https://dgeoe.itch.io/) (Works on Desktop & Mobile!)

## Script Breakdown
Scripting was kept both simple and short during the project, since the main focus was on creating multiple puzzle levels, it was important to have code that was readable for all team members and easily alterable. 

• **RotationPivot.cs** <br/>
Rotates the player to face the mouse or touch position by raycasting onto the ground layer and aligning the player’s forward direction toward the hit point.

• **Throw.cs** <br/>
Handles player input for throwing shurikens. When the player presses input, the throw point is shown (activating AimLine.cs); on release, a projectile is fired in the forward direction using a raycast to determine its path.

• **ProjectileLogic.cs** <br/>
This script manages the behavior of each thrown shuriken. Once initialized with a direction, the projectile continuously moves forward at a fixed speed using FixedUpdate. It handles different collision outcomes based on object tags: enemies take damage while allowing the shuriken to pass through, bounce surfaces reflect the projectile’s direction using the collision normal, and other objects like walls cause the projectile to be destroyed. 

### Creating a dashed line renderer for Aiming

(Photo of Line)

• **AimLine.cs** <br/>
Visualize the projected path of a thrown shuriken, including multiple ricochets. Starting from the throw point, it repeatedly casts rays in the current direction, updating each segment of the line to match where the projectile would travel and bounce. <br/>
(Photo of Bounce) <br/> <br/>
When hitting surfaces tagged as “Bounce,” the direction is reflected to simulate ricochet behavior, continuing up to a set number of bounces. To achieve this, I first had to set a total number of positions in the line renderers index. Each time you hit bounce, you iterate through all the unused segments in the index and fill them with the final point to keep the line consistent.<br/>
(Segment of code) <br/> <br/>
The line also changes color depending on what it intersects, such as enemies, barrels, etc, providing immediate visual feedback to the player.  <br/>
(Photo of Color Changing)
<br/> <br/>
## Collaborators:
[**Joe O'Shea**](https://dgeoe.itch.io/) - Programmer <br/>
[**Cian Fitzpatrick**](https://rockyhorrorfreakshow.itch.io/) - Fill in task <br/>
[**Daniel Moure**](https://dmotz.itch.io/) - Fill in task <br/>
[**David Fatoye**](https://iddav11.itch.io/) - Fill in task <br/>
