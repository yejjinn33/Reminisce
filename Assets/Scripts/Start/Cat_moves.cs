using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat_moves : MonoBehaviour
{
    public float cat_speed = 10.0f;    // �̵� �ӵ�
    public float rot_speed = 100.0f;  // ȸ�� �ӵ�
    public float jump_force = 10.0f;  // �⺻ ���� ��
    public float max_jump_force = 15.0f; // �ִ� ���� ��
    public float jump_charge_speed = 5.0f; // ���� �� ���� �ӵ�
    public float run_speed_threshold = 15.0f; // �޸��� �����ϴ� �ӵ� ����
    public float max_velocity = 20.0f; // �ִ� �̵� �ӵ�

    private Animator animator;
    private Rigidbody cat_rigidbody;

    private float moving_velocity;     // �̵� �ӵ�
    private float cat_angle;           // ȸ�� �Է�
    private bool is_grounded = true;   // ����̰� ���� �ִ��� Ȯ��
    private bool is_charging_jump = false; // ���� ���� ���� ������ Ȯ��

    void Start()
    {
        animator = GetComponent<Animator>();
        cat_rigidbody = GetComponent<Rigidbody>();
        cat_rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;

        // ȸ�� ���� (��� �� ȸ�� ����)
        cat_rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
    }

    void Update()
    {
        moving_velocity = Input.GetAxis("Vertical");
        cat_angle = Input.GetAxis("Horizontal");

        if (Input.GetKey(KeyCode.LeftShift))
        {
            moving_velocity *= 3.0f; // �ӵ� ����
        }

        float current_speed = Mathf.Abs(moving_velocity * cat_speed);

        // �ȱ� �ִϸ��̼� Ȱ��ȭ/��Ȱ��ȭ
        animator.SetBool("walking", current_speed > 0.1f);

        // �޸��� �ִϸ��̼�
        animator.SetFloat("speed", current_speed > run_speed_threshold ? current_speed : 0f);

        // ���� �� ����
        if (Input.GetKey(KeyCode.Space) && is_grounded)
        {
            is_charging_jump = true;
            jump_force += jump_charge_speed * Time.deltaTime;
            jump_force = Mathf.Clamp(jump_force, 5.0f, max_jump_force);
        }

        // ���� ����
        if (Input.GetKeyUp(KeyCode.Space) && is_charging_jump && is_grounded)
        {
            Jump();
        }
    }

    void FixedUpdate()
    {
        float distance_per_frame = cat_speed * moving_velocity;
        float degrees_per_frame = rot_speed * Time.fixedDeltaTime;

        // �ӵ� ��� �̵� ó��
        Vector3 forwardMovement = transform.forward * distance_per_frame;
        forwardMovement = Vector3.ClampMagnitude(forwardMovement, max_velocity); // �ִ� �ӵ� ����

        cat_rigidbody.velocity = new Vector3(forwardMovement.x, cat_rigidbody.velocity.y, forwardMovement.z);

        // ȸ�� ó��
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
        // ���� �Ǵ� �ٴڰ� �浹�� ���
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.layer == LayerMask.NameToLayer("Furniture"))
        {
            Debug.Log("Collided with: " + collision.gameObject.name);
            is_grounded = true;
            animator.speed = 1f; // �ִϸ��̼� ����
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // ���� �Ǵ� �ٴڿ��� ������ ���
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.layer == LayerMask.NameToLayer("Furniture"))
        {
            Debug.Log("Left: " + collision.gameObject.name);
            is_grounded = false;
        }
    }
}
