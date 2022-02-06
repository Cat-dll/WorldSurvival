using System;
using System.Collections.Generic;
using System.Linq;

namespace WorldSurvival.Gfx.Animation
{   
    public class AnimationData
    {
        protected Dictionary<string, Animation> AnimationTracks;

        public AnimationData()
        {
            this.AnimationTracks = new Dictionary<string, Animation>();
        }

        public Animation GetAnimation(string name) 
        {
            try
            { 
                return AnimationTracks[name];
            }
            catch (KeyNotFoundException)
            {
                // TODO: Remplace by logging api
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("[WARN] - Cannot get animation '{0}'!", name);
                Console.ResetColor();

                WorldSurvival.Close();
                return null;
            }
        }

        public Animation GetAnimation() => GetAnimation(AnimationTracks.Keys.First());

        public void AddAnimation(string name, Animation animation) => AnimationTracks.Add(name, animation);
    }
}
