using UnityEngine;
using UnityEngine.Events;

public class TriggerScript : MonoBehaviour
{
    [SerializeField]
    UnityEvent onPlayerEnter;
    [SerializeField]
    UnityEvent onPlayerStay;
    [SerializeField]
    UnityEvent onPlayerExit;


    private void OnTriggerEnter(Collider other)
    {
        if(other.name=="Player")  onPlayerEnter?.Invoke();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.name == "Player") onPlayerStay?.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Player") onPlayerExit?.Invoke();
    }
}
