/*
 * The Check Poly(gons) Colliders is used to ensure every game piece under the 
 * "Main" game object has a Polygon Collider component attached.
 * 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPolyColliders : MonoBehaviour
{
    [SerializeField] Component[] sprites;

    void Awake()
    {
        sprites = gameObject.transform.GetComponentsInChildren(typeof(SpriteRenderer));

        foreach (var sprite in sprites)
        {
            if (sprite.GetComponent<PolygonCollider2D>() == null && sprite.name != "Main")
            {
                // Adds a PolygonCollider and sets the game pieces tag name to "PlayablePiece".
                sprite.gameObject.AddComponent<PolygonCollider2D>();
                sprite.gameObject.tag = "PlayablePiece";
            }
        }
    }
}
