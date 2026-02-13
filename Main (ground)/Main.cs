using Godot;
using System;

public partial class Main : Node
{
	//use [Export] to take in the other scenes e.g. building, disaster etc
	
	//import building scene
	[Export]
	public PackedScene BuildingScene { get; set; }
	
	//define boolean for whether the settings are open or not
	public bool settingsOpen { get; set; }
	
	//testing variable
	[Export]
	public bool spawnBool { get; set; }
	
	
	//this is a test method and may be removed later
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

			// This prints a random integer between 10 and 60:
			//GD.Print(GD.Randi() % 51 + 10);
			
			//get random position on the ground within the given land area (between 10 and -10)
			var randX = (GD.Randi() % 10);
			var randZ = (GD.Randi() % 10);
			var Y = 5;
			
			Vector3 randomPos = new Vector3(randX, Y, randZ);
			
			//get building width, depth and height
			float cityBuildingW = buildingWidths[i].As<float>();
			float cityBuildingD = buildingDepths[i].As<float>();
			float cityBuildingH = buildingHeights[i].As<float>();
			
			Vector3 dimensions = new Vector3(cityBuildingW, cityBuildingD, cityBuildingH);

			cityBuilding.Initialise(randomPos, cityBuildingW, cityBuildingD, cityBuildingH);
			
			//if the given building overlaps with another building then find another random position - use OnBodyEntered() to check if there are collisions with anything
			//if this fails 3 times move on to the next building
			
			for (int j = 0; j < 4; j++)
			{
				if (cityBuilding.OnBodyEntered())
				{
					//the building to be placed is colliding with something; re-initialise it with new coords
					randX = (GD.Randi() % 10);
					randZ = (GD.Randi() % 10);
					randomPos = new Vector3(randX, Y, randZ);
					
					//cityBuilding.Initialise(randomPos, cityBuildingW, cityBuildingD, cityBuildingH);
					cityBuilding.Position = randomPos;
				}
				else
				{
					//the building is "fine"; we can stop the loop and move onto the next building
					j = 3;
				}
			}
			
			
			AddChild(cityBuilding);
			
			spawnBool = false;
			
			//check one last time for any overlaps - this DOESN'T work as it considers the building touching the ground to be an overlap
			//if (cityBuilding.OnBodyEntered())
			//{
				////delete building instance + add its number to the "failed" list
				////cityBuilding.QueueFree();
				//AddChild(cityBuilding);
			//}
			//else
			//{
				////add the city building as a child if it works
				////AddChild(cityBuilding);
			//}
			
		}
	}
	
	
	//when mouse enters the settings button set _settingsOpen to true; this stops camera movement and opens the menu
	public void OnUIMouseEntered()
	{
		GD.Print("UIENTERED");
		//stop camera movement
		settingsOpen = true;
		
		//open settings - set all child nodes to visible (THIS IS VERY SLOW and could be optimised later)
		Control UserInterface = GetNode<Control>("UserInterface");
		
		Godot.Collections.Array<Godot.Node> controlNodes = UserInterface.FindChildren("HSlider", "", true, true);
		
		GD.Print(controlNodes);
		
		//iterate through child nodes to set their .Visible to true
		foreach (Godot.Control node in controlNodes)
		{
			node.SetVisible(true);
			node.SetMouseFilter(0);
		}
		
	}
	
	
	
	//create an instance of a building when a key is pressed
	public override void _PhysicsProcess(double delta)
	{
		//spawn city on first physics frame
		if (spawnBool == true)
		{
			//initialise camera
			//Camera3D camera = GetNode<Camera3D>("CameraPivot/Camera");
			//test arrays - might need to change to just array instead of Godot.Collections.Array since it's faster for iteration
			Godot.Collections.Array widths = [1, 2];
			Godot.Collections.Array depths = [3, 2];
			Godot.Collections.Array heights = [1, 3];
			var buildingAmount = 2;
			SpawnRandomCity(buildingAmount, widths, depths, heights);
			GetNode<HSlider>("HSlider").SetVisible(true);
			spawnBool = false;
		}
		
		
		//check if the player is in the settings or not
		if (settingsOpen == true)
		{
			//stop camera
			Camera3D camera = GetNode<Camera3D>("CameraPivot/Camera");
			//camera.disabled = true;
			camera.SetPhysicsProcess(false);
			//get building values with settings button
			
		}
		
		//check if the player wanted to spawn a building
		if (Input.IsActionPressed("spawn_building"))
		{
			SpawnBuilding(Vector3.One, Vector3.One);
		}
	}
}
