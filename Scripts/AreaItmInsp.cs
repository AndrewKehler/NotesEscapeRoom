using Godot;
using System;

public partial class AreaItmInsp : Area2D
{
	[Export] string neededItemID;
	[Export] string newItemId;
	[Export] Texture2D newTexture;
	[Export] bool newHasScene;
	[Export] bool removeOld;
	[Export] bool removeSelected;
	private static ItemClass createdItem;
	public override void _Ready()
	{
		InputEvent += On_input_event;
	}
	
	private void On_input_event(Node viewport, InputEvent @event, long shapeIdx)
	{
		if (@event is InputEventMouseButton eventMouse)
		{
			if (eventMouse.Pressed && eventMouse.ButtonIndex == MouseButton.Left && Globals.selectedItem == neededItemID)
			{
				createdItem = new ItemClass(newItemId, newTexture, true, newHasScene);
				if (removeSelected)
				{
					Globals.Inventory.RemoveAt(Globals.selectedPanel);
					Globals.selectedPanel = -1;
				}
				if (removeOld)
				{
					string id = Globals.inspectionScene.Remove(Globals.inspectionScene.Length - 10);
					GD.Print(id);
					Globals.inspectionScene = "";
					Globals.Inventory.RemoveID(id);
	
				}
				Globals.Inventory.Add(createdItem);
				
			}
		}
	}

}
