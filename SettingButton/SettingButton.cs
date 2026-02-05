using Godot;
using System;

public partial class SettingButton : Label
{
	//define variable to tell whether the settings are open or not
	[Export]
	public bool settingsOpen { get; set; }
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}
	
	//define OnMouseEntered to display menu when the mouse hovers over the settings
	public void OnMouseEntered()
	{
		//set settingsOpen to true so mouse movement doesn't affect camera
		
		//show hslider node to affect width
		//GetNode("WidthSlider").IsVisible = true;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
