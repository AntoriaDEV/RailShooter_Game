using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float horizontalInput, verticalInput;

    [Header("Move and Tilt")]
    [SerializeField] float moveSpeed;
    [SerializeField] float tiltAngle = 30;
    [SerializeField] float tiltSpeed;
    Vector3 tilting;

    [Header("Clamp Variables")]
    [SerializeField] float xMin;
    [SerializeField] float xMax;
    [SerializeField] float yMin;
    [SerializeField] float yMax;

    [Header("Shooting")]
    [SerializeField] public Rigidbody playerProjectile;
    [SerializeField] public Transform[] shotSpawns;
    [SerializeField] public bool canShoot;
    [SerializeField] public int bulletSpeed;

    [Header("Inverted Controls")]
    [SerializeField] bool isInverted;

    //Rigidbody rb;

    private void Start()
    {
        canShoot = true;
    }
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        
        if(Input.GetKeyDown(KeyCode.Space) && canShoot)
        {
            canShoot = false;
            Rigidbody shot;
            foreach(Transform t in shotSpawns)
            {
                shot = Instantiate(playerProjectile, t.position, t.rotation) as Rigidbody;
                shot.AddForce(t.forward * bulletSpeed, ForceMode.Impulse);
            } 
            canShoot = true;
        } 

    }

    private void FixedUpdate()
    {
        Movement();
        ClampToScreen();
        HandleTilting();
    }

    void Movement()
    {
        if (isInverted)
        {
            verticalInput *= -1;
        }
        Vector3 _movement = new Vector3(horizontalInput, verticalInput, 0);
        transform.position += _movement * moveSpeed * Time.deltaTime;
    }

    void HandleTilting()
    {
        TiltZ(horizontalInput);
        TiltX(verticalInput);
    }

    void TiltZ(float axis)
    {
        Vector3 targetEulerAngle = transform.localEulerAngles;
        transform.localEulerAngles = new Vector3(targetEulerAngle.x,
            Mathf.LerpAngle(targetEulerAngle.y, axis * tiltAngle, tiltSpeed),
            Mathf.LerpAngle(targetEulerAngle.z, -axis * tiltAngle, tiltSpeed));
    }

    void TiltX(float axis)
    {
        if (isInverted)
        {
            axis *= -1;
        }
        Vector3 targetEulerAngle = transform.localEulerAngles;
        transform.localEulerAngles = new Vector3(Mathf.LerpAngle(targetEulerAngle.x, -axis * tiltAngle, tiltSpeed),
            targetEulerAngle.y,
            targetEulerAngle.z);
    }

    void ClampToScreen()
    {
        Vector3 _position = transform.position;
        _position.x = Mathf.Clamp(_position.x, xMin, xMax);
        _position.y = Mathf.Clamp(_position.y, yMin, yMax);
        transform.position = _position;
    }
}
