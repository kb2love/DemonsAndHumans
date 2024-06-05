using UnityEngine;
using Cinemachine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float gravity = -9.81f;
    [SerializeField] float moveSpeed = 2.5f;
    [SaveDuringPlay] float runSpeed = 5.0f;
    [SerializeField] float jumpForce = 5.0f;
    private Camera _camera;
    private Animator animator;
    private CharacterController ch;
    private float mvX = 0, mvZ = 0;
    private float mvY = 0;
    private const float _threshold = 0.01f;
    public bool toggleCameraRotation;
    private float smoothness = 10.0f;
    private bool isRun;
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        ch = GetComponent<CharacterController>();
        _camera = Camera.main;
    }

    void Update()
    {

        mvX = Input.GetAxis("Horizontal");
        mvZ = Input.GetAxis("Vertical");
        animator.SetFloat("moveX", mvX);
        animator.SetFloat("moveY", mvZ);

        if (!ch.isGrounded)
        {
            mvY += gravity * Time.deltaTime;
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                mvY = jumpForce;
            }
        }
        if(Input.GetKey(KeyCode.LeftAlt))
        {
            toggleCameraRotation = true;
        }
        else
        {
            toggleCameraRotation = false;
        }
        if(Input.GetKey(KeyCode.LeftShift))
        {
            isRun = true;   
        }
        else
        {
            isRun = false;
        }
        Vector3 dir = new Vector3(mvX, mvY, mvZ);
        dir = transform.TransformDirection(dir);
        ch.Move(dir * Time.deltaTime * (isRun ? runSpeed : moveSpeed));
    }
    private void LateUpdate()
    {
        if(!toggleCameraRotation)
        {
            Vector3 playerRotate = Vector3.Scale(_camera.transform.forward, new Vector3(1, 0, 1));
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(playerRotate), Time.deltaTime * smoothness);
        }
    }

}
