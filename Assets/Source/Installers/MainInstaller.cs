using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MainInstaller : MonoInstaller
{
    [SerializeField] private CharacterView _characterView;

    private List<ISubscribable> _subscribables = new List<ISubscribable>();

    public override void InstallBindings()
    {
        Bind(new PlayerInputController());
        Bind(new CharacterPresenter(new Character(), _characterView));
    }

    private void OnEnable()
    {
        foreach (var item in _subscribables)
            item.Subscribe();
    }

    private void OnDisable()
    {
        foreach (var item in _subscribables)
            item.Unsubscribe();
    }

    private T Bind<T>(T subscribable)
        where T : ISubscribable
    {
        _subscribables.Add(subscribable);
        Container.BindInterfacesAndSelfTo<T>().FromInstance(subscribable).AsSingle();
        return subscribable;
    }
}
