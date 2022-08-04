using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoServiceManager.helpers
{
    class Helper
    {
        private static bool _ShowPassword = false;
        public static bool ShowPassword {
            get {
                _ShowPassword = !_ShowPassword;
                return !_ShowPassword;
            }
            set { _ShowPassword = value; }
        }

        private static Bitmap _PasswordImage = Properties.Resources.view;
        public static Bitmap PasswordImage {
            get {
                _PasswordImage = _ShowPassword ? Properties.Resources.hidden : Properties.Resources.view;
                return _PasswordImage;
            }
            set { _PasswordImage = value; }
        }

        // Список форм в бэкстеке (минимизированное хранилище всех форм)
        // чтобы они умещались в один список, надо их объединить одним классом
        // BaseForm.
        // первоначально сам список приватный, чтобы скрыть к нему доступ
        private static List<BaseForm> _BackStack = new List<BaseForm>();
        
        // а доступ к бэкстеку реализовываем через отдельный параметр
        public static BaseForm BackStack
        {
            get
            {
                // при гет вызове параметра (гет когда мы что-то ждем от переменной
                // в данном случаем значение, а не передаем ее через равно или еще как)
                // но нам нужная последняя форма из бэкстека, которая была в него заложена
                // а так как мы уже её забрали, то нужно и удалить
                // это делается для того, чтобы туда, где мы запросим, вернулась последняя форма
                // и мы открыли ее, как переходя по кнопке Назад.
                var last = _BackStack.Last();
                _BackStack.Remove(last);
                return last;
            }
            set
            {
                // сеттер - просто добавляем переданную форму в список и скрываем её
                // чтобы не скрывать постоянно в коде
                _BackStack.Add(value);
                _BackStack.Last().Hide();
            }
        }

        // метод выхода, изначально переменная равна тру, значит мы хотим
        // закрыть приложение, а чтобы это сделать, нам понадобится самая первая
        // форма из бэкстека, ну а если мы явно передадим false, то откроется
        // самая последняя форма, которая была скрыта (не закрыта)
        public static void Quit(bool IsClose = true)
        {
            /* Проверка параметра закрытия, и возвращается у нас Action, 
             * проще говоря, это событие или даже действие, которое 
             * запускается через вызов ()
             * Аналогично:
             * if (IsClose) {
             *     _BackStack.First().Close();
             * } else {
             *     BackStack.Show();
             * }
             */
            (IsClose ? (Action)_BackStack.First().Close : BackStack.Show)();
        }

        public static int UserId = 0;
        public static int OrderId = 0;
    }
}
