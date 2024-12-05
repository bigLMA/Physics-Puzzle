using UnityEngine;

public class CannonController : MonoBehaviour
{
    [Header("Controlling")]
    [SerializeField]
    private GameObject cannonCamera;
    [SerializeField]
    private Transform barrel;
    [SerializeField]
    private GameObject hud;

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
    public GameObject PlayerRef { get; set; }
    private bool shoots = false;

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

        float hor = UnityEngine.Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up, hor * Time.deltaTime * turnSensibility);

        float ver = UnityEngine.Input.GetAxis("Vertical");

        // Rotate barrel
        barrel.Rotate(Vector3.right, ver * Time.deltaTime * turnSensibility * -1);
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
            RemoveControl();
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

    public void RemoveControl()
    {
        // Disable this script and cannon camera
        // Activate player game object
        this.enabled = false;
        PlayerRef.SetActive(true);
        cannonCamera.SetActive(false);
        hud.SetActive(true);
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
