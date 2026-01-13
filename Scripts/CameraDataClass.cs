using Godot;
using System;
[GlobalClass]
public partial class CameraData : Resource
{
    public Godot.Vector3 CamR;
    public Godot.Vector3 CamP;
    public String Location;
    public float FOV;

    public CameraData(Vector3 camR, Vector3 camP, String location, float fov)
    {
        CamR = camR;
        CamP = camP;
        Location = location;
        FOV = fov;
    }

    public void SetAll(Vector3 camR, Vector3 camP, String location, float fov)
    {
        CamR = camR;
        CamP = camP;
        Location = location;
        FOV = fov;
    }
}