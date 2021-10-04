using UnityEngine;

public class TestSounds : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SoundManager sm = gameObject.GetComponent<SoundManager>();
        sm.PlayPiece(0, 1f);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
