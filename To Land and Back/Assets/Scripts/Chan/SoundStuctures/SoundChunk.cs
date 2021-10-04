using UnityEngine;

[System.Serializable]
public class SoundChunk
{
    public string chunkName;
    public SoundPiece[] soundPieces;
    [Range(0f, 2f)] public float Volume = 1f;
    [Range(-3f, 3f)] public float Pitch = 1f;
}
