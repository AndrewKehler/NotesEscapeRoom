using Godot;
using System;
[GlobalClass]
public partial class ItemClass : Resource
{
    public string ItemID;
    public Texture2D Texture;
    public bool Collected;
    public bool HasScene;

    public ItemClass(string itemID, Texture2D texture, bool collected, bool hasScene)
    {
        ItemID = itemID;
        Texture = texture;
        Collected = collected;
        HasScene = hasScene;
    }


}
