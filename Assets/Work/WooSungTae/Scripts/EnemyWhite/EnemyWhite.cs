using System.Collections;
using UnityEngine;

public class EnemyWhite : MonoBehaviour
{
    [SerializeField] private SpriteRenderer hitSprite;
    [SerializeField] private float flashSpeed;

    public void HitEnemy()
    {
        StartCoroutine(HitFlash());
    }

   IEnumerator HitFlash()
    {
        float flash = 1;
        hitSprite.material.SetFloat("_Flash", flash);

        while (flash > 0f)
        {
            flash -= Time.unscaledDeltaTime * flashSpeed;
            hitSprite.material.SetFloat("_Flash", flash);
            yield return null;
        }
    }
}
