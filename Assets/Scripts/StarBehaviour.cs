using UnityEngine;

public class StarBehaviour : MonoBehaviour
{
    private int size = 1;
    public int Size => size;
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
