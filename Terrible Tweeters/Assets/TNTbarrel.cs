using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TNTbarrel : MonoBehaviour
{
    [SerializeField] AudioClip[] _clips;
    [SerializeField] ParticleSystem _particleSystem;

    bool _hasDied;
    public float radius = 5.0f;
    public float explosionForce = 2000f;
    public LayerMask affectedLayers;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var collision_mag = collision.relativeVelocity.magnitude;
        if (collision_mag > 1f && collision_mag < 3f)
        {
            int index = Random.Range(0, 2);
            AudioClip clip = _clips[index];
            GetComponent<AudioSource>().PlayOneShot(clip);
        }
        else if (collision_mag > 3f)
        {
            if (ShouldDieFromCollision(collision))
            {
                StartCoroutine(Die());
            }
        }
    }

    IEnumerator Die()
    {
        _hasDied = true;

        GetComponent<Rigidbody2D>().simulated = false;
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<PolygonCollider2D>().enabled = false;

        _particleSystem.Play();
        int index = Random.Range(3, 6);
        AudioClip clip = _clips[index];
        GetComponent<AudioSource>().PlayOneShot(clip);

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius, affectedLayers);
        foreach (Collider2D col in colliders)
        {
            

            // If the object has a Rigidbody2D, apply explosion force
            Rigidbody2D rb = col.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 direction = (col.transform.position - transform.position).normalized;
                rb.AddForce(direction * explosionForce, ForceMode2D.Impulse);
            }

            // Check if the object has a Die() method
            //MonoBehaviour script = col.GetComponent<MonoBehaviour>();
            //IEnumerator dieCoroutine = script.GetType().GetMethod("Die")?.Invoke(script, null) as IEnumerator;
            //if (dieCoroutine != null)
            //{
                //StartCoroutine(dieCoroutine);
            //}

        }

        

        

        yield return new WaitForSeconds(2);
        gameObject.SetActive(false);
    }

    bool ShouldDieFromCollision(Collision2D collision)
    {
        if (_hasDied)
            return false;

        Bird bird = collision.gameObject.GetComponent<Bird>();
        if (bird != null)
            return true;

        if (collision.contacts[0].normal.y < -0.5)
            return true;

        return false;
    }
}
