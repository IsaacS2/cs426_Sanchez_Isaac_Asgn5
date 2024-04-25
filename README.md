# cs426_Sanchez_Isaac_Mask_Mayhem

Most Updated scene: "DemoAsgn9" in "main" branch

Group members: Isaac Sanchez, Victor Savage, Ahmed Ruyyashi

# Controls for Asgn9MaskMayhemPublicDemo (our latest build): 

- A/D or left/right keys to rotate mask left or right before launching
                   
- W/S or up/down keys to adjust vertical arch angle

- Q/E to rotate vertical arch angle independently of mask
                   
- Hold and let go of space bar to charge and then release force to launch the mask during their turn

# Assignment 6 Notes

### For physics, we used a hinge joint for our mouse trap, particle systems for the masks, and basic box collisions for when the mask touches the face trigger around the human, or when the mask makes contact with the trampoline in the level. 

### For textures, we have 2 textures for the masks, a carpet texture, UI textures for the power bar, textures for the trampoline and sticky trap, and textures for each mecanim object. 

### Point lights are placed on the roomba, a spot light is above the human, and there's a point light above the trampolines.

### AI
- Isaac developed the FSM and pathfinding AI for the roomba via Navmesh. The roomba has a patrolling state, maskAbsored state, and returning state so that the roomba can absorb moving masks and returning them to their original spawn locations before returning to their patrolling states.
  
- Ahmed developed the flocking AI for the bees above the sticky trap. The flies indicate that the sticky trap is undesirable, helping the player understand what they should avoid in the levels.
  
- Victor developed the FSM for the spider/gremlin creature near the center of the room. The gremlin forces the players to make shots from slightly farther distances so that they avoid being attacked by the gremlin near the human.

### Mecanim
- Isaac contributed the mouse mecanim, where the mouse will jump when the space bar is release (usually indicating that a mask has been launched).
  
- Ahmed contributed the human mecanim, where the human will go into a scared state when their face is hit with a mask.
  
- Victor contributed the spider/gremlin mecanim, where the gremlin will move into an attack animation when they sense a mask nearby.

These components help make the game more challenging and/or help convey a sense of both humor and mystery, as the premise of masks stealing personality from others can be seen as both funny as somewhat dark. The new entrapments and powerups make the gameplay more engaging

# Assignment 7 Notes

### UI Design Issues
- There is a lack of clarity in terms of the distinction between the win text color and the trap message color. The texts should either be more uniform, or the win text should be more colorful to get the player’s attention towards the fact that they won. (Victor)

- The sticky trap is green, when it should be red (or some shade of color besides green), since green is supposed to indicate that something is bad. (Victor)

- The players is not told when their turn has started, which is confusing in the event that they get another turn because the other player landed on a sticky trap, or in general for feedback (Ahmed)

- The player is not told when they are sucked up by a Roomba with the bottom right text for consistency since the other traps provide a message to the player (Victor)
	
- The player is immediately surrounded by traps in the first level, which makes learnability difficult since they haven’t understood the power of the mask launches (Isaac)

- There is no explanation (even a brief one) on the controls of the game or how the power and launch angle work. This can be fixed with a brief explanation in a starting menu  (Isaac)

- The UI color for the power bar is a bit saturated and takes up a bit too much of the screen  (Isaac)

- The win message is in the top right corner of the screen, which harms clarity since it may be difficult to notice (Ahmed)

- After someone wins the game, no input can be made, which makes it unclear what to do next (Ahmed)

### UI Design Fixes
- The win location broke the clarity principle. The win message has been moved to the middle so it appears to the player that he won wide and clear. Also, after the game is beaten a replay button appears in the middle of screen that allow the players to do a rematch.

- The lack of a message for turns broke the clarity principle. Players are told when their turn has begun.

- The sticky trap color broke the consistency principle. Stick trap has been changed to darkish brown.

