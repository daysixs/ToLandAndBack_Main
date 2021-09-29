using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneEdgeCore : MonoBehaviour
{
    private Vector3 stageDimensions;
    private GameObject target;
    private Transform trans;
    private BoxCollider2D boxCollider;
    private Rigidbody2D rb;
    [SerializeField] GameObject transportedObject;
    [SerializeField] float extraFloat = 0.001f;
    [SerializeField] bool mirrorEdge = false;
    [SerializeField] bool initialScene = false;
    [SerializeField] bool retainAxis = true;
    [SerializeField] bool retainVelocity = true;
    [SerializeField] bool leftdeadlyEdge = false;
    [SerializeField] bool rightdeadlyEdge = false;
    [SerializeField] bool updeadlyEdge = false;
    [SerializeField] bool downdeadlyEdge = false;
    [SerializeField] bool upExit = false;
    [SerializeField] bool downExit = false;
    [SerializeField] bool leftExit = false;
    [SerializeField] bool rightExit = false;
    [SerializeField] string upScene;
    [SerializeField] string downScene;
    [SerializeField] string leftScene;
    [SerializeField] string rightScene;

    /*Note Space
     * Treat this behaviour like its a level manager...because it is! Why would it be a bug? Its a feature. 
     * Put this behaviour into a SceneMaster of every new scene and you can control which level the player ends up to.
     * Yes, there is in fact a lot of Serialized Field, looks ugly, but too bad.
     * Also needs to have the if, if else replaced with switches (They are rarely called anyway, but whatever.)
     */
    void Awake()
    {
        stageDimensions = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
    }
    // Start is called before the first frame update
    void Start()
    {
        if (initialScene == false)
        {
            string lastEnterFrom = PlayerPrefs.GetString("lastEnterFrom", "");
            float velocityX = PlayerPrefs.GetFloat("retainVelocityX", 0);
            float velocityY = PlayerPrefs.GetFloat("retainVelocityY", 0);
            float positionX = PlayerPrefs.GetFloat("retainAxisX", 0);
            float positionY = PlayerPrefs.GetFloat("retainAxisY", 0);
            GameObject sceneObject = Instantiate(transportedObject, new Vector3(0, 0), transform.rotation) as GameObject;
            target = GameObject.FindGameObjectWithTag("Player");
            trans = target.GetComponent<Transform>();
            rb = target.GetComponent<Rigidbody2D>();
            boxCollider = target.GetComponent<BoxCollider2D>();
            if (lastEnterFrom == "rightEnter")
            {
                Vector3 position = new Vector3(stageDimensions.x + boxCollider.bounds.extents.x - extraFloat, positionY);
                sceneObject.transform.position = position;
                sceneObject.transform.rotation = new Quaternion(0, 1, 0, 0);
                sceneObject.GetComponent<Rigidbody2D>().velocity = new Vector3(velocityX, velocityY);
            }
            else if (lastEnterFrom == "leftEnter")
            {
                Vector3 position = new Vector3(-stageDimensions.x - boxCollider.bounds.extents.x + extraFloat, positionY);
                sceneObject.transform.position = position;
                sceneObject.GetComponent<Rigidbody2D>().velocity = new Vector3(velocityX, velocityY);
            }
            else if (lastEnterFrom == "upEnter")
            {
                Vector3 position = new Vector3(stageDimensions.y + boxCollider.bounds.extents.y - extraFloat, positionY);
                sceneObject.transform.position = position;
                sceneObject.GetComponent<Rigidbody2D>().velocity = new Vector3(velocityX, velocityY);
            }
            else if (lastEnterFrom == "downEnter")
            {
                Vector3 position = new Vector3(-stageDimensions.y - boxCollider.bounds.extents.y + extraFloat, positionY);
                sceneObject.transform.position = position;
                sceneObject.GetComponent<Rigidbody2D>().velocity = new Vector3(velocityX, velocityY);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

        target = GameObject.FindGameObjectWithTag("Player");
        trans = target.GetComponent<Transform>();
        rb = target.GetComponent<Rigidbody2D>();
        boxCollider = target.GetComponent<BoxCollider2D>();
        if (trans.position.x < -stageDimensions.x + boxCollider.bounds.extents.x)
        {
            if (leftdeadlyEdge == false && leftExit == false && mirrorEdge == false)
            {
                target.transform.position = new Vector3(-stageDimensions.x + boxCollider.bounds.extents.x + extraFloat, trans.transform.position.y);
            }
            else if (trans.position.x < -stageDimensions.x - boxCollider.bounds.extents.x)
            {
                if (leftExit == true)
                {
                    PlayerPrefs.SetString("lastEnterFrom", "rightEnter");
                    SceneManager.LoadScene(leftScene);
                    if (retainAxis == true)
                    {
                        PlayerPrefs.SetFloat("retainAxisY", trans.position.y);
                    }
                    if (retainVelocity == true)
                    {
                        PlayerPrefs.SetFloat("retainVelocityX", rb.velocity.x);
                        PlayerPrefs.SetFloat("retainVelocityY", rb.velocity.y);
                    }
                }
                else if (mirrorEdge == true)
                {
                    target.transform.position = new Vector3(stageDimensions.x + boxCollider.bounds.extents.x - extraFloat, trans.transform.position.y);
                }
                else if (leftdeadlyEdge == true)
                {
                    HealthSystem health = target.GetComponent<HealthSystem>();
                    health.TakeDamage(health.maxHealth);
                }
            }
        }
        if (trans.position.x > stageDimensions.x - boxCollider.bounds.extents.x)
        {
            if (rightdeadlyEdge == false && rightExit == false && mirrorEdge == false)
            {
                target.transform.position = new Vector3(stageDimensions.x - boxCollider.bounds.extents.x - extraFloat, trans.transform.position.y);
            }
            else if (trans.position.x > stageDimensions.x + boxCollider.bounds.extents.x)
            {
                if (rightExit == true)
                {
                    PlayerPrefs.SetString("lastEnterFrom", "leftEnter");
                    SceneManager.LoadScene(rightScene);
                    if (retainAxis == true)
                    {
                        PlayerPrefs.SetFloat("retainAxisY", trans.position.y);
                    }
                    if (retainVelocity == true)
                    {
                        PlayerPrefs.SetFloat("retainVelocityX", rb.velocity.x);
                        PlayerPrefs.SetFloat("retainVelocityY", rb.velocity.y);
                    }
                }
                else if (mirrorEdge == true)
                {
                    target.transform.position = new Vector3(-stageDimensions.x - boxCollider.bounds.extents.x + extraFloat, trans.transform.position.y);
                }
                else if (rightdeadlyEdge == true)
                {
                    HealthSystem health = target.GetComponent<HealthSystem>();
                    health.TakeDamage(health.maxHealth);
                }
            }
        }
        if (trans.position.y < -stageDimensions.y + boxCollider.bounds.extents.y)
        {
            if (downdeadlyEdge == false && downExit == false && mirrorEdge == false)
            {
                target.transform.position = new Vector3(trans.transform.position.x, -stageDimensions.y + boxCollider.bounds.extents.y + extraFloat);
            }
            else if (trans.position.y < -stageDimensions.y - boxCollider.bounds.extents.y)
            {
                if (downExit == true)
                {
                    PlayerPrefs.SetString("lastEnterFrom", "upEnter");
                    SceneManager.LoadScene(downScene);
                    if (retainAxis == true)
                    {
                        PlayerPrefs.SetFloat("retainAxisX", trans.position.x);
                    }
                    if (retainVelocity == true)
                    {
                        PlayerPrefs.SetFloat("retainVelocityX", rb.velocity.y);
                        PlayerPrefs.SetFloat("retainVelocityY", rb.velocity.y);
                    }
                }
                else if (mirrorEdge == true)
                {
                    target.transform.position = new Vector3(trans.transform.position.y, stageDimensions.y + boxCollider.bounds.extents.y - extraFloat);
                }
                else if (downdeadlyEdge == true)
                {
                    HealthSystem health = target.GetComponent<HealthSystem>();
                    health.TakeDamage(health.maxHealth);
                }
            }
        }
        if (trans.position.y > stageDimensions.y - boxCollider.bounds.extents.y)
        {
            if (updeadlyEdge == false && upExit == false && mirrorEdge == false)
            {
                target.transform.position = new Vector3(trans.transform.position.x, stageDimensions.y - boxCollider.bounds.extents.y - extraFloat);
            }
            else if (trans.position.y > stageDimensions.y + boxCollider.bounds.extents.y)
            {
                if (upExit == true)
                {
                    PlayerPrefs.SetString("lastEnterFrom", "downEnter");
                    SceneManager.LoadScene(upScene);
                    if (retainAxis == true)
                    {
                        PlayerPrefs.SetFloat("retainAxisX", trans.position.x);
                    }
                    if (retainVelocity == true)
                    {
                        PlayerPrefs.SetFloat("retainVelocityX", rb.velocity.y);
                        PlayerPrefs.SetFloat("retainVelocityY", rb.velocity.y);
                    }
                }
                else if (mirrorEdge == true)
                {
                    target.transform.position = new Vector3(trans.transform.position.y, -stageDimensions.y - boxCollider.bounds.extents.y + extraFloat);
                }
                else if (updeadlyEdge == true)
                {
                    HealthSystem health = target.GetComponent<HealthSystem>();
                    health.TakeDamage(health.maxHealth);
                }
            }
        }

    }


}
