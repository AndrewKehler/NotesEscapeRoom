using Godot;
using System;

public partial class AreaItem : Area3D
{
	[Export] public string ItemID;
	[Export] public Texture2D Texture;
	PackedScene meshScene;
	[Export] public string visibleOn;
	[Export] public bool hasScene = false;
	[Export] public bool isInstancer = false;
	[Export] bool InstTog = false;
	[Export] public string[] allowInstance = [];
	public CollisionShape3D hitBox;

	public ItemClass Item;


	public override void _Ready()
	{
		hitBox = GetChild(0) as CollisionShape3D;
		if(ItemID != null){
			Item = new ItemClass(ItemID, Texture, false, hasScene);
			meshScene = ResourceLoader.Load<PackedScene>("res://Scenes/" + ItemID + ".tscn");
			AddChild(meshScene.Instantiate());
		}
		else
		{
			GD.Print("sdf");
			Item = null;
			meshScene = null;
		}
		this.InputEvent += on_input_event;
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

	public virtual void on_input_event(Node camera, InputEvent @event, Vector3 eventPosition, Vector3 normal, long shapeIdx)
	{
		if (@event is InputEventMouseButton eventMouse)
		{
			if (eventMouse.Pressed && eventMouse.ButtonIndex == MouseButton.Left)
			{
				
				if (!Item.Collected && !isInstancer)
				{	
					Item.Collected = true;
					Globals.Inventory.Add(Item);
					GetChild(this.GetChildCount()-1).QueueFree();
					hitBox.Disabled = true;
				} else if(isInstancer)
				{
					try
					{
						if (Item.Collected && Globals.IsIn(Globals.selectedItem, allowInstance))
						{
							Item.Collected = false;
							setItem(Globals.Inventory.Get(Globals.selectedPanel));
							Globals.Inventory.RemoveAt(Globals.selectedPanel);
							hitBox.Disabled = false;
						}
						else if (!Item.Collected)
						{
							Item.Collected = true;
							Globals.Inventory.Add(Item);
							GetChild(this.GetChildCount() - 1).QueueFree();
						}
					}
					catch (NullReferenceException)
					{
						GD.Print("nul!");
					}
				}
			}
		}
	}

	public void setItem(ItemClass item)
	{
		Item = item;
		ItemID = item.ItemID;
		Texture = item.Texture;
		hasScene = item.HasScene;
		meshScene = ResourceLoader.Load<PackedScene>("res://Scenes/" + ItemID + ".tscn");
		AddChild(meshScene.Instantiate());
	}

	public void removeItem()
	{
		Item = null;
		ItemID = "";
		Texture = null;
		hasScene = false;
		meshScene = null;
		GetChild(this.GetChildCount()-1).QueueFree();
	}

}
