using UnityEngine;

public class StarBehaviour : MonoBehaviour
{
    private int size = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    
    public void SetSize(int newSize)
    {
        size = newSize;
        transform.localScale = Vector3.one * (size switch
        {
            1 => 1f,
            3 => 2f,
            9 => 3f,
        });
    }
}
