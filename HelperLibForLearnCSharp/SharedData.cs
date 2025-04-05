using System;
using System.Collections.Generic;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelperLibForLearnCSharp
{
    public class SharedData
    {
        private static readonly string MutexName = @"Global\MySharedMemory_Mutex";//互斥体名称
        private static readonly string MapName = "SharedMemory";//共享内存名称
        private const int MapSize = sizeof(int);//共享内存大小
        private static readonly MemoryMappedFile mmf;//共享内存对象
        private static readonly MemoryMappedViewAccessor accessor;//共享内存访问器
        private static readonly Mutex mutex;//互斥体对象

        public static Mutex Mutex => mutex;//互斥体对象
        public static MemoryMappedViewAccessor Accessor => accessor;//共享内存访问器
        public static MemoryMappedFile Mmf => mmf;//共享内存对象

        static SharedData()
        {
            mmf = MemoryMappedFile.CreateOrOpen(MapName, MapSize);//创建或打开共享内存
            accessor = mmf.CreateViewAccessor();//创建共享内存访问器

            bool createdNew;
            mutex = new Mutex(false, MutexName, out createdNew);//创建互斥体
        }

        public static int GetData()
        {
            return accessor.ReadInt32(0);
        }

        public static void SetData(int value)
        {
            accessor.Write(0, value);
            accessor.Flush();
        }

        public static int SetAngGetDataByMutex(int value)
        {
            int data = 0;
            try
            {
                if (mutex.WaitOne(1000))
                {
                    accessor.Write(0, value);
                    accessor.Flush();
                    data = accessor.ReadInt32(0);
                }
                else
                {
                    Console.WriteLine("Failed to acquire mutex.");
                    return data;
                }
            }
            finally
            {
                mutex.ReleaseMutex();
            }

            return data;
        }

        public static void Dispose()
        {
            accessor.Dispose();
            mmf.Dispose();
            mutex.Dispose();
        }
    }
}
