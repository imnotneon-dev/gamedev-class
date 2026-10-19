# GAMEDEV - Prototyping 2: Ray-OE Zoning

**Student Name:** Jenrick Lim

---

### Video Showcase

Please access the screen recording of the gameplay demo either in the **`Documentations/`** folder (`Documentations/Prototype 2 Demo.mp4`) or through this link: (https://drive.google.com/file/d/14I8R7HA2IF3zM2uL0DydPlyFnTYWEgK4/view?usp=drive_link)

---

## Level Overview

Converted from the laser room corridor level into a first-person stealth level, players must navigate through a corridor guarded by moving hazards. Players can use a flashlight to reveal hidden wall secrets and inspect hazard danger zones.

### Key Features & Mechanics

- **Moving Hazards:** Kinematic hazard obstacles patrol back and forth across the corridor using smooth ping-pong movement.
- **Stealth & Line-of-Sight Detection:** Hazards use `Physics.CheckSphere` for proximity detection and a `Physics.Raycast` to check for direct line-of-sight.
- **Flashlight Mechanic:** Toggled using the **`F`** key. When active, it scans objects inside the spotlight's light cone combined with line-of-sight raycasts to prevent detecting objects through walls.
- **Hidden Objects:** Secret walls are placed on the `HiddenObject` layer remain invisible until illuminated by the flashlight beam.
- **Hazard Danger Zone Visualizer:** Shining the flashlight on a moving hazard calls `Reveal()` on its `HazardZoneMesh`, displaying a translucent red sphere dynamically auto-scaled to its exact detection radius.
