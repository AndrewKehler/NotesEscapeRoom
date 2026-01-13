using Godot;
using System;

public partial class AreaInteraction : Area3D
{
	[Export] String VisibleOn;
	[Export] Vector3 MoveTo;
	[Export] Vector3 RotateTo;
	[Export] bool ReverseOnClick;
	[Export] bool ReverseOnLeave;
	[Export] Vector3 OgPos;
	[Export] Vector3 OgRot;
	private bool clickable = true;

	public override void _Ready()
	{
		this.InputEvent += on_input_event;
	}


	public override void _Process(double delta)
	{
		if (Globals.camData.Location != VisibleOn)
		{
			clickable = false;
			if (ReverseOnLeave == true)
			{
				this.Rotation = OgRot; 
				this.Position = OgPos;
				   
			}
		}
		else
		{
			clickable = true;
		}
	}

	private void on_input_event(Node camera, InputEvent @event, Vector3 eventPosition, Vector3 normal, long shapeIdx)
	{
		if (@event is InputEventMouseButton eventMouse && clickable == true)
		{
			if (eventMouse.Pressed && eventMouse.ButtonIndex == MouseButton.Left)
			{
				if (ReverseOnClick == true && this.Position != OgPos)
				{
					this.Rotation = OgRot;
					this.Position = OgPos;
					
				}
				else
				{   
					this.Rotation = RotateTo;
					this.Position = MoveTo;
					
				}
			}
		}
	}
}
