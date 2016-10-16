using UnityEngine;
using System.Collections;

public class EatContext : ContextBase {
    

    public void OnMouseDown()
    {
        float distance = Mathf.Sqrt(Mathf.Pow(Player.transform.position.x - (transform.position.x + 1), 2) + Mathf.Pow(Player.transform.position.y - (transform.position.y - 2), 2));
        Debug.Log(distance);
        if (distance < range)
        {
            
            m.target = transform;
            m.doit = true;
            Debug.Log(transform.parent.transform.parent.gameObject);
            m.nutrition = nutrition;
            m.gg = transform.parent.transform.parent.gameObject;
        }
        ContextSprite.SetActive(false);
    }


 
}
