using System.Collections;
using UnityEngine;

//Trash ass unity made it so that this is the only way to set up soundChains
public class SoundManager : MonoBehaviour
{
    public AudioSource audioSource;
    public static SoundManager instance;
    [SerializeField] SoundChain[] soundChains;
    [SerializeField] SoundChunk[] soundChunks;
    [SerializeField] SoundPiece[] soundPieces;

    public void PlayChain(int soundChainID) //Chain plays chunks of sound until there is no more in that chain, set the delay to 0 on the sound chunk to make them play together
    {
        StartCoroutine(playSoundChain(soundChainID));
    }

    public void PlayChunk(int soundChunkID) //Chunk randomizes 1 sound from the list to create uniqueness
    {
        StartCoroutine(playSoundChunk(soundChunkID));
    }

    public void PlayPiece(int soundPieceID) //Piece plays a sound based on volume and pitch
    {
        StartCoroutine(playSoundPiece(soundPieceID));
    }

    //Currently unclean and will work on implimenting string based overload, todo todo todo
    //-----------------------------------------------------------------------------------------------------------
    /*public void PlayChain(string soundChainName) //Chain plays chunks of sound until there is no more in that chain, set the delay to 0 on the sound chunk to make them play together, overload for name instead of id
    {
        int count = 0;
        foreach (SoundChain soundChain in soundChains)
        {
            if (soundChain.chainName == soundChainName)
            {
                for (int i = 0; i < soundChains[count].soundChunks.Length; i++)
                {
                    int rand = Random.Range(0, soundChunks[i].soundPieces.Length);
                    StartCoroutine(Delay(soundChains[count].soundChunks[i].soundPieces[rand].delayBefore));
                    audioSource.PlayOneShot(soundChains[count].soundChunks[i].soundPieces[rand].Audio, soundChains[count].Volume * soundChunks[i].Volume * soundChunks[i].soundPieces[rand].Volume);
                }
            }
            else count++;
        }
    }
    public void PlayChunk(string soundChunkName) //Chunk randomizes 1 sound from the list to create uniqueness, overload for name instead of id
    {
        int count = 0;
        foreach (SoundChunk soundChunk in soundChunks)
        {
            if (soundChunk.chunkName == soundChunkName)
            {
                int rand = Random.Range(0, soundChunks[count].soundPieces.Length);
                StartCoroutine(Delay(soundChunks[count].soundPieces[rand].delayBefore));
                audioSource.PlayOneShot(soundChunks[count].soundPieces[rand].Audio, soundChunks[count].Volume * soundChunks[count].soundPieces[rand].Volume);
            }
            else count++;
        }
    }

    public void PlayPiece(string soundPieceName) //Piece plays a sound based on volume and pitch, overload for name instead of id
    {
        int count = 0;
        foreach (SoundPiece soundPiece in soundPieces)
        {
            if (soundPiece.pieceName == soundPieceName)
            {
                StartCoroutine(Delay(soundPieces[count].delayBefore));
                audioSource.PlayOneShot(soundPieces[count].Audio, soundPieces[count].Volume);
            }
            else count++;
        }
    }*/
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    private void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator playSoundChain(int soundChainID)
    {
        for (int i = 0; i < soundChains[soundChainID].soundChunks.Length; i++)
        {
            int rand = Random.Range(0, soundChains[soundChainID].soundChunks[i].soundPieces.Length);
            yield return new WaitForSeconds(soundChains[soundChainID].soundChunks[i].soundPieces[rand].delayBefore);
            audioSource.PlayOneShot(soundChains[soundChainID].soundChunks[i].soundPieces[rand].Audio, soundChains[soundChainID].Volume * soundChains[soundChainID].soundChunks[i].Volume * soundChains[soundChainID].soundChunks[i].soundPieces[rand].Volume);
        }
    }

    IEnumerator playSoundChunk(int soundChunkID)
    {
        int rand = Random.Range(0, soundChunks[soundChunkID].soundPieces.Length);
        yield return new WaitForSeconds(soundChunks[soundChunkID].soundPieces[rand].delayBefore);
        audioSource.PlayOneShot(soundChunks[soundChunkID].soundPieces[rand].Audio, soundChunks[soundChunkID].Volume * soundChunks[soundChunkID].soundPieces[rand].Volume);
    }

    IEnumerator playSoundPiece(int soundPieceID)
    {
        yield return new WaitForSeconds(soundPieces[soundPieceID].delayBefore);
        audioSource.PlayOneShot(soundPieces[soundPieceID].Audio, soundPieces[soundPieceID].Volume);
    }
}
