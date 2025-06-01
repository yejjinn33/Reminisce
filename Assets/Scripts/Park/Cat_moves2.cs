using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat_moves2 : MonoBehaviour
{
    public float cat_speed = 10.0f;    // 이동 속도
    public float rot_speed = 100.0f;  // 회전 속도
    public float jump_force = 5.0f;   // 기본 점프 힘
    public float max_jump_force = 15.0f; // 최대 점프 힘
    public float jump_charge_speed = 5.0f; // 점프 힘 증가 속도
    public float run_speed_threshold = 15.0f; // 달리기 시작하는 속도 기준
    public float max_velocity = 20.0f; // 최대 이동 속도
    public float push_force = 5.0f;    // 쓰레기봉지를 밀 힘
    public float interaction_range = 5.0f; // 쓰레기봉지와의 상호작용 범위

    private Animator animator;
    private Rigidbody cat_rigidbody;

    private float moving_velocity;    // 이동 속도
    private float cat_angle;          // 회전 입력
    private bool is_grounded = true;  // 고양이가 땅에 있는지 확인
    private bool is_charging_jump = false; // 점프 힘을 충전 중인지 확인
    //private bool is_slowed = false;   // 느려진 상태 확인
    private float original_speed;     // 원래 속도 저장
    private int groundContactCount = 0; // 땅과의 접촉을 추적

    private HashSet<GameObject> clickedItems = new HashSet<GameObject>();

    void Start()
    {
        animator = GetComponent<Animator>();
        cat_rigidbody = GetComponent<Rigidbody>();
        cat_rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;

        // 모든 축 회전 고정
        cat_rigidbody.constraints = RigidbodyConstraints.FreezeRotation;

        // 초기 속도 저장
        original_speed = cat_speed;
    }

    void Update()
    {
        moving_velocity = Input.GetAxis("Vertical");
        cat_angle = Input.GetAxis("Horizontal");

        // 이동 속도 증가
        if (Input.GetKey(KeyCode.LeftShift))
        {
            moving_velocity *= 3.0f; // 속도 증가
        }

        float current_speed = Mathf.Abs(moving_velocity * cat_speed);

        // 걷기 애니메이션 활성화/비활성화
        if (animator != null)
        {
            animator.SetBool("walking", current_speed > 0.1f);
            animator.SetFloat("speed", current_speed > run_speed_threshold ? current_speed : 0f);
        }

        // 점프 힘 충전
        if (Input.GetKey(KeyCode.Space) && is_grounded)
        {
            is_charging_jump = true;
            jump_force += jump_charge_speed * Time.deltaTime;
            jump_force = Mathf.Clamp(jump_force, 5.0f, max_jump_force);
        }

        // 점프 실행
        if (Input.GetKeyUp(KeyCode.Space) && is_charging_jump)
        {
            Jump();
        }
    }

    void FixedUpdate()
    {
        float distance_per_frame = cat_speed * Time.fixedDeltaTime;
        float degrees_per_frame = rot_speed * Time.fixedDeltaTime;

        // 속도 기반 이동 처리
        Vector3 forwardMovement = transform.forward * moving_velocity * cat_speed;
        forwardMovement = Vector3.ClampMagnitude(forwardMovement, max_velocity);

        cat_rigidbody.velocity = new Vector3(forwardMovement.x, cat_rigidbody.velocity.y, forwardMovement.z);

        // 회전 처리
        if (cat_angle != 0)
        {
            Quaternion rotation = Quaternion.Euler(0.0f, cat_angle * degrees_per_frame, 0.0f);
            cat_rigidbody.MoveRotation(cat_rigidbody.rotation * rotation);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log($"Collided with: {collision.gameObject.name} (Tag: {collision.gameObject.tag})");

            // 접촉 횟수 증가
            groundContactCount++;

            // 점프 가능 상태 설정
            is_grounded = true;

            // 수직 속도를 0으로 제한
            if (cat_rigidbody.velocity.y < 0)
            {
                cat_rigidbody.velocity = new Vector3(cat_rigidbody.velocity.x, 0, cat_rigidbody.velocity.z);
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log($"Left: {collision.gameObject.name} (Tag: {collision.gameObject.tag})");

            // 접촉 횟수 감소
            groundContactCount--;

            // 접촉이 완전히 없어진 경우에만 점프 비활성화
            if (groundContactCount <= 0)
            {
                groundContactCount = 0; // 안전을 위해 초기화
                is_grounded = false;
            }
        }
    }

    private void Jump()
    {
        if (is_grounded)
        {
            Debug.Log("Jumping!");

            // 수직 속도 초기화
            cat_rigidbody.velocity = new Vector3(cat_rigidbody.velocity.x, 0, cat_rigidbody.velocity.z);

            // 점프 힘 적용
            cat_rigidbody.AddForce(Vector3.up * jump_force, ForceMode.Impulse);

            // 점프 상태 전환
            is_grounded = false;

            // 점프 충전 초기화
            jump_force = 5.0f;
            is_charging_jump = false;

            Debug.Log($"Jump force applied: {jump_force}");
        }
    }
}
