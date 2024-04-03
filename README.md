# cs426_Sanchez_Isaac_Asgn5

Most Updated scene: "DemoAsgn6" in "Isaac2_Asgn2" branch

Group members: Isaac Sanchez, Victor Savage, Ahmed Ruyyashi

Controls for demo: 

                   A/D or left/right keys to rotate mask left or right before launching
                   
                   W/S or up/down keys to adjust vertical arch angle or recenter the arch to the front of the player after rotating the camera horizontally
                   
                   Hold and let go of space bar to charge and then release force to launch the mask during their turn

Note on multiple same-named branches (if they are still visible): the branches with "2" in their names are the most recent branches those respective team members are using. For the older branches, we encountered merge conflicts and the act of merging made those branches difficult to work with, so the numbered branches were created from older, stable commits from those older branches.

---Asgn 6 details---

For physics, we used a hinge joint for our mouse trap, particle systems for the masks, and basic box collisions for when the mask touches the face trigger around the human, or when the mask makes contact with the trampoline in the level. 

For textures, we have 2 textures for the masks, a carpet texture, UI textures for the power bar, textures for the trampoline and sticky trap, and textures for each mecanim object. 

Point lights are placed on the roomba, a spot light is above the human, and there's a point light above the trampolines.

AI
- Isaac developed the FSM and pathfinding AI for the roomba via Navmesh. The roomba has a patrolling state, maskAbsored state, and returning state so that the roomba can absorb moving masks and returning them to their original spawn locations before returning to their patrolling states.
- Ahmed developed the flocking AI for the bees above the sticky trap. The flies indicate that the sticky trap is undesirable, helping the player understand what they should avoid in the levels. 
- Victor developed the FSM for the spider/gremlin creature near the center of the room. The gremlin forces the players to make shots from slightly farther distances so that they avoid being attacked by the gremlin near the human.

Mecanim
- Isaac contributed the mouse mecanim, where the mouse will jump when the space bar is release (usually indicating that a mask has been launched).
- Ahmed contributed the human mecanim, where the human will go into a scared state when their face is hit with a mask.
- Victor contributed the spider/gremlin mecanim, where the gremlin will move into an attack animation when they sense a mask nearby.

These components help make the game more challenging and/or help convey a sense of both humor and mystery, as the premise of masks stealing personality from others can be seen as both funny as somewhat dark.
