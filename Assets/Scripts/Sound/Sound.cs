using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sound", menuName = "Sound", order = 1)]
public class Sound : ScriptableObject
{
    [SerializeField] private bool loop;
    [SerializeField] private bool solo;
    [SerializeField] private AudioClip clip;
    [Range(0,1)]
    [SerializeField] private float volume;

    public bool Loop { get => loop; set => loop = value; }
    public AudioClip Clip { get => clip; set => clip = value; }
    public float Volume { get => volume; set => volume = value; }
    public bool Solo { get => solo; set => solo = value; }
}
