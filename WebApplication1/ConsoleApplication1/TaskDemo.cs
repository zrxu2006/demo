using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class TaskDemo
    {
        static SemaphoreSlim _sem = new SemaphoreSlim(3); 
        public static void Start(string[] args)
        {
            //var dayName = Task.Run<string>(() => { return GetDayOfThisWeek(); });
            
            var dayName = Task.Run<string>(() => {
                Console.WriteLine("dayName：Thread Id {0}", Thread.CurrentThread.ManagedThreadId);
                return GetDayOfThisWeek();
            });

            dayName.GetAwaiter().OnCompleted(() =>
            {
                Console.WriteLine("今天是：{0}", dayName.Result);
            });

            //var dayName1 = Task.Run<string>(() =>
            //{
            //    Console.WriteLine("dayName1：Thread Id {0}", Thread.CurrentThread.ManagedThreadId);
            //    return GetDayOfThisWeekAsync();
            //});
            //Console.WriteLine("今天是：{0}", dayName.Result);
            Console.WriteLine("1今天是：{0}", DateTime.Now);
            //for (int i = 1; i <= 5; i++)
            //{
            //    //Task.Factory.StartNew(() =>
            //    //{                    
            //    //    Enter1(i);
            //    //});

            //    Task.Run(() =>
            //    {
            //        Enter1(i);
            //    });

            //    new Task(() =>
            //    {
            //        Enter1(i);
            //    }).Start();
            //    Enter(i).Start();
            //}
            Console.ReadLine();
        }

        private static string GetDayOfThisWeek()
        {
            
            Thread.Sleep(5000);
            Console.WriteLine("GetDayOfThisWeek：Thread Id {0}", Thread.CurrentThread.ManagedThreadId);
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        static void Enter1(object id)
        {
            Console.WriteLine(id + " 开始排队...");
            _sem.Wait();
            Console.WriteLine(id + " 开始执行！");
            Thread.Sleep(1000 * (int)id);
            Console.WriteLine(id + " 执行完毕，离开！");
            _sem.Release();
        }

        static Task Enter(int i)
        {
            return new Task(() =>
            {
                Console.WriteLine(i + " 开始排队...");
                _sem.Wait();
                Console.WriteLine(i + " 开始执行！");
                Thread.Sleep(5000);
                Console.WriteLine(i + " 执行完毕，离开！");
                _sem.Release();
            });
        }

        static Task<string> GetDayOfThisWeekAsync()
        {
            Console.WriteLine("GetDayOfThisWeekAsync：Thread Id {0}", Thread.CurrentThread.ManagedThreadId);
            //return new Task<string>(GetDayOfThisWeek);
            Console.WriteLine("GetDayOfThisWeekAsync：Thread Id {0}", Thread.CurrentThread.ManagedThreadId);
            var tsk = Task.Run<string>(() =>
            {
                Console.WriteLine("tsk：Thread Id {0}", Thread.CurrentThread.ManagedThreadId);
                return GetDayOfThisWeek();
            });

            //await tsk;
            Console.WriteLine("今天是：{0}", tsk.Result);

            return tsk;
        }
    }
}
