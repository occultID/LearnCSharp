using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnWPF
{
    public class LearnWPF
    {
        private static object objLock = new object();
        private static LearnWPF learnWPF = null;
        private LearnWPF()
        {
            MainWindow win = new MainWindow();
            win.Show(); 
        }

        public static void StartLearnWPF()
        {
            lock(objLock)
            {
                if (learnWPF is null)
                {
                    learnWPF = new LearnWPF();
                }
            }
        }
    }
}
