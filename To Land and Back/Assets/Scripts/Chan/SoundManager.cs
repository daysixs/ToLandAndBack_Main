using System.Collections;
using UnityEngine;

/*!!!Notes: 
 * Trash ass unity made it so that this is the only way to set up soundChains
 * Script Version (1.1V)
 * By Chan Kwok Chun (Gul)
 * Will be reused in future projects because its perfect for copy and pasting
 */
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

    //----------------------------------------------------------------------------------------------------------- !!!Code Divider to Keep it clean

    public void PlayChain(string soundChainName) //Chain plays chunks of sound until there is no more in that chain, set the delay to 0 on the sound chunk to make them play together, overload for name instead of id
    {
        //System.Array.Find(soundChains, soundChains => soundChains.chainName == soundChainName); !!!Note Possible usable system method, very useful
        StartCoroutine(playSoundChain(System.Array.IndexOf(soundChains, System.Array.Find(soundChains, soundChains => soundChains.chainName == soundChainName)))); //Using system to search for the instance value of array with the desired name, and pick it's instance index for array call usage
    }
    public void PlayChunk(string soundChunkName) //Chunk randomizes 1 sound from the list to create uniqueness, overload for name instead of id
    {
        StartCoroutine(playSoundChain(System.Array.IndexOf(soundChains, System.Array.Find(soundChunks, soundChunks => soundChunks.chunkName == soundChunkName)))); //Using system to search for the instance value of array with the desired name, and pick it's instance index for array call usage
    }

    public void PlayPiece(string soundPieceName) //Piece plays a sound based on volume and pitch, overload for name instead of id
    {
        StartCoroutine(playSoundChain(System.Array.IndexOf(soundChains, System.Array.Find(soundPieces, soundPieces => soundPieces.pieceName == soundPieceName)))); //Using system to search for the instance value of array with the desired name, and pick it's instance index for array call usage
    }
    // Awake is a preload
    void Awake()
    {
        instance = this;
    }
    IEnumerator playSoundChain(int soundChainID) //An IEnumerator to play the specific sound(Chain, Chunk or Piece) by ID
    {
        for (int i = 0; i < soundChains[soundChainID].soundChunks.Length; i++)
        {
            int rand = Random.Range(0, soundChains[soundChainID].soundChunks[i].soundPieces.Length); //Produce the random instance of the choosen sound in the chunk, picked and used to check for the following arrays
            yield return new WaitForSeconds(soundChains[soundChainID].soundChunks[i].soundPieces[rand].delayBefore); //Delay the sound until as stated duration later, used for timing
            audioSource.PlayOneShot(soundChains[soundChainID].soundChunks[i].soundPieces[rand].Audio, soundChains[soundChainID].Volume * soundChains[soundChainID].soundChunks[i].Volume * soundChains[soundChainID].soundChunks[i].soundPieces[rand].Volume); //Play the sound stated following the Volume multipliers
        }
    }

    IEnumerator playSoundChunk(int soundChunkID) //An IEnumerator to play the specific sound(Chain, Chunk or Piece) by ID
    {
        int rand = Random.Range(0, soundChunks[soundChunkID].soundPieces.Length); //Produce the random instance of the choosen sound in the chunk, picked and used to check for the following arrays
        yield return new WaitForSeconds(soundChunks[soundChunkID].soundPieces[rand].delayBefore); //Delay the sound until as stated duration later, used for timing
        audioSource.PlayOneShot(soundChunks[soundChunkID].soundPieces[rand].Audio, soundChunks[soundChunkID].Volume * soundChunks[soundChunkID].soundPieces[rand].Volume); //Play the sound stated following the Volume multipliers
    }

    IEnumerator playSoundPiece(int soundPieceID) //An IEnumerator to play the specific sound(Chain, Chunk or Piece) by ID
    {
        yield return new WaitForSeconds(soundPieces[soundPieceID].delayBefore); //Delay the sound until as stated duration later, used for timing
        audioSource.PlayOneShot(soundPieces[soundPieceID].Audio, soundPieces[soundPieceID].Volume); //Play the sound stated following the Volume multipliers
    }
}
