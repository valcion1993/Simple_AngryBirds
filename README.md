# Simple_AngryBirds
Simple Angry Birds game clone

*Refracted the code, in which it was totally messed up in some classes.

*Some had variables declared last, there were unnecessary methods, and calls per second where it wasn't necessary.

*The entire HUD was reworked and responsive to multiple resolutions.

*For the birds, a base class was created to create sub classes of birds. This to have a much cleaner code.

*A bird selection Manager was created, the available Slots are automatically taken to equip birds (3 for now).
It is a simple Drag and Drop system to equip birds, when you click on one of the equipped birds, this slot will remain empty.
You just have to make sure that there are positions available so that at the beginning of the game they can be created correctly.

![](https://github.com/valcion1993/Simple_AngryBirds/blob/main/Assets/Readme/BirdSelection.gif)

*A Particle Manager was created for effect particles when hitting or exploding a bird, this to avoid creating and destroying particles and saving resources.

*A Sound Manager was created to use and reuse the ones that are already inside the scene.

*A Floating Texts Manager was created to show how many points were made when hitting a target, so as not to create and destroy floating texts and save resources.

![](https://github.com/valcion1993/Simple_AngryBirds/blob/main/Assets/Readme/Demo.gif)

*A loading screen was created, to give more smoothness when restarting the scene, it can be used to load other scenes as well.
