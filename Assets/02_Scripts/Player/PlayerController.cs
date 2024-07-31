using UnityEngine;
using Cinemachine;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float walkSpeed = 2.5f;
    [SerializeField] float runSpeed = 5.0f;
    [SerializeField] AudioClip walkSound;
    [SerializeField] AudioClip runSound;
    [SerializeField] AudioSource audioSource;
    private float moveSpeed = 0;
    private Camera _camera;
    private Animator animator;
    private CharacterController ch;
    private float mvX = 0, mvZ = 0;
    private float mvY = 0, gravity = -9.8f;
    public bool toggleCameraRotation;
    private float smoothness = 10.0f;
    private bool isStop = false;
    private bool isRunning = false;
    private float stepTimer = 0.0f;
    private float stepInterval = 0.5f; // 걸을 때 소리 간격
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        ch = GetComponent<CharacterController>();
        audioSource = GetComponent<AudioSource>();
        _camera = Camera.main;
        moveSpeed = walkSpeed;
    }

    void Update()
    {
        if (!isStop)
            MoveAndRun();
    }

    private void MoveAndRun()
    {
        mvX = Input.GetAxis("Horizontal");
        mvZ = Input.GetAxis("Vertical");
        animator.SetFloat("moveX", mvX);
        animator.SetFloat("moveZ", mvZ);
        if (!ch.isGrounded)
            mvY += gravity;
        if (Input.GetKey(KeyCode.LeftShift) && mvZ > 0.1f && Mathf.Abs(mvX) < 0.1f)
        {
            animator.SetBool("IsRun", true);
            moveSpeed = runSpeed;
            isRunning = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift) || mvZ < 0.1f || Mathf.Abs(mvX) > 0.1f)
        {
            animator.SetBool("IsRun", false);
            moveSpeed = walkSpeed;
            isRunning = false;
        }
        if (mvZ > 0.1f || Mathf.Abs(mvX) > 0.1f)
        {
            stepTimer += Time.deltaTime;
            if (stepTimer > stepInterval)
            {
                PlayFootstepSound();
                stepTimer = 0.0f;
            }
        }
        Vector3 dir = new Vector3(mvX, mvY, mvZ);
        dir = transform.TransformDirection(dir);
        ch.Move(dir * Time.deltaTime * moveSpeed);
    }

    private void PlayFootstepSound()
    {
        if (isRunning)
        {
            SoundManager.soundInst.EffectSoundPlay(audioSource,runSound);
        }
        else
        {
            SoundManager.soundInst.EffectSoundPlay(audioSource,walkSound);
        }
    }
    private void LateUpdate()
    {
        Vector3 playerRotate = Vector3.Scale(_camera.transform.forward, new Vector3(1, 0, 1));
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(playerRotate), Time.deltaTime * smoothness);
    }
    public void IsStop(bool _isAttack)
    {
        isStop = _isAttack;
    }
}
