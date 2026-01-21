using Godot;
using System;

public partial class ScoreLabel : Label
{
	//[Export] means to export the following variable as a property that can be edited in the node's inspector
	[Export]
	public int Score { get; set; } = 0;
	
}
