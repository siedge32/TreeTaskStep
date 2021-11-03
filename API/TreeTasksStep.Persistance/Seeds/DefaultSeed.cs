using System;
using System.Collections.Generic;
using System.Linq;
using TreeTasksStep.Domain;

namespace TreeTasksStep.Persistance.Seeds
{
    public class DefaultSeed
    {
        public static void SeedData(DataContext dataContext)
        {
            if (!dataContext.Tasks.Any())
            {
                var tasks = new List<Task>
                {
                    new TreeTasksStep.Domain.Task()
                    {
                        Id = Guid.NewGuid(),
                        Name = "Task 1",
                        Steps = new List<Step>()
                    },

                    new TreeTasksStep.Domain.Task()
                    {
                        Id = Guid.NewGuid(),
                        Name = "Task 2",
                        Steps = new List<Step>()
                    },
                };

                var steps = new List<Step>
                {
                    new Step()
                    {
                        Id = Guid.NewGuid(),
                        Name = "Step 1a",
                        ParentStep = null,
                        ParentTask = tasks[0],
                    },
                    new Step()
                    {
                        Id = Guid.NewGuid(),
                        Name = "Step 1b",
                        ParentStep = null,
                        ParentTask = tasks[1],
                    },
                };

                steps[0].ChildrenSteps = new List<Step>()
                {
                    new Step()
                    {
                        Id = Guid.NewGuid(),
                        Name = "Step 1a.1a",
                        ParentStep = steps[0],
                        ParentTask = tasks[0],
                    },
                    new Step()
                    {
                        Id = Guid.NewGuid(),
                        Name = "Step 1a.2a",
                        ParentStep = steps[0],
                        ParentTask = tasks[0],
                    },
                };

                steps[1].ChildrenSteps = new List<Step>()
                {
                    new Step()
                    {
                        Id = Guid.NewGuid(),
                        Name = "Step 1b.1b",
                        ParentStep = steps[1],
                        ParentTask = tasks[0],
                    },
                    new Step()
                    {
                        Id = Guid.NewGuid(),
                        Name = "Step 1b.2b",
                        ParentStep = steps[1],
                        ParentTask = tasks[0],
                    },
                };

                tasks[0].Steps = new List<Step> { steps[0] };
                tasks[1].Steps = new List<Step> { steps[1] };

                dataContext.Tasks.AddRange(tasks);
                dataContext.SaveChanges();
            }
        }
    }
}
