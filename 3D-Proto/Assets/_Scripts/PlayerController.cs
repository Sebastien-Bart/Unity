using UnityEngine;
using Mirror;

public class PlayerController : NetworkBehaviour
{

    public float speed;
    public float jumpForce;
    public int projectileBaseSpeed;

    public Camera cam;
    public GameObject projectilePrefab;
    public Transform projectileSpawnPos;

    private float verticalVel;
    private readonly float gravity = -9.81f * 2.5f;

    private CharacterController controller;

    public override void OnStartLocalPlayer()
    {
        cam.enabled = true;
        cam.GetComponent<AudioListener>().enabled = true;
        cam.GetComponent<MouseLook>().enabled = true;
        AudioListener al = Camera.main.GetComponent<AudioListener>();
        if (al != null)
            al.enabled = false;
        Camera.main.enabled = false;
    }

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (isLocalPlayer)
        {
            Movement();
            Shooting();
        }
    }

    private void Shooting()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            CmdShoot();
        }
    }

    [Command]
    private void CmdShoot()
    {
        Vector3 velocity = cam.transform.forward * projectileBaseSpeed;
        GameObject projectile = Instantiate(projectilePrefab);
        NetworkServer.Spawn(projectile);
        projectile.transform.position = projectileSpawnPos.position;
        projectile.GetComponent<Rigidbody>().AddForce(velocity, ForceMode.Impulse);
        RpcOnShoot();
    }

    [ClientRpc]
    void RpcOnShoot()
    {
        // empty ?
    }

    private void Movement()
    {
        float hMove = Input.GetAxis("Horizontal");
        float vMove = Input.GetAxis("Vertical");

        Vector3 move = transform.right * hMove + transform.forward * vMove;
        move *= speed;

        if (controller.isGrounded && verticalVel < 0f)
        {
            verticalVel = -1f;
        }
        else
        {
            verticalVel += gravity * Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump") && controller.isGrounded)
        {
            verticalVel = Mathf.Sqrt(jumpForce * -2f * gravity);
        }

        move.y = verticalVel;
        controller.Move(move * Time.deltaTime);
    }
}
