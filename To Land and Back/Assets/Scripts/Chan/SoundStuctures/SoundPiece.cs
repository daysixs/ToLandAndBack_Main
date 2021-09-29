using UnityEngine;

[System.Serializable]
public class SoundPiece
{
    public string pieceName;
    public AudioClip Audio;
    [Range(0f, 10f)] public float delayBefore = 1f;
    [Range(0f, 2f)] public float Volume = 1f;
    [Range(-3f, 3f)] public float Pitch = 1f;
}
