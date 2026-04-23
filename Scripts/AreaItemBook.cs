using Godot;
using System;

public partial class AreaItemBook : AreaItem
{
	public override void on_input_event(Node camera, InputEvent @event, Vector3 eventPosition, Vector3 normal, long shapeIdx)
	{
		if (@event is InputEventMouseButton eventMouse)
		{
			if (eventMouse.Pressed && eventMouse.ButtonIndex == MouseButton.Left)
			{
				GD.Print(Globals.selectedItem);
				
				try
				{   
					if(!Globals.hasBook){
						GD.Print("b1");
						
						Item.Collected = true;
						Globals.hasBook = true;
						Globals.Inventory.Add(Item);
						GetChild(this.GetChildCount() - 1).QueueFree();
						Item = null;
					}
					else
					{
						if(Item != null && Globals.IsIn(Globals.selectedItem, allowInstance))
						{   
							GD.Print("b2");
							Item.Collected = true;
							
							var old = Globals.Inventory.changeAt(Globals.selectedPanel, Item);
							Globals.selectedItem = Item.ItemID;
							old.Collected = false;
							GetChild(this.GetChildCount()-1).QueueFree();
							setItem(old);
		
						}else if(Item == null && Globals.IsIn(Globals.selectedItem, allowInstance))
						{
							GD.Print("b3");
							Item = Globals.Inventory.Get(Globals.selectedPanel);
							setItem(Item);
							Item.Collected = false;
							Globals.Inventory.RemoveAt(Globals.selectedPanel);
							Globals.hasBook = false;
							
						}
						GD.Print(Globals.Inventory.getCount());
					}
				}
				catch (NullReferenceException)
				{
					GD.Print("bull");
				}
				
			}
		}
	}

}
