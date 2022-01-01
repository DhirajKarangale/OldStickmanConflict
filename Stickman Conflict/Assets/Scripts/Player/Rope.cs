using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyJoystick;

public class Rope : MonoBehaviour
{
    [SerializeField] Material mat;
    [SerializeField] Rigidbody2D origin;
    [SerializeField] LineRenderer line;
    [SerializeField] float lineWidth = 0.1f, speed = 75;
    [SerializeField] float pullForce = 50;
    [SerializeField] Joystick joystickHandRotate;
    //  [SerializeField] float stayTime = 1;
    // private IEnumerator timer;
    private Vector3 velocity;
    private bool pull = false;
    private bool update = false;

    private void Start()
    {
        line.material = mat;
        line.startWidth = lineWidth;
        line.endWidth = lineWidth;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RopePointerDown();
        }
        if (Input.GetKeyUp(KeyCode.R))
        {
            RopePointerUp();
        }

        if (!update)
        {
            return;
        }

        if (pull)
        {
            Vector2 dir = (Vector2)transform.position - origin.position;
            dir = dir.normalized;
            origin.AddForce(dir * pullForce);
        }
        else
        {
            transform.position += velocity * Time.deltaTime;
            float distance = Vector2.Distance(transform.position, origin.position);
            if (distance > 50)
            {
                update = false;
                line.SetPosition(0, Vector2.zero);
                line.SetPosition(1, Vector2.zero);
                return;
            }
        }

        line.SetPosition(0, transform.position);
        line.SetPosition(1, origin.position);
    }

    // IEnumerator reset(float delay)
    // {
    //     yield return new WaitForSeconds(delay);
    //     update = false;
    //     line.SetPosition(0, Vector2.zero);
    //     line.SetPosition(1, Vector2.zero);
    // }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        velocity = Vector3.zero;
        pull = true;
        //  timer =reset(stayTime);
        //  StartCoroutine(timer);
    }

    public void SetRope(Vector2 targetPos)
    {
     //   Vector2 dir = targetPos - origin.position;
        Vector2 dir = targetPos;
        dir = dir.normalized;
        velocity = dir * speed;
        transform.position = origin.position + dir;
        pull = false;
        update = true;
        // if(timer != null)
        // {
        //     StopCoroutine(timer);
        //     timer = null;
        // }
    }

    public void RopePointerDown()
    {
        Vector2 pos = new Vector2(joystickHandRotate.Horizontal(), joystickHandRotate.Vertical()) * 100;
        SetRope(pos);
    }

    public void RopePointerUp()
    {
        update = false;
        line.SetPosition(0, Vector2.zero);
        line.SetPosition(1, Vector2.zero);
    }
}