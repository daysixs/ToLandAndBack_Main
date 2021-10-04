using UnityEditor;

[CustomEditor(typeof(SoundManager))]
public class SoundTestKeys : Editor
{
    //Note: Editor based Coroutine with a wait for second, does not work in unity, trash, garbage, so, you will have to live with using only the normal playing to try the sounds
    /*public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        SoundManager playSound = (SoundManager)target;

        if (GUILayout.Button("TestPiece"))
        {
            playSound.PlayPiece(0);
            Debug.Log("PlayPiece");
        }
        if (GUILayout.Button("TestChunk"))
        {
            playSound.PlayChunk(0);
            Debug.Log("PlayChunk");
        }
        if (GUILayout.Button("TestChain"))
        {
            playSound.PlayChain(0);
            Debug.Log("PlayChain");
        }
    }*/
}
