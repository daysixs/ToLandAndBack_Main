using UnityEngine;

[System.Serializable]
public class SoundChain
{
    public string chainName;
    public SoundChunk[] soundChunks;
    [Range(0f, 2f)] public float Volume = 1f;
    [Range(-3f, 3f)] public float Pitch = 1f;
}
