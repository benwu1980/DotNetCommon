
using System.Diagnostics;
using System.Web;

namespace DotNetCommon.Web
{
    /// <summary>
    /// web页面的简单计时器,计算一个页面生成的时间
    /// </summary>
    public class MiniProfiler
    {
        private const string CacheKey = "_miniprofiler_";
        private readonly Stopwatch watch;

        public MiniProfiler()
        {
            watch = new Stopwatch();
        }

        public static MiniProfiler Current
        {
            get
            {
                HttpContext context = HttpContext.Current;
                var profiler = context.Items[CacheKey] as MiniProfiler;
                if (profiler == null)
                {
                    profiler = new MiniProfiler();
                    context.Items[CacheKey] = profiler;
                }
                return profiler;
            }
        }

        /// <summary>
        /// 执行时间，只是个大概，真正的执行时间会比这个更长。
        /// </summary>
        public decimal UsedTime
        {
            get
            {
                if (watch.IsRunning)
                {
                    watch.Stop();
                }

                return watch.ElapsedMilliseconds;
            }
        }

        public void Start()
        {
            watch.Start();
        }

        public void Stop()
        {
            if (watch.IsRunning)
                watch.Stop();
        }
    }
}
