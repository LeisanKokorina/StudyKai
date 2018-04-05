using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace application
{
    class Variables : Const
    {
        private List<string[]> varList;

        public Variables()
        {
            varList = new List<string[]>();
        }

        //добавляет новую переменную
        public void AddVar(string newVar, string newValue)
        {
            varList.Add(new string[] { newVar, newValue });
        }

        //удаляет переменную с указанным именем
        public bool DeleteVar(string varName)
        {
            for (int i = 0; i < varList.Count; i++) //перебираем все переменные
                if (varList[i][0].Equals(varName)) //если имеется с указанным именем - удаляем её
                {
                    varList.RemoveAt(i);
                    return true;
                }

            return false;
        }

        //выводит все переменные
        public void Show(ListBox listBox)
        {
            listBox.Items.Clear();

            if (varList.Count == 0)
            {
                listBox.Items.Add("Список переменных пуст");
            }
            else
            {
                foreach(string[] v in varList) //перебирает все переменные и выводит каждую из них в консоль
                {
                    listBox.Items.Add(v[0] + " = " + v[1]);
                }
            }
        }

        //заменяет в выражении переменные на их значения
        public bool SetVar(ref string expression)
        {
            //отсортируем список с переменными по убыванию длины имени меременной
            VarComparer vc = new VarComparer();
            varList.Sort(vc);

            //найдём все позиции символов в которых нет мат операций со словами. (cos sin exp и т.д.)

            bool[] notOperation = new bool[expression.Length];

            for (int i = 0; i < expression.Length; i++)
                notOperation[i] = true;

            //переберём все мат операции со словами. (cos sin exp и т.д.)
            for (int k = GetStrNumber("sin"); k <= GetStrNumber("sqrt"); k++)
            {
                for (int i = 0; i < expression.Length - str[k].Length + 1; i++)
                {
                    int index = expression.IndexOf(str[k], i, str[k].Length);

                    if (index != -1)
                    {
                        for (int m = 0; m < str[k].Length; m++)
                            notOperation[index + m] = false;

                        i = index + str[k].Length - 1;
                    }
                }
            }
            //нашли.

            //теперь переберём все переменные, и если на позиции переменной все notOperation = true, то сделаем замену
            foreach(string[] s in varList)
            {
                for (int i = 0; i < expression.Length - s[0].Length + 1; i++)
                {
                    int index = expression.IndexOf(s[0], i, s[0].Length);

                    if (index != -1)
                    {
                        bool mayReplace = true; //можно заменять?

                        for (int m = 0; m < s[0].Length; m++)
                            if (!notOperation[index + m])
                            {
                                mayReplace = false;
                                break;
                            }
                        if (mayReplace)
                        {
                            //мб есть ошбика: переменная начинается с цифры, проверим это
                            if ((index != 0) && (expression[index - 1] >= '0' && expression[index - 1] <= '9'))
                            {
                                Console.WriteLine("Ошибка! Переменная начинается с цифры:");
                                expression = expression.Insert(index + s[0].Length, "   ");
                                expression = expression.Insert(index - 1, "   ");
                                Console.WriteLine(expression);
                                return false;
                            }

                            expression = expression.Remove(index, s[0].Length);

                            expression = expression.Insert(index, "(" + s[1] + ")");

                            //теперь нужно изменить размер массива notOperation, поскольку длины имени переменной
                            //и её содержимого могут быть различны   //+2 это потому что еще нужно учитывать скобки
                            if (s[0].Length > (s[1].Length + 2))
                            {
                                for (int q = 0; q < (s[0].Length - (s[1].Length + 2)); q++)
                                {
                                    for (int w = index; w < notOperation.Length - 1; w++)
                                        notOperation[w] = notOperation[w + 1];
                                }

                                Array.Resize(ref notOperation, notOperation.Length - (s[0].Length - (s[1].Length + 2)));
                            }
                            else if (s[0].Length < (s[1].Length + 2))
                            {
                                Array.Resize(ref notOperation, notOperation.Length + ((s[1].Length + 2) - s[0].Length));

                                for (int q = 0; q < ((s[1].Length + 2) - s[0].Length); q++)
                                {
                                    for (int w = notOperation.Length - 1; w > index; w--)
                                        notOperation[w] = notOperation[w - 1];
                                }
                            }
                            //заменим в notOperation место где была вставлена переменная на false
                            for (int q = index; q < (index + (s[1].Length + 2)); q++)
                                notOperation[q] = false;
                            i = index + (s[1].Length + 2) - 1;
                        }
                    }
                }
            }

            expression = expression.Replace("pi", Math.PI.ToString());

            return true; //всё прошло успешно. ошибок в переменных нет
        }
    }
    //правило сравнения элементов списка с переменными. сравнивать будем по длине, сортируем по её убыванию
    public class VarComparer: IComparer<string[]>
    {
        public int Compare(string[] x, string[] y)
        {
            if (x[0].Length > y[0].Length)
                return -1;
            else if ((x[0].Length < y[0].Length))
                return 1;
            else
                return 0;
        }
    }
}