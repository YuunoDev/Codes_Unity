using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Control : MonoBehaviour
{
    CharacterController chare;
    [Header("Opciones de personaje")]
    public float walkspeed = 6.0f;
    public float runspeed = 10.0f;
    public float jumpSpped = 8.0f;
    public float gravity = 20.0f;
    [Header("Bar")]
    public float runscharge;

    public Vector3 move = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {

        chare = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (chare.isGrounded)
        {
            move = new Vector3(Input.GetAxisRaw("Horizontal"), 0.0f, Input.GetAxisRaw("Vertical")).normalized;


            if (Input.GetKey(KeyCode.LeftShift))
            {
                move = transform.TransformDirection(move) * runspeed;
            }
            else
            {
                move = transform.TransformDirection(move) * walkspeed;
            }

            if (Input.GetKey(KeyCode.Space))
            {
                move.y = jumpSpped;
            }
        }

        move.y -= gravity * Time.deltaTime;

        chare.Move(move * Time.deltaTime);
    }

    IEnumerator Espera()
    {
        yield return new WaitForSeconds(5);
    }
}