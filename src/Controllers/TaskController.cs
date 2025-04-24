using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TaskManagerApi.Contants;
using TaskManagerApi.Interfaces.Services;
using TaskManagerApi.Models;
using TaskManagerApi.Models.Response;

namespace TaskManagerApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _service;

        public TasksController(ITaskService service)
        {
            _service = service;
        }

        /// <summary>
        /// Cria uma nova tarefa.
        /// </summary>
        /// <remarks>
        /// Este endpoint cria uma nova tarefa com os dados fornecidos. Se a criação for bem-sucedida, retornará os detalhes da tarefa criada.
        /// </remarks>
        /// <param name="task">Objeto que contém os detalhes da tarefa a ser criada.</param>
        /// <returns>Retorna o status da criação da tarefa.</returns>
        /// <response code="200">Tarefa criada com sucesso.</response>
        /// <response code="400">Erro ao criar a tarefa.</response>
        [HttpPost]
        [SwaggerOperation(Summary = "Cria uma nova tarefa", Description = "Cria uma nova tarefa no sistema.")]
        [SwaggerResponse(200, "Tarefa criada com sucesso.")]
        [SwaggerResponse(400, "Erro ao criar a tarefa.")]
        public async Task<IActionResult> CreateTask([FromBody] TaskItem task)
        {
            try
            {
                if (task == null)
                {
                    return BadRequest("Tarefa inválida.");
                }

                await _service.CreateTaskAsync(task);
                var responseSuccess = BaseResponse.SuccessResponse(StringConstants.TaskCreated, task);
                return responseSuccess;
            }
            catch (Exception ex)
            {
                var responseError = BaseResponse.ErrorResponse($"{StringConstants.ErrorCreatingTask} {ex.Message}");
                return BadRequest(responseError);
            }
        }

        /// <summary>
        /// Obtém as tarefas de um usuário.
        /// </summary>
        /// <remarks>
        /// Este endpoint retorna todas as tarefas associadas a um usuário específico. Se o usuário não tiver tarefas, retornará um erro.
        /// </remarks>
        /// <param name="userId">ID do usuário cujas tarefas serão retornadas.</param>
        /// <returns>Retorna a lista de tarefas do usuário.</returns>
        /// <response code="200">Lista de tarefas do usuário.</response>
        /// <response code="404">Usuário não possui tarefas.</response>
        [HttpGet("{userId}")]
        [SwaggerOperation(Summary = "Obtém as tarefas de um usuário", Description = "Retorna todas as tarefas associadas ao usuário especificado.")]
        [SwaggerResponse(200, "Lista de tarefas do usuário.")]
        [SwaggerResponse(404, "Usuário não possui tarefas.")]
        public async Task<IActionResult> GetTaskByUser(string userId)
        {
            try
            {
                var tasks = await _service.GetTasksByUserAsync(userId);
                if (tasks == null || !tasks.Any())
                    return BaseResponse.NotFoundResponse(StringConstants.NoTasksForUser);

                return Ok(tasks);
            }
            catch (Exception ex)
            {
                return BaseResponse.ErrorResponse($"{StringConstants.ErrorFetchingTasks} {ex.Message}");
            }
        }

        /// <summary>
        /// Marca a tarefa como concluída.
        /// </summary>
        /// <remarks>
        /// Este endpoint permite marcar uma tarefa como concluída. Se a tarefa já estiver concluída, ele retorna um erro.
        /// </remarks>
        /// <param name="id">ID da tarefa que deseja concluir.</param>
        /// <returns>Retorna o status de sucesso ou erro.</returns>
        /// <response code="200">Tarefa concluída com sucesso.</response>
        /// <response code="400">Erro ao concluir a tarefa. (Tarefa já concluída ou outro erro).</response>
        /// <response code="404">Tarefa não encontrada.</response>
        [HttpPut("{id}/complete")]
        [SwaggerOperation(Summary = "Conclui uma tarefa", Description = "Marca a tarefa com o ID fornecido como concluída.")]
        [SwaggerResponse(200, "Tarefa concluída com sucesso.")]
        [SwaggerResponse(400, "Erro ao concluir a tarefa.")]
        [SwaggerResponse(404, "Tarefa não encontrada.")]
        public async Task<IActionResult> CompleteTask(string id)
        {
            try
            {
                var task = await _service.GetTaskByIdAsync(id);
                if (task == null)
                {
                    var response = BaseResponse.ErrorResponse(StringConstants.TaskNotFound);
                    return response;
                }

                if (task.IsCompleted)
                {
                    var response = BaseResponse.ErrorResponse(StringConstants.TaskAlreadyCompleted);
                    return response;
                }

                var success = await _service.CompleteTaskAsync(id);

                if (!success)
                {
                    var response = BaseResponse.ErrorResponse(StringConstants.ErrorCompletingTask);
                    return response;
                }

                var responseSuccess = BaseResponse.SuccessResponse(StringConstants.TaskCompleted);
                return responseSuccess;
            }
            catch (Exception ex)
            {
                var responseError = BaseResponse.ErrorResponse($"{StringConstants.ErrorCompletingTask} {ex.Message}");
                return responseError;
            }
        }

        /// <summary>
        /// Deleta uma tarefa.
        /// </summary>
        /// <remarks>
        /// Este endpoint permite deletar uma tarefa do sistema. Se a tarefa não for encontrada, retorna um erro.
        /// </remarks>
        /// <param name="id">ID da tarefa a ser deletada.</param>
        /// <returns>Retorna o status de sucesso ou erro.</returns>
        /// <response code="200">Tarefa deletada com sucesso.</response>
        /// <response code="400">Erro ao deletar a tarefa.</response>
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Deleta uma tarefa", Description = "Deleta a tarefa com o ID fornecido.")]
        [SwaggerResponse(200, "Tarefa deletada com sucesso.")]
        [SwaggerResponse(404, "Tarefa não encontrada.")]
        public async Task<IActionResult> DeleteTask(string id)
        {
            try
            {
                var success = await _service.DeleteTaskAsync(id);
                if (!success)
                    return BaseResponse.NotFoundResponse(StringConstants.TaskNotFound);

                return BaseResponse.OkResponse(StringConstants.TaskDeleted);
            }
            catch (Exception ex)
            {
                return BaseResponse.ErrorResponse($"{StringConstants.ErrorDeletingTask} {ex.Message}");
            }
        }
    }
}
