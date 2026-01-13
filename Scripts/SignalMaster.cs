using Godot;
using System;
using System.Numerics;

public partial class SignalMaster : Node3D
{
	public static Camera3D _camera;
	[Export] public NodePath CameraPath;
	private Button _buttonLeft;
	[Export] public NodePath ButtonLPath;
	private Button _buttonRight;
	[Export] public NodePath ButtonRPath;

	private Button _buttonDown;
	[Export] public NodePath ButtonDPath;
	private int wall;


	public override void _Ready()
	{
		_camera = GetNode<Camera3D>(CameraPath);

		_buttonLeft = GetNode<Button>(ButtonLPath);
		_buttonRight = GetNode<Button>(ButtonRPath);
		_buttonDown = GetNode<Button>(ButtonDPath);

		_buttonLeft.Pressed += OnButtonLeftPressed;
		_buttonRight.Pressed += OnButtonRightPressed;
		_buttonDown.Pressed += OnButtonDownPressed;

		SetCameraPosition(Globals.camData.CamR, Globals.camData.CamP);
		SetCameraFOV(Globals.camData.FOV);
		wall = 0;
		Globals.camData.Location = (wall % 4).ToString();

	}

	//defines what should happen on the left button press.
	private void OnButtonLeftPressed()
	{
		ChangeCameraPosition(new Godot.Vector3(0, 1.571f, 0), new Godot.Vector3(0, 0, 0));
		wall--;
		if (wall < 0)
		{
			wall += 4;
		}
		Globals.camData.Location = (wall % 4).ToString();
	}

	private void OnButtonRightPressed()
	{
		ChangeCameraPosition(new Godot.Vector3(0, -1.571f, 0), Globals.EmpVec);
		wall++;
		Globals.camData.Location = (wall % 4).ToString();
	}

	private void OnButtonDownPressed()
	{
		CameraData temp = Globals.camStack.Pop();
		SetCameraPosition(temp.CamR, temp.CamP);
		SetCameraFOV(temp.FOV);
		Globals.camData.SetAll(temp.CamR, temp.CamP, temp.Location, temp.FOV);
	}



	//sets the camera position and then updates the global camera position variable.
	public static void SetCameraPosition(Godot.Vector3 rotation, Godot.Vector3 position)
	{
		_camera.Rotation = rotation;
		_camera.Position = position;
		Globals.camData.CamR = _camera.Rotation;
		Globals.camData.CamP = _camera.Position;

	}

	//changes the camera position by a specified amount and then updates the global camera position variable.
	public static void ChangeCameraPosition(Godot.Vector3 rotation, Godot.Vector3 position)//adds to the position
	{
		_camera.Rotation += rotation;
		_camera.Position += position;
		Globals.camData.CamR = _camera.Rotation;
		Globals.camData.CamP = _camera.Position;
	}

	public static void SetCameraFOV(float fov)
	{
		_camera.Fov = fov;
	}

	
}
