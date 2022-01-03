using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Parallax : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private List<SpriteRenderer> objects;

    bool end;

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
        if (!end)
        {

            foreach (SpriteRenderer obj in objects)
            {
                obj.transform.position -= Vector3.right * speed;
                if (obj.transform.position.x <= -11)
                {
                    obj.transform.position = new Vector3(11, Random.Range(-5f, 5f), 0);
                    obj.sprite = sprites[Random.Range(0, sprites.Length)];
                }
            }
        }
    }

    public void Stop()
    {
        end = true;
    }
    public void Continue()
    {
        end = false;
    }
}
