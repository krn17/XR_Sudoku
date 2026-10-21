# 🔢 XR_Sudoku

[![Unity](https://img.shields.io/badge/Unity-2019.4%20LTS-black?logo=unity)](https://unity.com/)
[![C#](https://img.shields.io/badge/C%23-239120?logo=c-sharp&logoColor=white)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](LICENSE)
[![Platform](https://img.shields.io/badge/Platform-Mixed%20Reality%20%2F%20AR-orange)]()

An interactive **3D Mixed Reality (MR) Sudoku puzzle game** built in Unity. Solve number logic puzzles hovering directly in 3D physical space.

---

## 🌟 Key Features

* **🎲 Spatial 3D Sudoku Grid**: Play on an interactive floating 9x9 grid anchored into your environment (`Board.cs`).
* **👆 Interactive 3D Number Input**:
  * Spatial dials and number selection fields designed for XR raycast / pointer interaction (`NumberInput.cs`, `NumberField.cs`).
  * Dynamic piece spawning and number placement feedback (`PieceSpawner.cs`).
* **✅ Real-Time Validation**: Instant verification of row, column, and 3x3 block constraints with solution checking.
* **🏆 Leaderboard & State Persistence**: Track completion times, personal bests, and resume in-progress games (`Saving/`, `Leaderboard/`).
* **🎵 Audio & Spatial UI**: Tactile auditory feedback on number entry and menu selections.

---

## 🏗️ Project Architecture

```text
Assets/
├── Scripts/
│   ├── Board.cs             # Grid data structure, puzzle logic, and validator
│   ├── MenuManager.cs       # Spatial main menu and options
│   ├── UIManager.cs         # Heads-up display, timer, and win screens
│   ├── Saving/              # Game state serialization and saved games
│   └── Leaderboard/         # High score and completion tracking
├── NumberInput.cs           # Spatial interaction dial for number selection
├── NumberField.cs           # 3D cell receiver component
├── PieceSpawner.cs          # Instantiates 3D number glyphs
├── Scenes/
│   ├── SplashScreen.unity   # Startup scene
│   ├── MenuScreen.unity     # Menu navigation
│   └── GameScene.unity      # Active Sudoku gameplay scene
```

---

## 🚀 Getting Started

### Prerequisites
* **Unity**: `2019.4.3f1 LTS` (or compatible Unity 2019.4+ / 2020.3 LTS)
* **Target Platforms**: Android (XR Headsets / AR Glasses / Mobile AR) or Unity Editor with XR simulation.

### Setup Instructions
1. Clone the repository:
   ```bash
   git clone https://github.com/krn17/XR_Sudoku.git
   ```
2. Open **Unity Hub** and click **Add**.
3. Select the `XR_Sudoku` folder.
4. Set the project version to `2019.4.3f1` (or click *Confirm* to upgrade if using a newer LTS).
5. In Unity, open `Assets/Scenes/MenuScreen.unity` or `GameScene.unity`.
6. Press **Play** in the Unity Editor to test.

---

## 📄 License

This project is licensed under the [MIT License](LICENSE).
