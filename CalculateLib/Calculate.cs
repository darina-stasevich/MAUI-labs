// CalculateLib.cs
using System.Threading;

namespace CalculateLib
{
    public class Calculate
    {
        private readonly SemaphoreSlim _semaphore = new(1, 1);
        
        public delegate void ProgressHandler(int progress);
        public event ProgressHandler? OnProgressEvent;

        public async Task<double> CalculateIntAsync(CancellationToken ct)
        {
            await _semaphore.WaitAsync(ct);

            try
            {
                double value = 0;
                const double step = 0.0000001;
                const int totalIterations = 10000000;

                for (int i = 0; i < totalIterations; i++)
                {
                    ct.ThrowIfCancellationRequested();
                    
                    value += Math.Sin(step * i) * step;

                    for (int j = 0; j < 100; j++)
                    {
                        var temp = 1 * 10;
                    }

                    if (i % 100000 == 0)
                    {
                        var progress = (i * 100) / totalIterations;
                        OnProgressEvent?.Invoke(progress);
                        await Task.Delay(1, ct);
                    }
                }

                OnProgressEvent?.Invoke(100);
                return value;
            }
            finally
            {
                _semaphore.Release();
            }
        }
    }
}