Drony
=====

C# software allowing the creation of a flight plan for an Hexakopter (http://www.mikrokopter.de/ucwiki/fr/HexaKopter) which is a drone.
This project has been realized with Léo Riera, Vincent Voyer and Maxime Caly.
The software uses the Google Maps API in order to give to the user the possibility to place pinpoints on a dynamic map. After having set the beginning point, the user can decide on the flight path by placing other points on the map. There is a possibility to place pinpoints where the Hexakopter would have to take a photo with an embedded camera.
After having placed all the pinpoints, the user can export his trajectory in a file thanks to a parser which converts the flight informations in a language comprehensible by the drone.

The user interface gives the possibility to enter an address in order to jump to any location on the map. It also allows the user to enter informations concerning the flight, like the kind of drone, of camera used or the autonomy of the batteries.

The user can also use the user interface to create XML files. Those files contain information on the kind of drone or camera, which are then selectable in the menu.

Finally, thanks to all the informations entered by the user, there is a possibility to test if the flight is really realizable (if the batteries allow the drone to finish the foreseen trajectory).

=====

Programme C# permettant la création d'une trajectoire pour le vol d'un Hexakopter (http://www.mikrokopter.de/ucwiki/fr/HexaKopter).
Ce projet utilise l'API Google Maps afin de donner la possibilité à l'utilisateur de placer des points sur une carte dynamique. Après avoir désigné un point de départ, l'utilisateur peut déterminer la trajectoire en plaçant d'autres points sur la carte. Une possibilité pour placer des points spéciaux où l'Hexakopter peut prendre des photos à l'aide d'un appareil photo embarqué est présente.
Après avoir placé tous les points, l'utilisateur peut exporter sa trajectoire dans un fichier grâce à un parser qui traduit les informations de vol dans un langage compréhensible par le drone.

L'interface donne la possibilité d'entrer une adresse afin d'y accéder directement sur la map. Elle permet également à l'utilisateur d'entrer des informations concernant le vol, comme le type de drone, la caméra utilisée ou l'autonomie des batteries.

L'utilisateur peut également utiliser l'interface pour créer des fichiers XML. Ces fichiers contiennent des informations sur un type particulier de drone ou d'appareil photo, qui sont par la suite sélectionnables dans le menu.

Enfin, grâce aux informations entrées par l'utilisateur, ce dernier a la possibilité de vérifier si son vol est vraiment réalisable (entre autre en vérifiant si l'autonomie des batteries peut permettre de réaliser la trajectoire prévue).
