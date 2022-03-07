using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FishGame.Assets.Loader;

namespace FishGame.Assets
{
    public class AssetsManager
    {
        public ContentManager BaseContentManager { get; protected set; }

        protected Dictionary<Type, Dictionary<string, object>> Assets;

        protected Dictionary<Type, object> AssetLoaders;

        public string AssetsPath { get; private set; }

        public bool AllIsLoaded { get; private set; }

        protected Queue<AssetTask> Tasks { get; private set; }

        public AssetTask? CurrentTask { get; private set; }

        public AssetsManager(ContentManager pContentManager, string pAssetsDirectory)
        {
            this.BaseContentManager = pContentManager;
            this.AssetsPath = Path.Combine(Directory.GetCurrentDirectory(), pAssetsDirectory);

            this.Assets = new Dictionary<Type, Dictionary<string, object>>();
            this.AssetLoaders = new Dictionary<Type, object>();
            AddAssetLoader(typeof(Texture2D), new TextureLoader());

            this.Tasks = new Queue<AssetTask>();
            this.CurrentTask = null;

            this.AllIsLoaded = false;
        }

        public void Load<T>(string pAssetName, object? loaderParameters) where T : class
        {
            try
            {
                var loader = (AssetLoader<T>)AssetLoaders[typeof(T)];

                AssetTask task = new(pAssetName, new Task<T>(
                    () => loader.Load(this, pAssetName, loaderParameters)
                ));

                Tasks.Enqueue(task);
            }
            catch (InvalidCastException ex)
            {
                Console.Error.WriteLine("Cannot cast the asset loader!");
                Console.Error.WriteLine("\n\t" + ex.StackTrace);
                
                // TODO: Unload other assets
                Environment.Exit(-1);
            }
        }

        public void Unload<T>(T asset) where T : class
        {
            try
            {
                var assetType = typeof(T);
                AssetLoader<T>? loader = GetAssetLoader<T>();

                loader?.Unload(asset);
            }
            catch (KeyNotFoundException ex)
            {
                Console.Error.WriteLine("This type of asset is not supported. Failure to unload!");
                Console.Error.WriteLine("\n\t" + ex.StackTrace);

                Environment.Exit(-1);
            }
        }

        public void Update()
        {
            if (AllIsLoaded)
                return;

            if (CurrentTask == null)
                this.NextTask();
            else if (CurrentTask.IsCompleted)
            {
                switch (CurrentTask.Status)
                {
                    case TaskStatus.Faulted:
                        Console.Error.WriteLine("Failure to load {0}!", CurrentTask.AssetName);
                        break;
                    case TaskStatus.Canceled:
                        Console.Error.WriteLine("Loading {0} canceled!", CurrentTask.AssetName);
                        break;
                    default:
                        if (CurrentTask.Asset != null)
                            AddAsset(CurrentTask.AssetName, CurrentTask.Asset);
                        break;
                }

                if (!AllIsLoaded)
                    this.NextTask();

                AllIsLoaded = Tasks.Count <= 0;
            }
        }

        private void NextTask()
        {
            CurrentTask = Tasks.Dequeue();
            CurrentTask.Start();
        }

        // TODO: Generic revision of the methods..
        public void AddAssetLoader<T>(Type pAssetType, AssetLoader<T> assetLoader) where T : class
        {
            try
            {
                if (Assets.ContainsKey(pAssetType))
                    throw new ApplicationException($"'{pAssetType.Name}' asset type already exists!");
                Assets.Add(pAssetType, new Dictionary<string, object>());

                var type = assetLoader.GetType();

                if (AssetLoaders.ContainsKey(pAssetType))
                    throw new ApplicationException($"'{pAssetType.Name}' loader type already exists!");
                AssetLoaders.Add(pAssetType, assetLoader);   
            }
            catch (ApplicationException ex)
            {
                Console.Error.WriteLine(ex.Message);
            }
        }

        public void AddAsset(string pName, object pAsset)
        {
            var type = pAsset.GetType();

            try
            {
                if (!Assets.ContainsKey(type))
                    throw new ApplicationException($"Cannot add assets with '{type.Name}' type");

                Assets[type].Add(pName, pAsset);
            }
            catch (KeyNotFoundException ex)
            {

                Console.Error.WriteLine("Cannot found asset type: '{0}' !", type.Name);
                Console.Error.WriteLine("\n\t" + ex.StackTrace);

                // TODO: Unload other assets
                Environment.Exit(-1);
            }
        }

        public T? GetAsset<T>(string pName) where T : class
        {
            T? assets = null;

            var type = typeof(T);

            try
            {
                assets = (T)Assets[type][pName];
            }
            catch (KeyNotFoundException ex)
            {
                Console.Error.WriteLine("Cannot found asset with this name: '{0}'!", pName);
                Console.Error.WriteLine("\n\t" + ex.StackTrace);

                // TODO: Unload other assets
                Environment.Exit(-1);
            }
            catch (InvalidCastException ex)
            {
                Console.Error.WriteLine("Invalid asset type: '{0}'! Cannot cast asset!", type.Name);
                Console.Error.WriteLine("\n\t" + ex.StackTrace);

                Environment.Exit(-1);
            }

            return assets;
        }

        public AssetLoader<T>? GetAssetLoader<T>() where T : class
        {
            AssetLoader<T>? loader = null;

            var type = typeof(T);

            try
            {
                loader = (AssetLoader<T>)AssetLoaders[type];
            }
            catch (KeyNotFoundException ex)
            {
                Console.Error.WriteLine("Cannot found asset loader with this type: '{0}' !", type.Name);
                Console.Error.WriteLine("\n\t" + ex.StackTrace);

                // TODO: Unload other assets
                Environment.Exit(-1);
            }
            catch (InvalidCastException ex)
            {
                Console.Error.WriteLine("Cannot get correctly asset loader from type '{0}'!", type.Name);
                Console.Error.WriteLine("\n\t" + ex.StackTrace);

                // TODO: Unload other assets
                Environment.Exit(-1);
            }

            return loader;
        }
    }
}
