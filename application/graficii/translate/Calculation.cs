 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace application
{
	//класс вычисляет выражение
    static class Calculation
    {
        //без переменной
        public static double? Calc(string OPZ)
        {
            PartsOPZ pOPZ = new PartsOPZ(); //тут будут хранится части вычисляемого выражения в ОПЗ

            ReversePolishNotation.MakeParts(OPZ, pOPZ); //разделяем ОПЗ-выражение на части в список структур

            return pOPZ.Calculate(); //вычисляем и возвращаем результат
        }

        //с переменной
        public static double? Calc(string OPZ, string varValue)
        {
            //вычисляем выражение в ОПЗ
            return Calc(OPZ.Replace(Const.INTERVAL_VARIABLE, varValue));
        }

        public static string ConvertToReversePolishNotation(string expression)
        {
            //т.к. log(a, b) = ln(a):ln(b)
            //заменяем log в выражении на ln
            //это позволит правильно парсить и вычислять log(выражение, основание) 
            expression = expression.Replace("log", "ln");
            //заменяем запятую в выражении на скобки ):ln(
            //это позволит правильно парсить и вычислять log(выражение, основание) 
            expression = expression.Replace(",", "):ln(");

            //удаляем пробелы
            expression = expression.Replace(" ", "");
            //получаем обратную польскую запись
            return ReversePolishNotation.ConvertToReversePolishNotation(expression);
        }
    }
}
