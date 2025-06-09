using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MainInstaller : MonoInstaller
{
    [SerializeField] private PlayerCharacterView _characterView;

    private List<IActivatable> _activatables = new List<IActivatable>();
    private List<IUpdatable> _updatables = new List<IUpdatable>();

    public override void InstallBindings()
    {
        PlayerCharacter playerCharacter = new PlayerCharacter();

        var input = Bind(new PlayerInputController());
        Bind(new PlayerCharacterPresenter(playerCharacter, _characterView, input));
    }

    private void OnEnable()
    {
        foreach (var item in _activatables)
            item.Enable();
    }

    private void OnDisable()
    {
        foreach (var item in _activatables)
            item.Disable();
    }

    private void Update()
    {
        foreach (var item in _updatables)
            item.Update(Time.deltaTime);
    }

    private T Bind<T>(T item)
    {
        if (item is IActivatable activatable)
            _activatables.Add(activatable);

        if (item is IUpdatable updatable)
            _updatables.Add(updatable);

        Container.BindInterfacesAndSelfTo<T>().FromInstance(item).AsSingle();
        return item;
    }
}
