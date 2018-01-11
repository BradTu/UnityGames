# UnityGames

This repository contains five games made in the Unity game engine. All of them are 2D games and are for PC. All of the code and art for Parastic Passage and Pacific Pollution is by Brad Tully unless noted otherwise. The sounds were acquired from the website http://freesound.org/ . The code for 4HourGameJam, Apocalypse and SkiRoyaleNEW was written by Brad Tully, Joseph Yates and Jason Komoda. The art for those three games was made by Yitian Yie.

## Parasitic Passage

Art and Code by Brad Tully

In this game the player controls a parasite. The goal is to infect every bug in the level before the timer runs out.

Controls:

Move with WASD

Space bar shoots the parasite in the direction you are facing unless noted otherwise.

Bugs and Abilities:

Fly: Can fly and shoots the parasite downwards exclusively. Gets caught in webs.

Bee: Can fly. Gets caught in webs. 

Spider: Can spawn webs that it can climb on by left clicking the mouse on the screen. Can catch flys and bees in the webs.

Termite: Can walk/ climb through trees and wood.

Ant: Can walk through the ground.

First scene of game is "Start Screen" everything else loads in order.

### Start Screen

![Alt text](https://github.com/BradTu/UnityGames/blob/master/StartScreen.JPG)

### First Level

![Alt text](https://github.com/BradTu/UnityGames/blob/master/LevelOne.JPG)

### Rules Page

![Alt text](https://github.com/BradTu/UnityGames/blob/master/RulesPage.JPG)

## Pacific Pollution

Art and Code by Brad Tully

This game is supposed to be a statement on the pollution of our oceans. It starts with on a screen with a top down view of the ocean with trash floating. The player picks up as much as possible before the time runs out. The amount of trash determines how much trash will spawn in the second scene. The second scene is an "endless runner" where the player controls a turtle using the up and down arrows or 'W' and 'S', and tries to avoid eating trash. If the turtle eats 5 pieces of trash it dies and the game ends. As it eats trash it slows down. Also the trash is shown inside the turtles stomach in the bottom left corner of the screen.

### Start Screen

![Alt text](https://github.com/BradTu/UnityGames/blob/master/StartScreen2.JPG)

### Level One (Pickup Scene)

![Alt text](https://github.com/BradTu/UnityGames/blob/master/PicupScene.JPG)

### Level Two ("Endless Runner")

![Alt text](https://github.com/BradTu/UnityGames/blob/master/RunnerScene.JPG)

## 4HourGameJam

Programming: Brad Tully, Jason Komoda, Joseph Yates

Art: Yitian Yie

Scripts written by me: 

Markup:*https://github.com/BradTu/UnityGames/blob/master/4HourGameJam/Assets/Scripts/Asteroid.cs
*https://github.com/BradTu/UnityGames/blob/master/4HourGameJam/Assets/Scripts/Enemy.cs
*https://github.com/BradTu/UnityGames/blob/master/4HourGameJam/Assets/Scripts/ShootingEnemy.cs
*https://github.com/BradTu/UnityGames/blob/master/4HourGameJam/Assets/Scripts/SpinningEnemy.cs

On this project I mainly wrote the scripts of the enemies and their behavior.

Other scripts written by Jason Komoda and Joseph Yates

For this game we were tasked to make a game where each team member could only put 4 hours of work into the game. We made a side scrolling space shooter. The goal is to stay alive until the boss fight. Also do not get hit by objects and do not let them hit the left side of the screen. 

Controls: WASD to move and left click on the screen to shoot rockets.

Enemies: The purple aliens can shoot you. The yellow aliens spin and can only be killed by shooting the cap of the spaceship. The asteroids take three shots to destroy them. The boss (eyeball) takes many shots to kill.

![Alt text](https://github.com/BradTu/UnityGames/blob/master/SpaceShooter2.JPG)

This contains the purple and yellow aliens as well as the asteroid.

![Alt text](https://github.com/BradTu/UnityGames/blob/master/SpaceShooter3.JPG)

This contains the boss enemy.

## Apocalypse

Programming: Brad Tully, Jason Komoda, Joseph Yates

Art: Yitian Yie

Markup:
Scripts written by me:
*https://github.com/BradTu/UnityGames/blob/master/Apocalypse/Apocalyps/Assets/Scripts/Animals/AnimalParent.cs
*https://github.com/BradTu/UnityGames/blob/master/Apocalypse/Apocalyps/Assets/Scripts/Animals/Deer.cs
*https://github.com/BradTu/UnityGames/blob/master/Apocalypse/Apocalyps/Assets/Scripts/Animals/Rabbit.cs
*https://github.com/BradTu/UnityGames/blob/master/Apocalypse/Apocalyps/Assets/Scripts/Player/Axe.cs
*https://github.com/BradTu/UnityGames/blob/master/Apocalypse/Apocalyps/Assets/Scripts/Player/Player.cs
*https://github.com/BradTu/UnityGames/blob/master/Apocalypse/Apocalyps/Assets/Scripts/Player/Trap.cs (not implemented in game)

In GameController.cs wrote the "private IEnumerator hungerThirstTimer" starting at line 162
*https://github.com/BradTu/UnityGames/blob/master/Apocalypse/Apocalyps/Assets/Scripts/Controllers/GameController.cs 

For this project I mainly did the behavior of the animals and the player controls.

Other scripts written by Jason Komoda and Joseph Yates

For this game we were tasked to make a game where each team member could contribute 8-12 hours of work. We made an apocalypse survival game where there is two weather modes. One where it is raining and there is a lot of water and resources. Then there is a drought. During the drought there is less resources and fires pop up that can hurt the player.

Deer attack the player if you get close which will decrease your health. Rabbits run away when you get close. You can kill both with your axe and collect meat to eat. You can knock down trees with your axe and collect the wood to make a new axe if yours breaks. When you walk over water you collect it and can drink it to satisfy your thirst. Every time the timer goes to 0 on the right. The thirst and hunger will decrease. If you have food and water it will increase your health. Once they reach 0 your health decreases. When your health is 0 the game ends.

Controls: WASD to move. Left click to use the axe. Left click the buttons on top to use resources.

### The game during the drought
![Alt text](https://github.com/BradTu/UnityGames/blob/master/Apocalypse1.JPG)

### The game during a rain storm
![Alt text](https://github.com/BradTu/UnityGames/blob/master/Apocalypse3.JPG)

## Ski Royale

### (This game is still a work in progress, expected to be complete June 2018)

Programming: Brad Tully, Jason Komoda, Joseph Yates

Art: Yitian Yie

Scripts written by me:
Markup:
*https://github.com/BradTu/UnityGames/blob/master/SkiRoyaleNEW/Assets/Scripts/GameController.cs
*https://github.com/BradTu/UnityGames/blob/master/SkiRoyaleNEW/Assets/Scripts/Menu.cs
*https://github.com/BradTu/UnityGames/blob/master/SkiRoyaleNEW/Assets/Scripts/Player.cs
*https://github.com/BradTu/UnityGames/blob/master/SkiRoyaleNEW/Assets/Scripts/CameraController.cs
*https://github.com/BradTu/UnityGames/tree/master/SkiRoyaleNEW/Assets/Scripts/Tutorial

For this game the work I did revolved around the Player movement/ controls, the game controller which manages the race scene, the tutorial scenes and scripts, and the menus.

All other scripts written by Jason Komoda and Joseph Yates

This game is a combat focused party racing game. The goal is to win the race. You can hit other players with your ski poles and you can use the various items to help you and hurt your foes. As of now the game is 2 player split screen. In the future we will try to support up to 4 players split screen and allow for a single player experience as well. In order to get the full experience of the game you MUST use two XBOX controllers for the PC. Start the game on the "Menu" scene.

### Menu Screen
![Alt text](https://github.com/BradTu/UnityGames/blob/master/Ski1.JPG)

### Controls for the XBOX controller
![Alt text](https://github.com/BradTu/UnityGames/blob/master/Ski2.JPG)

### Item Descriptions
![Alt text](https://github.com/BradTu/UnityGames/blob/master/Ski5.JPG)

### Racing Scene
![Alt text](https://github.com/BradTu/UnityGames/blob/master/Ski3.JPG)

### Finish Screen
![Alt text](https://github.com/BradTu/UnityGames/blob/master/Ski4.JPG)
