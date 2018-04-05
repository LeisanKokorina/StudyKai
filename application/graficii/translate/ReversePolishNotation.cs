using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace application
{
   class ReversePolishNotation : Const
    {
        //переводит обычную запись в обратную польскую
        public static string ConvertToReversePolishNotation(string expression)
        {
            string output = "";
            Stack<string> stack = new Stack<string>();

            //если минус в начале: например -2+3
            if (expression.Length > 0)
                if (expression[0] == '-')
                    expression = str[GetStrNumber("0")] + expression;

            //Пока есть ещё символы для чтения
            while (expression.Length > 0)
            {
                //Если символ является символом интервальной переменной INTERVAL_VARIABLE, то добавляем его к выходной строке
                int expressionLengthUnderThisCheck = expression.Length;
                if (expression.Length >= INTERVAL_VARIABLE.Length)
                    if (expression.IndexOf(INTERVAL_VARIABLE, 0, INTERVAL_VARIABLE.Length) == 0)
                    {
                        output += " " + INTERVAL_VARIABLE + " ";
                        expression = expression.Remove(0, INTERVAL_VARIABLE.Length);
                    }
                if (expressionLengthUnderThisCheck != expression.Length) //если длина изменилась, т.е. INTERVAL_VARIABLE была найдена
                    continue;
                //Читаем очередной символ. получаем его номер из массива str
                int k = GetStrNumber(expression[0].ToString());
                //Если символ является числом, добавляем его к выходной строке
                if (k >= GetStrNumber(".") && k <= GetStrNumber("9")) //встречена цифра или точка (в дроби, например: 2.5)
                {
                    int i = 0; //индекс конца числа

                    do
                    {
                        i++;

                        if (i == expression.Length)
                            break;

                        k = GetStrNumber(expression[i].ToString());

                    } while (k >= GetStrNumber(".") && k <= GetStrNumber("9")); //пока идут цифры или точка

                    output += " " + expression.Substring(0, i) + " ";
                    expression = expression.Remove(0, i);
                    continue;
                }
                //Если символ является символом функции, помещаем его в стек
                expressionLengthUnderThisCheck = expression.Length;
                for (int i = GetStrNumber("sin"); i <= GetStrNumber("sqrt"); i++)
                    if (expression.Length >= str[i].Length)
                        if (expression.IndexOf(str[i], 0, str[i].Length) == 0)
                        {
                            stack.Push(str[i]);
                            expression = expression.Remove(0, str[i].Length);
                            break;
                        }
                if (expressionLengthUnderThisCheck != expression.Length) //если длина изменилась, т.е. функция была найдена
                    continue;
                //Если символ является открывающей скобкой, помещаем его в стек
                if (k == GetStrNumber("("))
                {
                    stack.Push(str[k]);
                    expression = expression.Remove(0, 1);

                    //если минус перед скобкой: например (-2+3)*2
                    if (expression.Length > 0)
                        if (expression[0] == '-')
                            expression = str[GetStrNumber("0")] + expression;

                    continue;
                }
                //Если символ является закрывающей скобкой
                if (k == GetStrNumber(")"))
                {
                    //До тех пор, пока верхним элементом стека не станет открывающая скобка, выталкиваем элементы из стека 
                    //в выходную строку. При этом открывающая скобка удаляется из стека, но в выходную строку не добавляется. 
                    //Если стек закончился раньше, чем мы встретили открывающую скобку, это означает, что в выражении либо 
                    //неверно поставлен разделитель, либо не согласованы скобки.
                    string s;
                    while (!(s = stack.Pop()).Equals(str[GetStrNumber("(")]))
                        output += " " + s + " ";
                    expression = expression.Remove(0, 1);

                    //если текущий верхний элемент стека функция, то его также необходимо извлечь и добавить в выходную строку
                    if (stack.Count > 0)
                    {
                        k = GetStrNumber(stack.Peek());
                        if (k >= GetStrNumber("sin") && k <= GetStrNumber("sqrt"))
                            output += " " + stack.Pop() + " ";
                    }

                    continue;
                }
                //Если символ является оператором о1, тогда:
                if (k >= GetStrNumber("+") && k <= GetStrNumber("^"))
                {
                    //1) пока
                    //приоритет o1 меньше либо равен приоритету оператора, находящегося на вершине стека
                    //выталкиваем верхний элемент стека в выходную строку
                    while (stack.Count > 0 && CheckPriority(k, stack))
                        output += " " + stack.Pop() + " ";
                    //2) помещаем оператор o1 в стек
                    stack.Push(str[k]);

                    expression = expression.Remove(0, 1);
                    continue;
                }
                //ЕСЛИ ДОШЛИ ДО СЮДА - ЗНАЧИТ В ВЫРАЖЕНИИ ОШИБКА, ЕГО НЕ РАСПАРСИТЬ
                MessageBox.Show("Некорректный символ! Либо отсутствует переменная, либо ошибка в имени функции.");
                throw new Exception();
            }
            //Когда входная строка закончилась, выталкиваем все символы из стека в выходную строку. 
            //В стеке должны были остаться только символы операторов; если это не так, значит в выражении 
            //не согласованы скобки.
            while (stack.Count > 0)
                output += " " + stack.Pop() + " ";

            //удаляем лишние пробелы
            output = output.Replace("  ", " ");
            if (output.Length > 0)
                if (output[0] == ' ')
                    output = output.Remove(0, 1);
            if (output.Length > 0)
                if (output[output.Length - 1] == ' ')
                    output = output.Remove(output.Length - 1, 1);

            return output;
        }

        //сравнение приоритетов операций
        private static bool CheckPriority(int o1, Stack<string> stack)
        {
            //Приоритеты:
            // ^ высокий = 3
            // * : средний = 2
            // + - низкий = 1
            // ( ) самый низкий = 0

            if (o1 == GetStrNumber("+"))
                o1 = 1;
            else if (o1 == GetStrNumber("-"))
                o1 = 1;
            else if (o1 == GetStrNumber(":"))
                o1 = 2;
            else if (o1 == GetStrNumber("*"))
                o1 = 2;
            else if (o1 == GetStrNumber("^"))
                o1 = 3;

            int oTop = -1; //операция в вершине стека

            int oTopStrNumber = GetStrNumber(stack.Peek()); //номер операции из вершины стека в массиве str

            if (oTopStrNumber == GetStrNumber("+"))
                oTop = 1;
            else if (oTopStrNumber == GetStrNumber("-"))
                oTop = 1;
            else if (oTopStrNumber == GetStrNumber(":"))
                oTop = 2;
            else if (oTopStrNumber == GetStrNumber("*"))
                oTop = 2;
            else if (oTopStrNumber == GetStrNumber("^"))
                oTop = 3;
            else if (oTopStrNumber == GetStrNumber("("))
                oTop = 0;
            else if (oTopStrNumber == GetStrNumber(")"))
                oTop = 0;

            if (oTop == -1)
                return false;

            if (o1 <= oTop)
                return true;
            else
                return false;
        }

        //разделение выражения в ОПЗ на части в виде списка структур
        public static void MakeParts(string expression, PartsOPZ pOPZ)
        {
            string[] elements = expression.Split(' '); //разделяем выражение по пробелу на части

            if (elements.Length == 1) //если всего один элемент в массиве - значит введено просто одно число
            {
                pOPZ.Add(elements[0], str[GetStrNumber("0")], str[GetStrNumber("+")]); //добавим в список частей это число + 0, чтобы можно было вычислить часть
                return;
            }
            //пока не останется один элемент в массиве (он будет сожержать всё выражение)
            int elementsLengthBeforeWhileIteration; //кол-во элементов в массиве до прохождения итерации while, если по окончании итерации elements.Length не изменилось - значит в выражении ошибка

            while (elements.Length > 1)
            {
                elementsLengthBeforeWhileIteration = elements.Length;

                for (int i = 0; i < elements.Length; i++) //перебираем элементы и ищем мат. операции и функции
                {
                    int k = GetStrNumber(elements[i]);

                    if (k >= GetStrNumber("+") && k <= GetStrNumber("^")) //операции требуется 2 операнда
                    {
                        pOPZ.Add(elements[i - 2], elements[i - 1], elements[i]); //добавляем в список часть выражения

                        for (int j = i - 1; j < elements.Length - 2; j++) //передвигаем элементы массива, чтобы затереть добавленное выражение
                            elements[j] = elements[j + 2];

                        elements[i - 2] = pOPZ.GetOPZExpressionFromPart(pOPZ.Length() - 1); //в первый операнд заносим добавленное выражение в формате ОПЗ

                        Array.Resize(ref elements, elements.Length - 2); //уменьшаем размер массива на 2

                        break;
                    }

                    if (k >= GetStrNumber("sin") && k <= GetStrNumber("sqrt")) //операции требуется 1 операнд
                    {
                        pOPZ.Add(elements[i - 1], null, elements[i]); //тут аналогично

                        for (int j = i; j < elements.Length - 1; j++)
                            elements[j] = elements[j + 1];

                        elements[i - 1] = pOPZ.GetOPZExpressionFromPart(pOPZ.Length() - 1);

                        Array.Resize(ref elements, elements.Length - 1);

                        break;
                    }
                }

                if (elementsLengthBeforeWhileIteration == elements.Length) //если кол-во элементов не изменилось - значит ошибка в выражении
                {
                    MessageBox.Show("Ошибка в выражении!");

                    throw new Exception();
                }
            }
        }
        
    }
}