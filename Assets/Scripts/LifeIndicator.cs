using UnityEngine;
using UnityEngine.UI;

public class LifeIndicator : MonoBehaviour {
    [SerializeField]
    private Image heart1;
    [SerializeField]
    private Image heart2;
    [SerializeField]
    private Image heart3;
    

    void Start()
    {
        if (heart1 == null)
        {
            Debug.LogError("STATUS INDICATOR: No health heart 1  object referenced");
        }
        if (heart2 == null)
        {
            Debug.LogError("STATUS INDICATOR: No health heart 2  object referenced");
        }
        if (heart2 == null)
        {
            Debug.LogError("STATUS INDICATOR: No health heart 3 object referenced");
        }
        
    }

    public void SetHealth(int _value)
    {
        //float _value = (float)_cur / _max;
        Debug.Log("VIDA : " + _value);
        if(_value > 65)
        {
            
        }
        else if(_value <= 65 && _value > 34)
        {
            //heart1.gameObject.SetActive(true);
            //heart2.gameObject.SetActive(true);
            heart3.gameObject.SetActive(false);
        }else if(_value <= 34 && _value > 0)
        {
            //heart1.gameObject.SetActive(true);
            heart2.gameObject.SetActive(false);
            heart3.gameObject.SetActive(false);
        }else
        {
            heart1.gameObject.SetActive(false);
            heart2.gameObject.SetActive(false);
            heart3.gameObject.SetActive(false);
        }
        
    }

    public void Reset()
    {
        heart1.gameObject.SetActive(true);
        heart2.gameObject.SetActive(true);
        heart3.gameObject.SetActive(true);
    }
}
