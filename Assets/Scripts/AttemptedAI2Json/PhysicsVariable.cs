using UnityEngine;

public class PhysicsVariable : MonoBehaviour
{
    public bool value = true;

    public void SetValue(bool x){ value = x; }

    public bool GetValue(){ return value; }
}
