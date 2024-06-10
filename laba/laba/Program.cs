using System;
namespace laba
{
    public class Programm
    {
        static void Main()
        {
            Console.WriteLine("login");
            string login = Console.ReadLine();
            Console.WriteLine("password");
            string password = Console.ReadLine();
       
            Console.WriteLine("Авторизация - 1, регистрация - 2");
            int com = int.Parse(Console.ReadLine());
         
            if (com == 1)
            {
                DailyPlanner.authorization(login, password);
                if (DailyPlanner.authorization(login, password))
                {
                    while (true)
                    {
                        Console.WriteLine("Введите команду:\n" +
                                          "0 - выйти\n" +
                                          "1 - добавить задачу, 2 - удалить задачу, 3 - редактировать задачу\n" +
                                          "Показать задачи на 4 - сегодня, 5 - завтра, 6 - неделю\n" +
                                          "Показать все задачи - 7 , прошедшие - 8, будущие - 9");
                        int comm = int.Parse(Console.ReadLine());
                        if (comm == 0)
                        {
                            break;
                        }
                        switch (comm)
                        {
                            case 1:
                                DailyPlanner.addTasks(login);
                                break;
                            case 2:
                                DailyPlanner.deleteTasks(login);
                                break;
                            case 3:
                                DailyPlanner.updateTasks(login);
                                break;
                            case 4:
                                DailyPlanner.tasksTimeToday(login);
                                break;
                            case 5:
                                DailyPlanner.tasksTimeTomorrow(login);
                                break;
                            case 6:
                                DailyPlanner.tasksTimeWeek(login);
                                break;
                            case 7:
                                DailyPlanner.tasksAll(login);
                                break;
                            case 8:
                                DailyPlanner.tasksPast(login);
                                break;
                            case 9:
                                DailyPlanner.tasksUpcoming(login);
                                break;
                            default:
                                Console.WriteLine("Команда не найдена");
                                break;
                        }
                    }
                }
                if (!DailyPlanner.authorization(login, password))
                {
                    Console.WriteLine("Логин или пароль неверны");
                }
            }
            else if (com == 2)
            {
                DailyPlanner.registration(login, password);
            }
            else
            {
                 Console.WriteLine("Команда не найдена");
            }
        }
    }
}
