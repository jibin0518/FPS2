using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player_V2 : MonoBehaviour
{
    public float spd = 3;
    public float walkspd = 3, walkbackspd = 2;
    public float runspd = 7, runbackspd = 5;

    public Vector3 dir;
    public float inX, inY;
    public float groundset;
    public LayerMask groundMask;
    Vector2 playerPos;

    public float gravity = -9.81f;
    Vector3 velocity;

    CharacterController controller;

    MoveBase CurState;

    public IdleState idle = new IdleState();
    public WalkState walk = new WalkState();
    public RunState run = new RunState();

    [HideInInspector] public Animator anima;

    void Start()
    {
        anima = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        SwitchState(idle);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Gravity();

        anima.SetFloat("Ver", inY);
        anima.SetFloat("Hori", inX);

        CurState.UpdateState(this);
    }

    public void SwitchState(MoveBase state)
    {
        CurState = state;
        CurState.EnterState(this);
    }

    void Move()
    {
        inX = Input.GetAxis("Horizontal");
        inY = Input.GetAxis("Vertical");//Vertical //Horizontal

        dir = transform.forward * inY + transform.right * inX;

        controller.Move(dir.normalized * spd * Time.deltaTime);
    }

    bool IsGround()
    {
        playerPos = new Vector3(transform.position.x, transform.position.y * gravity, transform.position.z);
        if (Physics.CheckSphere(playerPos, controller.radius - 0.05f, groundMask)) return true;
        return false;
    }

    void Gravity()
    {
        if (!IsGround())
        {
            velocity.y += gravity * Time.deltaTime;
        }
        else if (velocity.y < 0)
        {
            velocity.y = -2;
        }

        controller.Move(velocity * Time.deltaTime);
    }

    /*void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(playerPos, controller.radius - 0.05f);
    }*/
}