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
				
				try
				{   
					if(!Globals.hasBook){
						GD.Print("b1");
						Item.Collected = true;
						Globals.hasBook = true;
						Globals.Inventory.Add(Item);
						GetChild(this.GetChildCount() - 1).QueueFree();
					}
					else
					{
						if(!Item.Collected && Globals.IsIn(Globals.selectedItem, allowInstance))
						{   
							GD.Print("b2");
							Item= Globals.Inventory.changeAt(Globals.selectedPanel, Item);
							GetChild(this.GetChildCount()-1).QueueFree();
							setItem(Item);
		
						}else if(Item.Collected && Globals.IsIn(Globals.selectedItem, allowInstance))
						{
							GD.Print("b3");
							
							setItem(Globals.Inventory.Get(Globals.selectedPanel));
							Item.Collected = false;
							Globals.Inventory.RemoveAt(Globals.selectedPanel);
							Globals.hasBook = false;
							
						}
					}
				}catch(NullReferenceException){}
				
			}
		}
	}

}
