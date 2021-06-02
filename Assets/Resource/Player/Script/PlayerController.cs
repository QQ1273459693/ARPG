using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour,IDamageable
{
    [SerializeField]
    [Header("移动端摇杆")]
    public Joystick Mobile_Joy;
    public bool isCanMove;

    [SerializeField]
    [Header("人物移动类属性")]
    public static PlayerController instance;
    public Vector2 RLmove;
    public int facingDirction = 1;
    public float AcceleraTime;
    public float DcceleraTime;
    public float Movespeed;
    private float fixe;
    public float VElocityX;
    private bool isFacingRight = true;//脸部朝向
    public bool Stab;
    public int StabDir;

    [Header("人物组件加载")]
    public Animator anim;
    public Rigidbody2D rb;
    public AnimatorStateInfo info;

    [Header("人物面向方向")]
    public int Dir;//:-1是朝左,1是朝右,2是上,-2是下
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        fixe = Time.fixedDeltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        Mobile_JoyInput();
        UpdateAnimation();
        CheckMovementDirection(RLmove.x);
    }
    private void FixedUpdate()
    {
        Movement(RLmove);
        Player_Stab();
    }
    private void Mobile_JoyInput()//摇杆输入值
    {
        RLmove = new Vector2(Mobile_Joy.Horizontal, Mobile_Joy.Vertical);
    }
    private void UpdateAnimation()
    {
        //if (info.IsName("Dash_Front")|| info.IsName("Dash_Back"))
        //{
        //    rb.velocity = new Vector2(rb.velocity.x, 0);
        //}
        anim.SetFloat("Y", RLmove.y);
        anim.SetFloat("X", RLmove.x);
        anim.SetInteger("X_Int", (int)RLmove.x);
        anim.SetInteger("Y_Int", (int)RLmove.y);
        if (!info.IsName("Run_Slide"))
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        } 
        if (!info.IsName("Run_Front") && !info.IsName("Run_Back"))
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }
    }
    private void CheckMovementDirection(float RL)//精灵朝向
    {
        info = anim.GetCurrentAnimatorStateInfo(0);
        if (isFacingRight && RL < 0 && !info.IsName("Run_Front") && !info.IsName("Run_Back"))
        {
            Flip();
        }
        else if (!isFacingRight && RL > 0 && !info.IsName("Run_Front") && !info.IsName("Run_Back"))
        {
            Flip();
        }
    }
    private void Flip()
    {
        facingDirction *= -1;
        isFacingRight = !isFacingRight;
        transform.Rotate(0.0F, 180.0F, 0.0F);
    }
    private void Movement(Vector2 RL)
    {
        info = anim.GetCurrentAnimatorStateInfo(0);
        if (RL.x > 0)
        {
            anim.SetBool("walk", true);
            if (info.IsName("Run_Slide"))
            {
                rb.velocity = new Vector2(Mathf.SmoothDamp(rb.velocity.x, Movespeed * fixe * 60, ref VElocityX, AcceleraTime), rb.velocity.y);
            }
        }
        else if (RL.x < 0)
        {
            anim.SetBool("walk", true);
            if (info.IsName("Run_Slide"))
            {
                rb.velocity = new Vector2(Mathf.SmoothDamp(rb.velocity.x, Movespeed * fixe * 60 * -1, ref VElocityX, AcceleraTime), rb.velocity.y);
            }
        }
        else if (RL.x == 0)
        {
            anim.SetBool("walk", false);
            rb.velocity = new Vector2(0, rb.velocity.y);
        }


        if (RL.y > 0)
        {
            if (info.IsName("Run_Back"))
            {
                rb.velocity = new Vector2(rb.velocity.x, Mathf.SmoothDamp(rb.velocity.y, Movespeed * fixe * 60, ref VElocityX, AcceleraTime));
            }
        }
        else if (RL.y < 0)
        {
            if (info.IsName("Run_Front"))
            {
                rb.velocity = new Vector2(rb.velocity.x, Mathf.SmoothDamp(rb.velocity.y, Movespeed * fixe * 60 * -1, ref VElocityX, AcceleraTime));
            }
        }
        else if (RL.y == 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }
    }
     void Player_Stab()
    {
        if (Stab)
        {
            Movespeed = 0;
            switch (StabDir)
            {
                case 1:
                    if (facingDirction == 1)
                    {
                        rb.velocity = new Vector2(50, rb.velocity.y);
                    }
                    else
                    {
                        rb.velocity = new Vector2(-50, rb.velocity.y);
                    }
                    break;
                case 2:
                    rb.velocity = new Vector2(rb.velocity.x, 50);
                    break;
                case -2:
                    rb.velocity = new Vector2(rb.velocity.x, -50);
                    break;
                default:
                    break;
            }
            StabDir = 0;
            Stab = false;
            Movespeed = 4;
        }
    }

    public void GetHit(float damage)
    {
        if (damage!=0)
        {
            anim.SetInteger("Hurt",Dir);
        }
    }
}
