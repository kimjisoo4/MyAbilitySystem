﻿
#if SCOR_ENABLE_VISUALSCRIPTING
using Unity.VisualScripting;
using UnityEngine;

namespace StudioScor.AbilitySystem.VisualScripting
{
    [DisableAnnotation]
    [AddComponentMenu("")]
    [IncludeInSettings(false)]
    public sealed class AbilitySystemMessageListener : MessageListener
    {
        private void Start()
        {
            var abilitySystem = GetComponent<AbilitySystemComponent>();

            abilitySystem.OnGrantedAbility += AbilitySystem_OnGrantedAbility;
            abilitySystem.OnRemovedAbility += AbilitySystem_OnRemovedAbility;
            abilitySystem.OnActivatedAbility += AbilitySystem_OnActivatedAbility;
            abilitySystem.OnFinishedAbility += AbilitySystem_OnFinishedAbility;
        }
        private void OnDestroy()
        {
            if(TryGetComponent(out AbilitySystemComponent abilitySystem))
            {
                abilitySystem.OnGrantedAbility -= AbilitySystem_OnGrantedAbility;
                abilitySystem.OnRemovedAbility -= AbilitySystem_OnRemovedAbility;
                abilitySystem.OnActivatedAbility -= AbilitySystem_OnActivatedAbility;
                abilitySystem.OnFinishedAbility -= AbilitySystem_OnFinishedAbility;
            }
        }

        private void AbilitySystem_OnFinishedAbility(AbilitySystemComponent abilitySystemComponent, IAbilitySpec abilitySpec)
        {
            EventBus.Trigger(AbilitySystemWithVisualScriptingEvent.ABILITYSYSTEM_FINISH_ABILITY, gameObject, abilitySpec);
        }

        private void AbilitySystem_OnActivatedAbility(AbilitySystemComponent abilitySystemComponent, IAbilitySpec abilitySpec)
        {
            EventBus.Trigger(AbilitySystemWithVisualScriptingEvent.ABILITYSYSTEM_ACTIVE_ABILITY, gameObject, abilitySpec);
        }

        private void AbilitySystem_OnRemovedAbility(AbilitySystemComponent abilitySystemComponent, IAbilitySpec abilitySpec)
        {
            EventBus.Trigger(AbilitySystemWithVisualScriptingEvent.ABILITYSYSTEM_REMOVE_ABILITY, gameObject, abilitySpec);
        }

        private void AbilitySystem_OnGrantedAbility(AbilitySystemComponent abilitySystemComponent, IAbilitySpec abilitySpec)
        {
            EventBus.Trigger(AbilitySystemWithVisualScriptingEvent.ABILITYSYSTEM_GRANT_ABILITY, gameObject, abilitySpec);
        }
    }
}
#endif