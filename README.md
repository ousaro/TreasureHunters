# TreasureHunters

### Title: Captain Clown Nose's Quest for the Golden Skull

#### **Project Overview:**
**Objective:**
Develop a 2D platformer game where each class handles a single responsibility. The game features Captain Clown Nose on an adventurous quest to find the legendary Golden Skull. Implement clean code practices with a modular design, and incorporate the Observer Pattern for basic usage.

**Scenes:**
1. Menu Scene
2. The Palm Tree Island Scene
3. The Pirate Ship Showdown Scene

**Code Complexity:**
Low to Medium

**Assets:**
- Basic player sprite
- Enemy sprites (Crabby, Fierce Tooth, Pink Star)
- Tileset for platforming
- Background images of a palm tree island and a pirate ship
- Traps: Cannons, Seashells, and Totems

---

### **Story**

#### **Scene 1: The Menu**
**Setting:**
The game begins on a simple, interactive menu screen. The background features a pirate ship anchored near a palm tree island, setting the stage for the adventure.

**Objective:**
Allow players to start the game, view options, and read about the story.

**Key Elements:**
- **Start Game Button:** Begins the adventure.
- **Options Button:** Allows players to adjust game settings.(sound settings, credits)
- **Story Button:** Provides a brief introduction to Captain Clown Nose and his quest for the Golden Skull.

**Narrative Introduction:**
Welcome to Captain Clown Nose's Quest for the Golden Skull! Our whimsical yet brave pirate hero, known for his bright red clown nose, has discovered a map leading to a legendary treasure. 
Join him on this thrilling adventure as he navigates through the dangers of the palm tree island and battles his twin brother in search of the Golden Skull.

---

#### **Scene 2: The Palm Tree Island**
**Setting:**
Captain Clown Nose stands on the shore of a palm tree island. The island is filled with various platforms and hazards to navigate.

**Objective:**
Guide Captain Clown Nose through the island to collect map fragments and find the Golden Skull.

**Key Elements:**
- **PlayerController:** Handles player movement and actions.
- **EnemyController:** Manages enemy behaviors for:
  - **Crabby:** A giant, aggressive crab that stretches its claws to attack.
  - **Fierce Tooth:** A bizarre creature with a body and a huge tooth on its face that bites with its big tooth.
  - **Pink Star:** A deceptive starfish that rotates and attack Clown Nose.
- **GameManager:** Oversees game state, level progression, and map fragment collection.

**Gameplay Mechanics:**
- **Platforming:** Run and jump across gaps and obstacles.
- **Avoiding Hazards:** Navigate around traps and environmental hazards.
  - **Cannons:** Fire periodically, requiring precise timing to avoid.
  - **Seashells:** When stepped near to them, they shoot a perl, acting as a trap.
  - **Totems:** Totems shoot woods spikes.
- **Collectibles:** Gather map fragments, health potions, and extra lives.

**Climax:**
At the end of the scene, Captain Clown Nose finds the complete map and an ancient compass pointing to the Golden Skull's location. 
Suddenly, his long-lost twin brother, Captain Dark Nose, appears, revealing that he too is after the Golden Skull. A confrontation ensues, setting up the final showdown.

---

#### **Scene 3: The Pirate Ship Showdown**
**Setting:**
Captain Clown Nose and Captain Dark Nose's final showdown takes place aboard a grand pirate ship anchored off the island. The ship is filled with rigging, cannons, and treasure chests.

**Objective:**
Defeat Captain Dark Nose in an epic showdown to claim the Golden Skull.

**Key Elements:**
- **Boss Fight:** Use running and jumping skills to defeat Captain Dark Nose. He uses his own tricks and traps to make the fight challenging.
- **Final Collectible:** Secure the Golden Skull after defeating Captain Dark Nose.

**Climax:**
After a fierce battle, Captain Clown Nose emerges victorious. He claims the Golden Skull as the ship begins to show signs of an imminent storm. He must make a daring escape back to the island.

---

#### **Epilogue:**
Captain Clown Nose sets sail into the sunset on his ship, the Golden Skull safely aboard. He looks back at the island with a triumphant grin, ready for his next adventure. The screen fades to black with the words, "The End... or is it?" hinting at future adventures.

---

### **Implementation Plan**

**Classes:**
1. **PlayerController:** Manages player movement and actions.
2. **EnemyController:** Handles behavior for Crabby, Fierce Tooth, and Pink Star.
3. **GameManager:** Oversees game state, level progression, and manages collectibles and hazards.
4. **MenuManager:** Manages the menu scene and transitions to the main game scene.
5. **TrapManager:** Controls the behavior of traps (Cannons, Seashells, Totems).
6. **Observer Pattern:** Used for event handling, such as collectible pickups and enemy defeats.
7. **HealthManager**Manage the health system for the characters.
