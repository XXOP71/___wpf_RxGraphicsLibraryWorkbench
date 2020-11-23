namespace RxGraphicsLibrary.Tools
{
    using System.Diagnostics;
    using System.IO;
    using System.Text;



    public static class RxLog
    {
        public static void Trace(string tmsg)
        {
            Debug.WriteLine(tmsg);
        }


        /*
        private static StreamWriter _fs = null;

        public static void Clear()
        {
            if (_fs == null) return;
            try { _fs.Close(); }
            catch { }
            _fs = null;

            Trace.Listeners.Clear();
        }

        public static void Init(string tdp)
        {
            if (_fs == null)
            {
                try
                {
                    string tfp = Path.Combine(tdp, "Log.txt");
                    _fs = new StreamWriter(tfp, false, Encoding.UTF8);
                    TextWriterTraceListener t_twtl = new TextWriterTraceListener();
                    t_twtl.Writer = _fs;
                    Trace.Listeners.Add(t_twtl);
                    Trace.AutoFlush = true;
                }
                catch { }
            }
        }

        public static bool Enabled = true;

        public static void Test(string tmsg)
        {
            if (Enabled)
                Trace.WriteLine(tmsg);
        }
        */
    }
}
