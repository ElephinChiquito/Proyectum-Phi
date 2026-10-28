using System;
using System.Collections.Generic;
using System.Numerics;

[Serializable]
public class Objeto2D
{
    public string type;
    public Vector3Data position;
    public float scale;
    public Quaternion rotation;
    public string dialog;

    public Objeto2D(){}
    public Objeto2D(string type, Vector3Data position, float scale, Quaternion rotation)
    {
        this.type = type;
        this.position = position;
        this.scale = scale;
        this.rotation = rotation;
    }
    public Objeto2D(string type, Vector3Data position, float scale, Quaternion rotation, string dialog)
    {
        this.type = type;
        this.position = position;
        this.scale = scale;
        this.rotation = rotation;
        this.dialog = dialog;
    }
}
