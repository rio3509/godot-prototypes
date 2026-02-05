using Godot;
using System;

public partial class Main : Node
{
	//use [Export] to take in the other scenes e.g. building, disaster etc
	
	//import building scene
	[Export]
	public PackedScene BuildingScene { get; set; }
	//import settings button scene
	[Export]
	public PackedScene SettingButtonScene { get; set; }
	//define boolean for spawning
	[Export]
	public bool _spawnBool { get; set; }
	//define boolean for whether the settings are open or not
	public bool settingsOpen { get; set; }
	
	
	private void SpawnBuilding(Vector3 position, Vector3 scale)
	{
		//create a new instance of the building model
		Building building = BuildingScene.Instantiate<Building>();
		
		//initialise building
		building.Initialise(position, scale.X, scale.Z, scale.Y);
		
		//spawn building by adding it as a child of the main scene
		AddChild(building);
		//apply force to move it slightly
		building.AddForce(new Vector3(10, 5, 10), new Vector3(1, 1, 0));
	}
	
	//spawn random city based on building inputs
	private void SpawnRandomCity(int buildingCount, Godot.Collections.Array buildingWidths, Godot.Collections.Array buildingDepths, Godot.Collections.Array buildingHeights)
	{
		//iterate through each building
		for (int i = 0; i < buildingCount; i++)
		{
			//instantiate building
			Building cityBuilding = BuildingScene.Instantiate<Building>();

			// Prints a random integer between 10 and 60.
			//GD.Print(GD.Randi() % 51 + 10);
			
			//get random position on the ground within the given land area (between 10 and -10)
			var randX = (GD.Randi() % 10);
			var randZ = (GD.Randi() % 10);
			var Y = 2;
			
			Vector3 randomPos = new Vector3(randX, Y, randZ);
			
			//get building width, depth and height
			float cityBuildingW = buildingWidths[i].As<float>();
			float cityBuildingD = buildingDepths[i].As<float>();
			float cityBuildingH = buildingHeights[i].As<float>();
			
			Vector3 dimensions = new Vector3(cityBuildingW, cityBuildingD, cityBuildingH);

			cityBuilding.Initialise(randomPos, cityBuildingW, cityBuildingD, cityBuildingH);
			
			//if the given building overlaps with another building then find another random position
			//if this fails 3 times move on to the next building
			
			//add the city building as a child
			AddChild(cityBuilding);
			
		}
	}
	
	private void SpawnSettingButton()
	{
		//create a new instance of the settings button
		SettingButton button = SettingButtonScene.Instantiate<SettingButton>();
		
		//spawn settings button by adding it as a child of the main scene
		AddChild(button);
		
		//set _spawnBool to false to prevent further spawning
		_spawnBool = false;
	}
	
	//create an instance of a building when a key is pressed
	public override void _PhysicsProcess(double delta)
	{
		//spawn the settings button on the first physics frame
		if (_spawnBool == true)
		{
			Godot.Collections.Array widths = [1, 2];
			Godot.Collections.Array depths = [3, 2];
			Godot.Collections.Array heights = [1, 5];
			
			SpawnRandomCity(2, widths, depths, heights);
			Vector3 _test = new Vector3(2, 5, 2);
			//SpawnBuilding(Vector3.One, _test);
			SpawnSettingButton();
			settingsOpen = false;
		}
		
		//check if the player is in the settings or not
		if (settingsOpen == true)
		{
			//stop camera
				
		}
		
		//check if the player wanted to spawn a building
		if (Input.IsActionPressed("spawn_building"))
		{
			SpawnBuilding(Vector3.One, Vector3.One);
		}
	}
}
