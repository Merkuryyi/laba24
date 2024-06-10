namespace laba
{
    public class DailyPlanner 
    {
        public static void addTasks(string login)
        {
            using (var context = new ConnectDatabase())
            {
                Console.WriteLine("title: ");
                string title = Console.ReadLine();

                Console.WriteLine("description: ");
                string description = Console.ReadLine();

                Console.WriteLine("executeBefore (dd/MM/yyyy): ");
                DateTime executeBefore = DateTime.Parse(Console.ReadLine());

                context.Tasks.Add(new Task { Login = login, Title = title, 
                    Description = description, ExecuteBefore = executeBefore });
                context.SaveChanges();
            }
        }
        public static void deleteTasks(string login)
        {
            using (var context = new ConnectDatabase())
            {
                Console.WriteLine("id: ");
                int id = int.Parse(Console.ReadLine());
                var task = context.Tasks.FirstOrDefault(t => t.Id == id && t.Login == login);
                if (task != null)
                {
                    context.Tasks.Remove(task);
                    context.SaveChanges();
                }
            }
        }
        public static void updateTasks(string login)
        {
            using (var context = new ConnectDatabase())
            {
                Console.WriteLine("id задачи, которую нужно изменить: ");
                int id = int.Parse(Console.ReadLine());
        
                Console.WriteLine("title: ");
                string title = Console.ReadLine();
        
                Console.WriteLine("description: ");
                string description = Console.ReadLine();
        
                Console.WriteLine("executeBefore (dd/MM/yyyy) : ");
                DateTime executeBefore = DateTime.Parse(Console.ReadLine());
                Console.WriteLine(executeBefore);
                Task task = context.Tasks.FirstOrDefault(t => t.Id == id && t.Login == login);
                if (task != null)
                {
                    task.Title = title;
                    task.Description = description;
                    task.ExecuteBefore = executeBefore;
            
                    context.SaveChanges();
                }
            }
        }
        public static void tasksTimeToday(string login)
        {
            using (var context = new ConnectDatabase())
            {
                var tasksToday = context.Tasks.Where(t => t.ExecuteBefore.Date == DateTime.Today && t.Login == login).ToList();
                Console.WriteLine("Заметки на сегодня");
                foreach (var task in tasksToday)
                {
                    Console.WriteLine($"Id: {task.Id}, title: {task.Title}, description: {task.Description}, executeBefore: {task.ExecuteBefore}");
                }
            }
        }
        public static void tasksTimeTomorrow(string login)
        {
            using (var context = new ConnectDatabase())
            {
                var tasksTomorrow = context.Tasks.Where(t => t.ExecuteBefore.Date == DateTime.Today.AddDays(1) && t.Login == login).ToList();
                Console.WriteLine("Заметки на завтра");
                foreach (var task in tasksTomorrow)
                {
                    Console.WriteLine($"Id: {task.Id}, title: {task.Title}, description: {task.Description}, executeBefore: {task.ExecuteBefore}");
                }
            }
        }
        public static void tasksAll(string login)
        {
            using (var context = new ConnectDatabase())
            {
                var tasks = context.Tasks.Where(t => t.Login == login).ToList();
                foreach (var task in tasks)
                {
                    Console.WriteLine($"Id: {task.Id}, title: {task.Title}, description: {task.Description}, executeBefore: {task.ExecuteBefore}");
                }
            }
        }
        public static void tasksTimeWeek(string login)
        {
            using (var context = new ConnectDatabase())
            {
                DateTime thisDay = DateTime.Today;
                int dayToday = (int)thisDay.DayOfWeek;
                DateTime monday = thisDay.AddDays(-dayToday + 1);
                DateTime sunday = monday.AddDays(6);

                var tasks = context.Tasks.Where(t => t.ExecuteBefore >= monday && t.ExecuteBefore <= sunday && t.Login == login).ToList();

                Console.WriteLine("Заметки на неделю");
                foreach (var task in tasks)
                {
                    Console.WriteLine($"Id: {task.Id}, title: {task.Title}, description: {task.Description}, executeBefore: {task.ExecuteBefore}");
                }
            }
        }
        public static void tasksPast(string login)
        {
            using (var context = new ConnectDatabase())
            {
                var tasks = context.Tasks.Where(t => t.ExecuteBefore < DateTime.Today && t.Login == login).ToList();
                foreach (var task in tasks)
                {
                    Console.WriteLine($"Id: {task.Id}, title: {task.Title}, description: {task.Description}, executeBefore: {task.ExecuteBefore}");
                }
            }
        }
        public static void tasksUpcoming(string login)
        {
            using (var context = new ConnectDatabase())
            {
                var tasks = context.Tasks.Where(t => t.ExecuteBefore > DateTime.Today && t.Login == login).ToList();
                foreach (var task in tasks)
                {
                    Console.WriteLine($"Id: {task.Id}, title: {task.Title}, description: {task.Description}, executeBefore: {task.ExecuteBefore}");
                }
            }
        }
        public static bool authorization(string login, string password)
        {
            using (var context = new ConnectDatabase())
            {
                var users = context.Users.FirstOrDefault(u => u.Login == login && u.Password == password);
                if (users != null)
                {
                    return true;
                }
                return false;
            }
        }
        public static void registration(string login, string password)
        {
            using (var context = new ConnectDatabase())
            {
                var existingUsers = context.Users.Where(u => u.Login == login).FirstOrDefault();
                if (existingUsers == null)
                {
                    context.Users.Add(new Users { Login = login, Password = password });
                    context.SaveChanges();
                    Console.WriteLine("Готово");
                }
                else
                {
                    Console.WriteLine("Пользователь с таким логином уже существует");
                }
            }
        }
    }
}
