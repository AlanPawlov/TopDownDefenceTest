using UnityEngine;
using Zenject;

public abstract class BaseFactory<T> where T : Object
{
    protected readonly DiContainer _diContainer;

    protected BaseFactory(DiContainer diContainer)
    {
        _diContainer = diContainer;
    }

    public virtual T Create(T prefab, Vector3 position = new Vector3(),Quaternion rotation = new Quaternion())
    {
        var obj = Object.Instantiate(prefab, position, rotation);
        _diContainer.Inject(obj);
        return obj;
    }
}