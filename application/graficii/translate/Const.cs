using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace application
{
    //в этом классе хранятся все константы проекта. классы, использующие данные константы, наследуют данный класс
    abstract class Const
    {
        //интервальная переменная. на данное значение заменяется переменная используемая в качестве шага при передаче
        //в метод, преобразующий обычное выражение в обратную польскую запись
        public static string INTERVAL_VARIABLE = "INTERVALVARIABLE";
        //допустимый "алфавит" выражения из цифр и математических операторов
        public static string[] str = {".", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "+", "-", ":", "*", "^", "sin", 
			"cos", "tg", "ctg", "arcsin", "arccos", "arctg", "arcctg", "ln", "lg", "log", "exp", "sqrt", "(", ")", ","};

        public static int GetStrNumber(string s)
        {
            return Array.IndexOf(str, s);
        }
    }
}