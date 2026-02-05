using Godot;
using System;

public partial class Building : RigidBody3D
{
	[Export]
	public float Width { get; set; }
	[Export]
	public float Depth { get; set; }
	[Export]
	public float Height { get; set; }
	
	public bool MouseIn { get; set; }
	
	//define initialisation method
	public void Initialise(Vector3 StartPosition, float width, float depth, float height)
	{
		//place building at StartPosition
		Position = StartPosition;
		Width = width;
		Depth = depth;
		Height = height;
		//test
		var scale2 = GetScale();
		GD.Print(scale2);
		
		Vector3 scaleVector = new Vector3(Width, Depth, Height);
		CollisionShape3D collider = GetNode<CollisionShape3D>("CollisionShape3D");
		MeshInstance3D mesh = GetNode<MeshInstance3D>("MeshInstance3D");
		
		collider.SetScale(scaleVector);
		mesh.SetScale(scaleVector);
	}
	
	//this may not be needed in future
	private void OnVisibleOnScreenNotifier3DScreenExited()
	{
		//destroy instance of building when its onScreen node leaves the screen
		QueueFree();
	}
	
	//define force applier
	public void AddForce(Vector3 forceDirection, Vector3 forcePosition)
	{
		ApplyForce(forceDirection, forcePosition);
	}
	
	//check if the mouse is in the building or not
	public void OnMouseEntered()
	{
		MouseIn = true;
	}
	
	public void OnMouseExited()
	{
		MouseIn = false;
	}
	
	//physics process
	public override void _PhysicsProcess(double delta)
	{
		////move the building via click and drag
		//var mousePosition = GetViewport().GetMousePosition();
		//
		//if (MouseIn == true && Input.IsActionPressed("drag_building"))
		//{
			//Position = mousePosition;
		//}
		
	}
	
	////define physics calculation - Force is [strength, x direction, y direction, z direction]
	//public float CalculateStress(Mesh CollisionMesh, Godot.Collections.Array Force)
	//{
		////create a new mesh data tool called mdt
		//var mdt = new MeshDataTool();
		//
		////create a new array mesh and add surfaces to it using triangle type and box shape
		//var mesh = new ArrayMesh();
		//mesh.AddSurfaceFromArrays(Mesh.PrimitiveType.Triangles, new BoxMesh().GetMeshArrays());
		//mdt.CreateFromSurface(mesh, 0);
		//
		////iterate through the vertices on the mesh
		//for (int i = 0; i < mdt.GetVertexCount(); i++)
		//{
			////get the vertex to be calculated on
			//Vector3 vertex = mdt.GetVertex(i);
			//
			////perform physics calculation - round force directions to nearest x / y / z to determine direction
			////xz is the horizontal plane, y is vertical
			////all force xyz numbers will be between 0-1 to find their strength in that direction
			//for (int j = 1; j < 4; j++)
			//{
				////round all xyz directions to 1 or 0
				//Force[j] = Force[j].Round();
			//}
			//
			////shear stress = Shear force / cross-sectional area - works if the force is perpendicular to the side of the building
			//float crossArea = Width*Depth;
			//
			//
		//}
		//
		//return 0f;
	//}
}
