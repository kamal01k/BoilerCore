using UnityEngine;
using Core.DI;

namespace Example
{
    public class GameSetup : MonoBehaviour
    {
        private void Awake()
        {
            // Different binding types
            DI.Container.Bind<IAudioService>(() => new AudioService());
            DI.Container.BindSingleton<IPlayerData>(() => new PlayerData());
            DI.Container.BindNamed<IWeapon>("primary", () => new Sword());
            DI.Container.BindNamed<IWeapon>("secondary", () => new Bow());

            // Instance binding
            DI.Container.BindInstance(new GameConfig());
        }

        private void OnDestroy()
        {
            // Clean up when needed
            DI.Container.RemoveBinding<GameConfig>();
        }
    }
}