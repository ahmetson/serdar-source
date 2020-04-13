Structure of Level Scenes:

Env ->
	World, which is not interactable.

Roads ->
	Road
		Beginning
		End
		Waypoint manager 
			Waypoint
Campuses ->
	Campus
		Sign On Road
		Road
			Beginning
			End
			Waypoint Manage
				Waypoint
		Settlement
		Tribe
			Warrior
ETC ->
	Camera,
	ScriptHolders
	Light
SceneContext

------------------------------------------------------------------
Road script's structure and ability
------------------------------------------------------------------
I will be able to create road (With the inner Beginning and Ending points, Waypoint manager and so on.
That Road will always located at the Roads GameObject.
It also has a Road Script.

The road has continuations and signs. The continuations are, are links to another roads, where user will continue.

And signs indicates the Campuses or treasures.

All continuations will be showed with the link. And you can pick the Continuation roads by picking another roads.

During the Edit mode, and play mode, the Scene will show the continuations as a link between roads or arrows.


-----------------------------------------------------------------
Campus
-----------------------------------------------------------------
I will be able to create campus
Campus has a type.
Has a Settlement, SignOnRoad and Road.



-----------------------------------------------------------------
Campuses Script
-----------------------------------------------------------------
When Campus will be loaded, it will add himself into the Campuses list of the Scene.

At the campuses list of the scene, campuses are structurized under the road's scene.