# Project Overview

This Unity project was developed using **Unity 2021.3.26f1** and demonstrates core gameplay mechanics, including character movement, inventory management, and interaction with enemies. Below is an overview of the key features implemented in the project.

## Key Features

### 1. Map and Character Control
- **Tilemap**: The game scene features a tilemap-based map for the character to navigate.
- **Character Movement**: A virtual joystick in the bottom-left corner of the screen allows the player to control the character's movement.
- **Shooting**: The character can shoot when near an enemy. Ammo is consumed when shooting.
- **Health System**: The character has a health bar that decreases when taking damage. When health reaches zero, the character dies.

### 2. User Interface and Inventory
- **Buttons**: Several key buttons are placed in the bottom-right corner of the screen:
  - **"Shoot"**: Allows the character to shoot.
  - **"Backpack" (Inventory)**: Displays the inventory, showing collected items in individual slots with icons and quantities. If there is only one item in a slot, the quantity is hidden.
  - **Item Deletion**: Players can remove items from the inventory. When clicking on an item, a "delete" button appears, allowing for item removal.
- **Item Collection**: When the character approaches an item on the map, it is automatically added to the inventory.

### 3. Enemies
- **Enemy Spawn**: Three enemies spawn randomly on the map at the start of each session.
- **Enemy Behavior**: Enemies have health bars and will attack the character when within range. Upon defeating an enemy, an item is dropped.
  
### 4. Data Persistence
- **Save System**: The game includes a system to save the player's progress and inventory between sessions without relying on PlayerPrefs.

## Notes
- This project demonstrates my abilities in game development using Unity and shows my approach to structuring game architecture.
