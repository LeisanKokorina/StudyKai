/*
 * Created by SharpDevelop.
 * User: A7
 * Date: 05.05.2017
 * Time: 10:02
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace application
{
	/// <summary>
	/// Description of Errors.
	/// </summary>
	public static class Errors
    {
        //проверка ошибок. true - ошибок нет, false - есть ошибка
        public static bool CheckError(string expression, TextBox textBox)
        {
            //првоерим кол-во открывающих и закрывающих скобок
            int bracket = 0;

            for (int i = 0; i < expression.Length; i++)
            {
                if (expression[i] == '(')
                    bracket++;
                else if (expression[i] == ')')
                    bracket--;
                if (bracket < 0)
                {
                	MessageBox.Show("Ошибка! Скобки идут в неправильной последовательности.");
                	return false;
                }
            }
            if (bracket > 0)
            {
                MessageBox.Show("Ошибка! Количество открывающих и закрывающих скобок не совпадает.");
                return false;
            }
            /////////////////////////////

            //проверим, нет ли подряд идущих математических операций
            for (int i = 0; i < expression.Length - 1; i++)
                if ((expression[i] == '+' || expression[i] == '-' || expression[i] == '*' || expression[i] == ':') &&
                    (expression[i + 1] == '+' || expression[i + 1] == '-' || expression[i + 1] == '*' || 
                    expression[i + 1] == ':'))
                {
                    MessageBox.Show("Ошибка! Подряд идущие математические операции!");

                    expression = expression.Insert(i + 2, "   ");
                    expression = expression.Insert(i, "   ");
                    //вывод выражения с отмеченной ошибкой на экран
                    textBox.Text = expression;

                    return false;
                }
            ////////////////////////////////

            //проверим наличие открывающей скобки после математической функции
            for (int i = Const.GetStrNumber("sin"); i <= Const.GetStrNumber("sqrt"); i++) //перерибаем все функции
            {
                //индекс предыдущего нахождения функции в выражении
                int prevIndex = 0;

                while(true)
                {
                    prevIndex = expression.IndexOf(Const.str[i], prevIndex); //находим индекс вхождения начиная с prevIndex

                    if (prevIndex == -1)
                        break; //если -1, значит в выражении нет такой функции, выходим из while
                    else
                    {
                        if(expression[prevIndex + Const.str[i].Length] != '(') //если после имени функции не открывающая скобка
                        {
                            MessageBox.Show("Ошибка! После имени математической функции отсутствуют скобки!");
                            //визуально выделим это место в выражении
                            expression = expression.Insert(prevIndex + Const.str[i].Length, "   ");
                            //вывод выражения с отмеченной ошибкой на экран
                            textBox.Text = expression;

                            return false;
                        }
                    }
                    //увеличиваем на 1, чтобы в выражении изчать мат. функцию со следующего места
                    prevIndex++;
                }
            }

            ////////////////////////////////

            //проверим нет ли пустых скобок, т.е пустых аргументов
            for (int i = 0; i < expression.Length - 1; i++)
                if ((expression[i] == '(') && (expression[i + 1] == ')'))
                {
                    MessageBox.Show("Ошибка! Пустой аргумент!");

                    expression = expression.Insert(i + 2, "   ");
                    expression = expression.Insert(i, "   ");
                    //вывод выражения с отмеченной ошибкой на экран
                    textBox.Text = expression;

                    return false;
                }
            ////////////////////////////////
			return true;
        }
    }
}