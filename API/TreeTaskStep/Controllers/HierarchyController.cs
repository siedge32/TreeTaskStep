using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TreeTasksStep.Persistance;
using TreeTaskStep.Dtos.Response;

namespace TreeTaskStep.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HierarchyController : ControllerBase
    {
        private readonly ILogger<HierarchyController> _logger;
        private readonly DataContext _context;

        public HierarchyController(ILogger<HierarchyController> logger, DataContext dataContext)
        {
            _logger = logger;
            _context = dataContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TreeTasksStep.Domain.Task>>> GetTasks()
            => Ok(await _context.Tasks.ToListAsync());

        [HttpGet("task/steps/{id}")]
        public ActionResult<IEnumerable<StepResponse>> GetStepsOfTask(Guid id)
        {
            var task = _context.Tasks.Find(id);

            if (task is null)
            {
                _logger.LogError($"No task found with id {id}");
                return BadRequest();
            }

            var steps = task.Steps.Select(step => new StepResponse
            {
                Id = step.Id,
                Name = step.Name
            });

            return Ok(steps);
        }

        [HttpGet("step/substep/{id}")]
        public ActionResult<IEnumerable<StepResponse>> GetSubStepsOfStep(Guid id)
        {
            var step = _context.Steps.Find(id);

            if (step is null)
            {
                _logger.LogError($"No step found with id {id}");
                return BadRequest();
            }

            var steps = step.ChildrenSteps.Select(step => new StepResponse
            {
                Id = step.Id,
                Name = step.Name
            });

            return Ok(steps);
        }
    }
}
