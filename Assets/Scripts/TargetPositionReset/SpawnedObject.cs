using UnityEngine;

public class SpawnedObject : MonoBehaviour
{
    public delegate void OutOfBounds();
    public event OutOfBounds outOfBounds;
    public void BoundsCrossed() => outOfBounds();
}
