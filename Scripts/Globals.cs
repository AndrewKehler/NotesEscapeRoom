using Godot;
using System;
using System.Numerics;
using System.Collections.Generic;

public partial class Globals : Node
{
    public static Godot.Vector3 EmpVec = new(0, 0, 0);
    public static CameraData camData = new CameraData(new(0, 1.571f, 0), new(-0.124f, 1.33f, 0.069f), "", 85.4f);
    public static Stack<CameraData> camStack = new Stack<CameraData>();

    public static InventoryClass Inventory = new InventoryClass();

    public static int selectedPanel = -1;
    public static string selectedItem = "";

    public static string inspectionScene = "";

    public static string[] bookInstances = ["OrangeBook"];

    public static bool hasBook = false;    
    public static bool IsIn(object key, object[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            try
            {
                if (key.Equals(array[i]))
                {
                    return true;
                }
            }
            catch(NullReferenceException) {}
        }   
        return false;
    }

    public static Godot.Vector3 ToDeg(Godot.Vector3 vec)
    {
        Godot.Vector3 temp = new Godot.Vector3(
                Mathf.DegToRad(vec.X),
                Mathf.DegToRad(vec.Y),
                Mathf.DegToRad(vec.Z)
        );
        return temp;
    }
     
     
}
