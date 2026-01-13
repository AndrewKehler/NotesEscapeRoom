using Godot;
using System;

public partial class UIButton : Button
{
	[Export] public NodePath TexturePath;
	[Export] public String[] VisibleIn;
	private TextureRect _textureRect;

	public override void _Ready()
	{
		FocusMode = FocusModeEnum.None;

		_textureRect = GetNode<TextureRect>(TexturePath);
		_textureRect.Modulate = new Color(1, 1, 1, .5f);
		MouseEntered += OnHover;
		MouseExited += OnUnhover;
		Pressed += OnPress;

	}

	public override void _Process(double delta)
	{
		if (Globals.IsIn(Globals.camData.Location, VisibleIn))
		{
			this.Disabled = false;
			_textureRect.Visible = true;
		}
		else
		{
			this.Disabled = true;
			_textureRect.Visible = false;
		}
	}




	private void OnPress()
	{
		_textureRect = GetNode<TextureRect>(TexturePath);
		_textureRect.Modulate = new Color(1, 1, 1, 1);
	}


	private void OnUnhover()
	{
		_textureRect = GetNode<TextureRect>(TexturePath);
		_textureRect.Modulate = new Color(1, 1, 1, .5f);
	}


	private void OnHover()
	{
		_textureRect = GetNode<TextureRect>(TexturePath);
		_textureRect.Modulate = new Color(1, 1, 1, 1);
	}
}
