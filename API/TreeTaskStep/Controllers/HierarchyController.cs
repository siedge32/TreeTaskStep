using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TreeTasksStep.Domain;
using TreeTasksStep.Persistance;
using TreeTaskStep.Dtos.Response;
using TreeTaskStep.Services;

namespace TreeTaskStep.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HierarchyController : ControllerBase
    {
        private readonly ILogger<HierarchyController> _logger;
        private readonly DataContext _context;
        private readonly IHierarchyBuilder<Step> _hierarchyBuilder;

        public HierarchyController(
            ILogger<HierarchyController> logger, 
            DataContext dataContext, 
            IHierarchyBuilder<Step> hierarchyBuilder)
        {
            _logger = logger;
            _context = dataContext;
            _hierarchyBuilder = hierarchyBuilder;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TreeTasksStep.Domain.Task>>> GetTasks()
            => Ok(await _context.Tasks.ToListAsync());

        [HttpGet("test")]
        public ActionResult Test()
        {
            var tasks = _context.Tasks.Include(t => t.Steps).ToList();
            return Ok();
        }

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
            var stepTree = _hierarchyBuilder.GetFlattenedListOfNodes().FirstOrDefault(node => node.Data.Id == id);

            if (stepTree is null)
            {
                _logger.LogError($"No step found with id {id}");
                return BadRequest();
            }

            return Ok(stepTree.Children.ToList().Select(step => new StepResponse
            {
                Id = step.Data.Id,
                Name = step.Data.Name
            }));
        }
    }
}
