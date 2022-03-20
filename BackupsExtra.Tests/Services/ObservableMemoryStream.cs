using System;
using System.IO;

namespace BackupsExtra.Tests.Services
{
    public class ObservableMemoryStream : MemoryStream
    {
        public event Action<byte[]> OnDispose;

        public override long Length => ToArray().Length;

        protected override void Dispose(bool disposing)
        {
            OnDispose(base.ToArray());
            base.Dispose(disposing);
        }
    }
}
