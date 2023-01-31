using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] public GameObject buildMenu1; 
    [SerializeField] public GameObject buildMenu2; 
    [SerializeField] public GameObject buildMenu3; 
    [SerializeField] public GameObject buildMenu4; 
    
    
    // Start is called before the first frame update
    void Start()
    {
        buildMenu1.gameObject.SetActive(false);
        buildMenu2.gameObject.SetActive(false);
        buildMenu3.gameObject.SetActive(false);
        buildMenu4.gameObject.SetActive(false);
    }

    // Update is called once per frame
  
    public void ToggleMenu1()
    {
        buildMenu1.gameObject.SetActive(!buildMenu1.activeInHierarchy);
    }
    public void ToggleMenu2()
    {
        buildMenu2.gameObject.SetActive(!buildMenu2.activeInHierarchy);
    }
    public void ToggleMenu3()
    {
        buildMenu3.gameObject.SetActive(!buildMenu3.activeInHierarchy);
    }
    public void ToggleMenu4()
    {
        buildMenu4.gameObject.SetActive(!buildMenu4.activeInHierarchy);
    }
}
