using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public Material explosionMAT;

    public bool isExploding;

    public float explosionDissipation;
    public float targetValue = 1.0f;

    public ParticleSystem explosion;
    public ParticleSystem bolts;
    public ParticleSystem screws;

    // Start is called before the first frame update
    void Start()
    {
        var explosionEmission = explosion.emission;
        var boltsEmission = bolts.emission;
        var screwsEmission = screws.emission;

        explosionMAT.SetFloat("explosionDissipation_", explosionDissipation);

        explosionEmission.enabled = false;
        screwsEmission.enabled = false;
        boltsEmission.enabled = false;

        StartCoroutine(explosionVFX(targetValue, 1f));
    }

    // Update is called once per frame
    void Update()
    {

        explosionMAT.SetFloat("explosionDissipation_", explosionDissipation);

        if (Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(explosionVFX(targetValue, 1f));

        }

        if (isExploding == true)
        {
            StartCoroutine(explosionVFX(targetValue, 1f));

        }
    }

    IEnumerator explosionVFX(float endValue, float duration)
    {
        explosionMAT.SetFloat("explosionDissipation_", explosionDissipation);
        var explosionEmission = explosion.emission;
        var boltsEmission = bolts.emission;
        var screwsEmission = screws.emission;
        explosion.Play();
        bolts.Play();
        screws.Play();
        explosionEmission.enabled = true;
        screwsEmission.enabled = true;
        boltsEmission.enabled = true;
        float time = 0;
        float startValue = explosionDissipation;

        while(time < duration)
        {
            explosionDissipation = Mathf.Lerp(startValue, endValue, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        explosionDissipation = 0;
        explosionEmission.enabled = false;
        screwsEmission.enabled = false;
        boltsEmission.enabled = false;
        isExploding = false;
    }
}
