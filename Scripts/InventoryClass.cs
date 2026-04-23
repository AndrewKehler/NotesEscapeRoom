using Godot;
using System;
using System.Dynamic;
using System.Security.Cryptography;
[GlobalClass]
public partial class InventoryClass : Resource
{

	private static ItemClass[] Inventory;
	private static int Count;
	[Signal]
	public delegate void InventoryUpdatedEventHandler();

	public InventoryClass()
	{
		Inventory = new ItemClass[8];
		Count = 0;
	}

	public void Add(ItemClass item)
	{
		if (Count < Inventory.Length)
		{
			Inventory[Count] = item;
			shuffle();
			EmitSignal(SignalName.InventoryUpdated);
			Count++;
		}
	}
	public ItemClass Get(int index)
	{
		return Inventory[index];
	}
	public ItemClass RemoveAt(int index)
	{
		ItemClass temp = null;
		if (Inventory[index] != null)
		{
			temp = Inventory[index];
			Inventory[index] = null;
			Count--;
			
		}
		shuffle();
		EmitSignal(SignalName.InventoryUpdated);
		return temp;
	}
	//removes an item from the inven by searching for an ID.
	//Returns an exception or the removed item. 
	public ItemClass RemoveID(string id)
	{
		ItemClass temp = null;
		for (int i = 0; i < Inventory.Length; i++)
		{
			if (Inventory[i] != null && Inventory[i].ItemID.Equals(id))
			{
				temp = Inventory[i];
				Inventory[i] = null;
				Count--;
				shuffle();
				EmitSignal(SignalName.InventoryUpdated);
				return temp;
			}
		}
		throw new Exception("item not found.");
	}

	//Swaps an input item with the item at a specified index.
	//outputs the item that was at the index before the swap.
	public ItemClass changeAt(int index, ItemClass item)
	{
		ItemClass temp = Inventory[index];
		Inventory[index] = item;
		EmitSignal(SignalName.InventoryUpdated);
		return temp;
	}

	public string readOut()
	{
		string str = "";
		for (int i = 0; i < Count; i++)
		{
			str += Inventory[i].ItemID + ", ";
		}
		return str;
	}

	private void shuffle()
	{	
		ItemClass temp = null;
		for (int i = 0; i < Inventory.Length; i++)
		{
			int increase = 1;
			if (Inventory[i] == null)
			{
				
				try
					{
					while (Inventory[i + increase] == null)
					{
						increase++;
					}
						temp = Inventory[i + increase];
						Inventory[i] = temp;
						Inventory[i + increase] = null;
					}
					catch (IndexOutOfRangeException)
					{

					}
			}
		}
	}
	
	public int getCount()
	{
		return Count;
	}

}
