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
        if(UnityEngine.Input.GetMouseButtonDown(0))
        {
            Shoot();
        }

        float hor = UnityEngine.Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up, hor * Time.deltaTime * turnSensibility);

        float ver = UnityEngine.Input.GetAxis("Vertical");
        barrel.Rotate(Vector3.right, ver * Time.deltaTime * turnSensibility * -1);
        Vector3 angle = barrel.localEulerAngles;

        if (angle.x > 180f)
        {
            angle.x -= 360f;
        }
        else if(angle.x<-180f)
        {
            angle.x += 360f;
        }

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
        this.enabled = false;
        PlayerRef.SetActive(true);
        cannonCamera.SetActive(false);
        hud.SetActive(true);
    }

    public void Shoot()
    {
        if(shoots) return;

        shoots = true;
        animator.SetBool("Shoots", true);

        var projectile = Instantiate(projectilePrefabs[currentProjectileIndex], projectileSpawnPoint.position, Quaternion.identity);

        var projectileRb = projectile.GetComponent<Rigidbody>();
        projectileRb.linearVelocity = projectileSpawnPoint.forward * projectileSpeed;
    }

    public void StopShoot()
    {
        shoots = false;
        animator.SetBool("Shoots", false);
    }
}
