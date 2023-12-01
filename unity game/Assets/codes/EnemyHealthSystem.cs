using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthSystem : MonoBehaviour
{

    public int initialHitPoints = 2;
    private int hitPointsLeft;
    private SpriteRenderer sr;
    private Color initialSpriteColour;
    private Color deathColour = Color.red;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        initialSpriteColour = sr.color;
        hitPointsLeft = initialHitPoints;
    }

   public void RecieveHit (int damage)
    {
        hitPointsLeft = hitPointsLeft - damage;
        ChangeColour();
        if(hitPointsLeft <= 0)
        {
            Destroy(gameObject);
        }
    }

    void ChangeColour()
    {
        float persantageOfDamageTaken = 1f - ((float)hitPointsLeft / (float)initialHitPoints);
        Color newHealthColor = Color.Lerp(initialSpriteColour, deathColour, persantageOfDamageTaken);
        sr.color = newHealthColor;
    }
}
