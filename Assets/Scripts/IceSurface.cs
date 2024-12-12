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
        //StartCoroutine(SurfaceCoroutine());
        Invoke(nameof(SurfaceDestroyed), surfaceLifetime);
    }

    private void SurfaceDestroyed()
    {

        OnDestroyHandler?.Invoke();
        Destroy(gameObject);
    }

    private IEnumerator SurfaceCoroutine()
    {
        yield return new WaitForSeconds(surfaceLifetime);

        var name = gameObject.name;
        OnDestroyHandler();
        Destroy(gameObject);
    }
}
