using Godot;
using System;

public partial class CameraRoot : Node
{
	
	//3D code takes measurements in METERS not pixels
	[Export]
	public float MOUSE_SENSITIVITY { get; set; } = 0.1f;
	[Export]
	public Node3D _rotationHelper { get; set; }
	[Export]
	public Node3D _camera { get; set; }
	
	//set camera movement and rotation speed
	[Export]
	public int Speed { get; set; } = 1;
	[Export]
	public float RotateSpeed { get; set; } = 0.5f;
	
	public bool triggered { get; set; } = false;
	
	
	//set targetvelocity to 0
	private Vector3 _targetVelocity = Vector3.Zero;
	private Vector3 direction;
	 
	public override void _PhysicsProcess(double delta)
	{
		if (triggered != true)
		{
			//_rotationHelper = GetNode<Node3D>("CameraPivot");
			Input.MouseMode = Input.MouseModeEnum.Captured;
			triggered = true;
		}
		
		ProcessInput(delta);
		ProcessMovement(delta);
		
	}
	
	public void ProcessInput(double delta)
	{
		//MOVEMENT
		
		//local variable for input direction
		direction = Vector3.Zero;
		Transform3D cameraTransform = _camera.GetGlobalTransform();
		//GD.Print(_camera.GetGlobalTransform());
		
		Vector2 inputMovement = Vector2.Zero;
		
		//code left/right (x) movement
		if (Input.IsActionPressed("move_right"))
		{
			inputMovement.X += 1.0f;
		}
		if (Input.IsActionPressed("move_left"))
		{
			inputMovement.X -= 1.0f;
		}
		//code forward/backward (z) movement
		if (Input.IsActionPressed("move_forward"))
		{
			inputMovement.Y += 1.0f;
		}
		if (Input.IsActionPressed("move_backward"))
		{
			inputMovement.Y -= 1.0f;
		}
		
		//normalize inputMovement
		inputMovement = inputMovement.Normalized();
		
		direction += -cameraTransform.Basis.Z * inputMovement.Y;
		direction += cameraTransform.Basis.X * inputMovement.X;
		
		  // Moving the camera and the camera pivot
		_camera.GlobalTranslate(direction);
		_rotationHelper.GlobalTranslate(direction);
		//Vector3 position = GetGlobalPosition();
		//position += _targetVelocity;
		//SetGlobalPosition(position);
		//Translate(direction);
		
		////code up/down (y) movement
		//if (Input.IsActionPressed("move_up"))
		//{
			//direction.Y += 1.0f;
		//}
		//if (Input.IsActionPressed("move_down"))
		//{
			//direction.Y -= 1.0f;
		//}
}
	
	public void ProcessMovement(double delta)
	{
		direction.Y = 0;
		direction = direction.Normalized();
	}
	
	//ROTATION
	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventMouseMotion && Input.GetMouseMode() == Input.MouseModeEnum.Captured)
		{
			InputEventMouseMotion mouseEvent = @event as InputEventMouseMotion;
			//_rotationHelper.RotateX(Mathf.DegToRad(-mouseEvent.Relative.Y * MOUSE_SENSITIVITY));
			_rotationHelper.RotateObjectLocal(new Vector3(1, 0, 0), (Mathf.DegToRad(-mouseEvent.Relative.Y * MOUSE_SENSITIVITY)));
			//RotateY(Mathf.DegToRad(-mouseEvent.Relative.X * MOUSE_SENSITIVITY));
			_rotationHelper.RotateObjectLocal(new Vector3(0, 1, 0), (Mathf.DegToRad(-mouseEvent.Relative.X * MOUSE_SENSITIVITY)));
			
			Vector3 cameraRot = _rotationHelper.RotationDegrees;
			cameraRot.X = Mathf.Clamp(cameraRot.X, -70, 70);
			_rotationHelper.RotationDegrees = cameraRot;
		}
	}
}
