using Godot;
using System;

public partial class Building : Area3D
{
	//define initialisation method
	public void Initialise(Vector3 StartPosition)
	{
		//place mob at StartPosition
		Position = StartPosition;
	}
	
	private void OnVisibleOnScreenNotifier3DScreenExited()
	{
		//destroy instance of building when its onScreen node leaves the screen
		QueueFree();
	}
}
