using System.ComponentModel.DataAnnotations;

namespace TaskManager.API.Model
{
    public class TaskModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        public bool IsComplete { get; set; } = false;
    }
}
