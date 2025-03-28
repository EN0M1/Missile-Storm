using UnityEngine;

[CreateAssetMenu(fileName = "Planes", menuName = "Scriptable Objects/Planes")]
public class Planes : ScriptableObject
{
    public Plane[] planes;

    public int getCount()
    {
        return planes.Length;
    }

    public Plane getPlane(int index)
    {
        return planes[index];
    }
}
