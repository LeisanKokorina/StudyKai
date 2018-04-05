using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace application
{
    //в этом классе хранятся части вычисляемого выражения в ОПЗ
    class PartsOPZ : Const
    {
        struct part
        {
            public string a;
            public string b;
            public string operation;
        }

        List<part> parts; //список частей

        List<double?> results; //список для результатов каждой части

        //созвращает длину списка частей
        public int Length()
        {
            return parts.Count;
        }

        public PartsOPZ()
        {
            parts = new List<part>();

            results = new List<double?>();
        }

        //добавление новой части
        public void Add(string _a, string _b, string _operation)
        {
            parts.Add(new part { a = _a, b = _b, operation = _operation });

            results.Add(null);
        }

        //показать все части выражения
        public void Show(ListBox listBox)
        {
            string s;

            for (int i = 0; i < parts.Count; i++)
            {
                if (parts[i].b != null)
                    s = parts[i].a + " " + parts[i].b + " " + parts[i].operation;
                else
                    s = parts[i].a + " " + parts[i].operation;

                listBox.Items.Add(s + " = " + results[i]);
            }
        }

        //возвращает i-ю часть в виде строки в формате ОПЗ
        public string GetOPZExpressionFromPart(int i)
        {
            if (parts[i].b == null) //один операнд в выражении
                return parts[i].a + " " + parts[i].operation;
            else //два
                return parts[i].a + " " + parts[i].b + " " + parts[i].operation;
        }

        //вычисляет результат выражения по структуре
        public double? Calculate()
        {
            for (int p = 0; p < parts.Count; p++) //перебираем все части
            {
                string[] elements = GetOPZExpressionFromPart(p).Split(' '); //получаем часть в виде ОПЗ и разбиваем в массив по пробелу

                while (elements.Length > 1) //до тех пор пока не останется 1 элемент в массиве (это будет всё выражение части)
                {
                    for (int i = 0; i < elements.Length; i++) //перебираем все элементы части
                    {
                        int k = GetStrNumber(elements[i]); //номер элемента в массиве str

                        if (k >= GetStrNumber("+") && k <= GetStrNumber("^")) //операции требуется 2 операнда
                        {
                            elements[i - 2] = CalcPart(Convert.ToDouble(elements[i - 2]),
                                Convert.ToDouble(elements[i - 1]), k).ToString(); //вычисляем

                            for (int j = i - 1; j < elements.Length - 2; j++)
                                elements[j] = elements[j + 2]; //затираем два элемента в массиве

                            Array.Resize(ref elements, elements.Length - 2); //уменьшаем размер массива на 2

                            break;
                        }

                        if (k >= GetStrNumber("sin") && k <= GetStrNumber("sqrt")) //операции требуется 1 операнд
                        {
                            elements[i - 1] = CalcPart(Convert.ToDouble(elements[i - 1]), 0, k).ToString(); //аналогично

                            for (int j = i; j < elements.Length - 1; j++)
                                elements[j] = elements[j + 1];

                            Array.Resize(ref elements, elements.Length - 1);

                            break;
                        }
                    }
                }

                results[p] = Convert.ToDouble(elements[0]); //сохраняем результат вычисления части
            }

            return results[results.Count - 1]; //результат вычисления последней части - это рез-т всего выражения. возвращаем его
        }

        //вычисляет одну операцию
        private double CalcPart(double a, double b, int k)
        {
            if (k == GetStrNumber("+")) //+
            {
                return a + b;
            }
            if (k == GetStrNumber("-")) //-
            {
                return a - b;
            }
            if (k == GetStrNumber(":")) //:
            {
                return a / b;
            }
            if (k == GetStrNumber("*")) //*
            {
                return a * b;
            }
            if (k == GetStrNumber("^")) //^
            {
                return Math.Pow(a, b);
            }
            if (k == GetStrNumber("sin")) //sin
            {
                return Math.Sin(a);
            }
            if (k == GetStrNumber("cos")) //cos
            {
                return Math.Cos(a);
            }
            if (k == GetStrNumber("tg")) //tg
            {
                return Math.Tan(a);
            }
            if (k == GetStrNumber("ctg")) //ctg
            {
                return 1 / Math.Tan(a);
            }
            if (k == GetStrNumber("arcsin")) //arcsin
            {
                return Math.Asin(a);
            }
            if (k == GetStrNumber("arccos")) //arccos
            {
                return Math.Acos(a);
            }
            if (k == GetStrNumber("arctg")) //arctg
            {
                return Math.Atan(a);
            }
            if (k == GetStrNumber("arcctg")) //arcctg
            {
                return Math.PI / 2 - Math.Atan(a);
            }
            if (k == GetStrNumber("ln")) //ln
            {
                return Math.Log(a);
            }
            if (k == GetStrNumber("lg")) //lg
            {
                return Math.Log10(a);
            }
            if (k == GetStrNumber("exp")) //exp
            {
                return Math.Exp(a);
            }
            if (k == GetStrNumber("sqrt")) //sqrt
            {
                return Math.Sqrt(a);
            }

            return 0;
        }
    }
}
