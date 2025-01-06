# MansionEscape

**MansionEscape** is a first-person, exploratory puzzle game set in a haunted mansion where the player must navigate through each room in the mansion, solve puzzles, collect items, and find the hidden key to escape each room.

## Table of Contents

- [Game Concept](#game-concept)
- [Features (For Developers)](#features)
- [How to Play (For Players)](#how-to-play)
- [Requirements (For Developers)](#requirements)
- [Setup Instructions (For Developers)](#setup-instructions)
- [Asset References](#asset-references)


## Game Concept

### Puzzle Game

**Genre:**

- Adventure
- Narrative
- Puzzle
- Survival 

**Characteristics:**

- Exploration
- Control
- Choices
- Experimentation
- Problem-solving

**Central Gameplay Features**

- Collectibles and inventory systems
- Puzzles
- Narrative

## Features 

### Player Mechanics

**1. Movement:**

- Implement player movement (Arrow Keys or WASD)
- Confine player to room.
- Implement basic movement animation.

**2. Interaction:**

- Player can interact with objects in the scene (desks, paintings, etc.)
- Player receives a description of the interactive objects when near them.
- Player can pick up objects.

**3. Inventory:**

- Basic inventory system to collect and store objects in the room.
- Display of the inventory system.
- Player can inspect items in the inventory (brief description).

**4. Puzzle Solving:**

- Player can collect items from within each room (keys, puzzle pieces, notes etc.)
- Objects interact with other objects ( a player can pick up a key from the drawer of a desk).
- Clear feedback on if a puzzle is successful or not.
- Puzzles may be riddles that the player has to solve.

### Environment

The player navigates through a series of distinct rooms that collectively form a mysterious and haunting mansion. Each room features unique characteristics and challenges:

**1. Room Types:**

The mansion consists of various room types, each with its own theme and purpose:

- Ground Floor Hallway (Tutorial)
- Dining Room (Level 1)
- Kitchen & Pantry (Level 2)
- Living Room (Level 3)
- First Floor Hallway (Level 4)
- Study (Level 5)
- Twins Bedroom (Level 6)
- Master Bedroom (Level 7)

**2. Room Design:** Each room is enclosed within four walls, providing a sense of confinement while allowing players to view their surroundings freely.

**3. Blocked Exits:**

Each room contains an exit that may be locked or even hidden, adding to the game’s exploratory and puzzle-solving elements.

**4. Interactable Objects:**

Players can interact with a variety of objects scattered throughout the rooms, including:

- Furniture (tables, chairs, etc.)
- Storage units (drawers, cupboards, chests)
- Decorative items (pictures, lamps, etc.)
- Keys
- Locked Door/Exit

### Gameplay

Each room has designated puzzle designs (Larger puzzle for the exit solved by smaller puzzles)

**1. Code Puzzles:**

An object might contain a puzzle for the player to solve which rewards them with a key or clue once successfully solved.

**2. Red Herring:**

Puzzles or objects which contain unusable/incorrect clues to mislead the player.

### User Interface (UI)

**1. Inventory:**

- Clickable Icon: A visible icon on the UI menu that, when clicked, opens the inventory.

- Grid View: Once opened, the inventory will display a grid view showcasing the collectable items the player has gathered.

**2. Interaction Prompts:**

Near objects that the player can interact with, simple prompts will appear on screen:

- Interact Prompt: “Press E to interact.”
- Pickup Prompt: “Press E to pick up.”

**3. Puzzle Feedback:**

Display messages to player for updating puzzle success:

- “Solved”, “Correct”, “Success!”

- “Wrong Combination”, “Incorrect!”, “Note Quite”

**4. Hints System: (Future Idea)**

Players will be able to use hints in each room to aid in solving puzzles:

- Hint Icon: A permanent, visible icon on the screen that allows players to access hints.

- Limited Usage: Each room allows for 3 hints, which do not reset or reload after use.

**5. Timer: (Future Idea)**

A countdown timer can be displayed as part of the UI, which add pressure and difficulty to the player’s progress:

- If timer runs out and player does not successfully exit the room, a poltergiest event will take place and player loses a life and must restart the level.

- Allow players to toggle it on/off in the settings for more casual playthroughs.

**6. Audio Cues:**

- Subtle sound effects when the player interacts with objects (e.g., doors creaking, drawers opening, puzzle success/failure etc.).

**7. Visual Indicators:**

- Objects that can be interacted with (doors, drawers, key items etc.) highlight when the player is near them.

- Progress Bars to show the players progress through the mansion and their current location in the mansion.

**8. Game Tutorial:**
Short, simple tutorial explaining basic control functions and objectives.

## How to Play

**GAME LINK:** https://mansionescape.github.io/MansionEscape/build/index.html

### Storyline

You awaken inside a dark and isolated mansion with no memory of how you got there. Every room is locked, and the only way out is to solve the puzzles hidden within. The mansion is eerily quiet, but it feels as though someone or something is watching your every move...

Each room reveals fragments of the mansion’s dark past, but nothing is ever clear. Your only goal is to escape. But with each puzzle you solve, the mansion pulls you further in, and you start to wonder if escape is even possible...

### Goal

The objective is to escape each room of the haunted mansion. Every room has an exit, but it may be hidden or locked. Solve the puzzles within the room to reveal or unlock the exit and move on to the next room until you escape the Mansion completely.

### Controls

- W or Up Arrow - Move upwards.
- S or Down Arrow - Move downwards.
- D or Right Arrow - Move to the right.
- A or Left Arrow - Move to the left.
- Mouse - Buttons and Puzzles
- E - Interact with highlighted objects and unlock doors.

### Gameplay Instructions

**1. Explore the Room:** Investigate the room by moving around and examining objects. Some items or objects are hidden, and interactive objects will highlight (turn green) when you're close enough to interact with them. You will also see a description of the object (what it is) and a prompt to interact with it (E.g. press E).

**2. Solve Puzzles:** To unlock the exit, you will need to solve puzzles. These could involve finding hidden objects, re-arranging items, unlocking chests, or deciphering clues scattered around the room.

**3. Collect and Use Items:** When you find items (like keys or tools), press the button to add them to your inventory so you dont lose them. Use items from your inventory to unlock doors or solve specific puzzles.

**4. Exit the Room:** Once you've solved the room’s puzzles, the exit will either unlock when you are close to it or become visible. Approach the exit to move on to the next room.


## Requirements 

- A compatible computer (Windows, macOS, or Linux)
- Any version of Unity 2023 (you should also install the WebGL platform module)
- Unity Hub (for managing unity projects)
- Github Desktop (for git and version control)
- Visual Studio Code (for code editing)

## Setup Instructions

### 1. Clone the Repository

To get started, clone the repository to your local machine. Open your terminal or Git bash and change to the directory on your local machine where you would like to save the repository (using `cd` command) and run the following command:

```bash
git clone https://github.com/MansionEscape/MansionEscape.git

```

Alternatively, you can use Github Desktop:

- open Github Desktop
- File -> Clone Repository (Shortcut: Ctr+Shift+O)
- paste the url of the repository: https://github.com/MansionEscape/MansionEscape.git
- select a directory to store the repository

For more information on how to clone a repository check out the following GitHub Doc: https://docs.github.com/en/repositories/creating-and-managing-repositories/cloning-a-repository

### 2. Unity

After cloning the repository, open Unity Hub, and open a new project by navigating to the Projects tab and select Add and then 'Add project from disk':

![Projects -> Add -> Add project from disk](image.png)

After opening the project in unity, you should be able to see the following view with its different sections:

![unity editor](image-1.png)

- **The GameView:** WhenthePlaybutton is pressed, allows to see the game as it’ll be built and playtested (interacting with e.g. keyboard and mouse for sending input to the game, if this is setup).
- **The Hierarchy View:** Complete list of every object in the current scene. This is a hierarchical view: objects can be nested within others for better organization.
- **The Project Panel:** Contains all assets (animations, models, scripts, sound, audio, prefabs etc.) that can be used in the game.
- **The Inspector Panel:** Allows to add, modify and remove components from all objects in the scene.
- **The Console:** Where error and debug textual output will be displayed.
- **File → Build Settings...**, allows to build the game for any platform. You can also find the Unity3D Hotkeys useful: http://docs.unity3d.com/Manual/UnityHotkeys.html


## Asset References

1. Bathroom Props. Link: https://assetstore.unity.com/packages/3d/props/furniture/bathroom-props-25255
2. Breakable Jars, Vases, Pots. Link: https://assetstore.unity.com/packages/3d/props/furniture/breakable-jars-vases-pots-280906
3. Chair and Armchair. Link: https://assetstore.unity.com/packages/3d/props/furniture/chair-and-armchair-26360
4. Character Pack: Free Sample. Link: https://assetstore.unity.com/packages/3d/characters/humanoids/character-pack-free-sample-79870
5. Chest of Drawers. Link: https://assetstore.unity.com/packages/3d/props/furniture/chest-of-drawers-58835
6. Cheval Mirror. Link: https://assetstore.unity.com/packages/3d/props/cheval-mirror-259424
7. Classic Picture Frame. Link: https://assetstore.unity.com/packages/3d/props/furniture/classic-picture-frame-59038
8. Customizable Kitchen Pack. Link: https://assetstore.unity.com/packages/3d/props/interior/customizable-kitchen-pack-22269
9. Cutlery Silverware PBR. Link: https://assetstore.unity.com/packages/3d/props/food/cutlery-silverware-pbr-106932
10. Dark Fantasy Kit [Lite]. Link: https://assetstore.unity.com/packages/3d/environments/fantasy/dark-fantasy-kit-lite-127925
11. Dining Set. Linl: https://assetstore.unity.com/packages/3d/props/interior/dining-set-37029
12. Fantasy interior props free. link: https://assetstore.unity.com/packages/3d/props/interior/fantasy-interior-props-free-205233
13. Fantasy Map Assets Pack Lite Link: https://assetstore.unity.com/packages/2d/gui/fantasy-map-assets-pack-lite-259318
14. Free 8 Spooky Tracks Music Pack. Link: https://assetstore.unity.com/packages/audio/music/free-8-spooky-tracks-music-pack-275541
15. Free Fire VFX. link: https://assetstore.unity.com/packages/vfx/particles/fire-explosions/free-fire-vfx-266227
16. Free Pixel Food. Link: https://assetstore.unity.com/packages/2d/environments/free-pixel-food-113523
17. Free Playing Cards Pack. Link: https://assetstore.unity.com/packages/3d/props/tools/free-playing-cards-pack-154780
18. Free Rug Pack. Link: https://assetstore.unity.com/packages/3d/props/interior/free-rug-pack-118178
19. Furniture FREE Pack. Link: https://assetstore.unity.com/packages/3d/props/furniture/furniture-free-pack-192628
20. Ghost character Free. Link: https://assetstore.unity.com/packages/3d/characters/creatures/ghost-character-free-267003
21. Low-Poly Wooden Kid's Toys. Link: https://assetstore.unity.com/packages/3d/props/interior/low-poly-wooden-kid-s-toys-162585
22. Medieval props. Link: https://assetstore.unity.com/packages/3d/props/medieval-props-41540
23. Medieval Tavern Pack. Link: https://assetstore.unity.com/packages/3d/props/furniture/medieval-tavern-pack-112546
24. Old Bathroom Objects. Link: https://assetstore.unity.com/packages/3d/props/interior/old-bathroom-objects-120069
25. Pictures. Link: https://assetstore.unity.com/packages/3d/props/interior/pictures-88112
26. QA Books. Link: https://assetstore.unity.com/packages/3d/props/interior/qa-books-115415
27. Retro Style Wooden Boxes. link: https://assetstore.unity.com/packages/3d/props/interior/retro-style-wooden-boxes-206753
28. RPG Food Props DEMO. link: https://assetstore.unity.com/packages/3d/props/food/rpg-food-props-demo-248712
29. Rust Key. Link: https://assetstore.unity.com/packages/3d/props/rust-key-167590
30. Rusty Pack Models. Link: https://assetstore.unity.com/packages/3d/props/rusty-pack-models-14412
31. SteampunkUI. Link: https://assetstore.unity.com/packages/2d/gui/icons/steampunkui-238976
32. Ultimate Low Poly Dungeon. Link: https://assetstore.unity.com/packages/3d/environments/dungeons/ultimate-low-poly-dungeon-143535


