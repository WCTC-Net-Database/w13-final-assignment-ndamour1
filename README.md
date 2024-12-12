### FINAL PRESENTATION - ConsoleRPG EF Core Application

#### Basic Required functionality:
- **Add a new Character to the database**
  - Prompt the user to enter details for your character (e.g. Name, Health, Attack, and Defense).
  - Save the updated record to the database.
- **Edit an existing Character**
  - Allow users to update attributes like Health, Attack, and Defense.
  - Save the updated record to the database.
- **Display all Characters**
  - Include any relevant details to your character
- **Search for a specific Character by name**
  - Perform a **case-insensitive** search.
  - Display detailed information about the Character, as above.
- **Logging** (should already be in place)
  - Log all user interactions, such as adding, editing, or displaying data.

#### **"C" Level (405/500 points):**
1. **Include all necessary required features.**
2. **Add Abilities to a Character**
   - Allow users to add Abilities to existing Characters.
   - Prompt for related Ability details (for example, Name, Attack Bonus, Defense Bonus, etc).
   - Associate the Ability with the Character and save it to the database.
   - Output the results of the add to confirm back to the user.
3. **Display Character Abilities**
   - For a selected Character, display all their Abilities.  
   - Include the added properties from their abilities in the output (example, as above, Name, Attack Bonus, Defense Bonus, etc).
4. **Execute an ability during an attack**
   - When attacking ensure the ability is executed and displays the appropriate output.

#### **"B" Level (445/500 points):**
1. **Include all required and "C" level features.**
2. **Add new Room**  
   - Prompt the user to enter a Room name, Description, and other needed properties
   - Optionally add a character, player, etc, to that room.
   - Save the Room to the database.
   - Output the results of the add to confirm back to the user.
3. **Display details of a Room**  
   - Display all associated properties of the room.
   - Include a list of any inhabitants in the Room.  
   - Handle cases where the Room has no Characters gracefully.
4. **Navigate the Rooms**
   - Allow the character to navigate through the rooms and display room details upon entering.
      - Room details may include, for example, name, description, inhabitants, special features, etc.
   - ***Note it is not necessary to display a map as provided during the midterm.***

#### **"A" Level (475/500 points):**
1. **Include all required, "C" and "B" level features.**
2. **These features might represent if you were an "admin" character in the game.**
   - **List characters in the room by selected attribute:**  
     - Allow users to find the Character in the room matching criteria (e.g. Health, Attack, Name, etc).
   - **List all Rooms with all characters in those rooms**  
     - Group Characters by their Room and display them in a formatted list.
3. **Find a specific piece of a equipment and list the associated character and location**
   - Allow a user to specify the name of an item and output the following,
      - Character holding the item
      - Location of the character

#### **"A+" Stretch Level (500/500 points):**
##### The sky is the limit here!  Be creative!
1. **Include all "C", "B", and "A" level features.**
2. **Stretch Feature: Implement something creative of your own making**
   - This can be **anything** including such things as,
      - Interface improvements
      - Database improvements
      - Architectural changes
      - New feature ideas,
         - mini "quest" system
         - enhanced combat system
         - spell casting system
         - item collection system
         - equipment swapping
         - other character types for providing details
         - etc.
---

### Submission Requirements:
1. Submit the following:
   - A video demonstrating the full functionality of your application (approximately 5 minutes).  I recommend using Canvas Studio.
   - A link to your GitHub repository and your database connection string.
   - A README file, 
      - quickly describe the features you added at each grade level and grading level you attempted to achieve
      - include any final comments on the class in the provided README file
2. Use in-class examples and provided resources to complete the assignment.
3. Handle all user errors gracefully (e.g., invalid input, database issues) and log all errors.

#### Most of all.. Have fun!
---
### Notes for Students:
- This assignment integrates everything you've learned about Entity Framework Core, LINQ, SOLID principles, and advanced console programming.
- Use provided .sql files or seed data to prepopulate your database with sample Items and Abilities.
- Remember to create new migrations for any database changes and use `dotnet ef database update` to apply them.
---
Week 13 Assignment: FINAL Assignment
====================================

### Setup Menu
Users are prompted in the setup menu with the options to select a character to play as, add a new character, edit an existing character, create a new room for the game, read the attached guide, or play right away.

#### Selecting a Character
To search for a character to play as, the user has the option to list all available playable characters in the following ways: by name (i.e. alphabetical order), by their race, by their class, and how much experience or health they have.

#### Creating a Character
Creating a new character requires inputting their name, race, and class; equipping them with a weapon and armor, gviing them an ability, and placing in a room to start in. Health is decided automatically based on what class the user pick and all characters start with 0 experience upon creation. The user is then prompted to add how many more abilities are suitable for the race and class they chose for their character until they decide that they have enough.

