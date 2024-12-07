using UnityEngine;

public class CannonController : MonoBehaviour
{
    [Header("Controlling")]
    [SerializeField]
    private Transform barrel;
    [SerializeField]
    private MainUI UI;

    [Header("Shooting")]
    [SerializeField]
    private Transform projectileSpawnPoint;
    [SerializeField]
    [Tooltip("List of all projectiles to shoot possible")]
    private GameObject[] projectilePrefabs;
    [SerializeField]
    private float projectileSpeed = 80f;
    [SerializeField]
    private float turnSensibility = 8.5f;
    [SerializeField]
    private float barrelMaxAngle = 13f;
    [SerializeField]
    private float barrelMinAngle = -5.5f;

    private Animator animator;
    private int currentProjectileIndex = 0;
    private bool shoots = false;

    public delegate void OnInteract(bool interact);
    public event OnInteract OnInteractHandler;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // TODO INPUT
        if(UnityEngine.Input.GetMouseButtonDown(0))
        {
            Shoot();
        }

        // Rotate Barrel
        float hor = UnityEngine.Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up, hor * Time.deltaTime * turnSensibility);

        // Elevate barrel
        float ver = UnityEngine.Input.GetAxis("Vertical");
        float elevation = ver * Time.deltaTime * turnSensibility * -1;
        UI.ElevateBarrelAim(elevation *-1);
        barrel.Rotate(Vector3.right, elevation );
        Vector3 angle = barrel.localEulerAngles;

        // Adapt from -180-180 angles to 0-360
        if (angle.x > 180f)
        {
            angle.x -= 360f;
        }
        else if(angle.x<-180f)
        {
            angle.x += 360f;
        }

        // Clamp barrel rotation
        barrel.localEulerAngles = new Vector3(Mathf.Clamp(angle.x,  barrelMinAngle, barrelMaxAngle), 0f);

        if (UnityEngine.Input.GetMouseButtonDown(1))
        {
              OnInteractHandler(false);
        }

        if(UnityEngine.Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentProjectileIndex = 0;
        }
        else if(UnityEngine.Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentProjectileIndex = 1;
        }
    }

    public void Shoot()
    {
        if(shoots) return;

        // Start animation
        shoots = true;
        animator.SetBool("Shoots", true);

        // Create projectile
        var projectile = Instantiate(projectilePrefabs[currentProjectileIndex], projectileSpawnPoint.position, Quaternion.identity);

        // Launch projectile
        var projectileRb = projectile.GetComponent<Rigidbody>();
        projectileRb.linearVelocity = projectileSpawnPoint.forward * projectileSpeed;
    }

    public void StopShoot()
    {
        // Reset shoots
        // stop shoot animation
        shoots = false;
        animator.SetBool("Shoots", false);
    }
}
