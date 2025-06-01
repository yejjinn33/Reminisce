using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat_moves2 : MonoBehaviour
{
    public float cat_speed = 10.0f;    // �̵� �ӵ�
    public float rot_speed = 100.0f;  // ȸ�� �ӵ�
    public float jump_force = 5.0f;   // �⺻ ���� ��
    public float max_jump_force = 15.0f; // �ִ� ���� ��
    public float jump_charge_speed = 5.0f; // ���� �� ���� �ӵ�
    public float run_speed_threshold = 15.0f; // �޸��� �����ϴ� �ӵ� ����
    public float max_velocity = 20.0f; // �ִ� �̵� �ӵ�
    public float push_force = 5.0f;    // ����������� �� ��
    public float interaction_range = 5.0f; // ������������� ��ȣ�ۿ� ����

    private Animator animator;
    private Rigidbody cat_rigidbody;

    private float moving_velocity;    // �̵� �ӵ�
    private float cat_angle;          // ȸ�� �Է�
    private bool is_grounded = true;  // ����̰� ���� �ִ��� Ȯ��
    private bool is_charging_jump = false; // ���� ���� ���� ������ Ȯ��
    //private bool is_slowed = false;   // ������ ���� Ȯ��
    private float original_speed;     // ���� �ӵ� ����
    private int groundContactCount = 0; // ������ ������ ����

    private HashSet<GameObject> clickedItems = new HashSet<GameObject>();

    void Start()
    {
        animator = GetComponent<Animator>();
        cat_rigidbody = GetComponent<Rigidbody>();
        cat_rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;

        // ��� �� ȸ�� ����
        cat_rigidbody.constraints = RigidbodyConstraints.FreezeRotation;

        // �ʱ� �ӵ� ����
        original_speed = cat_speed;
    }

    void Update()
    {
        moving_velocity = Input.GetAxis("Vertical");
        cat_angle = Input.GetAxis("Horizontal");

        // �̵� �ӵ� ����
        if (Input.GetKey(KeyCode.LeftShift))
        {
            moving_velocity *= 3.0f; // �ӵ� ����
        }

        float current_speed = Mathf.Abs(moving_velocity * cat_speed);

        // �ȱ� �ִϸ��̼� Ȱ��ȭ/��Ȱ��ȭ
        if (animator != null)
        {
            animator.SetBool("walking", current_speed > 0.1f);
            animator.SetFloat("speed", current_speed > run_speed_threshold ? current_speed : 0f);
        }

        // ���� �� ����
        if (Input.GetKey(KeyCode.Space) && is_grounded)
        {
            is_charging_jump = true;
            jump_force += jump_charge_speed * Time.deltaTime;
            jump_force = Mathf.Clamp(jump_force, 5.0f, max_jump_force);
        }

        // ���� ����
        if (Input.GetKeyUp(KeyCode.Space) && is_charging_jump)
        {
            Jump();
        }
    }

    void FixedUpdate()
    {
        float distance_per_frame = cat_speed * Time.fixedDeltaTime;
        float degrees_per_frame = rot_speed * Time.fixedDeltaTime;

        // �ӵ� ��� �̵� ó��
        Vector3 forwardMovement = transform.forward * moving_velocity * cat_speed;
        forwardMovement = Vector3.ClampMagnitude(forwardMovement, max_velocity);

        cat_rigidbody.velocity = new Vector3(forwardMovement.x, cat_rigidbody.velocity.y, forwardMovement.z);

        // ȸ�� ó��
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

            // ���� Ƚ�� ����
            groundContactCount++;

            // ���� ���� ���� ����
            is_grounded = true;

            // ���� �ӵ��� 0���� ����
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

            // ���� Ƚ�� ����
            groundContactCount--;

            // ������ ������ ������ ��쿡�� ���� ��Ȱ��ȭ
            if (groundContactCount <= 0)
            {
                groundContactCount = 0; // ������ ���� �ʱ�ȭ
                is_grounded = false;
            }
        }
    }

    private void Jump()
    {
        if (is_grounded)
        {
            Debug.Log("Jumping!");

            // ���� �ӵ� �ʱ�ȭ
            cat_rigidbody.velocity = new Vector3(cat_rigidbody.velocity.x, 0, cat_rigidbody.velocity.z);

            // ���� �� ����
            cat_rigidbody.AddForce(Vector3.up * jump_force, ForceMode.Impulse);

            // ���� ���� ��ȯ
            is_grounded = false;

            // ���� ���� �ʱ�ȭ
            jump_force = 5.0f;
            is_charging_jump = false;

            Debug.Log($"Jump force applied: {jump_force}");
        }
    }
}
