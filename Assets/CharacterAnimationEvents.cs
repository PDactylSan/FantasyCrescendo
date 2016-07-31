using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace HouraiTeahouse.SmashBrew {

    /// <summary>
    /// AnimationEvent callbacks for SmashBrew Characters
    /// </summary>
    public class CharacterAnimationEvents : CharacterComponent {

        public static string HitboxFunc = "Hitbox";

        public void Hitbox(AnimationEvent animationEvent) {
            var eventData = animationEvent.objectReferenceParameter as EventData;
            Log.Debug("{0} {1} {2}", eventData, gameObject, animationEvent.intParameter);
            if (Character == null) {
                Log.Error("A Character script for corresponding to {0} cannot be found.", name);
                return;
            }
            if (eventData == null) {
                Log.Error("Hitbox was called without a EventData state as a parameter");
                return;
            }
            HitboxKeyframe keyframe = eventData[animationEvent.intParameter];
            List<int> ids = eventData.IDs;
            List<Hitbox.Type> states = keyframe.States;
            Assert.AreEqual(ids.Count, states.Count);
            for (var i = 0; i < ids.Count; i++) {
                Hitbox hitbox = Character.GetHitbox(ids[i]);
                if(hitbox == null) {
                    Log.Error("No Hitbox on {0} with ID {1} cannot be found.", Character, ids[i]);
                    continue;
                }
                Log.Debug("{0} {1} {2}", i, hitbox, states[i]);
                hitbox.CurrentType = states[i];
            }
        }

        /// <summary> Actually applies the force to jump. </summary>
        public void Jump() {
            Character.JumpImpl();
        }

    }

}

