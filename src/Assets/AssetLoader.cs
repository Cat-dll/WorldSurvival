using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishGame.Assets
{
    public abstract class AssetLoader<T> where T : class
    {
        protected T? defaultAssets = null;


        public abstract T Load(AssetsManager pManager, string pFilePath, object? pInfo);

        public abstract void Unload(T pAsset);
    }
}
