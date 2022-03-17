using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gecko : MonoBehaviour
{
    [SerializeField]
    private Transform[] routes;

    private int routeToGo;

    private float tParam;
    private Vector2 geckoPos;
    private float speeddModifier;
    private bool coroutineAllowed;

    private Animator geckoAnimator;

    public bool playerIsNear;

    // Start is called before the first frame update
    void Start()
    {
        geckoAnimator = GetComponent<Animator>();
        routeToGo = 0;
        tParam = 0f;
        speeddModifier = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (coroutineAllowed)
        {

            geckoAnimator.SetBool("running", true);
            StartCoroutine(GoByTheRoute(routeToGo));
        }
    }

    private IEnumerator GoByTheRoute(int routeNumber)
    {
        coroutineAllowed = false;

        Vector2 p0 = routes[routeNumber].GetChild(0).position;
        Vector2 p1 = routes[routeNumber].GetChild(1).position;
        Vector2 p2 = routes[routeNumber].GetChild(2).position;
        Vector2 p3 = routes[routeNumber].GetChild(3).position;

        while(tParam < 1)
        {
            tParam += Time.deltaTime * speeddModifier;

            geckoPos = Mathf.Pow(1 - tParam, 3) * p0 +
                3 * Mathf.Pow(1 - tParam, 2) *tParam * p1 +
                3 * (1 - tParam) * Mathf.Pow(tParam, 2) * p2 +
                Mathf.Pow(tParam, 3) * p3;

            transform.position = geckoPos;
            yield return new WaitForEndOfFrame();
        }
        geckoAnimator.SetBool("running", false);
        tParam = 0f;
        routeToGo += 1;
        if (routeToGo > routes.Length - 1)
            routeToGo = 0;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            coroutineAllowed = true;
            Debug.Log(playerIsNear);
            //geckoAnimator.SetBool("running", true);
        }
    }

}