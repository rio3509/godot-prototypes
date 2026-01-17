using Godot;
using System;

public partial class CameraBody3d : CharacterBody3D
{
	//3D code takes measurements in METERS not pixels
	
	//set camera movement speed
	[Export]
	public int Speed { get; set; } = 14;
	
	//set targetvelocity to 0
	private Vector3 _targetVelocity = Vector3.Zero;
	
	public override void _PhysicsProcess(double delta)
	{
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
		if (Input.IsActionPressed("move_forward"))
		{
			direction.Z += 1.0f;
		}
		if (Input.IsActionPressed("move_backward"))
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
			
			GetNode<Marker3D>("CameraPivot").Basis = Basis.LookingAt(direction);
		}
	
		 //Velocities
		_targetVelocity.X = direction.X * Speed;
		_targetVelocity.Z = direction.Z * Speed;
		_targetVelocity.Y = direction.Y * Speed;
		
		  // Moving the camera
		Velocity = _targetVelocity;
		MoveAndSlide();
	}
}
