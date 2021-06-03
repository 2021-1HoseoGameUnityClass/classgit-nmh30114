using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 3f;

    [SerializeField]
    private float playerSpeed = 0.1f;
    
    //���� ���� ����
    [SerializeField]
    private float playerJumpforce = 0.1f;

    private bool isJump = false;


    [SerializeField]
    private GameObject bulletObj = null;

    [SerializeField]
    private GameObject instantiateObj = null;

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
        if(Input.GetButtonDown("Jump"))
        {
            PlayerJump();
        }

        if (Input.GetKeyDown("Fire1"))
        {
            PlayerJump();
        }
    }

    private void PlayerMove()
    {
        float h = Input.GetAxis("Horizontal");
        float playerSpeed = h * moveSpeed * Time.deltaTime;

        Vector3 vector3 = new Vector3();
        vector3.x = playerSpeed;

        transform.Translate(vector3);

        if (h < 0)
        {
            GetComponent<Animator>().SetBool("Walk", true);
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (h == 0)
        {
            GetComponent<Animator>().SetBool("Walk", false);
        }
        else
        {
            GetComponent<Animator>().SetBool("Walk", true);
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    private void PlayerJump()
    {
        //�������°� �Ǿ� ���� �������� �����ϵ��� ��
        if(isJump == false)
        {
            //�ִϸ��̼� ó���κ�
            GetComponent<Animator>().SetBool("Walk", false);
            GetComponent<Animator>().SetBool("Jump", true);

            //��������ŭ add force
            Vector2 vector2 = new Vector2(0, playerJumpforce);
            GetComponent<Rigidbody2D>().AddForce(vector2);
            isJump = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //�浹ü�� �ݶ��̴��� �÷��� �±׶��
        if(collision.collider.tag == "Platform")
        {
            GetComponent<Animator>().SetBool("Jump", false);
            isJump = false;
        }
    }

    private void Fire()
    {
        float direction = transform.localPosition.x;
        Quaternion quaternion = new Quaternion(0, 0, 0, 0);
        Instantiate(bulletObj, instantiateObj.transform.position, quaternion).GetComponent<Bullet>
            ().InstantiateBullet(direction);
    }
}
