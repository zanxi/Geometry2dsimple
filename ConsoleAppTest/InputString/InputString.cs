using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleAppTest
{

    class CustomStringComparer : IComparer<int>
    {
        public int Compare(int x, int y)
        {            
            return x-y;
        }
    }

    public class InputString
    {
        string input = "";
        int posErr = 0;
        public InputString(string input)
        {
            this.input = input;
            TryInput(out posErr);
        }

        bool TryInput(out int m)
        {
            m = 0;
            for (int i = 0; i < input.Length; i++)
            {
                if (!Char.IsDigit(input[i])&& !(input[i]==' '))
                {
                    m = i;
                    return false;
                }
            }
            return true;
        }

        public List<int> GetNumbers()
        {
            List<string> listBox1 = new List<string>();
            listBox1.AddRange(
                input.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries).
                Where(t => int.TryParse(t, out int num)).
                ToArray());
            List<int> listNumbers = new List<int>();
            foreach (string n in listBox1) listNumbers.Add(int.Parse(n));
            return listNumbers;
        }

        public int GetPosErr()
        {
            return posErr;
        }

    }
}