- The lack of a message for roomba interactions broke the clarity principle. Players are now told with a status message when they’ve been sucked up by a roomba.

- The lack of a prominent color for the win message broke the clarity principle. Win message has been made yellow to make it more prominent

- The lack of space from traps broke the learnability principle. Space has been added between player and traps to let them try out the controls first

- The UI bar color broke the clarity principle. UI bar has been made less saturated and slightly smaller

- The lack of control explanation broke the learnability principle. New start menu with controls has been added.

### Sound Design
We chose the game Kirby’s Dream Course (https://www.youtube.com/watch?v=fFas8TNJmzc&t=476s) to analyze for sound effect inspiration. We counted about 8 sounds effects correlating to different actions in the game. 

#### Angle-changing sound (Isaac)
- This sound takes place when the player is making their shot and moving their shot cursor around the level.
  
- This impacts the visuals by illustrating to the player the importance and meticulousness of deciding a good path for shooting Kirby. This sound effect is perfect in terms of impact (not too strong).
  
- This sound effect is taking place to make adjusting shot angles more lively and less barren.
  
- This sound effect is not repetitive, since it’s very subtle and not distracting.
  
- For this sound, they might have used older golf games as an example.
  
- This sound effect is perfect, since it’s low in volume due to being one of few sounds being played while planning a shot, as opposed to being played when a shot has been made, and actions are taking place.

#### Charge Sound Effect (Isaac)
- This sound takes place when the player is charging the power of their shot after adjusting the shot angle.
  
- This impacts the visuals using the pitch of the sound effect to reflect the strength of the shot, giving the player a rough estimate of how significant charging the shot is for launching Kirby farther. The sound effect is strong for the visual element its portraying, as shot power is important when making long shots across levels, so the sound effect should reflect the vital nature of this formal element.
  
- The sound is taking place to provide a sense of power when making shots. This builds tension since the player is likely focused on timing a good shot to finish the level as quick as possible.
  
- The sound effect is slightly repetitive, since the player will be charging shots constantly. The game designers could have fixed this by providing a few variations in charge sound effects that play in a random order. However, this can disrupt gameplay if the player uses the shifting pitches of the charge sound effect to determine when to shoot Kirby.
  
- They likely used the idea of a revving car or power tool to conceptualize this sound effect.
  
- This sound effect is perfect in volume, since its use in determining shot power (alongside the power bar) is important for gameplay, and since the sound takes place right before Kirby is shot forwards, it does not drown out any other sound effects.

#### Target hit Sound effect (Ahmed)
- The sound is taking place when the player successfully hits the face in the game.

- The sound impacts the visuals by providing immediate feedback to the player, enhancing the sense of accomplishment.

- The sound is taking place to reinforce the player's actions and provide feedback on their performance.

- If there are no sounds, it could be due to a design choice to create a more minimalistic or serene gameplay experience, or it could be an oversight.

- To reduce repetition, game designers could implement variations of the target hit sound effect based on factors such as the type of target, the weapon used, or the player's performance.

- The target hit sound effect could have been created by recording various impacts such as hitting a drum or a solid object. These recordings were then edited and layered to create a unique sound that fits the game's context.

- During full-out gameplay, the sound should be well balanced with other game sounds to ensure it's audible without being overwhelming or drowned out by other sounds.

#### Win Sound effect (Ahmed)
- The sound is taking place when the player achieves victory.

- The sound impacts the visuals by signaling a moment of success, reinforcing positive feedback to the player. 

- The sound is taking place to celebrate the player's success and create a sense of satisfaction and motivation.

- If there are no sounds, it could diminish the sense of achievement and excitement for the player, making the victory feel less rewarding.

- To reduce repetition, game designers could create multiple versions of the win message sound effect with varying tones or musical elements to keep it fresh and engaging.

- The win message sound effect might have been inspired by classic victory fanfares from other games or movies. It could have been created by composing a short, uplifting melody using digital or orchestral instruments.

- During full-out gameplay, the win message sound effect should be balanced with other sounds to ensure it's noticeable without being too loud or overwhelming compared to other game audio.

#### Character Launch Sound (Victor)
- This sound takes place when the player is being launched
		
- The sound impacts the visuals by signaling that the character is off the ground; it helps inform the player of the success of their input.
		
- The sound is taking place to give the effect of an object with mass being launched with some force.
		
- If there is no sound, the game will be flat, with no active reinforcement for the player 

- The sound is not repetitive, as it is only required once: when the character is being launched off the ground.

- The sound effect of launching the character is inspired by the realism of hitting an object with some force, which makes a sound. Examples of this can be seen in golfing when the ball is hit by the club, gun shooting, etc.

- The sound effect is well balanced with the gameplay, as the sound is loud enough to be audible but quiet enough so as not to distract the player.

#### Character Landing Sound (Victor)
- This sound takes place after the character is launched and makes contact with the ground.

- The sounds inform the user when the launch sequence is over, and their character has made contact with the ground surface.

- The sound is taking place to give the effect of a character with a mass and some force landing on a surface.

- If there is no sound, the game will be flat, with no active reinforcement for the player 

- The sound can be repetitive if the character bounces along the ground surface, which is required to create a bouncing effect.

- The sound effect is inspired by the realism of an object with force hitting a surface and releasing energy in the form of sound, which can be noticed when an object is thrown.

- The sound effect is well balanced with the gameplay, as the sound is loud enough to be audible but quiet enough so as not to distract the player.

#### Sounds added/rejected
For the win sound effect, we rejected clips of people talking. For the trampoline, we rejected a several generic trampoline jumping sound. We rejected several generic horror sound effects in favor of sci-fi and self-made sound effects to better suit the spooky but humorous mood of the game. 

We added sounds for the mask landing, the mask being launched, the mask hitting the mouse trap/sticky trap/roomba, the mouse charging power, the mask adjusting its angle, the mask hitting a trampoline, and the mask hitting the face and winning. We also added background music for the start menu and main level, and background noise for the gremlin and the mouse. The mouse noise was made with lip sounds, and he power-charging sounds was made with an ocarina. The angle adjusting sound was made with snapping. These sounds helped make our game more mysterious, but fun.

# Assignment 8 Notes
### Ahmed Additions
Ahmed added an eye shader which makes the game more humorous and unsettling, made the traps appear clearly so players don’t fall into it without knowing its dangerous, also made a intro logo at the beginning of the game to make it look more professional and to introduce the game to the player, and made the objective of the goal clearer so they players should know what to aim for when playing the game.

### Victor Additions
Victor added a detail texture shader for the masks, for a more design centered effect that gives the masks more prominence. Due to some suggestions from the alpha release, we replaced the main music for a more context-appropriate option. Also added more user information regarding the gremlin, making them more aware of the gremlin’s action. Reduced the penalty from gremlin attack to lower player frustration. Attempted syncing trajectory line rotation with mask rotation.

### Isaac Addition
Isaac added a toon-like shader to the boxes and table of the room, to give these environmental surfaces more prominence so that the player knows to use their verticality to their advantage. Isaac also added the introductory dialogue to the start menu, to expand on the setting. In terms of making the game easier, the human’s size was lowered, boxes were lowered to make them easier to jump on, and the trajectory angle has been given a new feature of being able to turn it separately from the mask with the Q/E keys. This allows the player to turn the angle and have the camera at a better view for seeing where they will land. In addition, the trajectory angle now updates with the current charged force of the player, for more accurate shots. Finally, the mask model has been updated with a custom model made in blender, so that users correlate the character with a mask.

# For Assignment 9, we implemented bug fixes and feedback from beta testing to create the finished build seen in Asgn9MaskMayhemPublicDemo.zip. Download, unzip the folder, and run the "Mask Mayhem" executable to play the game. Enjoy!
