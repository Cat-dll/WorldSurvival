using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FishGame.Gfx;

namespace FishGame.Assets.Loader
{
    public class TextureLoader : AssetLoader<Texture2D>
    {
        private Texture2D? workTexture;

        public TextureLoader(/*Renderer renderer*/)
        {
            this.workTexture = null;
            //this.defaultAssets = new();
        } 

        public override Texture2D Load(AssetsManager pManager, string pFilePath, object? pInfo)
        {
            this.workTexture = pManager.BaseContentManager.Load<Texture2D>(pFilePath);

            return workTexture;
        }

        public override void Unload(Texture2D pAsset)
        {
            pAsset.Dispose();
        }
    }
}
