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
	
	
	private void SpawnBuilding()
	{
		//create a new instance of the building model
		Building building = BuildingScene.Instantiate<Building>();
		
		//initialise building
		Vector3 _start = new Vector3(1, 1, 1);
		building.Initialise(_start);
		
		//spawn building by adding it as a child of the main scene
		AddChild(building);
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
			SpawnSettingButton();
			_spawnBool = false;
		}
		
		if (Input.IsActionPressed("spawn_building"))
		{
			SpawnBuilding();
		}
	}
}
