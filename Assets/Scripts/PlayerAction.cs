using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    public float Speed = 5f;
    public GameManager manager;

    Animator anim;
    private Rigidbody2D rigid;
    private float horizon;
    private float vertical;
    private bool isHorizonMove;
    Vector3 dirVec;
    GameObject scanObject;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        horizon = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        bool hDown = Input.GetButtonDown("Horizontal");
        bool vDown = Input.GetButtonDown("Vertical");
        bool hUp = Input.GetButtonUp("Horizontal");
        bool vUp = Input.GetButtonUp("Vertical");

        if (hDown || vUp)
            isHorizonMove = true;
        else if (vDown || vUp)
            isHorizonMove = false;
        else if (hUp || vUp)
            isHorizonMove = (horizon != 0);

        if (anim.GetInteger("hAxisRaw") != horizon)
        {
            anim.SetBool("isChange", true);
            anim.SetInteger("hAxisRaw", (int)horizon);
        }
        else if (anim.GetInteger("vAxisRaw") != vertical)
        {
            anim.SetBool("isChange", true);
            anim.SetInteger("vAxisRaw", (int)vertical);
        }
        else
        {
            anim.SetBool("isChange", false);
        }
        if (vDown && vertical == 1)
            dirVec = Vector3.up;
        else if (vDown && vertical == -1)
            dirVec = Vector3.up;
        else if (hDown && horizon == 1)
            dirVec = Vector3.right;
        else if (hDown && horizon == -1)
            dirVec = Vector3.left;

        if (Input.GetButtonDown("Jump") && scanObject != null)
        {
            manager.Action(scanObject);
        }
    }

    void FixedUpdate()
    {
        Vector2 moveVec = isHorizonMove ? new Vector2(horizon, 0) : new Vector2(0, vertical);
        rigid.linearVelocity = moveVec * Speed;

        Debug.DrawRay(rigid.position, dirVec * 0.7f, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, dirVec, 0.7f, LayerMask.GetMask("Object"));

        if (rayHit.collider != null)
        {
            scanObject = rayHit.collider.gameObject;
        }
        else
        {
            scanObject = null;
        }
    }
}