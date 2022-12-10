using System;
using System.Collections.Generic;
using System.Linq;


namespace ConsoleAppTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(" *************** Test Введения строк ****************");
                        
            while (true)
            {
                Console.Write("ВВедите строку чисел разделенных пробелами:");
                string input = Console.ReadLine();
                //Console.ReadLine();
                //string input = "  143 44    23       462    54 ";
                //string input = "    54 ";
                Console.WriteLine(input);

                InputString isInput = new InputString(input);

                if (isInput.GetPosErr() == 0) // нет ошибок в строке
                {
                    List<int> listNumber = isInput.GetNumbers();
                    if(listNumber.Count()<2)
                    {
                        Console.WriteLine("ВВедено не более одного числа. Повторите ввод");
                        isInput = null;
                        continue;
                    }
                    var selectedList = listNumber.OrderBy(u => u, new CustomStringComparer()).ToList();
                    Console.WriteLine("\nmin = "+selectedList[0]+ ";\nmax = " + selectedList[selectedList.Count()-1]);
                    Console.WriteLine("max - min = " + (selectedList[selectedList.Count() - 1] - selectedList[0]));
                    Console.WriteLine("n[0]"+ selectedList[0]+"; n[1]" + selectedList[1]);
                    break;
                }
                else
                {
                    Console.WriteLine("\nОшибка при вводе символа с номером <" + isInput.GetPosErr() + "> Повторите ввод");
                    isInput = null;
                    continue;
                }
            }

            Console.WriteLine("\n\n *************** Test действий с точкой и вектором ****************");
            
            TestUtil.Operation();

            Console.ReadKey();
        }
    }
}
