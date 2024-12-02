using UnityEngine;

public class InteractTargetButton : MonoBehaviour, IInteractible, IDisplayable
{
    [Tooltip("Objects to reset target spawn.\nPass in game objects which scripts implements IInteract target interface")]
    [SerializeField]
    private GameObject[] interactObjects;

    public string Name() => "Reset button";

    public string Description() => "Press to reset cubes";

    public void Interact(PlayerController player)
    {
        foreach (var interactObject in interactObjects)
        {
            var interactTarget = interactObject.GetComponent<MonoBehaviour>() as IInteractTarget;

            if (interactTarget != null)
            {
                interactTarget.OnInteract();
            }
        }
    }
}
