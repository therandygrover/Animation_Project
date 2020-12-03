using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class character : MonoBehaviour
{
    Vector3 dest;
    Vector3 dir;
    bool doAction;
    float Maxspeed = 5f;
    float rotSpeed = 5f;
    Animator a;
    Vector3 start;

    public bool alive;

    [SerializeField]
    bool ally;
    [SerializeField]
    character enemy;
    [SerializeField]
    character enemy1;
    [SerializeField]
    character enemy2;
    [SerializeField]
    character friend;

    public bool shouldRotate;
    public float health;

    public GameObject attackPointG;
    public GameObject attackPointG1;
    public GameObject attackPointG2;

    Vector3 attackPoint;

    enum State
    {
        moving,
        attacking,
        returning,
        idle,
    }
    State state;

    Vector3 target;
    public int charToHit = 0;

    public void die()
    {
        alive = false;
        a.SetBool("Alive", false);
    }

    public void hurt()
    {
        a.SetBool("Hit", true);
    }

    public void go(string s)
    {
        if (s == "attack" || s == "attack2" || s == "heal2")
        {
            target = enemy.transform.position;
            if (s == "attack2" || s == "heal2")
            {
                a.SetBool("aoe", true);
            }
        }
        else
        {
            target = friend.transform.position;
        }
        doAction = true;
    }

    Game game;

    // Start is called before the first frame update
    void Start()
    {
        alive = true;
        
        health = 100f;
        attackPoint = attackPointG.transform.position;
        state = State.idle;
        dest = transform.position;
        dest.y = start.y;
        doAction = false;
        a = GetComponent<Animator>();
        start = transform.position;
        dir = transform.forward;
        a.SetBool("Alive", true);

        game = FindObjectOfType<Game>();
    }

    private void moveTo(float speed)
    {
        Vector3 newD = transform.position;
        dir = (dest - transform.position).normalized;
        newD += Maxspeed * Time.deltaTime * dir;
        newD.y = transform.position.y;

        transform.position = newD;
    }

    // Update is called once per frame
    void Update()
    {
        float speed = Maxspeed * Time.deltaTime;
        switch (state)
        {
            case (State.idle):
                {
                    if (doAction)
                    {
                        state = State.moving;
                        dest = attackPoint;
                        doAction = false;
                        dir = (dest - transform.position).normalized;
                        if (attackPointG1)
                        {
                            if (Random.value < .5)
                            {
                                a.SetBool("aoe", true);
                            }
                            else
                            {
                                if (Random.value < 0.5 && enemy2.GetComponent<character>().alive)
                                {
                                    dest = attackPointG2.transform.position;
                                    dest.y = transform.position.y;
                                    charToHit = 2;
                                }
                                else if (enemy1.GetComponent<character>().alive)
                                {
                                    dest = attackPointG1.transform.position;
                                    dest.y = transform.position.y;
                                    charToHit = 1;
                                }
                            }
                        }
                    }
                }
                break;
            case (State.attacking):
                {
                    if (a.GetBool("Attacking") == false)
                    {
                        state = State.returning;
                        dest = start;
                    }
                }
                break;
            case (State.returning):
                {
                    dir = (dest - transform.position).normalized;
                    if (transform.position.x - dest.x < -speed || transform.position.z - dest.z < -speed ||
                        transform.position.x - dest.x > speed || transform.position.z - dest.z > speed)
                    {
                        a.SetFloat("Speed", 1f);
                        if (!shouldRotate)
                            a.SetFloat("Speed", -1f);
                        moveTo(speed);
                    }
                    else
                    {
                        transform.position = dest;
                        state = State.idle;
                        a.SetFloat("Speed", 0f);
                        Vector3 t = attackPoint;
                        t.y = transform.position.y;
                        dir = (attackPoint - transform.position).normalized;
                        game.charDone(ally);
                    }
                }
                break;
            case (State.moving):
                {

                    if (transform.position.x - dest.x < -speed || transform.position.z - dest.z < -speed ||
                        transform.position.x - dest.x > speed || transform.position.z - dest.z > speed)
                    {
                        a.SetFloat("Speed", 1f);
                        moveTo(speed);
                    }
                    else
                    {
                        transform.position = dest;
                        state = State.attacking;
                        a.SetFloat("Speed", 0f);
                        a.SetBool("Attacking", true);
                        Vector3 e = target;
                        if (a.GetBool("aoe"))
                        {
                            if (enemy1 == null)
                            {
                                e = target;
                            }
                            else
                            {
                                e = transform.position;
                                e.z -= 5;
                            }
                        }
                        else
                        {
                           
                            if (charToHit == 1)
                            {
                                e = enemy1.transform.position;
                            }
                            else if (charToHit == 2)
                            {
                                e = enemy2.transform.position;
                            }
                        }
                        e.y = transform.position.y;
                        dir = e - transform.position;
                    }
                }
                break;
        }

        if (shouldRotate || state != State.returning)
        {
            Quaternion look = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, look, Time.deltaTime * rotSpeed);
        }
        else if (state == State.returning && shouldRotate)
        {
            Quaternion look = Quaternion.LookRotation(-dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, look, Time.deltaTime * rotSpeed);
        }

    }
}
