using Godot;
using System;

public partial class Camera : Camera3D
{
	//3D code takes measurements in METERS not pixels
	
	//set camera movement and rotation speed
	[Export]
	public int Speed { get; set; } = 1;
	[Export]
	public float RotateSpeed { get; set; } = 0.5f;
	
	//set targetvelocity to 0
	private Vector3 _targetVelocity = Vector3.Zero;
	 
	public override void _PhysicsProcess(double delta)
	{
		//MOVEMENT
		
		//local variable for input direction
		var direction = Vector3.Zero;
		
		//code left/right (x) movement
		if (Input.IsActionPressed("move_right"))
		{
			direction.X += 1.0f;
		}
		if (Input.IsActionPressed("move_left"))
		{
			direction.X -= 1.0f;
		}
		//code forward/backward (z) movement
		if (Input.IsActionPressed("move_backward"))
		{
			direction.Z += 1.0f;
		}
		if (Input.IsActionPressed("move_forward"))
		{
			direction.Z -= 1.0f;
		}
		//code up/down (y) movement
		if (Input.IsActionPressed("move_up"))
		{
			direction.Y += 1.0f;
		}
		if (Input.IsActionPressed("move_down"))
		{
			direction.Y -= 1.0f;
		}
		
		//normalise direction to ensure uniform movement diagonally
		if (direction != Vector3.Zero)
		{
			direction = direction.Normalized();
			
			//Basis = Basis.LookingAt(direction);
		}
	
		 //Velocities
		_targetVelocity.X = direction.X * Speed;
		_targetVelocity.Z = direction.Z * Speed;
		_targetVelocity.Y = direction.Y * Speed;
		
		  // Moving the camera
		Vector3 position = GetGlobalPosition();
		position += _targetVelocity;
		SetGlobalPosition(position);
		//global_translate(direction);
		
		//ROTATION
		
		//basically this:
		// Rotate around the object's local X axis by 0.1 radians.
		//RotateObjectLocal(new Vector3(1, 0, 0), 0.1f);
		//until Basis == Basis.LookingAt(direction);
		
		//get mouse position
		var mousePos = GetViewport().GetMousePosition();
		GD.Print(mousePos);
		
		//horizontal mouse rotation
		if (mousePos.X > ((1152 + 750) / 2))
		{
			RotateObjectLocal(new Vector3(0, 1, 0), (-0.1f * RotateSpeed));
		}
		if (mousePos.X < ((1152 - 750) / 2))
		{
			RotateObjectLocal(new Vector3(0, 1, 0), (0.1f * RotateSpeed));
		}
		else
		{
			RotateObjectLocal(new Vector3(0, 1, 0), (0f * RotateSpeed));
		}
		
		//vertical mouse rotation
		if (mousePos.Y > (648 + 500) / 2)
		{
			RotateObjectLocal(new Vector3(1, 0, 0), (-0.1f * RotateSpeed));
		}
		if (mousePos.Y < (648 - 500) / 2)
		{
			RotateObjectLocal(new Vector3(1, 0, 0), (0.1f * RotateSpeed));
		}
		else
		{
			RotateObjectLocal(new Vector3(1, 0, 0), (0f * RotateSpeed));
		}
	}
}
