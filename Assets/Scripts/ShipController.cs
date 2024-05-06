using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ShipController : MonoBehaviour
{
    public float speed;
    
    private CapsuleCollider collider;

    public static bool isMortal = false;

    [SerializeField]
    private int collisionDamage;

    public static int score = 0;
    public static int credits = 0;

    public static int totalKillEnemies = 0;
    public static int totalDestroyedMeteors = 0;

    [SerializeField]
    private int mustKillToFinish;


    void Awake() {
        collider = GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        // Save the current position of the gameObject
        Vector3 pos = transform.position;
        Vector3 velocity = Vector3.zero;

        #if UNITY_IOS || UNITY_ANDROID
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
    
            Vector2 touchPosition = Camera.main.ScreenToViewportPoint(touch.position);
            float horizontal = touchPosition.x - 0.5f; //
            float vertical = touchPosition.y - 0.5f;

            velocity += new Vector3(horizontal * speed, 0, vertical * speed);
        }
        #endif

        if(Input.GetKey(KeyCode.I)) {
            isMortal = !isMortal;
            Debug.Log(isMortal);

        }

        
        if (Input.GetKey(KeyCode.A))
            velocity -= speed * Vector3.right; // (1,0,0)
        if (Input.GetKey(KeyCode.D))
            velocity += speed * Vector3.right;
        if (Input.GetKey(KeyCode.S))
            velocity -= speed * Vector3.forward; // (0,0,1)
        if (Input.GetKey(KeyCode.W))
            velocity += speed * Vector3.forward;

        pos = GameUtils.Instance.ComputeEulerStep(pos, velocity, Time.deltaTime);
        pos = EnvironmentProps.Instance.IntoArea(pos, collider.radius, collider.radius);

        transform.position = pos;

        if(SceneManager.GetActiveScene().name == "Level1") {
            if(totalDestroyedMeteors == mustKillToFinish) {
                isMortal = true;
                totalDestroyedMeteors = 0;
                StartCoroutine(WinSequence());             
            }
        }
        //else {
            Debug.Log(totalKillEnemies);
            if(totalKillEnemies == mustKillToFinish) {
                isMortal = true;
                totalKillEnemies = 0;
                StartCoroutine(WinSequence());             
            }
        //}
        
    }

    IEnumerator WinSequence()
    {
        ScoreManager.Instance.YouWin();
        yield return new WaitForSeconds(5f);
        ScoreManager.Instance.YouWin();
        isMortal = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

    bool IsNextSceneFinal(string sceneName)
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            string nextScenePath = SceneUtility.GetScenePathByBuildIndex(nextSceneIndex);
            string nextSceneName = System.IO.Path.GetFileNameWithoutExtension(nextScenePath);

            return nextSceneName.Equals(sceneName);
        }
        
        return false;
    }

    //collision - player with enemy
    //coef - score 1 credits 1 
    private void OnCollisionEnter(Collision other) {
        PlayExplosion.PlayDestroyAudio();
        Health healthScript = other.gameObject.GetComponent<Health>();

        if (healthScript != null)
        {
            healthScript.DealDamage(collisionDamage);
            score += 1 * collisionDamage;
            credits += 1 * collisionDamage;
            //Debug.Log("Score is: " + score + "Credits are: " + credits);
            ScoreManager.Instance.UpdateScoreText(score);
            ScoreManager.Instance.UpdateCreditsText(credits);

            //enemy is dead
                //coef score 10 credits 2
            if(healthScript._currentHealth <= 0) {
                score += 10 * collisionDamage;
                credits += 2 * collisionDamage;
                //Debug.Log("Score is: " + score + "Credits are: " + credits);
                ScoreManager.Instance.UpdateScoreText(score);
                ScoreManager.Instance.UpdateCreditsText(credits);
            }

        }
    }
}
