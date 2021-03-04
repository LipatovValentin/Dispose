using System;
using System.IO;
using System.Runtime.ConstrainedExecution;

namespace Dispose
{
    class Program
    {
        static void Main(string[] args)
        {
            using (CoolFileStreamer fileStreamer = new CoolFileStreamer("someFile.txt"))
            {
                // Do something
            }

            Console.ReadKey();
        }
    }

    public class CoolFileStreamer : CriticalFinalizerObject, IDisposable
    {
        private bool _disposed;
        private FileStream _fileStream;

        public CoolFileStreamer(string filePath)
        {
            this._fileStream = new FileStream(filePath, FileMode.OpenOrCreate);
        }
        ~CoolFileStreamer()
        {
            this.Dispose(false);
        }
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (this._disposed == true)
            {
                return;
            }
            else
            {
                if (disposing == true)
                {
                    this._fileStream?.Dispose();
                }
                
                // Dispose of any unmanaged resources
                
                this._disposed = true;
            }
        }
    }
}
