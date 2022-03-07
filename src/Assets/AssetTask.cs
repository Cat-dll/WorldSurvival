using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishGame.Assets
{
    public class AssetTask
    {
        private Task CurrentTask { get; set; }

        public object? Asset { get; set; }

        public string AssetName { get; set; }

        public DateTime startTime { get; private set; }

        public bool IsCompleted
        {
            get => CurrentTask.IsCompleted;
        }

        public TaskStatus Status
        {
            get => CurrentTask.Status;
        }

        public AssetTask(string pAssetName, Task pTask)
        {
            this.Asset = null;
            this.AssetName = pAssetName;
            this.CurrentTask = pTask;

            this.startTime = new();
        }

        public void Start()
        {
            startTime = DateTime.Now;
            CurrentTask.Start();
        }
    }
}
