﻿
#if SCOR_ENABLE_VISUALSCRIPTING
using System;
using UnityEngine;
using Unity.VisualScripting;
using StudioScor.Utilities.VisualScripting;

namespace StudioScor.AbilitySystem.VisualScripting
{
    public abstract class AbilitySpecLevelChangedEventUnit : CustomEventUnit<GameObjectAbilitySpec, OnChangedLevelValue>
    {
        [DoNotSerialize]
        [PortLabel("Current Level")]
        public ValueOutput CurrentLevel { get; private set; }

        [DoNotSerialize]
        [PortLabel("Prev Level")]
        public ValueOutput PrevLevel { get; private set; }
        public override Type MessageListenerType => typeof(AbilitySpecMessageListener);

        protected override void Definition()
        {
            base.Definition();

            CurrentLevel = ValueOutput<int>(nameof(CurrentLevel));
            PrevLevel = ValueOutput<int>(nameof(PrevLevel));
        }

        protected override void AssignArguments(Flow flow, OnChangedLevelValue data)
        {
            flow.SetValue(CurrentLevel, data.CurrentLevel);
            flow.SetValue(PrevLevel, data.PrevLevel);
        }
    }
}

#endif