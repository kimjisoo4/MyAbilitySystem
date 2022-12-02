﻿using UnityEngine;
using System.Linq;
using System.Collections.Generic;

namespace KimScor.GameplayTagSystem.Ability
{
    public abstract class ActiveAbilitySpec : AbilitySpec
    {
        public ActiveAbilitySpec(Ability ability, AbilitySystem owner, int level) : base(ability, owner, level)
        {
            _ActiveAbility = ability as ActiveAbility;
        }

        private ActiveAbility _ActiveAbility;
        public ActiveAbility ActiveAbility => _ActiveAbility;
        public FAbilityTags AbilityTags => ActiveAbility.AbilityTags;


        public override void ForceActiveAbility()
        {
            AbilitySystem.OnCancelAbility(AbilityTags.CancelAbilityTags);

            GameplayTagSystem.AddOwnedTags(AbilityTags.AddOwnedTags);
            GameplayTagSystem.AddBlockTags(AbilityTags.AddBlockTags);

            base.ForceActiveAbility();
        }

        public override void EndAbility()
        {
            base.EndAbility();

            GameplayTagSystem.RemoveOwnedTags(AbilityTags.AddOwnedTags);
            GameplayTagSystem.RemoveBlockTags(AbilityTags.AddBlockTags);
        }

        public override void ForceCancelAbility()
        {
            base.OnCancelAbility();

            GameplayTagSystem.RemoveOwnedTags(AbilityTags.AddOwnedTags);
            GameplayTagSystem.RemoveBlockTags(AbilityTags.AddBlockTags);
        }

        public override bool CanActiveAbility()
        {
            // 어빌리티 태그가 블록 되어 있는가
            if (Ability.Tag is not null
                && GameplayTagSystem.ContainBlockTag(Ability.Tag))
            {
                Log("어빌리티가 블록되어 있음.");

                return false;
            }

            // 속성 태그 중에 블록되어 있는게 있는가
            if (Ability.AttributeTags is not null 
                && GameplayTagSystem.ContainOnceTagsInBlock(Ability.AttributeTags.ToArray()))
            {
                Log("어빌리티의 속성이 블록되어 있음.");

                return false;
            }

            // 해당 태그가 모두 존재하고 있는가
            if (AbilityTags.RequiredTags is not null 
                && !GameplayTagSystem.ContainAllTagsInOwned(AbilityTags.RequiredTags))
            {
                Log("필수 태그를 소유하고 있지 않음");

                return false;
            }

            // 해당 태그를 가지고 있는가
            if (AbilityTags.ObstacledTags is not null
                && GameplayTagSystem.ContainOnceTagsInOwned(AbilityTags.ObstacledTags))
            {
                Log("방해 태그를 소유하고 있음");

                return false;
            }

            return true;
        }
    }



}
