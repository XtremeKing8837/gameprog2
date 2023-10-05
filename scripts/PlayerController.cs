using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    private Animator playerAnim;
    [Range(0,15)]
    public float speed;
    
    // Start is called before the first frame update
    void Start()
    {
        playerAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            MoveForward();
        }
    }

    void MoveForward()
    {
        transform.position = Vector3.forward * speed * Time.time;
        playerAnim.SetBool("isRunning", true);
    }

    void StopMovement()
    {
        speed = 0;
        playerAnim.SetBool("isRunning", false);
    }
}
