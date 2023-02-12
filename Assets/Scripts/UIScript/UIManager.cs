using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.UI;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] public GameObject buildMenu1; 
    [SerializeField] public GameObject buildMenu2; 
    //[SerializeField] public GameObject buildMenu3; 
    [SerializeField] public GameObject buildMenu4; 
    [SerializeField] public GameObject Advisers;

    // Start is called before the first frame update
    void Start()
    {
        buildMenu1.gameObject.SetActive(false);
        buildMenu2.gameObject.SetActive(false);
        //buildMenu3.gameObject.SetActive(false);
        buildMenu4.gameObject.SetActive(false);
        Advisers.gameObject.SetActive(false);
    }

    // Update is called once per frame
  
    public void ToggleMenu1()
    {
        buildMenu1.gameObject.SetActive(!buildMenu1.activeInHierarchy);
        buildMenu1.gameObject.transform.localPosition = new Vector3(395, -168, 0);
    }public void ToggleAdvisers()
    {
        Advisers.gameObject.SetActive(!Advisers.activeInHierarchy);
        Advisers.gameObject.transform.localPosition = new Vector3(800, -168, 0);
    }
    public void ToggleMenu2()
    {
        buildMenu2.gameObject.SetActive(!buildMenu2.activeInHierarchy);
        buildMenu2.gameObject.transform.localPosition = new Vector3(395, -168, 0);
    }
    /*
    public void ToggleMenu3()
    {
        buildMenu3.gameObject.SetActive(!buildMenu3.activeInHierarchy);
        buildMenu3.gameObject.transform.localPosition = new Vector3(395, -168, 0);
    }
    */
    public void ToggleMenu4()
    {
        buildMenu4.gameObject.SetActive(!buildMenu4.activeInHierarchy);
        buildMenu4.gameObject.transform.localPosition = new Vector3(395, -168, 0);
    }
}
