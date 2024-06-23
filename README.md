# Random Start Mod for Darkest Dungeon
## Overview
This mod provides a randomized starting experience for the game Darkest Dungeon by modifying initial hero class selections and other parameters. It reads JSON configuration files, updates them with randomized data, and saves the modified configurations back to disk.

## Installation
* Download the Mod:
  * Download the RandomStart mod folder and place it in the game's Mods directory.
  * The path should look like this: Darkest Dungeon/Mods/RandomStart.
## How It Works
* JSON Configuration Files:
  * The program reads specific JSON files (stage_coach.building.json, etc.) located within the game's directories.
* Randomization:
  * It randomizes the selection of hero classes (first_hero_classes).
  * Other parameters like upgrades (number_of_recruits_upgrades, roster_size_upgrades, upgraded_recruits_upgrades) may also be randomized based on the JSON structure.
* Saving Changes:
  * After randomizing the data, the program creates the modified JSON files in the modded directory.
## Usage
* Basic Usage
  * Run the Darkest.exe before starting a new game of Darkest Dungeon. The program will randomly assign skills and other attributes to your heroes.
* Party Mode
  * To use party mode, add the -party argument when running the executable:
 ```
Darkest.exe -party
```
In party mode, the program will randomly select a predefined party configuration from the party_name_library.json file located in the shared/party_name directory. This will affect the initial setup of heroes when starting a new game. Your First two heroes will be with you at the start and the last two will be on the stage coach. If party mode is working correctly you should see a predefined name show up after completing your party.

# Requirements
* This mod requires the base game Darkest Dungeon installed on your system.
* Ensure the RandomStart.exe is correctly placed in the Mods/RandomStart directory before running.
* Only the RandomStart.exe and the project.xml file should be here before executing.

* .NET 6.0 Framework:
  * This program requires the .NET 6.0 Framework to be installed on your system. If you do not have it installed, you can download it from the official Microsoft website.
# Installing .NET 6.0 Framework
* Download .NET 6.0:
  * Visit the [Microsoft .NET Download page](https://dotnet.microsoft.com/download/dotnet/6.0)
* Choose Your OS:
  * Select your operating system (Windows, macOS, or Linux) and download the installer.
* Install .NET 6.0:
  * Run the downloaded installer and follow the on-screen instructions to install .NET 6.0 Framework on your system.
* Verify Installation:
  * Once installed, open a command prompt or terminal and type dotnet --version to verify that .NET 6.0 Framework is correctly installed.
