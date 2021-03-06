using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private EngineSound engineSound;

    private Rigidbody2D rb;
    private Keyboard keyboard;

    private bool pause;

    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        keyboard = Keyboard.current;
        SoundManager.Play("MainMusic");
    }
    private void OnEnable()
    {
        EnergyManager.PauseAction += Stop;
        EnergyManager.ContinueAction += Continue;
    }

    private void OnDisable()
    {
        EnergyManager.PauseAction -= Stop;
        EnergyManager.ContinueAction -= Continue;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if(!pause)
            Thrust();
    }

    public void Thrust()
    {
        if (keyboard.spaceKey.isPressed)
        {
            rb.AddForce(Vector2.up * speed, ForceMode2D.Impulse);
            engineSound.SpeedUp();
        }
        else
        {
            engineSound.SpeedDown();
        }
    }

    private void Stop()
    {
        pause = true;
        rb.gravityScale = 0;
        rb.velocity = Vector2.zero;
    }
    private void Continue()
    {
        pause = false;
        rb.gravityScale = 1;
    }

    private void Dead()
    {
        SoundManager.Play("GameOver");
        EnergyManager.PauseAction.Invoke();
        EnergyManager.GameOverAction.Invoke();
        transform.DetachChildren();
        rb.gravityScale = 1;
        transform.DOShakeScale(0.25f);
        transform.DORotate(new Vector3(0,0,180),2);
        GetComponent<Collider2D>().enabled = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Dead();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.SetActive(false);
        EnergyManager.EnergyUpAction.Invoke(25);
    }
}
