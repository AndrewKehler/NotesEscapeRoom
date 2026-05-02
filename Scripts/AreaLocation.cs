using Godot;
using System;
using System.Linq;

public partial class AreaLocation : Area3D
{
	[Export] String visibleOn;
	[Export] Vector3 cameraPChange;
	[Export] Vector3 cameraRChange;

	[Export] bool changeFOV;
	[Export] float fovChange;
	[Export] bool isClue;
	[Export] String clueName; 
	[Export] String locationID;
	CollisionShape3D hitBox;

	public override void _Ready()
	{
		hitBox = GetChild(0) as CollisionShape3D;
		this.InputEvent += On_input_event;
	}

	public override void _Process(double delta)
	{
		if (Globals.camData.Location != visibleOn)
		{
			hitBox.Disabled = true;
		}
		else
		{
			hitBox.Disabled = false;
		}
	}

	private void On_input_event(Node camera, InputEvent @event, Vector3 eventPosition, Vector3 normal, long shapeIdx)
	{
		if (@event is InputEventMouseButton eventMouse)
		{
			if (eventMouse.Pressed && eventMouse.ButtonIndex == MouseButton.Left)
			{
				CameraData temp = new(Globals.camData.CamR, Globals.camData.CamP, Globals.camData.Location, Globals.camData.FOV);
				Globals.camStack.Push(temp);
				Globals.camData.Location = locationID;
				Globals.camData.CamR = cameraRChange;
				Globals.camData.CamP = cameraPChange;
				SignalMaster.SetCameraPosition(cameraRChange, cameraPChange);
				if (changeFOV)
				{
					Globals.camData.FOV = fovChange;
					SignalMaster.SetCameraFOV(fovChange);
				}
				if (isClue && !Globals.IsIn(clueName, Globals.clues))
				{
					Globals.clues[Globals.clueCnt] = clueName;
					Globals.clueCnt++;
					
				}
			}
		}
	}
	
}
