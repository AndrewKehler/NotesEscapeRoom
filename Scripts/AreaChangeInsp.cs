using Godot;
using System;

public partial class AreaChangeInsp : Area2D
{
	[Export] public NodePath stepPath;
	private TextureRect parent;
	private TextureRect step;

	public override void _Ready()
	{
		parent = GetParent() as TextureRect;
		step = GetNode<TextureRect>(stepPath);
		parent.Visible = true;
		step.Visible = false;
		InputEvent += On_input_event;
	}

	private void On_input_event(Node viewport, InputEvent @event, long shapeIdx)
	{
		if (@event is InputEventMouseButton eventMouse)
		{
			if (eventMouse.Pressed && eventMouse.ButtonIndex == MouseButton.Left)
			{
				parent.Visible = false;
				step.Visible = true;
				
			}
		}
	}

}
