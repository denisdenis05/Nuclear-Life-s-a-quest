using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class somnoros : MonoBehaviour
{
    public Animator somnorosanim;
    private Vector2[] pozitii = new Vector2[6]; 
    // Start is called before the first frame update
    void Start()
    {
        
        pozitii[0] = new Vector2(248.6f, 352.9f);
        pozitii[1] = new Vector2(239.5363f, 291.3428f);
        pozitii[2] = new Vector2(268.5f, 241.4f);
        pozitii[3] = new Vector2(269.3f, 138.6f);
        pozitii[4] = new Vector2(253.1f, 77.3f);
        pozitii[5] = new Vector2(241.1f, -19.5f);
        updatepoz();
        
    }

    public void updatepoz()
    {
        float rand = Random.Range(1, 3);
        int i = Random.Range(1, 5);
        somnorosanim.SetFloat("nranim", rand);
        gameObject.transform.position = pozitii[i];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
