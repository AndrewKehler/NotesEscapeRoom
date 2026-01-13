using Godot;
using System;

public partial class InspectContainer : CenterContainer
{

	private Button exit;
	private TextureRect exitTxtr;
	private TextureRect itemTxtr;
	private string texturePath;
	private HBoxContainer hBox;
	private Node childInstance;
	private bool hasChildInst = false;

	public override void _Ready()
	{
		Globals.Inventory.InventoryUpdated += OnInventoryUpdate;
		hBox = GetNode<HBoxContainer>("Panel/HBoxContainer");
		exit = GetNode<Button>("Panel/HBoxContainer/Button");
		exitTxtr = GetNode<TextureRect>("Panel/HBoxContainer/Button/TextureRect");
		itemTxtr = GetNode<TextureRect>("Panel/HBoxContainer/ScenelessInspection");
		this.Visible = false;
		exit.Pressed += OnExit;
		exit.MouseEntered += OnHover;
		exit.MouseExited += OnUnHover;
		exit.FocusMode = FocusModeEnum.None;
		exitTxtr.Modulate = new Color(1, 1, 1, .5f);


	}


	public void OnInspection(string selectedItem)
	{

		removeChild(childInstance);
		if (!Globals.Inventory.Get(Globals.selectedPanel).HasScene)
		{
			Globals.inspectionScene = "Sceneless";
			texturePath = "res://Sprites/" + selectedItem + ".png";
			itemTxtr.Texture = GD.Load<Texture2D>(texturePath);
			itemTxtr.Visible = true;

		}
		else
		{
			hasChildInst = true;
			itemTxtr.Visible = false;
			Globals.inspectionScene = selectedItem + "Inspection";
			PackedScene childScene = GD.Load<PackedScene>("res://Scenes/" + Globals.inspectionScene + ".tscn");
			childInstance = childScene.Instantiate();
			hBox.AddChild(childInstance);
		}
		this.Visible = true;



	}
	private void removeChild(Node child)
	{
		try
		{
			if (hasChildInst)
			{
				hBox.RemoveChild(child);
				childInstance.QueueFree();
				hasChildInst = false;
			}
		}
		catch (NullReferenceException) { }
	}
	public void OnExit()
	{
		removeChild(childInstance);
		Globals.inspectionScene = "";
		this.Visible = false;
	}

	private void OnHover()
	{
		exitTxtr.Modulate = new Color(1, 1, 1, 1);
	}

	private void OnUnHover()
	{
		exitTxtr.Modulate = new Color(1, 1, 1, .5f);
	}

	private void OnInventoryUpdate()
	{
		if (Globals.inspectionScene.Equals(""))
		{
			this.Visible = false;
		}
	}
}
