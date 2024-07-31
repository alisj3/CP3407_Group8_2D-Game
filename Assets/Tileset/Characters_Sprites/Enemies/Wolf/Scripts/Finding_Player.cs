using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finding_Player : MonoBehaviour
{
    [SerializeField] float m_detectionRange = 10.0f;
    [SerializeField] float m_speed = 4.0f;

    private Animator anim;

    private Transform playerTransform;
    private Rigidbody2D m_body2d;

    // Start is called before the first frame update
    void Start()
    {
        m_body2d = GetComponent<Rigidbody2D>();
        GameObject player = GameObject.Find("Takeshi_Player"); // Finding the player by name
        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.LogError("Player object 'Takeshi_Player' not found");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