##### Creating an Ability
Create a new ability requires inputting the type of ability the user wants and a descriptive name for said ability. Inputting the damage, defense, and distance stats of the ability whether it allows the player character to negate enemy damage is dependent on what type of ability the user chooses.

#### Editing a Character
Users are given the option to edit the properties of a chosen character based on a list of existing characters. These properties include their name, their health, and their equipment. Users can choose to either manually input how much health they want their character to have, or to let the program determine the new amount like when the character was first created. If the user chooses to edit the character's equipment, they are given a list of the items in the character's inventory and the ability to equip either a weapon or a piece of armor to them, in place of whatever the character had on before.

#### Creating a Room
Creating a new room requires inputting its name, describing its contents, determining what rooms it connects to on the in-game map, and whether or not a character starts in there.

#### Signing In
After they select to start the game, before the actual gameplay, the user has to input their name. By own name grants "administrative acess" that allows for additional features to the control menu of the main game.

### Game Loop
In the game proper, the user has the option to move into a new room, equip a new piece of armor or weapon in the player character's inventory, find an item that being used in the game, consult the attached guide, attack an enemy should one or more be in the room, or exit the game entirely. My own "administrative features" have the options to find a character in the room the player character is currently in, or display the characters in-use in the game by what room they occupy.

#### Move Into Another Room
The user is able to move the player character into another room based on the four cardinal directions. The map displays the player character's current location, as well as what rooms are acessible from the one they're currently in.

#### Change Equipment
This is basically that same as the user editing a player character's current equipment using the "Edit Character" option from the Setup Menu and choosing, well, their, equipment. All items listed to choose from come from that character's inventory, with the weapons and armor only being available to actually choose from.

#### Find Item
The user inputs the name of an item logged in the game and, if it's a valid name and in a character's inventory, returns the item in question and whether someone has it.

#### Attack
Should there be an enemy in the room, the user is given the option to attack them. If there's more than one, the user is also given the option to choose which one to attack. The damage dealt to the enemy is dependent on whether the player character has a weapon equipped and how much experience they have. The user is then asked to input the description of the player character's ability or one of the player character's abilities. After that, the enemy attacks the player character, with the damage dealt being dependent on the enemy's race and the player character's total defense stat.

If the player character reaches 0 health at any point during this cycle, the game ends. The user is then given the option to restart the game or to quit, and therefore exit, the game. If the enemy reaches 0 health instead; the player character's health is restored, gains 10 experience, and loots a ranodm item not in another player character's inventory from the enemy.

#### Find Characters in Room
The user is given the option to search for characters in the room their player character is currently in based on their name, race, class, health, attack power, or defense power. They are then given the options to either search for player characters or enemies. The class option automatically makes the characters searched for player characters. After inputting appropriately based on these options, a list is displayed based on the criteria or if no one has been found.

#### Group Characters by Room
The user is given the choice between player characters and enemies. A list is then generated that groups the characters of the assigned type by the room they occupy.

### Special Features
#### Name Input
When the game asks for the user to input a name in order to perform a function, it adds the objects with that name to a list based on what function is being performed. If the list is empty, it responds that no object with that name has been found. If there is only one item on the list, it returns that singular value. If the list has more than one item, the user is then asked to to provide an integer based on what position on the list the object they want is located. It then returns that particular value.

#### Game Guide
The guide acts a means for the user to get information on the various races, classes, and abilities in the game. In addition to sweet, juicy LORE based on various franchises; the guide allows the user to make decisions on creating a character, such as what abilities to approproately their character. For example, a vampire sorcerer would logically possess Drain, Bite, Mist, Bubble, Fire, and Lightning abilities. A vampire is able to drink the blood of mortals to revitalize themselves, use their sharp fangs as weapons, and turn into mist to protect themselves from physical harm and to cross long distances. Meanwhile, sorcerers are able to cast spells that allow them project either fire or lightning and erect magical barriers.

### Project Goals
I strived to complete all of the listed requirements for an A+ as I truly did want to do my best for the assignment. Those two screwups on my assignments during the semester's first half really got to me and I wanted to hopefully make up for them here. Plus, I'm already a fan of the RPG concept, do it was a perfect opportunity to do my (technically) second proper attempt at it. Plus it allowed me the opportunity to inject lore into what was otherwise a blank slate thanks to my addition of the GameGuide, which fulfilled the A+ requirement of creating something of my own design.

### Thoughts on the Class
Overall, this was probably one of my favorite college classes I've every taken. While I did have a rocky start, I was able to catch up and was able to grasp the concepts being taught once I knew what I was doing. It's probably why I liked injecting my own comments and questions at times more often than I usually do, since otherwise I'm pretty much quiet during the entire class unless I had an answer to a question or was called upon for something. So, I think was a really great run and I hope this trend continues in the future with my IT track.
