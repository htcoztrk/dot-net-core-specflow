using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevProject.Models
{
    internal class Todo
    {
        public int userId { get; set; }
        public int id { get; set; }
        public string title { get; set; }

        public bool completed { get; set; }

        public Todo(int userId, int id, string title, bool completed)
        {
            this.userId = userId;
            this.id = id;
            this.title = title;
            this.completed = completed;
        }

        public Todo()
        {
        }

    }
}
