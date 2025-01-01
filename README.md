# FlappyBird-Clone

Welcome to the **FlappyBird-Clone** project! This repository contains a Unity-based recreation of the classic Flappy Bird game. It provides a simple and customizable implementation suitable for learning or extending further.

## Table of Contents
- [Features](#features)
- [Installation](#installation)
- [Gameplay](#gameplay)
- [Project Structure](#project-structure)
- [Assets](#assets)
- [Known Issues](#known-issues)
- [Future Improvements](#future-improvements)
- [License](#license)

---

## Features
- Classic Flappy Bird gameplay mechanics
- Basic scoring and leaderboard system
- Main menu and restart button
- Smooth animations and sound effects
- Mobile and desktop compatibility (with minor issues on mobile leaderboard system)

## Installation
1. Clone the repository:
   ```bash
   git clone https://github.com/batuhancetinkaya1/FlappyBird-Clone.git
   ```
2. Open the project in Unity (tested with Unity version [Add Unity version used here]).
3. Play the game by opening the `Main Scene` and pressing the Play button in the Unity Editor.

You can also play the game directly on [itch.io](https://batuhancetinkaya.itch.io/flappy-bird-clone).

## Gameplay
The goal of the game is to navigate the bird through gaps between pipes without hitting them or falling. The game ends when the bird collides with an obstacle or the ground.

### Controls
- Press `Space` or click the screen to make the bird flap its wings.

## Project Structure
Below is a high-level overview of the project structure:

### [Assets](https://github.com/batuhancetinkaya1/FlappyBird-Clone/tree/main/Assets)
Contains all the resources required for the game, such as scripts, prefabs, and sound effects.

### [Scripts](https://github.com/batuhancetinkaya1/FlappyBird-Clone/tree/main/Assets/Scripts)
Includes C# scripts for handling game mechanics:
- **BirdController.cs**: Controls bird movement and collision.
- **PipeSpawner.cs**: Spawns pipes dynamically.
- **GameManager.cs**: Manages game state and logic.

### [Prefabs](https://github.com/batuhancetinkaya1/FlappyBird-Clone/tree/main/Assets/Prefabs)
Contains reusable game objects like the bird and pipes.

### [Sound Effects](https://github.com/batuhancetinkaya1/FlappyBird-Clone/tree/main/Assets/Sound%20Effects)
Audio files for enhancing the gameplay experience (e.g., flap and collision sounds).

## Known Issues
- The visual keyboard for entering leaderboard data does not appear on mobile devices but works fine on desktop.
- The game otherwise functions smoothly on mobile platforms.

## Future Improvements
- Fix the visual keyboard issue on mobile devices
- Enhance the leaderboard system with online capabilities
- Include additional levels or difficulty settings
- Enhance graphics and animations

## License
This project is open source and available under the [MIT License](LICENSE). Feel free to use, modify, and distribute the code as per the license terms.

---

Happy coding! If you encounter any issues or have suggestions, please open an issue in the repository or reach out. 🚀
