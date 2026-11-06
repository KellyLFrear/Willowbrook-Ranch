<img width="1242" height="650" alt="image" src="https://github.com/user-attachments/assets/43da5155-95f6-40b7-9539-727b45a80916" /># 🌾 Willowbrook Ranch

**A cozy, open-world farming simulation built with Unity and C# as a collaborative game development project.**

Explore a small countryside community, grow crops, catch fish, visit local markets, and settle into a daily routine. Willowbrook Ranch connects three distinct areas—the **Farm**, **Town**, and **Beach**—through in-world scene transitions, giving players the freedom to choose how they spend their time.

> **Project status:** Completed course project / playable prototype. Some proposed features remain future improvements.

## 🎮 Gameplay

### 🌱 Farm
- Grow **tomatoes, carrots, mushrooms, and eggplants** across **16 crop plots**.
- Plant seeds, water crops over time, and harvest them when ready.
- Use the farmhouse to advance to the next day.
- Follow an in-game day/night cycle; staying out past midnight triggers a pass-out event and returns the player to the farmhouse at 8:00 AM.

### 🏘️ Town
- Explore a handcrafted small-town environment.
- Speak with **two NPCs** through dialogue interactions.
- Visit the **General Market** to purchase seeds and sell harvested crops for gold.

### 🎣 Beach
- Fish from the dock in a reaction-based minigame with randomized timing and easy/hard fish challenges.
- Buy or sell fish at the **Fish Market**.
- Explore a separate coastal environment connected to the rest of the world.

### 🎒 Other Features
- Inventory and hotbar for seeds, crops, and fish.
- Gold tracking and buying/selling transactions.
- Player movement, sprinting, a following camera, and contextual animations.
- Main menu with **Start Game**, **Credits**, and **Exit** options.
- Audio and on-screen UI for gameplay interactions.

## 🕹️ Controls

| Input | Action |
| --- | --- |
| **WASD** or **Arrow Keys** | Move |
| **Shift** | Sprint |
| **P** | Plant a selected seed near an empty crop plot |
| **Space** | Water a crop; also used during the fishing sequence |
| **H** | Harvest a mature crop |
| **F** | Start fishing when near the fishing dock |
| **Fishing prompts** | Press the displayed keys before time runs out |
| **Mouse** | Select menu and market UI buttons |

Some actions are available only when the player is close to the corresponding object or interaction area.

## 🛠️ Built With

- **Unity** — 3D environments, scenes, prefabs, colliders, UI, and animation
- **C#** — Gameplay logic and interactions
- **Unity Animator** — Character animation state transitions
- **Unity Canvas UI** — Menus, dialogue, markets, and pop-up windows
- **Git & GitHub** — Team version control and collaboration

## ⚙️ Technical Design

The project is organized into **four Unity scenes**: the starting menu, Farm, Town, and Beach. Gameplay systems are divided into scripts for player controls, farming, inventory, fishing, markets, and user interfaces.

- **Crop state management:** Each plot tracks its planting, watering, growth, and harvest state.
- **Trigger-based interactions:** Colliders activate market interfaces and transitions between areas.
- **Shared game state:** Gold and inventory-related values are coordinated across gameplay systems and scenes.
- **Fishing minigame:** Randomized events, timers, and key-input checks determine the outcome.
- **Animation state machine:** The player switches between idle, walking, farming, and interaction animations.
- **Scene organization:** Environment objects are grouped in Unity hierarchies, with reusable assets and prefabs.

### Windows build

1. Obtain the **Willowbrook Ranch executable submission ZIP** from the project team or an available release download.
2. Extract the **entire ZIP** into a folder on your Windows computer.
3. Open the extracted `Willowbrook Ranch Exe Submission` folder.
4. Run **`Willowbrook Ranch.exe`**.
5. Select **Start Game** from the main menu.

**Important:** Keep `Willowbrook Ranch.exe`, `Willowbrook Ranch_Data`, `UnityPlayer.dll`, and the other included runtime files together. The executable alone is not the complete game.

### Unity project

The repository contains Unity project folders such as `Assets`, `Packages`, and `ProjectSettings`. To explore the project in the editor, clone or download the repository, add its folder in Unity Hub, and open it with a compatible Unity Editor version. The exact Unity Editor version should be checked against `ProjectSettings/ProjectVersion.txt` before opening.

## 👥 Development Team

Willowbrook Ranch was developed collaboratively by a five-person team:

| Team member | Primary contributions |
| --- | --- |
| **Kelly Frear** | Project management, repository coordination, Farm and Town environment design, day/night and day-counter systems, gold tracking, markets, sleeping/day transitions |
| **Diego Torres** | Farming mechanics, crop types and growth stages, farming/inventory integration, animation support |
| **Benjamine Medina** | Initial inventory/item systems, Beach environment, sound integration, debugging |
| **Owen Man** | Inventory interface and item integration, buying/selling logic, hotbar improvements |
| **Daniel Pangilinan** | Fishing minigame, NPC dialogue and interactions, level-design assistance |

The project was developed through shared planning, testing, debugging, and GitHub collaboration.

## 🌿 Potential Future Improvements

The final project report identified several features for possible future development:

- NPC friendship meters and relationship progression
- Quests and additional character interactions
- An energy bar and more extensive energy-related gameplay
- More flexible inventory selection and item switching

These are **planned improvements**, not features claimed as complete in the submitted version.

## 🎨 Assets and Acknowledgments

The project uses third-party 3D assets from the **Unity Asset Store**, including low-poly environment, vegetation, pathway, and character packs. It also uses some 2D item imagery sourced from the **Stardew Valley Wiki**, and custom interface artwork commissioned from an external artist.

See the team's final project report for the detailed asset references. Third-party assets remain the property of their respective creators; inclusion in the project does not imply ownership or permission to redistribute them independently.

## 📄 About This Project

Willowbrook Ranch was created as a team course project and was the developers' first full Unity game. The project provided hands-on experience with C# scripting, gameplay system integration, scene construction, UI design, version control, debugging, and collaborative development.
