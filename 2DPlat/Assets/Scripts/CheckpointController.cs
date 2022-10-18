using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    public Sprite lampUnlit;
    public Sprite lampLit;
    private SpriteRenderer checkpointSpriteRenderer;
    public bool checkpointReached;

    // Start is called before the first frame update
    void Start()
    {
        checkpointSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D (Collider2D other) {
        if (other.tag == "Player") {
            checkpointSpriteRenderer.sprite = lampLit;
            checkpointReached = true;
        }
    }
}
