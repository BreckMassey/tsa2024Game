using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCheckerMovement : MonoBehaviour
{
    public float speed;
    public float rotationSpeed;
    public float damage;
    public Transform enemyToTrack;
    public Sprite[] checkerSprites;
    float timeAlive;
    Vector3 originalScale;
    public GameObject particlePrefab;
    float intY;
    // Start is called before the first frame update
    void Start()
    {
        transform.GetComponent<SpriteRenderer>().sprite = checkerSprites[Random.Range(0, checkerSprites.Length)];
        originalScale = transform.localScale;
        intY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        timeAlive += Time.deltaTime;
        if (timeAlive > 3)
        {
            transform.localScale = originalScale * (3.5f - timeAlive);
            if (timeAlive > 3.5)
            {
                Instantiate(particlePrefab, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }


        transform.Translate(new Vector2(speed * Time.deltaTime, 0));
       


        if (enemyToTrack != null && timeAlive <= 10)
        {
            float angle = Vector2.SignedAngle(transform.right, enemyToTrack.transform.position - transform.position);
            transform.Rotate(0, 0, rotationSpeed * Time.deltaTime * Mathf.Sign(angle));

        }
        else
        {
            GameObject[] possibleTargets = GameObject.FindGameObjectsWithTag("player");
            int closest = 0;
            float score = 100000;
            for (int i = 0; i < possibleTargets.Length; i++)
            {
                float currentScore = Vector2.Distance(possibleTargets[i].transform.position, transform.position);
                currentScore += Vector2.Angle(transform.right, possibleTargets[i].transform.position - transform.position) / 10;
                if (currentScore < score)
                {
                    closest = i;
                    score = currentScore;
                }
            }
            enemyToTrack = possibleTargets[closest].transform;   //FindGameObjectsWithTag
        }
    }

   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "player") 
        {
            collision.transform.GetComponent<PlayerController2>().takeDamage();
            Instantiate(particlePrefab, transform.position, Quaternion.identity);

            gameObject.GetComponent<CircleCollider2D>().enabled = false;
            timeAlive = Mathf.Max(timeAlive, 10);
            Destroy(gameObject);
        }

        if (collision.tag == "wall")
        {

            Instantiate(particlePrefab, transform.position, Quaternion.identity);

            gameObject.GetComponent<CircleCollider2D>().enabled = false;
            timeAlive = Mathf.Max(timeAlive, 10);
            Destroy(gameObject);
        }
    }
}
