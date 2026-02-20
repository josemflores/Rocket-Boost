# Rocket Boost

Rocket Boost is a physics-based arcade navigation game built using the Unity engine and the Universal Render Pipeline (URP). The project challenges players to navigate a high-momentum spacecraft through hazardous environments, requiring precise control and resource management to reach a safe landing zone.
Play the Game

The latest build of Rocket Boost (Rocket Gen1) is available to play in your browser:
https://play.unity.com/api/v1/games/game/b3c5b7d6-57ea-459d-a2d0-344f554d987b/build/latest/frame
# Project Overview

This project was developed as a key component of the Software Programming and Development pathway at Byron Nelson High School. It serves as a practical application of game design theory, physics integration, and the iterative software development lifecycle. The primary goal was to create a responsive, challenging gameplay loop while maintaining clean, modular project architecture.
# Technical Implementation

__Physics-Based Navigation__

The flight model utilizes Unity's physics engine to simulate realistic momentum. Players must manage constant gravitational pull and inertial forces, making every thrust and rotation a calculated decision. The sensitivity and gravity settings were meticulously tuned in the project configuration to balance difficulty with player agency.
 
__Object-Oriented Environmental Logic__

The game utilizes a robust tag-based collision system to manage environmental interactions. By categorizing objects into specific layers—such as "Friendly" for landing zones, "Fuel" for resource replenishment, and "Player Rocket" for the primary actor—the system can efficiently handle complex collision logic without taxing performance.

__Rendering and Performance__

Built on the Universal Render Pipeline, the project is optimized for both visual fidelity and performance across platforms, including the WebGL build. The use of URP allows for advanced lighting and post-processing while ensuring the game remains accessible on lower-end hardware.
# Software Engineering Insights

Developing Rocket Boost provided hands-on experience with several core engineering principles:

__Component-Based Architecture__

The project follows Unity's component-based design pattern. By decoupling movement, collision handling, and resource management into separate modules, the codebase remains scalable. This approach taught the importance of "separation of concerns," making it easier to troubleshoot specific mechanics without affecting the entire system.

__Configuration and Version Control__

Managing the ProjectSettings and Packages manifests required a disciplined approach to version control. Handling YAML-based asset files and configuring comprehensive .gitignore and ignore.conf rules highlighted the necessity of maintaining a clean repository, ensuring that temporary local data (like the Library folder) does not interfere with the shared codebase.

__Iterative Problem Solving__

Fine-tuning the "feel" of the rocket required constant iteration. Through adjusting the input axes for thrust and rotation, the development process emphasized the importance of user experience (UX) testing and learning how to translate technical variables into an intuitive and rewarding player experience.
# Controls

The input system is configured for standard keyboard and mouse layouts:

    Thrust: Spacebar

    Rotation: A / D Keys or Left / Right Arrow Keys

    Interaction: Left Ctrl or Mouse 0

# Credits

    Jose Perez Flores – Developer and Designer

    Gavin Johnson – Developer and Designer
