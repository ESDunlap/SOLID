using NUnit.Framework;
using UnityEngine;

public class Switcher : MonoBehaviour
{
    [SerializeField] private KeyCode switchButton;
    private IChangeable _changeableObject;

    private void Start()
    {
        _changeableObject = GetComponent<IChangeable>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(switchButton))
        {
            _changeableObject.Next();
        }
    }
}

public interface IChangeable
{
    void Next();
}