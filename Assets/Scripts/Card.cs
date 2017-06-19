using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour {
    public Sprite front;
    public Sprite back;
    bool rotate;
    public bool turned;
    public float rotateSpeed;
	// Use this for initialization
	void Start () {
        rotate = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        if (rotate)
        {
            if (transform.localRotation.eulerAngles.y >= 90)
            {
                GetComponent<Image>().sprite = back;
            }
            if(transform.localRotation.eulerAngles.y >= 178)
            {
                rotate = false;
                turned = true;
                transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
            }
            transform.Rotate(new Vector3(0, 90 * Time.deltaTime, 0));
        }
        
	}
    public void ClickCard()
    {
        if (!turned)
        {
            rotate = true;
            
        }
       
    }
    public IEnumerator AutoChangeCard()
    {
        yield return new WaitForSeconds(20.0f);
        GetComponent<Image>().sprite = back;
    }
}
