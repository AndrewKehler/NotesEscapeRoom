using Godot;
using System;

public partial class Test : Area3D
{
	public override void _Ready()
	{
		this.InputEvent += on_input_event;
	}

	private void on_input_event(Node camera, InputEvent @event, Vector3 eventPosition, Vector3 normal, long shapeIdx)
	{
		if(@event is InputEventMouseButton eventMouse)
		{
			if (eventMouse.Pressed && eventMouse.ButtonIndex == MouseButton.Left)
			{
				GD.Print("Clicked!");
			}
		}
	}

}
