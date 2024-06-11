using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Renderer))]
public class Goal : MonoBehaviour
{
    static public bool goalMet = false;
    // Start is called before the first frame update

    void OnTriggerEnter(Collider other)
    {
        // when trigger hit, check if its a projectile
        Projectile proj = other.GetComponent<Projectile>();
        if (proj != null) {
            Goal.goalMet = true;
            // set alpha of color to higher intensity
            Material mat = GetComponent<Renderer>().material;
            Color c = mat.color;
            c.a = 0.75f;
            mat.color = c;
        }
    }
   
}
