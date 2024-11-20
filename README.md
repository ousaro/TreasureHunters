# Treasure Hunter: A Unity2D Game

## Overview  
**Treasure Hunter** is a simple Unity2D game designed to explore and learn about essential game development concepts and software design patterns. It is a single-scene game with the goal of collecting treasures while learning to organize a project effectively.

<img src="./screenshot.JPG" alt="Preview Image" width="500" style="display: inline-block; margin-right: 20px;"/>

### Features:  
- **Design Patterns**: Demonstrates the use of Singleton, State, and Observer patterns.  
- **Data Separation**: Implements ScriptableObjects as data holders to decouple logic from data.  
- **Cinemachine Integration**: Utilizes Cinemachine’s Confiner2D for camera boundaries.  
- **Animator Techniques**: Explores Blend Trees for smooth character animations.  
- **Project Organization**: Adopts best practices for project structuring.  

---


## Game Preview
Watch a preview of the game [here](https://youtu.be/gCIBulQ_Vxg).


---

## Learning Objectives  

### Design Patterns  
1. **Singleton Pattern**:  
   - Manages game-wide single-instance components like GameManager or AudioManager or Player.  
2. **State Pattern**:  
   - Handles different states of the player (Idle, Running, Attacking, Jumping, etc.).  
3. **Observer Pattern**:  
   - Implements a notification system between objects (e.g., player damage  notify the UI, or a damagabel instance get detected by the player).  

### Scriptable Objects  
- **Purpose**: Simplifies data management and allows easy adjustments without modifying code.  
- **Usage**: Stores player stats and enemy stats.

### Unity Components  
1. **Cinemachine Confiner2D**:  
   - Ensures the camera stays within predefined boundaries of the game world.  
2. **Animator Blend Tree**:  
   - Smoothly transitions between animations, such as Jumping and Falling.  

### Project Organization  
- **Folder Structure**: Clear separation of assets:  
  - `Scripts/`: Contains all logic and patterns.  
  - `Assets/`: Stores game graphics.  
  - `Prefabs/`: Game objects with preset configurations.  
  - `Animations/`: Animator controllers and Blend Trees.  
  - `Scenes/`: Contains the main game scene.  
  - `UI/`: For UI elements

---

## How to Play  
1. Launch the game in Unity.  
2. Navigate the player character around the map to collect treasures.  
3. Watch as the camera dynamically adjusts and transitions smoothly across boundaries.  

---

## Technical Insights  
- **Cinemachine Confiner2D**: Maintains immersion by keeping the player in view while respecting the level boundaries.  
- **Blend Trees**: Provides fluid and responsive animations for player movement.  
- **ScriptableObjects**: Streamlines game balancing by allowing data tweaking in the Unity editor.

---

## Installation and Setup  
1. Clone the repository:  
   ```bash  
   git clone https://github.com/your-repo/treasure-hunter.git  
   ```  
2. Open the project in Unity.  
3. Ensure you have installed **Cinemachine** and **TextMeshPro** from the Unity Package Manager.  
4. Play the game from the `MenuScene`.

---

## Acknowledgments  
This project was inspired by the need to understand and implement foundational principles of game development and design patterns.  

Happy coding! 🎮  