using System;
using System.Collections.Generic;

namespace KursovaPP._2
{
    public static class Extensions
    {

        //Work like .Split(" "). But if elements in [ ],then we don't split them. 
        public static string[] Filter(this string mainStr)
        {

            List<string> splitedElem = new List<string>();

            string temp = string.Empty;

            while (mainStr.Contains(' '))
            {
                int index = mainStr.IndexOf(' ');
                if (mainStr.StartsWith('['))
                {
                    if (!mainStr.Contains(']'))
                    {
                        Console.WriteLine("Incorrect format");
                        Program.DataProcess();
                    }
                    index = mainStr.IndexOf(']');
                    temp = Remove(index, ref mainStr);
                    temp = TrimElement(temp);
                }
                else
                {
                    temp = Remove(index, ref mainStr);
                    Validation(temp);
                }
                if (temp == "")
                    continue;
                splitedElem.Add(temp);

            }

            if (mainStr.StartsWith('['))
            {
                mainStr = TrimElement(mainStr);
            }

            Validation(mainStr);

            splitedElem.Add(mainStr);

           // if(splitedElem { Count: 5})
            return splitedElem.ToArray();

            
        }

        private static string Remove(int index, ref string str)
        {
            string temp = str.Remove(index);
            str = str.Remove(0, index + 1);
            return temp;
        }
        private static void Validation(string str)
        {
            if (str.StartsWith(']') || str.EndsWith('[') || str.EndsWith(']'))
            {
                Console.WriteLine("Incorrect format");
                Program.DataProcess();
            }
        }
        private static string TrimElement(string element)
        {
            return element.Trim(new char[] { '[', ']' });
        }

    }
}
