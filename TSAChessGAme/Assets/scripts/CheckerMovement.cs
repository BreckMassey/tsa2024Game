using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckerMovement : MonoBehaviour
{
    public float speed;
    public float rotationSpeed;
    public float damage;
    public Transform enemyToTrack;
    public Sprite[] checkerSprites;
    float timeAlive;
    Vector3 originalScale;
    public GameObject particlePrefab;
    // Start is called before the first frame update
    void Start()
    {
        transform.GetComponent<SpriteRenderer>().sprite = checkerSprites[Random.Range(0, checkerSprites.Length)];
        originalScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        timeAlive += Time.deltaTime;
        if (timeAlive > 10) {
            transform.localScale = originalScale*(10.5f-timeAlive);
            if (timeAlive > 10.5) {
                Destroy(gameObject);
            }
        }


        transform.Translate(new Vector2(speed*Time.deltaTime,0));
        if (enemyToTrack != null&&timeAlive<=10)
        {
            float angle = Vector2.SignedAngle(transform.right, enemyToTrack.transform.position - transform.position);
            transform.Rotate(0, 0, rotationSpeed * Time.deltaTime * Mathf.Sign(angle));

        }
        else {
            GameObject[] possibleTargets = GameObject.FindGameObjectsWithTag("enemy");
            int closest = 0;
            float score = 100000;
            for (int i = 0; i < possibleTargets.Length; i++) {
                float currentScore = Vector2.Distance(possibleTargets[i].transform.position, transform.position);
                currentScore += Vector2.Angle(transform.right, possibleTargets[i].transform.position - transform.position)/10;
                if (currentScore < score) {
                    closest = i;
                    score = currentScore;
                }
            }
            enemyToTrack = possibleTargets[closest].transform;   //FindGameObjectsWithTag
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "enemy") {
            collision.transform.GetComponent<BossParent>().damage(damage);
        }
        
        Instantiate(particlePrefab,transform.position,Quaternion.identity);
        
        gameObject.GetComponent<CircleCollider2D>().enabled = false;
        timeAlive = Mathf.Max(timeAlive, 10);
        Destroy(gameObject);
    }
}
