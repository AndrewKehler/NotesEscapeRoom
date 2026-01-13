using Godot;
using System;

public partial class InventoryPanel : TextureButton
{
	[Export] public int _index;
	[Export] public NodePath _panelPath;
	private Panel panel;
	[Export] public NodePath _spritePath;
	private Sprite2D sprite;

	[Export] public NodePath _inspectPath;
	[Export] public NodePath _inspectTexPath;
	private Button inspectBtn;
	private TextureRect inspectTxtr;

	bool selected;
	StyleBoxFlat style;
	string texturePath;


	[Signal]
	public delegate void InspectItemEventHandler( string selected);

	public override void _Ready()
	{
		this.Pressed += OnPress;
		Globals.Inventory.InventoryUpdated += OnInventoryUpdate;

		panel = GetNode<Panel>(_panelPath);
		style = panel.GetThemeStylebox("panel").Duplicate() as StyleBoxFlat;
		panel.MouseFilter = Control.MouseFilterEnum.Ignore;

		sprite = GetNode<Sprite2D>(_spritePath);

		inspectBtn = GetNode<Button>(_inspectPath);
		inspectTxtr = GetNode<TextureRect>(_inspectTexPath);
		inspectTxtr.Modulate = new Color(1, 1, 1, .5f);
		inspectBtn.Visible = false;
		inspectBtn.FocusMode = FocusModeEnum.None;

		inspectBtn.Pressed += OnInspectPress;
		inspectBtn.MouseEntered += OnInspectHover;
		inspectBtn.MouseExited += OnInspectHoverOff;

		var inspectContainer = GetNode<CenterContainer>("../../../../MarginContainer/InspectContainer");
		this.Connect(SignalName.InspectItem, new Callable(inspectContainer, nameof(InspectContainer.OnInspection)));

		if (style != null)
		{

			style.BgColor = new Color(0.194f, 0.194f, 0.194f);
			style.SetCornerRadiusAll(10);

			panel.AddThemeStyleboxOverride("panel", style);
		}
	}



	public override void _Process(double delta)
	{
		if (Globals.selectedPanel != _index)
		{
			selected = false;
			style.SetBorderWidthAll(0);
			inspectBtn.Visible = false;
		}
	}

	private void OnInventoryUpdate()
	{
		if (Globals.Inventory.Get(_index) != null)
		{
			texturePath = "res://Sprites/" + Globals.Inventory.Get(_index).ItemID + ".png";
			sprite.Texture = GD.Load<Texture2D>(texturePath);
		}
		else
		{
			sprite.Texture = null;
			Globals.selectedItem = null;
			style.SetBorderWidthAll(0);
			inspectBtn.Visible = false;
		}
	}

	private void OnPress()
	{
		if (!selected)
		{
			try
			{
				selected = true;
				Globals.selectedPanel = _index;
				Globals.selectedItem = Globals.Inventory.Get(_index).ItemID;
				style.SetBorderWidthAll(5);
				inspectBtn.Visible = true;
				GD.Print(Globals.selectedPanel, " ", Globals.selectedItem);
			}
			catch (NullReferenceException)
			{
			}
		}
		else
		{
			selected = false;
			Globals.selectedPanel = -1;
			style.SetBorderWidthAll(0);
			inspectBtn.Visible = false;
		}

	}


	private void OnInspectPress()
	{

		EmitSignal(SignalName.InspectItem, Globals.selectedItem);
	}

	private void OnInspectHover()
	{
		inspectTxtr.Modulate = new Color(1, 1, 1, 1);
	}
	private void OnInspectHoverOff()
	{
		inspectTxtr.Modulate = new Color(1, 1, 1, .5f);
	}
}
