# Frieren: Demon Hunt — Game Design Document

Three heroes. Three fighting styles. One journey against the demons.

> **Status:** This is an initial design document for an academic project, not a description of an already developed game. The mechanics described below are design proposals and may change following prototyping and playtesting.

## Game Concept

*Frieren: Demon Hunt* is a mission-based **2D Action RPG**, featuring a **top-down or isometric perspective** and free movement within combat arenas. The game takes place in the world of *Frieren: Beyond Journey's End*. The player accepts demon-hunting quests at a tavern, selects one of three playable characters—Frieren, Fern, or Stark—and enters a combat arena. Each character has a distinct fighting style: Frieren specializes in powerful, slower spells; Fern uses rapid, continuous magic attacks; and Stark fights with an axe in close-range combat. Winning battles rewards the player with resources to upgrade characters and unlock more challenging missions. Special abilities also incorporate short **quick-time events (QTEs)**, in which precise timing affects the attack's power.

**Platform:** Windows PC, controlled using a keyboard and mouse; the project is planned for development in Unity.

![Game title screen](images/01_title_screen.png)

---

## Formal Elements

### 1. Players

The game is designed for **one player**, primarily fans of 2D action games, real-time combat, fantasy, and anime. The intended audience is teenagers and older players. The game should be approachable for beginners while still offering opportunities to improve timing, dodging skills, and the ability to recognize enemy attack patterns. Players can replay completed missions with different characters to experience alternative fighting styles.

![Character selection: Frieren, Fern, and Stark](images/03_character_select.png)

### 2. Objectives

The goal of each mission is to **defeat all required enemies** (or the boss) while retaining at least one health point. Completing missions grants rewards that can be used to upgrade characters. The overall objective of the demo is to finish **two missions against groups of enemies** followed by a third mission featuring **a boss with recognizable attack patterns**.

Objectives will be communicated through mission descriptions at the tavern and brief on-screen instructions during battle, such as “Defeat all demons,” an enemy counter, and a boss health bar where relevant. Success and failure will be clearly communicated on an end-of-mission screen.

![Tavern and quest board](images/02_tavern_quest_board.png)

### 3. Procedures

**Starting the game — the first 30 seconds:** The player sees a tavern featuring a prominent quest board. Clicking on the board opens a short list of available missions. After choosing a mission, the player is taken to a character selection screen with a one-line explanation of each fighting style. The player then enters combat immediately and receives brief on-screen control instructions. The goal is to get the player into the action quickly, without lengthy dialogue.

**Core gameplay loop:** At the tavern, the player selects a mission and character → moves around the arena using WASD, aims with the mouse, and attacks using the left mouse button → dodges enemy attacks and activates a special ability, for example by pressing Q → a short QTE may appear during the special ability, and completing it successfully grants an attack bonus → after winning, the player returns to the tavern and spends rewards on character upgrades before the next mission.

**Ending a mission:** A mission ends when all enemies are defeated or the boss's health reaches zero. A victory screen displays the rewards and an option to return to the tavern. If the player's health reaches zero, a defeat screen offers the option to retry. After defeating the boss in the third mission, a demo-completion screen appears; in an expanded version of the game, players could continue progressing beyond this point.

**Proposed controls:** WASD for movement, mouse for aiming, left mouse button for basic attacks, Q for a special ability, and Space for dodging. During QTEs, specific keys appear and must be pressed within a short time window. These are proposed controls and may change during playtesting.

**Teaching the procedures:** Short instructions during the first battle, contextual hints near abilities, and an accessible help screen. A separate, lengthy tutorial should not be necessary.

![Selecting a character for a mission](images/03_character_select.png)

![Fighting waves of enemies](images/05_forest_enemy_wave.png)

![Quick-time event during a special ability](images/08_special_ability_qte.png)

![Mission summary and rewards](images/09_mission_complete.png)

![Upgrading character stats](images/10_character_upgrades.png)

![Returning to the tavern and selecting the next mission](images/12_return_to_tavern.png)

### 4. Rules

- Each character has a health bar. If it is depleted, the mission fails.
- Movement is restricted to the arena boundaries; the player cannot leave the arena during combat.
- Every character has a basic attack and a special ability. Special abilities consume a resource (Mana for the mages and Stamina for Stark) and/or are subject to a cooldown, depending on the final game balance.
- Damage is determined by character stats, the type of attack, and whether the attack actually hits the enemy; attacks do not automatically connect.
- A well-timed dodge allows the player to avoid certain attacks, subject to the dodge mechanics established in the prototype.
- Successfully completing a QTE provides a bonus to the special attack. Failing a QTE does not necessarily cancel the attack entirely, to avoid overly harsh penalties.

Players will learn the rules through UI indicators, animations, resource bars, highlighted danger zones, and experimentation during the first battle.

### 5. Resources

**HP (Health Points):** Allows the character to remain in combat; enemy attacks reduce it. **Mana / Stamina:** Used to activate special abilities; it may regenerate over time or through another system determined during balancing. **Coins or upgrade points:** Earned by successfully completing missions and used to improve a selected character's HP, Damage, Movement Speed, and special-ability resource capacity.

To keep upgrade decisions meaningful, each mission will award a limited amount of currency or points, and upgrade costs may increase gradually. If missions can be replayed for rewards, the earning rate should be limited so upgrades cannot be accumulated without restriction before the boss fight.

