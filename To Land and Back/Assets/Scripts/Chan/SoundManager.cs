using System.Collections;
using UnityEngine;

/*!!!Notes: 
 * Trash ass unity made it so that this is the only way to set up soundChains
 * Script Version (1.2V)
 * By Chan Kwok Chun (Gul)
 * Will be reused in future projects because its perfect for copy and pasting
 */
public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    [SerializeField] SoundChain[] soundChains;
    [SerializeField] SoundChunk[] soundChunks;
    [SerializeField] SoundPiece[] soundPieces;
    [SerializeField] SoundCast[] soundCasts;
    private Rigidbody2D thisRB;

    /* Disabled until I decide to upgrade this
    float calculateDistanceMultiplier(GameObject ear, float soundReductionScale)
    {
        return Vector2.Distance(ear.transform.position, gameObject.transform.position);
    }

    public void soundCastFiltered(string castName, string soundName)
    {
        SoundCast sc = System.Array.Find(soundCasts, soundCasts => soundCasts.castName == castName);
    }
    */

    // Awake is a preload
    void Awake()
    {
        instance = this;
        thisRB = gameObject.GetComponent<Rigidbody2D>();
        foreach (SoundChain SoundChain in soundChains)
            foreach (SoundChunk SoundChunk in soundChunks)
                foreach (SoundPiece SoundPiece in soundPieces)
                {
                    SoundPiece.source = gameObject.AddComponent<AudioSource>();
                    SoundPiece.source.clip = SoundPiece.Audio;
                    SoundPiece.source.volume = SoundPiece.originalMaxVolume = SoundChain.Volume * SoundChunk.Volume * SoundPiece.Volume;
                    SoundPiece.source.pitch = SoundPiece.originalPitch = SoundChain.Pitch * SoundChunk.Pitch * SoundPiece.Pitch;
                }
        foreach (SoundChunk SoundChunk in soundChunks)
            foreach (SoundPiece SoundPiece in soundPieces)
            {
                SoundPiece.source = gameObject.AddComponent<AudioSource>();
                SoundPiece.source.clip = SoundPiece.Audio;
                SoundPiece.source.volume = SoundPiece.originalMaxVolume = SoundChunk.Volume * SoundPiece.Volume;
                SoundPiece.source.pitch = SoundPiece.originalPitch = SoundChunk.Pitch * SoundPiece.Pitch;
            }
        foreach (SoundPiece SoundPiece in soundPieces)
        {
            SoundPiece.source = gameObject.AddComponent<AudioSource>();
            SoundPiece.source.clip = SoundPiece.Audio;
            SoundPiece.source.volume = SoundPiece.originalMaxVolume = SoundPiece.Volume;
            SoundPiece.source.pitch = SoundPiece.originalPitch = SoundPiece.Pitch;
        }
    }

    void Update()
    {
        //Update each soundPiece under used of their AudioSource volume everytime the ear or the gameobject moves
        foreach (SoundChain SoundChain in soundChains)
            foreach (SoundChunk SoundChunk in soundChunks)
                foreach (SoundPiece SoundPiece in soundPieces)
                {
                    if (SoundPiece.source.isPlaying)
                    {
                        SoundCast SC = System.Array.Find(soundCasts, soundCasts => soundCasts.castName == SoundPiece.soundCastName);
                        Collider2D[] ears = Physics2D.OverlapCircleAll(gameObject.transform.position, SC.soundRange, SC.earsLayer);
                        if (SC != null)
                        {
                            SoundPiece.source.volume = 0f;
                            foreach (Collider2D ear in ears)
                            {
                                if (ear.gameObject.tag == SC.earsTag)
                                {
                                    Rigidbody2D rb = ear.GetComponent<Rigidbody2D>();
                                    SoundPiece.originalMaxVolume = SoundChain.Volume * SoundChunk.Volume * SoundPiece.Volume;
                                    SoundPiece.originalPitch = SoundChain.Pitch * SoundChunk.Pitch * SoundPiece.Pitch;
                                    float distance = Vector2.Distance(ear.transform.position, gameObject.transform.position);
                                    SoundPiece.source.volume = (1f - (distance / SC.soundRange)) * SoundPiece.originalMaxVolume;
                                    SoundPiece.source.panStereo = -ear.transform.position.x / SC.soundRange;
                                    //SoundPiece.source.pitch = (1f - (distance / SC.soundRange)) * SoundPiece.originalPitch;
                                    break;
                                }
                            }
                        }
                    }
                }
        foreach (SoundChunk SoundChunk in soundChunks)
            foreach (SoundPiece SoundPiece in soundPieces)
            {
                if (SoundPiece.source.isPlaying)
                {
                    SoundCast SC = System.Array.Find(soundCasts, soundCasts => soundCasts.castName == SoundPiece.soundCastName);
                    Collider2D[] ears = Physics2D.OverlapCircleAll(gameObject.transform.position, SC.soundRange, SC.earsLayer);
                    if (SC != null)
                    {
                        SoundPiece.source.volume = 0f;
                        foreach (Collider2D ear in ears)
                        {
                            if (ear.gameObject.tag == SC.earsTag)
                            {
                                Rigidbody2D rb = ear.GetComponent<Rigidbody2D>();
                                SoundPiece.originalMaxVolume = SoundChunk.Volume * SoundPiece.Volume;
                                SoundPiece.originalPitch = SoundChunk.Pitch * SoundPiece.Pitch;
                                float distance = Vector2.Distance(ear.transform.position, gameObject.transform.position);
                                SoundPiece.source.volume = (1f - (distance / SC.soundRange)) * SoundPiece.originalMaxVolume;
                                SoundPiece.source.panStereo = -ear.transform.position.x / SC.soundRange;
                                //SoundPiece.source.pitch = (1f - (distance / SC.soundRange)) * SoundPiece.originalPitch;
                                break;
                            }
                        }
                    }
                }
            }
        foreach (SoundPiece SoundPiece in soundPieces)
        {
            if (SoundPiece.source.isPlaying)
            {
                SoundCast SC = System.Array.Find(soundCasts, soundCasts => soundCasts.castName == SoundPiece.soundCastName);
                Collider2D[] ears = Physics2D.OverlapCircleAll(gameObject.transform.position, SC.soundRange, SC.earsLayer);
                if (SC != null)
                {
                    SoundPiece.source.volume = 0f;
                    foreach (Collider2D ear in ears)
                    {
                        if (ear.gameObject.tag == SC.earsTag)
                        {
                            Rigidbody2D rb = ear.GetComponent<Rigidbody2D>();
                            Debug.Log(rb.velocity.y);
                            Debug.Log(rb.velocity.x);
                            Debug.Log(thisRB.velocity.y);
                            Debug.Log(thisRB.velocity.x);
                            SoundPiece.originalMaxVolume = SoundPiece.Volume;
                            SoundPiece.originalPitch = SoundPiece.Pitch;
                            float distance = Vector2.Distance(ear.transform.position, gameObject.transform.position);
                            SoundPiece.source.volume = (1f - (distance / SC.soundRange)) * SoundPiece.originalMaxVolume;
                            SoundPiece.source.panStereo = -ear.transform.position.x / SC.soundRange;
                            //SoundPiece.source.pitch = (1f - (distance / SC.soundRange)) * SoundPiece.originalPitch;
                            break;
                        }
                    }
                }
            }
        }
    }

    //----------------------------------------------------------------------------------------------------------- !!!Code Divider to Keep it clean

    public void PlayChain(int soundChainID, float Volume) //Chain plays chunks of sound until there is no more in that chain, set the delay to 0 on the sound chunk to make them play together
    {
        StartCoroutine(playSoundChain(soundChainID, Volume));
    }

    public void PlayChunk(int soundChunkID, float Volume) //Chunk randomizes 1 sound from the list to create uniqueness
    {
        StartCoroutine(playSoundChunk(soundChunkID, Volume));
    }

    public void PlayPiece(int soundPieceID, float Volume) //Piece plays a sound based on volume and pitch
    {
        //soundPieces[soundPieceID].source.PlayDelayed(soundPieces[soundPieceID].delayBefore);
        StartCoroutine(playSoundPiece(soundPieceID, Volume));
    }

    //----------------------------------------------------------------------------------------------------------- !!!Code Divider to Keep it clean

    public void PlayChain(string soundChainName, float Volume) //Chain plays chunks of sound until there is no more in that chain, set the delay to 0 on the sound chunk to make them play together, overload for name instead of id
    {
        //System.Array.Find(soundChains, soundChains => soundChains.chainName == soundChainName); !!!Note Possible usable system method, very useful
        StartCoroutine(playSoundChain(System.Array.IndexOf(soundChains, System.Array.Find(soundChains, soundChains => soundChains.chainName == soundChainName)), Volume)); //Using system to search for the instance value of array with the desired name, and pick it's instance index for array call usage
    }
    public void PlayChunk(string soundChunkName, float Volume) //Chunk randomizes 1 sound from the list to create uniqueness, overload for name instead of id
    {
        StartCoroutine(playSoundChunk(System.Array.IndexOf(soundChains, System.Array.Find(soundChunks, soundChunks => soundChunks.chunkName == soundChunkName)), Volume)); //Using system to search for the instance value of array with the desired name, and pick it's instance index for array call usage
    }

    public void PlayPiece(string soundPieceName, float Volume) //Piece plays a sound based on volume and pitch, overload for name instead of id
    {
        StartCoroutine(playSoundPiece(System.Array.IndexOf(soundChains, System.Array.Find(soundPieces, soundPieces => soundPieces.pieceName == soundPieceName)), Volume)); //Using system to search for the instance value of array with the desired name, and pick it's instance index for array call usage
    }

    void OnDrawGizmosSelected()
    {
        foreach (SoundCast SoundCast in soundCasts)
        {
            if (SoundCast == null)
            {
                return;
            }
            Gizmos.DrawWireSphere(gameObject.transform.position, SoundCast.soundRange);
        }
    }

    //----------------------------------------------------------------------------------------------------------- !!!Code Divider to Keep it clean

    IEnumerator playSoundChain(int soundChainID, float Volume) //An IEnumerator to play the specific sound(Chain, Chunk or Piece) by ID
    {
        for (int i = 0; i < soundChains[soundChainID].soundChunks.Length; i++)
        {
            int rand = Random.Range(0, soundChains[soundChainID].soundChunks[i].soundPieces.Length); //Produce the random instance of the choosen chunk in the chain, picked and used to check for the following arrays
            yield return new WaitForSeconds(soundChains[soundChainID].soundChunks[i].soundPieces[rand].delayBefore); //Delay the sound until as stated duration later, used for timing
            soundChains[soundChainID].soundChunks[i].soundPieces[rand].source.Play(); //Play the attributed sound source
            //audioSource.PlayOneShot(soundChains[soundChainID].soundChunks[i].soundPieces[rand].Audio, soundChains[soundChainID].Volume * soundChains[soundChainID].soundChunks[i].Volume * soundChains[soundChainID].soundChunks[i].soundPieces[rand].Volume * Volume); //Play the sound stated following the Volume multipliers
        }
        if (soundChains[soundChainID].loop)
        {
            StartCoroutine(playSoundChain(soundChainID, Volume));
        }
    }

    IEnumerator playSoundChunk(int soundChunkID, float Volume) //An IEnumerator to play the specific sound(Chain, Chunk or Piece) by ID
    {
        int rand = Random.Range(0, soundChunks[soundChunkID].soundPieces.Length); //Produce the random instance of the choosen sound in the chunk, picked and used to check for the following arrays
        yield return new WaitForSeconds(soundChunks[soundChunkID].soundPieces[rand].delayBefore); //Delay the sound until as stated duration later, used for timing
        soundChunks[soundChunkID].soundPieces[rand].source.Play(); //Play the attributed sound source
        if (soundChunks[soundChunkID].loop)
            yield return new WaitWhile(() => soundChunks[soundChunkID].soundPieces[rand].source.isPlaying);

        //audioSource.PlayOneShot(soundChunks[soundChunkID].soundPieces[rand].Audio, soundChunks[soundChunkID].Volume * soundChunks[soundChunkID].soundPieces[rand].Volume * Volume); //Play the sound stated following the Volume multipliers

        if (soundChunks[soundChunkID].loop)
        {
            StartCoroutine(playSoundChunk(soundChunkID, Volume));
        }
    }

    IEnumerator playSoundPiece(int soundPieceID, float Volume) //An IEnumerator to play the specific sound(Chain, Chunk or Piece) by ID
    {
        yield return new WaitForSeconds(soundPieces[soundPieceID].delayBefore); //Delay the sound until as stated duration later, used for timing
        soundPieces[soundPieceID].source.Play(); //Play the attributed sound source
        //Debug.Log(soundPieces[soundPieceID].source.clip.length);
        if (soundPieces[soundPieceID].loop)
            yield return new WaitForSeconds(soundPieces[soundPieceID].source.clip.length);

        //audioSource.PlayOneShot(soundPieces[soundPieceID].Audio, soundPieces[soundPieceID].Volume * Volume); //Play the sound stated following the Volume multipliers

        if (soundPieces[soundPieceID].loop)
        {
            StartCoroutine(playSoundPiece(soundPieceID, Volume));
        }
    }
}


[System.Serializable]
public class SoundChain
{
    public string chainName;
    public SoundChunk[] soundChunks;
    public bool loop;
    [Range(0f, 2f)] public float Volume = 1f;
    [Range(0.1f, 3f)] public float Pitch = 0;
}


[System.Serializable]
public class SoundChunk
{
    public string chunkName;
    public SoundPiece[] soundPieces;
    public bool loop;
    [Range(0f, 2f)] public float Volume = 1f;
    [Range(0.1f, 3f)] public float Pitch = 0;
}


[System.Serializable]
public class SoundPiece
{
    public string pieceName;
    public AudioClip Audio;
    public string soundCastName;
    public bool loop;
    [Range(0f, 10f)] public float delayBefore = 1f;
    [Range(0f, 2f)] public float Volume = 1f;
    [Range(0.1f, 3f)] public float Pitch = 1f;

    [HideInInspector] public float originalMaxVolume;
    [HideInInspector] public float originalPitch;
    [HideInInspector] public AudioSource source;
}

[System.Serializable]
public class SoundCast
{
    public string castName;
    public string earsTag;
    public LayerMask earsLayer;
    [Range(0f, 100f)] public float soundRange;
}
