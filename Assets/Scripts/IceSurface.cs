using System.Collections;
using UnityEngine;

public class IceSurface : MonoBehaviour
{
    [SerializeField]
    private float surfaceLifetime = 4.5f;

    public delegate void OnDestroy();
    public event OnDestroy OnDestroyHandler;

    private void Start()
    {
        StartCoroutine(SurfaceCoroutine());
    }

    private IEnumerator SurfaceCoroutine()
    {
        yield return new WaitForSeconds(surfaceLifetime);

        OnDestroyHandler();
        Destroy(gameObject);
    }
}
