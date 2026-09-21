using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonCamera : MonoBehaviour
{
    [Header("따라갈 대상")]

    [SerializeField] private Transform target;

    [Header("카메라 위치")]
    [SerializeField] private float distance = 7f;
    [SerializeField] private float height = 1.5f;

    [Header("마우스 회전")]
    [SerializeField] private float mouseSensitivuty = 0.12f;
    [SerializeField] private float minVerticalAngle = -30f;
    [SerializeField] private float maxVerticalAngle = 60f;

    [Header("마우스 스크롤")]
    [SerializeField] private float zoomSpeed = 0.01f;
    [SerializeField] private float minDistance = 2f;
    [SerializeField] private float maxDistance = 10f;

    private float yaw;
    private float pitch = 15f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {


        Mouse mouse = Mouse.current;

        if(mouse == null)
        {
            return;
        }

        Vector2 mouseDelta = mouse.delta.ReadValue();

        yaw += mouseDelta.x * mouseSensitivuty;
        pitch -= mouseDelta.y * mouseSensitivuty;
        pitch = Mathf.Clamp(pitch, minVerticalAngle, maxVerticalAngle);

        // 마우스 휠 확대와 축소

        float scroll = mouse.scroll.ReadValue().y;

        distance -= scroll * zoomSpeed;
        distance = Mathf.Clamp(distance, minDistance, maxDistance);
    }

    private void LateUpdate()
    {
        if (target == null)
        {
            return;
        }

        // 카메라가 바라볼 위치
        Vector3 lookPoint = target.position + Vector3.up * height;

        // yaw 와 pitch룰 실제 회ㅏ전값으로 변환
        Quaternion orbitRotation = Quaternion.Euler(pitch, yaw, 0f);

        // 화전 방향을 기준으로 player 뒤쪽 위치 계산
        Vector3 cameraOffset = orbitRotation * new Vector3(0f, 0f, -distance);

        // Player 주변의 계산된 위치로 이동
        transform.position = lookPoint + cameraOffset;

        // Player 중심을 바라본다
        transform.LookAt(lookPoint);

    }
}
