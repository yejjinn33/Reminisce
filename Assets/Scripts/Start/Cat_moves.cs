using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat_moves : MonoBehaviour
{
    public float cat_speed = 10.0f;    // 이동 속도
    public float rot_speed = 100.0f;  // 회전 속도
    public float jump_force = 10.0f;  // 기본 점프 힘
    public float max_jump_force = 15.0f; // 최대 점프 힘
    public float jump_charge_speed = 5.0f; // 점프 힘 증가 속도
    public float run_speed_threshold = 15.0f; // 달리기 시작하는 속도 기준
    public float max_velocity = 20.0f; // 최대 이동 속도

    private Animator animator;
    private Rigidbody cat_rigidbody;

    private float moving_velocity;     // 이동 속도
    private float cat_angle;           // 회전 입력
    private bool is_grounded = true;   // 고양이가 땅에 있는지 확인
    private bool is_charging_jump = false; // 점프 힘을 충전 중인지 확인

    void Start()
    {
        animator = GetComponent<Animator>();
        cat_rigidbody = GetComponent<Rigidbody>();
        cat_rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;

        // 회전 고정 (모든 축 회전 방지)
        cat_rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
    }

    void Update()
    {
        moving_velocity = Input.GetAxis("Vertical");
        cat_angle = Input.GetAxis("Horizontal");

        if (Input.GetKey(KeyCode.LeftShift))
        {
            moving_velocity *= 3.0f; // 속도 증가
        }

        float current_speed = Mathf.Abs(moving_velocity * cat_speed);

        // 걷기 애니메이션 활성화/비활성화
        animator.SetBool("walking", current_speed > 0.1f);

        // 달리기 애니메이션
        animator.SetFloat("speed", current_speed > run_speed_threshold ? current_speed : 0f);

        // 점프 힘 충전
        if (Input.GetKey(KeyCode.Space) && is_grounded)
        {
            is_charging_jump = true;
            jump_force += jump_charge_speed * Time.deltaTime;
            jump_force = Mathf.Clamp(jump_force, 5.0f, max_jump_force);
        }

        // 점프 실행
        if (Input.GetKeyUp(KeyCode.Space) && is_charging_jump && is_grounded)
        {
            Jump();
        }
    }

    void FixedUpdate()
    {
        float distance_per_frame = cat_speed * moving_velocity;
        float degrees_per_frame = rot_speed * Time.fixedDeltaTime;

        // 속도 기반 이동 처리
        Vector3 forwardMovement = transform.forward * distance_per_frame;
        forwardMovement = Vector3.ClampMagnitude(forwardMovement, max_velocity); // 최대 속도 제한

        cat_rigidbody.velocity = new Vector3(forwardMovement.x, cat_rigidbody.velocity.y, forwardMovement.z);

        // 회전 처리
        Quaternion rotation = Quaternion.Euler(0.0f, cat_angle * degrees_per_frame, 0.0f);
        cat_rigidbody.MoveRotation(cat_rigidbody.rotation * rotation);
    }

    private void Jump()
    {
        animator.speed = 0.3f;
        cat_rigidbody.AddForce(Vector3.up * jump_force, ForceMode.Impulse);
        is_grounded = false;
        jump_force = 10.0f;
        is_charging_jump = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 가구 또는 바닥과 충돌한 경우
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.layer == LayerMask.NameToLayer("Furniture"))
        {
            Debug.Log("Collided with: " + collision.gameObject.name);
            is_grounded = true;
            animator.speed = 1f; // 애니메이션 복구
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // 가구 또는 바닥에서 떨어진 경우
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.layer == LayerMask.NameToLayer("Furniture"))
        {
            Debug.Log("Left: " + collision.gameObject.name);
            is_grounded = false;
        }
    }
}