The combat interface will display health and the special-ability resource; the tavern will display currency or upgrade points and the selected character's stats.

### 6. Conflicts

**Player versus the game system:** Demons that attack the player, including close-range enemies, ranged enemies, and a boss with attacks that can be anticipated and learned. **Player versus themselves:** Choosing the most suitable character for a mission, deciding when to use a special ability, where to dodge, and which stats to upgrade.

![Top-down combat with health and magic indicators](images/04_forest_first_battle.png)

### 7. Boundaries

The game world is **closed and arena-based**: the tavern serves as a hub for choosing missions and upgrades, and each mission transports the player to a defined combat arena. Movement takes place on a **flat, two-dimensional plane**, even if the camera angle and art give the environment an isometric sense of depth. Arenas are enclosed by walls, rocks, trees, or other clearly visible environmental boundaries that cannot be crossed.

**Map design principles:**
- **Meaning:** Each arena looks and behaves according to its mission type (forest, abandoned village, or boss arena).
- **Mobility:** Clear paths allow movement and dodging without obstacles that make combat unfairly difficult.
- **Orientation:** Boundaries are easy to see, and the positions of the player, enemies, and hazards remain clear.
- **Interest:** Visual variety between arenas, different enemy types, and distinct attack indicators.
- **Guidance:** A clear quest board at the tavern, combat alerts, and visual warnings for boss attacks.

![Enclosed forest arena](images/04_forest_first_battle.png)

![Abandoned village arena](images/06_abandoned_village.png)

![Boss arena with marked danger zones](images/07_boss_battle.png)

### 8. Outcomes

Each mission has two basic outcomes: **victory** (defeating the enemies and receiving rewards) or **defeat** (losing all health and having to retry). The demo concludes once the boss mission has been completed. The outcome depends primarily on **player skill**—movement, timing, understanding enemy patterns, and using abilities effectively—rather than luck. The demo has no human opponents and is not a zero-sum competition between players; it is a single-player action game against the game system.

---

## Review of Existing Games

**Example search terms:** `top-down action RPG multiple characters`, `2D fast paced spell combat game`, `mission based arena action RPG`, `isometric pixel art dungeon combat`.

### 1. Children of Morta

**Link:** https://store.steampowered.com/app/330020/ChildrenofMorta/

**Similarities:** A 2D action RPG featuring different playable characters, each with their own abilities and fighting style. Players can select different characters for missions or runs, with character progression over time.

**What makes our game different:** A shorter, more focused tavern mission → combat arena structure; three specific characters from the world of Frieren; and QTE-based special attacks instead of an emphasis on dungeon exploration.

![Gameplay screenshot from Children of Morta](https://images.squarespace-cdn.com/content/v1/59a2574903596e3462d0bd19/1570999405688-RB0AGS4DR5NOPJXV37L0/Children%2Bof%2BMorta%2B%282%29.jpg)

### 2. Wizard of Legend

**Link:** https://wizardoflegend.com/press/wizard_of_legend/index.html

**Similarities:** Fast-paced top-down combat, magic-based attacks, dodging, and encounters with enemies and bosses.

**What makes our game different:** A choice among three significantly different fighting styles, including a close-range axe fighter; returning to the tavern between missions; and a short QTE mechanic that can enhance the execution of special abilities.

![Gameplay screenshot from Wizard of Legend](https://cdn.mos.cms.futurecdn.net/RkovJbkUsHUk35BKfJvRmL.jpg)

### 3. Archero

**Link:** https://play.google.com/store/apps/details?id=com.habby.archero

**Similarities:** Arenas and stages populated by multiple enemies, dodging projectiles and attacks, progressing through battles, and improving character stats.

**What makes our game different:** Active control over three distinct character fighting styles, mission and character selection at the tavern, and a dedicated timing-based skill mechanic through QTE special attacks.

![Gameplay screenshot from Archero](https://i.playground.ru/i/screenshot/185362/archero.jpg)

**Formal elements that most clearly highlight our game's originality:** **Players** (distinct character choices), **Procedures** (short missions with a tavern–combat–upgrade loop), **Rules** (QTEs in special attacks), and **Conflicts** (boss battles that require recognizing attack patterns).

---

## Demo Scope and Future Development

**Demo goals:** Three selectable characters; a tavern with a quest board and basic upgrades; two missions against groups of enemies; one boss mission; at least two types of regular enemies; one special ability and one basic QTE system for each character; and victory and defeat screens.

**Potential future expansions (not commitments for the demo):** Switching characters during combat, an explorable world, cosmetic clothing systems, additional abilities, arenas and bosses, more complex equipment systems, or multiplayer.

**Recommended development order:** (1) Prototype with one character and one enemy, (2) basic attacks/dodging and combat feel, (3) enemies and a boss with attack patterns, (4) the other two characters, (5) tavern–missions–rewards loop, (6) QTEs and visual polish. This order can be adapted to the team's division of work.

**Intellectual property note:** The game uses an existing fictional world and characters and is therefore intended solely as a non-commercial educational prototype; public or commercial distribution may require permission from the rights holders. One option for future development is to replace the characters and setting with original content while retaining the gameplay mechanics.

**Visual mockups:** The game concept images in this document were generated using AI tools for illustrative purposes only. They are not screenshots of a playable or existing game, but visual concepts showing the intended design. The screenshots of the three existing games in the comparison section are sourced from external websites and are not AI-generated mockups.
