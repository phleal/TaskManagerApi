using Swashbuckle.AspNetCore.Annotations;

namespace TaskManagerApi.Models
{
    /// <summary>
    /// Representa uma tarefa no sistema.
    /// </summary>
    public class TaskItem
    {
        /// <summary>
        /// Identificador único da tarefa.
        /// </summary>
        /// <example>1</example>
        [SwaggerSchema(Description = "Identificador único da tarefa.")]
        public string Id { get; set; }

        /// <summary>
        /// Título da tarefa.
        /// </summary>
        /// <example>Comprar leite</example>
        [SwaggerSchema(Description = "Título da tarefa.")]
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Descrição detalhada da tarefa.
        /// </summary>
        /// <example>Ir ao supermercado e comprar leite e ovos.</example>
        [SwaggerSchema(Description = "Descrição detalhada da tarefa.")]
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Data e hora de criação da tarefa.
        /// </summary>
        /// <example>2025-04-23T15:30:00Z</example>
        [SwaggerSchema(Description = "Data e hora de criação da tarefa.")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Data limite para conclusão da tarefa.
        /// </summary>
        /// <example>2025-04-30T23:59:59Z</example>
        [SwaggerSchema(Description = "Data limite para conclusão da tarefa.")]
        public DateTime DueDate { get; set; }

        /// <summary>
        /// Identificador único do usuário associado à tarefa.
        /// </summary>
        /// <example>12345</example>
        [SwaggerSchema(Description = "Identificador único do usuário associado à tarefa.")]
        public string UserId { get; set; } = string.Empty;

        /// <summary>
        /// Indica se a tarefa foi concluída ou não.
        /// </summary>
        /// <example>false</example>
        [SwaggerSchema(Description = "Indica se a tarefa foi concluída ou não.")]
        public bool IsCompleted { get; set; } = false;
    }
}
