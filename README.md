Rocket Boost

Rocket Boost is a physics-based arcade navigation game developed in Unity. The project focuses on controlling a spacecraft through challenging environments while managing resource constraints and environmental hazards.
Play the Game

The latest build of Rocket Boost can be accessed here:
[Insert Web Link Here]
Project Overview

This project was developed as part of the Software Programming and Development pathway at Byron Nelson High School. It represents a practical exploration of game mechanics, physics-based movement, and the iterative software development lifecycle within a professional engine environment.
Technical Implementation

The game utilizes Unity's Universal Render Pipeline (URP) and is built with a focus on modular component design.
Key Mechanics

    Physics-Based Control: The flight model relies on constant momentum and gravitational forces, requiring players to balance thrust and rotation to maintain stability.

    Resource Logic: Players must interact with specific environmental objects, such as fuel pickups, to extend their flight time.

    Collision Matrix: The game uses a custom tag system (Player Rocket, Friendly, Fuel) to differentiate between safe landing zones, essential resources, and hazardous obstacles.

Software Engineering Insights

Developing Rocket Boost provided significant experience in several core software engineering disciplines:
Component-Based Architecture

Working within Unity highlighted the importance of a component-based design pattern. Instead of building monolithic scripts, we learned to separate concerns into distinct modules for movement, resource management, and collision logic. This modularity made the project easier to debug and extend.
Version Control and Collaboration

Collaborating on a shared codebase required disciplined use of version control. We learned how to manage project settings, handle merge conflicts in YAML-based asset files, and maintain a consistent project structure to ensure that changes made by one developer did not break the work of another.
Problem Solving and Iteration

The development process emphasized the importance of the "fail fast" mentality. Early prototypes of the flight physics were often uncontrollable, requiring multiple rounds of tuning variables like gravity, sensitivity, and mass within the project settings to achieve a "difficult but fair" gameplay loop.
Controls

The input system supports both keyboard and mouse configurations:

    Thrust: Spacebar

    Rotation: A/D Keys or Left/Right Arrow Keys

    Action/Interact: Left Ctrl or Mouse 0

Credits

    Jose Perez Flores – Co-Developer and Designer

    Gavin Johnson – Co-Developer and Designer

Note on Source Files: While the core gameplay mechanics and input configurations were successfully deciphered from the project's asset and management files, the specific C# (MonoScript) logic files were not available for review in this directory. The README is based on the inferred logic from the TagManager and InputManager configurations.
