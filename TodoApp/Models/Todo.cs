using System;
namespace TodoApp.Models
{
    public class Todo
    {
        public Todo()
        {
            Id = new Guid();
            CreatedOn = DateTime.Now;
            IsCompleted = false;
        }


        public Guid Id { get; set; }
        public DateTime CreatedOn { get; private set; }
        public string Title { get; set; } = "Пустой title";
        public string Body { get; set; } = "Пустое боди";
        public bool IsCompleted { get; private set; }

    }
}
