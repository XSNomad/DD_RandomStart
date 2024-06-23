# Random Start Mod for Darkest Dungeon
## Overview
This mod provides a randomized starting experience for the game Darkest Dungeon by modifying initial hero class selections and other parameters. It reads JSON configuration files, updates them with randomized data, and saves the modified configurations back to disk.

## Installation
* Download the Mod:
  * Download the RandomStart mod folder and place it in the game's Mods directory.
  * The path should look like this: Darkest Dungeon/Mods/RandomStart.
* Run the Program:
  * Locate and run the RandomStart.exe executable file found inside the RandomStart mod folder.
  * Run this executable before starting a new game in Darkest Dungeon.
# How It Works
* JSON Configuration Files:
  * The program reads specific JSON files (stage_coach.building.json, etc.) located within the game's directories.
* Randomization:
  * It randomizes the selection of hero classes (first_hero_classes).
  * Other parameters like upgrades (number_of_recruits_upgrades, roster_size_upgrades, upgraded_recruits_upgrades) may also be randomized based on the JSON structure.
* Saving Changes:
  * After randomizing the data, the program creates the modified JSON files in the modded directory.
# Requirements
* This mod requires the base game Darkest Dungeon installed on your system.
* Ensure the RandomStart.exe is correctly placed in the Mods/RandomStart directory before running.
* Only the RandomStart.exe and the project.xml file should be here before executing. 
