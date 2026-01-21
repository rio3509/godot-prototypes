using Godot;
using System;

public partial class Main : Node
{
	//use [Export] to take in the other scenes e.g. building, disaster etc
	[Export]
	public PackedScene BuildingScene { get; set; }
	
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
	
	//create an instance of a building when a key is pressed
	public override void _PhysicsProcess(double delta)
	{
		if (Input.IsActionPressed("spawn_building"))
		{
			SpawnBuilding();
		}
	}
}
