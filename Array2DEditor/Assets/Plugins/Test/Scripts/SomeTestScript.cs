using UnityEngine;

public class SomeTestScript : MonoBehaviour
{
    [SerializeField]
    public SomeClassInt someInt;

    [SerializeField]
    private SomeClassString someString;

    public void Reset()
    {
        someInt.Reset();
        someString.Reset();
    }

    public void DoStuff()
    {
        someInt.DoStuff();
        someString.DoStuff();
    }

    public void AddOne()
    {
        someInt.Value++;
    }
}
