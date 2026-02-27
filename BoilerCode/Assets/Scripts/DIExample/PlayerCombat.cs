using UnityEngine;
using Core.DI;

namespace Example
{
    public class PlayerCombat : MonoBehaviour
    {
        private IWeapon _primaryWeapon;
        private IWeapon _secondaryWeapon;

        private void Start()
        {
            // Safe resolution
            var audio = DI.Container.ResolveOrDefault<IAudioService>();

            // Named resolution
            _primaryWeapon = DI.Container.ResolveNamed<IWeapon>("primary");
            _secondaryWeapon = DI.Container.ResolveNamed<IWeapon>("secondary");

            // Check existence first
            if (DI.Container.HasBinding<IPlayerData>())
            {
                var playerData = DI.Container.Resolve<IPlayerData>();
            }
        }
    }
}