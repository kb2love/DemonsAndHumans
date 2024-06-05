using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] public Transform playerTr;
    [SerializeField] private float followSpeed = 10f;
    [SerializeField] [Range(0f, 1000f)] private float sensitivity = 100.0f;
    [SerializeField] private float clampAngle = 70.0f;

    private float rotX, rotY;

    [SerializeField] private Transform realCamera;
    [SerializeField] Vector3 dirNormalized;
    [SerializeField] Vector3 finalDir;
    [SerializeField] float minDistance;
    [SerializeField] float maxDistance;
    [SerializeField] float finalDistance;
    [SerializeField] private float smoothness;
    private int plLayer = 1 << 3;
    void Start()
    {
        rotX = transform.localRotation.eulerAngles.x;
        rotY = transform.localRotation.eulerAngles.y;

        if(realCamera != null )
        {
            dirNormalized = realCamera.localPosition.normalized;
            finalDistance = realCamera.localPosition.magnitude;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        rotX += -Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
        rotY += Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;

        rotX = Mathf.Clamp(rotX, minDistance-clampAngle, clampAngle);
        Quaternion rot = Quaternion.Euler(rotX, rotY, 0);
        transform.rotation = rot;
    }
    private void LateUpdate()
    {
        if(playerTr != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, playerTr.position, followSpeed * Time.deltaTime);
            finalDir = transform.TransformPoint(dirNormalized * maxDistance);
            RaycastHit hit;
            if (Physics.Linecast(transform.position, finalDir, out hit, ~plLayer))
            {
                //finalDistance = Mathf.Clamp(hit.distance, minDistance, maxDistance);
                finalDistance = hit.distance;
            }
            else
            {
                finalDistance = maxDistance;
            }
            realCamera.localPosition = Vector3.Lerp(realCamera.localPosition, dirNormalized * finalDistance, Time.deltaTime * smoothness);

        }
    }
}
